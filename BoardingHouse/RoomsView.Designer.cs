namespace BoardingHouse
{
    partial class RoomsView
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            topBar = new Panel();
            lblStatus = new Label();
            lblSearch = new Label();
            lblBh = new Label();
            btnRefresh = new Button();
            btnSearch = new Button();
            cbStatusFilter = new ComboBox();
            txtSearch = new TextBox();
            cbBoardingHouses = new ComboBox();
            addRoomBtn = new Button();
            flpRooms = new FlowLayoutPanel();
            button1 = new Button();
            detailsModal = new Panel();
            btnCloseDetails = new Button();
            grpQuickActions = new GroupBox();
            btnMarkInactive = new Button();
            btnMarkMaintenance = new Button();
            btnMarkOccupied = new Button();
            btnMarkAvailable = new Button();
            grpDetails = new GroupBox();
            button3 = new Button();
            button2 = new Button();
            lblOccupancy = new Label();
            detailsTenantsList = new ListBox();
            detailsNotes = new TextBox();
            labelNotes = new Label();
            labelTenant = new Label();
            detailsStatus = new TextBox();
            labelRoomStatus = new Label();
            detailsRate = new TextBox();
            labelRate = new Label();
            detailsCapacity = new TextBox();
            labelCap = new Label();
            detailsRoomType = new TextBox();
            labelType = new Label();
            detailsRoomNo = new TextBox();
            labelRoomNo = new Label();
            lblRoomTitle = new Label();
            addRoomModal = new Panel();
            addRoomTitle = new Label();
            addRoomCloseBtn = new Button();
            labelAddRoomBh = new Label();
            addRoomBhCb = new ComboBox();
            labelAddRoomNo = new Label();
            addRoomRoomNoTxt = new TextBox();
            labelAddRoomType = new Label();
            addRoomTypeTxt = new TextBox();
            labelAddRoomCap = new Label();
            addRoomCapNum = new NumericUpDown();
            labelAddRoomRate = new Label();
            addRoomRateNum = new NumericUpDown();
            labelAddRoomStatus = new Label();
            addRoomStatusCb = new ComboBox();
            labelAddRoomNotes = new Label();
            addRoomNotesTxt = new TextBox();
            addRoomSaveBtn = new Button();
            addRoomCancelBtn = new Button();
            topBar.SuspendLayout();
            flpRooms.SuspendLayout();
            detailsModal.SuspendLayout();
            grpQuickActions.SuspendLayout();
            grpDetails.SuspendLayout();
            addRoomModal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)addRoomCapNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)addRoomRateNum).BeginInit();
            SuspendLayout();
            // 
            // topBar
            // 
            topBar.BackColor = Color.WhiteSmoke;
            topBar.Controls.Add(lblStatus);
            topBar.Controls.Add(lblSearch);
            topBar.Controls.Add(lblBh);
            topBar.Controls.Add(btnRefresh);
            topBar.Controls.Add(btnSearch);
            topBar.Controls.Add(cbStatusFilter);
            topBar.Controls.Add(txtSearch);
            topBar.Controls.Add(cbBoardingHouses);
            topBar.Controls.Add(addRoomBtn);
            topBar.Dock = DockStyle.Top;
            topBar.Location = new Point(0, 0);
            topBar.Name = "topBar";
            topBar.Padding = new Padding(18, 10, 18, 10);
            topBar.Size = new Size(1317, 92);
            topBar.TabIndex = 0;
            topBar.Paint += topBar_Paint;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(366, 16);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(49, 20);
            lblStatus.TabIndex = 4;
            lblStatus.Text = "Status";
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Location = new Point(606, 16);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(53, 20);
            lblSearch.TabIndex = 2;
            lblSearch.Text = "Search";
            // 
            // lblBh
            // 
            lblBh.AutoSize = true;
            lblBh.Location = new Point(102, 16);
            lblBh.Name = "lblBh";
            lblBh.Size = new Size(116, 20);
            lblBh.TabIndex = 0;
            lblBh.Text = "Boarding House";
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(992, 40);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(104, 34);
            btnRefresh.TabIndex = 7;
            btnRefresh.Text = "Reset";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(876, 40);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(104, 34);
            btnSearch.TabIndex = 6;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // cbStatusFilter
            // 
            cbStatusFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cbStatusFilter.FormattingEnabled = true;
            cbStatusFilter.Location = new Point(366, 40);
            cbStatusFilter.Name = "cbStatusFilter";
            cbStatusFilter.Size = new Size(220, 28);
            cbStatusFilter.TabIndex = 5;
            cbStatusFilter.SelectedIndexChanged += cbStatusFilter_SelectedIndexChanged;
            // 
            // txtSearch
            // 
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Location = new Point(606, 40);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "room no / type / tenant";
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
            // addRoomBtn
            // 
            addRoomBtn.Location = new Point(1108, 40);
            addRoomBtn.Name = "addRoomBtn";
            addRoomBtn.Size = new Size(104, 34);
            addRoomBtn.TabIndex = 8;
            addRoomBtn.Text = "Add Room";
            addRoomBtn.UseVisualStyleBackColor = true;
            addRoomBtn.Click += addRoomBtn_Click;
            // 
            // flpRooms
            // 
            flpRooms.AutoScroll = true;
            flpRooms.Controls.Add(button1);
            flpRooms.Location = new Point(41, 127);
            flpRooms.Name = "flpRooms";
            flpRooms.Padding = new Padding(16);
            flpRooms.Size = new Size(1232, 730);
            flpRooms.TabIndex = 1;
            flpRooms.Paint += flpRooms_Paint_1;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(128, 255, 128);
            button1.ForeColor = SystemColors.ActiveCaptionText;
            button1.Location = new Point(19, 19);
            button1.Name = "button1";
            button1.Size = new Size(138, 34);
            button1.TabIndex = 1;
            button1.Text = "AVAILABLE";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click_1;
            // 
            // detailsModal
            // 
            detailsModal.BackColor = Color.FromArgb(48, 54, 92);
            detailsModal.BorderStyle = BorderStyle.FixedSingle;
            detailsModal.Controls.Add(btnCloseDetails);
            detailsModal.Controls.Add(grpQuickActions);
            detailsModal.Controls.Add(grpDetails);
            detailsModal.Controls.Add(lblRoomTitle);
            detailsModal.Dock = DockStyle.Right;
            detailsModal.Location = new Point(1317, 0);
            detailsModal.Name = "detailsModal";
            detailsModal.Padding = new Padding(16);
            detailsModal.Size = new Size(360, 975);
            detailsModal.TabIndex = 2;
            detailsModal.Visible = false;
            detailsModal.Paint += detailsModal_Paint;
            // 
            // btnCloseDetails
            // 
            btnCloseDetails.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCloseDetails.BackColor = Color.FromArgb(255, 128, 128);
            btnCloseDetails.Location = new Point(294, 11);
            btnCloseDetails.Name = "btnCloseDetails";
            btnCloseDetails.Size = new Size(45, 28);
            btnCloseDetails.TabIndex = 4;
            btnCloseDetails.Text = "X";
            btnCloseDetails.UseVisualStyleBackColor = false;
            btnCloseDetails.Click += button1_Click;
            // 
            // grpQuickActions
            // 
            grpQuickActions.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpQuickActions.Controls.Add(btnMarkInactive);
            grpQuickActions.Controls.Add(btnMarkMaintenance);
            grpQuickActions.Controls.Add(btnMarkOccupied);
            grpQuickActions.Controls.Add(btnMarkAvailable);
            grpQuickActions.ForeColor = SystemColors.ButtonHighlight;
            grpQuickActions.Location = new Point(12, 800);
            grpQuickActions.Name = "grpQuickActions";
            grpQuickActions.Padding = new Padding(12);
            grpQuickActions.Size = new Size(317, 154);
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
            btnMarkInactive.Click += btnMarkInactive_Click;
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
            btnMarkMaintenance.Click += btnMarkMaintenance_Click;
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
            btnMarkOccupied.Click += btnMarkOccupied_Click;
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
            btnMarkAvailable.Click += btnMarkAvailable_Click;
            // 
            // grpDetails
            // 
            grpDetails.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpDetails.Controls.Add(button3);
            grpDetails.Controls.Add(button2);
            grpDetails.Controls.Add(lblOccupancy);
            grpDetails.Controls.Add(detailsTenantsList);
            grpDetails.Controls.Add(detailsNotes);
            grpDetails.Controls.Add(labelNotes);
            grpDetails.Controls.Add(labelTenant);
            grpDetails.Controls.Add(detailsStatus);
            grpDetails.Controls.Add(labelRoomStatus);
            grpDetails.Controls.Add(detailsRate);
            grpDetails.Controls.Add(labelRate);
            grpDetails.Controls.Add(detailsCapacity);
            grpDetails.Controls.Add(labelCap);
            grpDetails.Controls.Add(detailsRoomType);
            grpDetails.Controls.Add(labelType);
            grpDetails.Controls.Add(detailsRoomNo);
            grpDetails.Controls.Add(labelRoomNo);
            grpDetails.ForeColor = SystemColors.ButtonHighlight;
            grpDetails.Location = new Point(12, 48);
            grpDetails.Name = "grpDetails";
            grpDetails.Padding = new Padding(12);
            grpDetails.Size = new Size(324, 756);
            grpDetails.TabIndex = 2;
            grpDetails.TabStop = false;
            grpDetails.Text = "Details";
            grpDetails.Enter += grpDetails_Enter;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(128, 255, 128);
            button3.ForeColor = SystemColors.ActiveCaptionText;
            button3.Location = new Point(188, 640);
            button3.Name = "button3";
            button3.Size = new Size(104, 34);
            button3.TabIndex = 21;
            button3.Text = "Update";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(255, 192, 192);
            button2.ForeColor = SystemColors.ActiveCaptionText;
            button2.Location = new Point(25, 640);
            button2.Name = "button2";
            button2.Size = new Size(104, 34);
            button2.TabIndex = 20;
            button2.Text = "Delete";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click_2;
            // 
            // lblOccupancy
            // 
            lblOccupancy.AutoSize = true;
            lblOccupancy.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblOccupancy.Location = new Point(12, 598);
            lblOccupancy.Name = "lblOccupancy";
            lblOccupancy.Size = new Size(172, 28);
            lblOccupancy.TabIndex = 17;
            lblOccupancy.Text = "Occupancys: 0 / 0";
            lblOccupancy.Click += lblOccupancy_Click;
            // 
            // detailsTenantsList
            // 
            detailsTenantsList.Location = new Point(15, 338);
            detailsTenantsList.Name = "detailsTenantsList";
            detailsTenantsList.Size = new Size(297, 84);
            detailsTenantsList.TabIndex = 19;
            // 
            // detailsNotes
            // 
            detailsNotes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            detailsNotes.Location = new Point(17, 466);
            detailsNotes.Multiline = true;
            detailsNotes.Name = "detailsNotes";
            detailsNotes.ScrollBars = ScrollBars.Vertical;
            detailsNotes.Size = new Size(300, 80);
            detailsNotes.TabIndex = 13;
            // 
            // labelNotes
            // 
            labelNotes.AutoSize = true;
            labelNotes.ForeColor = SystemColors.ButtonHighlight;
            labelNotes.Location = new Point(17, 448);
            labelNotes.Name = "labelNotes";
            labelNotes.Size = new Size(48, 20);
            labelNotes.TabIndex = 12;
            labelNotes.Text = "Notes";
            // 
            // labelTenant
            // 
            labelTenant.AutoSize = true;
            labelTenant.ForeColor = SystemColors.ButtonHighlight;
            labelTenant.Location = new Point(12, 315);
            labelTenant.Name = "labelTenant";
            labelTenant.Size = new Size(53, 20);
            labelTenant.TabIndex = 10;
            labelTenant.Text = "Tenant";
            // 
            // detailsStatus
            // 
            detailsStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            detailsStatus.Location = new Point(15, 282);
            detailsStatus.Name = "detailsStatus";
            detailsStatus.ReadOnly = true;
            detailsStatus.Size = new Size(300, 27);
            detailsStatus.TabIndex = 9;
            // 
            // labelRoomStatus
            // 
            labelRoomStatus.AutoSize = true;
            labelRoomStatus.ForeColor = SystemColors.ButtonHighlight;
            labelRoomStatus.Location = new Point(15, 264);
            labelRoomStatus.Name = "labelRoomStatus";
            labelRoomStatus.Size = new Size(49, 20);
            labelRoomStatus.TabIndex = 8;
            labelRoomStatus.Text = "Status";
            // 
            // detailsRate
            // 
            detailsRate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            detailsRate.Location = new Point(15, 220);
            detailsRate.Name = "detailsRate";
            detailsRate.Size = new Size(300, 27);
            detailsRate.TabIndex = 7;
            // 
            // labelRate
            // 
            labelRate.AutoSize = true;
            labelRate.ForeColor = SystemColors.ButtonHighlight;
            labelRate.Location = new Point(15, 202);
            labelRate.Name = "labelRate";
            labelRate.Size = new Size(97, 20);
            labelRate.TabIndex = 6;
            labelRate.Text = "Monthly Rate";
            // 
            // detailsCapacity
            // 
            detailsCapacity.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            detailsCapacity.Location = new Point(15, 161);
            detailsCapacity.Name = "detailsCapacity";
            detailsCapacity.Size = new Size(300, 27);
            detailsCapacity.TabIndex = 5;
            // 
            // labelCap
            // 
            labelCap.AutoSize = true;
            labelCap.ForeColor = SystemColors.ButtonHighlight;
            labelCap.Location = new Point(15, 143);
            labelCap.Name = "labelCap";
            labelCap.Size = new Size(66, 20);
            labelCap.TabIndex = 4;
            labelCap.Text = "Capacity";
            // 
            // detailsRoomType
            // 
            detailsRoomType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            detailsRoomType.Location = new Point(15, 102);
            detailsRoomType.Name = "detailsRoomType";
            detailsRoomType.Size = new Size(300, 27);
            detailsRoomType.TabIndex = 3;
            // 
            // labelType
            // 
            labelType.AutoSize = true;
            labelType.ForeColor = SystemColors.ButtonHighlight;
            labelType.Location = new Point(15, 84);
            labelType.Name = "labelType";
            labelType.Size = new Size(40, 20);
            labelType.TabIndex = 2;
            labelType.Text = "Type";
            // 
            // detailsRoomNo
            // 
            detailsRoomNo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            detailsRoomNo.Location = new Point(12, 49);
            detailsRoomNo.Name = "detailsRoomNo";
            detailsRoomNo.Size = new Size(300, 27);
            detailsRoomNo.TabIndex = 1;
            // 
            // labelRoomNo
            // 
            labelRoomNo.AutoSize = true;
            labelRoomNo.ForeColor = SystemColors.ButtonHighlight;
            labelRoomNo.Location = new Point(12, 31);
            labelRoomNo.Name = "labelRoomNo";
            labelRoomNo.Size = new Size(73, 20);
            labelRoomNo.TabIndex = 0;
            labelRoomNo.Text = "Room No";
            // 
            // lblRoomTitle
            // 
            lblRoomTitle.AutoSize = true;
            lblRoomTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblRoomTitle.ForeColor = SystemColors.ButtonHighlight;
            lblRoomTitle.Location = new Point(12, 12);
            lblRoomTitle.Name = "lblRoomTitle";
            lblRoomTitle.Size = new Size(129, 25);
            lblRoomTitle.TabIndex = 1;
            lblRoomTitle.Text = "Room Details";
            // 
            // addRoomModal
            // 
            addRoomModal.Anchor = AnchorStyles.None;
            addRoomModal.BackColor = Color.WhiteSmoke;
            addRoomModal.BorderStyle = BorderStyle.FixedSingle;
            addRoomModal.Controls.Add(addRoomTitle);
            addRoomModal.Controls.Add(addRoomCloseBtn);
            addRoomModal.Controls.Add(labelAddRoomBh);
            addRoomModal.Controls.Add(addRoomBhCb);
            addRoomModal.Controls.Add(labelAddRoomNo);
            addRoomModal.Controls.Add(addRoomRoomNoTxt);
            addRoomModal.Controls.Add(labelAddRoomType);
            addRoomModal.Controls.Add(addRoomTypeTxt);
            addRoomModal.Controls.Add(labelAddRoomCap);
            addRoomModal.Controls.Add(addRoomCapNum);
            addRoomModal.Controls.Add(labelAddRoomRate);
            addRoomModal.Controls.Add(addRoomRateNum);
            addRoomModal.Controls.Add(labelAddRoomStatus);
            addRoomModal.Controls.Add(addRoomStatusCb);
            addRoomModal.Controls.Add(labelAddRoomNotes);
            addRoomModal.Controls.Add(addRoomNotesTxt);
            addRoomModal.Controls.Add(addRoomSaveBtn);
            addRoomModal.Controls.Add(addRoomCancelBtn);
            addRoomModal.Location = new Point(514, 98);
            addRoomModal.Name = "addRoomModal";
            addRoomModal.Size = new Size(480, 560);
            addRoomModal.TabIndex = 5;
            addRoomModal.Visible = false;
            addRoomModal.Paint += addRoomModal_Paint;
            // 
            // addRoomTitle
            // 
            addRoomTitle.AutoSize = true;
            addRoomTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            addRoomTitle.Location = new Point(16, 16);
            addRoomTitle.Name = "addRoomTitle";
            addRoomTitle.Size = new Size(111, 28);
            addRoomTitle.TabIndex = 0;
            addRoomTitle.Text = "Add Room";
            // 
            // addRoomCloseBtn
            // 
            addRoomCloseBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            addRoomCloseBtn.Location = new Point(714, 16);
            addRoomCloseBtn.Name = "addRoomCloseBtn";
            addRoomCloseBtn.Size = new Size(28, 28);
            addRoomCloseBtn.TabIndex = 1;
            addRoomCloseBtn.Text = "X";
            addRoomCloseBtn.UseVisualStyleBackColor = true;
            addRoomCloseBtn.Click += addRoomCloseBtn_Click;
            // 
            // labelAddRoomBh
            // 
            labelAddRoomBh.AutoSize = true;
            labelAddRoomBh.Location = new Point(16, 56);
            labelAddRoomBh.Name = "labelAddRoomBh";
            labelAddRoomBh.Size = new Size(116, 20);
            labelAddRoomBh.TabIndex = 2;
            labelAddRoomBh.Text = "Boarding House";
            // 
            // addRoomBhCb
            // 
            addRoomBhCb.DropDownStyle = ComboBoxStyle.DropDownList;
            addRoomBhCb.FormattingEnabled = true;
            addRoomBhCb.Location = new Point(16, 78);
            addRoomBhCb.Name = "addRoomBhCb";
            addRoomBhCb.Size = new Size(448, 28);
            addRoomBhCb.TabIndex = 3;
            // 
            // labelAddRoomNo
            // 
            labelAddRoomNo.AutoSize = true;
            labelAddRoomNo.Location = new Point(16, 116);
            labelAddRoomNo.Name = "labelAddRoomNo";
            labelAddRoomNo.Size = new Size(73, 20);
            labelAddRoomNo.TabIndex = 4;
            labelAddRoomNo.Text = "Room No";
            // 
            // addRoomRoomNoTxt
            // 
            addRoomRoomNoTxt.Location = new Point(16, 138);
            addRoomRoomNoTxt.Name = "addRoomRoomNoTxt";
            addRoomRoomNoTxt.Size = new Size(448, 27);
            addRoomRoomNoTxt.TabIndex = 5;
            // 
            // labelAddRoomType
            // 
            labelAddRoomType.AutoSize = true;
            labelAddRoomType.Location = new Point(16, 176);
            labelAddRoomType.Name = "labelAddRoomType";
            labelAddRoomType.Size = new Size(40, 20);
            labelAddRoomType.TabIndex = 6;
            labelAddRoomType.Text = "Type";
            // 
            // addRoomTypeTxt
            // 
            addRoomTypeTxt.Location = new Point(16, 198);
            addRoomTypeTxt.Name = "addRoomTypeTxt";
            addRoomTypeTxt.Size = new Size(448, 27);
            addRoomTypeTxt.TabIndex = 7;
            // 
            // labelAddRoomCap
            // 
            labelAddRoomCap.AutoSize = true;
            labelAddRoomCap.Location = new Point(16, 236);
            labelAddRoomCap.Name = "labelAddRoomCap";
            labelAddRoomCap.Size = new Size(66, 20);
            labelAddRoomCap.TabIndex = 8;
            labelAddRoomCap.Text = "Capacity";
            // 
            // addRoomCapNum
            // 
            addRoomCapNum.Location = new Point(16, 258);
            addRoomCapNum.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            addRoomCapNum.Name = "addRoomCapNum";
            addRoomCapNum.Size = new Size(120, 27);
            addRoomCapNum.TabIndex = 9;
            addRoomCapNum.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // labelAddRoomRate
            // 
            labelAddRoomRate.AutoSize = true;
            labelAddRoomRate.Location = new Point(16, 296);
            labelAddRoomRate.Name = "labelAddRoomRate";
            labelAddRoomRate.Size = new Size(97, 20);
            labelAddRoomRate.TabIndex = 10;
            labelAddRoomRate.Text = "Monthly Rate";
            // 
            // addRoomRateNum
            // 
            addRoomRateNum.DecimalPlaces = 2;
            addRoomRateNum.Increment = new decimal(new int[] { 25, 0, 0, 131072 });
            addRoomRateNum.Location = new Point(16, 318);
            addRoomRateNum.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            addRoomRateNum.Name = "addRoomRateNum";
            addRoomRateNum.Size = new Size(140, 27);
            addRoomRateNum.TabIndex = 11;
            // 
            // labelAddRoomStatus
            // 
            labelAddRoomStatus.AutoSize = true;
            labelAddRoomStatus.Location = new Point(16, 356);
            labelAddRoomStatus.Name = "labelAddRoomStatus";
            labelAddRoomStatus.Size = new Size(49, 20);
            labelAddRoomStatus.TabIndex = 12;
            labelAddRoomStatus.Text = "Status";
            // 
            // addRoomStatusCb
            // 
            addRoomStatusCb.DropDownStyle = ComboBoxStyle.DropDownList;
            addRoomStatusCb.FormattingEnabled = true;
            addRoomStatusCb.Items.AddRange(new object[] { "AVAILABLE", "OCCUPIED", "MAINTENANCE", "INACTIVE" });
            addRoomStatusCb.Location = new Point(16, 378);
            addRoomStatusCb.Name = "addRoomStatusCb";
            addRoomStatusCb.Size = new Size(200, 28);
            addRoomStatusCb.TabIndex = 13;
            // 
            // labelAddRoomNotes
            // 
            labelAddRoomNotes.AutoSize = true;
            labelAddRoomNotes.Location = new Point(16, 416);
            labelAddRoomNotes.Name = "labelAddRoomNotes";
            labelAddRoomNotes.Size = new Size(48, 20);
            labelAddRoomNotes.TabIndex = 14;
            labelAddRoomNotes.Text = "Notes";
            // 
            // addRoomNotesTxt
            // 
            addRoomNotesTxt.Location = new Point(16, 438);
            addRoomNotesTxt.Multiline = true;
            addRoomNotesTxt.Name = "addRoomNotesTxt";
            addRoomNotesTxt.ScrollBars = ScrollBars.Vertical;
            addRoomNotesTxt.Size = new Size(448, 70);
            addRoomNotesTxt.TabIndex = 15;
            // 
            // addRoomSaveBtn
            // 
            addRoomSaveBtn.Location = new Point(354, 520);
            addRoomSaveBtn.Name = "addRoomSaveBtn";
            addRoomSaveBtn.Size = new Size(110, 32);
            addRoomSaveBtn.TabIndex = 16;
            addRoomSaveBtn.Text = "Save";
            addRoomSaveBtn.UseVisualStyleBackColor = true;
            addRoomSaveBtn.Click += addRoomSaveBtn_Click;
            // 
            // addRoomCancelBtn
            // 
            addRoomCancelBtn.Location = new Point(16, 520);
            addRoomCancelBtn.Name = "addRoomCancelBtn";
            addRoomCancelBtn.Size = new Size(110, 32);
            addRoomCancelBtn.TabIndex = 17;
            addRoomCancelBtn.Text = "Cancel";
            addRoomCancelBtn.UseVisualStyleBackColor = true;
            addRoomCancelBtn.Click += addRoomCancelBtn_Click;
            // 
            // RoomsView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            Controls.Add(addRoomModal);
            Controls.Add(topBar);
            Controls.Add(detailsModal);
            Controls.Add(flpRooms);
            Name = "RoomsView";
            Size = new Size(1677, 975);
            Load += RoomsView_Load;
            Resize += RoomsView_Resize;
            topBar.ResumeLayout(false);
            topBar.PerformLayout();
            flpRooms.ResumeLayout(false);
            detailsModal.ResumeLayout(false);
            detailsModal.PerformLayout();
            grpQuickActions.ResumeLayout(false);
            grpDetails.ResumeLayout(false);
            grpDetails.PerformLayout();
            addRoomModal.ResumeLayout(false);
            addRoomModal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)addRoomCapNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)addRoomRateNum).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel topBar;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cbStatusFilter;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cbBoardingHouses;
        private System.Windows.Forms.Button addRoomBtn;
        private System.Windows.Forms.Label lblBh;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.FlowLayoutPanel flpRooms;

        private System.Windows.Forms.Panel detailsModal;
        private System.Windows.Forms.Label lblRoomTitle;

        private System.Windows.Forms.GroupBox grpDetails;
        private System.Windows.Forms.TextBox detailsRoomNo;
        private System.Windows.Forms.Label labelRoomNo;
        private System.Windows.Forms.TextBox detailsRoomType;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.TextBox detailsCapacity;
        private System.Windows.Forms.Label labelCap;
        private System.Windows.Forms.TextBox detailsRate;
        private System.Windows.Forms.Label labelRate;
        private System.Windows.Forms.TextBox detailsStatus;
        private System.Windows.Forms.Label labelRoomStatus;
        private System.Windows.Forms.Label labelTenant;
        private System.Windows.Forms.TextBox detailsNotes;
        private System.Windows.Forms.Label labelNotes;

        private System.Windows.Forms.GroupBox grpQuickActions;
        private System.Windows.Forms.Button btnMarkInactive;
        private System.Windows.Forms.Button btnMarkMaintenance;
        private System.Windows.Forms.Button btnMarkOccupied;
        private System.Windows.Forms.Button btnMarkAvailable;
        private System.Windows.Forms.Panel addRoomModal;
        private System.Windows.Forms.Label addRoomTitle;
        private System.Windows.Forms.Button addRoomCloseBtn;
        private System.Windows.Forms.Label labelAddRoomBh;
        private System.Windows.Forms.ComboBox addRoomBhCb;
        private System.Windows.Forms.Label labelAddRoomNo;
        private System.Windows.Forms.TextBox addRoomRoomNoTxt;
        private System.Windows.Forms.Label labelAddRoomType;
        private System.Windows.Forms.TextBox addRoomTypeTxt;
        private System.Windows.Forms.Label labelAddRoomCap;
        private System.Windows.Forms.NumericUpDown addRoomCapNum;
        private System.Windows.Forms.Label labelAddRoomRate;
        private System.Windows.Forms.NumericUpDown addRoomRateNum;
        private System.Windows.Forms.Label labelAddRoomStatus;
        private System.Windows.Forms.ComboBox addRoomStatusCb;
        private System.Windows.Forms.Label labelAddRoomNotes;
        private System.Windows.Forms.TextBox addRoomNotesTxt;
        private System.Windows.Forms.Button addRoomSaveBtn;
        private System.Windows.Forms.Button addRoomCancelBtn;
        private System.Windows.Forms.Button btnCloseDetails;
        private Button button1;
        private ListBox detailsTenantsList;
        private Label lblOccupancy;
        private Button button2;
        private Button button3;
    }
}
