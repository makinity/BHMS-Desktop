using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BoardingHouse
{
    partial class TenantsView
    {
        private IContainer components = null!;

        private void InitializeComponent()
        {
            dgvTenants = new DataGridView();
            topBar = new Panel();
            lblStatus = new Label();
            lblBh = new Label();
            btnSearch = new Button();
            cbStatusFilter = new ComboBox();
            txtSearch = new TextBox();
            cbBoardingHouses = new ComboBox();
            addTenantsBtn = new Button();
            lblTenantTitle = new Label();
            grpQuickActions = new GroupBox();
            btnMarkInactive = new Button();
            btnMarkMaintenance = new Button();
            btnMarkOccupied = new Button();
            btnMarkAvailable = new Button();
            btnCloseDetails = new Button();
            detailsModal = new Panel();
            grpDetails = new GroupBox();
            ViewRoomBtn = new Label();
            detailsOpenCameraBtn = new Button();
            detailsBrowseProfileBtn = new Button();
            details_profilePathTxt = new TextBox();
            detailsTenantImg = new PictureBox();
            endRentalBtn = new Button();
            btnStartRental = new Button();
            btnViewCurrentRental = new Button();
            cbDetailsRoom = new ComboBox();
            label4 = new Label();
            tenantUpdateBtn = new Button();
            tenantDeleteBtn = new Button();
            detailsCbStatus = new ComboBox();
            label6 = new Label();
            detailsEContact = new TextBox();
            label1 = new Label();
            detailsEName = new TextBox();
            label2 = new Label();
            detailsAddress = new TextBox();
            label3 = new Label();
            detailsEmail = new TextBox();
            labelRoomStatus = new Label();
            detailsContact = new TextBox();
            labelRate = new Label();
            detailsMiddlename = new TextBox();
            labelCap = new Label();
            detailsFirstname = new TextBox();
            labelType = new Label();
            detailsLastname = new TextBox();
            labelRoomNo = new Label();
            addTenantsModal = new Panel();
            registrationOpenCameraBtn = new Button();
            browseProfileBtn = new Button();
            profilePathTxt = new TextBox();
            addTenantImg = new PictureBox();
            cancelTenantRegister = new Button();
            registerTenantBtn = new Button();
            tenantAddressTxt = new TextBox();
            label16 = new Label();
            tenantEmergencyContactTxt = new TextBox();
            label11 = new Label();
            tenantEmergencyNameTxt = new TextBox();
            label10 = new Label();
            tenantEmailTxt = new TextBox();
            label8 = new Label();
            tenantContactTxt = new TextBox();
            label7 = new Label();
            tenantMiddleNameTxt = new TextBox();
            label5 = new Label();
            tenantFirstNameTxt = new TextBox();
            label9 = new Label();
            tenantLastNameTxt = new TextBox();
            label12 = new Label();
            addTenantCloseBtn = new Button();
            totalTenants = new Label();
            label15 = new Label();
            paymentHistoryPanel = new Panel();
            label17 = new Label();
            label14 = new Label();
            dataGridView1 = new DataGridView();
            label13 = new Label();
            tenantsSnapshotPanel = new Panel();
            pnlSnapshotActions = new Panel();
            lblSnapshotHint = new Label();
            btnSnapshotOpenPayments = new Button();
            btnSnapshotViewRoom = new Button();
            btnSnapshotRefresh = new Button();
            pnlSnapshotCard = new Panel();
            lblSnapshotStatusBadge = new Label();
            lblFieldTenant = new Label();
            lblSnapshotTenant = new Label();
            lblFieldTenantId = new Label();
            lblSnapshotTenantId = new Label();
            lblFieldBoardingHouse = new Label();
            lblSnapshotBoardingHouse = new Label();
            lblFieldRoomAssigned = new Label();
            lblSnapshotRoomAssigned = new Label();
            lblFieldRentalStart = new Label();
            lblSnapshotRentalStart = new Label();
            lblFieldDuration = new Label();
            lblSnapshotDuration = new Label();
            lblFieldLastPayment = new Label();
            lblSnapshotLastPayment = new Label();
            lblFieldLastAmount = new Label();
            lblSnapshotLastAmount = new Label();
            pnlSnapshotDivider = new Panel();
            lblSnapshotTitle = new Label();
            ((ISupportInitialize)dgvTenants).BeginInit();
            topBar.SuspendLayout();
            grpQuickActions.SuspendLayout();
            detailsModal.SuspendLayout();
            grpDetails.SuspendLayout();
            ((ISupportInitialize)detailsTenantImg).BeginInit();
            addTenantsModal.SuspendLayout();
            ((ISupportInitialize)addTenantImg).BeginInit();
            paymentHistoryPanel.SuspendLayout();
            ((ISupportInitialize)dataGridView1).BeginInit();
            tenantsSnapshotPanel.SuspendLayout();
            pnlSnapshotActions.SuspendLayout();
            pnlSnapshotCard.SuspendLayout();
            SuspendLayout();
            // 
            // dgvTenants
            // 
            dgvTenants.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTenants.Location = new Point(32, 116);
            dgvTenants.Name = "dgvTenants";
            dgvTenants.RowHeadersWidth = 51;
            dgvTenants.Size = new Size(499, 271);
            dgvTenants.TabIndex = 0;
            dgvTenants.CellClick += dgvTenants_CellClick;
            dgvTenants.CellContentClick += dgvTenants_CellContentClick_1;
            // 
            // topBar
            // 
            topBar.BackColor = Color.WhiteSmoke;
            topBar.Controls.Add(lblStatus);
            topBar.Controls.Add(lblBh);
            topBar.Controls.Add(btnSearch);
            topBar.Controls.Add(cbStatusFilter);
            topBar.Controls.Add(txtSearch);
            topBar.Controls.Add(cbBoardingHouses);
            topBar.Controls.Add(addTenantsBtn);
            topBar.Location = new Point(0, 0);
            topBar.Name = "topBar";
            topBar.Padding = new Padding(18, 10, 18, 10);
            topBar.Size = new Size(1243, 92);
            topBar.TabIndex = 1;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(285, 17);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(49, 20);
            lblStatus.TabIndex = 4;
            lblStatus.Text = "Status";
            // 
            // lblBh
            // 
            lblBh.AutoSize = true;
            lblBh.Location = new Point(21, 17);
            lblBh.Name = "lblBh";
            lblBh.Size = new Size(116, 20);
            lblBh.TabIndex = 0;
            lblBh.Text = "Boarding House";
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(781, 40);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(85, 28);
            btnSearch.TabIndex = 6;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // cbStatusFilter
            // 
            cbStatusFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cbStatusFilter.FormattingEnabled = true;
            cbStatusFilter.Location = new Point(285, 40);
            cbStatusFilter.Name = "cbStatusFilter";
            cbStatusFilter.Size = new Size(220, 28);
            cbStatusFilter.TabIndex = 5;
            // 
            // txtSearch
            // 
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Location = new Point(525, 40);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "search";
            txtSearch.Size = new Size(250, 27);
            txtSearch.TabIndex = 3;
            // 
            // cbBoardingHouses
            // 
            cbBoardingHouses.DropDownStyle = ComboBoxStyle.DropDownList;
            cbBoardingHouses.FormattingEnabled = true;
            cbBoardingHouses.Location = new Point(21, 40);
            cbBoardingHouses.Name = "cbBoardingHouses";
            cbBoardingHouses.Size = new Size(240, 28);
            cbBoardingHouses.TabIndex = 1;
            cbBoardingHouses.SelectedIndexChanged += cbBoardingHouses_SelectedIndexChanged;
            // 
            // addTenantsBtn
            // 
            addTenantsBtn.Location = new Point(1052, 45);
            addTenantsBtn.Name = "addTenantsBtn";
            addTenantsBtn.Size = new Size(104, 34);
            addTenantsBtn.TabIndex = 8;
            addTenantsBtn.Text = "+ Tenants";
            addTenantsBtn.UseVisualStyleBackColor = true;
            addTenantsBtn.Click += addTenantsBtn_Click;
            // 
            // lblTenantTitle
            // 
            lblTenantTitle.AutoSize = true;
            lblTenantTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTenantTitle.ForeColor = SystemColors.ButtonHighlight;
            lblTenantTitle.Location = new Point(28, 28);
            lblTenantTitle.Name = "lblTenantTitle";
            lblTenantTitle.Size = new Size(136, 25);
            lblTenantTitle.TabIndex = 1;
            lblTenantTitle.Text = "Tenant Details";
            // 
            // grpQuickActions
            // 
            grpQuickActions.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpQuickActions.Controls.Add(btnMarkInactive);
            grpQuickActions.Controls.Add(btnMarkMaintenance);
            grpQuickActions.Controls.Add(btnMarkOccupied);
            grpQuickActions.Controls.Add(btnMarkAvailable);
            grpQuickActions.ForeColor = SystemColors.ButtonHighlight;
            grpQuickActions.Location = new Point(28, 1657);
            grpQuickActions.Name = "grpQuickActions";
            grpQuickActions.Padding = new Padding(12);
            grpQuickActions.Size = new Size(496, 154);
            grpQuickActions.TabIndex = 3;
            grpQuickActions.TabStop = false;
            grpQuickActions.Text = "Quick Actions";
            // 
            // btnMarkInactive
            // 
            btnMarkInactive.BackColor = Color.Gray;
            btnMarkInactive.ForeColor = SystemColors.ActiveCaptionText;
            btnMarkInactive.Location = new Point(164, 78);
            btnMarkInactive.Name = "btnMarkInactive";
            btnMarkInactive.Size = new Size(138, 34);
            btnMarkInactive.TabIndex = 3;
            btnMarkInactive.Text = "INACTIVE";
            btnMarkInactive.UseVisualStyleBackColor = false;
            // 
            // btnMarkMaintenance
            // 
            btnMarkMaintenance.BackColor = Color.FromArgb(255, 128, 128);
            btnMarkMaintenance.ForeColor = SystemColors.ActiveCaptionText;
            btnMarkMaintenance.Location = new Point(12, 78);
            btnMarkMaintenance.Name = "btnMarkMaintenance";
            btnMarkMaintenance.Size = new Size(138, 34);
            btnMarkMaintenance.TabIndex = 2;
            btnMarkMaintenance.Text = "MAINTENANCE";
            btnMarkMaintenance.UseVisualStyleBackColor = false;
            // 
            // btnMarkOccupied
            // 
            btnMarkOccupied.BackColor = Color.FromArgb(255, 255, 128);
            btnMarkOccupied.ForeColor = SystemColors.ActiveCaptionText;
            btnMarkOccupied.Location = new Point(164, 32);
            btnMarkOccupied.Name = "btnMarkOccupied";
            btnMarkOccupied.Size = new Size(138, 34);
            btnMarkOccupied.TabIndex = 1;
            btnMarkOccupied.Text = "OCCUPIED";
            btnMarkOccupied.UseVisualStyleBackColor = false;
            // 
            // btnMarkAvailable
            // 
            btnMarkAvailable.BackColor = Color.FromArgb(128, 255, 128);
            btnMarkAvailable.ForeColor = SystemColors.ActiveCaptionText;
            btnMarkAvailable.Location = new Point(12, 32);
            btnMarkAvailable.Name = "btnMarkAvailable";
            btnMarkAvailable.Size = new Size(138, 34);
            btnMarkAvailable.TabIndex = 0;
            btnMarkAvailable.Text = "AVAILABLE";
            btnMarkAvailable.UseVisualStyleBackColor = false;
            // 
            // btnCloseDetails
            // 
            btnCloseDetails.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCloseDetails.BackColor = Color.FromArgb(255, 128, 128);
            btnCloseDetails.Location = new Point(489, 27);
            btnCloseDetails.Name = "btnCloseDetails";
            btnCloseDetails.Size = new Size(45, 28);
            btnCloseDetails.TabIndex = 4;
            btnCloseDetails.Text = "X";
            btnCloseDetails.UseVisualStyleBackColor = false;
            // 
            // detailsModal
            // 
            detailsModal.BackColor = Color.FromArgb(48, 54, 92);
            detailsModal.BorderStyle = BorderStyle.FixedSingle;
            detailsModal.Controls.Add(btnCloseDetails);
            detailsModal.Controls.Add(grpQuickActions);
            detailsModal.Controls.Add(grpDetails);
            detailsModal.Controls.Add(lblTenantTitle);
            detailsModal.Dock = DockStyle.Right;
            detailsModal.Location = new Point(1264, 0);
            detailsModal.Name = "detailsModal";
            detailsModal.Padding = new Padding(16);
            detailsModal.Size = new Size(413, 975);
            detailsModal.TabIndex = 3;
            detailsModal.Paint += detailsModal_Paint_1;
            // 
            // grpDetails
            // 
            grpDetails.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpDetails.Controls.Add(ViewRoomBtn);
            grpDetails.Controls.Add(detailsOpenCameraBtn);
            grpDetails.Controls.Add(detailsBrowseProfileBtn);
            grpDetails.Controls.Add(details_profilePathTxt);
            grpDetails.Controls.Add(detailsTenantImg);
            grpDetails.Controls.Add(endRentalBtn);
            grpDetails.Controls.Add(btnStartRental);
            grpDetails.Controls.Add(btnViewCurrentRental);
            grpDetails.Controls.Add(cbDetailsRoom);
            grpDetails.Controls.Add(label4);
            grpDetails.Controls.Add(tenantUpdateBtn);
            grpDetails.Controls.Add(tenantDeleteBtn);
            grpDetails.Controls.Add(detailsCbStatus);
            grpDetails.Controls.Add(label6);
            grpDetails.Controls.Add(detailsEContact);
            grpDetails.Controls.Add(label1);
            grpDetails.Controls.Add(detailsEName);
            grpDetails.Controls.Add(label2);
            grpDetails.Controls.Add(detailsAddress);
            grpDetails.Controls.Add(label3);
            grpDetails.Controls.Add(detailsEmail);
            grpDetails.Controls.Add(labelRoomStatus);
            grpDetails.Controls.Add(detailsContact);
            grpDetails.Controls.Add(labelRate);
            grpDetails.Controls.Add(detailsMiddlename);
            grpDetails.Controls.Add(labelCap);
            grpDetails.Controls.Add(detailsFirstname);
            grpDetails.Controls.Add(labelType);
            grpDetails.Controls.Add(detailsLastname);
            grpDetails.Controls.Add(labelRoomNo);
            grpDetails.ForeColor = SystemColors.ButtonHighlight;
            grpDetails.Location = new Point(16, 59);
            grpDetails.Name = "grpDetails";
            grpDetails.Padding = new Padding(12);
            grpDetails.Size = new Size(376, 799);
            grpDetails.TabIndex = 2;
            grpDetails.TabStop = false;
            grpDetails.Text = "Details";
            grpDetails.Enter += grpDetails_Enter;
            // 
            // ViewRoomBtn
            // 
            ViewRoomBtn.AutoSize = true;
            ViewRoomBtn.Location = new Point(312, 666);
            ViewRoomBtn.Name = "ViewRoomBtn";
            ViewRoomBtn.Size = new Size(30, 20);
            ViewRoomBtn.TabIndex = 70;
            ViewRoomBtn.Text = "👁";
            ViewRoomBtn.Visible = false;
            ViewRoomBtn.Click += ViewRoomBtn_Click;
            // 
            // detailsOpenCameraBtn
            // 
            detailsOpenCameraBtn.ForeColor = SystemColors.ActiveCaptionText;
            detailsOpenCameraBtn.Location = new Point(260, 51);
            detailsOpenCameraBtn.Name = "detailsOpenCameraBtn";
            detailsOpenCameraBtn.Size = new Size(85, 28);
            detailsOpenCameraBtn.TabIndex = 69;
            detailsOpenCameraBtn.Text = "Camera";
            detailsOpenCameraBtn.UseVisualStyleBackColor = true;
            detailsOpenCameraBtn.Click += detailsOpenCameraBtn_Click;
            // 
            // detailsBrowseProfileBtn
            // 
            detailsBrowseProfileBtn.ForeColor = SystemColors.ActiveCaptionText;
            detailsBrowseProfileBtn.Location = new Point(260, 17);
            detailsBrowseProfileBtn.Name = "detailsBrowseProfileBtn";
            detailsBrowseProfileBtn.Size = new Size(85, 28);
            detailsBrowseProfileBtn.TabIndex = 68;
            detailsBrowseProfileBtn.Text = "Browse";
            detailsBrowseProfileBtn.UseVisualStyleBackColor = true;
            detailsBrowseProfileBtn.Click += detailsBrowseProfileBtn_Click;
            // 
            // details_profilePathTxt
            // 
            details_profilePathTxt.Location = new Point(117, 17);
            details_profilePathTxt.Multiline = true;
            details_profilePathTxt.Name = "details_profilePathTxt";
            details_profilePathTxt.Size = new Size(136, 10);
            details_profilePathTxt.TabIndex = 67;
            details_profilePathTxt.Visible = false;
            // 
            // detailsTenantImg
            // 
            detailsTenantImg.BackColor = Color.White;
            detailsTenantImg.Location = new Point(117, 17);
            detailsTenantImg.Name = "detailsTenantImg";
            detailsTenantImg.Size = new Size(136, 135);
            detailsTenantImg.SizeMode = PictureBoxSizeMode.StretchImage;
            detailsTenantImg.TabIndex = 66;
            detailsTenantImg.TabStop = false;
            // 
            // endRentalBtn
            // 
            endRentalBtn.BackColor = Color.FromArgb(255, 255, 192);
            endRentalBtn.ForeColor = SystemColors.ActiveCaptionText;
            endRentalBtn.Location = new Point(141, 750);
            endRentalBtn.Name = "endRentalBtn";
            endRentalBtn.Size = new Size(85, 34);
            endRentalBtn.TabIndex = 23;
            endRentalBtn.Text = "End Rental";
            endRentalBtn.UseVisualStyleBackColor = false;
            endRentalBtn.Visible = false;
            endRentalBtn.Click += endRentalBtn_Click;
            // 
            // btnStartRental
            // 
            btnStartRental.BackColor = Color.FromArgb(255, 224, 192);
            btnStartRental.ForeColor = SystemColors.ActiveCaptionText;
            btnStartRental.Location = new Point(191, 721);
            btnStartRental.Name = "btnStartRental";
            btnStartRental.Size = new Size(151, 28);
            btnStartRental.TabIndex = 32;
            btnStartRental.Text = "Start Rental";
            btnStartRental.UseVisualStyleBackColor = false;
            btnStartRental.Visible = false;
            btnStartRental.Click += btnStartRental_Click;
            // 
            // btnViewCurrentRental
            // 
            btnViewCurrentRental.BackColor = Color.FromArgb(192, 255, 255);
            btnViewCurrentRental.ForeColor = SystemColors.ActiveCaptionText;
            btnViewCurrentRental.Location = new Point(191, 689);
            btnViewCurrentRental.Name = "btnViewCurrentRental";
            btnViewCurrentRental.Size = new Size(151, 28);
            btnViewCurrentRental.TabIndex = 31;
            btnViewCurrentRental.Text = "View Current Rental";
            btnViewCurrentRental.UseVisualStyleBackColor = false;
            btnViewCurrentRental.Visible = false;
            btnViewCurrentRental.Click += btnViewCurrentRental_Click;
            // 
            // cbDetailsRoom
            // 
            cbDetailsRoom.FormattingEnabled = true;
            cbDetailsRoom.Location = new Point(191, 689);
            cbDetailsRoom.Name = "cbDetailsRoom";
            cbDetailsRoom.Size = new Size(151, 28);
            cbDetailsRoom.TabIndex = 30;
            cbDetailsRoom.Visible = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = SystemColors.ButtonHighlight;
            label4.Location = new Point(191, 666);
            label4.Name = "label4";
            label4.Size = new Size(49, 20);
            label4.TabIndex = 29;
            label4.Text = "Room";
            label4.Visible = false;
            label4.Click += label4_Click;
            // 
            // tenantUpdateBtn
            // 
            tenantUpdateBtn.BackColor = Color.FromArgb(128, 255, 128);
            tenantUpdateBtn.ForeColor = SystemColors.ActiveCaptionText;
            tenantUpdateBtn.Location = new Point(257, 750);
            tenantUpdateBtn.Name = "tenantUpdateBtn";
            tenantUpdateBtn.Size = new Size(85, 34);
            tenantUpdateBtn.TabIndex = 23;
            tenantUpdateBtn.Text = "Update";
            tenantUpdateBtn.UseVisualStyleBackColor = false;
            tenantUpdateBtn.Visible = false;
            tenantUpdateBtn.Click += tenantUpdateBtn_Click_1;
            // 
            // tenantDeleteBtn
            // 
            tenantDeleteBtn.BackColor = Color.FromArgb(255, 192, 192);
            tenantDeleteBtn.ForeColor = SystemColors.ActiveCaptionText;
            tenantDeleteBtn.Location = new Point(24, 750);
            tenantDeleteBtn.Name = "tenantDeleteBtn";
            tenantDeleteBtn.Size = new Size(85, 34);
            tenantDeleteBtn.TabIndex = 22;
            tenantDeleteBtn.Text = "Delete";
            tenantDeleteBtn.UseVisualStyleBackColor = false;
            tenantDeleteBtn.Visible = false;
            tenantDeleteBtn.Click += tenantDeleteBtn_Click_1;
            // 
            // detailsCbStatus
            // 
            detailsCbStatus.FormattingEnabled = true;
            detailsCbStatus.Location = new Point(24, 689);
            detailsCbStatus.Name = "detailsCbStatus";
            detailsCbStatus.Size = new Size(151, 28);
            detailsCbStatus.TabIndex = 28;
            detailsCbStatus.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = SystemColors.ButtonHighlight;
            label6.Location = new Point(24, 666);
            label6.Name = "label6";
            label6.Size = new Size(49, 20);
            label6.TabIndex = 27;
            label6.Text = "Status";
            // 
            // detailsEContact
            // 
            detailsEContact.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            detailsEContact.Location = new Point(24, 623);
            detailsEContact.Name = "detailsEContact";
            detailsEContact.ReadOnly = true;
            detailsEContact.Size = new Size(318, 27);
            detailsEContact.TabIndex = 26;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(24, 600);
            label1.Name = "label1";
            label1.Size = new Size(161, 20);
            label1.TabIndex = 25;
            label1.Text = "Emergency Contact No";
            // 
            // detailsEName
            // 
            detailsEName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            detailsEName.Location = new Point(24, 556);
            detailsEName.Name = "detailsEName";
            detailsEName.Size = new Size(318, 27);
            detailsEName.TabIndex = 24;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(24, 533);
            label2.Name = "label2";
            label2.Size = new Size(181, 20);
            label2.TabIndex = 23;
            label2.Text = "Emergency Contact Name";
            // 
            // detailsAddress
            // 
            detailsAddress.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            detailsAddress.Location = new Point(24, 492);
            detailsAddress.Name = "detailsAddress";
            detailsAddress.Size = new Size(318, 27);
            detailsAddress.TabIndex = 22;
            detailsAddress.TextChanged += textBox3_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = SystemColors.ButtonHighlight;
            label3.Location = new Point(24, 469);
            label3.Name = "label3";
            label3.Size = new Size(62, 20);
            label3.TabIndex = 21;
            label3.Text = "Address";
            // 
            // detailsEmail
            // 
            detailsEmail.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            detailsEmail.Location = new Point(24, 427);
            detailsEmail.Name = "detailsEmail";
            detailsEmail.ReadOnly = true;
            detailsEmail.Size = new Size(318, 27);
            detailsEmail.TabIndex = 20;
            // 
            // labelRoomStatus
            // 
            labelRoomStatus.AutoSize = true;
            labelRoomStatus.ForeColor = SystemColors.ButtonHighlight;
            labelRoomStatus.Location = new Point(24, 404);
            labelRoomStatus.Name = "labelRoomStatus";
            labelRoomStatus.Size = new Size(46, 20);
            labelRoomStatus.TabIndex = 19;
            labelRoomStatus.Text = "Email";
            // 
            // detailsContact
            // 
            detailsContact.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            detailsContact.Location = new Point(24, 360);
            detailsContact.Name = "detailsContact";
            detailsContact.Size = new Size(318, 27);
            detailsContact.TabIndex = 18;
            detailsContact.TextChanged += detailsRate_TextChanged;
            // 
            // labelRate
            // 
            labelRate.AutoSize = true;
            labelRate.ForeColor = SystemColors.ButtonHighlight;
            labelRate.Location = new Point(24, 337);
            labelRate.Name = "labelRate";
            labelRate.Size = new Size(60, 20);
            labelRate.TabIndex = 17;
            labelRate.Text = "Contact";
            // 
            // detailsMiddlename
            // 
            detailsMiddlename.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            detailsMiddlename.Location = new Point(24, 300);
            detailsMiddlename.Name = "detailsMiddlename";
            detailsMiddlename.Size = new Size(318, 27);
            detailsMiddlename.TabIndex = 16;
            // 
            // labelCap
            // 
            labelCap.AutoSize = true;
            labelCap.ForeColor = SystemColors.ButtonHighlight;
            labelCap.Location = new Point(24, 279);
            labelCap.Name = "labelCap";
            labelCap.Size = new Size(93, 20);
            labelCap.TabIndex = 15;
            labelCap.Text = "Middlename";
            // 
            // detailsFirstname
            // 
            detailsFirstname.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            detailsFirstname.Location = new Point(24, 238);
            detailsFirstname.Name = "detailsFirstname";
            detailsFirstname.Size = new Size(318, 27);
            detailsFirstname.TabIndex = 14;
            // 
            // labelType
            // 
            labelType.AutoSize = true;
            labelType.ForeColor = SystemColors.ButtonHighlight;
            labelType.Location = new Point(24, 215);
            labelType.Name = "labelType";
            labelType.Size = new Size(73, 20);
            labelType.TabIndex = 13;
            labelType.Text = "Firstname";
            // 
            // detailsLastname
            // 
            detailsLastname.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            detailsLastname.Location = new Point(24, 176);
            detailsLastname.Name = "detailsLastname";
            detailsLastname.Size = new Size(318, 27);
            detailsLastname.TabIndex = 12;
            detailsLastname.TextChanged += detailsRoomNo_TextChanged;
            // 
            // labelRoomNo
            // 
            labelRoomNo.AutoSize = true;
            labelRoomNo.ForeColor = SystemColors.ButtonHighlight;
            labelRoomNo.Location = new Point(24, 153);
            labelRoomNo.Name = "labelRoomNo";
            labelRoomNo.Size = new Size(72, 20);
            labelRoomNo.TabIndex = 11;
            labelRoomNo.Text = "Lastname";
            labelRoomNo.Click += labelRoomNo_Click;
            // 
            // addTenantsModal
            // 
            addTenantsModal.Anchor = AnchorStyles.None;
            addTenantsModal.BackColor = Color.FromArgb(48, 54, 92);
            addTenantsModal.BorderStyle = BorderStyle.FixedSingle;
            addTenantsModal.Controls.Add(registrationOpenCameraBtn);
            addTenantsModal.Controls.Add(browseProfileBtn);
            addTenantsModal.Controls.Add(profilePathTxt);
            addTenantsModal.Controls.Add(addTenantImg);
            addTenantsModal.Controls.Add(cancelTenantRegister);
            addTenantsModal.Controls.Add(registerTenantBtn);
            addTenantsModal.Controls.Add(tenantAddressTxt);
            addTenantsModal.Controls.Add(label16);
            addTenantsModal.Controls.Add(tenantEmergencyContactTxt);
            addTenantsModal.Controls.Add(label11);
            addTenantsModal.Controls.Add(tenantEmergencyNameTxt);
            addTenantsModal.Controls.Add(label10);
            addTenantsModal.Controls.Add(tenantEmailTxt);
            addTenantsModal.Controls.Add(label8);
            addTenantsModal.Controls.Add(tenantContactTxt);
            addTenantsModal.Controls.Add(label7);
            addTenantsModal.Controls.Add(tenantMiddleNameTxt);
            addTenantsModal.Controls.Add(label5);
            addTenantsModal.Controls.Add(tenantFirstNameTxt);
            addTenantsModal.Controls.Add(label9);
            addTenantsModal.Controls.Add(tenantLastNameTxt);
            addTenantsModal.Controls.Add(label12);
            addTenantsModal.Controls.Add(addTenantCloseBtn);
            addTenantsModal.Controls.Add(totalTenants);
            addTenantsModal.Controls.Add(label15);
            addTenantsModal.ForeColor = SystemColors.ButtonHighlight;
            addTenantsModal.Location = new Point(367, 98);
            addTenantsModal.Name = "addTenantsModal";
            addTenantsModal.Size = new Size(718, 599);
            addTenantsModal.TabIndex = 4;
            addTenantsModal.Visible = false;
            addTenantsModal.Paint += addTenantsModal_Paint;
            // 
            // registrationOpenCameraBtn
            // 
            registrationOpenCameraBtn.ForeColor = SystemColors.ActiveCaptionText;
            registrationOpenCameraBtn.Location = new Point(425, 109);
            registrationOpenCameraBtn.Name = "registrationOpenCameraBtn";
            registrationOpenCameraBtn.Size = new Size(85, 28);
            registrationOpenCameraBtn.TabIndex = 66;
            registrationOpenCameraBtn.Text = "Camera";
            registrationOpenCameraBtn.UseVisualStyleBackColor = true;
            registrationOpenCameraBtn.Click += registrationOpenCameraBtn_Click;
            // 
            // browseProfileBtn
            // 
            browseProfileBtn.ForeColor = SystemColors.ActiveCaptionText;
            browseProfileBtn.Location = new Point(425, 75);
            browseProfileBtn.Name = "browseProfileBtn";
            browseProfileBtn.Size = new Size(85, 28);
            browseProfileBtn.TabIndex = 65;
            browseProfileBtn.Text = "Browse";
            browseProfileBtn.UseVisualStyleBackColor = true;
            browseProfileBtn.Click += browseProfileBtn_Click;
            // 
            // profilePathTxt
            // 
            profilePathTxt.Location = new Point(282, 75);
            profilePathTxt.Multiline = true;
            profilePathTxt.Name = "profilePathTxt";
            profilePathTxt.Size = new Size(136, 10);
            profilePathTxt.TabIndex = 64;
            profilePathTxt.Visible = false;
            // 
            // addTenantImg
            // 
            addTenantImg.BackColor = Color.White;
            addTenantImg.Location = new Point(282, 75);
            addTenantImg.Name = "addTenantImg";
            addTenantImg.Size = new Size(136, 131);
            addTenantImg.SizeMode = PictureBoxSizeMode.StretchImage;
            addTenantImg.TabIndex = 63;
            addTenantImg.TabStop = false;
            // 
            // cancelTenantRegister
            // 
            cancelTenantRegister.BackColor = Color.WhiteSmoke;
            cancelTenantRegister.ForeColor = SystemColors.ActiveCaptionText;
            cancelTenantRegister.Location = new Point(139, 522);
            cancelTenantRegister.Name = "cancelTenantRegister";
            cancelTenantRegister.Size = new Size(158, 45);
            cancelTenantRegister.TabIndex = 62;
            cancelTenantRegister.Text = "Cancel";
            cancelTenantRegister.UseVisualStyleBackColor = false;
            cancelTenantRegister.Click += cancelTenantRegister_Click;
            // 
            // registerTenantBtn
            // 
            registerTenantBtn.BackColor = Color.FromArgb(128, 255, 128);
            registerTenantBtn.ForeColor = SystemColors.ActiveCaptionText;
            registerTenantBtn.Location = new Point(415, 522);
            registerTenantBtn.Name = "registerTenantBtn";
            registerTenantBtn.Size = new Size(158, 45);
            registerTenantBtn.TabIndex = 61;
            registerTenantBtn.Text = "Register Tenant";
            registerTenantBtn.UseVisualStyleBackColor = false;
            registerTenantBtn.Click += registerTenantBtn_Click;
            // 
            // tenantAddressTxt
            // 
            tenantAddressTxt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tenantAddressTxt.Location = new Point(368, 243);
            tenantAddressTxt.Multiline = true;
            tenantAddressTxt.Name = "tenantAddressTxt";
            tenantAddressTxt.Size = new Size(294, 40);
            tenantAddressTxt.TabIndex = 60;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.ForeColor = SystemColors.ButtonHighlight;
            label16.Location = new Point(368, 225);
            label16.Name = "label16";
            label16.Size = new Size(69, 20);
            label16.TabIndex = 59;
            label16.Text = "Address :";
            // 
            // tenantEmergencyContactTxt
            // 
            tenantEmergencyContactTxt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tenantEmergencyContactTxt.Location = new Point(368, 433);
            tenantEmergencyContactTxt.Multiline = true;
            tenantEmergencyContactTxt.Name = "tenantEmergencyContactTxt";
            tenantEmergencyContactTxt.Size = new Size(294, 40);
            tenantEmergencyContactTxt.TabIndex = 56;
            tenantEmergencyContactTxt.TextChanged += tenantEmergencyContactTxt_TextChanged;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.ForeColor = SystemColors.ButtonHighlight;
            label11.Location = new Point(368, 410);
            label11.Name = "label11";
            label11.Size = new Size(144, 20);
            label11.TabIndex = 55;
            label11.Text = "Emergency Contact :";
            // 
            // tenantEmergencyNameTxt
            // 
            tenantEmergencyNameTxt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tenantEmergencyNameTxt.Location = new Point(368, 367);
            tenantEmergencyNameTxt.Multiline = true;
            tenantEmergencyNameTxt.Name = "tenantEmergencyNameTxt";
            tenantEmergencyNameTxt.Size = new Size(294, 40);
            tenantEmergencyNameTxt.TabIndex = 54;
            tenantEmergencyNameTxt.TextChanged += tenantEmergencyNameTxt_TextChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.ForeColor = SystemColors.ButtonHighlight;
            label10.Location = new Point(370, 344);
            label10.Name = "label10";
            label10.Size = new Size(129, 20);
            label10.TabIndex = 53;
            label10.Text = "Emergency Name:";
            // 
            // tenantEmailTxt
            // 
            tenantEmailTxt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tenantEmailTxt.Location = new Point(40, 433);
            tenantEmailTxt.Multiline = true;
            tenantEmailTxt.Name = "tenantEmailTxt";
            tenantEmailTxt.Size = new Size(294, 40);
            tenantEmailTxt.TabIndex = 52;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.ForeColor = SystemColors.ButtonHighlight;
            label8.Location = new Point(40, 415);
            label8.Name = "label8";
            label8.Size = new Size(49, 20);
            label8.TabIndex = 51;
            label8.Text = "Email:";
            // 
            // tenantContactTxt
            // 
            tenantContactTxt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tenantContactTxt.Location = new Point(368, 302);
            tenantContactTxt.Multiline = true;
            tenantContactTxt.Name = "tenantContactTxt";
            tenantContactTxt.Size = new Size(294, 40);
            tenantContactTxt.TabIndex = 50;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = SystemColors.ButtonHighlight;
            label7.Location = new Point(368, 284);
            label7.Name = "label7";
            label7.Size = new Size(67, 20);
            label7.TabIndex = 49;
            label7.Text = "Contact :";
            // 
            // tenantMiddleNameTxt
            // 
            tenantMiddleNameTxt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tenantMiddleNameTxt.Location = new Point(40, 368);
            tenantMiddleNameTxt.Multiline = true;
            tenantMiddleNameTxt.Name = "tenantMiddleNameTxt";
            tenantMiddleNameTxt.Size = new Size(294, 40);
            tenantMiddleNameTxt.TabIndex = 48;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = SystemColors.ButtonHighlight;
            label5.Location = new Point(40, 350);
            label5.Name = "label5";
            label5.Size = new Size(100, 20);
            label5.TabIndex = 47;
            label5.Text = "Middle name:";
            // 
            // tenantFirstNameTxt
            // 
            tenantFirstNameTxt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tenantFirstNameTxt.Location = new Point(40, 304);
            tenantFirstNameTxt.Multiline = true;
            tenantFirstNameTxt.Name = "tenantFirstNameTxt";
            tenantFirstNameTxt.Size = new Size(294, 40);
            tenantFirstNameTxt.TabIndex = 46;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.ForeColor = SystemColors.ButtonHighlight;
            label9.Location = new Point(40, 286);
            label9.Name = "label9";
            label9.Size = new Size(76, 20);
            label9.TabIndex = 45;
            label9.Text = "Firstname:";
            // 
            // tenantLastNameTxt
            // 
            tenantLastNameTxt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tenantLastNameTxt.Location = new Point(40, 240);
            tenantLastNameTxt.Multiline = true;
            tenantLastNameTxt.Name = "tenantLastNameTxt";
            tenantLastNameTxt.Size = new Size(294, 40);
            tenantLastNameTxt.TabIndex = 44;
            tenantLastNameTxt.TextChanged += tenantLastNameTxt_TextChanged;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.ForeColor = SystemColors.ButtonHighlight;
            label12.Location = new Point(42, 217);
            label12.Name = "label12";
            label12.Size = new Size(75, 20);
            label12.TabIndex = 43;
            label12.Text = "Lastname:";
            // 
            // addTenantCloseBtn
            // 
            addTenantCloseBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            addTenantCloseBtn.BackColor = Color.FromArgb(255, 128, 128);
            addTenantCloseBtn.ForeColor = SystemColors.ActiveCaptionText;
            addTenantCloseBtn.Location = new Point(668, -1);
            addTenantCloseBtn.Name = "addTenantCloseBtn";
            addTenantCloseBtn.Size = new Size(45, 28);
            addTenantCloseBtn.TabIndex = 42;
            addTenantCloseBtn.Text = "X";
            addTenantCloseBtn.UseVisualStyleBackColor = false;
            addTenantCloseBtn.Click += addTenantCloseBtn_Click;
            // 
            // totalTenants
            // 
            totalTenants.AutoSize = true;
            totalTenants.Location = new Point(20, 52);
            totalTenants.Name = "totalTenants";
            totalTenants.Size = new Size(50, 20);
            totalTenants.TabIndex = 41;
            totalTenants.Text = "(total)";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI Semibold", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label15.Location = new Point(3, 3);
            label15.Name = "label15";
            label15.Size = new Size(347, 50);
            label15.TabIndex = 0;
            label15.Text = "Tenant Registration";
            // 
            // paymentHistoryPanel
            // 
            paymentHistoryPanel.BackColor = Color.Gainsboro;
            paymentHistoryPanel.Controls.Add(label17);
            paymentHistoryPanel.Controls.Add(label14);
            paymentHistoryPanel.Controls.Add(dataGridView1);
            paymentHistoryPanel.Controls.Add(label13);
            paymentHistoryPanel.Location = new Point(32, 414);
            paymentHistoryPanel.Name = "paymentHistoryPanel";
            paymentHistoryPanel.Size = new Size(499, 445);
            paymentHistoryPanel.TabIndex = 5;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            label17.ForeColor = SystemColors.ActiveCaptionText;
            label17.Location = new Point(392, 36);
            label17.Name = "label17";
            label17.Size = new Size(67, 25);
            label17.TabIndex = 4;
            label17.Text = "₱ 0.00";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            label14.ForeColor = SystemColors.ActiveCaptionText;
            label14.Location = new Point(360, 11);
            label14.Name = "label14";
            label14.Size = new Size(138, 25);
            label14.TabIndex = 3;
            label14.Text = "Total Payment";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(0, 87);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(499, 343);
            dataGridView1.TabIndex = 2;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI Semibold", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label13.Location = new Point(3, 11);
            label13.Name = "label13";
            label13.Size = new Size(301, 50);
            label13.TabIndex = 1;
            label13.Text = "Payment History";
            // 
            // tenantsSnapshotPanel
            // 
            tenantsSnapshotPanel.BackColor = Color.Gainsboro;
            tenantsSnapshotPanel.Controls.Add(pnlSnapshotActions);
            tenantsSnapshotPanel.Controls.Add(pnlSnapshotCard);
            tenantsSnapshotPanel.Controls.Add(pnlSnapshotDivider);
            tenantsSnapshotPanel.Controls.Add(lblSnapshotTitle);
            tenantsSnapshotPanel.Location = new Point(550, 116);
            tenantsSnapshotPanel.Name = "tenantsSnapshotPanel";
            tenantsSnapshotPanel.Size = new Size(693, 743);
            tenantsSnapshotPanel.TabIndex = 6;
            // 
            // pnlSnapshotActions
            // 
            pnlSnapshotActions.BackColor = Color.WhiteSmoke;
            pnlSnapshotActions.BorderStyle = BorderStyle.FixedSingle;
            pnlSnapshotActions.Controls.Add(lblSnapshotHint);
            pnlSnapshotActions.Controls.Add(btnSnapshotOpenPayments);
            pnlSnapshotActions.Controls.Add(btnSnapshotViewRoom);
            pnlSnapshotActions.Controls.Add(btnSnapshotRefresh);
            pnlSnapshotActions.Location = new Point(20, 545);
            pnlSnapshotActions.Name = "pnlSnapshotActions";
            pnlSnapshotActions.Size = new Size(653, 170);
            pnlSnapshotActions.TabIndex = 3;
            // 
            // lblSnapshotHint
            // 
            lblSnapshotHint.AutoSize = true;
            lblSnapshotHint.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSnapshotHint.Location = new Point(20, 15);
            lblSnapshotHint.Name = "lblSnapshotHint";
            lblSnapshotHint.Size = new Size(143, 28);
            lblSnapshotHint.TabIndex = 0;
            lblSnapshotHint.Text = "Quick Actions";
            // 
            // btnSnapshotOpenPayments
            // 
            btnSnapshotOpenPayments.Location = new Point(20, 60);
            btnSnapshotOpenPayments.Name = "btnSnapshotOpenPayments";
            btnSnapshotOpenPayments.Size = new Size(180, 45);
            btnSnapshotOpenPayments.TabIndex = 1;
            btnSnapshotOpenPayments.Text = "Open Payments";
            btnSnapshotOpenPayments.UseVisualStyleBackColor = true;
            btnSnapshotOpenPayments.Click += btnSnapshotOpenPayments_Click;
            // 
            // btnSnapshotViewRoom
            // 
            btnSnapshotViewRoom.Location = new Point(220, 60);
            btnSnapshotViewRoom.Name = "btnSnapshotViewRoom";
            btnSnapshotViewRoom.Size = new Size(180, 45);
            btnSnapshotViewRoom.TabIndex = 2;
            btnSnapshotViewRoom.Text = "View Room";
            btnSnapshotViewRoom.UseVisualStyleBackColor = true;
            btnSnapshotViewRoom.Click += btnSnapshotViewRoom_Click;
            // 
            // btnSnapshotRefresh
            // 
            btnSnapshotRefresh.Location = new Point(420, 60);
            btnSnapshotRefresh.Name = "btnSnapshotRefresh";
            btnSnapshotRefresh.Size = new Size(180, 45);
            btnSnapshotRefresh.TabIndex = 3;
            btnSnapshotRefresh.Text = "Refresh";
            btnSnapshotRefresh.UseVisualStyleBackColor = true;
            btnSnapshotRefresh.Click += btnSnapshotRefresh_Click;
            // 
            // pnlSnapshotCard
            // 
            pnlSnapshotCard.BackColor = Color.White;
            pnlSnapshotCard.BorderStyle = BorderStyle.FixedSingle;
            pnlSnapshotCard.Controls.Add(lblSnapshotStatusBadge);
            pnlSnapshotCard.Controls.Add(lblFieldTenant);
            pnlSnapshotCard.Controls.Add(lblSnapshotTenant);
            pnlSnapshotCard.Controls.Add(lblFieldTenantId);
            pnlSnapshotCard.Controls.Add(lblSnapshotTenantId);
            pnlSnapshotCard.Controls.Add(lblFieldBoardingHouse);
            pnlSnapshotCard.Controls.Add(lblSnapshotBoardingHouse);
            pnlSnapshotCard.Controls.Add(lblFieldRoomAssigned);
            pnlSnapshotCard.Controls.Add(lblSnapshotRoomAssigned);
            pnlSnapshotCard.Controls.Add(lblFieldRentalStart);
            pnlSnapshotCard.Controls.Add(lblSnapshotRentalStart);
            pnlSnapshotCard.Controls.Add(lblFieldDuration);
            pnlSnapshotCard.Controls.Add(lblSnapshotDuration);
            pnlSnapshotCard.Controls.Add(lblFieldLastPayment);
            pnlSnapshotCard.Controls.Add(lblSnapshotLastPayment);
            pnlSnapshotCard.Controls.Add(lblFieldLastAmount);
            pnlSnapshotCard.Controls.Add(lblSnapshotLastAmount);
            pnlSnapshotCard.Location = new Point(20, 95);
            pnlSnapshotCard.Name = "pnlSnapshotCard";
            pnlSnapshotCard.Size = new Size(653, 430);
            pnlSnapshotCard.TabIndex = 2;
            // 
            // lblSnapshotStatusBadge
            // 
            lblSnapshotStatusBadge.BackColor = Color.SeaGreen;
            lblSnapshotStatusBadge.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSnapshotStatusBadge.ForeColor = Color.White;
            lblSnapshotStatusBadge.Location = new Point(520, 20);
            lblSnapshotStatusBadge.Name = "lblSnapshotStatusBadge";
            lblSnapshotStatusBadge.Size = new Size(110, 32);
            lblSnapshotStatusBadge.TabIndex = 16;
            lblSnapshotStatusBadge.Text = "ACTIVE";
            lblSnapshotStatusBadge.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblFieldTenant
            // 
            lblFieldTenant.AutoSize = true;
            lblFieldTenant.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFieldTenant.ForeColor = Color.DimGray;
            lblFieldTenant.Location = new Point(20, 20);
            lblFieldTenant.Name = "lblFieldTenant";
            lblFieldTenant.Size = new Size(77, 25);
            lblFieldTenant.TabIndex = 0;
            lblFieldTenant.Text = "Tenant:";
            // 
            // lblSnapshotTenant
            // 
            lblSnapshotTenant.AutoSize = true;
            lblSnapshotTenant.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSnapshotTenant.ForeColor = Color.Black;
            lblSnapshotTenant.Location = new Point(260, 20);
            lblSnapshotTenant.Name = "lblSnapshotTenant";
            lblSnapshotTenant.Size = new Size(32, 28);
            lblSnapshotTenant.TabIndex = 1;
            lblSnapshotTenant.Text = "—";
            // 
            // lblFieldTenantId
            // 
            lblFieldTenantId.AutoSize = true;
            lblFieldTenantId.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFieldTenantId.ForeColor = Color.DimGray;
            lblFieldTenantId.Location = new Point(20, 65);
            lblFieldTenantId.Name = "lblFieldTenantId";
            lblFieldTenantId.Size = new Size(102, 25);
            lblFieldTenantId.TabIndex = 2;
            lblFieldTenantId.Text = "Tenant ID:";
            // 
            // lblSnapshotTenantId
            // 
            lblSnapshotTenantId.AutoSize = true;
            lblSnapshotTenantId.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSnapshotTenantId.ForeColor = Color.Black;
            lblSnapshotTenantId.Location = new Point(260, 65);
            lblSnapshotTenantId.Name = "lblSnapshotTenantId";
            lblSnapshotTenantId.Size = new Size(32, 28);
            lblSnapshotTenantId.TabIndex = 3;
            lblSnapshotTenantId.Text = "—";
            // 
            // lblFieldBoardingHouse
            // 
            lblFieldBoardingHouse.AutoSize = true;
            lblFieldBoardingHouse.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFieldBoardingHouse.ForeColor = Color.DimGray;
            lblFieldBoardingHouse.Location = new Point(20, 110);
            lblFieldBoardingHouse.Name = "lblFieldBoardingHouse";
            lblFieldBoardingHouse.Size = new Size(162, 25);
            lblFieldBoardingHouse.TabIndex = 4;
            lblFieldBoardingHouse.Text = "Boarding House:";
            // 
            // lblSnapshotBoardingHouse
            // 
            lblSnapshotBoardingHouse.AutoSize = true;
            lblSnapshotBoardingHouse.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSnapshotBoardingHouse.ForeColor = Color.Black;
            lblSnapshotBoardingHouse.Location = new Point(260, 110);
            lblSnapshotBoardingHouse.Name = "lblSnapshotBoardingHouse";
            lblSnapshotBoardingHouse.Size = new Size(32, 28);
            lblSnapshotBoardingHouse.TabIndex = 5;
            lblSnapshotBoardingHouse.Text = "—";
            // 
            // lblFieldRoomAssigned
            // 
            lblFieldRoomAssigned.AutoSize = true;
            lblFieldRoomAssigned.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFieldRoomAssigned.ForeColor = Color.DimGray;
            lblFieldRoomAssigned.Location = new Point(20, 155);
            lblFieldRoomAssigned.Name = "lblFieldRoomAssigned";
            lblFieldRoomAssigned.Size = new Size(155, 25);
            lblFieldRoomAssigned.TabIndex = 6;
            lblFieldRoomAssigned.Text = "Room Assigned:";
            // 
            // lblSnapshotRoomAssigned
            // 
            lblSnapshotRoomAssigned.AutoSize = true;
            lblSnapshotRoomAssigned.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSnapshotRoomAssigned.ForeColor = Color.Black;
            lblSnapshotRoomAssigned.Location = new Point(260, 155);
            lblSnapshotRoomAssigned.Name = "lblSnapshotRoomAssigned";
            lblSnapshotRoomAssigned.Size = new Size(32, 28);
            lblSnapshotRoomAssigned.TabIndex = 7;
            lblSnapshotRoomAssigned.Text = "—";
            // 
            // lblFieldRentalStart
            // 
            lblFieldRentalStart.AutoSize = true;
            lblFieldRentalStart.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFieldRentalStart.ForeColor = Color.DimGray;
            lblFieldRentalStart.Location = new Point(20, 200);
            lblFieldRentalStart.Name = "lblFieldRentalStart";
            lblFieldRentalStart.Size = new Size(168, 25);
            lblFieldRentalStart.TabIndex = 8;
            lblFieldRentalStart.Text = "Rental Start Date:";
            // 
            // lblSnapshotRentalStart
            // 
            lblSnapshotRentalStart.AutoSize = true;
            lblSnapshotRentalStart.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSnapshotRentalStart.ForeColor = Color.Black;
            lblSnapshotRentalStart.Location = new Point(260, 200);
            lblSnapshotRentalStart.Name = "lblSnapshotRentalStart";
            lblSnapshotRentalStart.Size = new Size(32, 28);
            lblSnapshotRentalStart.TabIndex = 9;
            lblSnapshotRentalStart.Text = "—";
            // 
            // lblFieldDuration
            // 
            lblFieldDuration.AutoSize = true;
            lblFieldDuration.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFieldDuration.ForeColor = Color.DimGray;
            lblFieldDuration.Location = new Point(20, 245);
            lblFieldDuration.Name = "lblFieldDuration";
            lblFieldDuration.Size = new Size(97, 25);
            lblFieldDuration.TabIndex = 10;
            lblFieldDuration.Text = "Duration:";
            // 
            // lblSnapshotDuration
            // 
            lblSnapshotDuration.AutoSize = true;
            lblSnapshotDuration.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSnapshotDuration.ForeColor = Color.Black;
            lblSnapshotDuration.Location = new Point(260, 245);
            lblSnapshotDuration.Name = "lblSnapshotDuration";
            lblSnapshotDuration.Size = new Size(32, 28);
            lblSnapshotDuration.TabIndex = 11;
            lblSnapshotDuration.Text = "—";
            // 
            // lblFieldLastPayment
            // 
            lblFieldLastPayment.AutoSize = true;
            lblFieldLastPayment.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFieldLastPayment.ForeColor = Color.DimGray;
            lblFieldLastPayment.Location = new Point(20, 290);
            lblFieldLastPayment.Name = "lblFieldLastPayment";
            lblFieldLastPayment.Size = new Size(135, 25);
            lblFieldLastPayment.TabIndex = 12;
            lblFieldLastPayment.Text = "Last Payment:";
            // 
            // lblSnapshotLastPayment
            // 
            lblSnapshotLastPayment.AutoSize = true;
            lblSnapshotLastPayment.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSnapshotLastPayment.ForeColor = Color.Black;
            lblSnapshotLastPayment.Location = new Point(260, 290);
            lblSnapshotLastPayment.Name = "lblSnapshotLastPayment";
            lblSnapshotLastPayment.Size = new Size(32, 28);
            lblSnapshotLastPayment.TabIndex = 13;
            lblSnapshotLastPayment.Text = "—";
            // 
            // lblFieldLastAmount
            // 
            lblFieldLastAmount.AutoSize = true;
            lblFieldLastAmount.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFieldLastAmount.ForeColor = Color.DimGray;
            lblFieldLastAmount.Location = new Point(20, 335);
            lblFieldLastAmount.Name = "lblFieldLastAmount";
            lblFieldLastAmount.Size = new Size(130, 25);
            lblFieldLastAmount.TabIndex = 14;
            lblFieldLastAmount.Text = "Last Amount:";
            // 
            // lblSnapshotLastAmount
            // 
            lblSnapshotLastAmount.AutoSize = true;
            lblSnapshotLastAmount.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSnapshotLastAmount.ForeColor = Color.Black;
            lblSnapshotLastAmount.Location = new Point(260, 335);
            lblSnapshotLastAmount.Name = "lblSnapshotLastAmount";
            lblSnapshotLastAmount.Size = new Size(66, 28);
            lblSnapshotLastAmount.TabIndex = 15;
            lblSnapshotLastAmount.Text = "₱ 0.00";
            // 
            // pnlSnapshotDivider
            // 
            pnlSnapshotDivider.BackColor = Color.Silver;
            pnlSnapshotDivider.Location = new Point(20, 75);
            pnlSnapshotDivider.Name = "pnlSnapshotDivider";
            pnlSnapshotDivider.Size = new Size(653, 2);
            pnlSnapshotDivider.TabIndex = 1;
            // 
            // lblSnapshotTitle
            // 
            lblSnapshotTitle.AutoSize = true;
            lblSnapshotTitle.Font = new Font("Segoe UI", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSnapshotTitle.Location = new Point(20, 20);
            lblSnapshotTitle.Name = "lblSnapshotTitle";
            lblSnapshotTitle.Size = new Size(310, 50);
            lblSnapshotTitle.TabIndex = 0;
            lblSnapshotTitle.Text = "Tenant Snapshot";
            // 
            // TenantsView
            // 
            BackColor = Color.White;
            Controls.Add(addTenantsModal);
            Controls.Add(tenantsSnapshotPanel);
            Controls.Add(paymentHistoryPanel);
            Controls.Add(detailsModal);
            Controls.Add(topBar);
            Controls.Add(dgvTenants);
            Margin = new Padding(0);
            Name = "TenantsView";
            Size = new Size(1677, 975);
            Load += TenantsView_Load_1;
            ((ISupportInitialize)dgvTenants).EndInit();
            topBar.ResumeLayout(false);
            topBar.PerformLayout();
            grpQuickActions.ResumeLayout(false);
            detailsModal.ResumeLayout(false);
            detailsModal.PerformLayout();
            grpDetails.ResumeLayout(false);
            grpDetails.PerformLayout();
            ((ISupportInitialize)detailsTenantImg).EndInit();
            addTenantsModal.ResumeLayout(false);
            addTenantsModal.PerformLayout();
            ((ISupportInitialize)addTenantImg).EndInit();
            paymentHistoryPanel.ResumeLayout(false);
            paymentHistoryPanel.PerformLayout();
            ((ISupportInitialize)dataGridView1).EndInit();
            tenantsSnapshotPanel.ResumeLayout(false);
            tenantsSnapshotPanel.PerformLayout();
            pnlSnapshotActions.ResumeLayout(false);
            pnlSnapshotActions.PerformLayout();
            pnlSnapshotCard.ResumeLayout(false);
            pnlSnapshotCard.PerformLayout();
            ResumeLayout(false);
        }
        private DataGridView dgvTenants;
        private Panel topBar;
        private Label lblStatus;
        private Label lblSearch;
        private Label lblBh;
        private Button btnSearch;
        private ComboBox cbStatusFilter;
        private TextBox txtSearch;
        private ComboBox cbBoardingHouses;
        private Button addTenantsBtn;
        private Label lblTenantTitle;
        private GroupBox grpQuickActions;
        private Button btnMarkInactive;
        private Button btnMarkMaintenance;
        private Button btnMarkOccupied;
        private Button btnMarkAvailable;
        private Button btnCloseDetails;
        private Panel detailsModal;
        private GroupBox grpDetails;
        private Label labelTenant;
        private TextBox detailsEmail;
        private Label labelRoomStatus;
        private TextBox detailsContact;
        private Label labelRate;
        private TextBox detailsMiddlename;
        private Label labelCap;
        private TextBox detailsFirstname;
        private Label labelType;
        private TextBox detailsLastname;
        private Label labelRoomNo;
        private TextBox detailsEContact;
        private Label label1;
        private TextBox detailsEName;
        private Label label2;
        private TextBox detailsAddress;
        private Label label3;
        private Label label6;
        private ComboBox detailsCbStatus;
        private Button tenantUpdateBtn;
        private Button tenantDeleteBtn;
        private ComboBox cbDetailsRoom;
        private Label label4;
        private Button btnViewCurrentRental;
        private Button btnStartRental;
        private Panel addTenantsModal;
        private Label totalTenants;
        private Button clearTenantFormBtn;
        private Button assignTenantBtn;
        private Button saveTenantBtn;
        private Button endStayBtn;
        private ListBox currentTenantsList;
        private Label label15;
        private Button addTenantCloseBtn;
        private TextBox tenantEmergencyContactTxt;
        private Label label11;
        private TextBox tenantEmergencyNameTxt;
        private Label label10;
        private TextBox tenantEmailTxt;
        private Label label8;
        private TextBox tenantContactTxt;
        private Label label7;
        private TextBox tenantMiddleNameTxt;
        private Label label5;
        private TextBox tenantFirstNameTxt;
        private Label label9;
        private TextBox tenantLastNameTxt;
        private Label label12;
        private TextBox tenantAddressTxt;
        private Label label16;
        private Button cancelTenantRegister;
        private Button registerTenantBtn;
        private Button endRentalBtn;
        private Button browseProfileBtn;
        private TextBox profilePathTxt;
        private PictureBox addTenantImg;
        private Button detailsBrowseProfileBtn;
        private TextBox details_profilePathTxt;
        private PictureBox detailsTenantImg;
        private Button registrationOpenCameraBtn;
        private Button detailsOpenCameraBtn;
        private Label ViewRoomBtn;
        private Panel paymentHistoryPanel;
        private Panel tenantsSnapshotPanel;
        private Label label13;
        private Label label17;
        private Label label14;
        private DataGridView dataGridView1;
        private Label lblSnapshotTitle;
        private Panel pnlSnapshotDivider;
        private Panel pnlSnapshotCard;
        private Label lblFieldTenant;
        private Label lblSnapshotTenant;
        private Label lblFieldTenantId;
        private Label lblSnapshotTenantId;
        private Label lblFieldBoardingHouse;
        private Label lblSnapshotBoardingHouse;
        private Label lblFieldRoomAssigned;
        private Label lblSnapshotRoomAssigned;
        private Label lblFieldRentalStart;
        private Label lblSnapshotRentalStart;
        private Label lblFieldDuration;
        private Label lblSnapshotDuration;
        private Label lblFieldLastPayment;
        private Label lblSnapshotLastPayment;
        private Label lblFieldLastAmount;
        private Label lblSnapshotLastAmount;
        private Label lblSnapshotStatusBadge;
        private Panel pnlSnapshotActions;
        private Label lblSnapshotHint;
        private Button btnSnapshotOpenPayments;
        private Button btnSnapshotViewRoom;
        private Button btnSnapshotRefresh;
    }
}
