using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BoardingHouse
{
    partial class PaymentsView
    {
        private IContainer components = null!;

        private void InitializeComponent()
        {
            topBar = new Panel();
            label18 = new Label();
            label14 = new Label();
            lblBh = new Label();
            cbBoardingHouses = new ComboBox();
            dpBilling_Month = new DateTimePicker();
            panel1 = new Panel();
            billingItemsModal = new Panel();
            btnVoidCharge = new Button();
            btnAddOrUpdateCharge = new Button();
            labelBillingItemsHint = new Label();
            nudChargeAmount = new NumericUpDown();
            labelChargeAmountTitle = new Label();
            txtChargeDescription = new TextBox();
            labelChargeDescriptionTitle = new Label();
            cbChargeType = new ComboBox();
            labelChargeTypeTitle = new Label();
            labelBillingItemsEditor = new Label();
            closeBillingItemsBtn = new Button();
            label17 = new Label();
            clearSearchBtn = new Button();
            cbSearchBy = new ComboBox();
            txtBoardingHouseName = new Label();
            label11 = new Label();
            txtRoomName = new Label();
            label8 = new Label();
            txtTenantName = new Label();
            label3 = new Label();
            clearFormBtn = new Button();
            paymentBtn = new Button();
            label7 = new Label();
            richtxtRemarks = new RichTextBox();
            label6 = new Label();
            label5 = new Label();
            txtRefNum = new TextBox();
            label4 = new Label();
            cbPaymentMethod = new ComboBox();
            label2 = new Label();
            txtAmount = new TextBox();
            btnPayFullBalance = new Button();
            dgvDataSource = new DataGridView();
            searchBtn = new Button();
            label1 = new Label();
            txtSearch = new TextBox();
            panel2 = new Panel();
            manageBillItemsBtn = new Button();
            modalDialog = new Panel();
            label15 = new Label();
            lblVoidInfo = new Label();
            richTxtVoid = new RichTextBox();
            voidCancelBtn = new Button();
            voidConfirmBtn = new Button();
            lvPaymentHistory = new ListView();
            columnHeaderDate = new ColumnHeader();
            columnHeaderBillMonth = new ColumnHeader();
            columnHeaderPaymentAmount = new ColumnHeader();
            columnHeaderMethod = new ColumnHeader();
            columnHeaderRefNo = new ColumnHeader();
            columnHeaderPaymentStatus = new ColumnHeader();
            btnVoidSelected = new Button();
            btnViewReceipt = new Button();
            panel4 = new Panel();
            alerLbl = new Label();
            label16 = new Label();
            panel3 = new Panel();
            lblRmngBal = new Label();
            lblTotalPaid = new Label();
            lblTotalCharges = new Label();
            label13 = new Label();
            label12 = new Label();
            label10 = new Label();
            lvChargeList = new ListView();
            columnHeaderChargeType = new ColumnHeader();
            columnHeaderDescription = new ColumnHeader();
            columnHeaderChargeAmount = new ColumnHeader();
            columnHeaderChargeStatus = new ColumnHeader();
            label9 = new Label();
            topBar.SuspendLayout();
            panel1.SuspendLayout();
            billingItemsModal.SuspendLayout();
            ((ISupportInitialize)nudChargeAmount).BeginInit();
            ((ISupportInitialize)dgvDataSource).BeginInit();
            panel2.SuspendLayout();
            modalDialog.SuspendLayout();
            panel4.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // topBar
            // 
            topBar.BackColor = Color.WhiteSmoke;
            topBar.Controls.Add(label18);
            topBar.Controls.Add(label14);
            topBar.Controls.Add(lblBh);
            topBar.Controls.Add(cbBoardingHouses);
            topBar.Controls.Add(dpBilling_Month);
            topBar.Dock = DockStyle.Top;
            topBar.Location = new Point(0, 0);
            topBar.Name = "topBar";
            topBar.Padding = new Padding(18, 10, 18, 10);
            topBar.Size = new Size(1677, 92);
            topBar.TabIndex = 2;
            topBar.Paint += topBar_Paint;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(588, 15);
            label18.Name = "label18";
            label18.Size = new Size(101, 20);
            label18.TabIndex = 35;
            label18.Text = "Billing Month:";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label14.Location = new Point(21, 28);
            label14.Name = "label14";
            label14.Size = new Size(275, 41);
            label14.TabIndex = 34;
            label14.Text = "Manage Payments";
            // 
            // lblBh
            // 
            lblBh.AutoSize = true;
            lblBh.Location = new Point(315, 19);
            lblBh.Name = "lblBh";
            lblBh.Size = new Size(116, 20);
            lblBh.TabIndex = 0;
            lblBh.Text = "Boarding House";
            // 
            // cbBoardingHouses
            // 
            cbBoardingHouses.DropDownStyle = ComboBoxStyle.DropDownList;
            cbBoardingHouses.FormattingEnabled = true;
            cbBoardingHouses.Location = new Point(315, 42);
            cbBoardingHouses.Name = "cbBoardingHouses";
            cbBoardingHouses.Size = new Size(240, 28);
            cbBoardingHouses.TabIndex = 1;
            cbBoardingHouses.SelectedIndexChanged += cbBoardingHouses_SelectedIndexChanged;
            // 
            // dpBilling_Month
            // 
            dpBilling_Month.CustomFormat = "MMMM yyyy";
            dpBilling_Month.Format = DateTimePickerFormat.Custom;
            dpBilling_Month.Location = new Point(588, 40);
            dpBilling_Month.Name = "dpBilling_Month";
            dpBilling_Month.ShowUpDown = true;
            dpBilling_Month.Size = new Size(250, 27);
            dpBilling_Month.TabIndex = 13;
            dpBilling_Month.ValueChanged += dpBilling_Month_ValueChanged;
            // 
            // panel1
            // 
            panel1.BackColor = Color.WhiteSmoke;
            panel1.Controls.Add(billingItemsModal);
            panel1.Controls.Add(label17);
            panel1.Controls.Add(clearSearchBtn);
            panel1.Controls.Add(cbSearchBy);
            panel1.Controls.Add(txtBoardingHouseName);
            panel1.Controls.Add(label11);
            panel1.Controls.Add(txtRoomName);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(txtTenantName);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(clearFormBtn);
            panel1.Controls.Add(paymentBtn);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(richtxtRemarks);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(txtRefNum);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(cbPaymentMethod);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(txtAmount);
            panel1.Controls.Add(btnPayFullBalance);
            panel1.Controls.Add(dgvDataSource);
            panel1.Controls.Add(searchBtn);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(txtSearch);
            panel1.Location = new Point(14, 98);
            panel1.Name = "panel1";
            panel1.Size = new Size(948, 874);
            panel1.TabIndex = 3;
            panel1.Paint += panel1_Paint;
            // 
            // billingItemsModal
            // 
            billingItemsModal.Controls.Add(btnVoidCharge);
            billingItemsModal.Controls.Add(btnAddOrUpdateCharge);
            billingItemsModal.Controls.Add(labelBillingItemsHint);
            billingItemsModal.Controls.Add(nudChargeAmount);
            billingItemsModal.Controls.Add(labelChargeAmountTitle);
            billingItemsModal.Controls.Add(txtChargeDescription);
            billingItemsModal.Controls.Add(labelChargeDescriptionTitle);
            billingItemsModal.Controls.Add(cbChargeType);
            billingItemsModal.Controls.Add(labelChargeTypeTitle);
            billingItemsModal.Controls.Add(labelBillingItemsEditor);
            billingItemsModal.Controls.Add(closeBillingItemsBtn);
            billingItemsModal.Location = new Point(490, 22);
            billingItemsModal.Name = "billingItemsModal";
            billingItemsModal.Size = new Size(446, 515);
            billingItemsModal.TabIndex = 36;
            billingItemsModal.Visible = false;
            // 
            // btnVoidCharge
            // 
            btnVoidCharge.BackColor = Color.LightCoral;
            btnVoidCharge.FlatStyle = FlatStyle.Flat;
            btnVoidCharge.ForeColor = SystemColors.ButtonHighlight;
            btnVoidCharge.Location = new Point(190, 305);
            btnVoidCharge.Name = "btnVoidCharge";
            btnVoidCharge.Size = new Size(150, 38);
            btnVoidCharge.TabIndex = 36;
            btnVoidCharge.Text = "Void Selected";
            btnVoidCharge.UseVisualStyleBackColor = false;
            btnVoidCharge.Click += btnVoidCharge_Click;
            // 
            // btnAddOrUpdateCharge
            // 
            btnAddOrUpdateCharge.BackColor = Color.LightSeaGreen;
            btnAddOrUpdateCharge.FlatStyle = FlatStyle.Flat;
            btnAddOrUpdateCharge.ForeColor = SystemColors.ButtonHighlight;
            btnAddOrUpdateCharge.Location = new Point(20, 305);
            btnAddOrUpdateCharge.Name = "btnAddOrUpdateCharge";
            btnAddOrUpdateCharge.Size = new Size(150, 38);
            btnAddOrUpdateCharge.TabIndex = 35;
            btnAddOrUpdateCharge.Text = "Add / Update";
            btnAddOrUpdateCharge.UseVisualStyleBackColor = false;
            btnAddOrUpdateCharge.Click += btnAddOrUpdateCharge_Click;
            // 
            // labelBillingItemsHint
            // 
            labelBillingItemsHint.AutoSize = true;
            labelBillingItemsHint.Font = new Font("Segoe UI", 8F, FontStyle.Italic, GraphicsUnit.Point, 0);
            labelBillingItemsHint.Location = new Point(20, 277);
            labelBillingItemsHint.Name = "labelBillingItemsHint";
            labelBillingItemsHint.Size = new Size(404, 19);
            labelBillingItemsHint.TabIndex = 34;
            labelBillingItemsHint.Text = "Choose a charge and click Add / Update or select a row to edit.";
            // 
            // nudChargeAmount
            // 
            nudChargeAmount.DecimalPlaces = 2;
            nudChargeAmount.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            nudChargeAmount.Location = new Point(20, 238);
            nudChargeAmount.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudChargeAmount.Name = "nudChargeAmount";
            nudChargeAmount.Size = new Size(150, 27);
            nudChargeAmount.TabIndex = 33;
            nudChargeAmount.ThousandsSeparator = true;
            // 
            // labelChargeAmountTitle
            // 
            labelChargeAmountTitle.AutoSize = true;
            labelChargeAmountTitle.Location = new Point(20, 215);
            labelChargeAmountTitle.Name = "labelChargeAmountTitle";
            labelChargeAmountTitle.Size = new Size(65, 20);
            labelChargeAmountTitle.TabIndex = 32;
            labelChargeAmountTitle.Text = "Amount:";
            // 
            // txtChargeDescription
            // 
            txtChargeDescription.BorderStyle = BorderStyle.FixedSingle;
            txtChargeDescription.Location = new Point(20, 141);
            txtChargeDescription.Multiline = true;
            txtChargeDescription.Name = "txtChargeDescription";
            txtChargeDescription.PlaceholderText = "Add a short description";
            txtChargeDescription.Size = new Size(320, 60);
            txtChargeDescription.TabIndex = 31;
            // 
            // labelChargeDescriptionTitle
            // 
            labelChargeDescriptionTitle.AutoSize = true;
            labelChargeDescriptionTitle.Location = new Point(20, 118);
            labelChargeDescriptionTitle.Name = "labelChargeDescriptionTitle";
            labelChargeDescriptionTitle.Size = new Size(88, 20);
            labelChargeDescriptionTitle.TabIndex = 30;
            labelChargeDescriptionTitle.Text = "Description:";
            // 
            // cbChargeType
            // 
            cbChargeType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbChargeType.FormattingEnabled = true;
            cbChargeType.Items.AddRange(new object[] { "RENT", "ELECTRIC", "WATER", "INTERNET", "PENALTY", "OTHER" });
            cbChargeType.Location = new Point(20, 78);
            cbChargeType.Name = "cbChargeType";
            cbChargeType.Size = new Size(220, 28);
            cbChargeType.TabIndex = 29;
            // 
            // labelChargeTypeTitle
            // 
            labelChargeTypeTitle.AutoSize = true;
            labelChargeTypeTitle.Location = new Point(20, 55);
            labelChargeTypeTitle.Name = "labelChargeTypeTitle";
            labelChargeTypeTitle.Size = new Size(94, 20);
            labelChargeTypeTitle.TabIndex = 28;
            labelChargeTypeTitle.Text = "Charge Type:";
            // 
            // labelBillingItemsEditor
            // 
            labelBillingItemsEditor.AutoSize = true;
            labelBillingItemsEditor.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelBillingItemsEditor.Location = new Point(20, 15);
            labelBillingItemsEditor.Name = "labelBillingItemsEditor";
            labelBillingItemsEditor.Size = new Size(175, 28);
            labelBillingItemsEditor.TabIndex = 27;
            labelBillingItemsEditor.Text = "Billing Item Editor";
            // 
            // closeBillingItemsBtn
            // 
            closeBillingItemsBtn.BackColor = Color.LightCoral;
            closeBillingItemsBtn.Location = new Point(393, 0);
            closeBillingItemsBtn.Name = "closeBillingItemsBtn";
            closeBillingItemsBtn.Size = new Size(50, 34);
            closeBillingItemsBtn.TabIndex = 26;
            closeBillingItemsBtn.Text = "X";
            closeBillingItemsBtn.UseVisualStyleBackColor = false;
            closeBillingItemsBtn.Click += closeBillingItemsBtn_Click;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label17.Location = new Point(595, 405);
            label17.Name = "label17";
            label17.Size = new Size(20, 23);
            label17.TabIndex = 35;
            label17.Text = "₱";
            // 
            // clearSearchBtn
            // 
            clearSearchBtn.Location = new Point(245, 84);
            clearSearchBtn.Name = "clearSearchBtn";
            clearSearchBtn.Size = new Size(68, 28);
            clearSearchBtn.TabIndex = 34;
            clearSearchBtn.Text = "Clear";
            clearSearchBtn.UseVisualStyleBackColor = true;
            clearSearchBtn.Click += clearSearchBtn_Click;
            // 
            // cbSearchBy
            // 
            cbSearchBy.DropDownStyle = ComboBoxStyle.DropDownList;
            cbSearchBy.FormattingEnabled = true;
            cbSearchBy.Items.AddRange(new object[] { "Tenant Name", "Room", "Contact No" });
            cbSearchBy.Location = new Point(400, 85);
            cbSearchBy.Name = "cbSearchBy";
            cbSearchBy.Size = new Size(132, 28);
            cbSearchBy.TabIndex = 33;
            cbSearchBy.SelectedIndexChanged += cbSearchBy_SelectedIndexChanged;
            // 
            // txtBoardingHouseName
            // 
            txtBoardingHouseName.AutoSize = true;
            txtBoardingHouseName.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtBoardingHouseName.Location = new Point(592, 255);
            txtBoardingHouseName.Name = "txtBoardingHouseName";
            txtBoardingHouseName.Size = new Size(164, 28);
            txtBoardingHouseName.TabIndex = 32;
            txtBoardingHouseName.Text = "(Boardinghouse)";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(592, 235);
            label11.Name = "label11";
            label11.Size = new Size(123, 20);
            label11.TabIndex = 31;
            label11.Text = "Boarding House :";
            // 
            // txtRoomName
            // 
            txtRoomName.AutoSize = true;
            txtRoomName.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtRoomName.Location = new Point(592, 322);
            txtRoomName.Name = "txtRoomName";
            txtRoomName.Size = new Size(79, 28);
            txtRoomName.TabIndex = 30;
            txtRoomName.Text = "(Room)";
            txtRoomName.Click += label9_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(592, 302);
            label8.Name = "label8";
            label8.Size = new Size(56, 20);
            label8.TabIndex = 29;
            label8.Text = "Room :";
            // 
            // txtTenantName
            // 
            txtTenantName.AutoSize = true;
            txtTenantName.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtTenantName.Location = new Point(592, 192);
            txtTenantName.Name = "txtTenantName";
            txtTenantName.Size = new Size(142, 28);
            txtTenantName.TabIndex = 28;
            txtTenantName.Text = "(tenant name)";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(592, 172);
            label3.Name = "label3";
            label3.Size = new Size(60, 20);
            label3.TabIndex = 27;
            label3.Text = "Tenant :";
            label3.Click += label3_Click;
            // 
            // clearFormBtn
            // 
            clearFormBtn.BackColor = Color.FromArgb(255, 192, 128);
            clearFormBtn.FlatStyle = FlatStyle.Flat;
            clearFormBtn.ForeColor = SystemColors.ButtonHighlight;
            clearFormBtn.Location = new Point(592, 766);
            clearFormBtn.Name = "clearFormBtn";
            clearFormBtn.Size = new Size(134, 40);
            clearFormBtn.TabIndex = 26;
            clearFormBtn.Text = "Clear Form";
            clearFormBtn.UseVisualStyleBackColor = false;
            clearFormBtn.Click += clearFormBtn_Click;
            // 
            // paymentBtn
            // 
            paymentBtn.BackColor = Color.FromArgb(128, 128, 255);
            paymentBtn.FlatStyle = FlatStyle.Flat;
            paymentBtn.ForeColor = SystemColors.ButtonHighlight;
            paymentBtn.Location = new Point(759, 766);
            paymentBtn.Name = "paymentBtn";
            paymentBtn.Size = new Size(134, 40);
            paymentBtn.TabIndex = 25;
            paymentBtn.Text = "Confirm Payment";
            paymentBtn.UseVisualStyleBackColor = false;
            paymentBtn.Click += paymentBtn_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(592, 595);
            label7.Name = "label7";
            label7.Size = new Size(72, 20);
            label7.TabIndex = 24;
            label7.Text = "Remarks :";
            // 
            // richtxtRemarks
            // 
            richtxtRemarks.Location = new Point(592, 618);
            richtxtRemarks.Name = "richtxtRemarks";
            richtxtRemarks.Size = new Size(301, 81);
            richtxtRemarks.TabIndex = 23;
            richtxtRemarks.Text = "";
            richtxtRemarks.TextChanged += richtxtRemarks_TextChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(700, 520);
            label6.Name = "label6";
            label6.Size = new Size(207, 17);
            label6.TabIndex = 21;
            label6.Text = "(Required for non-cash payments)";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(592, 520);
            label5.Name = "label5";
            label5.Size = new Size(102, 20);
            label5.TabIndex = 20;
            label5.Text = "Reference No.";
            // 
            // txtRefNum
            // 
            txtRefNum.BorderStyle = BorderStyle.FixedSingle;
            txtRefNum.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtRefNum.Location = new Point(592, 543);
            txtRefNum.Multiline = true;
            txtRefNum.Name = "txtRefNum";
            txtRefNum.PlaceholderText = "Enter Ref No.";
            txtRefNum.Size = new Size(301, 32);
            txtRefNum.TabIndex = 19;
            txtRefNum.TextChanged += txtRefNum_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(592, 455);
            label4.Name = "label4";
            label4.Size = new Size(136, 20);
            label4.TabIndex = 18;
            label4.Text = "Payement Method :";
            // 
            // cbPaymentMethod
            // 
            cbPaymentMethod.DropDownStyle = ComboBoxStyle.DropDownList;
            cbPaymentMethod.FormattingEnabled = true;
            cbPaymentMethod.Items.AddRange(new object[] { "CASH", "GCASH", "BANK", "OTHER" });
            cbPaymentMethod.Location = new Point(592, 478);
            cbPaymentMethod.Name = "cbPaymentMethod";
            cbPaymentMethod.Size = new Size(151, 28);
            cbPaymentMethod.TabIndex = 17;
            cbPaymentMethod.SelectedIndexChanged += cbPaymentMethod_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(592, 378);
            label2.Name = "label2";
            label2.Size = new Size(69, 20);
            label2.TabIndex = 14;
            label2.Text = "Amount :";
            // 
            // txtAmount
            // 
            txtAmount.BorderStyle = BorderStyle.FixedSingle;
            txtAmount.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtAmount.Location = new Point(613, 401);
            txtAmount.Multiline = true;
            txtAmount.Name = "txtAmount";
            txtAmount.PlaceholderText = "     Enter Amount";
            txtAmount.Size = new Size(200, 32);
            txtAmount.TabIndex = 9;
            // 
            // btnPayFullBalance
            // 
            btnPayFullBalance.BackColor = Color.FromArgb(192, 255, 192);
            btnPayFullBalance.FlatStyle = FlatStyle.Flat;
            btnPayFullBalance.Location = new Point(829, 401);
            btnPayFullBalance.Name = "btnPayFullBalance";
            btnPayFullBalance.Size = new Size(85, 32);
            btnPayFullBalance.TabIndex = 27;
            btnPayFullBalance.Text = "Pay Full Balance";
            btnPayFullBalance.UseVisualStyleBackColor = false;
            btnPayFullBalance.Click += btnPayFullBalance_Click;
            // 
            // dgvDataSource
            // 
            dgvDataSource.AllowUserToAddRows = false;
            dgvDataSource.AllowUserToDeleteRows = false;
            dgvDataSource.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDataSource.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDataSource.Location = new Point(15, 128);
            dgvDataSource.MultiSelect = false;
            dgvDataSource.Name = "dgvDataSource";
            dgvDataSource.ReadOnly = true;
            dgvDataSource.RowHeadersVisible = false;
            dgvDataSource.RowHeadersWidth = 51;
            dgvDataSource.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDataSource.Size = new Size(517, 683);
            dgvDataSource.TabIndex = 12;
            dgvDataSource.CellClick += dgvDataSource_CellClick;
            dgvDataSource.CellContentClick += dgvDataSource_CellContentClick;
            // 
            // searchBtn
            // 
            searchBtn.Location = new Point(195, 84);
            searchBtn.Name = "searchBtn";
            searchBtn.Size = new Size(44, 28);
            searchBtn.TabIndex = 11;
            searchBtn.Text = "🔍";
            searchBtn.UseVisualStyleBackColor = true;
            searchBtn.Click += searchBtn_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(15, 12);
            label1.Name = "label1";
            label1.Size = new Size(245, 41);
            label1.TabIndex = 0;
            label1.Text = "Payment Details";
            // 
            // txtSearch
            // 
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Location = new Point(15, 84);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search tenant, room, or contact";
            txtSearch.Size = new Size(174, 27);
            txtSearch.TabIndex = 10;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // panel2
            // 
            panel2.BackColor = Color.WhiteSmoke;
            panel2.Controls.Add(manageBillItemsBtn);
            panel2.Controls.Add(modalDialog);
            panel2.Controls.Add(lvPaymentHistory);
            panel2.Controls.Add(btnVoidSelected);
            panel2.Controls.Add(btnViewReceipt);
            panel2.Controls.Add(panel4);
            panel2.Controls.Add(panel3);
            panel2.Controls.Add(lvChargeList);
            panel2.Controls.Add(label9);
            panel2.Location = new Point(977, 98);
            panel2.Name = "panel2";
            panel2.Size = new Size(697, 874);
            panel2.TabIndex = 4;
            panel2.Paint += panel2_Paint;
            // 
            // manageBillItemsBtn
            // 
            manageBillItemsBtn.BackColor = Color.FromArgb(224, 224, 224);
            manageBillItemsBtn.FlatStyle = FlatStyle.Flat;
            manageBillItemsBtn.Location = new Point(293, 28);
            manageBillItemsBtn.Name = "manageBillItemsBtn";
            manageBillItemsBtn.Size = new Size(130, 32);
            manageBillItemsBtn.TabIndex = 28;
            manageBillItemsBtn.Text = "Manage Charges";
            manageBillItemsBtn.UseVisualStyleBackColor = false;
            manageBillItemsBtn.Click += manageBillItemsBtn_Click;
            // 
            // modalDialog
            // 
            modalDialog.BackColor = Color.FromArgb(255, 255, 192);
            modalDialog.Controls.Add(label15);
            modalDialog.Controls.Add(lblVoidInfo);
            modalDialog.Controls.Add(richTxtVoid);
            modalDialog.Controls.Add(voidCancelBtn);
            modalDialog.Controls.Add(voidConfirmBtn);
            modalDialog.Location = new Point(21, 716);
            modalDialog.Name = "modalDialog";
            modalDialog.Size = new Size(479, 158);
            modalDialog.TabIndex = 38;
            modalDialog.Visible = false;
            modalDialog.Paint += modalDialog_Paint;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(15, 11);
            label15.Name = "label15";
            label15.Size = new Size(99, 20);
            label15.TabIndex = 21;
            label15.Text = "Void Payment";
            // 
            // lblVoidInfo
            // 
            lblVoidInfo.AutoSize = true;
            lblVoidInfo.Location = new Point(15, 38);
            lblVoidInfo.Name = "lblVoidInfo";
            lblVoidInfo.Size = new Size(176, 20);
            lblVoidInfo.TabIndex = 22;
            lblVoidInfo.Text = "Selected Payment: (none)";
            // 
            // richTxtVoid
            // 
            richTxtVoid.Location = new Point(15, 63);
            richTxtVoid.Name = "richTxtVoid";
            richTxtVoid.Size = new Size(439, 35);
            richTxtVoid.TabIndex = 24;
            richTxtVoid.Text = "";
            // 
            // voidCancelBtn
            // 
            voidCancelBtn.BackColor = Color.LightGray;
            voidCancelBtn.Location = new Point(178, 108);
            voidCancelBtn.Name = "voidCancelBtn";
            voidCancelBtn.Size = new Size(94, 29);
            voidCancelBtn.TabIndex = 26;
            voidCancelBtn.Text = "Cancel";
            voidCancelBtn.UseVisualStyleBackColor = false;
            voidCancelBtn.Click += button2_Click;
            // 
            // voidConfirmBtn
            // 
            voidConfirmBtn.BackColor = Color.LightCoral;
            voidConfirmBtn.Location = new Point(348, 108);
            voidConfirmBtn.Name = "voidConfirmBtn";
            voidConfirmBtn.Size = new Size(94, 29);
            voidConfirmBtn.TabIndex = 25;
            voidConfirmBtn.Text = "Confirm";
            voidConfirmBtn.UseVisualStyleBackColor = false;
            voidConfirmBtn.Click += voidConfirmBtn_Click;
            // 
            // lvPaymentHistory
            // 
            lvPaymentHistory.Columns.AddRange(new ColumnHeader[] { columnHeaderDate, columnHeaderBillMonth, columnHeaderPaymentAmount, columnHeaderMethod, columnHeaderRefNo, columnHeaderPaymentStatus });
            lvPaymentHistory.FullRowSelect = true;
            lvPaymentHistory.GridLines = true;
            lvPaymentHistory.Location = new Point(19, 390);
            lvPaymentHistory.Name = "lvPaymentHistory";
            lvPaymentHistory.Size = new Size(665, 279);
            lvPaymentHistory.TabIndex = 37;
            lvPaymentHistory.UseCompatibleStateImageBehavior = false;
            lvPaymentHistory.View = View.Details;
            lvPaymentHistory.SelectedIndexChanged += lvPaymentHistory_SelectedIndexChanged;
            // 
            // columnHeaderDate
            // 
            columnHeaderDate.Text = "Date";
            columnHeaderDate.Width = 115;
            // 
            // columnHeaderBillMonth
            // 
            columnHeaderBillMonth.Text = "Bill Month";
            columnHeaderBillMonth.Width = 105;
            // 
            // columnHeaderPaymentAmount
            // 
            columnHeaderPaymentAmount.Text = "Amount";
            columnHeaderPaymentAmount.Width = 85;
            // 
            // columnHeaderMethod
            // 
            columnHeaderMethod.Text = "Method";
            columnHeaderMethod.Width = 75;
            // 
            // columnHeaderRefNo
            // 
            columnHeaderRefNo.Text = "Ref No";
            columnHeaderRefNo.Width = 135;
            // 
            // columnHeaderPaymentStatus
            // 
            columnHeaderPaymentStatus.Text = "Status";
            columnHeaderPaymentStatus.Width = 75;
            // 
            // btnVoidSelected
            // 
            btnVoidSelected.BackColor = Color.LightCoral;
            btnVoidSelected.FlatStyle = FlatStyle.Flat;
            btnVoidSelected.Location = new Point(19, 672);
            btnVoidSelected.Name = "btnVoidSelected";
            btnVoidSelected.Size = new Size(120, 32);
            btnVoidSelected.TabIndex = 38;
            btnVoidSelected.Text = "Void Selected";
            btnVoidSelected.UseVisualStyleBackColor = false;
            btnVoidSelected.Click += btnVoidSelected_Click;
            // 
            // btnViewReceipt
            // 
            btnViewReceipt.BackColor = Color.LightSteelBlue;
            btnViewReceipt.FlatStyle = FlatStyle.Flat;
            btnViewReceipt.Location = new Point(169, 672);
            btnViewReceipt.Name = "btnViewReceipt";
            btnViewReceipt.Size = new Size(120, 32);
            btnViewReceipt.TabIndex = 39;
            btnViewReceipt.Text = "View Receipt";
            btnViewReceipt.UseVisualStyleBackColor = false;
            btnViewReceipt.Click += btnViewReceipt_Click;
            // 
            // panel4
            // 
            panel4.Controls.Add(alerLbl);
            panel4.Controls.Add(label16);
            panel4.Location = new Point(444, 220);
            panel4.Name = "panel4";
            panel4.Size = new Size(240, 125);
            panel4.TabIndex = 36;
            panel4.Paint += panel4_Paint;
            // 
            // alerLbl
            // 
            alerLbl.BorderStyle = BorderStyle.FixedSingle;
            alerLbl.Location = new Point(12, 38);
            alerLbl.Name = "alerLbl";
            alerLbl.Size = new Size(220, 75);
            alerLbl.TabIndex = 3;
            alerLbl.Text = "(alerts here)";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(12, 18);
            label16.Name = "label16";
            label16.Size = new Size(86, 20);
            label16.TabIndex = 0;
            label16.Text = "Alerts/Info :";
            // 
            // panel3
            // 
            panel3.Controls.Add(lblRmngBal);
            panel3.Controls.Add(lblTotalPaid);
            panel3.Controls.Add(lblTotalCharges);
            panel3.Controls.Add(label13);
            panel3.Controls.Add(label12);
            panel3.Controls.Add(label10);
            panel3.Location = new Point(444, 66);
            panel3.Name = "panel3";
            panel3.Size = new Size(240, 145);
            panel3.TabIndex = 35;
            panel3.Paint += panel3_Paint;
            // 
            // lblRmngBal
            // 
            lblRmngBal.AutoSize = true;
            lblRmngBal.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRmngBal.Location = new Point(161, 108);
            lblRmngBal.Name = "lblRmngBal";
            lblRmngBal.Size = new Size(35, 23);
            lblRmngBal.TabIndex = 5;
            lblRmngBal.Text = "(N)";
            // 
            // lblTotalPaid
            // 
            lblTotalPaid.AutoSize = true;
            lblTotalPaid.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalPaid.Location = new Point(99, 59);
            lblTotalPaid.Name = "lblTotalPaid";
            lblTotalPaid.Size = new Size(35, 23);
            lblTotalPaid.TabIndex = 4;
            lblTotalPaid.Text = "(N)";
            // 
            // lblTotalCharges
            // 
            lblTotalCharges.AutoSize = true;
            lblTotalCharges.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalCharges.Location = new Point(120, 15);
            lblTotalCharges.Name = "lblTotalCharges";
            lblTotalCharges.Size = new Size(35, 23);
            lblTotalCharges.TabIndex = 3;
            lblTotalCharges.Text = "(N)";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(12, 111);
            label13.Name = "label13";
            label13.Size = new Size(143, 20);
            label13.TabIndex = 2;
            label13.Text = "Remaining Balance :";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(12, 62);
            label12.Name = "label12";
            label12.Size = new Size(81, 20);
            label12.TabIndex = 1;
            label12.Text = "Total Paid :";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(12, 18);
            label10.Name = "label10";
            label10.Size = new Size(106, 20);
            label10.TabIndex = 0;
            label10.Text = "Total Charges :";
            label10.Click += label10_Click;
            // 
            // lvChargeList
            // 
            lvChargeList.Columns.AddRange(new ColumnHeader[] { columnHeaderChargeType, columnHeaderDescription, columnHeaderChargeAmount, columnHeaderChargeStatus });
            lvChargeList.FullRowSelect = true;
            lvChargeList.GridLines = true;
            lvChargeList.Location = new Point(19, 66);
            lvChargeList.Name = "lvChargeList";
            lvChargeList.Size = new Size(404, 279);
            lvChargeList.TabIndex = 34;
            lvChargeList.UseCompatibleStateImageBehavior = false;
            lvChargeList.View = View.Details;
            lvChargeList.SelectedIndexChanged += lvChargeList_SelectedIndexChanged;
            // 
            // columnHeaderChargeType
            // 
            columnHeaderChargeType.Text = "Charge Type";
            columnHeaderChargeType.Width = 95;
            // 
            // columnHeaderDescription
            // 
            columnHeaderDescription.Text = "Description";
            columnHeaderDescription.Width = 185;
            // 
            // columnHeaderChargeAmount
            // 
            columnHeaderChargeAmount.Text = "Amount";
            columnHeaderChargeAmount.Width = 70;
            // 
            // columnHeaderChargeStatus
            // 
            columnHeaderChargeStatus.Text = "Status";
            columnHeaderChargeStatus.Width = 70;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(19, 12);
            label9.Name = "label9";
            label9.Size = new Size(252, 41);
            label9.TabIndex = 33;
            label9.Text = "Billing Summary";
            // 
            // PaymentsView
            // 
            BackColor = Color.White;
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(topBar);
            Margin = new Padding(0);
            Name = "PaymentsView";
            Size = new Size(1677, 975);
            Load += PaymentsView_Load;
            topBar.ResumeLayout(false);
            topBar.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            billingItemsModal.ResumeLayout(false);
            billingItemsModal.PerformLayout();
            ((ISupportInitialize)nudChargeAmount).EndInit();
            ((ISupportInitialize)dgvDataSource).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            modalDialog.ResumeLayout(false);
            modalDialog.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }
        private Panel topBar;
        private Label lblBh;
        private Button btnSearch;
        private TextBox txtSearch;
        private ComboBox cbBoardingHouses;
        private Panel panel1;
        private Label label1;
        private TextBox txtAmount;
        private DateTimePicker dpBilling_Month;
        private DataGridView dgvDataSource;
        private Button searchBtn;
        private ComboBox cbPaymentMethod;
        private Label label2;
        private Label label6;
        private Label label5;
        private TextBox txtRefNum;
        private Label label4;
        private Label label7;
        private RichTextBox richtxtRemarks;
        private Button clearFormBtn;
        private Button paymentBtn;
        private Button btnPayFullBalance;
        private Label txtBoardingHouseName;
        private Label label11;
        private Label txtRoomName;
        private Label label8;
        private Label txtTenantName;
        private Label label3;
        private Panel panel2;
        private ListView lvChargeList;
        private ColumnHeader columnHeaderChargeType;
        private ColumnHeader columnHeaderDescription;
        private ColumnHeader columnHeaderChargeAmount;
        private ColumnHeader columnHeaderChargeStatus;
        private Label label9;
        private Panel panel3;
        private Label label10;
        private Label label12;
        private Panel panel4;
        private Label label16;
        private Label label13;
        private ListView lvPaymentHistory;
        private ColumnHeader columnHeaderDate;
        private ColumnHeader columnHeaderBillMonth;
        private ColumnHeader columnHeaderPaymentAmount;
        private ColumnHeader columnHeaderMethod;
        private ColumnHeader columnHeaderRefNo;
        private ColumnHeader columnHeaderPaymentStatus;
        private Panel modalDialog;
        private Button voidCancelBtn;
        private Button voidConfirmBtn;
        private RichTextBox richTxtVoid;
        private Label lblVoidInfo;
        private Label label15;
        private Label lblTotalCharges;
        private Label lblRmngBal;
        private Label lblTotalPaid;
        private Label label14;
        private Button clearSearchBtn;
        private ComboBox cbSearchBy;
        private Label label17;
        private Label alerLbl;
        private Label label18;
        private Button btnVoidSelected;
        private Button btnViewReceipt;
        private Label labelBillingItemsEditor;
        private Label labelChargeTypeTitle;
        private ComboBox cbChargeType;
        private Label labelChargeDescriptionTitle;
        private TextBox txtChargeDescription;
        private Label labelChargeAmountTitle;
        private NumericUpDown nudChargeAmount;
        private Label labelBillingItemsHint;
        private Button btnAddOrUpdateCharge;
        private Button btnVoidCharge;
        private Panel billingItemsModal;
        private Button closeBillingItemsBtn;
        private Button manageBillItemsBtn;
    }
}
