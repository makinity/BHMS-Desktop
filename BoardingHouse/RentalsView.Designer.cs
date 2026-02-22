namespace BoardingHouse
{
    partial class RentalsView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion

        #region Component Designer generated code

        private void InitializeComponent()
        {
            pnlTopBar = new Panel();
            lblTitle = new Label();
            btnNewRental = new Button();
            btnRefresh = new Button();
            btnExport = new Button();
            splitMain = new SplitContainer();
            modalNewRental = new Panel();
            lblModalNewTitle = new Label();
            lblModalOccupant = new Label();
            cbModalOccupant = new ComboBox();
            lblModalBh = new Label();
            cbModalBh = new ComboBox();
            lblModalRoom = new Label();
            cbModalRoom = new ComboBox();
            lblModalStart = new Label();
            dtModalStart = new DateTimePicker();
            lblModalRate = new Label();
            txtModalRate = new TextBox();
            lblModalNotes = new Label();
            txtModalNotes = new TextBox();
            btnModalNewCancel = new Button();
            btnModalNewSave = new Button();
            modalTransfer = new Panel();
            lblModalTransferTitle = new Label();
            lblModalCurrent = new Label();
            lblModalCurrentVal = new Label();
            lblModalNewBh = new Label();
            cbModalTransferBh = new ComboBox();
            lblModalNewRoom = new Label();
            cbModalTransferRoom = new ComboBox();
            lblModalTransferDate = new Label();
            dtModalTransferDate = new DateTimePicker();
            chkModalKeepRate = new CheckBox();
            lblModalNewRate = new Label();
            txtModalTransferRate = new TextBox();
            lblModalTransferNotes = new Label();
            txtModalTransferNotes = new TextBox();
            btnModalTransferCancel = new Button();
            btnModalTransferConfirm = new Button();
            grpFilters = new GroupBox();
            lblFilterBoardingHouse = new Label();
            cbBoardingHouse = new ComboBox();
            lblFilterRoom = new Label();
            cbRoom = new ComboBox();
            lblFilterStatus = new Label();
            cbStatus = new ComboBox();
            lblFilterFrom = new Label();
            dtFrom = new DateTimePicker();
            lblFilterTo = new Label();
            dtTo = new DateTimePicker();
            txtSearch = new TextBox();
            btnSearch = new Button();
            btnClear = new Button();
            dgvRentals = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colBoardingHouse = new DataGridViewTextBoxColumn();
            colRoomNo = new DataGridViewTextBoxColumn();
            colOccupant = new DataGridViewTextBoxColumn();
            colStart = new DataGridViewTextBoxColumn();
            colEnd = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            grpSummary = new GroupBox();
            lblSumRentalNo = new Label();
            lblSumRentalNoVal = new Label();
            lblSumOccupant = new Label();
            lblSumOccupantVal = new Label();
            lblSumContact = new Label();
            lblSumContactVal = new Label();
            lblSumRoom = new Label();
            lblSumRoomVal = new Label();
            lblSumBoardingHouse = new Label();
            lblSumBoardingHouseVal = new Label();
            lblSumRate = new Label();
            lblSumRateVal = new Label();
            lblNotes = new Label();
            txtNotes = new TextBox();
            grpAlerts = new GroupBox();
            txtAlerts = new TextBox();
            grpAudit = new GroupBox();
            dgvAudit = new DataGridView();
            colAuditDate = new DataGridViewTextBoxColumn();
            colAuditAction = new DataGridViewTextBoxColumn();
            colAuditUser = new DataGridViewTextBoxColumn();
            colAuditDetails = new DataGridViewTextBoxColumn();
            btnTransferRoom = new Button();
            pnlTopBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
            splitMain.Panel1.SuspendLayout();
            splitMain.Panel2.SuspendLayout();
            splitMain.SuspendLayout();
            modalNewRental.SuspendLayout();
            modalTransfer.SuspendLayout();
            grpFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRentals).BeginInit();
            grpSummary.SuspendLayout();
            grpAlerts.SuspendLayout();
            grpAudit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAudit).BeginInit();
            SuspendLayout();
            // 
            // pnlTopBar
            // 
            pnlTopBar.BackColor = Color.WhiteSmoke;
            pnlTopBar.Controls.Add(lblTitle);
            pnlTopBar.Controls.Add(btnNewRental);
            pnlTopBar.Controls.Add(btnRefresh);
            pnlTopBar.Controls.Add(btnExport);
            pnlTopBar.Dock = DockStyle.Top;
            pnlTopBar.Location = new Point(0, 0);
            pnlTopBar.Name = "pnlTopBar";
            pnlTopBar.Padding = new Padding(12, 10, 12, 10);
            pnlTopBar.Size = new Size(1677, 66);
            pnlTopBar.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(12, 14);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(420, 40);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Manage Rentals";
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnNewRental
            // 
            btnNewRental.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnNewRental.Location = new Point(1220, 18);
            btnNewRental.Name = "btnNewRental";
            btnNewRental.Size = new Size(150, 34);
            btnNewRental.TabIndex = 1;
            btnNewRental.Text = "New Rental";
            btnNewRental.UseVisualStyleBackColor = true;
            btnNewRental.Click += btnNewRental_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefresh.Location = new Point(1380, 18);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(120, 34);
            btnRefresh.TabIndex = 2;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnExport
            // 
            btnExport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExport.Location = new Point(1510, 18);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(120, 34);
            btnExport.TabIndex = 3;
            btnExport.Text = "Export";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += btnExport_Click;
            // 
            // splitMain
            // 
            splitMain.Dock = DockStyle.Fill;
            splitMain.Location = new Point(0, 66);
            splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            splitMain.Panel1.Controls.Add(modalNewRental);
            splitMain.Panel1.Controls.Add(modalTransfer);
            splitMain.Panel1.Controls.Add(grpFilters);
            splitMain.Panel1.Controls.Add(dgvRentals);
            // 
            // splitMain.Panel2
            // 
            splitMain.Panel2.Controls.Add(grpSummary);
            splitMain.Panel2.Controls.Add(grpAlerts);
            splitMain.Panel2.Controls.Add(grpAudit);
            splitMain.Size = new Size(1677, 909);
            splitMain.SplitterDistance = 1120;
            splitMain.TabIndex = 1;
            // 
            // modalNewRental
            // 
            modalNewRental.Anchor = AnchorStyles.None;
            modalNewRental.BackColor = Color.FromArgb(48, 54, 92);
            modalNewRental.BorderStyle = BorderStyle.FixedSingle;
            modalNewRental.Controls.Add(lblModalNewTitle);
            modalNewRental.Controls.Add(lblModalOccupant);
            modalNewRental.Controls.Add(cbModalOccupant);
            modalNewRental.Controls.Add(lblModalBh);
            modalNewRental.Controls.Add(cbModalBh);
            modalNewRental.Controls.Add(lblModalRoom);
            modalNewRental.Controls.Add(cbModalRoom);
            modalNewRental.Controls.Add(lblModalStart);
            modalNewRental.Controls.Add(dtModalStart);
            modalNewRental.Controls.Add(lblModalRate);
            modalNewRental.Controls.Add(txtModalRate);
            modalNewRental.Controls.Add(lblModalNotes);
            modalNewRental.Controls.Add(txtModalNotes);
            modalNewRental.Controls.Add(btnModalNewCancel);
            modalNewRental.Controls.Add(btnModalNewSave);
            modalNewRental.ForeColor = SystemColors.ButtonHighlight;
            modalNewRental.Location = new Point(299, 6);
            modalNewRental.Name = "modalNewRental";
            modalNewRental.Size = new Size(840, 560);
            modalNewRental.TabIndex = 50;
            modalNewRental.Visible = false;
            modalNewRental.Paint += modalNewRental_Paint;
            // 
            // lblModalNewTitle
            // 
            lblModalNewTitle.AutoSize = true;
            lblModalNewTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblModalNewTitle.Location = new Point(24, 20);
            lblModalNewTitle.Name = "lblModalNewTitle";
            lblModalNewTitle.Size = new Size(213, 32);
            lblModalNewTitle.TabIndex = 0;
            lblModalNewTitle.Text = "New / Edit Rental";
            // 
            // lblModalOccupant
            // 
            lblModalOccupant.AutoSize = true;
            lblModalOccupant.Location = new Point(28, 85);
            lblModalOccupant.Name = "lblModalOccupant";
            lblModalOccupant.Size = new Size(75, 20);
            lblModalOccupant.TabIndex = 1;
            lblModalOccupant.Text = "Occupant:";
            // 
            // cbModalOccupant
            // 
            cbModalOccupant.DropDownStyle = ComboBoxStyle.DropDownList;
            cbModalOccupant.FormattingEnabled = true;
            cbModalOccupant.Location = new Point(32, 110);
            cbModalOccupant.Name = "cbModalOccupant";
            cbModalOccupant.Size = new Size(360, 28);
            cbModalOccupant.TabIndex = 2;
            cbModalOccupant.SelectedIndexChanged += cbModalOccupant_SelectedIndexChanged;
            // 
            // lblModalBh
            // 
            lblModalBh.AutoSize = true;
            lblModalBh.Location = new Point(430, 85);
            lblModalBh.Name = "lblModalBh";
            lblModalBh.Size = new Size(119, 20);
            lblModalBh.TabIndex = 3;
            lblModalBh.Text = "Boarding House:";
            // 
            // cbModalBh
            // 
            cbModalBh.DropDownStyle = ComboBoxStyle.DropDownList;
            cbModalBh.FormattingEnabled = true;
            cbModalBh.Location = new Point(434, 110);
            cbModalBh.Name = "cbModalBh";
            cbModalBh.Size = new Size(360, 28);
            cbModalBh.TabIndex = 4;
            cbModalBh.SelectedIndexChanged += cbModalBh_SelectedIndexChanged;
            // 
            // lblModalRoom
            // 
            lblModalRoom.AutoSize = true;
            lblModalRoom.Location = new Point(28, 155);
            lblModalRoom.Name = "lblModalRoom";
            lblModalRoom.Size = new Size(52, 20);
            lblModalRoom.TabIndex = 5;
            lblModalRoom.Text = "Room:";
            // 
            // cbModalRoom
            // 
            cbModalRoom.DropDownStyle = ComboBoxStyle.DropDownList;
            cbModalRoom.FormattingEnabled = true;
            cbModalRoom.Location = new Point(32, 180);
            cbModalRoom.Name = "cbModalRoom";
            cbModalRoom.Size = new Size(360, 28);
            cbModalRoom.TabIndex = 6;
            cbModalRoom.SelectedIndexChanged += cbModalRoom_SelectedIndexChanged;
            // 
            // lblModalStart
            // 
            lblModalStart.AutoSize = true;
            lblModalStart.Location = new Point(430, 155);
            lblModalStart.Name = "lblModalStart";
            lblModalStart.Size = new Size(79, 20);
            lblModalStart.TabIndex = 7;
            lblModalStart.Text = "Start Date:";
            // 
            // dtModalStart
            // 
            dtModalStart.Format = DateTimePickerFormat.Short;
            dtModalStart.Location = new Point(434, 180);
            dtModalStart.Name = "dtModalStart";
            dtModalStart.Size = new Size(360, 27);
            dtModalStart.TabIndex = 8;
            // 
            // lblModalRate
            // 
            lblModalRate.AutoSize = true;
            lblModalRate.Location = new Point(28, 225);
            lblModalRate.Name = "lblModalRate";
            lblModalRate.Size = new Size(100, 20);
            lblModalRate.TabIndex = 9;
            lblModalRate.Text = "Monthly Rate:";
            // 
            // txtModalRate
            // 
            txtModalRate.BorderStyle = BorderStyle.FixedSingle;
            txtModalRate.Location = new Point(32, 250);
            txtModalRate.Name = "txtModalRate";
            txtModalRate.PlaceholderText = "0.00";
            txtModalRate.Size = new Size(360, 27);
            txtModalRate.TabIndex = 10;
            // 
            // lblModalNotes
            // 
            lblModalNotes.AutoSize = true;
            lblModalNotes.Location = new Point(28, 295);
            lblModalNotes.Name = "lblModalNotes";
            lblModalNotes.Size = new Size(51, 20);
            lblModalNotes.TabIndex = 11;
            lblModalNotes.Text = "Notes:";
            // 
            // txtModalNotes
            // 
            txtModalNotes.BorderStyle = BorderStyle.FixedSingle;
            txtModalNotes.Location = new Point(32, 320);
            txtModalNotes.Multiline = true;
            txtModalNotes.Name = "txtModalNotes";
            txtModalNotes.Size = new Size(762, 150);
            txtModalNotes.TabIndex = 12;
            // 
            // btnModalNewCancel
            // 
            btnModalNewCancel.BackColor = Color.FromArgb(255, 192, 192);
            btnModalNewCancel.ForeColor = SystemColors.ActiveCaptionText;
            btnModalNewCancel.Location = new Point(584, 490);
            btnModalNewCancel.Name = "btnModalNewCancel";
            btnModalNewCancel.Size = new Size(105, 36);
            btnModalNewCancel.TabIndex = 13;
            btnModalNewCancel.Text = "Cancel";
            btnModalNewCancel.UseVisualStyleBackColor = false;
            btnModalNewCancel.Click += btnModalNewCancel_Click;
            // 
            // btnModalNewSave
            // 
            btnModalNewSave.BackColor = Color.FromArgb(128, 255, 128);
            btnModalNewSave.ForeColor = SystemColors.ActiveCaptionText;
            btnModalNewSave.Location = new Point(695, 490);
            btnModalNewSave.Name = "btnModalNewSave";
            btnModalNewSave.Size = new Size(105, 36);
            btnModalNewSave.TabIndex = 14;
            btnModalNewSave.Text = "Save";
            btnModalNewSave.UseVisualStyleBackColor = false;
            btnModalNewSave.Click += btnModalNewSave_Click;
            // 
            // modalTransfer
            // 
            modalTransfer.Anchor = AnchorStyles.None;
            modalTransfer.BackColor = Color.FromArgb(48, 54, 92);
            modalTransfer.BorderStyle = BorderStyle.FixedSingle;
            modalTransfer.Controls.Add(lblModalTransferTitle);
            modalTransfer.Controls.Add(lblModalCurrent);
            modalTransfer.Controls.Add(lblModalCurrentVal);
            modalTransfer.Controls.Add(lblModalNewBh);
            modalTransfer.Controls.Add(cbModalTransferBh);
            modalTransfer.Controls.Add(lblModalNewRoom);
            modalTransfer.Controls.Add(cbModalTransferRoom);
            modalTransfer.Controls.Add(lblModalTransferDate);
            modalTransfer.Controls.Add(dtModalTransferDate);
            modalTransfer.Controls.Add(chkModalKeepRate);
            modalTransfer.Controls.Add(lblModalNewRate);
            modalTransfer.Controls.Add(txtModalTransferRate);
            modalTransfer.Controls.Add(lblModalTransferNotes);
            modalTransfer.Controls.Add(txtModalTransferNotes);
            modalTransfer.Controls.Add(btnModalTransferCancel);
            modalTransfer.Controls.Add(btnModalTransferConfirm);
            modalTransfer.ForeColor = SystemColors.ButtonHighlight;
            modalTransfer.Location = new Point(423, 0);
            modalTransfer.Name = "modalTransfer";
            modalTransfer.Size = new Size(720, 520);
            modalTransfer.TabIndex = 51;
            modalTransfer.Visible = false;
            // 
            // lblModalTransferTitle
            // 
            lblModalTransferTitle.AutoSize = true;
            lblModalTransferTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblModalTransferTitle.Location = new Point(24, 20);
            lblModalTransferTitle.Name = "lblModalTransferTitle";
            lblModalTransferTitle.Size = new Size(181, 32);
            lblModalTransferTitle.TabIndex = 0;
            lblModalTransferTitle.Text = "Transfer Room";
            // 
            // lblModalCurrent
            // 
            lblModalCurrent.AutoSize = true;
            lblModalCurrent.Location = new Point(28, 80);
            lblModalCurrent.Name = "lblModalCurrent";
            lblModalCurrent.Size = new Size(104, 20);
            lblModalCurrent.TabIndex = 1;
            lblModalCurrent.Text = "Current Room:";
            // 
            // lblModalCurrentVal
            // 
            lblModalCurrentVal.AutoSize = true;
            lblModalCurrentVal.Location = new Point(170, 80);
            lblModalCurrentVal.Name = "lblModalCurrentVal";
            lblModalCurrentVal.Size = new Size(15, 20);
            lblModalCurrentVal.TabIndex = 2;
            lblModalCurrentVal.Text = "-";
            // 
            // lblModalNewBh
            // 
            lblModalNewBh.AutoSize = true;
            lblModalNewBh.Location = new Point(28, 120);
            lblModalNewBh.Name = "lblModalNewBh";
            lblModalNewBh.Size = new Size(153, 20);
            lblModalNewBh.TabIndex = 3;
            lblModalNewBh.Text = "New Boarding House:";
            // 
            // cbModalTransferBh
            // 
            cbModalTransferBh.DropDownStyle = ComboBoxStyle.DropDownList;
            cbModalTransferBh.FormattingEnabled = true;
            cbModalTransferBh.Location = new Point(32, 145);
            cbModalTransferBh.Name = "cbModalTransferBh";
            cbModalTransferBh.Size = new Size(656, 28);
            cbModalTransferBh.TabIndex = 4;
            cbModalTransferBh.SelectedIndexChanged += cbModalTransferBh_SelectedIndexChanged;
            // 
            // lblModalNewRoom
            // 
            lblModalNewRoom.AutoSize = true;
            lblModalNewRoom.Location = new Point(28, 190);
            lblModalNewRoom.Name = "lblModalNewRoom";
            lblModalNewRoom.Size = new Size(86, 20);
            lblModalNewRoom.TabIndex = 5;
            lblModalNewRoom.Text = "New Room:";
            // 
            // cbModalTransferRoom
            // 
            cbModalTransferRoom.DropDownStyle = ComboBoxStyle.DropDownList;
            cbModalTransferRoom.FormattingEnabled = true;
            cbModalTransferRoom.Location = new Point(32, 215);
            cbModalTransferRoom.Name = "cbModalTransferRoom";
            cbModalTransferRoom.Size = new Size(656, 28);
            cbModalTransferRoom.TabIndex = 6;
            cbModalTransferRoom.SelectedIndexChanged += cbModalTransferRoom_SelectedIndexChanged;
            // 
            // lblModalTransferDate
            // 
            lblModalTransferDate.AutoSize = true;
            lblModalTransferDate.Location = new Point(28, 260);
            lblModalTransferDate.Name = "lblModalTransferDate";
            lblModalTransferDate.Size = new Size(100, 20);
            lblModalTransferDate.TabIndex = 7;
            lblModalTransferDate.Text = "Transfer Date:";
            // 
            // dtModalTransferDate
            // 
            dtModalTransferDate.Format = DateTimePickerFormat.Short;
            dtModalTransferDate.Location = new Point(32, 285);
            dtModalTransferDate.Name = "dtModalTransferDate";
            dtModalTransferDate.Size = new Size(240, 27);
            dtModalTransferDate.TabIndex = 8;
            // 
            // chkModalKeepRate
            // 
            chkModalKeepRate.AutoSize = true;
            chkModalKeepRate.Location = new Point(300, 288);
            chkModalKeepRate.Name = "chkModalKeepRate";
            chkModalKeepRate.Size = new Size(134, 24);
            chkModalKeepRate.TabIndex = 9;
            chkModalKeepRate.Text = "Keep same rate";
            chkModalKeepRate.UseVisualStyleBackColor = true;
            chkModalKeepRate.CheckedChanged += chkModalKeepRate_CheckedChanged;
            // 
            // lblModalNewRate
            // 
            lblModalNewRate.AutoSize = true;
            lblModalNewRate.Location = new Point(28, 330);
            lblModalNewRate.Name = "lblModalNewRate";
            lblModalNewRate.Size = new Size(76, 20);
            lblModalNewRate.TabIndex = 10;
            lblModalNewRate.Text = "New Rate:";
            // 
            // txtModalTransferRate
            // 
            txtModalTransferRate.BorderStyle = BorderStyle.FixedSingle;
            txtModalTransferRate.Location = new Point(32, 355);
            txtModalTransferRate.Name = "txtModalTransferRate";
            txtModalTransferRate.PlaceholderText = "0.00";
            txtModalTransferRate.Size = new Size(240, 27);
            txtModalTransferRate.TabIndex = 11;
            // 
            // lblModalTransferNotes
            // 
            lblModalTransferNotes.AutoSize = true;
            lblModalTransferNotes.Location = new Point(300, 330);
            lblModalTransferNotes.Name = "lblModalTransferNotes";
            lblModalTransferNotes.Size = new Size(51, 20);
            lblModalTransferNotes.TabIndex = 12;
            lblModalTransferNotes.Text = "Notes:";
            // 
            // txtModalTransferNotes
            // 
            txtModalTransferNotes.BorderStyle = BorderStyle.FixedSingle;
            txtModalTransferNotes.Location = new Point(304, 355);
            txtModalTransferNotes.Multiline = true;
            txtModalTransferNotes.Name = "txtModalTransferNotes";
            txtModalTransferNotes.Size = new Size(384, 80);
            txtModalTransferNotes.TabIndex = 13;
            // 
            // btnModalTransferCancel
            // 
            btnModalTransferCancel.BackColor = Color.FromArgb(255, 192, 192);
            btnModalTransferCancel.ForeColor = SystemColors.ActiveCaptionText;
            btnModalTransferCancel.Location = new Point(468, 455);
            btnModalTransferCancel.Name = "btnModalTransferCancel";
            btnModalTransferCancel.Size = new Size(105, 36);
            btnModalTransferCancel.TabIndex = 14;
            btnModalTransferCancel.Text = "Cancel";
            btnModalTransferCancel.UseVisualStyleBackColor = false;
            btnModalTransferCancel.Click += btnModalTransferCancel_Click;
            // 
            // btnModalTransferConfirm
            // 
            btnModalTransferConfirm.BackColor = Color.FromArgb(128, 255, 128);
            btnModalTransferConfirm.ForeColor = SystemColors.ActiveCaptionText;
            btnModalTransferConfirm.Location = new Point(583, 455);
            btnModalTransferConfirm.Name = "btnModalTransferConfirm";
            btnModalTransferConfirm.Size = new Size(105, 36);
            btnModalTransferConfirm.TabIndex = 15;
            btnModalTransferConfirm.Text = "Confirm";
            btnModalTransferConfirm.UseVisualStyleBackColor = false;
            btnModalTransferConfirm.Click += btnModalTransferConfirm_Click;
            // 
            // grpFilters
            // 
            grpFilters.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpFilters.Controls.Add(lblFilterBoardingHouse);
            grpFilters.Controls.Add(cbBoardingHouse);
            grpFilters.Controls.Add(lblFilterRoom);
            grpFilters.Controls.Add(cbRoom);
            grpFilters.Controls.Add(lblFilterStatus);
            grpFilters.Controls.Add(cbStatus);
            grpFilters.Controls.Add(lblFilterFrom);
            grpFilters.Controls.Add(dtFrom);
            grpFilters.Controls.Add(lblFilterTo);
            grpFilters.Controls.Add(dtTo);
            grpFilters.Controls.Add(txtSearch);
            grpFilters.Controls.Add(btnSearch);
            grpFilters.Controls.Add(btnClear);
            grpFilters.Location = new Point(12, 12);
            grpFilters.Name = "grpFilters";
            grpFilters.Size = new Size(1092, 130);
            grpFilters.TabIndex = 0;
            grpFilters.TabStop = false;
            grpFilters.Text = "Rental Details";
            // 
            // lblFilterBoardingHouse
            // 
            lblFilterBoardingHouse.AutoSize = true;
            lblFilterBoardingHouse.Location = new Point(14, 30);
            lblFilterBoardingHouse.Name = "lblFilterBoardingHouse";
            lblFilterBoardingHouse.Size = new Size(119, 20);
            lblFilterBoardingHouse.TabIndex = 0;
            lblFilterBoardingHouse.Text = "Boarding House:";
            // 
            // cbBoardingHouse
            // 
            cbBoardingHouse.DropDownStyle = ComboBoxStyle.DropDownList;
            cbBoardingHouse.FormattingEnabled = true;
            cbBoardingHouse.Location = new Point(130, 26);
            cbBoardingHouse.Name = "cbBoardingHouse";
            cbBoardingHouse.Size = new Size(240, 28);
            cbBoardingHouse.TabIndex = 1;
            cbBoardingHouse.SelectedIndexChanged += cbBoardingHouse_SelectedIndexChanged;
            // 
            // lblFilterRoom
            // 
            lblFilterRoom.AutoSize = true;
            lblFilterRoom.Location = new Point(390, 30);
            lblFilterRoom.Name = "lblFilterRoom";
            lblFilterRoom.Size = new Size(52, 20);
            lblFilterRoom.TabIndex = 2;
            lblFilterRoom.Text = "Room:";
            // 
            // cbRoom
            // 
            cbRoom.DropDownStyle = ComboBoxStyle.DropDownList;
            cbRoom.FormattingEnabled = true;
            cbRoom.Location = new Point(445, 26);
            cbRoom.Name = "cbRoom";
            cbRoom.Size = new Size(160, 28);
            cbRoom.TabIndex = 3;
            cbRoom.SelectedIndexChanged += cbRoom_SelectedIndexChanged;
            // 
            // lblFilterStatus
            // 
            lblFilterStatus.AutoSize = true;
            lblFilterStatus.Location = new Point(625, 30);
            lblFilterStatus.Name = "lblFilterStatus";
            lblFilterStatus.Size = new Size(52, 20);
            lblFilterStatus.TabIndex = 4;
            lblFilterStatus.Text = "Status:";
            // 
            // cbStatus
            // 
            cbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cbStatus.FormattingEnabled = true;
            cbStatus.Location = new Point(683, 26);
            cbStatus.Name = "cbStatus";
            cbStatus.Size = new Size(160, 28);
            cbStatus.TabIndex = 5;
            cbStatus.SelectedIndexChanged += cbStatus_SelectedIndexChanged;
            // 
            // lblFilterFrom
            // 
            lblFilterFrom.AutoSize = true;
            lblFilterFrom.Location = new Point(14, 75);
            lblFilterFrom.Name = "lblFilterFrom";
            lblFilterFrom.Size = new Size(46, 20);
            lblFilterFrom.TabIndex = 6;
            lblFilterFrom.Text = "From:";
            // 
            // dtFrom
            // 
            dtFrom.Format = DateTimePickerFormat.Short;
            dtFrom.Location = new Point(64, 71);
            dtFrom.Name = "dtFrom";
            dtFrom.Size = new Size(140, 27);
            dtFrom.TabIndex = 7;
            // 
            // lblFilterTo
            // 
            lblFilterTo.AutoSize = true;
            lblFilterTo.Location = new Point(220, 75);
            lblFilterTo.Name = "lblFilterTo";
            lblFilterTo.Size = new Size(28, 20);
            lblFilterTo.TabIndex = 8;
            lblFilterTo.Text = "To:";
            // 
            // dtTo
            // 
            dtTo.Format = DateTimePickerFormat.Short;
            dtTo.Location = new Point(251, 71);
            dtTo.Name = "dtTo";
            dtTo.Size = new Size(140, 27);
            dtTo.TabIndex = 9;
            // 
            // txtSearch
            // 
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Location = new Point(405, 71);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search occupant, room no, rental id...";
            txtSearch.Size = new Size(440, 27);
            txtSearch.TabIndex = 10;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(860, 69);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(100, 30);
            btnSearch.TabIndex = 11;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(970, 69);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(100, 30);
            btnClear.TabIndex = 12;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // dgvRentals
            // 
            dgvRentals.AllowUserToAddRows = false;
            dgvRentals.AllowUserToDeleteRows = false;
            dgvRentals.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvRentals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRentals.ColumnHeadersHeight = 29;
            dgvRentals.Columns.AddRange(new DataGridViewColumn[] { colId, colBoardingHouse, colRoomNo, colOccupant, colStart, colEnd, colStatus });
            dgvRentals.Location = new Point(12, 152);
            dgvRentals.MultiSelect = false;
            dgvRentals.Name = "dgvRentals";
            dgvRentals.ReadOnly = true;
            dgvRentals.RowHeadersVisible = false;
            dgvRentals.RowHeadersWidth = 51;
            dgvRentals.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRentals.Size = new Size(1092, 745);
            dgvRentals.TabIndex = 1;
            dgvRentals.CellClick += dgvRentals_CellClick;
            // 
            // colId
            // 
            colId.HeaderText = "ID";
            colId.MinimumWidth = 6;
            colId.Name = "colId";
            colId.ReadOnly = true;
            // 
            // colBoardingHouse
            // 
            colBoardingHouse.HeaderText = "Boarding House";
            colBoardingHouse.MinimumWidth = 6;
            colBoardingHouse.Name = "colBoardingHouse";
            colBoardingHouse.ReadOnly = true;
            // 
            // colRoomNo
            // 
            colRoomNo.HeaderText = "Room No";
            colRoomNo.MinimumWidth = 6;
            colRoomNo.Name = "colRoomNo";
            colRoomNo.ReadOnly = true;
            // 
            // colOccupant
            // 
            colOccupant.HeaderText = "Occupant";
            colOccupant.MinimumWidth = 6;
            colOccupant.Name = "colOccupant";
            colOccupant.ReadOnly = true;
            // 
            // colStart
            // 
            colStart.HeaderText = "From";
            colStart.MinimumWidth = 6;
            colStart.Name = "colStart";
            colStart.ReadOnly = true;
            // 
            // colEnd
            // 
            colEnd.HeaderText = "To";
            colEnd.MinimumWidth = 6;
            colEnd.Name = "colEnd";
            colEnd.ReadOnly = true;
            // 
            // colStatus
            // 
            colStatus.HeaderText = "Status";
            colStatus.MinimumWidth = 6;
            colStatus.Name = "colStatus";
            colStatus.ReadOnly = true;
            // 
            // grpSummary
            // 
            grpSummary.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpSummary.Controls.Add(btnTransferRoom);
            grpSummary.Controls.Add(lblSumRentalNo);
            grpSummary.Controls.Add(lblSumRentalNoVal);
            grpSummary.Controls.Add(lblSumOccupant);
            grpSummary.Controls.Add(lblSumOccupantVal);
            grpSummary.Controls.Add(lblSumContact);
            grpSummary.Controls.Add(lblSumContactVal);
            grpSummary.Controls.Add(lblSumRoom);
            grpSummary.Controls.Add(lblSumRoomVal);
            grpSummary.Controls.Add(lblSumBoardingHouse);
            grpSummary.Controls.Add(lblSumBoardingHouseVal);
            grpSummary.Controls.Add(lblSumRate);
            grpSummary.Controls.Add(lblSumRateVal);
            grpSummary.Controls.Add(lblNotes);
            grpSummary.Controls.Add(txtNotes);
            grpSummary.Location = new Point(12, 12);
            grpSummary.Name = "grpSummary";
            grpSummary.Size = new Size(461, 279);
            grpSummary.TabIndex = 0;
            grpSummary.TabStop = false;
            grpSummary.Text = "Rental Summary";
            // 
            // lblSumRentalNo
            // 
            lblSumRentalNo.AutoSize = true;
            lblSumRentalNo.Location = new Point(18, 35);
            lblSumRentalNo.Name = "lblSumRentalNo";
            lblSumRentalNo.Size = new Size(67, 20);
            lblSumRentalNo.TabIndex = 0;
            lblSumRentalNo.Text = "Rental #:";
            // 
            // lblSumRentalNoVal
            // 
            lblSumRentalNoVal.AutoSize = true;
            lblSumRentalNoVal.Location = new Point(160, 35);
            lblSumRentalNoVal.Name = "lblSumRentalNoVal";
            lblSumRentalNoVal.Size = new Size(15, 20);
            lblSumRentalNoVal.TabIndex = 1;
            lblSumRentalNoVal.Text = "-";
            // 
            // lblSumOccupant
            // 
            lblSumOccupant.AutoSize = true;
            lblSumOccupant.Location = new Point(18, 65);
            lblSumOccupant.Name = "lblSumOccupant";
            lblSumOccupant.Size = new Size(75, 20);
            lblSumOccupant.TabIndex = 2;
            lblSumOccupant.Text = "Occupant:";
            // 
            // lblSumOccupantVal
            // 
            lblSumOccupantVal.AutoSize = true;
            lblSumOccupantVal.Location = new Point(160, 65);
            lblSumOccupantVal.Name = "lblSumOccupantVal";
            lblSumOccupantVal.Size = new Size(15, 20);
            lblSumOccupantVal.TabIndex = 3;
            lblSumOccupantVal.Text = "-";
            // 
            // lblSumContact
            // 
            lblSumContact.AutoSize = true;
            lblSumContact.Location = new Point(18, 95);
            lblSumContact.Name = "lblSumContact";
            lblSumContact.Size = new Size(63, 20);
            lblSumContact.TabIndex = 4;
            lblSumContact.Text = "Contact:";
            // 
            // lblSumContactVal
            // 
            lblSumContactVal.AutoSize = true;
            lblSumContactVal.Location = new Point(160, 95);
            lblSumContactVal.Name = "lblSumContactVal";
            lblSumContactVal.Size = new Size(15, 20);
            lblSumContactVal.TabIndex = 5;
            lblSumContactVal.Text = "-";
            // 
            // lblSumRoom
            // 
            lblSumRoom.AutoSize = true;
            lblSumRoom.Location = new Point(18, 125);
            lblSumRoom.Name = "lblSumRoom";
            lblSumRoom.Size = new Size(52, 20);
            lblSumRoom.TabIndex = 6;
            lblSumRoom.Text = "Room:";
            // 
            // lblSumRoomVal
            // 
            lblSumRoomVal.AutoSize = true;
            lblSumRoomVal.Location = new Point(160, 125);
            lblSumRoomVal.Name = "lblSumRoomVal";
            lblSumRoomVal.Size = new Size(15, 20);
            lblSumRoomVal.TabIndex = 7;
            lblSumRoomVal.Text = "-";
            // 
            // lblSumBoardingHouse
            // 
            lblSumBoardingHouse.AutoSize = true;
            lblSumBoardingHouse.Location = new Point(18, 155);
            lblSumBoardingHouse.Name = "lblSumBoardingHouse";
            lblSumBoardingHouse.Size = new Size(119, 20);
            lblSumBoardingHouse.TabIndex = 8;
            lblSumBoardingHouse.Text = "Boarding House:";
            // 
            // lblSumBoardingHouseVal
            // 
            lblSumBoardingHouseVal.AutoSize = true;
            lblSumBoardingHouseVal.Location = new Point(160, 155);
            lblSumBoardingHouseVal.Name = "lblSumBoardingHouseVal";
            lblSumBoardingHouseVal.Size = new Size(15, 20);
            lblSumBoardingHouseVal.TabIndex = 9;
            lblSumBoardingHouseVal.Text = "-";
            // 
            // lblSumRate
            // 
            lblSumRate.AutoSize = true;
            lblSumRate.Location = new Point(18, 185);
            lblSumRate.Name = "lblSumRate";
            lblSumRate.Size = new Size(42, 20);
            lblSumRate.TabIndex = 10;
            lblSumRate.Text = "Rate:";
            // 
            // lblSumRateVal
            // 
            lblSumRateVal.AutoSize = true;
            lblSumRateVal.Location = new Point(160, 185);
            lblSumRateVal.Name = "lblSumRateVal";
            lblSumRateVal.Size = new Size(15, 20);
            lblSumRateVal.TabIndex = 11;
            lblSumRateVal.Text = "-";
            // 
            // lblNotes
            // 
            lblNotes.AutoSize = true;
            lblNotes.Location = new Point(18, 215);
            lblNotes.Name = "lblNotes";
            lblNotes.Size = new Size(51, 20);
            lblNotes.TabIndex = 12;
            lblNotes.Text = "Notes:";
            // 
            // txtNotes
            // 
            txtNotes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtNotes.BorderStyle = BorderStyle.FixedSingle;
            txtNotes.Location = new Point(22, 238);
            txtNotes.Multiline = true;
            txtNotes.Name = "txtNotes";
            txtNotes.Size = new Size(417, 18);
            txtNotes.TabIndex = 13;
            // 
            // grpAlerts
            // 
            grpAlerts.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpAlerts.Controls.Add(txtAlerts);
            grpAlerts.Location = new Point(15, 317);
            grpAlerts.Name = "grpAlerts";
            grpAlerts.Size = new Size(517, 160);
            grpAlerts.TabIndex = 1;
            grpAlerts.TabStop = false;
            grpAlerts.Text = "Alerts";
            // 
            // txtAlerts
            // 
            txtAlerts.BackColor = Color.LightYellow;
            txtAlerts.BorderStyle = BorderStyle.FixedSingle;
            txtAlerts.Dock = DockStyle.Fill;
            txtAlerts.Location = new Point(3, 23);
            txtAlerts.Multiline = true;
            txtAlerts.Name = "txtAlerts";
            txtAlerts.ReadOnly = true;
            txtAlerts.ScrollBars = ScrollBars.Vertical;
            txtAlerts.Size = new Size(511, 134);
            txtAlerts.TabIndex = 0;
            // 
            // grpAudit
            // 
            grpAudit.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpAudit.Controls.Add(dgvAudit);
            grpAudit.Location = new Point(12, 497);
            grpAudit.Name = "grpAudit";
            grpAudit.Size = new Size(517, 400);
            grpAudit.TabIndex = 2;
            grpAudit.TabStop = false;
            grpAudit.Text = "Activity / Audit Logs";
            // 
            // dgvAudit
            // 
            dgvAudit.AllowUserToAddRows = false;
            dgvAudit.AllowUserToDeleteRows = false;
            dgvAudit.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAudit.ColumnHeadersHeight = 29;
            dgvAudit.Columns.AddRange(new DataGridViewColumn[] { colAuditDate, colAuditAction, colAuditUser, colAuditDetails });
            dgvAudit.Dock = DockStyle.Fill;
            dgvAudit.Location = new Point(3, 23);
            dgvAudit.Name = "dgvAudit";
            dgvAudit.ReadOnly = true;
            dgvAudit.RowHeadersVisible = false;
            dgvAudit.RowHeadersWidth = 51;
            dgvAudit.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAudit.Size = new Size(511, 374);
            dgvAudit.TabIndex = 0;
            // 
            // colAuditDate
            // 
            colAuditDate.HeaderText = "Date";
            colAuditDate.MinimumWidth = 6;
            colAuditDate.Name = "colAuditDate";
            colAuditDate.ReadOnly = true;
            // 
            // colAuditAction
            // 
            colAuditAction.HeaderText = "Action";
            colAuditAction.MinimumWidth = 6;
            colAuditAction.Name = "colAuditAction";
            colAuditAction.ReadOnly = true;
            // 
            // colAuditUser
            // 
            colAuditUser.HeaderText = "User";
            colAuditUser.MinimumWidth = 6;
            colAuditUser.Name = "colAuditUser";
            colAuditUser.ReadOnly = true;
            // 
            // colAuditDetails
            // 
            colAuditDetails.HeaderText = "Details";
            colAuditDetails.MinimumWidth = 6;
            colAuditDetails.Name = "colAuditDetails";
            colAuditDetails.ReadOnly = true;
            // 
            // btnTransferRoom
            // 
            btnTransferRoom.BackColor = Color.FromArgb(48, 54, 92);
            btnTransferRoom.ForeColor = SystemColors.ButtonHighlight;
            btnTransferRoom.Location = new Point(329, 16);
            btnTransferRoom.Name = "btnTransferRoom";
            btnTransferRoom.Size = new Size(126, 36);
            btnTransferRoom.TabIndex = 15;
            btnTransferRoom.Text = "Transfer Room";
            btnTransferRoom.UseVisualStyleBackColor = false;
            btnTransferRoom.Click += btnTransferRoom_Click;
            // 
            // RentalsView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(splitMain);
            Controls.Add(pnlTopBar);
            Name = "RentalsView";
            Size = new Size(1677, 975);
            pnlTopBar.ResumeLayout(false);
            splitMain.Panel1.ResumeLayout(false);
            splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
            splitMain.ResumeLayout(false);
            modalNewRental.ResumeLayout(false);
            modalNewRental.PerformLayout();
            modalTransfer.ResumeLayout(false);
            modalTransfer.PerformLayout();
            grpFilters.ResumeLayout(false);
            grpFilters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRentals).EndInit();
            grpSummary.ResumeLayout(false);
            grpSummary.PerformLayout();
            grpAlerts.ResumeLayout(false);
            grpAlerts.PerformLayout();
            grpAudit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvAudit).EndInit();
            ResumeLayout(false);
        }

        #endregion

        // Top bar
        private Panel pnlTopBar;
        private Label lblTitle;
        private Button btnNewRental;
        private Button btnRefresh;
        private Button btnExport;

        // Main split
        private SplitContainer splitMain;

        // Left filters + grid
        private GroupBox grpFilters;
        private Label lblFilterBoardingHouse;
        private ComboBox cbBoardingHouse;
        private Label lblFilterRoom;
        private ComboBox cbRoom;
        private Label lblFilterStatus;
        private ComboBox cbStatus;
        private Label lblFilterFrom;
        private DateTimePicker dtFrom;
        private Label lblFilterTo;
        private DateTimePicker dtTo;
        private TextBox txtSearch;
        private Button btnSearch;
        private Button btnClear;

        private DataGridView dgvRentals;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colBoardingHouse;
        private DataGridViewTextBoxColumn colRoomNo;
        private DataGridViewTextBoxColumn colOccupant;
        private DataGridViewTextBoxColumn colStart;
        private DataGridViewTextBoxColumn colEnd;
        private DataGridViewTextBoxColumn colStatus;

        // Right summary/alerts/audit
        private GroupBox grpSummary;
        private Label lblSumRentalNo;
        private Label lblSumRentalNoVal;
        private Label lblSumOccupant;
        private Label lblSumOccupantVal;
        private Label lblSumContact;
        private Label lblSumContactVal;
        private Label lblSumRoom;
        private Label lblSumRoomVal;
        private Label lblSumBoardingHouse;
        private Label lblSumBoardingHouseVal;
        private Label lblSumRate;
        private Label lblSumRateVal;
        private Label lblNotes;
        private TextBox txtNotes;

        private GroupBox grpAlerts;
        private TextBox txtAlerts;

        private GroupBox grpAudit;
        private DataGridView dgvAudit;
        private DataGridViewTextBoxColumn colAuditDate;
        private DataGridViewTextBoxColumn colAuditAction;
        private DataGridViewTextBoxColumn colAuditUser;
        private DataGridViewTextBoxColumn colAuditDetails;

        // Hidden modals (simple panels)
        private Panel modalNewRental;
        private Label lblModalNewTitle;
        private Label lblModalOccupant;
        private ComboBox cbModalOccupant;
        private Label lblModalBh;
        private ComboBox cbModalBh;
        private Label lblModalRoom;
        private ComboBox cbModalRoom;
        private Label lblModalStart;
        private DateTimePicker dtModalStart;
        private Label lblModalRate;
        private TextBox txtModalRate;
        private Label lblModalNotes;
        private TextBox txtModalNotes;
        private Button btnModalNewSave;
        private Button btnModalNewCancel;

        private Panel modalTransfer;
        private Label lblModalTransferTitle;
        private Label lblModalCurrent;
        private Label lblModalCurrentVal;
        private Label lblModalNewBh;
        private ComboBox cbModalTransferBh;
        private Label lblModalNewRoom;
        private ComboBox cbModalTransferRoom;
        private Label lblModalTransferDate;
        private DateTimePicker dtModalTransferDate;
        private CheckBox chkModalKeepRate;
        private Label lblModalNewRate;
        private TextBox txtModalTransferRate;
        private Label lblModalTransferNotes;
        private TextBox txtModalTransferNotes;
        private Button btnModalTransferConfirm;
        private Button btnModalTransferCancel;
        private Button btnTransferRoom;
    }
}