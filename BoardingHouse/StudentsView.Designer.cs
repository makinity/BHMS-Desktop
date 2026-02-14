namespace BoardingHouse
{
    partial class StudentsView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnSnapshotOpenPayments = new Button();
            pnlSnapshotActions = new Panel();
            lblSnapshotHint = new Label();
            btnSnapshotViewRoom = new Button();
            btnSnapshotRefresh = new Button();
            studentsSnapshotPanel = new Panel();
            pnlSnapshotCard = new Panel();
            lblSnapshotStatusBadge = new Label();
            lblFieldTenant = new Label();
            lblSnapshotStudent = new Label();
            lblFieldTenantId = new Label();
            lblSnapshotStudentId = new Label();
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
            label13 = new Label();
            label17 = new Label();
            label14 = new Label();
            dataGridView1 = new DataGridView();
            paymentHistoryPanel = new Panel();
            dgvStudents = new DataGridView();
            topBar = new Panel();
            lblStatus = new Label();
            lblBh = new Label();
            btnSearch = new Button();
            cbStatusFilter = new ComboBox();
            txtSearch = new TextBox();
            cbBoardingHouses = new ComboBox();
            addStudentsBtn = new Button();
            addStudentsModal = new Panel();
            cancelTenantRegister = new Button();
            registerTenantBtn = new Button();
            txtStudentNo = new TextBox();
            labelStudentNo = new Label();
            studentFirstnameTxt = new TextBox();
            addStudentCloseBtn = new Button();
            studentAddressTxt = new TextBox();
            label16 = new Label();
            studentEmergencyContactTxt = new TextBox();
            label11 = new Label();
            studentEmergencyNameTxt = new TextBox();
            label10 = new Label();
            studentContactTxt = new TextBox();
            label7 = new Label();
            studentEmailTxt = new TextBox();
            label8 = new Label();
            studentMiddleNameTxt = new TextBox();
            label5 = new Label();
            label9 = new Label();
            studentLastNameTxt = new TextBox();
            label12 = new Label();
            registrationOpenCameraBtn = new Button();
            browseProfileBtn = new Button();
            profilePathTxt = new TextBox();
            addStudentImg = new PictureBox();
            addTenantCloseBtn = new Button();
            totalStudents = new Label();
            label15 = new Label();
            detailsModal = new Panel();
            lblStudentTitle = new Label();
            grpDetails = new GroupBox();
            ViewRoomBtn = new Label();
            detailsOpenCameraBtn = new Button();
            detailsBrowseProfileBtn = new Button();
            details_profilePathTxt = new TextBox();
            detailsStudenttImg = new PictureBox();
            endRentalBtn = new Button();
            cbDetailsRoom = new ComboBox();
            label4 = new Label();
            studentUpdateBtn = new Button();
            studentDeleteBtn = new Button();
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
            cbOccType = new ComboBox();
            label18 = new Label();
            pnlSnapshotActions.SuspendLayout();
            studentsSnapshotPanel.SuspendLayout();
            pnlSnapshotCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            paymentHistoryPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStudents).BeginInit();
            topBar.SuspendLayout();
            addStudentsModal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)addStudentImg).BeginInit();
            detailsModal.SuspendLayout();
            grpDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)detailsStudenttImg).BeginInit();
            SuspendLayout();
            // 
            // btnSnapshotOpenPayments
            // 
            btnSnapshotOpenPayments.Location = new Point(20, 60);
            btnSnapshotOpenPayments.Name = "btnSnapshotOpenPayments";
            btnSnapshotOpenPayments.Size = new Size(180, 45);
            btnSnapshotOpenPayments.TabIndex = 1;
            btnSnapshotOpenPayments.Text = "Open Payments";
            btnSnapshotOpenPayments.UseVisualStyleBackColor = true;
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
            // btnSnapshotViewRoom
            // 
            btnSnapshotViewRoom.Location = new Point(220, 60);
            btnSnapshotViewRoom.Name = "btnSnapshotViewRoom";
            btnSnapshotViewRoom.Size = new Size(180, 45);
            btnSnapshotViewRoom.TabIndex = 2;
            btnSnapshotViewRoom.Text = "View Room";
            btnSnapshotViewRoom.UseVisualStyleBackColor = true;
            // 
            // btnSnapshotRefresh
            // 
            btnSnapshotRefresh.Location = new Point(420, 60);
            btnSnapshotRefresh.Name = "btnSnapshotRefresh";
            btnSnapshotRefresh.Size = new Size(180, 45);
            btnSnapshotRefresh.TabIndex = 3;
            btnSnapshotRefresh.Text = "Refresh";
            btnSnapshotRefresh.UseVisualStyleBackColor = true;
            // 
            // studentsSnapshotPanel
            // 
            studentsSnapshotPanel.BackColor = Color.Gainsboro;
            studentsSnapshotPanel.Controls.Add(pnlSnapshotActions);
            studentsSnapshotPanel.Controls.Add(pnlSnapshotCard);
            studentsSnapshotPanel.Controls.Add(pnlSnapshotDivider);
            studentsSnapshotPanel.Controls.Add(lblSnapshotTitle);
            studentsSnapshotPanel.Location = new Point(550, 116);
            studentsSnapshotPanel.Name = "studentsSnapshotPanel";
            studentsSnapshotPanel.Size = new Size(693, 743);
            studentsSnapshotPanel.TabIndex = 12;
            // 
            // pnlSnapshotCard
            // 
            pnlSnapshotCard.BackColor = Color.White;
            pnlSnapshotCard.BorderStyle = BorderStyle.FixedSingle;
            pnlSnapshotCard.Controls.Add(lblSnapshotStatusBadge);
            pnlSnapshotCard.Controls.Add(lblFieldTenant);
            pnlSnapshotCard.Controls.Add(lblSnapshotStudent);
            pnlSnapshotCard.Controls.Add(lblFieldTenantId);
            pnlSnapshotCard.Controls.Add(lblSnapshotStudentId);
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
            lblFieldTenant.Size = new Size(88, 25);
            lblFieldTenant.TabIndex = 0;
            lblFieldTenant.Text = "Student:";
            // 
            // lblSnapshotStudent
            // 
            lblSnapshotStudent.AutoSize = true;
            lblSnapshotStudent.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSnapshotStudent.ForeColor = Color.Black;
            lblSnapshotStudent.Location = new Point(260, 20);
            lblSnapshotStudent.Name = "lblSnapshotStudent";
            lblSnapshotStudent.Size = new Size(32, 28);
            lblSnapshotStudent.TabIndex = 1;
            lblSnapshotStudent.Text = "—";
            // 
            // lblFieldTenantId
            // 
            lblFieldTenantId.AutoSize = true;
            lblFieldTenantId.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFieldTenantId.ForeColor = Color.DimGray;
            lblFieldTenantId.Location = new Point(20, 65);
            lblFieldTenantId.Name = "lblFieldTenantId";
            lblFieldTenantId.Size = new Size(113, 25);
            lblFieldTenantId.TabIndex = 2;
            lblFieldTenantId.Text = "Student ID:";
            // 
            // lblSnapshotStudentId
            // 
            lblSnapshotStudentId.AutoSize = true;
            lblSnapshotStudentId.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSnapshotStudentId.ForeColor = Color.Black;
            lblSnapshotStudentId.Location = new Point(260, 65);
            lblSnapshotStudentId.Name = "lblSnapshotStudentId";
            lblSnapshotStudentId.Size = new Size(32, 28);
            lblSnapshotStudentId.TabIndex = 3;
            lblSnapshotStudentId.Text = "—";
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
            lblSnapshotTitle.Size = new Size(328, 50);
            lblSnapshotTitle.TabIndex = 0;
            lblSnapshotTitle.Text = "Student Snapshot";
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
            paymentHistoryPanel.TabIndex = 11;
            // 
            // dgvStudents
            // 
            dgvStudents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvStudents.Location = new Point(32, 116);
            dgvStudents.Name = "dgvStudents";
            dgvStudents.RowHeadersWidth = 51;
            dgvStudents.Size = new Size(499, 271);
            dgvStudents.TabIndex = 7;
            dgvStudents.CellClick += dgvStudents_CellClick;
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
            topBar.Controls.Add(addStudentsBtn);
            topBar.Location = new Point(0, 0);
            topBar.Name = "topBar";
            topBar.Padding = new Padding(18, 10, 18, 10);
            topBar.Size = new Size(1243, 92);
            topBar.TabIndex = 8;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(303, 27);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(49, 20);
            lblStatus.TabIndex = 4;
            lblStatus.Text = "Status";
            // 
            // lblBh
            // 
            lblBh.AutoSize = true;
            lblBh.Location = new Point(39, 27);
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
            // addStudentsBtn
            // 
            addStudentsBtn.Location = new Point(1052, 45);
            addStudentsBtn.Name = "addStudentsBtn";
            addStudentsBtn.Size = new Size(104, 34);
            addStudentsBtn.TabIndex = 8;
            addStudentsBtn.Text = "+ Student";
            addStudentsBtn.UseVisualStyleBackColor = true;
            addStudentsBtn.Click += addStudentsBtn_Click;
            // 
            // addStudentsModal
            // 
            addStudentsModal.Anchor = AnchorStyles.None;
            addStudentsModal.BackColor = Color.FromArgb(48, 54, 92);
            addStudentsModal.BorderStyle = BorderStyle.FixedSingle;
            addStudentsModal.Controls.Add(cancelTenantRegister);
            addStudentsModal.Controls.Add(registerTenantBtn);
            addStudentsModal.Controls.Add(txtStudentNo);
            addStudentsModal.Controls.Add(labelStudentNo);
            addStudentsModal.Controls.Add(cbOccType);
            addStudentsModal.Controls.Add(label18);
            addStudentsModal.Controls.Add(studentFirstnameTxt);
            addStudentsModal.Controls.Add(addStudentCloseBtn);
            addStudentsModal.Controls.Add(studentAddressTxt);
            addStudentsModal.Controls.Add(label16);
            addStudentsModal.Controls.Add(studentEmergencyContactTxt);
            addStudentsModal.Controls.Add(label11);
            addStudentsModal.Controls.Add(studentEmergencyNameTxt);
            addStudentsModal.Controls.Add(label10);
            addStudentsModal.Controls.Add(studentContactTxt);
            addStudentsModal.Controls.Add(label7);
            addStudentsModal.Controls.Add(studentEmailTxt);
            addStudentsModal.Controls.Add(label8);
            addStudentsModal.Controls.Add(studentMiddleNameTxt);
            addStudentsModal.Controls.Add(label5);
            addStudentsModal.Controls.Add(label9);
            addStudentsModal.Controls.Add(studentLastNameTxt);
            addStudentsModal.Controls.Add(label12);
            addStudentsModal.Controls.Add(registrationOpenCameraBtn);
            addStudentsModal.Controls.Add(browseProfileBtn);
            addStudentsModal.Controls.Add(profilePathTxt);
            addStudentsModal.Controls.Add(addStudentImg);
            addStudentsModal.Controls.Add(addTenantCloseBtn);
            addStudentsModal.Controls.Add(totalStudents);
            addStudentsModal.Controls.Add(label15);
            addStudentsModal.ForeColor = SystemColors.ButtonHighlight;
            addStudentsModal.Location = new Point(342, 106);
            addStudentsModal.Name = "addStudentsModal";
            addStudentsModal.Size = new Size(718, 599);
            addStudentsModal.TabIndex = 13;
            addStudentsModal.Visible = false;
            // 
            // cancelTenantRegister
            // 
            cancelTenantRegister.BackColor = Color.FromArgb(255, 192, 192);
            cancelTenantRegister.ForeColor = SystemColors.ActiveCaptionText;
            cancelTenantRegister.Location = new Point(45, 529);
            cancelTenantRegister.Name = "cancelTenantRegister";
            cancelTenantRegister.Size = new Size(219, 40);
            cancelTenantRegister.TabIndex = 94;
            cancelTenantRegister.Text = "Cancel";
            cancelTenantRegister.UseVisualStyleBackColor = false;
            // 
            // registerTenantBtn
            // 
            registerTenantBtn.BackColor = Color.FromArgb(128, 255, 128);
            registerTenantBtn.ForeColor = SystemColors.ActiveCaptionText;
            registerTenantBtn.Location = new Point(459, 529);
            registerTenantBtn.Name = "registerTenantBtn";
            registerTenantBtn.Size = new Size(219, 40);
            registerTenantBtn.TabIndex = 93;
            registerTenantBtn.Text = "Register Student";
            registerTenantBtn.UseVisualStyleBackColor = false;
            // 
            // txtStudentNo
            // 
            txtStudentNo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtStudentNo.Location = new Point(45, 184);
            txtStudentNo.Name = "txtStudentNo";
            txtStudentNo.Size = new Size(219, 27);
            txtStudentNo.TabIndex = 90;
            // 
            // labelStudentNo
            // 
            labelStudentNo.AutoSize = true;
            labelStudentNo.ForeColor = SystemColors.ButtonHighlight;
            labelStudentNo.Location = new Point(45, 161);
            labelStudentNo.Name = "labelStudentNo";
            labelStudentNo.Size = new Size(84, 20);
            labelStudentNo.TabIndex = 89;
            labelStudentNo.Text = "Student No";
            // 
            // studentFirstnameTxt
            // 
            studentFirstnameTxt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            studentFirstnameTxt.Location = new Point(45, 320);
            studentFirstnameTxt.Multiline = true;
            studentFirstnameTxt.Name = "studentFirstnameTxt";
            studentFirstnameTxt.Size = new Size(294, 40);
            studentFirstnameTxt.TabIndex = 88;
            // 
            // addStudentCloseBtn
            // 
            addStudentCloseBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            addStudentCloseBtn.BackColor = Color.FromArgb(255, 128, 128);
            addStudentCloseBtn.ForeColor = SystemColors.ActiveCaptionText;
            addStudentCloseBtn.Location = new Point(668, 3);
            addStudentCloseBtn.Name = "addStudentCloseBtn";
            addStudentCloseBtn.Size = new Size(45, 28);
            addStudentCloseBtn.TabIndex = 87;
            addStudentCloseBtn.Text = "X";
            addStudentCloseBtn.UseVisualStyleBackColor = false;
            // 
            // studentAddressTxt
            // 
            studentAddressTxt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            studentAddressTxt.Location = new Point(384, 253);
            studentAddressTxt.Multiline = true;
            studentAddressTxt.Name = "studentAddressTxt";
            studentAddressTxt.Size = new Size(294, 40);
            studentAddressTxt.TabIndex = 86;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.ForeColor = SystemColors.ButtonHighlight;
            label16.Location = new Point(384, 235);
            label16.Name = "label16";
            label16.Size = new Size(69, 20);
            label16.TabIndex = 85;
            label16.Text = "Address :";
            // 
            // studentEmergencyContactTxt
            // 
            studentEmergencyContactTxt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            studentEmergencyContactTxt.Location = new Point(384, 443);
            studentEmergencyContactTxt.Multiline = true;
            studentEmergencyContactTxt.Name = "studentEmergencyContactTxt";
            studentEmergencyContactTxt.Size = new Size(294, 40);
            studentEmergencyContactTxt.TabIndex = 84;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.ForeColor = SystemColors.ButtonHighlight;
            label11.Location = new Point(384, 420);
            label11.Name = "label11";
            label11.Size = new Size(144, 20);
            label11.TabIndex = 83;
            label11.Text = "Emergency Contact :";
            // 
            // studentEmergencyNameTxt
            // 
            studentEmergencyNameTxt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            studentEmergencyNameTxt.Location = new Point(384, 377);
            studentEmergencyNameTxt.Multiline = true;
            studentEmergencyNameTxt.Name = "studentEmergencyNameTxt";
            studentEmergencyNameTxt.Size = new Size(294, 40);
            studentEmergencyNameTxt.TabIndex = 82;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.ForeColor = SystemColors.ButtonHighlight;
            label10.Location = new Point(386, 354);
            label10.Name = "label10";
            label10.Size = new Size(129, 20);
            label10.TabIndex = 81;
            label10.Text = "Emergency Name:";
            // 
            // studentContactTxt
            // 
            studentContactTxt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            studentContactTxt.Location = new Point(384, 312);
            studentContactTxt.Multiline = true;
            studentContactTxt.Name = "studentContactTxt";
            studentContactTxt.Size = new Size(294, 40);
            studentContactTxt.TabIndex = 80;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = SystemColors.ButtonHighlight;
            label7.Location = new Point(384, 294);
            label7.Name = "label7";
            label7.Size = new Size(67, 20);
            label7.TabIndex = 79;
            label7.Text = "Contact :";
            // 
            // studentEmailTxt
            // 
            studentEmailTxt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            studentEmailTxt.Location = new Point(45, 446);
            studentEmailTxt.Multiline = true;
            studentEmailTxt.Name = "studentEmailTxt";
            studentEmailTxt.Size = new Size(294, 40);
            studentEmailTxt.TabIndex = 78;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.ForeColor = SystemColors.ButtonHighlight;
            label8.Location = new Point(45, 428);
            label8.Name = "label8";
            label8.Size = new Size(49, 20);
            label8.TabIndex = 77;
            label8.Text = "Email:";
            // 
            // studentMiddleNameTxt
            // 
            studentMiddleNameTxt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            studentMiddleNameTxt.Location = new Point(45, 381);
            studentMiddleNameTxt.Multiline = true;
            studentMiddleNameTxt.Name = "studentMiddleNameTxt";
            studentMiddleNameTxt.Size = new Size(294, 40);
            studentMiddleNameTxt.TabIndex = 76;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = SystemColors.ButtonHighlight;
            label5.Location = new Point(45, 363);
            label5.Name = "label5";
            label5.Size = new Size(100, 20);
            label5.TabIndex = 75;
            label5.Text = "Middle name:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.ForeColor = SystemColors.ButtonHighlight;
            label9.Location = new Point(45, 299);
            label9.Name = "label9";
            label9.Size = new Size(76, 20);
            label9.TabIndex = 73;
            label9.Text = "Firstname:";
            // 
            // studentLastNameTxt
            // 
            studentLastNameTxt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            studentLastNameTxt.Location = new Point(45, 253);
            studentLastNameTxt.Multiline = true;
            studentLastNameTxt.Name = "studentLastNameTxt";
            studentLastNameTxt.Size = new Size(294, 40);
            studentLastNameTxt.TabIndex = 72;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.ForeColor = SystemColors.ButtonHighlight;
            label12.Location = new Point(47, 230);
            label12.Name = "label12";
            label12.Size = new Size(75, 20);
            label12.TabIndex = 71;
            label12.Text = "Lastname:";
            // 
            // registrationOpenCameraBtn
            // 
            registrationOpenCameraBtn.ForeColor = SystemColors.ActiveCaptionText;
            registrationOpenCameraBtn.Location = new Point(434, 123);
            registrationOpenCameraBtn.Name = "registrationOpenCameraBtn";
            registrationOpenCameraBtn.Size = new Size(85, 28);
            registrationOpenCameraBtn.TabIndex = 66;
            registrationOpenCameraBtn.Text = "Camera";
            registrationOpenCameraBtn.UseVisualStyleBackColor = true;
            // 
            // browseProfileBtn
            // 
            browseProfileBtn.ForeColor = SystemColors.ActiveCaptionText;
            browseProfileBtn.Location = new Point(434, 89);
            browseProfileBtn.Name = "browseProfileBtn";
            browseProfileBtn.Size = new Size(85, 28);
            browseProfileBtn.TabIndex = 65;
            browseProfileBtn.Text = "Browse";
            browseProfileBtn.UseVisualStyleBackColor = true;
            // 
            // profilePathTxt
            // 
            profilePathTxt.Location = new Point(291, 89);
            profilePathTxt.Multiline = true;
            profilePathTxt.Name = "profilePathTxt";
            profilePathTxt.Size = new Size(136, 10);
            profilePathTxt.TabIndex = 64;
            profilePathTxt.Visible = false;
            // 
            // addStudentImg
            // 
            addStudentImg.BackColor = Color.White;
            addStudentImg.Location = new Point(291, 89);
            addStudentImg.Name = "addStudentImg";
            addStudentImg.Size = new Size(136, 131);
            addStudentImg.SizeMode = PictureBoxSizeMode.StretchImage;
            addStudentImg.TabIndex = 63;
            addStudentImg.TabStop = false;
            // 
            // addTenantCloseBtn
            // 
            addTenantCloseBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            addTenantCloseBtn.BackColor = Color.FromArgb(255, 128, 128);
            addTenantCloseBtn.ForeColor = SystemColors.ActiveCaptionText;
            addTenantCloseBtn.Location = new Point(620, 3);
            addTenantCloseBtn.Name = "addTenantCloseBtn";
            addTenantCloseBtn.Size = new Size(45, 28);
            addTenantCloseBtn.TabIndex = 42;
            addTenantCloseBtn.Text = "X";
            addTenantCloseBtn.UseVisualStyleBackColor = false;
            // 
            // totalStudents
            // 
            totalStudents.AutoSize = true;
            totalStudents.Location = new Point(20, 52);
            totalStudents.Name = "totalStudents";
            totalStudents.Size = new Size(50, 20);
            totalStudents.TabIndex = 41;
            totalStudents.Text = "(total)";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI Semibold", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label15.Location = new Point(3, 3);
            label15.Name = "label15";
            label15.Size = new Size(366, 50);
            label15.TabIndex = 0;
            label15.Text = "Student Registration";
            // 
            // detailsModal
            // 
            detailsModal.BackColor = Color.FromArgb(48, 54, 92);
            detailsModal.Controls.Add(lblStudentTitle);
            detailsModal.Location = new Point(1249, 3);
            detailsModal.Name = "detailsModal";
            detailsModal.Size = new Size(425, 969);
            detailsModal.TabIndex = 14;
            // 
            // lblStudentTitle
            // 
            lblStudentTitle.AutoSize = true;
            lblStudentTitle.BackColor = Color.FromArgb(48, 54, 92);
            lblStudentTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblStudentTitle.ForeColor = SystemColors.ButtonHighlight;
            lblStudentTitle.Location = new Point(24, 19);
            lblStudentTitle.Name = "lblStudentTitle";
            lblStudentTitle.Size = new Size(147, 25);
            lblStudentTitle.TabIndex = 3;
            lblStudentTitle.Text = "Student Details";
            // 
            // grpDetails
            // 
            grpDetails.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpDetails.BackColor = Color.FromArgb(48, 54, 92);
            grpDetails.Controls.Add(ViewRoomBtn);
            grpDetails.Controls.Add(detailsOpenCameraBtn);
            grpDetails.Controls.Add(detailsBrowseProfileBtn);
            grpDetails.Controls.Add(details_profilePathTxt);
            grpDetails.Controls.Add(detailsStudenttImg);
            grpDetails.Controls.Add(endRentalBtn);
            grpDetails.Controls.Add(cbDetailsRoom);
            grpDetails.Controls.Add(label4);
            grpDetails.Controls.Add(studentUpdateBtn);
            grpDetails.Controls.Add(studentDeleteBtn);
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
            grpDetails.Location = new Point(1249, 64);
            grpDetails.Name = "grpDetails";
            grpDetails.Padding = new Padding(12);
            grpDetails.Size = new Size(422, 837);
            grpDetails.TabIndex = 4;
            grpDetails.TabStop = false;
            grpDetails.Text = "Details";
            // 
            // ViewRoomBtn
            // 
            ViewRoomBtn.AutoSize = true;
            ViewRoomBtn.Location = new Point(318, 683);
            ViewRoomBtn.Name = "ViewRoomBtn";
            ViewRoomBtn.Size = new Size(30, 20);
            ViewRoomBtn.TabIndex = 70;
            ViewRoomBtn.Text = "👁";
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
            // detailsStudenttImg
            // 
            detailsStudenttImg.BackColor = Color.White;
            detailsStudenttImg.Location = new Point(117, 17);
            detailsStudenttImg.Name = "detailsStudenttImg";
            detailsStudenttImg.Size = new Size(136, 135);
            detailsStudenttImg.SizeMode = PictureBoxSizeMode.StretchImage;
            detailsStudenttImg.TabIndex = 66;
            detailsStudenttImg.TabStop = false;
            // 
            // endRentalBtn
            // 
            endRentalBtn.BackColor = Color.FromArgb(255, 255, 192);
            endRentalBtn.ForeColor = SystemColors.ActiveCaptionText;
            endRentalBtn.Location = new Point(183, 775);
            endRentalBtn.Name = "endRentalBtn";
            endRentalBtn.Size = new Size(85, 34);
            endRentalBtn.TabIndex = 23;
            endRentalBtn.Text = "End Rental";
            endRentalBtn.UseVisualStyleBackColor = false;
            endRentalBtn.Visible = false;
            // 
            // cbDetailsRoom
            // 
            cbDetailsRoom.FormattingEnabled = true;
            cbDetailsRoom.Location = new Point(209, 707);
            cbDetailsRoom.Name = "cbDetailsRoom";
            cbDetailsRoom.Size = new Size(151, 28);
            cbDetailsRoom.TabIndex = 30;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = SystemColors.ButtonHighlight;
            label4.Location = new Point(209, 684);
            label4.Name = "label4";
            label4.Size = new Size(49, 20);
            label4.TabIndex = 29;
            label4.Text = "Room";
            // 
            // studentUpdateBtn
            // 
            studentUpdateBtn.BackColor = Color.FromArgb(128, 255, 128);
            studentUpdateBtn.ForeColor = SystemColors.ActiveCaptionText;
            studentUpdateBtn.Location = new Point(299, 775);
            studentUpdateBtn.Name = "studentUpdateBtn";
            studentUpdateBtn.Size = new Size(85, 34);
            studentUpdateBtn.TabIndex = 23;
            studentUpdateBtn.Text = "Update";
            studentUpdateBtn.UseVisualStyleBackColor = false;
            studentUpdateBtn.Visible = false;
            // 
            // studentDeleteBtn
            // 
            studentDeleteBtn.BackColor = Color.FromArgb(255, 192, 192);
            studentDeleteBtn.ForeColor = SystemColors.ActiveCaptionText;
            studentDeleteBtn.Location = new Point(66, 775);
            studentDeleteBtn.Name = "studentDeleteBtn";
            studentDeleteBtn.Size = new Size(85, 34);
            studentDeleteBtn.TabIndex = 22;
            studentDeleteBtn.Text = "Delete";
            studentDeleteBtn.UseVisualStyleBackColor = false;
            studentDeleteBtn.Visible = false;
            // 
            // detailsCbStatus
            // 
            detailsCbStatus.FormattingEnabled = true;
            detailsCbStatus.Location = new Point(48, 707);
            detailsCbStatus.Name = "detailsCbStatus";
            detailsCbStatus.Size = new Size(151, 28);
            detailsCbStatus.TabIndex = 28;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = SystemColors.ButtonHighlight;
            label6.Location = new Point(42, 684);
            label6.Name = "label6";
            label6.Size = new Size(49, 20);
            label6.TabIndex = 27;
            label6.Text = "Status";
            // 
            // detailsEContact
            // 
            detailsEContact.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            detailsEContact.Location = new Point(48, 641);
            detailsEContact.Name = "detailsEContact";
            detailsEContact.ReadOnly = true;
            detailsEContact.Size = new Size(347, 27);
            detailsEContact.TabIndex = 26;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(42, 618);
            label1.Name = "label1";
            label1.Size = new Size(161, 20);
            label1.TabIndex = 25;
            label1.Text = "Emergency Contact No";
            // 
            // detailsEName
            // 
            detailsEName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            detailsEName.Location = new Point(48, 574);
            detailsEName.Name = "detailsEName";
            detailsEName.Size = new Size(347, 27);
            detailsEName.TabIndex = 24;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(42, 551);
            label2.Name = "label2";
            label2.Size = new Size(181, 20);
            label2.TabIndex = 23;
            label2.Text = "Emergency Contact Name";
            // 
            // detailsAddress
            // 
            detailsAddress.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            detailsAddress.Location = new Point(48, 510);
            detailsAddress.Name = "detailsAddress";
            detailsAddress.Size = new Size(347, 27);
            detailsAddress.TabIndex = 22;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = SystemColors.ButtonHighlight;
            label3.Location = new Point(42, 487);
            label3.Name = "label3";
            label3.Size = new Size(62, 20);
            label3.TabIndex = 21;
            label3.Text = "Address";
            // 
            // detailsEmail
            // 
            detailsEmail.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            detailsEmail.Location = new Point(48, 445);
            detailsEmail.Name = "detailsEmail";
            detailsEmail.ReadOnly = true;
            detailsEmail.Size = new Size(347, 27);
            detailsEmail.TabIndex = 20;
            // 
            // labelRoomStatus
            // 
            labelRoomStatus.AutoSize = true;
            labelRoomStatus.ForeColor = SystemColors.ButtonHighlight;
            labelRoomStatus.Location = new Point(42, 422);
            labelRoomStatus.Name = "labelRoomStatus";
            labelRoomStatus.Size = new Size(46, 20);
            labelRoomStatus.TabIndex = 19;
            labelRoomStatus.Text = "Email";
            // 
            // detailsContact
            // 
            detailsContact.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            detailsContact.Location = new Point(48, 378);
            detailsContact.Name = "detailsContact";
            detailsContact.Size = new Size(347, 27);
            detailsContact.TabIndex = 18;
            // 
            // labelRate
            // 
            labelRate.AutoSize = true;
            labelRate.ForeColor = SystemColors.ButtonHighlight;
            labelRate.Location = new Point(42, 355);
            labelRate.Name = "labelRate";
            labelRate.Size = new Size(60, 20);
            labelRate.TabIndex = 17;
            labelRate.Text = "Contact";
            // 
            // detailsMiddlename
            // 
            detailsMiddlename.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            detailsMiddlename.Location = new Point(48, 318);
            detailsMiddlename.Name = "detailsMiddlename";
            detailsMiddlename.Size = new Size(347, 27);
            detailsMiddlename.TabIndex = 16;
            // 
            // labelCap
            // 
            labelCap.AutoSize = true;
            labelCap.ForeColor = SystemColors.ButtonHighlight;
            labelCap.Location = new Point(42, 297);
            labelCap.Name = "labelCap";
            labelCap.Size = new Size(93, 20);
            labelCap.TabIndex = 15;
            labelCap.Text = "Middlename";
            // 
            // detailsFirstname
            // 
            detailsFirstname.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            detailsFirstname.Location = new Point(48, 256);
            detailsFirstname.Name = "detailsFirstname";
            detailsFirstname.Size = new Size(347, 27);
            detailsFirstname.TabIndex = 14;
            // 
            // labelType
            // 
            labelType.AutoSize = true;
            labelType.ForeColor = SystemColors.ButtonHighlight;
            labelType.Location = new Point(42, 233);
            labelType.Name = "labelType";
            labelType.Size = new Size(73, 20);
            labelType.TabIndex = 13;
            labelType.Text = "Firstname";
            // 
            // detailsLastname
            // 
            detailsLastname.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            detailsLastname.Location = new Point(48, 194);
            detailsLastname.Name = "detailsLastname";
            detailsLastname.Size = new Size(347, 27);
            detailsLastname.TabIndex = 12;
            // 
            // labelRoomNo
            // 
            labelRoomNo.AutoSize = true;
            labelRoomNo.ForeColor = SystemColors.ButtonHighlight;
            labelRoomNo.Location = new Point(42, 171);
            labelRoomNo.Name = "labelRoomNo";
            labelRoomNo.Size = new Size(72, 20);
            labelRoomNo.TabIndex = 11;
            labelRoomNo.Text = "Lastname";
            // 
            // cbOccType
            // 
            cbOccType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbOccType.FormattingEnabled = true;
            cbOccType.Location = new Point(384, 183);
            cbOccType.Name = "cbOccType";
            cbOccType.Size = new Size(294, 28);
            cbOccType.TabIndex = 92;
            cbOccType.Visible = false;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.ForeColor = SystemColors.ButtonHighlight;
            label18.Location = new Point(384, 161);
            label18.Name = "label18";
            label18.Size = new Size(107, 20);
            label18.TabIndex = 91;
            label18.Text = "Occupant Type";
            label18.Visible = false;
            // 
            // StudentsView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(grpDetails);
            Controls.Add(detailsModal);
            Controls.Add(addStudentsModal);
            Controls.Add(studentsSnapshotPanel);
            Controls.Add(paymentHistoryPanel);
            Controls.Add(dgvStudents);
            Controls.Add(topBar);
            Name = "StudentsView";
            Size = new Size(1677, 975);
            Load += Students_Load;
            pnlSnapshotActions.ResumeLayout(false);
            pnlSnapshotActions.PerformLayout();
            studentsSnapshotPanel.ResumeLayout(false);
            studentsSnapshotPanel.PerformLayout();
            pnlSnapshotCard.ResumeLayout(false);
            pnlSnapshotCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            paymentHistoryPanel.ResumeLayout(false);
            paymentHistoryPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStudents).EndInit();
            topBar.ResumeLayout(false);
            topBar.PerformLayout();
            addStudentsModal.ResumeLayout(false);
            addStudentsModal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)addStudentImg).EndInit();
            detailsModal.ResumeLayout(false);
            detailsModal.PerformLayout();
            grpDetails.ResumeLayout(false);
            grpDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)detailsStudenttImg).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnSnapshotOpenPayments;
        private Panel pnlSnapshotActions;
        private Label lblSnapshotHint;
        private Button btnSnapshotViewRoom;
        private Button btnSnapshotRefresh;
        private Panel studentsSnapshotPanel;
        private Panel pnlSnapshotCard;
        private Label lblSnapshotStatusBadge;
        private Label lblFieldTenant;
        private Label lblSnapshotStudent;
        private Label lblFieldTenantId;
        private Label lblSnapshotStudentId;
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
        private Panel pnlSnapshotDivider;
        private Label lblSnapshotTitle;
        private Label label13;
        private Label label17;
        private Label label14;
        private DataGridView dataGridView1;
        private Panel paymentHistoryPanel;
        private DataGridView dgvStudents;
        private Panel topBar;
        private Label lblStatus;
        private Label lblBh;
        private Button btnSearch;
        private ComboBox cbStatusFilter;
        private TextBox txtSearch;
        private ComboBox cbBoardingHouses;
        private Button addStudentsBtn;
        private Panel addStudentsModal;
        private TextBox txtStudentNo;
        private Label labelStudentNo;
        private Button registrationOpenCameraBtn;
        private Button browseProfileBtn;
        private TextBox profilePathTxt;
        private PictureBox addStudentImg;
        private Button cancelTenantRegister;
        private Button registerTenantBtn;
        private Button addTenantCloseBtn;
        private Label totalStudents;
        private Label label15;
        private TextBox studentEmailTxt;
        private Label label8;
        private TextBox studentMiddleNameTxt;
        private Label label5;
        private TextBox tenantFirstNameTxt;
        private Label label9;
        private TextBox studentLastNameTxt;
        private Label label12;
        private TextBox studentAddressTxt;
        private Label label16;
        private TextBox studentEmergencyContactTxt;
        private Label label11;
        private TextBox studentEmergencyNameTxt;
        private Label label10;
        private TextBox studentContactTxt;
        private Label label7;
        private Panel detailsModal;
        private GroupBox grpDetails;
        private Label ViewRoomBtn;
        private Button detailsOpenCameraBtn;
        private Button detailsBrowseProfileBtn;
        private TextBox details_profilePathTxt;
        private PictureBox detailsStudenttImg;
        private Button endRentalBtn;
        private ComboBox cbDetailsRoom;
        private Label label4;
        private Button studentUpdateBtn;
        private Button studentDeleteBtn;
        private ComboBox detailsCbStatus;
        private Label label6;
        private TextBox detailsEContact;
        private Label label1;
        private TextBox detailsEName;
        private Label label2;
        private TextBox detailsAddress;
        private Label label3;
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
        private Label lblStudentTitle;
        private Button addStudentCloseBtn;
        private TextBox studentFirstnameTxt;
        private ComboBox cbOccType;
        private Label label18;
    }
}
