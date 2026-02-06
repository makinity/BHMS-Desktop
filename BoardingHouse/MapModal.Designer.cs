using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;

namespace BoardingHouse
{
    partial class MapModal
    {
        private IContainer components = null!;
        private WebView2 webView2 = null!;

        private void InitializeComponent()
        {
            components = new Container();
            webView2 = new WebView2();
            SuspendLayout();
            webView2.Dock = DockStyle.Fill;
            Controls.Add(webView2);
            ClientSize = new Size(960, 600);
            Text = "Boarding Houses Map";
            ResumeLayout(false);
        }
    }
}
