// ReservationsView.Designer.cs
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BoardingHouse
{
    partial class ReservationsView
    {
        private IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            label1 = new Label();
            btnNewReservation = new Button();
            btnRefresh = new Button();
            btnExport = new Button();
            pnlMain = new Panel();
            pnlRight = new Panel();
            gbLogs = new GroupBox();
            dgvAuditLogs = new DataGridView();
            colLogDate = new DataGridViewTextBoxColumn();
            colLogAction = new DataGridViewTextBoxColumn();
            colLogUser = new DataGridViewTextBoxColumn();
            colLogDetails = new DataGridViewTextBoxColumn();
            gbSummary = new GroupBox();
            lblSelectedId = new Label();
            lblSelectedStatusBadge = new Label();
            lblOccName = new Label();
            lblOccContact = new Label();
            lblRoomNo = new Label();
            lblBHName = new Label();
            lblSelectedNotesTitle = new Label();
            txtSelectedNotes = new TextBox();
            btnApprove = new Button();
            btnReject = new Button();
            btnCancelReservation = new Button();
            btnCheckIn = new Button();
            btnConvertToRental = new Button();
            btnEditReservation = new Button();
            lblAlertsTitle = new Label();
            txtAlerts = new TextBox();
            gbReservations = new GroupBox();
            lblBoardingHouse = new Label();
            cbBoardingHouse = new ComboBox();
            lblRoom = new Label();
            cbRoom = new ComboBox();
            lblStatusFilter = new Label();
            cbStatusFilter = new ComboBox();
            lblFrom = new Label();
            dtFilterFrom = new DateTimePicker();
            lblTo = new Label();
            dtFilterTo = new DateTimePicker();
            txtSearch = new TextBox();
            btnSearch = new Button();
            btnClear = new Button();
            dgvReservations = new DataGridView();
            colResId = new DataGridViewTextBoxColumn();
            colBH = new DataGridViewTextBoxColumn();
            colRoomNo = new DataGridViewTextBoxColumn();
            colOccupant = new DataGridViewTextBoxColumn();
            colFrom = new DataGridViewTextBoxColumn();
            colTo = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            pnlEditor = new Panel();
            lblEditorTitle = new Label();
            btnCloseEditor = new Button();
            lblEditBoardingHouse = new Label();
            cbEditorBoardingHouse = new ComboBox();
            lblEditRoom = new Label();
            cbEditorRoom = new ComboBox();
            lblEditOccupant = new Label();
            cbEditorOccupant = new ComboBox();
            lblEditStatus = new Label();
            cbEditorStatus = new ComboBox();
            lblEditFrom = new Label();
            dtFrom = new DateTimePicker();
            lblEditTo = new Label();
            dtTo = new DateTimePicker();
            lblEditNotes = new Label();
            txtEditorNotes = new TextBox();
            lblConflictWarning = new Label();
            btnAddUpdate = new Button();
            btnVoidSelected = new Button();
            btnEditorClear = new Button();
            pnlHeader.SuspendLayout();
            pnlMain.SuspendLayout();
            pnlRight.SuspendLayout();
            gbLogs.SuspendLayout();
            ((ISupportInitialize)dgvAuditLogs).BeginInit();
            gbSummary.SuspendLayout();
            gbReservations.SuspendLayout();
            ((ISupportInitialize)dgvReservations).BeginInit();
            pnlEditor.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(label1);
            pnlHeader.Controls.Add(btnNewReservation);
            pnlHeader.Controls.Add(btnRefresh);
            pnlHeader.Controls.Add(btnExport);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(12, 10, 12, 10);
            pnlHeader.Size = new Size(1677, 62);
            pnlHeader.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 19F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 8);
            label1.Name = "label1";
            label1.Size = new Size(340, 45);
            label1.TabIndex = 0;
            label1.Text = "Manage Reservations";
            // 
            // btnNewReservation
            // 
            btnNewReservation.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnNewReservation.BackColor = Color.FromArgb(41, 128, 185);
            btnNewReservation.FlatStyle = FlatStyle.Flat;
            btnNewReservation.ForeColor = Color.White;
            btnNewReservation.Location = new Point(1242, 20);
            btnNewReservation.Name = "btnNewReservation";
            btnNewReservation.Size = new Size(172, 34);
            btnNewReservation.TabIndex = 1;
            btnNewReservation.Text = "New Reservation";
            btnNewReservation.UseVisualStyleBackColor = false;
            btnNewReservation.Click += btnNewReservation_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Location = new Point(1420, 20);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(110, 34);
            btnRefresh.TabIndex = 2;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnExport
            // 
            btnExport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExport.FlatStyle = FlatStyle.Flat;
            btnExport.Location = new Point(1536, 20);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(110, 34);
            btnExport.TabIndex = 3;
            btnExport.Text = "Export";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += btnExport_Click;
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.FromArgb(245, 246, 248);
            pnlMain.Controls.Add(pnlRight);
            pnlMain.Controls.Add(gbReservations);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 62);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(10);
            pnlMain.Size = new Size(1677, 913);
            pnlMain.TabIndex = 1;
            // 
            // pnlRight
            // 
            pnlRight.Controls.Add(gbLogs);
            pnlRight.Controls.Add(gbSummary);
            pnlRight.Dock = DockStyle.Fill;
            pnlRight.Location = new Point(990, 10);
            pnlRight.Name = "pnlRight";
            pnlRight.Padding = new Padding(10, 0, 0, 0);
            pnlRight.Size = new Size(677, 893);
            pnlRight.TabIndex = 1;
            // 
            // gbLogs
            // 
            gbLogs.Controls.Add(dgvAuditLogs);
            gbLogs.Location = new Point(10, 470);
            gbLogs.Name = "gbLogs";
            gbLogs.Padding = new Padding(10);
            gbLogs.Size = new Size(667, 378);
            gbLogs.TabIndex = 1;
            gbLogs.TabStop = false;
            gbLogs.Text = "Activity / Audit Logs";
            // 
            // dgvAuditLogs
            // 
            dgvAuditLogs.AllowUserToAddRows = false;
            dgvAuditLogs.AllowUserToDeleteRows = false;
            dgvAuditLogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAuditLogs.BackgroundColor = Color.White;
            dgvAuditLogs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAuditLogs.Columns.AddRange(new DataGridViewColumn[] { colLogDate, colLogAction, colLogUser, colLogDetails });
            dgvAuditLogs.Dock = DockStyle.Fill;
            dgvAuditLogs.Location = new Point(10, 33);
            dgvAuditLogs.MultiSelect = false;
            dgvAuditLogs.Name = "dgvAuditLogs";
            dgvAuditLogs.ReadOnly = true;
            dgvAuditLogs.RowHeadersVisible = false;
            dgvAuditLogs.RowHeadersWidth = 51;
            dgvAuditLogs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAuditLogs.Size = new Size(647, 335);
            dgvAuditLogs.TabIndex = 0;
            dgvAuditLogs.CellClick += dgvAuditLogs_CellClick;
            // 
            // colLogDate
            // 
            colLogDate.HeaderText = "Date";
            colLogDate.MinimumWidth = 6;
            colLogDate.Name = "colLogDate";
            colLogDate.ReadOnly = true;
            // 
            // colLogAction
            // 
            colLogAction.HeaderText = "Action";
            colLogAction.MinimumWidth = 6;
            colLogAction.Name = "colLogAction";
            colLogAction.ReadOnly = true;
            // 
            // colLogUser
            // 
            colLogUser.HeaderText = "User";
            colLogUser.MinimumWidth = 6;
            colLogUser.Name = "colLogUser";
            colLogUser.ReadOnly = true;
            // 
            // colLogDetails
            // 
            colLogDetails.HeaderText = "Details";
            colLogDetails.MinimumWidth = 6;
            colLogDetails.Name = "colLogDetails";
            colLogDetails.ReadOnly = true;
            // 
            // gbSummary
            // 
            gbSummary.Controls.Add(lblSelectedId);
            gbSummary.Controls.Add(lblSelectedStatusBadge);
            gbSummary.Controls.Add(lblOccName);
            gbSummary.Controls.Add(lblOccContact);
            gbSummary.Controls.Add(lblRoomNo);
            gbSummary.Controls.Add(lblBHName);
            gbSummary.Controls.Add(lblSelectedNotesTitle);
            gbSummary.Controls.Add(txtSelectedNotes);
            gbSummary.Controls.Add(btnApprove);
            gbSummary.Controls.Add(btnReject);
            gbSummary.Controls.Add(btnCancelReservation);
            gbSummary.Controls.Add(btnCheckIn);
            gbSummary.Controls.Add(btnConvertToRental);
            gbSummary.Controls.Add(btnEditReservation);
            gbSummary.Controls.Add(lblAlertsTitle);
            gbSummary.Controls.Add(txtAlerts);
            gbSummary.Location = new Point(7, 15);
            gbSummary.MinimumSize = new Size(400, 300);
            gbSummary.Name = "gbSummary";
            gbSummary.Padding = new Padding(10);
            gbSummary.Size = new Size(667, 470);
            gbSummary.TabIndex = 0;
            gbSummary.TabStop = false;
            gbSummary.Text = "Reservation Summary";
            // 
            // lblSelectedId
            // 
            lblSelectedId.AutoSize = true;
            lblSelectedId.Location = new Point(16, 34);
            lblSelectedId.Name = "lblSelectedId";
            lblSelectedId.Size = new Size(125, 23);
            lblSelectedId.TabIndex = 0;
            lblSelectedId.Text = "Reservation # -";
            // 
            // lblSelectedStatusBadge
            // 
            lblSelectedStatusBadge.BackColor = Color.SlateGray;
            lblSelectedStatusBadge.ForeColor = Color.White;
            lblSelectedStatusBadge.Location = new Point(182, 30);
            lblSelectedStatusBadge.Name = "lblSelectedStatusBadge";
            lblSelectedStatusBadge.Padding = new Padding(6, 4, 6, 4);
            lblSelectedStatusBadge.Size = new Size(122, 28);
            lblSelectedStatusBadge.TabIndex = 1;
            lblSelectedStatusBadge.Text = "-";
            lblSelectedStatusBadge.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblOccName
            // 
            lblOccName.Location = new Point(16, 70);
            lblOccName.Name = "lblOccName";
            lblOccName.Size = new Size(620, 20);
            lblOccName.TabIndex = 2;
            lblOccName.Text = "Occupant: -";
            // 
            // lblOccContact
            // 
            lblOccContact.Location = new Point(16, 94);
            lblOccContact.Name = "lblOccContact";
            lblOccContact.Size = new Size(620, 20);
            lblOccContact.TabIndex = 3;
            lblOccContact.Text = "Contact: -";
            // 
            // lblRoomNo
            // 
            lblRoomNo.Location = new Point(16, 118);
            lblRoomNo.Name = "lblRoomNo";
            lblRoomNo.Size = new Size(620, 20);
            lblRoomNo.TabIndex = 4;
            lblRoomNo.Text = "Room: -";
            // 
            // lblBHName
            // 
            lblBHName.Location = new Point(16, 142);
            lblBHName.Name = "lblBHName";
            lblBHName.Size = new Size(620, 20);
            lblBHName.TabIndex = 5;
            lblBHName.Text = "Boarding House: -";
            // 
            // lblSelectedNotesTitle
            // 
            lblSelectedNotesTitle.AutoSize = true;
            lblSelectedNotesTitle.Location = new Point(16, 170);
            lblSelectedNotesTitle.Name = "lblSelectedNotesTitle";
            lblSelectedNotesTitle.Size = new Size(55, 23);
            lblSelectedNotesTitle.TabIndex = 6;
            lblSelectedNotesTitle.Text = "Notes";
            // 
            // txtSelectedNotes
            // 
            txtSelectedNotes.Location = new Point(16, 188);
            txtSelectedNotes.Multiline = true;
            txtSelectedNotes.Name = "txtSelectedNotes";
            txtSelectedNotes.ReadOnly = true;
            txtSelectedNotes.ScrollBars = ScrollBars.Vertical;
            txtSelectedNotes.Size = new Size(630, 88);
            txtSelectedNotes.TabIndex = 7;
            // 
            // btnApprove
            // 
            btnApprove.FlatStyle = FlatStyle.Flat;
            btnApprove.Location = new Point(16, 286);
            btnApprove.Name = "btnApprove";
            btnApprove.Size = new Size(86, 30);
            btnApprove.TabIndex = 8;
            btnApprove.Text = "Approve";
            btnApprove.UseVisualStyleBackColor = true;
            btnApprove.Click += btnApprove_Click;
            // 
            // btnReject
            // 
            btnReject.FlatStyle = FlatStyle.Flat;
            btnReject.Location = new Point(108, 286);
            btnReject.Name = "btnReject";
            btnReject.Size = new Size(86, 30);
            btnReject.TabIndex = 9;
            btnReject.Text = "Reject";
            btnReject.UseVisualStyleBackColor = true;
            btnReject.Click += btnReject_Click;
            // 
            // btnCancelReservation
            // 
            btnCancelReservation.FlatStyle = FlatStyle.Flat;
            btnCancelReservation.Location = new Point(200, 286);
            btnCancelReservation.Name = "btnCancelReservation";
            btnCancelReservation.Size = new Size(132, 30);
            btnCancelReservation.TabIndex = 10;
            btnCancelReservation.Text = "Cancel Reservation";
            btnCancelReservation.UseVisualStyleBackColor = true;
            btnCancelReservation.Click += btnCancelReservation_Click;
            // 
            // btnCheckIn
            // 
            btnCheckIn.FlatStyle = FlatStyle.Flat;
            btnCheckIn.Location = new Point(338, 286);
            btnCheckIn.Name = "btnCheckIn";
            btnCheckIn.Size = new Size(86, 30);
            btnCheckIn.TabIndex = 11;
            btnCheckIn.Text = "Check In";
            btnCheckIn.UseVisualStyleBackColor = true;
            btnCheckIn.Click += btnCheckIn_Click;
            // 
            // btnConvertToRental
            // 
            btnConvertToRental.FlatStyle = FlatStyle.Flat;
            btnConvertToRental.Location = new Point(430, 286);
            btnConvertToRental.Name = "btnConvertToRental";
            btnConvertToRental.Size = new Size(120, 30);
            btnConvertToRental.TabIndex = 12;
            btnConvertToRental.Text = "Convert";
            btnConvertToRental.UseVisualStyleBackColor = true;
            btnConvertToRental.Click += btnConvertToRental_Click;
            // 
            // btnEditReservation
            // 
            btnEditReservation.FlatStyle = FlatStyle.Flat;
            btnEditReservation.Location = new Point(556, 286);
            btnEditReservation.Name = "btnEditReservation";
            btnEditReservation.Size = new Size(90, 30);
            btnEditReservation.TabIndex = 13;
            btnEditReservation.Text = "Edit";
            btnEditReservation.UseVisualStyleBackColor = true;
            btnEditReservation.Click += btnEditReservation_Click;
            // 
            // lblAlertsTitle
            // 
            lblAlertsTitle.AutoSize = true;
            lblAlertsTitle.Location = new Point(16, 328);
            lblAlertsTitle.Name = "lblAlertsTitle";
            lblAlertsTitle.Size = new Size(53, 23);
            lblAlertsTitle.TabIndex = 14;
            lblAlertsTitle.Text = "Alerts";
            // 
            // txtAlerts
            // 
            txtAlerts.BackColor = Color.FromArgb(255, 249, 196);
            txtAlerts.Location = new Point(16, 346);
            txtAlerts.Multiline = true;
            txtAlerts.Name = "txtAlerts";
            txtAlerts.ReadOnly = true;
            txtAlerts.ScrollBars = ScrollBars.Vertical;
            txtAlerts.Size = new Size(630, 102);
            txtAlerts.TabIndex = 15;
            // 
            // gbReservations
            // 
            gbReservations.Controls.Add(pnlEditor);
            gbReservations.Controls.Add(lblBoardingHouse);
            gbReservations.Controls.Add(cbBoardingHouse);
            gbReservations.Controls.Add(lblRoom);
            gbReservations.Controls.Add(cbRoom);
            gbReservations.Controls.Add(lblStatusFilter);
            gbReservations.Controls.Add(cbStatusFilter);
            gbReservations.Controls.Add(lblFrom);
            gbReservations.Controls.Add(dtFilterFrom);
            gbReservations.Controls.Add(lblTo);
            gbReservations.Controls.Add(dtFilterTo);
            gbReservations.Controls.Add(txtSearch);
            gbReservations.Controls.Add(btnSearch);
            gbReservations.Controls.Add(btnClear);
            gbReservations.Controls.Add(dgvReservations);
            gbReservations.Dock = DockStyle.Left;
            gbReservations.Location = new Point(10, 10);
            gbReservations.MinimumSize = new Size(780, 300);
            gbReservations.Name = "gbReservations";
            gbReservations.Padding = new Padding(10);
            gbReservations.Size = new Size(980, 893);
            gbReservations.TabIndex = 0;
            gbReservations.TabStop = false;
            gbReservations.Text = "Reservation Details";
            // 
            // lblBoardingHouse
            // 
            lblBoardingHouse.AutoSize = true;
            lblBoardingHouse.Location = new Point(14, 36);
            lblBoardingHouse.Name = "lblBoardingHouse";
            lblBoardingHouse.Size = new Size(136, 23);
            lblBoardingHouse.TabIndex = 0;
            lblBoardingHouse.Text = "Boarding House:";
            // 
            // cbBoardingHouse
            // 
            cbBoardingHouse.DropDownStyle = ComboBoxStyle.DropDownList;
            cbBoardingHouse.FormattingEnabled = true;
            cbBoardingHouse.Location = new Point(154, 33);
            cbBoardingHouse.Name = "cbBoardingHouse";
            cbBoardingHouse.Size = new Size(190, 31);
            cbBoardingHouse.TabIndex = 1;
            cbBoardingHouse.SelectedIndexChanged += cbBoardingHouse_SelectedIndexChanged;
            // 
            // lblRoom
            // 
            lblRoom.AutoSize = true;
            lblRoom.Location = new Point(362, 36);
            lblRoom.Name = "lblRoom";
            lblRoom.Size = new Size(59, 23);
            lblRoom.TabIndex = 2;
            lblRoom.Text = "Room:";
            // 
            // cbRoom
            // 
            cbRoom.DropDownStyle = ComboBoxStyle.DropDownList;
            cbRoom.FormattingEnabled = true;
            cbRoom.Location = new Point(426, 33);
            cbRoom.Name = "cbRoom";
            cbRoom.Size = new Size(150, 31);
            cbRoom.TabIndex = 3;
            cbRoom.SelectedIndexChanged += cbRoom_SelectedIndexChanged;
            // 
            // lblStatusFilter
            // 
            lblStatusFilter.AutoSize = true;
            lblStatusFilter.Location = new Point(592, 36);
            lblStatusFilter.Name = "lblStatusFilter";
            lblStatusFilter.Size = new Size(56, 23);
            lblStatusFilter.TabIndex = 4;
            lblStatusFilter.Text = "Status";
            // 
            // cbStatusFilter
            // 
            cbStatusFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cbStatusFilter.FormattingEnabled = true;
            cbStatusFilter.Items.AddRange(new object[] { "ALL", "PENDING", "APPROVED", "REJECTED", "CANCELLED", "EXPIRED", "CHECKED_IN" });
            cbStatusFilter.Location = new Point(654, 33);
            cbStatusFilter.Name = "cbStatusFilter";
            cbStatusFilter.Size = new Size(160, 31);
            cbStatusFilter.TabIndex = 5;
            cbStatusFilter.SelectedIndexChanged += cbStatusFilter_SelectedIndexChanged;
            // 
            // lblFrom
            // 
            lblFrom.AutoSize = true;
            lblFrom.Location = new Point(14, 75);
            lblFrom.Name = "lblFrom";
            lblFrom.Size = new Size(49, 23);
            lblFrom.TabIndex = 6;
            lblFrom.Text = "From";
            // 
            // dtFilterFrom
            // 
            dtFilterFrom.CustomFormat = "yyyy-MM-dd";
            dtFilterFrom.Format = DateTimePickerFormat.Custom;
            dtFilterFrom.Location = new Point(69, 70);
            dtFilterFrom.Name = "dtFilterFrom";
            dtFilterFrom.Size = new Size(130, 30);
            dtFilterFrom.TabIndex = 7;
            dtFilterFrom.ValueChanged += dtFilterFrom_ValueChanged;
            // 
            // lblTo
            // 
            lblTo.AutoSize = true;
            lblTo.Location = new Point(210, 75);
            lblTo.Name = "lblTo";
            lblTo.Size = new Size(27, 23);
            lblTo.TabIndex = 8;
            lblTo.Text = "To";
            // 
            // dtFilterTo
            // 
            dtFilterTo.CustomFormat = "yyyy-MM-dd";
            dtFilterTo.Format = DateTimePickerFormat.Custom;
            dtFilterTo.Location = new Point(243, 70);
            dtFilterTo.Name = "dtFilterTo";
            dtFilterTo.Size = new Size(130, 30);
            dtFilterTo.TabIndex = 9;
            dtFilterTo.ValueChanged += dtFilterTo_ValueChanged;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(379, 71);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search occupant, room no, reservation id...";
            txtSearch.Size = new Size(352, 30);
            txtSearch.TabIndex = 10;
            // 
            // btnSearch
            // 
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Location = new Point(737, 70);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(98, 32);
            btnSearch.TabIndex = 11;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnClear
            // 
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Location = new Point(841, 70);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(98, 32);
            btnClear.TabIndex = 12;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // dgvReservations
            // 
            dgvReservations.AllowUserToAddRows = false;
            dgvReservations.AllowUserToDeleteRows = false;
            dgvReservations.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReservations.BackgroundColor = Color.White;
            dgvReservations.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReservations.Columns.AddRange(new DataGridViewColumn[] { colResId, colBH, colRoomNo, colOccupant, colFrom, colTo, colStatus });
            dgvReservations.Location = new Point(13, 109);
            dgvReservations.MultiSelect = false;
            dgvReservations.Name = "dgvReservations";
            dgvReservations.ReadOnly = true;
            dgvReservations.RowHeadersVisible = false;
            dgvReservations.RowHeadersWidth = 51;
            dgvReservations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReservations.Size = new Size(954, 751);
            dgvReservations.TabIndex = 14;
            // 
            // colResId
            // 
            colResId.HeaderText = "ID";
            colResId.MinimumWidth = 6;
            colResId.Name = "colResId";
            colResId.ReadOnly = true;
            // 
            // colBH
            // 
            colBH.HeaderText = "Boarding House";
            colBH.MinimumWidth = 6;
            colBH.Name = "colBH";
            colBH.ReadOnly = true;
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
            // colFrom
            // 
            colFrom.HeaderText = "From";
            colFrom.MinimumWidth = 6;
            colFrom.Name = "colFrom";
            colFrom.ReadOnly = true;
            // 
            // colTo
            // 
            colTo.HeaderText = "To";
            colTo.MinimumWidth = 6;
            colTo.Name = "colTo";
            colTo.ReadOnly = true;
            // 
            // colStatus
            // 
            colStatus.HeaderText = "Status";
            colStatus.MinimumWidth = 6;
            colStatus.Name = "colStatus";
            colStatus.ReadOnly = true;
            // 
            // pnlEditor
            // 
            pnlEditor.Anchor = AnchorStyles.None;
            pnlEditor.BackColor = Color.White;
            pnlEditor.BorderStyle = BorderStyle.FixedSingle;
            pnlEditor.Controls.Add(lblEditorTitle);
            pnlEditor.Controls.Add(btnCloseEditor);
            pnlEditor.Controls.Add(lblEditBoardingHouse);
            pnlEditor.Controls.Add(cbEditorBoardingHouse);
            pnlEditor.Controls.Add(lblEditRoom);
            pnlEditor.Controls.Add(cbEditorRoom);
            pnlEditor.Controls.Add(lblEditOccupant);
            pnlEditor.Controls.Add(cbEditorOccupant);
            pnlEditor.Controls.Add(lblEditStatus);
            pnlEditor.Controls.Add(cbEditorStatus);
            pnlEditor.Controls.Add(lblEditFrom);
            pnlEditor.Controls.Add(dtFrom);
            pnlEditor.Controls.Add(lblEditTo);
            pnlEditor.Controls.Add(dtTo);
            pnlEditor.Controls.Add(lblEditNotes);
            pnlEditor.Controls.Add(txtEditorNotes);
            pnlEditor.Controls.Add(lblConflictWarning);
            pnlEditor.Controls.Add(btnAddUpdate);
            pnlEditor.Controls.Add(btnVoidSelected);
            pnlEditor.Controls.Add(btnEditorClear);
            pnlEditor.Location = new Point(379, 62);
            pnlEditor.Name = "pnlEditor";
            pnlEditor.Size = new Size(560, 460);
            pnlEditor.TabIndex = 2;
            pnlEditor.Visible = false;
            // 
            // lblEditorTitle
            // 
            lblEditorTitle.AutoSize = true;
            lblEditorTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblEditorTitle.Location = new Point(14, 10);
            lblEditorTitle.Name = "lblEditorTitle";
            lblEditorTitle.Size = new Size(188, 28);
            lblEditorTitle.TabIndex = 0;
            lblEditorTitle.Text = "Reservation Editor";
            // 
            // btnCloseEditor
            // 
            btnCloseEditor.BackColor = Color.FromArgb(220, 53, 69);
            btnCloseEditor.FlatStyle = FlatStyle.Flat;
            btnCloseEditor.ForeColor = Color.White;
            btnCloseEditor.Location = new Point(521, -1);
            btnCloseEditor.Name = "btnCloseEditor";
            btnCloseEditor.Size = new Size(38, 32);
            btnCloseEditor.TabIndex = 1;
            btnCloseEditor.Text = "X";
            btnCloseEditor.UseVisualStyleBackColor = false;
            // 
            // lblEditBoardingHouse
            // 
            lblEditBoardingHouse.AutoSize = true;
            lblEditBoardingHouse.Location = new Point(20, 52);
            lblEditBoardingHouse.Name = "lblEditBoardingHouse";
            lblEditBoardingHouse.Size = new Size(132, 23);
            lblEditBoardingHouse.TabIndex = 2;
            lblEditBoardingHouse.Text = "Boarding House";
            // 
            // cbEditorBoardingHouse
            // 
            cbEditorBoardingHouse.DropDownStyle = ComboBoxStyle.DropDownList;
            cbEditorBoardingHouse.FormattingEnabled = true;
            cbEditorBoardingHouse.Location = new Point(20, 70);
            cbEditorBoardingHouse.Name = "cbEditorBoardingHouse";
            cbEditorBoardingHouse.Size = new Size(250, 31);
            cbEditorBoardingHouse.TabIndex = 3;
            cbEditorBoardingHouse.SelectedIndexChanged += cbEditorBoardingHouse_SelectedIndexChanged;
            // 
            // lblEditRoom
            // 
            lblEditRoom.AutoSize = true;
            lblEditRoom.Location = new Point(290, 52);
            lblEditRoom.Name = "lblEditRoom";
            lblEditRoom.Size = new Size(55, 23);
            lblEditRoom.TabIndex = 4;
            lblEditRoom.Text = "Room";
            // 
            // cbEditorRoom
            // 
            cbEditorRoom.DropDownStyle = ComboBoxStyle.DropDownList;
            cbEditorRoom.FormattingEnabled = true;
            cbEditorRoom.Location = new Point(290, 70);
            cbEditorRoom.Name = "cbEditorRoom";
            cbEditorRoom.Size = new Size(250, 31);
            cbEditorRoom.TabIndex = 5;
            cbEditorRoom.SelectedIndexChanged += cbEditorRoom_SelectedIndexChanged;
            // 
            // lblEditOccupant
            // 
            lblEditOccupant.AutoSize = true;
            lblEditOccupant.Location = new Point(20, 106);
            lblEditOccupant.Name = "lblEditOccupant";
            lblEditOccupant.Size = new Size(84, 23);
            lblEditOccupant.TabIndex = 6;
            lblEditOccupant.Text = "Occupant";
            // 
            // cbEditorOccupant
            // 
            cbEditorOccupant.DropDownStyle = ComboBoxStyle.DropDownList;
            cbEditorOccupant.FormattingEnabled = true;
            cbEditorOccupant.Location = new Point(20, 124);
            cbEditorOccupant.Name = "cbEditorOccupant";
            cbEditorOccupant.Size = new Size(250, 31);
            cbEditorOccupant.TabIndex = 7;
            cbEditorOccupant.SelectedIndexChanged += cbEditorOccupant_SelectedIndexChanged;
            // 
            // lblEditStatus
            // 
            lblEditStatus.AutoSize = true;
            lblEditStatus.Location = new Point(290, 106);
            lblEditStatus.Name = "lblEditStatus";
            lblEditStatus.Size = new Size(56, 23);
            lblEditStatus.TabIndex = 8;
            lblEditStatus.Text = "Status";
            // 
            // cbEditorStatus
            // 
            cbEditorStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cbEditorStatus.FormattingEnabled = true;
            cbEditorStatus.Items.AddRange(new object[] { "PENDING", "APPROVED", "REJECTED", "CANCELLED", "EXPIRED", "CHECKED_IN" });
            cbEditorStatus.Location = new Point(290, 124);
            cbEditorStatus.Name = "cbEditorStatus";
            cbEditorStatus.Size = new Size(250, 31);
            cbEditorStatus.TabIndex = 9;
            cbEditorStatus.SelectedIndexChanged += cbEditorStatus_SelectedIndexChanged;
            // 
            // lblEditFrom
            // 
            lblEditFrom.AutoSize = true;
            lblEditFrom.Location = new Point(20, 160);
            lblEditFrom.Name = "lblEditFrom";
            lblEditFrom.Size = new Size(49, 23);
            lblEditFrom.TabIndex = 10;
            lblEditFrom.Text = "From";
            // 
            // dtFrom
            // 
            dtFrom.CustomFormat = "yyyy-MM-dd HH:mm";
            dtFrom.Format = DateTimePickerFormat.Custom;
            dtFrom.Location = new Point(20, 178);
            dtFrom.Name = "dtFrom";
            dtFrom.ShowUpDown = true;
            dtFrom.Size = new Size(250, 30);
            dtFrom.TabIndex = 11;
            dtFrom.ValueChanged += dtFrom_ValueChanged;
            // 
            // lblEditTo
            // 
            lblEditTo.AutoSize = true;
            lblEditTo.Location = new Point(290, 160);
            lblEditTo.Name = "lblEditTo";
            lblEditTo.Size = new Size(27, 23);
            lblEditTo.TabIndex = 12;
            lblEditTo.Text = "To";
            // 
            // dtTo
            // 
            dtTo.CustomFormat = "yyyy-MM-dd HH:mm";
            dtTo.Format = DateTimePickerFormat.Custom;
            dtTo.Location = new Point(290, 178);
            dtTo.Name = "dtTo";
            dtTo.ShowUpDown = true;
            dtTo.Size = new Size(250, 30);
            dtTo.TabIndex = 13;
            dtTo.ValueChanged += dtTo_ValueChanged;
            // 
            // lblEditNotes
            // 
            lblEditNotes.AutoSize = true;
            lblEditNotes.Location = new Point(20, 214);
            lblEditNotes.Name = "lblEditNotes";
            lblEditNotes.Size = new Size(55, 23);
            lblEditNotes.TabIndex = 14;
            lblEditNotes.Text = "Notes";
            // 
            // txtEditorNotes
            // 
            txtEditorNotes.Location = new Point(20, 232);
            txtEditorNotes.MaxLength = 255;
            txtEditorNotes.Multiline = true;
            txtEditorNotes.Name = "txtEditorNotes";
            txtEditorNotes.ScrollBars = ScrollBars.Vertical;
            txtEditorNotes.Size = new Size(520, 112);
            txtEditorNotes.TabIndex = 15;
            // 
            // lblConflictWarning
            // 
            lblConflictWarning.AutoSize = true;
            lblConflictWarning.ForeColor = Color.Red;
            lblConflictWarning.Location = new Point(20, 352);
            lblConflictWarning.Name = "lblConflictWarning";
            lblConflictWarning.Size = new Size(455, 23);
            lblConflictWarning.TabIndex = 16;
            lblConflictWarning.Text = "Conflict detected: room already reserved for selected time.";
            lblConflictWarning.Visible = false;
            // 
            // btnAddUpdate
            // 
            btnAddUpdate.BackColor = Color.FromArgb(40, 167, 69);
            btnAddUpdate.FlatStyle = FlatStyle.Flat;
            btnAddUpdate.ForeColor = Color.White;
            btnAddUpdate.Location = new Point(20, 388);
            btnAddUpdate.Name = "btnAddUpdate";
            btnAddUpdate.Size = new Size(140, 34);
            btnAddUpdate.TabIndex = 17;
            btnAddUpdate.Text = "Add / Update";
            btnAddUpdate.UseVisualStyleBackColor = false;
            btnAddUpdate.Click += btnAddUpdate_Click;
            // 
            // btnVoidSelected
            // 
            btnVoidSelected.FlatStyle = FlatStyle.Flat;
            btnVoidSelected.Location = new Point(166, 388);
            btnVoidSelected.Name = "btnVoidSelected";
            btnVoidSelected.Size = new Size(140, 34);
            btnVoidSelected.TabIndex = 18;
            btnVoidSelected.Text = "Cancel Selected";
            btnVoidSelected.UseVisualStyleBackColor = true;
            btnVoidSelected.Click += btnVoidSelected_Click;
            // 
            // btnEditorClear
            // 
            btnEditorClear.FlatStyle = FlatStyle.Flat;
            btnEditorClear.Location = new Point(312, 388);
            btnEditorClear.Name = "btnEditorClear";
            btnEditorClear.Size = new Size(120, 34);
            btnEditorClear.TabIndex = 19;
            btnEditorClear.Text = "Clear";
            btnEditorClear.UseVisualStyleBackColor = true;
            btnEditorClear.Click += btnEditorClear_Click;
            // 
            // ReservationsView
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 246, 248);
            Controls.Add(pnlMain);
            Controls.Add(pnlHeader);
            Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "ReservationsView";
            Size = new Size(1677, 975);
            Load += ReservationsView_Load_1;
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlMain.ResumeLayout(false);
            pnlRight.ResumeLayout(false);
            gbLogs.ResumeLayout(false);
            ((ISupportInitialize)dgvAuditLogs).EndInit();
            gbSummary.ResumeLayout(false);
            gbSummary.PerformLayout();
            gbReservations.ResumeLayout(false);
            gbReservations.PerformLayout();
            ((ISupportInitialize)dgvReservations).EndInit();
            pnlEditor.ResumeLayout(false);
            pnlEditor.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label label1;
        private Button btnNewReservation;
        private Button btnRefresh;
        private Button btnExport;

        private Panel pnlMain;
        private GroupBox gbReservations;
        private Label lblBoardingHouse;
        private ComboBox cbBoardingHouse;
        private Label lblRoom;
        private ComboBox cbRoom;
        private Label lblStatusFilter;
        private ComboBox cbStatusFilter;
        private Label lblFrom;
        private DateTimePicker dtFilterFrom;
        private Label lblTo;
        private DateTimePicker dtFilterTo;
        private TextBox txtSearch;
        private Button btnSearch;
        private Button btnClear;
        private DataGridView dgvReservations;
        private DataGridViewTextBoxColumn colResId;
        private DataGridViewTextBoxColumn colBH;
        private DataGridViewTextBoxColumn colRoomNo;
        private DataGridViewTextBoxColumn colOccupant;
        private DataGridViewTextBoxColumn colFrom;
        private DataGridViewTextBoxColumn colTo;
        private DataGridViewTextBoxColumn colStatus;

        private Panel pnlRight;
        private GroupBox gbSummary;
        private Label lblSelectedId;
        private Label lblSelectedStatusBadge;
        private Label lblOccName;
        private Label lblOccContact;
        private Label lblRoomNo;
        private Label lblBHName;
        private Label lblSelectedNotesTitle;
        private TextBox txtSelectedNotes;
        private Button btnApprove;
        private Button btnReject;
        private Button btnCancelReservation;
        private Button btnCheckIn;
        private Button btnConvertToRental;
        private Button btnEditReservation;
        private Label lblAlertsTitle;
        private TextBox txtAlerts;

        private GroupBox gbLogs;
        private DataGridView dgvAuditLogs;
        private DataGridViewTextBoxColumn colLogDate;
        private DataGridViewTextBoxColumn colLogAction;
        private DataGridViewTextBoxColumn colLogUser;
        private DataGridViewTextBoxColumn colLogDetails;

        private Panel pnlEditor;
        private Label lblEditorTitle;
        private Button btnCloseEditor;
        private Label lblEditBoardingHouse;
        private ComboBox cbEditorBoardingHouse;
        private Label lblEditRoom;
        private ComboBox cbEditorRoom;
        private Label lblEditOccupant;
        private ComboBox cbEditorOccupant;
        private Label lblEditStatus;
        private ComboBox cbEditorStatus;
        private Label lblEditFrom;
        private DateTimePicker dtFrom;
        private Label lblEditTo;
        private DateTimePicker dtTo;
        private Label lblEditNotes;
        private TextBox txtEditorNotes;
        private Label lblConflictWarning;
        private Button btnAddUpdate;
        private Button btnVoidSelected;
        private Button btnEditorClear;
    }
}
