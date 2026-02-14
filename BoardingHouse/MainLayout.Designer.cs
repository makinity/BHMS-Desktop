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
        private Button btnStudents;
        private Button btnRooms;
        private Button btnReservations;
        private Button btnPayments;
        private Button btnReports;
        private Button btnBHOwners;

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
            label2 = new Label();
            profileImg = new PictureBox();
            label1 = new Label();
            panel1 = new Panel();
            pictureBox1 = new PictureBox();
            dropDownPanel = new Panel();
            closeDPPanelBtn = new Button();
            logoutBtn = new Button();
            settingsBtn = new Button();
            profileBtn = new Button();
            bodyPanel = new Panel();
            contentPanel = new Panel();
            sidebarPanel = new Panel();
            buttonsPanel = new Panel();
            btnReports = new Button();
            btnPayments = new Button();
            btnReservations = new Button();
            btnRooms = new Button();
            btnTenants = new Button();
            btnBoardingHouses = new Button();
            btnBHOwners = new Button();
            btnDashboard = new Button();
            activeIndicator = new Panel();
            btnStudents = new Button();
            topPanel.SuspendLayout();
            ((ISupportInitialize)profileImg).BeginInit();
            panel1.SuspendLayout();
            ((ISupportInitialize)pictureBox1).BeginInit();
            dropDownPanel.SuspendLayout();
            bodyPanel.SuspendLayout();
            contentPanel.SuspendLayout();
            sidebarPanel.SuspendLayout();
            buttonsPanel.SuspendLayout();
            SuspendLayout();
            // 
            // topPanel
            // 
            topPanel.BackColor = Color.FromArgb(48, 54, 92);
            topPanel.Controls.Add(dropDownPanel);
            topPanel.Controls.Add(label2);
            topPanel.Controls.Add(profileImg);
            topPanel.Controls.Add(label1);
            topPanel.Controls.Add(panel1);
            topPanel.Dock = DockStyle.Top;
            topPanel.Location = new Point(0, 0);
            topPanel.Name = "topPanel";
            topPanel.Size = new Size(1280, 90);
            topPanel.TabIndex = 1;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(1109, 23);
            label2.Name = "label2";
            label2.Size = new Size(72, 28);
            label2.TabIndex = 3;
            label2.Text = "Admin";
            // 
            // profileImg
            // 
            profileImg.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            profileImg.Image = (Image)resources.GetObject("profileImg.Image");
            profileImg.Location = new Point(1187, 3);
            profileImg.Name = "profileImg";
            profileImg.Size = new Size(90, 82);
            profileImg.SizeMode = PictureBoxSizeMode.StretchImage;
            profileImg.TabIndex = 2;
            profileImg.TabStop = false;
            profileImg.Click += profileImg_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(274, 23);
            label1.Name = "label1";
            label1.Size = new Size(264, 50);
            label1.TabIndex = 1;
            label1.Text = "BHMS System";
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
            // dropDownPanel
            // 
            dropDownPanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            dropDownPanel.BackColor = Color.FromArgb(41, 42, 70);
            dropDownPanel.Controls.Add(closeDPPanelBtn);
            dropDownPanel.Controls.Add(logoutBtn);
            dropDownPanel.Controls.Add(settingsBtn);
            dropDownPanel.Controls.Add(profileBtn);
            dropDownPanel.Location = new Point(606, 30);
            dropDownPanel.Name = "dropDownPanel";
            dropDownPanel.Size = new Size(164, 192);
            dropDownPanel.TabIndex = 0;
            dropDownPanel.Visible = false;
            // 
            // closeDPPanelBtn
            // 
            closeDPPanelBtn.BackColor = Color.FromArgb(255, 192, 192);
            closeDPPanelBtn.Location = new Point(137, 3);
            closeDPPanelBtn.Name = "closeDPPanelBtn";
            closeDPPanelBtn.Size = new Size(24, 25);
            closeDPPanelBtn.TabIndex = 4;
            closeDPPanelBtn.Text = "X";
            closeDPPanelBtn.UseVisualStyleBackColor = false;
            closeDPPanelBtn.Click += closeDPPanelBtn_Click;
            // 
            // logoutBtn
            // 
            logoutBtn.BackColor = Color.LightCyan;
            logoutBtn.FlatStyle = FlatStyle.Flat;
            logoutBtn.Location = new Point(3, 137);
            logoutBtn.Name = "logoutBtn";
            logoutBtn.Size = new Size(158, 45);
            logoutBtn.TabIndex = 2;
            logoutBtn.Text = "Logout";
            logoutBtn.UseVisualStyleBackColor = false;
            logoutBtn.Click += button2_Click;
            // 
            // settingsBtn
            // 
            settingsBtn.BackColor = Color.LightCyan;
            settingsBtn.FlatStyle = FlatStyle.Flat;
            settingsBtn.Location = new Point(3, 86);
            settingsBtn.Name = "settingsBtn";
            settingsBtn.Size = new Size(158, 45);
            settingsBtn.TabIndex = 1;
            settingsBtn.Text = "Settings";
            settingsBtn.UseVisualStyleBackColor = false;
            settingsBtn.Click += settingsBtn_Click;
            // 
            // profileBtn
            // 
            profileBtn.BackColor = Color.LightCyan;
            profileBtn.FlatStyle = FlatStyle.Flat;
            profileBtn.Location = new Point(3, 35);
            profileBtn.Name = "profileBtn";
            profileBtn.Size = new Size(158, 45);
            profileBtn.TabIndex = 0;
            profileBtn.Text = "Profile";
            profileBtn.UseVisualStyleBackColor = false;
            profileBtn.Click += profileBtn_Click;
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
            buttonsPanel.Controls.Add(btnReservations);
            buttonsPanel.Controls.Add(btnRooms);
            buttonsPanel.Controls.Add(btnTenants);
            buttonsPanel.Controls.Add(btnBoardingHouses);
            buttonsPanel.Controls.Add(btnBHOwners);
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
            btnReports.Location = new Point(0, 444);
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
            btnPayments.Location = new Point(0, 370);
            btnPayments.Name = "btnPayments";
            btnPayments.Padding = new Padding(16, 0, 0, 0);
            btnPayments.Size = new Size(250, 74);
            btnPayments.TabIndex = 1;
            btnPayments.Text = "Payments";
            btnPayments.TextAlign = ContentAlignment.MiddleLeft;
            btnPayments.UseVisualStyleBackColor = false;
            btnPayments.Click += PaymentsButton_Click;
            // 
            // btnReservations
            // 
            btnReservations.BackColor = Color.FromArgb(41, 42, 70);
            btnReservations.Dock = DockStyle.Top;
            btnReservations.FlatAppearance.BorderSize = 0;
            btnReservations.FlatStyle = FlatStyle.Flat;
            btnReservations.ForeColor = Color.White;
            btnReservations.Location = new Point(0, 370);
            btnReservations.Name = "btnReservations";
            btnReservations.Padding = new Padding(16, 0, 0, 0);
            btnReservations.Size = new Size(250, 74);
            btnReservations.TabIndex = 1;
            btnReservations.Text = "Reservations";
            btnReservations.TextAlign = ContentAlignment.MiddleLeft;
            btnReservations.UseVisualStyleBackColor = false;
            btnReservations.Click += btnReservations_Click;
            // 
            // btnRooms
            // 
            btnRooms.BackColor = Color.FromArgb(41, 42, 70);
            btnRooms.Dock = DockStyle.Top;
            btnRooms.FlatAppearance.BorderSize = 0;
            btnRooms.FlatStyle = FlatStyle.Flat;
            btnRooms.ForeColor = Color.White;
            btnRooms.Location = new Point(0, 296);
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
            btnTenants.Location = new Point(0, 222);
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
            btnBoardingHouses.Location = new Point(0, 148);
            btnBoardingHouses.Name = "btnBoardingHouses";
            btnBoardingHouses.Padding = new Padding(16, 0, 0, 0);
            btnBoardingHouses.Size = new Size(250, 74);
            btnBoardingHouses.TabIndex = 5;
            btnBoardingHouses.Text = "Boarding Houses";
            btnBoardingHouses.TextAlign = ContentAlignment.MiddleLeft;
            btnBoardingHouses.UseVisualStyleBackColor = false;
            btnBoardingHouses.Click += BoardingHousesButton_Click;
            // 
            // btnBHOwners
            // 
            btnBHOwners.BackColor = Color.FromArgb(41, 42, 70);
            btnBHOwners.Dock = DockStyle.Top;
            btnBHOwners.FlatAppearance.BorderSize = 0;
            btnBHOwners.FlatStyle = FlatStyle.Flat;
            btnBHOwners.ForeColor = Color.White;
            btnBHOwners.Location = new Point(0, 74);
            btnBHOwners.Name = "btnBHOwners";
            btnBHOwners.Padding = new Padding(16, 0, 0, 0);
            btnBHOwners.Size = new Size(250, 74);
            btnBHOwners.TabIndex = 6;
            btnBHOwners.Text = "BH Owners";
            btnBHOwners.TextAlign = ContentAlignment.MiddleLeft;
            btnBHOwners.UseVisualStyleBackColor = false;
            btnBHOwners.Click += btnBHOwners_Click;
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
            btnDashboard.TabIndex = 7;
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
            // btnStudents
            // 
            btnStudents.BackColor = Color.FromArgb(41, 42, 70);
            btnStudents.Dock = DockStyle.Top;
            btnStudents.FlatAppearance.BorderSize = 0;
            btnStudents.FlatStyle = FlatStyle.Flat;
            btnStudents.ForeColor = Color.White;
            btnStudents.Location = new Point(0, 222);
            btnStudents.Name = "btnStudents";
            btnStudents.Padding = new Padding(16, 0, 0, 0);
            btnStudents.Size = new Size(250, 74);
            btnStudents.TabIndex = 4;
            btnStudents.Text = "Students";
            btnStudents.TextAlign = ContentAlignment.MiddleLeft;
            btnStudents.UseVisualStyleBackColor = false;
            btnStudents.Click += btnStudents_Click;
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
            topPanel.PerformLayout();
            ((ISupportInitialize)profileImg).EndInit();
            panel1.ResumeLayout(false);
            ((ISupportInitialize)pictureBox1).EndInit();
            dropDownPanel.ResumeLayout(false);
            bodyPanel.ResumeLayout(false);
            contentPanel.ResumeLayout(false);
            sidebarPanel.ResumeLayout(false);
            buttonsPanel.ResumeLayout(false);
            ResumeLayout(false);
        }
        private Panel panel1;
        private PictureBox pictureBox1;
        private Panel contentPanel;
        private Label label1;
        private PictureBox profileImg;
        private Panel dropDownPanel;
        private Button logoutBtn;
        private Button settingsBtn;
        private Button profileBtn;
        private Label label2;
        private Button closeDPPanelBtn;
    }
}
