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
            label14 = new Label();
            label15 = new Label();
            ((ISupportInitialize)dgvTenants).BeginInit();
            topBar.SuspendLayout();
            grpQuickActions.SuspendLayout();
            detailsModal.SuspendLayout();
            grpDetails.SuspendLayout();
            addTenantsModal.SuspendLayout();
            SuspendLayout();
            // 
            // dgvTenants
            // 
            dgvTenants.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTenants.Location = new Point(32, 116);
            dgvTenants.Name = "dgvTenants";
            dgvTenants.RowHeadersWidth = 51;
            dgvTenants.Size = new Size(1226, 743);
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
            topBar.Dock = DockStyle.Top;
            topBar.Location = new Point(0, 0);
            topBar.Name = "topBar";
            topBar.Padding = new Padding(18, 10, 18, 10);
            topBar.Size = new Size(1677, 92);
            topBar.TabIndex = 1;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(366, 17);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(49, 20);
            lblStatus.TabIndex = 4;
            lblStatus.Text = "Status";
            // 
            // lblBh
            // 
            lblBh.AutoSize = true;
            lblBh.Location = new Point(102, 17);
            lblBh.Name = "lblBh";
            lblBh.Size = new Size(116, 20);
            lblBh.TabIndex = 0;
            lblBh.Text = "Boarding House";
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(862, 40);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(85, 28);
            btnSearch.TabIndex = 6;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // cbStatusFilter
            // 
            cbStatusFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cbStatusFilter.FormattingEnabled = true;
            cbStatusFilter.Location = new Point(366, 40);
            cbStatusFilter.Name = "cbStatusFilter";
            cbStatusFilter.Size = new Size(220, 28);
            cbStatusFilter.TabIndex = 5;
            // 
            // txtSearch
            // 
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Location = new Point(606, 40);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "search";
            txtSearch.Size = new Size(250, 27);
            txtSearch.TabIndex = 3;
            // 
            // cbBoardingHouses
            // 
            cbBoardingHouses.DropDownStyle = ComboBoxStyle.DropDownList;
            cbBoardingHouses.FormattingEnabled = true;
            cbBoardingHouses.Location = new Point(102, 40);
            cbBoardingHouses.Name = "cbBoardingHouses";
            cbBoardingHouses.Size = new Size(240, 28);
            cbBoardingHouses.TabIndex = 1;
            cbBoardingHouses.SelectedIndexChanged += cbBoardingHouses_SelectedIndexChanged;
            // 
            // addTenantsBtn
            // 
            addTenantsBtn.Location = new Point(1154, 40);
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
            grpQuickActions.Location = new Point(28, 1565);
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
            detailsModal.Location = new Point(1264, 92);
            detailsModal.Name = "detailsModal";
            detailsModal.Padding = new Padding(16);
            detailsModal.Size = new Size(413, 883);
            detailsModal.TabIndex = 3;
            detailsModal.Visible = false;
            detailsModal.Paint += detailsModal_Paint_1;
            // 
            // grpDetails
            // 
            grpDetails.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
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
            grpDetails.Size = new Size(376, 756);
            grpDetails.TabIndex = 2;
            grpDetails.TabStop = false;
            grpDetails.Text = "Details";
            grpDetails.Enter += grpDetails_Enter;
            // 
            // cbDetailsRoom
            // 
            cbDetailsRoom.FormattingEnabled = true;
            cbDetailsRoom.Location = new Point(191, 578);
            cbDetailsRoom.Name = "cbDetailsRoom";
            cbDetailsRoom.Size = new Size(151, 28);
            cbDetailsRoom.TabIndex = 30;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = SystemColors.ButtonHighlight;
            label4.Location = new Point(191, 555);
            label4.Name = "label4";
            label4.Size = new Size(49, 20);
            label4.TabIndex = 29;
            label4.Text = "Room";
            label4.Click += label4_Click;
            // 
            // tenantUpdateBtn
            // 
            tenantUpdateBtn.BackColor = Color.FromArgb(128, 255, 128);
            tenantUpdateBtn.ForeColor = SystemColors.ActiveCaptionText;
            tenantUpdateBtn.Location = new Point(219, 673);
            tenantUpdateBtn.Name = "tenantUpdateBtn";
            tenantUpdateBtn.Size = new Size(104, 34);
            tenantUpdateBtn.TabIndex = 23;
            tenantUpdateBtn.Text = "Update";
            tenantUpdateBtn.UseVisualStyleBackColor = false;
            tenantUpdateBtn.Click += tenantUpdateBtn_Click_1;
            // 
            // tenantDeleteBtn
            // 
            tenantDeleteBtn.BackColor = Color.FromArgb(255, 192, 192);
            tenantDeleteBtn.ForeColor = SystemColors.ActiveCaptionText;
            tenantDeleteBtn.Location = new Point(56, 673);
            tenantDeleteBtn.Name = "tenantDeleteBtn";
            tenantDeleteBtn.Size = new Size(104, 34);
            tenantDeleteBtn.TabIndex = 22;
            tenantDeleteBtn.Text = "Delete";
            tenantDeleteBtn.UseVisualStyleBackColor = false;
            // 
            // detailsCbStatus
            // 
            detailsCbStatus.FormattingEnabled = true;
            detailsCbStatus.Location = new Point(24, 578);
            detailsCbStatus.Name = "detailsCbStatus";
            detailsCbStatus.Size = new Size(151, 28);
            detailsCbStatus.TabIndex = 28;
            detailsCbStatus.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = SystemColors.ButtonHighlight;
            label6.Location = new Point(24, 555);
            label6.Name = "label6";
            label6.Size = new Size(49, 20);
            label6.TabIndex = 27;
            label6.Text = "Status";
            // 
            // detailsEContact
            // 
            detailsEContact.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            detailsEContact.Location = new Point(24, 512);
            detailsEContact.Name = "detailsEContact";
            detailsEContact.ReadOnly = true;
            detailsEContact.Size = new Size(318, 27);
            detailsEContact.TabIndex = 26;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(24, 489);
            label1.Name = "label1";
            label1.Size = new Size(161, 20);
            label1.TabIndex = 25;
            label1.Text = "Emergency Contact No";
            // 
            // detailsEName
            // 
            detailsEName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            detailsEName.Location = new Point(24, 445);
            detailsEName.Name = "detailsEName";
            detailsEName.Size = new Size(318, 27);
            detailsEName.TabIndex = 24;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(24, 422);
            label2.Name = "label2";
            label2.Size = new Size(181, 20);
            label2.TabIndex = 23;
            label2.Text = "Emergency Contact Name";
            // 
            // detailsAddress
            // 
            detailsAddress.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            detailsAddress.Location = new Point(24, 381);
            detailsAddress.Name = "detailsAddress";
            detailsAddress.Size = new Size(318, 27);
            detailsAddress.TabIndex = 22;
            detailsAddress.TextChanged += textBox3_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = SystemColors.ButtonHighlight;
            label3.Location = new Point(24, 358);
            label3.Name = "label3";
            label3.Size = new Size(62, 20);
            label3.TabIndex = 21;
            label3.Text = "Address";
            // 
            // detailsEmail
            // 
            detailsEmail.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            detailsEmail.Location = new Point(24, 316);
            detailsEmail.Name = "detailsEmail";
            detailsEmail.ReadOnly = true;
            detailsEmail.Size = new Size(318, 27);
            detailsEmail.TabIndex = 20;
            // 
            // labelRoomStatus
            // 
            labelRoomStatus.AutoSize = true;
            labelRoomStatus.ForeColor = SystemColors.ButtonHighlight;
            labelRoomStatus.Location = new Point(24, 293);
            labelRoomStatus.Name = "labelRoomStatus";
            labelRoomStatus.Size = new Size(46, 20);
            labelRoomStatus.TabIndex = 19;
            labelRoomStatus.Text = "Email";
            // 
            // detailsContact
            // 
            detailsContact.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            detailsContact.Location = new Point(24, 249);
            detailsContact.Name = "detailsContact";
            detailsContact.Size = new Size(318, 27);
            detailsContact.TabIndex = 18;
            detailsContact.TextChanged += detailsRate_TextChanged;
            // 
            // labelRate
            // 
            labelRate.AutoSize = true;
            labelRate.ForeColor = SystemColors.ButtonHighlight;
            labelRate.Location = new Point(24, 226);
            labelRate.Name = "labelRate";
            labelRate.Size = new Size(60, 20);
            labelRate.TabIndex = 17;
            labelRate.Text = "Contact";
            // 
            // detailsMiddlename
            // 
            detailsMiddlename.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            detailsMiddlename.Location = new Point(24, 185);
            detailsMiddlename.Name = "detailsMiddlename";
            detailsMiddlename.Size = new Size(318, 27);
            detailsMiddlename.TabIndex = 16;
            // 
            // labelCap
            // 
            labelCap.AutoSize = true;
            labelCap.ForeColor = SystemColors.ButtonHighlight;
            labelCap.Location = new Point(24, 162);
            labelCap.Name = "labelCap";
            labelCap.Size = new Size(93, 20);
            labelCap.TabIndex = 15;
            labelCap.Text = "Middlename";
            // 
            // detailsFirstname
            // 
            detailsFirstname.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            detailsFirstname.Location = new Point(24, 121);
            detailsFirstname.Name = "detailsFirstname";
            detailsFirstname.Size = new Size(318, 27);
            detailsFirstname.TabIndex = 14;
            // 
            // labelType
            // 
            labelType.AutoSize = true;
            labelType.ForeColor = SystemColors.ButtonHighlight;
            labelType.Location = new Point(24, 98);
            labelType.Name = "labelType";
            labelType.Size = new Size(73, 20);
            labelType.TabIndex = 13;
            labelType.Text = "Firstname";
            // 
            // detailsLastname
            // 
            detailsLastname.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            detailsLastname.Location = new Point(24, 59);
            detailsLastname.Name = "detailsLastname";
            detailsLastname.Size = new Size(318, 27);
            detailsLastname.TabIndex = 12;
            detailsLastname.TextChanged += detailsRoomNo_TextChanged;
            // 
            // labelRoomNo
            // 
            labelRoomNo.AutoSize = true;
            labelRoomNo.ForeColor = SystemColors.ButtonHighlight;
            labelRoomNo.Location = new Point(24, 36);
            labelRoomNo.Name = "labelRoomNo";
            labelRoomNo.Size = new Size(72, 20);
            labelRoomNo.TabIndex = 11;
            labelRoomNo.Text = "Lastname";
            labelRoomNo.Click += labelRoomNo_Click;
            // 
            // addTenantsModal
            // 
            addTenantsModal.Anchor = AnchorStyles.None;
            addTenantsModal.BackColor = Color.WhiteSmoke;
            addTenantsModal.BorderStyle = BorderStyle.FixedSingle;
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
            addTenantsModal.Controls.Add(label14);
            addTenantsModal.Controls.Add(label15);
            addTenantsModal.Location = new Point(400, 92);
            addTenantsModal.Name = "addTenantsModal";
            addTenantsModal.Size = new Size(718, 599);
            addTenantsModal.TabIndex = 4;
            addTenantsModal.Visible = false;
            addTenantsModal.Paint += addTenantsModal_Paint;
            // 
            // cancelTenantRegister
            // 
            cancelTenantRegister.BackColor = Color.WhiteSmoke;
            cancelTenantRegister.ForeColor = SystemColors.ActiveCaptionText;
            cancelTenantRegister.Location = new Point(143, 504);
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
            registerTenantBtn.Location = new Point(419, 504);
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
            tenantAddressTxt.Location = new Point(369, 157);
            tenantAddressTxt.Multiline = true;
            tenantAddressTxt.Name = "tenantAddressTxt";
            tenantAddressTxt.Size = new Size(294, 40);
            tenantAddressTxt.TabIndex = 60;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.ForeColor = SystemColors.ActiveCaptionText;
            label16.Location = new Point(369, 139);
            label16.Name = "label16";
            label16.Size = new Size(69, 20);
            label16.TabIndex = 59;
            label16.Text = "Address :";
            // 
            // tenantEmergencyContactTxt
            // 
            tenantEmergencyContactTxt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tenantEmergencyContactTxt.Location = new Point(369, 347);
            tenantEmergencyContactTxt.Multiline = true;
            tenantEmergencyContactTxt.Name = "tenantEmergencyContactTxt";
            tenantEmergencyContactTxt.Size = new Size(294, 40);
            tenantEmergencyContactTxt.TabIndex = 56;
            tenantEmergencyContactTxt.TextChanged += tenantEmergencyContactTxt_TextChanged;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.ForeColor = SystemColors.ActiveCaptionText;
            label11.Location = new Point(369, 324);
            label11.Name = "label11";
            label11.Size = new Size(144, 20);
            label11.TabIndex = 55;
            label11.Text = "Emergency Contact :";
            // 
            // tenantEmergencyNameTxt
            // 
            tenantEmergencyNameTxt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tenantEmergencyNameTxt.Location = new Point(369, 281);
            tenantEmergencyNameTxt.Multiline = true;
            tenantEmergencyNameTxt.Name = "tenantEmergencyNameTxt";
            tenantEmergencyNameTxt.Size = new Size(294, 40);
            tenantEmergencyNameTxt.TabIndex = 54;
            tenantEmergencyNameTxt.TextChanged += tenantEmergencyNameTxt_TextChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.ForeColor = SystemColors.ActiveCaptionText;
            label10.Location = new Point(371, 258);
            label10.Name = "label10";
            label10.Size = new Size(129, 20);
            label10.TabIndex = 53;
            label10.Text = "Emergency Name:";
            // 
            // tenantEmailTxt
            // 
            tenantEmailTxt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tenantEmailTxt.Location = new Point(41, 347);
            tenantEmailTxt.Multiline = true;
            tenantEmailTxt.Name = "tenantEmailTxt";
            tenantEmailTxt.Size = new Size(294, 40);
            tenantEmailTxt.TabIndex = 52;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.ForeColor = SystemColors.ActiveCaptionText;
            label8.Location = new Point(41, 329);
            label8.Name = "label8";
            label8.Size = new Size(49, 20);
            label8.TabIndex = 51;
            label8.Text = "Email:";
            // 
            // tenantContactTxt
            // 
            tenantContactTxt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tenantContactTxt.Location = new Point(369, 216);
            tenantContactTxt.Multiline = true;
            tenantContactTxt.Name = "tenantContactTxt";
            tenantContactTxt.Size = new Size(294, 40);
            tenantContactTxt.TabIndex = 50;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = SystemColors.ActiveCaptionText;
            label7.Location = new Point(369, 198);
            label7.Name = "label7";
            label7.Size = new Size(67, 20);
            label7.TabIndex = 49;
            label7.Text = "Contact :";
            // 
            // tenantMiddleNameTxt
            // 
            tenantMiddleNameTxt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tenantMiddleNameTxt.Location = new Point(41, 282);
            tenantMiddleNameTxt.Multiline = true;
            tenantMiddleNameTxt.Name = "tenantMiddleNameTxt";
            tenantMiddleNameTxt.Size = new Size(294, 40);
            tenantMiddleNameTxt.TabIndex = 48;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = SystemColors.ActiveCaptionText;
            label5.Location = new Point(41, 264);
            label5.Name = "label5";
            label5.Size = new Size(100, 20);
            label5.TabIndex = 47;
            label5.Text = "Middle name:";
            // 
            // tenantFirstNameTxt
            // 
            tenantFirstNameTxt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tenantFirstNameTxt.Location = new Point(41, 218);
            tenantFirstNameTxt.Multiline = true;
            tenantFirstNameTxt.Name = "tenantFirstNameTxt";
            tenantFirstNameTxt.Size = new Size(294, 40);
            tenantFirstNameTxt.TabIndex = 46;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.ForeColor = SystemColors.ActiveCaptionText;
            label9.Location = new Point(41, 200);
            label9.Name = "label9";
            label9.Size = new Size(76, 20);
            label9.TabIndex = 45;
            label9.Text = "Firstname:";
            // 
            // tenantLastNameTxt
            // 
            tenantLastNameTxt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tenantLastNameTxt.Location = new Point(41, 154);
            tenantLastNameTxt.Multiline = true;
            tenantLastNameTxt.Name = "tenantLastNameTxt";
            tenantLastNameTxt.Size = new Size(294, 40);
            tenantLastNameTxt.TabIndex = 44;
            tenantLastNameTxt.TextChanged += tenantLastNameTxt_TextChanged;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.ForeColor = SystemColors.ActiveCaptionText;
            label12.Location = new Point(43, 131);
            label12.Name = "label12";
            label12.Size = new Size(75, 20);
            label12.TabIndex = 43;
            label12.Text = "Lastname:";
            // 
            // addTenantCloseBtn
            // 
            addTenantCloseBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            addTenantCloseBtn.BackColor = Color.FromArgb(255, 128, 128);
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
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label14.Location = new Point(66, 86);
            label14.Name = "label14";
            label14.Size = new Size(235, 31);
            label14.TabIndex = 7;
            label14.Text = "Enter Tenant Detaills";
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
            // TenantsView
            // 
            BackColor = Color.White;
            Controls.Add(addTenantsModal);
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
            addTenantsModal.ResumeLayout(false);
            addTenantsModal.PerformLayout();
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
        private Panel addTenantsModal;
        private Label totalTenants;
        private Button clearTenantFormBtn;
        private Button assignTenantBtn;
        private Button saveTenantBtn;
        private Button endStayBtn;
        private ListBox currentTenantsList;
        private Label label14;
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
    }
}
