using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BoardingHouse
{
    public static class ReportsDataService
    {
        public static async Task<ReportsPayload> GetReportsAsync(DateTime from, DateTime to, int? boardingHouseId)
        {
            var fromDate = from.Date;
            var toDate = to.Date;
            if (toDate < fromDate)
                toDate = fromDate;

            var fromDateTime = fromDate;
            var toExclusive = toDate.AddDays(1);

            using var conn = DbConnectionFactory.CreateConnection();
            EnsureOpen(conn);

            var payload = new ReportsPayload
            {
                From = fromDate,
                To = toDate,
                SelectedBoardingHouseId = boardingHouseId,
                BoardingHouses = await LoadBoardingHouseOptionsAsync(conn),
                SelectedOwner = boardingHouseId.HasValue
                    ? await LoadOwnerForBoardingHouseAsync(conn, boardingHouseId.Value)
                    : null
            };

            var paymentSummary = await LoadPaymentsSummaryAsync(conn, fromDateTime, toExclusive, boardingHouseId);
            payload.TotalCollections = paymentSummary.TotalCollections;
            payload.TotalPaymentsCount = paymentSummary.TotalPaymentsCount;

            payload.ActiveRentalsCount = await LoadActiveRentalsCountAsync(conn, fromDate, toDate, boardingHouseId);

            var occupancySummary = await LoadOccupancySummaryAsync(conn, boardingHouseId);
            payload.OccupancyOccupied = occupancySummary.Occupied;
            payload.OccupancyAvailable = occupancySummary.Available;
            payload.OccupancyMaintenance = occupancySummary.Maintenance;
            payload.OccupancyRate = occupancySummary.TotalRooms <= 0
                ? 0m
                : Math.Round((decimal)occupancySummary.Occupied * 100m / occupancySummary.TotalRooms, 2);

            payload.MonthlyTrend = await LoadMonthlyTrendAsync(conn, fromDateTime, toExclusive, boardingHouseId);
            payload.CollectionsRows = await LoadCollectionsRowsAsync(conn, fromDateTime, toExclusive, boardingHouseId);
            payload.OccupancyByHouse = await LoadOccupancyByHouseAsync(conn, boardingHouseId);

            return payload;
        }

        private static void EnsureOpen(MySqlConnection conn)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
        }

        private static async Task<List<BoardingHouseOption>> LoadBoardingHouseOptionsAsync(MySqlConnection conn)
        {
            var result = new List<BoardingHouseOption>();

            const string sql = @"
                SELECT id, name
                FROM boarding_houses
                ORDER BY name;";

            using var cmd = new MySqlCommand(sql, conn);
            using var reader = (MySqlDataReader)await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(new BoardingHouseOption
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Name = reader["name"]?.ToString() ?? "(Unnamed)"
                });
            }

            return result;
        }

        private static async Task<OwnerInfo?> LoadOwnerForBoardingHouseAsync(MySqlConnection conn, int boardingHouseId)
        {
            const string sql = @"
                SELECT
                    o.lastname,
                    o.firstname,
                    o.middlename,
                    o.contact_no,
                    o.email
                FROM boarding_houses bh
                LEFT JOIN owners o ON o.id = bh.owner_id
                WHERE bh.id = @bhId
                LIMIT 1;";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@bhId", boardingHouseId);

            using var reader = (MySqlDataReader)await cmd.ExecuteReaderAsync();
            if (!await reader.ReadAsync())
                return null;

            var last = reader["lastname"] == DBNull.Value ? "" : reader["lastname"]?.ToString() ?? "";
            var first = reader["firstname"] == DBNull.Value ? "" : reader["firstname"]?.ToString() ?? "";
            var middle = reader["middlename"] == DBNull.Value ? "" : reader["middlename"]?.ToString() ?? "";

            if (string.IsNullOrWhiteSpace(last) && string.IsNullOrWhiteSpace(first) && string.IsNullOrWhiteSpace(middle))
                return null;

            return new OwnerInfo
            {
                FullName = FormatName(last, first, middle),
                ContactNo = reader["contact_no"] == DBNull.Value ? "" : reader["contact_no"]?.ToString() ?? "",
                Email = reader["email"] == DBNull.Value ? "" : reader["email"]?.ToString() ?? ""
            };
        }

        private static async Task<(decimal TotalCollections, int TotalPaymentsCount)> LoadPaymentsSummaryAsync(
            MySqlConnection conn,
            DateTime from,
            DateTime toExclusive,
            int? boardingHouseId)
        {
            var sql = @"
                SELECT
                    COALESCE(SUM(p.amount), 0) AS total_collections,
                    COUNT(*) AS payments_count
                FROM payments p
                JOIN rentals ren ON ren.id = p.rental_id
                JOIN rooms rm ON rm.id = ren.room_id
                JOIN boarding_houses bh ON bh.id = rm.boarding_house_id
                JOIN occupants o ON o.id = ren.occupant_id
                WHERE p.status = 'POSTED'
                  AND p.payment_date >= @from
                  AND p.payment_date < @toExclusive";

            if (boardingHouseId.HasValue)
                sql += " AND bh.id = @bhId";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@from", from);
            cmd.Parameters.AddWithValue("@toExclusive", toExclusive);
            if (boardingHouseId.HasValue)
                cmd.Parameters.AddWithValue("@bhId", boardingHouseId.Value);

            using var reader = (MySqlDataReader)await cmd.ExecuteReaderAsync();
            if (!await reader.ReadAsync())
                return (0m, 0);

            var totalCollections = reader["total_collections"] == DBNull.Value
                ? 0m
                : Convert.ToDecimal(reader["total_collections"]);
            var totalPaymentsCount = reader["payments_count"] == DBNull.Value
                ? 0
                : Convert.ToInt32(reader["payments_count"]);

            return (totalCollections, totalPaymentsCount);
        }

        private static async Task<int> LoadActiveRentalsCountAsync(
            MySqlConnection conn,
            DateTime from,
            DateTime to,
            int? boardingHouseId)
        {
            var sql = @"
                SELECT COUNT(*)
                FROM rentals ren
                JOIN rooms rm ON rm.id = ren.room_id
                WHERE ren.status = 'ACTIVE'
                  AND ren.start_date <= @toDate
                  AND (ren.end_date IS NULL OR ren.end_date >= @fromDate)";

            if (boardingHouseId.HasValue)
                sql += " AND rm.boarding_house_id = @bhId";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@fromDate", from);
            cmd.Parameters.AddWithValue("@toDate", to);
            if (boardingHouseId.HasValue)
                cmd.Parameters.AddWithValue("@bhId", boardingHouseId.Value);

            var result = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(result ?? 0);
        }

        private static async Task<(int Occupied, int Available, int Maintenance, int TotalRooms)> LoadOccupancySummaryAsync(
            MySqlConnection conn,
            int? boardingHouseId)
        {
            var sql = @"
                SELECT
                    SUM(CASE WHEN rm.status = 'OCCUPIED' THEN 1 ELSE 0 END) AS occupied,
                    SUM(CASE WHEN rm.status = 'AVAILABLE' THEN 1 ELSE 0 END) AS available,
                    SUM(CASE WHEN rm.status = 'MAINTENANCE' THEN 1 ELSE 0 END) AS maintenance,
                    COUNT(*) AS total_rooms
                FROM rooms rm
                WHERE 1 = 1";

            if (boardingHouseId.HasValue)
                sql += " AND rm.boarding_house_id = @bhId";

            using var cmd = new MySqlCommand(sql, conn);
            if (boardingHouseId.HasValue)
                cmd.Parameters.AddWithValue("@bhId", boardingHouseId.Value);

            using var reader = (MySqlDataReader)await cmd.ExecuteReaderAsync();
            if (!await reader.ReadAsync())
                return (0, 0, 0, 0);

            return (
                reader["occupied"] == DBNull.Value ? 0 : Convert.ToInt32(reader["occupied"]),
                reader["available"] == DBNull.Value ? 0 : Convert.ToInt32(reader["available"]),
                reader["maintenance"] == DBNull.Value ? 0 : Convert.ToInt32(reader["maintenance"]),
                reader["total_rooms"] == DBNull.Value ? 0 : Convert.ToInt32(reader["total_rooms"])
            );
        }

        private static async Task<List<MonthlyTrendPoint>> LoadMonthlyTrendAsync(
            MySqlConnection conn,
            DateTime from,
            DateTime toExclusive,
            int? boardingHouseId)
        {
            var result = new List<MonthlyTrendPoint>();

            var sql = @"
                SELECT
                    YEAR(p.payment_date) AS yr,
                    MONTH(p.payment_date) AS mo,
                    COALESCE(SUM(p.amount), 0) AS amount
                FROM payments p
                JOIN rentals ren ON ren.id = p.rental_id
                JOIN rooms rm ON rm.id = ren.room_id
                JOIN boarding_houses bh ON bh.id = rm.boarding_house_id
                JOIN occupants o ON o.id = ren.occupant_id
                WHERE p.status = 'POSTED'
                  AND p.payment_date >= @from
                  AND p.payment_date < @toExclusive";

            if (boardingHouseId.HasValue)
                sql += " AND bh.id = @bhId";

            sql += @"
                GROUP BY YEAR(p.payment_date), MONTH(p.payment_date)
                ORDER BY YEAR(p.payment_date), MONTH(p.payment_date);";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@from", from);
            cmd.Parameters.AddWithValue("@toExclusive", toExclusive);
            if (boardingHouseId.HasValue)
                cmd.Parameters.AddWithValue("@bhId", boardingHouseId.Value);

            using var reader = (MySqlDataReader)await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var year = Convert.ToInt32(reader["yr"]);
                var month = Convert.ToInt32(reader["mo"]);
                var monthDate = new DateTime(year, month, 1);

                result.Add(new MonthlyTrendPoint
                {
                    MonthLabel = monthDate.ToString("MMM yyyy", CultureInfo.InvariantCulture),
                    Amount = reader["amount"] == DBNull.Value ? 0m : Convert.ToDecimal(reader["amount"])
                });
            }

            return result;
        }

        private static async Task<List<CollectionRow>> LoadCollectionsRowsAsync(
            MySqlConnection conn,
            DateTime from,
            DateTime toExclusive,
            int? boardingHouseId)
        {
            var result = new List<CollectionRow>();

            var sql = @"
                SELECT
                    p.payment_date,
                    p.amount,
                    p.status,
                    bh.name AS boarding_house_name,
                    rm.room_no,
                    rm.room_type,
                    o.full_name,
                    o.lastname,
                    o.firstname,
                    o.middlename
                FROM payments p
                JOIN rentals ren ON ren.id = p.rental_id
                JOIN rooms rm ON rm.id = ren.room_id
                JOIN boarding_houses bh ON bh.id = rm.boarding_house_id
                JOIN occupants o ON o.id = ren.occupant_id
                WHERE p.status = 'POSTED'
                  AND p.payment_date >= @from
                  AND p.payment_date < @toExclusive";

            if (boardingHouseId.HasValue)
                sql += " AND bh.id = @bhId";

            sql += " ORDER BY p.payment_date DESC;";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@from", from);
            cmd.Parameters.AddWithValue("@toExclusive", toExclusive);
            if (boardingHouseId.HasValue)
                cmd.Parameters.AddWithValue("@bhId", boardingHouseId.Value);

            using var reader = (MySqlDataReader)await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var roomNo = reader["room_no"] == DBNull.Value ? "" : reader["room_no"]?.ToString() ?? "";
                var roomType = reader["room_type"] == DBNull.Value ? "" : reader["room_type"]?.ToString() ?? "";
                var roomNameOrNumber = BuildRoomLabel(roomNo, roomType);

                var fullName = reader["full_name"] == DBNull.Value ? "" : reader["full_name"]?.ToString() ?? "";
                if (string.IsNullOrWhiteSpace(fullName))
                {
                    var last = reader["lastname"] == DBNull.Value ? "" : reader["lastname"]?.ToString() ?? "";
                    var first = reader["firstname"] == DBNull.Value ? "" : reader["firstname"]?.ToString() ?? "";
                    var middle = reader["middlename"] == DBNull.Value ? "" : reader["middlename"]?.ToString() ?? "";
                    fullName = FormatName(last, first, middle);
                }

                result.Add(new CollectionRow
                {
                    PaymentDate = reader["payment_date"] == DBNull.Value
                        ? DateTime.MinValue
                        : Convert.ToDateTime(reader["payment_date"]),
                    BoardingHouseName = reader["boarding_house_name"]?.ToString() ?? "(Unknown)",
                    RoomNameOrNumber = roomNameOrNumber,
                    TenantFullName = fullName,
                    Amount = reader["amount"] == DBNull.Value ? 0m : Convert.ToDecimal(reader["amount"]),
                    Status = reader["status"]?.ToString() ?? ""
                });
            }

            return result;
        }

        private static async Task<List<OccupancyByHouseRow>> LoadOccupancyByHouseAsync(MySqlConnection conn, int? boardingHouseId)
        {
            var result = new List<OccupancyByHouseRow>();

            var sql = @"
                SELECT
                    bh.name AS boarding_house_name,
                    SUM(CASE WHEN rm.status = 'OCCUPIED' THEN 1 ELSE 0 END) AS occupied,
                    SUM(CASE WHEN rm.status = 'AVAILABLE' THEN 1 ELSE 0 END) AS available,
                    SUM(CASE WHEN rm.status = 'MAINTENANCE' THEN 1 ELSE 0 END) AS maintenance,
                    COUNT(rm.id) AS total_rooms
                FROM boarding_houses bh
                LEFT JOIN rooms rm ON rm.boarding_house_id = bh.id
                WHERE 1 = 1";

            if (boardingHouseId.HasValue)
                sql += " AND bh.id = @bhId";

            sql += @"
                GROUP BY bh.id, bh.name
                ORDER BY bh.name;";

            using var cmd = new MySqlCommand(sql, conn);
            if (boardingHouseId.HasValue)
                cmd.Parameters.AddWithValue("@bhId", boardingHouseId.Value);

            using var reader = (MySqlDataReader)await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(new OccupancyByHouseRow
                {
                    BoardingHouseName = reader["boarding_house_name"]?.ToString() ?? "(Unnamed)",
                    Occupied = reader["occupied"] == DBNull.Value ? 0 : Convert.ToInt32(reader["occupied"]),
                    Available = reader["available"] == DBNull.Value ? 0 : Convert.ToInt32(reader["available"]),
                    Maintenance = reader["maintenance"] == DBNull.Value ? 0 : Convert.ToInt32(reader["maintenance"]),
                    TotalRooms = reader["total_rooms"] == DBNull.Value ? 0 : Convert.ToInt32(reader["total_rooms"])
                });
            }

            return result;
        }

        private static string BuildRoomLabel(string roomNo, string roomType)
        {
            roomNo = (roomNo ?? "").Trim();
            roomType = (roomType ?? "").Trim();

            if (!string.IsNullOrWhiteSpace(roomNo) && !string.IsNullOrWhiteSpace(roomType))
                return $"{roomNo} - {roomType}";
            if (!string.IsNullOrWhiteSpace(roomNo))
                return roomNo;
            if (!string.IsNullOrWhiteSpace(roomType))
                return roomType;

            return "(Room)";
        }

        private static string FormatName(string lastname, string firstname, string middlename)
        {
            var ln = (lastname ?? "").Trim();
            var fn = (firstname ?? "").Trim();
            var mn = (middlename ?? "").Trim();

            var display = "";
            if (!string.IsNullOrWhiteSpace(ln))
                display = ln;

            if (!string.IsNullOrWhiteSpace(fn))
            {
                if (!string.IsNullOrWhiteSpace(display))
                    display += ", ";
                display += fn;
            }

            if (!string.IsNullOrWhiteSpace(mn))
            {
                if (!string.IsNullOrWhiteSpace(display))
                    display += " ";
                display += mn;
            }

            return string.IsNullOrWhiteSpace(display) ? "(Unnamed)" : display;
        }
    }

    public sealed class ReportsPayload
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public List<BoardingHouseOption> BoardingHouses { get; set; } = new();
        public int? SelectedBoardingHouseId { get; set; }
        public OwnerInfo? SelectedOwner { get; set; }

        public decimal TotalCollections { get; set; }
        public int TotalPaymentsCount { get; set; }
        public int ActiveRentalsCount { get; set; }
        public int OccupancyOccupied { get; set; }
        public int OccupancyAvailable { get; set; }
        public int OccupancyMaintenance { get; set; }
        public decimal OccupancyRate { get; set; }

        public List<MonthlyTrendPoint> MonthlyTrend { get; set; } = new();
        public List<CollectionRow> CollectionsRows { get; set; } = new();
        public List<OccupancyByHouseRow> OccupancyByHouse { get; set; } = new();
    }

    public sealed class BoardingHouseOption
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
    }

    public sealed class OwnerInfo
    {
        public string FullName { get; set; } = "";
        public string ContactNo { get; set; } = "";
        public string Email { get; set; } = "";
    }

    public sealed class MonthlyTrendPoint
    {
        public string MonthLabel { get; set; } = "";
        public decimal Amount { get; set; }
    }

    public sealed class CollectionRow
    {
        public DateTime PaymentDate { get; set; }
        public string BoardingHouseName { get; set; } = "";
        public string RoomNameOrNumber { get; set; } = "";
        public string TenantFullName { get; set; } = "";
        public decimal Amount { get; set; }
        public string Status { get; set; } = "";
    }

    public sealed class OccupancyByHouseRow
    {
        public string BoardingHouseName { get; set; } = "";
        public int Occupied { get; set; }
        public int Available { get; set; }
        public int Maintenance { get; set; }
        public int TotalRooms { get; set; }
    }
}
