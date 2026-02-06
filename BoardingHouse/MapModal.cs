using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;

namespace BoardingHouse
{
    public partial class MapModal : Form
    {
        private bool webViewInitialized;

        public MapModal()
        {
            InitializeComponent();
            Load += MapModal_Load;
        }

        private async void MapModal_Load(object sender, EventArgs e)
        {
            await EnsureWebViewReady();
        }

        private async Task EnsureWebViewReady()
        {
            if (webViewInitialized)
                return;

            await webView2.EnsureCoreWebView2Async();
            var mapPath = Path.Combine(Application.StartupPath, "Map", "index.html");
            webView2.CoreWebView2.Navigate(new Uri(Path.GetFullPath(mapPath)).AbsoluteUri);
            webViewInitialized = true;
        }

        public async Task LoadBoardingHousesJson(string json)
        {
            await EnsureWebViewReady();
            webView2.CoreWebView2.PostWebMessageAsString(json);
        }
    }
}
