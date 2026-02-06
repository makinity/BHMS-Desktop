using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BoardingHouse
{
    partial class ReportsView
    {
        private IContainer components = null!;
        private Panel pnlRoot;
        private Panel pnlWebHost;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewReports;

        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(ReportsView));
            pnlRoot = new Panel();
            pnlWebHost = new Panel();
            webViewReports = new Microsoft.Web.WebView2.WinForms.WebView2();
            pictureBox1 = new PictureBox();
            pnlRoot.SuspendLayout();
            pnlWebHost.SuspendLayout();
            ((ISupportInitialize)webViewReports).BeginInit();
            ((ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pnlRoot
            // 
            pnlRoot.BackColor = Color.White;
            pnlRoot.Controls.Add(pnlWebHost);
            pnlRoot.Dock = DockStyle.Fill;
            pnlRoot.Location = new Point(0, 0);
            pnlRoot.Margin = new Padding(0);
            pnlRoot.Name = "pnlRoot";
            pnlRoot.Padding = new Padding(14, 16, 14, 16);
            pnlRoot.Size = new Size(1917, 1300);
            pnlRoot.TabIndex = 0;
            // 
            // pnlWebHost
            // 
            pnlWebHost.BackColor = Color.WhiteSmoke;
            pnlWebHost.Controls.Add(webViewReports);
            pnlWebHost.Controls.Add(pictureBox1);
            pnlWebHost.Dock = DockStyle.Fill;
            pnlWebHost.Location = new Point(14, 16);
            pnlWebHost.Margin = new Padding(0);
            pnlWebHost.Name = "pnlWebHost";
            pnlWebHost.Size = new Size(1889, 1268);
            pnlWebHost.TabIndex = 0;
            // 
            // webViewReports
            // 
            webViewReports.AllowExternalDrop = true;
            webViewReports.BackColor = Color.White;
            webViewReports.CreationProperties = null;
            webViewReports.DefaultBackgroundColor = Color.Gainsboro;
            webViewReports.Dock = DockStyle.Fill;
            webViewReports.Location = new Point(0, 0);
            webViewReports.Margin = new Padding(3, 4, 3, 4);
            webViewReports.Name = "webViewReports";
            webViewReports.Size = new Size(1889, 1268);
            webViewReports.TabIndex = 0;
            webViewReports.ZoomFactor = 1D;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(
                (this.ClientSize.Width - pictureBox1.Width) / 2,
                (this.ClientSize.Height - pictureBox1.Height) / 2
            );
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(411, 266);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // ReportsView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(pnlRoot);
            Margin = new Padding(0);
            Name = "ReportsView";
            Size = new Size(1917, 1300);
            pnlRoot.ResumeLayout(false);
            pnlWebHost.ResumeLayout(false);
            ((ISupportInitialize)webViewReports).EndInit();
            ((ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }
        private PictureBox pictureBox1;
    }
}
