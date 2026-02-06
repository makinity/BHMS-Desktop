// FILE: ReportsDataService.cs
using System;
using System.Collections.Generic;
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

            var toExclusive = toDate.AddDays(1);

            using var conn = DbConnectionFactory.CreateConnection();

            var payload = new ReportsPayload
            {
                From = fromDate,
                To = toDate,
                SelectedBoardingHouseId = boardingHouseId,
                BoardingHouses = await LoadBoardingHouseOptionsAsync(conn)
            };

            var paymentSummary = await LoadPaymentsSummaryAsync(conn, fromDate, toExclusive, boardingHouseId);
            payload.TotalCollections = paymentSummary.TotalCollections;
            payload.TotalPaymentsCount = paymentSummary.PaymentsCount;

            payload.ActiveRentalsCount = await LoadActiveRentalsCountAsync(conn, fromDate, toDate, boardingHouseId);

            var occupancy = await LoadOccupancySummaryAsync(conn, boardingHouseId);
            payload.OccupancyOccupied = occupancy.Occupied;
            payload.OccupancyAvailable = occupancy.Available;
            payload.OccupancyMaintenance = occupancy.Maintenance;
            payload.OccupancyRate = occupancy.TotalRooms == 0
                ? 0d
                : Math.Round((double)occupancy.Occupied / occupancy.TotalRooms * 100d, 2);

            payload.MonthlyTrend = await LoadMonthlyTrendAsync(conn, fromDate, toExclusive, boardingHouseId);
            payload.CollectionsRows = await LoadCollectionRowsAsync(conn, fromDate, toExclusive, boardingHouseId);
            payload.OccupancyByHouse = await LoadOccupancyByHouseAsync(conn, boardingHouseId);
            payload.GeneratedAt = DateTime.Now;

            return payload;
        }

        private static async Task<List<BoardingHouseOption>> LoadBoardingHouseOptionsAsync(MySqlConnection conn)
        {
            var list = new List<BoardingHouseOption>();
            const string sql = @"
                SELECT id, name
                FROM boarding_houses
                WHERE status = 'ACTIVE'
                ORDER BY name;";

            using var cmd = new MySqlCommand(sql, conn);
            using var reader = (MySqlDataReader)await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new BoardingHouseOption
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Name = reader["name"]?.ToString() ?? "(Unnamed)"
                });
            }

            return list;
        }

        private static async Task<(decimal TotalCollections, int PaymentsCount)> LoadPaymentsSummaryAsync(
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
                INNER JOIN rentals r ON r.id = p.rental_id
                INNER JOIN rooms rm ON rm.id = r.room_id
                INNER JOIN boarding_houses bh ON bh.id = rm.boarding_house_id
                WHERE p.status = 'POSTED'
                  AND bh.status = 'ACTIVE'
                  AND p.payment_date >= @from
                  AND p.payment_date < @toExclusive";

            if (boardingHouseId.HasValue)
                sql += " AND bh.id = @boardingHouseId";

            sql += ";";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@from", from);
            cmd.Parameters.AddWithValue("@toExclusive", toExclusive);
            AddBoardingHouseParameter(cmd, boardingHouseId);

            using var reader = (MySqlDataReader)await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                var total = reader.IsDBNull(reader.GetOrdinal("total_collections"))
                    ? 0m
                    : reader.GetDecimal("total_collections");
                var count = reader.IsDBNull(reader.GetOrdinal("payments_count"))
                    ? 0
                    : Convert.ToInt32(reader["payments_count"]);

                return (total, count);
            }

            return (0m, 0);
        }

        private static async Task<int> LoadActiveRentalsCountAsync(
            MySqlConnection conn,
            DateTime from,
            DateTime to,
            int? boardingHouseId)
        {
            var sql = @"
                SELECT COUNT(*)
                FROM rentals r
                INNER JOIN rooms rm ON rm.id = r.room_id
                INNER JOIN boarding_houses bh ON bh.id = rm.boarding_house_id
                WHERE bh.status = 'ACTIVE'
                  AND r.status = 'ACTIVE'
                  AND r.start_date <= @toInclusive
                  AND (r.end_date IS NULL OR r.end_date >= @from)";

            if (boardingHouseId.HasValue)
                sql += " AND bh.id = @boardingHouseId";

            sql += ";";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@from", from);
            cmd.Parameters.AddWithValue("@toInclusive", to);
            AddBoardingHouseParameter(cmd, boardingHouseId);
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
                INNER JOIN boarding_houses bh ON bh.id = rm.boarding_house_id
                WHERE bh.status = 'ACTIVE'";

            if (boardingHouseId.HasValue)
                sql += " AND bh.id = @boardingHouseId";

            sql += ";";

            using var cmd = new MySqlCommand(sql, conn);
            AddBoardingHouseParameter(cmd, boardingHouseId);
            using var reader = (MySqlDataReader)await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return (
                    Convert.ToInt32(reader["occupied"]),
                    Convert.ToInt32(reader["available"]),
                    Convert.ToInt32(reader["maintenance"]),
                    Convert.ToInt32(reader["total_rooms"])
                );
            }

            return (0, 0, 0, 0);
        }

        private static async Task<List<MonthlyTrendPoint>> LoadMonthlyTrendAsync(
            MySqlConnection conn,
            DateTime from,
            DateTime toExclusive,
            int? boardingHouseId)
        {
            var trend = new List<MonthlyTrendPoint>();
            var sql = @"
                SELECT
                    YEAR(p.payment_date) AS yr,
                    MONTH(p.payment_date) AS mo,
                    COALESCE(SUM(p.amount), 0) AS amount
                FROM payments p
                INNER JOIN rentals r ON r.id = p.rental_id
                INNER JOIN rooms rm ON rm.id = r.room_id
                INNER JOIN boarding_houses bh ON bh.id = rm.boarding_house_id
                WHERE p.status = 'POSTED'
                  AND bh.status = 'ACTIVE'
                  AND p.payment_date >= @from
                  AND p.payment_date < @toExclusive";

            if (boardingHouseId.HasValue)
                sql += " AND bh.id = @boardingHouseId";

            sql += @"
                GROUP BY YEAR(p.payment_date), MONTH(p.payment_date)
                ORDER BY YEAR(p.payment_date), MONTH(p.payment_date);";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@from", from);
            cmd.Parameters.AddWithValue("@toExclusive", toExclusive);
            AddBoardingHouseParameter(cmd, boardingHouseId);

            var amounts = new Dictionary<DateTime, decimal>();
            using var reader = (MySqlDataReader)await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var year = reader.GetInt32("yr");
                var month = reader.GetInt32("mo");
                var amount = reader.IsDBNull(reader.GetOrdinal("amount"))
                    ? 0m
                    : reader.GetDecimal("amount");

                var key = new DateTime(year, month, 1);
                amounts[key] = amount;
            }

            var effectiveFrom = new DateTime(from.Year, from.Month, 1);
            var inclusiveTo = toExclusive.AddDays(-1);
            var effectiveTo = new DateTime(inclusiveTo.Year, inclusiveTo.Month, 1);
            if (effectiveTo < effectiveFrom)
                effectiveTo = effectiveFrom;

            var cursor = effectiveFrom;
            while (cursor <= effectiveTo)
            {
                amounts.TryGetValue(cursor, out var value);
                trend.Add(new MonthlyTrendPoint
                {
                    MonthLabel = cursor.ToString("yyyy-MM", CultureInfo.InvariantCulture),
                    Amount = value
                });
                cursor = cursor.AddMonths(1);
            }

            return trend;
        }

        private static async Task<List<CollectionRow>> LoadCollectionRowsAsync(
            MySqlConnection conn,
            DateTime from,
            DateTime toExclusive,
            int? boardingHouseId)
        {
            var list = new List<CollectionRow>();
            var sql = @"
                SELECT
                    p.payment_date,
                    p.amount,
                    p.status,
                    bh.name AS boarding_house_name,
                    COALESCE(rm.room_no, '(Room)') AS room_label,
                    t.lastname,
                    t.firstname,
                    t.middlename
                FROM payments p
                INNER JOIN rentals r ON r.id = p.rental_id
                INNER JOIN rooms rm ON rm.id = r.room_id
                INNER JOIN boarding_houses bh ON bh.id = rm.boarding_house_id
                INNER JOIN tenants t ON t.id = r.tenant_id
                WHERE p.status = 'POSTED'
                  AND bh.status = 'ACTIVE'
                  AND p.payment_date >= @from
                  AND p.payment_date < @toExclusive";

            if (boardingHouseId.HasValue)
                sql += " AND bh.id = @boardingHouseId";

            sql += @"
                ORDER BY p.payment_date DESC
                LIMIT 200;";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@from", from);
            cmd.Parameters.AddWithValue("@toExclusive", toExclusive);
            AddBoardingHouseParameter(cmd, boardingHouseId);

            using var reader = (MySqlDataReader)await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var paymentDate = reader.IsDBNull(reader.GetOrdinal("payment_date"))
                    ? DateTime.MinValue
                    : reader.GetDateTime("payment_date");

                list.Add(new CollectionRow
                {
                    PaymentDate = paymentDate,
                    Amount = reader.IsDBNull(reader.GetOrdinal("amount"))
                        ? 0m
                        : reader.GetDecimal("amount"),
                    Status = reader["status"]?.ToString() ?? "",
                    BoardingHouseName = reader["boarding_house_name"]?.ToString() ?? "(Unknown)",
                    RoomNameOrNumber = reader["room_label"]?.ToString() ?? "(Room)",
                    TenantFullName = BuildTenantDisplayName(reader)
                });
            }

            return list;
        }

        private static async Task<List<OccupancyByHouseRow>> LoadOccupancyByHouseAsync(
            MySqlConnection conn,
            int? boardingHouseId)
        {
            var list = new List<OccupancyByHouseRow>();
            var sql = @"
                SELECT
                    bh.name AS boarding_house_name,
                    SUM(CASE WHEN rm.status = 'OCCUPIED' THEN 1 ELSE 0 END) AS occupied,
                    SUM(CASE WHEN rm.status = 'AVAILABLE' THEN 1 ELSE 0 END) AS available,
                    SUM(CASE WHEN rm.status = 'MAINTENANCE' THEN 1 ELSE 0 END) AS maintenance,
                    COUNT(rm.id) AS total_rooms
                FROM boarding_houses bh
                LEFT JOIN rooms rm ON rm.boarding_house_id = bh.id
                WHERE bh.status = 'ACTIVE'";

            if (boardingHouseId.HasValue)
                sql += " AND bh.id = @boardingHouseId";

            sql += @"
                GROUP BY bh.id, bh.name
                ORDER BY bh.name;";

            using var cmd = new MySqlCommand(sql, conn);
            AddBoardingHouseParameter(cmd, boardingHouseId);

            using var reader = (MySqlDataReader)await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new OccupancyByHouseRow
                {
                    BoardingHouseName = reader["boarding_house_name"]?.ToString() ?? "(Unnamed)",
                    Occupied = Convert.ToInt32(reader["occupied"]),
                    Available = Convert.ToInt32(reader["available"]),
                    Maintenance = Convert.ToInt32(reader["maintenance"]),
                    TotalRooms = Convert.ToInt32(reader["total_rooms"])
                });
            }

            return list;
        }

        private static void AddBoardingHouseParameter(MySqlCommand cmd, int? boardingHouseId)
        {
            if (boardingHouseId.HasValue)
            {
                cmd.Parameters.AddWithValue("@boardingHouseId", boardingHouseId.Value);
            }
        }

        private static string BuildTenantDisplayName(MySqlDataReader reader)
        {
            var names = new List<string>();
            var last = reader["lastname"]?.ToString()?.Trim();
            var first = reader["firstname"]?.ToString()?.Trim();
            var middle = reader["middlename"]?.ToString()?.Trim();

            if (!string.IsNullOrWhiteSpace(last)) names.Add(last);
            if (!string.IsNullOrWhiteSpace(first)) names.Add(first);
            if (!string.IsNullOrWhiteSpace(middle)) names.Add(middle);

            return names.Count == 0 ? "(Unnamed)" : string.Join(" ", names);
        }
    }

    public class ReportsPayload
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int? SelectedBoardingHouseId { get; set; }

        public decimal TotalCollections { get; set; }
        public int TotalPaymentsCount { get; set; }
        public int ActiveRentalsCount { get; set; }

        public int OccupancyOccupied { get; set; }
        public int OccupancyAvailable { get; set; }
        public int OccupancyMaintenance { get; set; }
        public double OccupancyRate { get; set; }

        public List<MonthlyTrendPoint> MonthlyTrend { get; set; } = new();
        public List<CollectionRow> CollectionsRows { get; set; } = new();
        public List<OccupancyByHouseRow> OccupancyByHouse { get; set; } = new();
        public List<BoardingHouseOption> BoardingHouses { get; set; } = new();

        public DateTime GeneratedAt { get; set; }
    }

    public class MonthlyTrendPoint
    {
        public string MonthLabel { get; set; } = "";
        public decimal Amount { get; set; }
    }

    public class CollectionRow
    {
        public DateTime PaymentDate { get; set; }
        public string BoardingHouseName { get; set; } = "";
        public string RoomNameOrNumber { get; set; } = "";
        public string TenantFullName { get; set; } = "";
        public decimal Amount { get; set; }
        public string Status { get; set; } = "";
    }

    public class OccupancyByHouseRow
    {
        public string BoardingHouseName { get; set; } = "";
        public int Occupied { get; set; }
        public int Available { get; set; }
        public int Maintenance { get; set; }
        public int TotalRooms { get; set; }
    }

    public class BoardingHouseOption
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
    }
}
