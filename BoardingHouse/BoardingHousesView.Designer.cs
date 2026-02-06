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
            roomManageBtn = new Button();
            details_txtThumbnailPath = new TextBox();
            label19 = new Label();
            details_txtOwnerName = new TextBox();
            label18 = new Label();
            details_txtContactNo = new TextBox();
            details_cbstatus = new ComboBox();
            label17 = new Label();
            label16 = new Label();
            editBrowseBtn = new Button();
            btnDelete = new Button();
            detailsClosebtn = new Button();
            btnViewMap = new Button();
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
            textBox1 = new TextBox();
            searchbtn = new Button();
            viewMapBtn = new Button();
            addNewBtn = new Button();
            AddModal = new Panel();
            label11 = new Label();
            locateBtn = new Button();
            txtThumbnailPath = new TextBox();
            label21 = new Label();
            txtContactNo = new TextBox();
            label20 = new Label();
            txtOwnerName = new TextBox();
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
            dgvBoardingHouses = new DataGridView();
            panel2 = new Panel();
            mapSingleModal = new Panel();
            mapSingleWebView = new Microsoft.Web.WebView2.WinForms.WebView2();
            button3 = new Button();
            mapModal = new Panel();
            panel6 = new Panel();
            totalInactive = new Label();
            label26 = new Label();
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
            panel5 = new Panel();
            totalActive = new Label();
            label24 = new Label();
            panel4 = new Panel();
            totalBH = new Label();
            label22 = new Label();
            mapWebView = new Microsoft.Web.WebView2.WinForms.WebView2();
            closemapBtn = new Button();
            manageRoomsModal = new Panel();
            panel9 = new Panel();
            btnMarkInactive = new Button();
            btnMarkMaintenance = new Button();
            btnMarkOccupied = new Button();
            btnMarkAvailable = new Button();
            dgvRooms = new DataGridView();
            lblRoomsFor = new Label();
            closeManageRoom = new Button();
            detailsModal.SuspendLayout();
            ((ISupportInitialize)details_picThumbnail).BeginInit();
            AddModal.SuspendLayout();
            ((ISupportInitialize)picThumbnail).BeginInit();
            ((ISupportInitialize)dgvBoardingHouses).BeginInit();
            panel2.SuspendLayout();
            mapSingleModal.SuspendLayout();
            ((ISupportInitialize)mapSingleWebView).BeginInit();
            mapModal.SuspendLayout();
            panel6.SuspendLayout();
            mapLocatorModal.SuspendLayout();
            panel14.SuspendLayout();
            panel8.SuspendLayout();
            panel7.SuspendLayout();
            ((ISupportInitialize)mapLocatorWebView).BeginInit();
            panel5.SuspendLayout();
            panel4.SuspendLayout();
            ((ISupportInitialize)mapWebView).BeginInit();
            manageRoomsModal.SuspendLayout();
            panel9.SuspendLayout();
            ((ISupportInitialize)dgvRooms).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.Location = new Point(38, 28);
            label1.Name = "label1";
            label1.Size = new Size(246, 41);
            label1.TabIndex = 0;
            label1.Text = "Boarding Houses";
            // 
            // detailsModal
            // 
            detailsModal.AutoSize = true;
            detailsModal.BackColor = Color.Silver;
            detailsModal.Controls.Add(roomManageBtn);
            detailsModal.Controls.Add(details_txtThumbnailPath);
            detailsModal.Controls.Add(label19);
            detailsModal.Controls.Add(details_txtOwnerName);
            detailsModal.Controls.Add(label18);
            detailsModal.Controls.Add(details_txtContactNo);
            detailsModal.Controls.Add(details_cbstatus);
            detailsModal.Controls.Add(label17);
            detailsModal.Controls.Add(label16);
            detailsModal.Controls.Add(editBrowseBtn);
            detailsModal.Controls.Add(btnDelete);
            detailsModal.Controls.Add(detailsClosebtn);
            detailsModal.Controls.Add(btnViewMap);
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
            detailsModal.Location = new Point(352, 111);
            detailsModal.Name = "detailsModal";
            detailsModal.Size = new Size(518, 811);
            detailsModal.TabIndex = 2;
            detailsModal.Visible = false;
            detailsModal.Paint += panel1_Paint;
            // 
            // roomManageBtn
            // 
            roomManageBtn.Location = new Point(323, 285);
            roomManageBtn.Name = "roomManageBtn";
            roomManageBtn.Size = new Size(123, 29);
            roomManageBtn.TabIndex = 34;
            roomManageBtn.Text = "Manage Rooms";
            roomManageBtn.UseVisualStyleBackColor = true;
            roomManageBtn.Click += button4_Click;
            // 
            // details_txtThumbnailPath
            // 
            details_txtThumbnailPath.Location = new Point(323, 106);
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
            label19.Location = new Point(27, 333);
            label19.Name = "label19";
            label19.Size = new Size(77, 25);
            label19.TabIndex = 26;
            label19.Text = "Owner :";
            // 
            // details_txtOwnerName
            // 
            details_txtOwnerName.Location = new Point(27, 361);
            details_txtOwnerName.Multiline = true;
            details_txtOwnerName.Name = "details_txtOwnerName";
            details_txtOwnerName.Size = new Size(212, 41);
            details_txtOwnerName.TabIndex = 25;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label18.Location = new Point(28, 183);
            label18.Name = "label18";
            label18.Size = new Size(110, 25);
            label18.TabIndex = 24;
            label18.Text = "Contact No.";
            // 
            // details_txtContactNo
            // 
            details_txtContactNo.Location = new Point(28, 211);
            details_txtContactNo.Multiline = true;
            details_txtContactNo.Name = "details_txtContactNo";
            details_txtContactNo.Size = new Size(186, 41);
            details_txtContactNo.TabIndex = 23;
            // 
            // details_cbstatus
            // 
            details_cbstatus.FormattingEnabled = true;
            details_cbstatus.Location = new Point(102, 408);
            details_cbstatus.Name = "details_cbstatus";
            details_cbstatus.Size = new Size(139, 28);
            details_cbstatus.TabIndex = 22;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label17.Location = new Point(27, 408);
            label17.Name = "label17";
            label17.Size = new Size(72, 25);
            label17.TabIndex = 21;
            label17.Text = "Status :";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label16.Location = new Point(208, 456);
            label16.Name = "label16";
            label16.Size = new Size(90, 25);
            label16.TabIndex = 20;
            label16.Text = "Latitude :";
            // 
            // editBrowseBtn
            // 
            editBrowseBtn.Location = new Point(323, 243);
            editBrowseBtn.Name = "editBrowseBtn";
            editBrowseBtn.Size = new Size(78, 29);
            editBrowseBtn.TabIndex = 19;
            editBrowseBtn.Text = "Browse";
            editBrowseBtn.UseVisualStyleBackColor = true;
            editBrowseBtn.Click += editBrowseBtn_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(255, 128, 128);
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Location = new Point(27, 702);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(118, 40);
            btnDelete.TabIndex = 18;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // detailsClosebtn
            // 
            detailsClosebtn.BackColor = Color.FromArgb(255, 128, 128);
            detailsClosebtn.FlatStyle = FlatStyle.Flat;
            detailsClosebtn.Location = new Point(462, 3);
            detailsClosebtn.Name = "detailsClosebtn";
            detailsClosebtn.Size = new Size(53, 29);
            detailsClosebtn.TabIndex = 17;
            detailsClosebtn.Text = "X";
            detailsClosebtn.UseVisualStyleBackColor = false;
            detailsClosebtn.Click += detailsClosebtn_Click;
            // 
            // btnViewMap
            // 
            btnViewMap.BackColor = Color.FromArgb(192, 192, 255);
            btnViewMap.FlatStyle = FlatStyle.Flat;
            btnViewMap.Location = new Point(364, 702);
            btnViewMap.Name = "btnViewMap";
            btnViewMap.Size = new Size(118, 40);
            btnViewMap.TabIndex = 16;
            btnViewMap.Text = "View Map";
            btnViewMap.UseVisualStyleBackColor = false;
            btnViewMap.Click += btnViewMap_Click_2;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.FromArgb(255, 255, 128);
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.Location = new Point(196, 702);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(118, 40);
            btnUpdate.TabIndex = 15;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.DarkGray;
            panel1.Location = new Point(14, 481);
            panel1.Name = "panel1";
            panel1.Size = new Size(478, 13);
            panel1.TabIndex = 14;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(27, 592);
            label9.Name = "label9";
            label9.Size = new Size(90, 25);
            label9.TabIndex = 13;
            label9.Text = "Latitude :";
            // 
            // details_txtLongitude
            // 
            details_txtLongitude.Location = new Point(27, 620);
            details_txtLongitude.Multiline = true;
            details_txtLongitude.Name = "details_txtLongitude";
            details_txtLongitude.Size = new Size(455, 41);
            details_txtLongitude.TabIndex = 12;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.Location = new Point(27, 511);
            label8.Name = "label8";
            label8.Size = new Size(107, 25);
            label8.TabIndex = 11;
            label8.Text = "Longitude :";
            // 
            // details_txtLatitude
            // 
            details_txtLatitude.Location = new Point(27, 539);
            details_txtLatitude.Multiline = true;
            details_txtLatitude.Name = "details_txtLatitude";
            details_txtLatitude.Size = new Size(455, 41);
            details_txtLatitude.TabIndex = 10;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(323, 381);
            label7.Name = "label7";
            label7.Size = new Size(158, 25);
            label7.TabIndex = 9;
            label7.Text = "Available Rooms :";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(323, 75);
            label6.Name = "label6";
            label6.Size = new Size(100, 25);
            label6.TabIndex = 7;
            label6.Text = "Overview :";
            // 
            // details_picThumbnail
            // 
            details_picThumbnail.Location = new Point(323, 106);
            details_picThumbnail.Name = "details_picThumbnail";
            details_picThumbnail.Size = new Size(161, 131);
            details_picThumbnail.SizeMode = PictureBoxSizeMode.StretchImage;
            details_picThumbnail.TabIndex = 6;
            details_picThumbnail.TabStop = false;
            // 
            // details_txtAvailableRooms
            // 
            details_txtAvailableRooms.Location = new Point(323, 409);
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
            label5.Location = new Point(29, 266);
            label5.Name = "label5";
            label5.Size = new Size(88, 25);
            label5.TabIndex = 5;
            label5.Text = "Address :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(29, 75);
            label3.Name = "label3";
            label3.Size = new Size(99, 25);
            label3.TabIndex = 3;
            label3.Text = "BH Name :";
            // 
            // details_txtTotalRooms
            // 
            details_txtTotalRooms.Location = new Point(323, 352);
            details_txtTotalRooms.Multiline = true;
            details_txtTotalRooms.Name = "details_txtTotalRooms";
            details_txtTotalRooms.Size = new Size(169, 28);
            details_txtTotalRooms.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(323, 324);
            label4.Name = "label4";
            label4.Size = new Size(123, 25);
            label4.TabIndex = 4;
            label4.Text = "Total Rooms :";
            // 
            // details_txtAddress
            // 
            details_txtAddress.Location = new Point(29, 294);
            details_txtAddress.Multiline = true;
            details_txtAddress.Name = "details_txtAddress";
            details_txtAddress.Size = new Size(185, 41);
            details_txtAddress.TabIndex = 2;
            // 
            // details_txtBHName
            // 
            details_txtBHName.Location = new Point(29, 106);
            details_txtBHName.Multiline = true;
            details_txtBHName.Name = "details_txtBHName";
            details_txtBHName.Size = new Size(212, 41);
            details_txtBHName.TabIndex = 0;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(38, 111);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(205, 27);
            textBox1.TabIndex = 4;
            // 
            // searchbtn
            // 
            searchbtn.Location = new Point(249, 111);
            searchbtn.Name = "searchbtn";
            searchbtn.Size = new Size(94, 29);
            searchbtn.TabIndex = 6;
            searchbtn.Text = "search";
            searchbtn.UseVisualStyleBackColor = true;
            searchbtn.Click += searchbtn_Click_1;
            // 
            // viewMapBtn
            // 
            viewMapBtn.Location = new Point(1551, 109);
            viewMapBtn.Name = "viewMapBtn";
            viewMapBtn.Size = new Size(94, 29);
            viewMapBtn.TabIndex = 7;
            viewMapBtn.Text = "View Map";
            viewMapBtn.UseVisualStyleBackColor = true;
            viewMapBtn.Click += viewMapBtn_Click;
            // 
            // addNewBtn
            // 
            addNewBtn.Location = new Point(1440, 109);
            addNewBtn.Name = "addNewBtn";
            addNewBtn.Size = new Size(94, 29);
            addNewBtn.TabIndex = 8;
            addNewBtn.Text = "Add +";
            addNewBtn.UseVisualStyleBackColor = true;
            addNewBtn.Click += addNewBtn_Click;
            // 
            // AddModal
            // 
            AddModal.AutoSize = true;
            AddModal.BackColor = Color.Silver;
            AddModal.Controls.Add(label11);
            AddModal.Controls.Add(locateBtn);
            AddModal.Controls.Add(txtThumbnailPath);
            AddModal.Controls.Add(label21);
            AddModal.Controls.Add(txtContactNo);
            AddModal.Controls.Add(label20);
            AddModal.Controls.Add(txtOwnerName);
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
            AddModal.Location = new Point(876, 111);
            AddModal.Name = "AddModal";
            AddModal.Size = new Size(557, 811);
            AddModal.TabIndex = 9;
            AddModal.Visible = false;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label11.Location = new Point(267, 333);
            label11.Name = "label11";
            label11.Size = new Size(287, 25);
            label11.TabIndex = 33;
            label11.Text = "You can add rooms after Register";
            // 
            // locateBtn
            // 
            locateBtn.BackColor = Color.FromArgb(192, 192, 255);
            locateBtn.FlatStyle = FlatStyle.Flat;
            locateBtn.Location = new Point(29, 676);
            locateBtn.Name = "locateBtn";
            locateBtn.Size = new Size(90, 29);
            locateBtn.TabIndex = 32;
            locateBtn.Text = "Locate";
            locateBtn.UseVisualStyleBackColor = false;
            locateBtn.Click += locateBtn_Click;
            // 
            // txtThumbnailPath
            // 
            txtThumbnailPath.Location = new Point(323, 109);
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
            label21.Location = new Point(28, 266);
            label21.Name = "label21";
            label21.Size = new Size(110, 25);
            label21.TabIndex = 30;
            label21.Text = "Contact No.";
            // 
            // txtContactNo
            // 
            txtContactNo.Location = new Point(28, 294);
            txtContactNo.Multiline = true;
            txtContactNo.Name = "txtContactNo";
            txtContactNo.Size = new Size(211, 41);
            txtContactNo.TabIndex = 29;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label20.Location = new Point(28, 168);
            label20.Name = "label20";
            label20.Size = new Size(77, 25);
            label20.TabIndex = 28;
            label20.Text = "Owner :";
            // 
            // txtOwnerName
            // 
            txtOwnerName.Location = new Point(28, 196);
            txtOwnerName.Multiline = true;
            txtOwnerName.Name = "txtOwnerName";
            txtOwnerName.Size = new Size(212, 41);
            txtOwnerName.TabIndex = 27;
            // 
            // addBrowseBtn
            // 
            addBrowseBtn.Location = new Point(323, 243);
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
            addNewCloseBtn.Location = new Point(502, 3);
            addNewCloseBtn.Name = "addNewCloseBtn";
            addNewCloseBtn.Size = new Size(52, 29);
            addNewCloseBtn.TabIndex = 17;
            addNewCloseBtn.Text = "X";
            addNewCloseBtn.UseVisualStyleBackColor = false;
            addNewCloseBtn.Click += addNewCloseBtn_Click;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(128, 255, 128);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Location = new Point(272, 739);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(151, 40);
            btnSave.TabIndex = 16;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Location = new Point(78, 739);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(151, 40);
            btnCancel.TabIndex = 15;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // panel3
            // 
            panel3.BackColor = Color.DarkGray;
            panel3.Location = new Point(16, 484);
            panel3.Name = "panel3";
            panel3.Size = new Size(478, 13);
            panel3.TabIndex = 14;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(29, 595);
            label2.Name = "label2";
            label2.Size = new Size(90, 25);
            label2.TabIndex = 13;
            label2.Text = "Latitude :";
            // 
            // txtLatitude
            // 
            txtLatitude.Location = new Point(29, 623);
            txtLatitude.Multiline = true;
            txtLatitude.Name = "txtLatitude";
            txtLatitude.Size = new Size(455, 41);
            txtLatitude.TabIndex = 12;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.Location = new Point(29, 514);
            label10.Name = "label10";
            label10.Size = new Size(107, 25);
            label10.TabIndex = 11;
            label10.Text = "Longitude :";
            // 
            // txtLongitude
            // 
            txtLongitude.Location = new Point(29, 542);
            txtLongitude.Multiline = true;
            txtLongitude.Name = "txtLongitude";
            txtLongitude.Size = new Size(455, 41);
            txtLongitude.TabIndex = 10;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label12.Location = new Point(323, 75);
            label12.Name = "label12";
            label12.Size = new Size(100, 25);
            label12.TabIndex = 7;
            label12.Text = "Overview :";
            // 
            // picThumbnail
            // 
            picThumbnail.Location = new Point(323, 106);
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
            label13.Location = new Point(29, 364);
            label13.Name = "label13";
            label13.Size = new Size(88, 25);
            label13.TabIndex = 5;
            label13.Text = "Address :";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label15.Location = new Point(29, 75);
            label15.Name = "label15";
            label15.Size = new Size(99, 25);
            label15.TabIndex = 3;
            label15.Text = "BH Name :";
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(29, 392);
            txtAddress.Multiline = true;
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(212, 41);
            txtAddress.TabIndex = 2;
            // 
            // txtBHName
            // 
            txtBHName.Location = new Point(29, 106);
            txtBHName.Multiline = true;
            txtBHName.Name = "txtBHName";
            txtBHName.Size = new Size(212, 41);
            txtBHName.TabIndex = 0;
            // 
            // dgvBoardingHouses
            // 
            dgvBoardingHouses.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBoardingHouses.Location = new Point(3, 3);
            dgvBoardingHouses.Name = "dgvBoardingHouses";
            dgvBoardingHouses.RowHeadersWidth = 51;
            dgvBoardingHouses.Size = new Size(1613, 697);
            dgvBoardingHouses.TabIndex = 10;
            dgvBoardingHouses.CellClick += dgvBoardingHouses_CellClick;
            // 
            // panel2
            // 
            panel2.Controls.Add(dgvBoardingHouses);
            panel2.Location = new Point(38, 164);
            panel2.Name = "panel2";
            panel2.Size = new Size(1616, 700);
            panel2.TabIndex = 11;
            // 
            // mapSingleModal
            // 
            mapSingleModal.BackColor = Color.FromArgb(48, 54, 92);
            mapSingleModal.Controls.Add(mapSingleWebView);
            mapSingleModal.Controls.Add(button3);
            mapSingleModal.Location = new Point(226, 87);
            mapSingleModal.Name = "mapSingleModal";
            mapSingleModal.Size = new Size(1293, 770);
            mapSingleModal.TabIndex = 18;
            mapSingleModal.Visible = false;
            mapSingleModal.Paint += mapSingleModal_Paint;
            // 
            // mapSingleWebView
            // 
            mapSingleWebView.AllowExternalDrop = true;
            mapSingleWebView.CreationProperties = null;
            mapSingleWebView.DefaultBackgroundColor = Color.White;
            mapSingleWebView.Location = new Point(28, 63);
            mapSingleWebView.Name = "mapSingleWebView";
            mapSingleWebView.Size = new Size(1235, 679);
            mapSingleWebView.TabIndex = 11;
            mapSingleWebView.ZoomFactor = 1D;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(255, 128, 128);
            button3.FlatStyle = FlatStyle.Flat;
            button3.Location = new Point(1239, 3);
            button3.Name = "button3";
            button3.Size = new Size(51, 29);
            button3.TabIndex = 10;
            button3.Text = "X";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // mapModal
            // 
            mapModal.BackColor = Color.FromArgb(48, 54, 92);
            mapModal.Controls.Add(panel6);
            mapModal.Controls.Add(panel5);
            mapModal.Controls.Add(panel4);
            mapModal.Controls.Add(mapWebView);
            mapModal.Controls.Add(closemapBtn);
            mapModal.Location = new Point(285, 71);
            mapModal.Name = "mapModal";
            mapModal.Size = new Size(1287, 848);
            mapModal.TabIndex = 12;
            mapModal.Visible = false;
            mapModal.Paint += mapModal_Paint;
            // 
            // panel6
            // 
            panel6.BackColor = Color.WhiteSmoke;
            panel6.Controls.Add(totalInactive);
            panel6.Controls.Add(label26);
            panel6.Location = new Point(956, 610);
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
            mapLocatorModal.Location = new Point(268, 115);
            mapLocatorModal.Name = "mapLocatorModal";
            mapLocatorModal.Size = new Size(1238, 776);
            mapLocatorModal.TabIndex = 11;
            mapLocatorModal.Visible = false;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(128, 255, 128);
            button2.FlatStyle = FlatStyle.Flat;
            button2.Location = new Point(959, 699);
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
            label27.Location = new Point(878, 529);
            label27.Name = "label27";
            label27.Size = new Size(110, 31);
            label27.TabIndex = 15;
            label27.Text = "Latitude :";
            // 
            // panel14
            // 
            panel14.Controls.Add(locateLatTxt);
            panel14.Location = new Point(878, 566);
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
            label25.Location = new Point(878, 305);
            label25.Name = "label25";
            label25.Size = new Size(131, 31);
            label25.TabIndex = 13;
            label25.Text = "Longitude :";
            // 
            // panel8
            // 
            panel8.Controls.Add(locateLongTxt);
            panel8.Location = new Point(878, 342);
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
            label23.Location = new Point(878, 103);
            label23.Name = "label23";
            label23.Size = new Size(110, 31);
            label23.TabIndex = 11;
            label23.Text = "Address :";
            // 
            // panel7
            // 
            panel7.Controls.Add(locateAddressTxt);
            panel7.Location = new Point(878, 140);
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
            button1.Location = new Point(1184, 3);
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
            mapLocatorWebView.Location = new Point(17, 22);
            mapLocatorWebView.Name = "mapLocatorWebView";
            mapLocatorWebView.Size = new Size(809, 729);
            mapLocatorWebView.TabIndex = 0;
            mapLocatorWebView.ZoomFactor = 1D;
            // 
            // panel5
            // 
            panel5.BackColor = Color.WhiteSmoke;
            panel5.Controls.Add(totalActive);
            panel5.Controls.Add(label24);
            panel5.Location = new Point(956, 333);
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
            panel4.Location = new Point(956, 53);
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
            // mapWebView
            // 
            mapWebView.AllowExternalDrop = true;
            mapWebView.CreationProperties = null;
            mapWebView.DefaultBackgroundColor = Color.White;
            mapWebView.Location = new Point(54, 53);
            mapWebView.Name = "mapWebView";
            mapWebView.Size = new Size(832, 742);
            mapWebView.TabIndex = 9;
            mapWebView.ZoomFactor = 1D;
            // 
            // closemapBtn
            // 
            closemapBtn.BackColor = Color.FromArgb(255, 128, 128);
            closemapBtn.FlatStyle = FlatStyle.Flat;
            closemapBtn.Location = new Point(1233, 3);
            closemapBtn.Name = "closemapBtn";
            closemapBtn.Size = new Size(51, 29);
            closemapBtn.TabIndex = 8;
            closemapBtn.Text = "X";
            closemapBtn.UseVisualStyleBackColor = false;
            closemapBtn.Click += closemapBtn_Click;
            // 
            // manageRoomsModal
            // 
            manageRoomsModal.BackColor = Color.FromArgb(48, 54, 92);
            manageRoomsModal.Controls.Add(panel9);
            manageRoomsModal.Controls.Add(dgvRooms);
            manageRoomsModal.Controls.Add(lblRoomsFor);
            manageRoomsModal.Controls.Add(closeManageRoom);
            manageRoomsModal.Location = new Point(223, 120);
            manageRoomsModal.Name = "manageRoomsModal";
            manageRoomsModal.Size = new Size(1341, 611);
            manageRoomsModal.TabIndex = 19;
            manageRoomsModal.Visible = false;
            manageRoomsModal.Paint += manageRoomsModal_Paint;
            // 
            // panel9
            // 
            panel9.BackColor = Color.FromArgb(48, 54, 80);
            panel9.Controls.Add(btnMarkInactive);
            panel9.Controls.Add(btnMarkMaintenance);
            panel9.Controls.Add(btnMarkOccupied);
            panel9.Controls.Add(btnMarkAvailable);
            panel9.Location = new Point(978, 125);
            panel9.Name = "panel9";
            panel9.Size = new Size(314, 448);
            panel9.TabIndex = 15;
            panel9.Visible = false;
            // 
            // btnMarkInactive
            // 
            btnMarkInactive.BackColor = Color.Gray;
            btnMarkInactive.FlatStyle = FlatStyle.Flat;
            btnMarkInactive.Location = new Point(70, 346);
            btnMarkInactive.Name = "btnMarkInactive";
            btnMarkInactive.Size = new Size(178, 46);
            btnMarkInactive.TabIndex = 15;
            btnMarkInactive.Text = "In Maintenance";
            btnMarkInactive.UseVisualStyleBackColor = false;
            btnMarkInactive.Click += btnMarkInactive_Click;
            // 
            // btnMarkMaintenance
            // 
            btnMarkMaintenance.BackColor = Color.FromArgb(255, 128, 128);
            btnMarkMaintenance.FlatStyle = FlatStyle.Flat;
            btnMarkMaintenance.Location = new Point(70, 249);
            btnMarkMaintenance.Name = "btnMarkMaintenance";
            btnMarkMaintenance.Size = new Size(178, 46);
            btnMarkMaintenance.TabIndex = 14;
            btnMarkMaintenance.Text = "In Maintenance";
            btnMarkMaintenance.UseVisualStyleBackColor = false;
            btnMarkMaintenance.Click += btnMarkMaintenance_Click;
            // 
            // btnMarkOccupied
            // 
            btnMarkOccupied.BackColor = Color.FromArgb(255, 255, 128);
            btnMarkOccupied.FlatStyle = FlatStyle.Flat;
            btnMarkOccupied.Location = new Point(70, 156);
            btnMarkOccupied.Name = "btnMarkOccupied";
            btnMarkOccupied.Size = new Size(178, 46);
            btnMarkOccupied.TabIndex = 13;
            btnMarkOccupied.Text = "Mark As Occupied";
            btnMarkOccupied.UseVisualStyleBackColor = false;
            btnMarkOccupied.Click += btnMarkOccupied_Click;
            // 
            // btnMarkAvailable
            // 
            btnMarkAvailable.BackColor = Color.FromArgb(128, 255, 128);
            btnMarkAvailable.FlatStyle = FlatStyle.Flat;
            btnMarkAvailable.Location = new Point(72, 52);
            btnMarkAvailable.Name = "btnMarkAvailable";
            btnMarkAvailable.Size = new Size(178, 46);
            btnMarkAvailable.TabIndex = 12;
            btnMarkAvailable.Text = "Mark As Available";
            btnMarkAvailable.UseVisualStyleBackColor = false;
            btnMarkAvailable.Click += btnMarkAvailable_Click;
            // 
            // dgvRooms
            // 
            dgvRooms.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRooms.Location = new Point(82, 125);
            dgvRooms.Name = "dgvRooms";
            dgvRooms.RowHeadersWidth = 51;
            dgvRooms.Size = new Size(850, 454);
            dgvRooms.TabIndex = 14;
            dgvRooms.CellClick += dgvRooms_CellClick;
            // 
            // lblRoomsFor
            // 
            lblRoomsFor.AutoSize = true;
            lblRoomsFor.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRoomsFor.ForeColor = SystemColors.ButtonHighlight;
            lblRoomsFor.Location = new Point(79, 16);
            lblRoomsFor.Name = "lblRoomsFor";
            lblRoomsFor.Size = new Size(197, 46);
            lblRoomsFor.TabIndex = 13;
            lblRoomsFor.Text = "(BH Name)";
            // 
            // closeManageRoom
            // 
            closeManageRoom.BackColor = Color.FromArgb(255, 128, 128);
            closeManageRoom.FlatStyle = FlatStyle.Flat;
            closeManageRoom.Location = new Point(1287, 7);
            closeManageRoom.Name = "closeManageRoom";
            closeManageRoom.Size = new Size(51, 29);
            closeManageRoom.TabIndex = 11;
            closeManageRoom.Text = "X";
            closeManageRoom.UseVisualStyleBackColor = false;
            closeManageRoom.Click += closeManageRoom_Click;
            // 
            // BoardingHousesView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            Controls.Add(manageRoomsModal);
            Controls.Add(mapModal);
            Controls.Add(mapLocatorModal);
            Controls.Add(mapSingleModal);
            Controls.Add(detailsModal);
            Controls.Add(AddModal);
            Controls.Add(panel2);
            Controls.Add(addNewBtn);
            Controls.Add(viewMapBtn);
            Controls.Add(searchbtn);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Name = "BoardingHousesView";
            Size = new Size(1677, 975);
            Load += BoardingHousesView_Load;
            detailsModal.ResumeLayout(false);
            detailsModal.PerformLayout();
            ((ISupportInitialize)details_picThumbnail).EndInit();
            AddModal.ResumeLayout(false);
            AddModal.PerformLayout();
            ((ISupportInitialize)picThumbnail).EndInit();
            ((ISupportInitialize)dgvBoardingHouses).EndInit();
            panel2.ResumeLayout(false);
            mapSingleModal.ResumeLayout(false);
            ((ISupportInitialize)mapSingleWebView).EndInit();
            mapModal.ResumeLayout(false);
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            mapLocatorModal.ResumeLayout(false);
            mapLocatorModal.PerformLayout();
            panel14.ResumeLayout(false);
            panel14.PerformLayout();
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            ((ISupportInitialize)mapLocatorWebView).EndInit();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ((ISupportInitialize)mapWebView).EndInit();
            manageRoomsModal.ResumeLayout(false);
            manageRoomsModal.PerformLayout();
            panel9.ResumeLayout(false);
            ((ISupportInitialize)dgvRooms).EndInit();
            ResumeLayout(false);
            PerformLayout();
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
        private Button btnViewMap;
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
        private Label label16;
        private Label label19;
        private TextBox details_txtOwnerName;
        private Label label18;
        private TextBox details_txtContactNo;
        private ComboBox details_cbstatus;
        private Label label21;
        private TextBox txtContactNo;
        private Label label20;
        private TextBox txtOwnerName;
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
        private Button roomManageBtn;
        private Label label11;
        private Panel manageRoomsModal;
        private Label lblRoomsFor;
        private Button closeManageRoom;
        private DataGridView dgvRooms;
        private Panel panel9;
        private Button btnMarkOccupied;
        private Button btnMarkAvailable;
        private Button btnMarkInactive;
        private Button btnMarkMaintenance;
    }
}
