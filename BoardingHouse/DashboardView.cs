// DashboardView.cs (WinForms UserControl)
using System;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;

namespace BoardingHouse
{
    public partial class DashboardView : UserControl
    {
        public DashboardView()
        {
            InitializeComponent();
            CenterLoader();
            HideLoader();
            this.Load += DashboardView_Load;
            this.Resize += DashboardView_Resize;
        }

        private async void DashboardView_Load(object sender, EventArgs e)
        {
            ShowLoader();
            await webViewDashboard.EnsureCoreWebView2Async();

            string htmlPath = Path.Combine(Application.StartupPath, "Dashboard", "dashboard.html");

            // Ensure we only hook once
            webViewDashboard.NavigationCompleted -= WebViewDashboard_NavigationCompleted;
            webViewDashboard.NavigationCompleted += WebViewDashboard_NavigationCompleted;
            webViewDashboard.NavigationStarting -= WebViewDashboard_NavigationStarting;
            webViewDashboard.NavigationStarting += WebViewDashboard_NavigationStarting;

            // IMPORTANT: Use proper file URI
            webViewDashboard.Source = new Uri(htmlPath);
        }

        private void DashboardView_Resize(object? sender, EventArgs e)
        {
            CenterLoader();
        }

        private void WebViewDashboard_NavigationStarting(object? sender, CoreWebView2NavigationStartingEventArgs e)
        {
            ShowLoader();
        }

        private async void WebViewDashboard_NavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            HideLoader();
            if (!e.IsSuccess) return;

            var core = webViewDashboard.CoreWebView2;
            if (core == null) return;

            try
            {
                var data = await DashboardDataService.GetDashboardAsync();

                // IMPORTANT: Send as STRING so JS can JSON.parse reliably
                string json = JsonSerializer.Serialize(data, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                core.PostWebMessageAsString(json);
            }
            catch (Exception ex)
            {
                await core.ExecuteScriptAsync(
                    $"console.error({JsonSerializer.Serialize(ex.ToString())});"
                );
            }
        }

        private void webViewDashboard_Click(object sender, EventArgs e)
        {

        }

        private void CenterLoader()
        {
            if (pictureBox1 == null)
                return;

            var parentSize = this.ClientSize;
            var x = (parentSize.Width - pictureBox1.Width) / 2;
            var y = (parentSize.Height - pictureBox1.Height) / 2;
            pictureBox1.Location = new Point(Math.Max(x, 0), Math.Max(y, 0));
        }

        private void ShowLoader()
        {
            CenterLoader();
            pictureBox1.Visible = true;
            pictureBox1.SendToBack();
            webViewDashboard?.BringToFront();
        }

        private void HideLoader()
        {
            pictureBox1.Visible = false;
            webViewDashboard?.BringToFront();
        }
    }
}
