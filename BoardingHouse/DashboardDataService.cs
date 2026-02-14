// DashboardDataService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BoardingHouse
{
    public static class DashboardDataService
    {
        public static async Task<DashboardPayload> GetDashboardAsync()
        {
            var payload = new DashboardPayload();

            // NOTE: DbConnectionFactory opens the connection already.
            using var conn = DbConnectionFactory.CreateConnection();

            payload.KpiRegisteredBoardingHouses = await ScalarIntAsync(conn, @"
                SELECT COUNT(*)
                FROM boarding_houses
                WHERE status = 'ACTIVE';
            ");

            payload.KpiTotalTenants = await ScalarIntAsync(conn, @"
                SELECT COUNT(*)
                FROM tenants
                WHERE status = 'ACTIVE';
            ");

            payload.KpiThisMonthCollections = await ScalarDecimalAsync(conn, @"
                SELECT COALESCE(SUM(amount), 0)
                FROM payments
                WHERE status = 'POSTED'
                  AND payment_date >= DATE_FORMAT(CURDATE(), '%Y-%m-01')
                  AND payment_date <  DATE_ADD(DATE_FORMAT(CURDATE(), '%Y-%m-01'), INTERVAL 1 MONTH);
            ");

            // Occupancy breakdown based on rooms.status
            payload.OccupiedRooms = await ScalarIntAsync(conn, @"SELECT COUNT(*) FROM rooms WHERE status='OCCUPIED';");
            payload.VacantRooms = await ScalarIntAsync(conn, @"SELECT COUNT(*) FROM rooms WHERE status='AVAILABLE';");

            // You don't have RESERVED in schema. Using MAINTENANCE as "Reserved" bucket.
            payload.ReservedRooms = await ScalarIntAsync(conn, @"SELECT COUNT(*) FROM rooms WHERE status='MAINTENANCE';");

            // Top boarding houses by ACTIVE rentals count (top 5)
            payload.TopBoardingHouses = await QueryAsync(conn, @"
                SELECT bh.name AS boarding_house_name,
                       COUNT(rn.id) AS active_rentals
                FROM boarding_houses bh
                JOIN rooms rm ON rm.boarding_house_id = bh.id
                LEFT JOIN rentals rn ON rn.room_id = rm.id AND rn.status = 'ACTIVE'
                WHERE bh.status = 'ACTIVE'
                GROUP BY bh.id, bh.name
                ORDER BY active_rentals DESC, bh.name ASC
                LIMIT 5;
            ", r => new TopBoardingHouse
            {
                Name = r.GetString("boarding_house_name"),
                Score = Convert.ToInt32(r["active_rentals"])
            });

            payload.RecentActivities = await QueryAsync(conn, @"
                SELECT action, entity, entity_id, details, created_at
                FROM audit_logs
                ORDER BY created_at DESC
                LIMIT 5;
            ", r =>
            {
                string action = r.GetString("action");
                string entity = r.GetString("entity");
                long entityId = Convert.ToInt64(r["entity_id"]);
                string details = r["details"] == DBNull.Value ? "" : Convert.ToString(r["details"]) ?? "";

                return new ActivityItem
                {
                    Title = AuditDetailsFormatter.ToTitle(action, entity, entityId),
                    Meta = AuditDetailsFormatter.ToMeta(action, details),
                    CreatedAt = Convert.ToDateTime(r["created_at"])
                };
            });


            payload.GeneratedAt = DateTime.Now;
            return payload;
        }

        private static async Task<int> ScalarIntAsync(MySqlConnection conn, string sql)
        {
            using var cmd = new MySqlCommand(sql, conn);
            var o = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(o ?? 0);
        }

        private static async Task<decimal> ScalarDecimalAsync(MySqlConnection conn, string sql)
        {
            using var cmd = new MySqlCommand(sql, conn);
            var o = await cmd.ExecuteScalarAsync();
            return Convert.ToDecimal(o ?? 0m);
        }

        private static async Task<List<T>> QueryAsync<T>(
            MySqlConnection conn,
            string sql,
            Func<MySqlDataReader, T> map
        )
        {
            var list = new List<T>();
            using var cmd = new MySqlCommand(sql, conn);
            using var rdr = (MySqlDataReader)await cmd.ExecuteReaderAsync();

            while (await rdr.ReadAsync())
                list.Add(map(rdr));

            return list;
        }
    }

    public class DashboardPayload
    {
        public int KpiRegisteredBoardingHouses { get; set; }
        public int KpiTotalTenants { get; set; }
        public decimal KpiThisMonthCollections { get; set; }

        public int OccupiedRooms { get; set; }
        public int VacantRooms { get; set; }
        public int ReservedRooms { get; set; }

        public List<TopBoardingHouse> TopBoardingHouses { get; set; } = new();
        public List<ActivityItem> RecentActivities { get; set; } = new();

        public DateTime GeneratedAt { get; set; }
    }

    public class TopBoardingHouse
    {
        public string Name { get; set; } = "";
        public int Score { get; set; }
    }



    public class ActivityItem
    {
        public string Title { get; set; } = "";
        public string Meta { get; set; } = "";
        public DateTime CreatedAt { get; set; }
    }
}
