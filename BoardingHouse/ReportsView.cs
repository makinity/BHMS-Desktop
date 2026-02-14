using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;

namespace BoardingHouse
{
    public partial class ReportsView : UserControl
    {
        private bool _pageReady;
        private static readonly JsonSerializerOptions CamelCaseSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public ReportsView()
        {
            InitializeComponent();
            Load += ReportsView_Load;
            Resize += ReportsView_Resize;
        }

        private async void ReportsView_Load(object? sender, EventArgs e)
        {
            CenterLoader();
            try
            {
                await webViewReports.EnsureCoreWebView2Async();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return;
            }

            webViewReports.NavigationCompleted -= WebViewReports_NavigationCompleted;
            webViewReports.NavigationCompleted += WebViewReports_NavigationCompleted;

            var core = webViewReports.CoreWebView2;
            if (core != null)
            {
                core.WebMessageReceived -= CoreWebView2_WebMessageReceived;
                core.WebMessageReceived += CoreWebView2_WebMessageReceived;
            }

            var htmlPath = Path.Combine(Application.StartupPath, "Reports", "reports.html");
            webViewReports.Source = new Uri(htmlPath);
        }

        private async void WebViewReports_NavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            if (!e.IsSuccess)
                return;

            _pageReady = true;
            var (from, to) = GetDefaultFilterRange();
            await RefreshReportsAsync(from, to, null);
        }

        private async void CoreWebView2_WebMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            var message = ExtractWebMessage(e);
            if (string.IsNullOrWhiteSpace(message))
                return;

            try
            {
                using var doc = JsonDocument.Parse(message);
                var root = doc.RootElement;
                if (!root.TryGetProperty("type", out var typeElement))
                    return;

                var type = typeElement.GetString();
                if (type?.Equals("filters", StringComparison.OrdinalIgnoreCase) != true)
                    return;

                var defaultRange = GetDefaultFilterRange();
                var from = defaultRange.from;
                var to = defaultRange.to;

                if (root.TryGetProperty("from", out var fromElement) && fromElement.ValueKind == JsonValueKind.String)
                {
                    var fromText = fromElement.GetString() ?? string.Empty;
                    if (DateTime.TryParseExact(fromText, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedFrom))
                        from = parsedFrom;
                }

                if (root.TryGetProperty("to", out var toElement) && toElement.ValueKind == JsonValueKind.String)
                {
                    var toText = toElement.GetString() ?? string.Empty;
                    if (DateTime.TryParseExact(toText, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedTo))
                        to = parsedTo;
                }

                if (to < from)
                    to = from;

                int? boardingHouseId = null;
                if (root.TryGetProperty("boardingHouseId", out var bhElement))
                {
                    if (bhElement.ValueKind == JsonValueKind.Number && bhElement.TryGetInt32(out var bhId))
                        boardingHouseId = bhId;
                }

                await RefreshReportsAsync(from, to, boardingHouseId);
            }
            catch (JsonException)
            {
                // Ignore malformed messages
            }
        }

        public async Task RefreshReportsAsync(DateTime from, DateTime to, int? boardingHouseId)
        {
            if (!_pageReady || webViewReports.CoreWebView2 == null)
                return;

            if (to < from)
                to = from;

            try
            {
                var payload = await ReportsDataService.GetReportsAsync(from, to, boardingHouseId);
                var json = JsonSerializer.Serialize(payload, CamelCaseSerializerOptions);
                webViewReports.CoreWebView2.PostWebMessageAsString(json);
            }
            catch (Exception ex)
            {
                await webViewReports.CoreWebView2.ExecuteScriptAsync(
                    $"console.error({JsonSerializer.Serialize(ex.Message)});"
                );
            }
        }

        private static (DateTime from, DateTime to) GetDefaultFilterRange()
        {
            var now = DateTime.Now;
            var from = new DateTime(now.Year, now.Month, 1);
            var to = from.AddMonths(1).AddDays(-1);
            return (from, to);
        }

        private void pnlWebHost_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ReportsView_Resize(object? sender, EventArgs e)
        {
            CenterLoader();
        }

        private void CenterLoader()
        {
            if (pictureBox1 == null)
                return;

            var x = (this.ClientSize.Width - pictureBox1.Width) / 2;
            var y = (this.ClientSize.Height - pictureBox1.Height) / 2;
            pictureBox1.Location = new Point(Math.Max(x, 0), Math.Max(y, 0));
            pictureBox1.SendToBack();
            webViewReports?.BringToFront();
        }

        private static string? ExtractWebMessage(CoreWebView2WebMessageReceivedEventArgs e)
        {
            try
            {
                var json = e.WebMessageAsJson;
                if (!string.IsNullOrWhiteSpace(json))
                    return json;
            }
            catch (InvalidOperationException)
            {
                // fallback to string payload
            }

            var text = e.TryGetWebMessageAsString();
            return string.IsNullOrWhiteSpace(text) ? null : text;
        }

        private void pnlRoot_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
