using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BoardingHouse
{
    partial class BoardingHousesView
    {
        private IContainer components = null!;

        private void InitializeComponent()
        {
            label1 = new Label();
            detailsModal = new Panel();
            viewRoomsBtn = new Button();
            details_txtThumbnailPath = new TextBox();
            label19 = new Label();
            details_cbOwner = new ComboBox();
            label18 = new Label();
            details_txtContactNo = new TextBox();
            details_cbstatus = new ComboBox();
            label17 = new Label();
            editBrowseBtn = new Button();
            btnDelete = new Button();
            detailsClosebtn = new Button();
            btnUpdate = new Button();
            panel1 = new Panel();
            label9 = new Label();
            details_txtLongitude = new TextBox();
            label8 = new Label();
            details_txtLatitude = new TextBox();
            label7 = new Label();
            label6 = new Label();
            details_picThumbnail = new PictureBox();
            details_txtAvailableRooms = new TextBox();
            label5 = new Label();
            label3 = new Label();
            details_txtTotalRooms = new TextBox();
            label4 = new Label();
            details_txtAddress = new TextBox();
            details_txtBHName = new TextBox();
            mapSingleModal = new Panel();
            mapSingleWebView = new Microsoft.Web.WebView2.WinForms.WebView2();
            button3 = new Button();
            rightPanel = new Panel();
            AddModal = new Panel();
            label11 = new Label();
            locateBtn = new Button();
            txtThumbnailPath = new TextBox();
            label21 = new Label();
            txtContactNo = new TextBox();
            label20 = new Label();
            cbOwner = new ComboBox();
            addBrowseBtn = new Button();
            addNewCloseBtn = new Button();
            btnSave = new Button();
            btnCancel = new Button();
            panel3 = new Panel();
            label2 = new Label();
            txtLatitude = new TextBox();
            label10 = new Label();
            txtLongitude = new TextBox();
            label12 = new Label();
            picThumbnail = new PictureBox();
            label13 = new Label();
            label15 = new Label();
            txtAddress = new TextBox();
            txtBHName = new TextBox();
            contentHostPanel = new Panel();
            textBox1 = new TextBox();
            searchbtn = new Button();
            viewMapBtn = new Button();
            addNewBtn = new Button();
            dgvBoardingHouses = new DataGridView();
            panel2 = new Panel();
            mapLocatorModal = new Panel();
            button2 = new Button();
            label27 = new Label();
            panel14 = new Panel();
            locateLatTxt = new Label();
            label25 = new Label();
            panel8 = new Panel();
            locateLongTxt = new Label();
            label23 = new Label();
            panel7 = new Panel();
            locateAddressTxt = new Label();
            button1 = new Button();
            mapLocatorWebView = new Microsoft.Web.WebView2.WinForms.WebView2();
            mapModal = new Panel();
            mapWebView = new Microsoft.Web.WebView2.WinForms.WebView2();
            closemapBtn = new Button();
            panel6 = new Panel();
            totalInactive = new Label();
            label26 = new Label();
            panel5 = new Panel();
            totalActive = new Label();
            label24 = new Label();
            panel4 = new Panel();
            totalBH = new Label();
            label22 = new Label();
            roomsHostPanel = new Panel();
            panel9 = new Panel();
            bhStatsContainer = new Panel();
            panel13 = new Panel();
            panel17 = new Panel();
            totalAvailableRoomsTxt = new Label();
            label33 = new Label();
            panel16 = new Panel();
            tootalRoomsOccupiedTxt = new Label();
            label31 = new Label();
            panel15 = new Panel();
            totalRoomsTxt = new Label();
            label16 = new Label();
            panel12 = new Panel();
            activeRenatlsTxt = new Label();
            label30 = new Label();
            panel11 = new Panel();
            thisMontEarningsTxt = new Label();
            label28 = new Label();
            panel10 = new Panel();
            earningsTxt = new Label();
            label14 = new Label();
            detailsModal.SuspendLayout();
            ((ISupportInitialize)details_picThumbnail).BeginInit();
            mapSingleModal.SuspendLayout();
            ((ISupportInitialize)mapSingleWebView).BeginInit();
            rightPanel.SuspendLayout();
            AddModal.SuspendLayout();
            ((ISupportInitialize)picThumbnail).BeginInit();
            ((ISupportInitialize)dgvBoardingHouses).BeginInit();
            panel2.SuspendLayout();
            mapLocatorModal.SuspendLayout();
            panel14.SuspendLayout();
            panel8.SuspendLayout();
            panel7.SuspendLayout();
            ((ISupportInitialize)mapLocatorWebView).BeginInit();
            mapModal.SuspendLayout();
            ((ISupportInitialize)mapWebView).BeginInit();
            panel6.SuspendLayout();
            panel5.SuspendLayout();
            panel4.SuspendLayout();
            panel9.SuspendLayout();
            bhStatsContainer.SuspendLayout();
            panel13.SuspendLayout();
            panel17.SuspendLayout();
            panel16.SuspendLayout();
            panel15.SuspendLayout();
            panel12.SuspendLayout();
            panel11.SuspendLayout();
            panel10.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.Location = new Point(30, 13);
            label1.Name = "label1";
            label1.Size = new Size(246, 41);
            label1.TabIndex = 0;
            label1.Text = "Boarding Houses";
            // 
            // detailsModal
            // 
            detailsModal.BackColor = Color.Gainsboro;
            detailsModal.Controls.Add(viewRoomsBtn);
            detailsModal.Controls.Add(details_txtThumbnailPath);
            detailsModal.Controls.Add(label19);
            detailsModal.Controls.Add(details_cbOwner);
            detailsModal.Controls.Add(label18);
            detailsModal.Controls.Add(details_txtContactNo);
            detailsModal.Controls.Add(details_cbstatus);
            detailsModal.Controls.Add(label17);
            detailsModal.Controls.Add(editBrowseBtn);
            detailsModal.Controls.Add(btnDelete);
            detailsModal.Controls.Add(detailsClosebtn);
            detailsModal.Controls.Add(btnUpdate);
            detailsModal.Controls.Add(panel1);
            detailsModal.Controls.Add(label9);
            detailsModal.Controls.Add(details_txtLongitude);
            detailsModal.Controls.Add(label8);
            detailsModal.Controls.Add(details_txtLatitude);
            detailsModal.Controls.Add(label7);
            detailsModal.Controls.Add(label6);
            detailsModal.Controls.Add(details_picThumbnail);
            detailsModal.Controls.Add(details_txtAvailableRooms);
            detailsModal.Controls.Add(label5);
            detailsModal.Controls.Add(label3);
            detailsModal.Controls.Add(details_txtTotalRooms);
            detailsModal.Controls.Add(label4);
            detailsModal.Controls.Add(details_txtAddress);
            detailsModal.Controls.Add(details_txtBHName);
            detailsModal.ForeColor = SystemColors.ActiveCaptionText;
            detailsModal.Location = new Point(3, 365);
            detailsModal.Name = "detailsModal";
            detailsModal.Size = new Size(540, 607);
            detailsModal.TabIndex = 2;
            detailsModal.Paint += panel1_Paint;
            // 
            // viewRoomsBtn
            // 
            viewRoomsBtn.FlatStyle = FlatStyle.Flat;
            viewRoomsBtn.Location = new Point(325, 353);
            viewRoomsBtn.Name = "viewRoomsBtn";
            viewRoomsBtn.Size = new Size(78, 30);
            viewRoomsBtn.TabIndex = 35;
            viewRoomsBtn.Text = "Rooms";
            viewRoomsBtn.UseVisualStyleBackColor = true;
            viewRoomsBtn.Visible = false;
            viewRoomsBtn.Click += viewRoomsBtn_Click;
            // 
            // details_txtThumbnailPath
            // 
            details_txtThumbnailPath.Location = new Point(333, 54);
            details_txtThumbnailPath.Multiline = true;
            details_txtThumbnailPath.Name = "details_txtThumbnailPath";
            details_txtThumbnailPath.Size = new Size(161, 20);
            details_txtThumbnailPath.TabIndex = 32;
            details_txtThumbnailPath.Visible = false;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label19.Location = new Point(35, 234);
            label19.Name = "label19";
            label19.Size = new Size(77, 25);
            label19.TabIndex = 26;
            label19.Text = "Owner :";
            // 
            // details_cbOwner
            // 
            details_cbOwner.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            details_cbOwner.AutoCompleteSource = AutoCompleteSource.ListItems;
            details_cbOwner.FormattingEnabled = true;
            details_cbOwner.Location = new Point(35, 262);
            details_cbOwner.Name = "details_cbOwner";
            details_cbOwner.Size = new Size(212, 28);
            details_cbOwner.TabIndex = 25;
            details_cbOwner.SelectedIndexChanged += details_cbOwner_SelectedIndexChanged;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label18.Location = new Point(35, 95);
            label18.Name = "label18";
            label18.Size = new Size(110, 25);
            label18.TabIndex = 24;
            label18.Text = "Contact No.";
            // 
            // details_txtContactNo
            // 
            details_txtContactNo.Location = new Point(35, 123);
            details_txtContactNo.Multiline = true;
            details_txtContactNo.Name = "details_txtContactNo";
            details_txtContactNo.Size = new Size(212, 27);
            details_txtContactNo.TabIndex = 23;
            // 
            // details_cbstatus
            // 
            details_cbstatus.FormattingEnabled = true;
            details_cbstatus.Location = new Point(110, 309);
            details_cbstatus.Name = "details_cbstatus";
            details_cbstatus.Size = new Size(139, 28);
            details_cbstatus.TabIndex = 22;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label17.Location = new Point(35, 309);
            label17.Name = "label17";
            label17.Size = new Size(72, 25);
            label17.TabIndex = 21;
            label17.Text = "Status :";
            // 
            // editBrowseBtn
            // 
            editBrowseBtn.ForeColor = SystemColors.ActiveCaptionText;
            editBrowseBtn.Location = new Point(333, 191);
            editBrowseBtn.Name = "editBrowseBtn";
            editBrowseBtn.Size = new Size(78, 29);
            editBrowseBtn.TabIndex = 19;
            editBrowseBtn.Text = "Browse";
            editBrowseBtn.UseVisualStyleBackColor = true;
            editBrowseBtn.Visible = false;
            editBrowseBtn.Click += editBrowseBtn_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(255, 128, 128);
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.ForeColor = SystemColors.ActiveCaptionText;
            btnDelete.Location = new Point(137, 549);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(118, 29);
            btnDelete.TabIndex = 18;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Visible = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // detailsClosebtn
            // 
            detailsClosebtn.BackColor = Color.FromArgb(255, 128, 128);
            detailsClosebtn.FlatStyle = FlatStyle.Flat;
            detailsClosebtn.ForeColor = SystemColors.ActiveCaptionText;
            detailsClosebtn.Location = new Point(484, 3);
            detailsClosebtn.Name = "detailsClosebtn";
            detailsClosebtn.Size = new Size(53, 29);
            detailsClosebtn.TabIndex = 17;
            detailsClosebtn.Text = "X";
            detailsClosebtn.UseVisualStyleBackColor = false;
            detailsClosebtn.Visible = false;
            detailsClosebtn.Click += detailsClosebtn_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.FromArgb(255, 255, 128);
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.ForeColor = SystemColors.ActiveCaptionText;
            btnUpdate.Location = new Point(306, 549);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(118, 29);
            btnUpdate.TabIndex = 15;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Visible = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.DarkGray;
            panel1.Location = new Point(23, 397);
            panel1.Name = "panel1";
            panel1.Size = new Size(478, 13);
            panel1.TabIndex = 14;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(38, 471);
            label9.Name = "label9";
            label9.Size = new Size(90, 25);
            label9.TabIndex = 13;
            label9.Text = "Latitude :";
            // 
            // details_txtLongitude
            // 
            details_txtLongitude.Location = new Point(38, 499);
            details_txtLongitude.Multiline = true;
            details_txtLongitude.Name = "details_txtLongitude";
            details_txtLongitude.Size = new Size(455, 27);
            details_txtLongitude.TabIndex = 12;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.Location = new Point(36, 413);
            label8.Name = "label8";
            label8.Size = new Size(107, 25);
            label8.TabIndex = 11;
            label8.Text = "Longitude :";
            // 
            // details_txtLatitude
            // 
            details_txtLatitude.Location = new Point(36, 441);
            details_txtLatitude.Multiline = true;
            details_txtLatitude.Name = "details_txtLatitude";
            details_txtLatitude.Size = new Size(455, 27);
            details_txtLatitude.TabIndex = 10;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(325, 291);
            label7.Name = "label7";
            label7.Size = new Size(158, 25);
            label7.TabIndex = 9;
            label7.Text = "Available Rooms :";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(333, 23);
            label6.Name = "label6";
            label6.Size = new Size(100, 25);
            label6.TabIndex = 7;
            label6.Text = "Overview :";
            // 
            // details_picThumbnail
            // 
            details_picThumbnail.BackColor = Color.WhiteSmoke;
            details_picThumbnail.Location = new Point(333, 54);
            details_picThumbnail.Name = "details_picThumbnail";
            details_picThumbnail.Size = new Size(161, 131);
            details_picThumbnail.SizeMode = PictureBoxSizeMode.StretchImage;
            details_picThumbnail.TabIndex = 6;
            details_picThumbnail.TabStop = false;
            // 
            // details_txtAvailableRooms
            // 
            details_txtAvailableRooms.Location = new Point(325, 319);
            details_txtAvailableRooms.Multiline = true;
            details_txtAvailableRooms.Name = "details_txtAvailableRooms";
            details_txtAvailableRooms.Size = new Size(169, 28);
            details_txtAvailableRooms.TabIndex = 8;
            details_txtAvailableRooms.TextChanged += textBox5_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(35, 165);
            label5.Name = "label5";
            label5.Size = new Size(88, 25);
            label5.TabIndex = 5;
            label5.Text = "Address :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(35, 23);
            label3.Name = "label3";
            label3.Size = new Size(99, 25);
            label3.TabIndex = 3;
            label3.Text = "BH Name :";
            // 
            // details_txtTotalRooms
            // 
            details_txtTotalRooms.Location = new Point(325, 262);
            details_txtTotalRooms.Multiline = true;
            details_txtTotalRooms.Name = "details_txtTotalRooms";
            details_txtTotalRooms.Size = new Size(169, 28);
            details_txtTotalRooms.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(325, 234);
            label4.Name = "label4";
            label4.Size = new Size(123, 25);
            label4.TabIndex = 4;
            label4.Text = "Total Rooms :";
            // 
            // details_txtAddress
            // 
            details_txtAddress.Location = new Point(35, 193);
            details_txtAddress.Multiline = true;
            details_txtAddress.Name = "details_txtAddress";
            details_txtAddress.Size = new Size(211, 27);
            details_txtAddress.TabIndex = 2;
            // 
            // details_txtBHName
            // 
            details_txtBHName.Location = new Point(35, 54);
            details_txtBHName.Multiline = true;
            details_txtBHName.Name = "details_txtBHName";
            details_txtBHName.Size = new Size(212, 29);
            details_txtBHName.TabIndex = 0;
            // 
            // mapSingleModal
            // 
            mapSingleModal.BackColor = Color.DarkGray;
            mapSingleModal.Controls.Add(mapSingleWebView);
            mapSingleModal.Location = new Point(3, 0);
            mapSingleModal.Name = "mapSingleModal";
            mapSingleModal.Size = new Size(534, 360);
            mapSingleModal.TabIndex = 18;
            // 
            // mapSingleWebView
            // 
            mapSingleWebView.AllowExternalDrop = true;
            mapSingleWebView.BackColor = Color.WhiteSmoke;
            mapSingleWebView.CreationProperties = null;
            mapSingleWebView.DefaultBackgroundColor = Color.White;
            mapSingleWebView.Dock = DockStyle.Fill;
            mapSingleWebView.Location = new Point(0, 0);
            mapSingleWebView.Margin = new Padding(0);
            mapSingleWebView.Name = "mapSingleWebView";
            mapSingleWebView.Size = new Size(534, 360);
            mapSingleWebView.TabIndex = 11;
            mapSingleWebView.ZoomFactor = 1D;
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button3.BackColor = Color.FromArgb(255, 128, 128);
            button3.FlatStyle = FlatStyle.Flat;
            button3.Location = new Point(486, 3);
            button3.Name = "button3";
            button3.Size = new Size(51, 29);
            button3.TabIndex = 10;
            button3.Text = "X";
            button3.UseVisualStyleBackColor = false;
            button3.Visible = false;
            button3.Click += button3_Click;
            // 
            // rightPanel
            // 
            rightPanel.Controls.Add(mapSingleModal);
            rightPanel.Controls.Add(detailsModal);
            rightPanel.Controls.Add(AddModal);
            rightPanel.Dock = DockStyle.Right;
            rightPanel.Location = new Point(1137, 0);
            rightPanel.Name = "rightPanel";
            rightPanel.Size = new Size(540, 975);
            rightPanel.TabIndex = 13;
            // 
            // AddModal
            // 
            AddModal.BackColor = Color.Gainsboro;
            AddModal.Controls.Add(label11);
            AddModal.Controls.Add(locateBtn);
            AddModal.Controls.Add(txtThumbnailPath);
            AddModal.Controls.Add(label21);
            AddModal.Controls.Add(txtContactNo);
            AddModal.Controls.Add(label20);
            AddModal.Controls.Add(cbOwner);
            AddModal.Controls.Add(addBrowseBtn);
            AddModal.Controls.Add(addNewCloseBtn);
            AddModal.Controls.Add(btnSave);
            AddModal.Controls.Add(btnCancel);
            AddModal.Controls.Add(panel3);
            AddModal.Controls.Add(label2);
            AddModal.Controls.Add(txtLatitude);
            AddModal.Controls.Add(label10);
            AddModal.Controls.Add(txtLongitude);
            AddModal.Controls.Add(label12);
            AddModal.Controls.Add(picThumbnail);
            AddModal.Controls.Add(label13);
            AddModal.Controls.Add(label15);
            AddModal.Controls.Add(txtAddress);
            AddModal.Controls.Add(txtBHName);
            AddModal.ForeColor = SystemColors.ActiveCaptionText;
            AddModal.Location = new Point(3, 365);
            AddModal.Name = "AddModal";
            AddModal.Size = new Size(540, 610);
            AddModal.TabIndex = 9;
            AddModal.Visible = false;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label11.Location = new Point(247, 235);
            label11.Name = "label11";
            label11.Size = new Size(287, 25);
            label11.TabIndex = 33;
            label11.Text = "You can add rooms after Register";
            // 
            // locateBtn
            // 
            locateBtn.BackColor = Color.FromArgb(192, 192, 255);
            locateBtn.FlatStyle = FlatStyle.Flat;
            locateBtn.ForeColor = SystemColors.ActiveCaptionText;
            locateBtn.Location = new Point(48, 497);
            locateBtn.Name = "locateBtn";
            locateBtn.Size = new Size(90, 29);
            locateBtn.TabIndex = 32;
            locateBtn.Text = "Locate";
            locateBtn.UseVisualStyleBackColor = false;
            locateBtn.Click += locateBtn_Click;
            // 
            // txtThumbnailPath
            // 
            txtThumbnailPath.Location = new Point(333, 69);
            txtThumbnailPath.Multiline = true;
            txtThumbnailPath.Name = "txtThumbnailPath";
            txtThumbnailPath.Size = new Size(161, 20);
            txtThumbnailPath.TabIndex = 31;
            txtThumbnailPath.Visible = false;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label21.Location = new Point(39, 107);
            label21.Name = "label21";
            label21.Size = new Size(110, 25);
            label21.TabIndex = 30;
            label21.Text = "Contact No.";
            // 
            // txtContactNo
            // 
            txtContactNo.Location = new Point(40, 135);
            txtContactNo.Multiline = true;
            txtContactNo.Name = "txtContactNo";
            txtContactNo.Size = new Size(211, 29);
            txtContactNo.TabIndex = 29;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label20.Location = new Point(38, 247);
            label20.Name = "label20";
            label20.Size = new Size(77, 25);
            label20.TabIndex = 28;
            label20.Text = "Owner :";
            // 
            // cbOwner
            // 
            cbOwner.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbOwner.AutoCompleteSource = AutoCompleteSource.ListItems;
            cbOwner.FormattingEnabled = true;
            cbOwner.Location = new Point(38, 275);
            cbOwner.Name = "cbOwner";
            cbOwner.Size = new Size(212, 28);
            cbOwner.TabIndex = 27;
            cbOwner.SelectedIndexChanged += cbOwner_SelectedIndexChanged;
            // 
            // addBrowseBtn
            // 
            addBrowseBtn.ForeColor = SystemColors.ActiveCaptionText;
            addBrowseBtn.Location = new Point(333, 203);
            addBrowseBtn.Name = "addBrowseBtn";
            addBrowseBtn.Size = new Size(78, 29);
            addBrowseBtn.TabIndex = 18;
            addBrowseBtn.Text = "Browse";
            addBrowseBtn.UseVisualStyleBackColor = true;
            addBrowseBtn.Click += addBrowseBtn_Click;
            // 
            // addNewCloseBtn
            // 
            addNewCloseBtn.BackColor = Color.FromArgb(255, 128, 128);
            addNewCloseBtn.FlatStyle = FlatStyle.Flat;
            addNewCloseBtn.ForeColor = SystemColors.ActiveCaptionText;
            addNewCloseBtn.Location = new Point(483, 0);
            addNewCloseBtn.Name = "addNewCloseBtn";
            addNewCloseBtn.Size = new Size(52, 29);
            addNewCloseBtn.TabIndex = 17;
            addNewCloseBtn.Text = "X";
            addNewCloseBtn.UseVisualStyleBackColor = false;
            addNewCloseBtn.Visible = false;
            addNewCloseBtn.Click += addNewCloseBtn_Click;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(128, 255, 128);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.ForeColor = SystemColors.ActiveCaptionText;
            btnSave.Location = new Point(344, 549);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(118, 29);
            btnSave.TabIndex = 16;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Location = new Point(79, 549);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(118, 29);
            btnCancel.TabIndex = 15;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // panel3
            // 
            panel3.BackColor = Color.DarkGray;
            panel3.Location = new Point(38, 331);
            panel3.Name = "panel3";
            panel3.Size = new Size(478, 13);
            panel3.TabIndex = 14;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(38, 435);
            label2.Name = "label2";
            label2.Size = new Size(90, 25);
            label2.TabIndex = 13;
            label2.Text = "Latitude :";
            // 
            // txtLatitude
            // 
            txtLatitude.Location = new Point(38, 463);
            txtLatitude.Multiline = true;
            txtLatitude.Name = "txtLatitude";
            txtLatitude.Size = new Size(455, 28);
            txtLatitude.TabIndex = 12;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.Location = new Point(38, 369);
            label10.Name = "label10";
            label10.Size = new Size(107, 25);
            label10.TabIndex = 11;
            label10.Text = "Longitude :";
            // 
            // txtLongitude
            // 
            txtLongitude.Location = new Point(38, 397);
            txtLongitude.Multiline = true;
            txtLongitude.Name = "txtLongitude";
            txtLongitude.Size = new Size(455, 28);
            txtLongitude.TabIndex = 10;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label12.Location = new Point(333, 35);
            label12.Name = "label12";
            label12.Size = new Size(100, 25);
            label12.TabIndex = 7;
            label12.Text = "Overview :";
            // 
            // picThumbnail
            // 
            picThumbnail.BackColor = Color.WhiteSmoke;
            picThumbnail.Location = new Point(333, 66);
            picThumbnail.Name = "picThumbnail";
            picThumbnail.Size = new Size(161, 131);
            picThumbnail.SizeMode = PictureBoxSizeMode.StretchImage;
            picThumbnail.TabIndex = 6;
            picThumbnail.TabStop = false;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label13.Location = new Point(39, 180);
            label13.Name = "label13";
            label13.Size = new Size(88, 25);
            label13.TabIndex = 5;
            label13.Text = "Address :";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label15.Location = new Point(38, 35);
            label15.Name = "label15";
            label15.Size = new Size(99, 25);
            label15.TabIndex = 3;
            label15.Text = "BH Name :";
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(40, 208);
            txtAddress.Multiline = true;
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(212, 29);
            txtAddress.TabIndex = 2;
            // 
            // txtBHName
            // 
            txtBHName.Location = new Point(39, 66);
            txtBHName.Multiline = true;
            txtBHName.Name = "txtBHName";
            txtBHName.Size = new Size(212, 29);
            txtBHName.TabIndex = 0;
            // 
            // contentHostPanel
            // 
            contentHostPanel.Location = new Point(0, 0);
            contentHostPanel.Name = "contentHostPanel";
            contentHostPanel.Size = new Size(200, 100);
            contentHostPanel.TabIndex = 0;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(30, 96);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(205, 27);
            textBox1.TabIndex = 4;
            // 
            // searchbtn
            // 
            searchbtn.Location = new Point(241, 96);
            searchbtn.Name = "searchbtn";
            searchbtn.Size = new Size(94, 29);
            searchbtn.TabIndex = 6;
            searchbtn.Text = "search";
            searchbtn.UseVisualStyleBackColor = true;
            searchbtn.Click += searchbtn_Click_1;
            // 
            // viewMapBtn
            // 
            viewMapBtn.Location = new Point(577, 95);
            viewMapBtn.Name = "viewMapBtn";
            viewMapBtn.Size = new Size(94, 29);
            viewMapBtn.TabIndex = 7;
            viewMapBtn.Text = "View Map";
            viewMapBtn.UseVisualStyleBackColor = true;
            viewMapBtn.Click += viewMapBtn_Click;
            // 
            // addNewBtn
            // 
            addNewBtn.Location = new Point(477, 96);
            addNewBtn.Name = "addNewBtn";
            addNewBtn.Size = new Size(94, 29);
            addNewBtn.TabIndex = 8;
            addNewBtn.Text = "Add +";
            addNewBtn.UseVisualStyleBackColor = true;
            addNewBtn.Click += addNewBtn_Click;
            // 
            // dgvBoardingHouses
            // 
            dgvBoardingHouses.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBoardingHouses.Location = new Point(3, 3);
            dgvBoardingHouses.Name = "dgvBoardingHouses";
            dgvBoardingHouses.RowHeadersWidth = 51;
            dgvBoardingHouses.Size = new Size(635, 349);
            dgvBoardingHouses.TabIndex = 10;
            dgvBoardingHouses.CellClick += dgvBoardingHouses_CellClick;
            // 
            // panel2
            // 
            panel2.Controls.Add(dgvBoardingHouses);
            panel2.Location = new Point(30, 135);
            panel2.Name = "panel2";
            panel2.Size = new Size(641, 352);
            panel2.TabIndex = 11;
            // 
            // mapLocatorModal
            // 
            mapLocatorModal.BackColor = Color.FromArgb(48, 54, 92);
            mapLocatorModal.Controls.Add(button2);
            mapLocatorModal.Controls.Add(label27);
            mapLocatorModal.Controls.Add(panel14);
            mapLocatorModal.Controls.Add(label25);
            mapLocatorModal.Controls.Add(panel8);
            mapLocatorModal.Controls.Add(label23);
            mapLocatorModal.Controls.Add(panel7);
            mapLocatorModal.Controls.Add(button1);
            mapLocatorModal.Controls.Add(mapLocatorWebView);
            mapLocatorModal.Location = new Point(26, 98);
            mapLocatorModal.Name = "mapLocatorModal";
            mapLocatorModal.Size = new Size(1517, 867);
            mapLocatorModal.TabIndex = 11;
            mapLocatorModal.Visible = false;
            mapLocatorModal.Paint += mapLocatorModal_Paint;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(128, 255, 128);
            button2.FlatStyle = FlatStyle.Flat;
            button2.Location = new Point(1224, 749);
            button2.Name = "button2";
            button2.Size = new Size(151, 40);
            button2.TabIndex = 17;
            button2.Text = "Save";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label27.ForeColor = SystemColors.ButtonHighlight;
            label27.Location = new Point(1149, 544);
            label27.Name = "label27";
            label27.Size = new Size(110, 31);
            label27.TabIndex = 15;
            label27.Text = "Latitude :";
            // 
            // panel14
            // 
            panel14.Controls.Add(locateLatTxt);
            panel14.Location = new Point(1136, 581);
            panel14.Name = "panel14";
            panel14.Size = new Size(309, 44);
            panel14.TabIndex = 14;
            // 
            // locateLatTxt
            // 
            locateLatTxt.AutoSize = true;
            locateLatTxt.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            locateLatTxt.ForeColor = SystemColors.ButtonHighlight;
            locateLatTxt.Location = new Point(18, 10);
            locateLatTxt.Name = "locateLatTxt";
            locateLatTxt.Size = new Size(114, 31);
            locateLatTxt.TabIndex = 12;
            locateLatTxt.Text = "(Latitude)";
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label25.ForeColor = SystemColors.ButtonHighlight;
            label25.Location = new Point(1136, 320);
            label25.Name = "label25";
            label25.Size = new Size(131, 31);
            label25.TabIndex = 13;
            label25.Text = "Longitude :";
            // 
            // panel8
            // 
            panel8.Controls.Add(locateLongTxt);
            panel8.Location = new Point(1136, 357);
            panel8.Name = "panel8";
            panel8.Size = new Size(309, 49);
            panel8.TabIndex = 12;
            // 
            // locateLongTxt
            // 
            locateLongTxt.AutoSize = true;
            locateLongTxt.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            locateLongTxt.ForeColor = SystemColors.ButtonHighlight;
            locateLongTxt.Location = new Point(18, 10);
            locateLongTxt.Name = "locateLongTxt";
            locateLongTxt.Size = new Size(135, 31);
            locateLongTxt.TabIndex = 12;
            locateLongTxt.Text = "(Longitude)";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label23.ForeColor = SystemColors.ButtonHighlight;
            label23.Location = new Point(1136, 121);
            label23.Name = "label23";
            label23.Size = new Size(110, 31);
            label23.TabIndex = 11;
            label23.Text = "Address :";
            // 
            // panel7
            // 
            panel7.Controls.Add(locateAddressTxt);
            panel7.Location = new Point(1136, 155);
            panel7.Name = "panel7";
            panel7.Size = new Size(309, 49);
            panel7.TabIndex = 10;
            // 
            // locateAddressTxt
            // 
            locateAddressTxt.AutoSize = true;
            locateAddressTxt.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            locateAddressTxt.ForeColor = SystemColors.ButtonHighlight;
            locateAddressTxt.Location = new Point(18, 10);
            locateAddressTxt.Name = "locateAddressTxt";
            locateAddressTxt.Size = new Size(114, 31);
            locateAddressTxt.TabIndex = 12;
            locateAddressTxt.Text = "(Address)";
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(255, 128, 128);
            button1.FlatStyle = FlatStyle.Flat;
            button1.Location = new Point(1463, 3);
            button1.Name = "button1";
            button1.Size = new Size(51, 29);
            button1.TabIndex = 9;
            button1.Text = "X";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // mapLocatorWebView
            // 
            mapLocatorWebView.AllowExternalDrop = true;
            mapLocatorWebView.CreationProperties = null;
            mapLocatorWebView.DefaultBackgroundColor = Color.White;
            mapLocatorWebView.Location = new Point(17, 18);
            mapLocatorWebView.Name = "mapLocatorWebView";
            mapLocatorWebView.Size = new Size(1070, 827);
            mapLocatorWebView.TabIndex = 0;
            mapLocatorWebView.ZoomFactor = 1D;
            // 
            // mapModal
            // 
            mapModal.BackColor = Color.FromArgb(48, 54, 92);
            mapModal.Controls.Add(mapWebView);
            mapModal.Controls.Add(closemapBtn);
            mapModal.Controls.Add(panel6);
            mapModal.Controls.Add(panel5);
            mapModal.Controls.Add(panel4);
            mapModal.Location = new Point(3, 80);
            mapModal.Name = "mapModal";
            mapModal.Size = new Size(1585, 895);
            mapModal.TabIndex = 12;
            mapModal.Visible = false;
            mapModal.Paint += mapModal_Paint;
            // 
            // mapWebView
            // 
            mapWebView.AllowExternalDrop = true;
            mapWebView.CreationProperties = null;
            mapWebView.DefaultBackgroundColor = Color.White;
            mapWebView.Location = new Point(17, 18);
            mapWebView.Name = "mapWebView";
            mapWebView.Size = new Size(1149, 858);
            mapWebView.TabIndex = 9;
            mapWebView.ZoomFactor = 1D;
            // 
            // closemapBtn
            // 
            closemapBtn.BackColor = Color.FromArgb(255, 192, 192);
            closemapBtn.FlatStyle = FlatStyle.Flat;
            closemapBtn.Location = new Point(1531, 3);
            closemapBtn.Name = "closemapBtn";
            closemapBtn.Size = new Size(51, 29);
            closemapBtn.TabIndex = 8;
            closemapBtn.Text = "X";
            closemapBtn.UseVisualStyleBackColor = false;
            closemapBtn.Click += closemapBtn_Click;
            // 
            // panel6
            // 
            panel6.BackColor = Color.WhiteSmoke;
            panel6.Controls.Add(totalInactive);
            panel6.Controls.Add(label26);
            panel6.Location = new Point(1236, 610);
            panel6.Name = "panel6";
            panel6.Size = new Size(277, 185);
            panel6.TabIndex = 12;
            // 
            // totalInactive
            // 
            totalInactive.AutoSize = true;
            totalInactive.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            totalInactive.Location = new Point(112, 94);
            totalInactive.Name = "totalInactive";
            totalInactive.Size = new Size(52, 41);
            totalInactive.TabIndex = 3;
            totalInactive.Text = "00";
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label26.Location = new Point(89, 26);
            label26.Name = "label26";
            label26.Size = new Size(107, 31);
            label26.TabIndex = 2;
            label26.Text = "Inactive :";
            // 
            // panel5
            // 
            panel5.BackColor = Color.WhiteSmoke;
            panel5.Controls.Add(totalActive);
            panel5.Controls.Add(label24);
            panel5.Location = new Point(1236, 333);
            panel5.Name = "panel5";
            panel5.Size = new Size(277, 185);
            panel5.TabIndex = 11;
            // 
            // totalActive
            // 
            totalActive.AutoSize = true;
            totalActive.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            totalActive.Location = new Point(112, 96);
            totalActive.Name = "totalActive";
            totalActive.Size = new Size(52, 41);
            totalActive.TabIndex = 3;
            totalActive.Text = "00";
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label24.Location = new Point(96, 24);
            label24.Name = "label24";
            label24.Size = new Size(90, 31);
            label24.TabIndex = 2;
            label24.Text = "Active :";
            // 
            // panel4
            // 
            panel4.BackColor = Color.WhiteSmoke;
            panel4.Controls.Add(totalBH);
            panel4.Controls.Add(label22);
            panel4.Location = new Point(1236, 53);
            panel4.Name = "panel4";
            panel4.Size = new Size(277, 185);
            panel4.TabIndex = 10;
            // 
            // totalBH
            // 
            totalBH.AutoSize = true;
            totalBH.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            totalBH.Location = new Point(112, 76);
            totalBH.Name = "totalBH";
            totalBH.Size = new Size(52, 41);
            totalBH.TabIndex = 1;
            totalBH.Text = "00";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label22.Location = new Point(5, 13);
            label22.Name = "label22";
            label22.Size = new Size(255, 31);
            label22.TabIndex = 0;
            label22.Text = "Total Boarding Houses:";
            label22.Click += label22_Click;
            // 
            // roomsHostPanel
            // 
            roomsHostPanel.BackColor = Color.Gainsboro;
            roomsHostPanel.Location = new Point(38, 519);
            roomsHostPanel.Name = "roomsHostPanel";
            roomsHostPanel.Size = new Size(1092, 424);
            roomsHostPanel.TabIndex = 14;
            // 
            // panel9
            // 
            panel9.BackColor = Color.Gainsboro;
            panel9.Controls.Add(label1);
            panel9.Controls.Add(textBox1);
            panel9.Controls.Add(searchbtn);
            panel9.Controls.Add(viewMapBtn);
            panel9.Controls.Add(panel2);
            panel9.Controls.Add(addNewBtn);
            panel9.Location = new Point(37, 3);
            panel9.Name = "panel9";
            panel9.Size = new Size(687, 510);
            panel9.TabIndex = 15;
            // 
            // bhStatsContainer
            // 
            bhStatsContainer.BackColor = Color.Gainsboro;
            bhStatsContainer.Controls.Add(panel13);
            bhStatsContainer.Controls.Add(panel12);
            bhStatsContainer.Controls.Add(panel11);
            bhStatsContainer.Controls.Add(panel10);
            bhStatsContainer.Location = new Point(730, 3);
            bhStatsContainer.Name = "bhStatsContainer";
            bhStatsContainer.Size = new Size(401, 508);
            bhStatsContainer.TabIndex = 16;
            // 
            // panel13
            // 
            panel13.Controls.Add(panel17);
            panel13.Controls.Add(panel16);
            panel13.Controls.Add(panel15);
            panel13.Location = new Point(3, 377);
            panel13.Name = "panel13";
            panel13.Size = new Size(395, 125);
            panel13.TabIndex = 3;
            // 
            // panel17
            // 
            panel17.BackColor = Color.FromArgb(128, 255, 128);
            panel17.Controls.Add(totalAvailableRoomsTxt);
            panel17.Controls.Add(label33);
            panel17.Location = new Point(282, 9);
            panel17.Name = "panel17";
            panel17.Size = new Size(98, 107);
            panel17.TabIndex = 2;
            // 
            // totalAvailableRoomsTxt
            // 
            totalAvailableRoomsTxt.AutoSize = true;
            totalAvailableRoomsTxt.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            totalAvailableRoomsTxt.Location = new Point(29, 56);
            totalAvailableRoomsTxt.Name = "totalAvailableRoomsTxt";
            totalAvailableRoomsTxt.Size = new Size(23, 28);
            totalAvailableRoomsTxt.TabIndex = 9;
            totalAvailableRoomsTxt.Text = "0";
            // 
            // label33
            // 
            label33.AutoSize = true;
            label33.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label33.Location = new Point(3, 9);
            label33.Name = "label33";
            label33.Size = new Size(91, 25);
            label33.TabIndex = 8;
            label33.Text = "Available:";
            // 
            // panel16
            // 
            panel16.BackColor = Color.FromArgb(255, 255, 128);
            panel16.Controls.Add(tootalRoomsOccupiedTxt);
            panel16.Controls.Add(label31);
            panel16.Location = new Point(148, 9);
            panel16.Name = "panel16";
            panel16.Size = new Size(98, 107);
            panel16.TabIndex = 1;
            // 
            // tootalRoomsOccupiedTxt
            // 
            tootalRoomsOccupiedTxt.AutoSize = true;
            tootalRoomsOccupiedTxt.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tootalRoomsOccupiedTxt.Location = new Point(31, 55);
            tootalRoomsOccupiedTxt.Name = "tootalRoomsOccupiedTxt";
            tootalRoomsOccupiedTxt.Size = new Size(23, 28);
            tootalRoomsOccupiedTxt.TabIndex = 9;
            tootalRoomsOccupiedTxt.Text = "0";
            // 
            // label31
            // 
            label31.AutoSize = true;
            label31.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label31.Location = new Point(3, 8);
            label31.Name = "label31";
            label31.Size = new Size(94, 25);
            label31.TabIndex = 8;
            label31.Text = "Occupied:";
            // 
            // panel15
            // 
            panel15.BackColor = Color.FromArgb(128, 128, 255);
            panel15.Controls.Add(totalRoomsTxt);
            panel15.Controls.Add(label16);
            panel15.Location = new Point(14, 8);
            panel15.Name = "panel15";
            panel15.Size = new Size(98, 107);
            panel15.TabIndex = 0;
            // 
            // totalRoomsTxt
            // 
            totalRoomsTxt.AutoSize = true;
            totalRoomsTxt.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            totalRoomsTxt.Location = new Point(32, 53);
            totalRoomsTxt.Name = "totalRoomsTxt";
            totalRoomsTxt.Size = new Size(23, 28);
            totalRoomsTxt.TabIndex = 9;
            totalRoomsTxt.Text = "0";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label16.Location = new Point(3, 9);
            label16.Name = "label16";
            label16.Size = new Size(73, 25);
            label16.TabIndex = 8;
            label16.Text = "Rooms:";
            // 
            // panel12
            // 
            panel12.BackColor = Color.White;
            panel12.Controls.Add(activeRenatlsTxt);
            panel12.Controls.Add(label30);
            panel12.Location = new Point(46, 253);
            panel12.Name = "panel12";
            panel12.Size = new Size(306, 102);
            panel12.TabIndex = 2;
            // 
            // activeRenatlsTxt
            // 
            activeRenatlsTxt.AutoSize = true;
            activeRenatlsTxt.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            activeRenatlsTxt.Location = new Point(136, 67);
            activeRenatlsTxt.Name = "activeRenatlsTxt";
            activeRenatlsTxt.Size = new Size(23, 28);
            activeRenatlsTxt.TabIndex = 2;
            activeRenatlsTxt.Text = "0";
            // 
            // label30
            // 
            label30.AutoSize = true;
            label30.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label30.Location = new Point(3, 8);
            label30.Name = "label30";
            label30.Size = new Size(222, 41);
            label30.TabIndex = 1;
            label30.Text = "Active Rentals :";
            // 
            // panel11
            // 
            panel11.BackColor = Color.White;
            panel11.Controls.Add(thisMontEarningsTxt);
            panel11.Controls.Add(label28);
            panel11.Location = new Point(46, 135);
            panel11.Name = "panel11";
            panel11.Size = new Size(306, 102);
            panel11.TabIndex = 1;
            // 
            // thisMontEarningsTxt
            // 
            thisMontEarningsTxt.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            thisMontEarningsTxt.Location = new Point(99, 60);
            thisMontEarningsTxt.Name = "thisMontEarningsTxt";
            thisMontEarningsTxt.Size = new Size(68, 28);
            thisMontEarningsTxt.TabIndex = 2;
            thisMontEarningsTxt.Text = "₱ 0.00";
            thisMontEarningsTxt.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label28.Location = new Point(3, 10);
            label28.Name = "label28";
            label28.Size = new Size(183, 41);
            label28.TabIndex = 1;
            label28.Text = "This Month :";
            // 
            // panel10
            // 
            panel10.BackColor = Color.White;
            panel10.Controls.Add(earningsTxt);
            panel10.Controls.Add(label14);
            panel10.Location = new Point(46, 13);
            panel10.Name = "panel10";
            panel10.Size = new Size(306, 102);
            panel10.TabIndex = 0;
            // 
            // earningsTxt
            // 
            earningsTxt.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            earningsTxt.Location = new Point(99, 51);
            earningsTxt.Name = "earningsTxt";
            earningsTxt.Size = new Size(81, 35);
            earningsTxt.TabIndex = 2;
            earningsTxt.Text = "₱ 0.00";
            earningsTxt.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label14.Location = new Point(3, 10);
            label14.Name = "label14";
            label14.Size = new Size(227, 41);
            label14.TabIndex = 1;
            label14.Text = "Total Earnings :";
            // 
            // BoardingHousesView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(mapModal);
            Controls.Add(mapLocatorModal);
            Controls.Add(bhStatsContainer);
            Controls.Add(panel9);
            Controls.Add(rightPanel);
            Controls.Add(roomsHostPanel);
            Name = "BoardingHousesView";
            Size = new Size(1677, 975);
            Load += BoardingHousesView_Load;
            detailsModal.ResumeLayout(false);
            detailsModal.PerformLayout();
            ((ISupportInitialize)details_picThumbnail).EndInit();
            mapSingleModal.ResumeLayout(false);
            ((ISupportInitialize)mapSingleWebView).EndInit();
            rightPanel.ResumeLayout(false);
            AddModal.ResumeLayout(false);
            AddModal.PerformLayout();
            ((ISupportInitialize)picThumbnail).EndInit();
            ((ISupportInitialize)dgvBoardingHouses).EndInit();
            panel2.ResumeLayout(false);
            mapLocatorModal.ResumeLayout(false);
            mapLocatorModal.PerformLayout();
            panel14.ResumeLayout(false);
            panel14.PerformLayout();
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            ((ISupportInitialize)mapLocatorWebView).EndInit();
            mapModal.ResumeLayout(false);
            ((ISupportInitialize)mapWebView).EndInit();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel9.ResumeLayout(false);
            panel9.PerformLayout();
            bhStatsContainer.ResumeLayout(false);
            panel13.ResumeLayout(false);
            panel17.ResumeLayout(false);
            panel17.PerformLayout();
            panel16.ResumeLayout(false);
            panel16.PerformLayout();
            panel15.ResumeLayout(false);
            panel15.PerformLayout();
            panel12.ResumeLayout(false);
            panel12.PerformLayout();
            panel11.ResumeLayout(false);
            panel11.PerformLayout();
            panel10.ResumeLayout(false);
            panel10.PerformLayout();
            ResumeLayout(false);
        }
        private Label label1;
        private Panel detailsModal;
        private TextBox textBox1;
        private Button searchbtn;
        private Button viewMapBtn;
        private Button addNewBtn;
        private TextBox details_txtAddress;
        private TextBox details_txtTotalRooms;
        private TextBox details_txtBHName;
        private Label label3;
        private Label label4;
        private Panel panel1;
        private Label label9;
        private TextBox details_txtLongitude;
        private Label label8;
        private TextBox details_txtLatitude;
        private Label label7;
        private TextBox details_txtAvailableRooms;
        private Label label6;
        private PictureBox details_picThumbnail;
        private Label label5;
        private Button btnUpdate;
        private Button btnDelete;
        private Button detailsClosebtn;
        private Panel AddModal;
        private Button addNewCloseBtn;
        private Button btnSave;
        private Button btnCancel;
        private Panel panel3;
        private Label label2;
        private TextBox txtLatitude;
        private Label label10;
        private TextBox txtLongitude;
        private Label label12;
        private PictureBox picThumbnail;
        private Label label13;
        private Label label15;
        private TextBox txtAddress;
        private TextBox txtBHName;
        private Button editBrowseBtn;
        private Button addBrowseBtn;
        private Label label17;
        private Label label19;
        private ComboBox details_cbOwner;
        private Label label18;
        private TextBox details_txtContactNo;
        private ComboBox details_cbstatus;
        private Label label21;
        private TextBox txtContactNo;
        private Label label20;
        private ComboBox cbOwner;
        private TextBox txtThumbnailPath;
        private DataGridView dgvBoardingHouses;
        private Panel panel2;
        private TextBox details_txtThumbnailPath;
        private Panel mapModal;
        private Microsoft.Web.WebView2.WinForms.WebView2 mapWebView;
        private Button closemapBtn;
        private Panel panel5;
        private Panel panel4;
        private Panel panel6;
        private Label label22;
        private Label totalInactive;
        private Label label26;
        private Label totalActive;
        private Label label24;
        private Label totalBH;
        private Button locateBtn;
        private Panel mapLocatorModal;
        private Button button1;
        private Microsoft.Web.WebView2.WinForms.WebView2 mapLocatorWebView;
        private Label label27;
        private Panel panel14;
        private Label locateLatTxt;
        private Label label25;
        private Panel panel8;
        private Label locateLongTxt;
        private Label label23;
        private Panel panel7;
        private Label locateAddressTxt;
        private Button button2;
        private Panel mapSingleModal;
        private Microsoft.Web.WebView2.WinForms.WebView2 mapSingleWebView;
        private Button button3;
        private Panel rightPanel;
        private Panel contentHostPanel;
        private Label label11;
        private Button viewRoomsBtn;
        private SplitContainer splitRooms;
        private FlowLayoutPanel flpRooms;
        private DataGridView dgvRoomTenants;
        private Label lblRoomTitle;
        private Label lblRoomMeta;
        private Label lblOccupancy;
        private Panel roomsLeftHeaderPanel;
        private Panel roomsRightHeaderPanel;
        private Panel roomsHostPanel;
        private Panel panel9;
        private Panel bhStatsContainer;
        private Panel panel10;
        private Label earningsTxt;
        private Label label14;
        private Panel panel13;
        private Panel panel12;
        private Label activeRenatlsTxt;
        private Label label30;
        private Panel panel11;
        private Label thisMontEarningsTxt;
        private Label label28;
        private Panel panel17;
        private Label totalAvailableRoomsTxt;
        private Label label33;
        private Panel panel16;
        private Label tootalRoomsOccupiedTxt;
        private Label label31;
        private Panel panel15;
        private Label totalRoomsTxt;
        private Label label16;
    }
}

