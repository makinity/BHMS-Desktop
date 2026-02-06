using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BoardingHouse
{
    partial class DashboardView
    {
        private IContainer components = null!;

        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(DashboardView));
            webViewDashboard = new Microsoft.Web.WebView2.WinForms.WebView2();
            pictureBox1 = new PictureBox();
            ((ISupportInitialize)webViewDashboard).BeginInit();
            ((ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // webViewDashboard
            // 
            webViewDashboard.AllowExternalDrop = true;
            webViewDashboard.CreationProperties = null;
            webViewDashboard.DefaultBackgroundColor = Color.White;
            webViewDashboard.Dock = DockStyle.Fill;
            webViewDashboard.Location = new Point(0, 0);
            webViewDashboard.Name = "webViewDashboard";
            webViewDashboard.Size = new Size(1428, 636);
            webViewDashboard.TabIndex = 0;
            webViewDashboard.ZoomFactor = 1D;
            webViewDashboard.Click += webViewDashboard_Click;
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
            // DashboardView
            // 
            BackColor = Color.WhiteSmoke;
            Controls.Add(webViewDashboard);
            Controls.Add(pictureBox1);
            Margin = new Padding(0);
            Name = "DashboardView";
            Size = new Size(1428, 636);
            ((ISupportInitialize)webViewDashboard).EndInit();
            ((ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewDashboard;
        private PictureBox pictureBox1;
    }
}
