using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BoardingHouse
{
    partial class MainLayout
    {
        private IContainer components = null;
        private Panel topPanel;
        private Panel bodyPanel;
        private Panel sidebarPanel;
        private Panel buttonsPanel;
        private Panel activeIndicator;
        private Button btnDashboard;
        private Button btnBoardingHouses;
        private Button btnTenants;
        private Button btnRooms;
        private Button btnPayments;
        private Button btnReports;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(MainLayout));
            topPanel = new Panel();
            panel1 = new Panel();
            pictureBox1 = new PictureBox();
            bodyPanel = new Panel();
            contentPanel = new Panel();
            sidebarPanel = new Panel();
            buttonsPanel = new Panel();
            btnReports = new Button();
            btnPayments = new Button();
            btnRooms = new Button();
            btnTenants = new Button();
            btnBoardingHouses = new Button();
            btnDashboard = new Button();
            activeIndicator = new Panel();
            topPanel.SuspendLayout();
            panel1.SuspendLayout();
            ((ISupportInitialize)pictureBox1).BeginInit();
            bodyPanel.SuspendLayout();
            sidebarPanel.SuspendLayout();
            buttonsPanel.SuspendLayout();
            SuspendLayout();
            // 
            // topPanel
            // 
            topPanel.BackColor = Color.FromArgb(48, 54, 92);
            topPanel.Controls.Add(panel1);
            topPanel.Dock = DockStyle.Top;
            topPanel.Location = new Point(0, 0);
            topPanel.Name = "topPanel";
            topPanel.Size = new Size(1280, 90);
            topPanel.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(41, 42, 70);
            panel1.Controls.Add(pictureBox1);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(250, 90);
            panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(70, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(94, 72);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // bodyPanel
            // 
            bodyPanel.Controls.Add(contentPanel);
            bodyPanel.Controls.Add(sidebarPanel);
            bodyPanel.Dock = DockStyle.Fill;
            bodyPanel.Location = new Point(0, 90);
            bodyPanel.Name = "bodyPanel";
            bodyPanel.Size = new Size(1280, 631);
            bodyPanel.TabIndex = 0;
            // 
            // contentPanel
            // 
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.Location = new Point(250, 0);
            contentPanel.Name = "contentPanel";
            contentPanel.Size = new Size(1030, 631);
            contentPanel.TabIndex = 1;
            // 
            // sidebarPanel
            // 
            sidebarPanel.BackColor = Color.FromArgb(31, 30, 68);
            sidebarPanel.Controls.Add(buttonsPanel);
            sidebarPanel.Dock = DockStyle.Left;
            sidebarPanel.Location = new Point(0, 0);
            sidebarPanel.Name = "sidebarPanel";
            sidebarPanel.Size = new Size(250, 631);
            sidebarPanel.TabIndex = 0;
            // 
            // buttonsPanel
            // 
            buttonsPanel.Controls.Add(btnReports);
            buttonsPanel.Controls.Add(btnPayments);
            buttonsPanel.Controls.Add(btnRooms);
            buttonsPanel.Controls.Add(btnTenants);
            buttonsPanel.Controls.Add(btnBoardingHouses);
            buttonsPanel.Controls.Add(btnDashboard);
            buttonsPanel.Controls.Add(activeIndicator);
            buttonsPanel.Dock = DockStyle.Fill;
            buttonsPanel.Location = new Point(0, 0);
            buttonsPanel.Name = "buttonsPanel";
            buttonsPanel.Size = new Size(250, 631);
            buttonsPanel.TabIndex = 0;
            // 
            // btnReports
            // 
            btnReports.BackColor = Color.FromArgb(41, 42, 70);
            btnReports.Dock = DockStyle.Top;
            btnReports.FlatAppearance.BorderSize = 0;
            btnReports.FlatStyle = FlatStyle.Flat;
            btnReports.ForeColor = Color.White;
            btnReports.Location = new Point(0, 370);
            btnReports.Name = "btnReports";
            btnReports.Padding = new Padding(16, 0, 0, 0);
            btnReports.Size = new Size(250, 74);
            btnReports.TabIndex = 0;
            btnReports.Text = "Reports";
            btnReports.TextAlign = ContentAlignment.MiddleLeft;
            btnReports.UseVisualStyleBackColor = false;
            btnReports.Click += ReportsButton_Click;
            // 
            // btnPayments
            // 
            btnPayments.BackColor = Color.FromArgb(41, 42, 70);
            btnPayments.Dock = DockStyle.Top;
            btnPayments.FlatAppearance.BorderSize = 0;
            btnPayments.FlatStyle = FlatStyle.Flat;
            btnPayments.ForeColor = Color.White;
            btnPayments.Location = new Point(0, 296);
            btnPayments.Name = "btnPayments";
            btnPayments.Padding = new Padding(16, 0, 0, 0);
            btnPayments.Size = new Size(250, 74);
            btnPayments.TabIndex = 1;
            btnPayments.Text = "Payments";
            btnPayments.TextAlign = ContentAlignment.MiddleLeft;
            btnPayments.UseVisualStyleBackColor = false;
            btnPayments.Click += PaymentsButton_Click;
            // 
            // btnRooms
            // 
            btnRooms.BackColor = Color.FromArgb(41, 42, 70);
            btnRooms.Dock = DockStyle.Top;
            btnRooms.FlatAppearance.BorderSize = 0;
            btnRooms.FlatStyle = FlatStyle.Flat;
            btnRooms.ForeColor = Color.White;
            btnRooms.Location = new Point(0, 222);
            btnRooms.Name = "btnRooms";
            btnRooms.Padding = new Padding(16, 0, 0, 0);
            btnRooms.Size = new Size(250, 74);
            btnRooms.TabIndex = 2;
            btnRooms.Text = "Rooms";
            btnRooms.TextAlign = ContentAlignment.MiddleLeft;
            btnRooms.UseVisualStyleBackColor = false;
            btnRooms.Click += RoomsButton_Click;
            // 
            // btnTenants
            // 
            btnTenants.BackColor = Color.FromArgb(41, 42, 70);
            btnTenants.Dock = DockStyle.Top;
            btnTenants.FlatAppearance.BorderSize = 0;
            btnTenants.FlatStyle = FlatStyle.Flat;
            btnTenants.ForeColor = Color.White;
            btnTenants.Location = new Point(0, 148);
            btnTenants.Name = "btnTenants";
            btnTenants.Padding = new Padding(16, 0, 0, 0);
            btnTenants.Size = new Size(250, 74);
            btnTenants.TabIndex = 3;
            btnTenants.Text = "Tenants";
            btnTenants.TextAlign = ContentAlignment.MiddleLeft;
            btnTenants.UseVisualStyleBackColor = false;
            btnTenants.Click += TenantsButton_Click;
            // 
            // btnBoardingHouses
            // 
            btnBoardingHouses.BackColor = Color.FromArgb(41, 42, 70);
            btnBoardingHouses.Dock = DockStyle.Top;
            btnBoardingHouses.FlatAppearance.BorderSize = 0;
            btnBoardingHouses.FlatStyle = FlatStyle.Flat;
            btnBoardingHouses.ForeColor = Color.White;
            btnBoardingHouses.Location = new Point(0, 74);
            btnBoardingHouses.Name = "btnBoardingHouses";
            btnBoardingHouses.Padding = new Padding(16, 0, 0, 0);
            btnBoardingHouses.Size = new Size(250, 74);
            btnBoardingHouses.TabIndex = 4;
            btnBoardingHouses.Text = "Boarding Houses";
            btnBoardingHouses.TextAlign = ContentAlignment.MiddleLeft;
            btnBoardingHouses.UseVisualStyleBackColor = false;
            btnBoardingHouses.Click += BoardingHousesButton_Click;
            // 
            // btnDashboard
            // 
            btnDashboard.BackColor = Color.FromArgb(41, 42, 70);
            btnDashboard.Dock = DockStyle.Top;
            btnDashboard.FlatAppearance.BorderSize = 0;
            btnDashboard.FlatStyle = FlatStyle.Flat;
            btnDashboard.ForeColor = Color.White;
            btnDashboard.Location = new Point(0, 0);
            btnDashboard.Name = "btnDashboard";
            btnDashboard.Padding = new Padding(16, 0, 0, 0);
            btnDashboard.Size = new Size(250, 74);
            btnDashboard.TabIndex = 5;
            btnDashboard.Text = "Dashboard";
            btnDashboard.TextAlign = ContentAlignment.MiddleLeft;
            btnDashboard.UseVisualStyleBackColor = false;
            btnDashboard.Click += DashboardButton_Click;
            // 
            // activeIndicator
            // 
            activeIndicator.BackColor = Color.DodgerBlue;
            activeIndicator.Location = new Point(0, 0);
            activeIndicator.Name = "activeIndicator";
            activeIndicator.Size = new Size(4, 74);
            activeIndicator.TabIndex = 6;
            activeIndicator.Visible = false;
            // 
            // MainLayout
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 721);
            Controls.Add(bodyPanel);
            Controls.Add(topPanel);
            MinimumSize = new Size(1024, 768);
            Name = "MainLayout";
            Text = "Boarding House";
            Load += MainLayout_Load;
            topPanel.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ((ISupportInitialize)pictureBox1).EndInit();
            bodyPanel.ResumeLayout(false);
            sidebarPanel.ResumeLayout(false);
            buttonsPanel.ResumeLayout(false);
            ResumeLayout(false);
        }
        private Panel panel1;
        private PictureBox pictureBox1;
        private Panel contentPanel;
    }
}
