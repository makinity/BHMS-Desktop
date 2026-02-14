using BoardingHouse;
using System;
using System.Text.Json;

public static class AuditLogger
{
    public static void Log(
        int userId,
        string action,
        string entity,
        long entityId,
        object? details = null)
    {
        try
        {
            string? detailsJson = details == null
                ? null
                : JsonSerializer.Serialize(details);

            using var conn = DbConnectionFactory.CreateConnection();
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
                INSERT INTO audit_logs
                    (user_id, action, entity, entity_id, details, created_at)
                VALUES
                    (@user_id, @action, @entity, @entity_id, @details, NOW());
            ";

            cmd.Parameters.AddWithValue("@user_id", userId);
            cmd.Parameters.AddWithValue("@action", action);
            cmd.Parameters.AddWithValue("@entity", entity);
            cmd.Parameters.AddWithValue("@entity_id", entityId);
            cmd.Parameters.AddWithValue("@details", (object?)detailsJson ?? DBNull.Value);

            cmd.ExecuteNonQuery();
        }
        catch
        {
            // Never break main flow if audit logging fails
        }
    }
}
