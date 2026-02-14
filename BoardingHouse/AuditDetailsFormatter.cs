using System;
using System.Linq;
using System.Text.Json;

namespace BoardingHouse
{
    public static class AuditDetailsFormatter
    {
        public static string ToTitle(string action, string entity, long entityId)
            => $"{action} ({entity} #{entityId})";

        public static string ToMeta(string action, string detailsJson)
        {
            if (string.IsNullOrWhiteSpace(detailsJson)) return "";

            try
            {
                using var doc = JsonDocument.Parse(detailsJson);
                var root = doc.RootElement;

                // UPDATE: details = { before: {...}, after: {...} }
                if (action.Equals("UPDATE", StringComparison.OrdinalIgnoreCase) &&
                    root.TryGetProperty("before", out var before) &&
                    root.TryGetProperty("after", out var after))
                {
                    var changed = before.EnumerateObject()
                        .Where(p =>
                        {
                            if (!after.TryGetProperty(p.Name, out var a)) return true;
                            return p.Value.ToString() != a.ToString();
                        })
                        .Select(p => p.Name)
                        .ToList();

                    return changed.Count > 0
                        ? $"Changed: {string.Join(", ", changed)}"
                        : "Updated";
                }

                // DELETE: details = { deleted: {...} }
                if (action.Equals("DELETE", StringComparison.OrdinalIgnoreCase) &&
                    root.TryGetProperty("deleted", out var deleted))
                {
                    var name = GetName(deleted);
                    return !string.IsNullOrWhiteSpace(name) ? $"Deleted: {name}" : "Deleted";
                }

                // CREATE (or others): details = { lastname, firstname, ... }
                if (action.Equals("CREATE", StringComparison.OrdinalIgnoreCase))
                {
                    var name = GetName(root);
                    return !string.IsNullOrWhiteSpace(name) ? $"Created: {name}" : "Created";
                }

                // fallback: try show name if present
                var fallbackName = GetName(root);
                return !string.IsNullOrWhiteSpace(fallbackName) ? fallbackName : "";
            }
            catch
            {
                // if not valid JSON, don't show it
                return "";
            }
        }

        private static string GetName(JsonElement obj)
        {
            var last = obj.TryGetProperty("lastname", out var l) ? l.ToString() : "";
            var first = obj.TryGetProperty("firstname", out var f) ? f.ToString() : "";
            var name = $"{last}, {first}".Trim(' ', ',');
            return name;
        }
    }
}
