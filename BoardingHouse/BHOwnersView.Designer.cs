namespace BoardingHouse
{
    partial class BHOwnersView
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            topBar = new Panel();
            label1 = new Label();
            lblSearch = new Label();
            btnRefresh = new Button();
            btnSearch = new Button();
            txtSearch = new TextBox();
            addOwnerBtn = new Button();
            detailsModal = new Panel();
            grpQuickActions = new GroupBox();
            btnMarkInactive = new Button();
            btnMarkMaintenance = new Button();
            btnMarkOccupied = new Button();
            btnMarkAvailable = new Button();
            grpDetails = new GroupBox();
            viewBoardingHousesBtn = new Button();
            detailsCameraBtn = new Button();
            detailsOwnerAddresstxt = new TextBox();
            label8 = new Label();
            detailsOwnerEmailtxt = new TextBox();
            label7 = new Label();
            detailsOwnerContacttxt = new TextBox();
            label6 = new Label();
            detailsOwnerImgTXT = new TextBox();
            detailsBrowseImgBtn = new Button();
            label5 = new Label();
            detailsOwnerImg = new PictureBox();
            detailsOwnerMiddlenametxt = new TextBox();
            label4 = new Label();
            detailsOwnerFirstnametxt = new TextBox();
            detailsOwnerLastnametxt = new TextBox();
            updateOwnerBtn = new Button();
            deleteOwnerBtn = new Button();
            detailsLastname = new Label();
            lblOwnerTitle = new Label();
            flpOwners = new FlowLayoutPanel();
            addOwnerModal = new Panel();
            openCameraBtn = new Button();
            addOwnerImageTXT = new TextBox();
            addOwnerBrowseImageBtn = new Button();
            addOwnerImg = new PictureBox();
            addOwnerAddresstxt = new TextBox();
            label2 = new Label();
            addOwnerEmailtxt = new TextBox();
            label3 = new Label();
            addOwnerContacttxt = new TextBox();
            addOwnerLastnametxt = new TextBox();
            addOwnerTitle = new Label();
            addRoomCloseBtn = new Button();
            labelAddRoomBh = new Label();
            labelAddRoomNo = new Label();
            addOwnerFirstnametxt = new TextBox();
            labelAddRoomType = new Label();
            addOwnerMiddlenametxt = new TextBox();
            labelAddRoomCap = new Label();
            addOwnerSaveBtn = new Button();
            addOwnerCancelBtn = new Button();
            ownerBHModal = new Panel();
            closeBHModal = new Button();
            viewBhBtn = new Button();
            addNewBhBtn = new Button();
            lvBoardingHouses = new ListView();
            label11 = new Label();
            button3 = new Button();
            topBar.SuspendLayout();
            detailsModal.SuspendLayout();
            grpQuickActions.SuspendLayout();
            grpDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)detailsOwnerImg).BeginInit();
            addOwnerModal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)addOwnerImg).BeginInit();
            ownerBHModal.SuspendLayout();
            SuspendLayout();
            // 
            // topBar
            // 
            topBar.BackColor = Color.WhiteSmoke;
            topBar.Controls.Add(label1);
            topBar.Controls.Add(lblSearch);
            topBar.Controls.Add(btnRefresh);
            topBar.Controls.Add(btnSearch);
            topBar.Controls.Add(txtSearch);
            topBar.Controls.Add(addOwnerBtn);
            topBar.Location = new Point(1, 1);
            topBar.Name = "topBar";
            topBar.Padding = new Padding(18, 10, 18, 10);
            topBar.Size = new Size(1317, 92);
            topBar.TabIndex = 1;
            topBar.Paint += topBar_Paint;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label1.Location = new Point(34, 28);
            label1.Name = "label1";
            label1.Size = new Size(240, 28);
            label1.TabIndex = 9;
            label1.Text = "Boarding House Owners";
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Location = new Point(333, 17);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(53, 20);
            lblSearch.TabIndex = 2;
            lblSearch.Text = "Search";
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(699, 35);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(104, 34);
            btnRefresh.TabIndex = 7;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(589, 35);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(104, 34);
            btnSearch.TabIndex = 6;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // txtSearch
            // 
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Location = new Point(333, 40);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search";
            txtSearch.Size = new Size(250, 27);
            txtSearch.TabIndex = 3;
            // 
            // addOwnerBtn
            // 
            addOwnerBtn.Location = new Point(1162, 40);
            addOwnerBtn.Name = "addOwnerBtn";
            addOwnerBtn.Size = new Size(104, 34);
            addOwnerBtn.TabIndex = 8;
            addOwnerBtn.Text = "New +";
            addOwnerBtn.UseVisualStyleBackColor = true;
            addOwnerBtn.Click += addTenantBtn_Click;
            // 
            // detailsModal
            // 
            detailsModal.BackColor = Color.FromArgb(48, 54, 92);
            detailsModal.BorderStyle = BorderStyle.FixedSingle;
            detailsModal.Controls.Add(grpQuickActions);
            detailsModal.Controls.Add(grpDetails);
            detailsModal.Controls.Add(lblOwnerTitle);
            detailsModal.Dock = DockStyle.Right;
            detailsModal.Location = new Point(1317, 0);
            detailsModal.Name = "detailsModal";
            detailsModal.Padding = new Padding(16);
            detailsModal.Size = new Size(360, 975);
            detailsModal.TabIndex = 3;
            detailsModal.Paint += detailsModal_Paint;
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
            grpQuickActions.Size = new Size(443, 154);
            grpQuickActions.TabIndex = 3;
            grpQuickActions.TabStop = false;
            grpQuickActions.Text = "Quick Actions";
            grpQuickActions.Visible = false;
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
            // grpDetails
            // 
            grpDetails.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpDetails.Controls.Add(viewBoardingHousesBtn);
            grpDetails.Controls.Add(detailsCameraBtn);
            grpDetails.Controls.Add(detailsOwnerAddresstxt);
            grpDetails.Controls.Add(label8);
            grpDetails.Controls.Add(detailsOwnerEmailtxt);
            grpDetails.Controls.Add(label7);
            grpDetails.Controls.Add(detailsOwnerContacttxt);
            grpDetails.Controls.Add(label6);
            grpDetails.Controls.Add(detailsOwnerImgTXT);
            grpDetails.Controls.Add(detailsBrowseImgBtn);
            grpDetails.Controls.Add(label5);
            grpDetails.Controls.Add(detailsOwnerImg);
            grpDetails.Controls.Add(detailsOwnerMiddlenametxt);
            grpDetails.Controls.Add(label4);
            grpDetails.Controls.Add(detailsOwnerFirstnametxt);
            grpDetails.Controls.Add(detailsOwnerLastnametxt);
            grpDetails.Controls.Add(updateOwnerBtn);
            grpDetails.Controls.Add(deleteOwnerBtn);
            grpDetails.Controls.Add(detailsLastname);
            grpDetails.ForeColor = SystemColors.ButtonHighlight;
            grpDetails.Location = new Point(28, 64);
            grpDetails.Name = "grpDetails";
            grpDetails.Padding = new Padding(12);
            grpDetails.Size = new Size(311, 890);
            grpDetails.TabIndex = 2;
            grpDetails.TabStop = false;
            grpDetails.Text = "Details";
            // 
            // viewBoardingHousesBtn
            // 
            viewBoardingHousesBtn.BackColor = Color.FromArgb(48, 54, 92);
            viewBoardingHousesBtn.ForeColor = SystemColors.ButtonHighlight;
            viewBoardingHousesBtn.Location = new Point(21, 704);
            viewBoardingHousesBtn.Name = "viewBoardingHousesBtn";
            viewBoardingHousesBtn.Size = new Size(129, 32);
            viewBoardingHousesBtn.TabIndex = 17;
            viewBoardingHousesBtn.Text = "BoardingHouses";
            viewBoardingHousesBtn.UseVisualStyleBackColor = false;
            viewBoardingHousesBtn.Visible = false;
            viewBoardingHousesBtn.Click += viewBoardingHousesBtn_Click;
            // 
            // detailsCameraBtn
            // 
            detailsCameraBtn.ForeColor = SystemColors.ActiveCaptionText;
            detailsCameraBtn.Location = new Point(229, 62);
            detailsCameraBtn.Name = "detailsCameraBtn";
            detailsCameraBtn.Size = new Size(71, 25);
            detailsCameraBtn.TabIndex = 36;
            detailsCameraBtn.Text = "Camera";
            detailsCameraBtn.UseVisualStyleBackColor = true;
            detailsCameraBtn.Click += detailsCameraBtn_Click;
            // 
            // detailsOwnerAddresstxt
            // 
            detailsOwnerAddresstxt.Location = new Point(21, 632);
            detailsOwnerAddresstxt.Multiline = true;
            detailsOwnerAddresstxt.Name = "detailsOwnerAddresstxt";
            detailsOwnerAddresstxt.Size = new Size(275, 44);
            detailsOwnerAddresstxt.TabIndex = 35;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(21, 609);
            label8.Name = "label8";
            label8.Size = new Size(69, 20);
            label8.TabIndex = 34;
            label8.Text = "Address :";
            // 
            // detailsOwnerEmailtxt
            // 
            detailsOwnerEmailtxt.Location = new Point(21, 533);
            detailsOwnerEmailtxt.Multiline = true;
            detailsOwnerEmailtxt.Name = "detailsOwnerEmailtxt";
            detailsOwnerEmailtxt.Size = new Size(275, 44);
            detailsOwnerEmailtxt.TabIndex = 33;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(21, 510);
            label7.Name = "label7";
            label7.Size = new Size(53, 20);
            label7.TabIndex = 32;
            label7.Text = "Email :";
            // 
            // detailsOwnerContacttxt
            // 
            detailsOwnerContacttxt.Location = new Point(21, 441);
            detailsOwnerContacttxt.Multiline = true;
            detailsOwnerContacttxt.Name = "detailsOwnerContacttxt";
            detailsOwnerContacttxt.Size = new Size(275, 44);
            detailsOwnerContacttxt.TabIndex = 31;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(21, 418);
            label6.Name = "label6";
            label6.Size = new Size(67, 20);
            label6.TabIndex = 30;
            label6.Text = "Contact :";
            // 
            // detailsOwnerImgTXT
            // 
            detailsOwnerImgTXT.Location = new Point(84, 26);
            detailsOwnerImgTXT.Multiline = true;
            detailsOwnerImgTXT.Name = "detailsOwnerImgTXT";
            detailsOwnerImgTXT.Size = new Size(139, 15);
            detailsOwnerImgTXT.TabIndex = 29;
            detailsOwnerImgTXT.Visible = false;
            // 
            // detailsBrowseImgBtn
            // 
            detailsBrowseImgBtn.ForeColor = SystemColors.ActiveCaptionText;
            detailsBrowseImgBtn.Location = new Point(229, 26);
            detailsBrowseImgBtn.Name = "detailsBrowseImgBtn";
            detailsBrowseImgBtn.Size = new Size(71, 25);
            detailsBrowseImgBtn.TabIndex = 28;
            detailsBrowseImgBtn.Text = "Browse";
            detailsBrowseImgBtn.UseVisualStyleBackColor = true;
            detailsBrowseImgBtn.Click += detailsBrowseImgBtn_Click_1;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(21, 332);
            label5.Name = "label5";
            label5.Size = new Size(100, 20);
            label5.TabIndex = 27;
            label5.Text = "Middlename :";
            // 
            // detailsOwnerImg
            // 
            detailsOwnerImg.BackColor = Color.Silver;
            detailsOwnerImg.Location = new Point(84, 26);
            detailsOwnerImg.Name = "detailsOwnerImg";
            detailsOwnerImg.Size = new Size(139, 121);
            detailsOwnerImg.SizeMode = PictureBoxSizeMode.StretchImage;
            detailsOwnerImg.TabIndex = 27;
            detailsOwnerImg.TabStop = false;
            // 
            // detailsOwnerMiddlenametxt
            // 
            detailsOwnerMiddlenametxt.Location = new Point(21, 354);
            detailsOwnerMiddlenametxt.Multiline = true;
            detailsOwnerMiddlenametxt.Name = "detailsOwnerMiddlenametxt";
            detailsOwnerMiddlenametxt.Size = new Size(275, 44);
            detailsOwnerMiddlenametxt.TabIndex = 28;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(21, 242);
            label4.Name = "label4";
            label4.Size = new Size(80, 20);
            label4.TabIndex = 25;
            label4.Text = "Firstname :";
            // 
            // detailsOwnerFirstnametxt
            // 
            detailsOwnerFirstnametxt.Location = new Point(21, 264);
            detailsOwnerFirstnametxt.Multiline = true;
            detailsOwnerFirstnametxt.Name = "detailsOwnerFirstnametxt";
            detailsOwnerFirstnametxt.Size = new Size(275, 44);
            detailsOwnerFirstnametxt.TabIndex = 26;
            // 
            // detailsOwnerLastnametxt
            // 
            detailsOwnerLastnametxt.Location = new Point(21, 177);
            detailsOwnerLastnametxt.Multiline = true;
            detailsOwnerLastnametxt.Name = "detailsOwnerLastnametxt";
            detailsOwnerLastnametxt.Size = new Size(275, 44);
            detailsOwnerLastnametxt.TabIndex = 24;
            // 
            // updateOwnerBtn
            // 
            updateOwnerBtn.BackColor = Color.FromArgb(192, 255, 192);
            updateOwnerBtn.ForeColor = SystemColors.ActiveCaptionText;
            updateOwnerBtn.Location = new Point(202, 801);
            updateOwnerBtn.Name = "updateOwnerBtn";
            updateOwnerBtn.Size = new Size(94, 29);
            updateOwnerBtn.TabIndex = 23;
            updateOwnerBtn.Text = "Update";
            updateOwnerBtn.UseVisualStyleBackColor = false;
            updateOwnerBtn.Visible = false;
            updateOwnerBtn.Click += updateTenantBtn_Click;
            // 
            // deleteOwnerBtn
            // 
            deleteOwnerBtn.BackColor = Color.FromArgb(255, 192, 192);
            deleteOwnerBtn.ForeColor = Color.Black;
            deleteOwnerBtn.Location = new Point(21, 801);
            deleteOwnerBtn.Name = "deleteOwnerBtn";
            deleteOwnerBtn.Size = new Size(94, 29);
            deleteOwnerBtn.TabIndex = 22;
            deleteOwnerBtn.Text = "Delete";
            deleteOwnerBtn.UseVisualStyleBackColor = false;
            deleteOwnerBtn.Visible = false;
            deleteOwnerBtn.Click += deleteTenantBtn_Click;
            // 
            // detailsLastname
            // 
            detailsLastname.AutoSize = true;
            detailsLastname.ForeColor = SystemColors.ButtonHighlight;
            detailsLastname.Location = new Point(21, 150);
            detailsLastname.Name = "detailsLastname";
            detailsLastname.Size = new Size(72, 20);
            detailsLastname.TabIndex = 0;
            detailsLastname.Text = "Latsname";
            // 
            // lblOwnerTitle
            // 
            lblOwnerTitle.AutoSize = true;
            lblOwnerTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblOwnerTitle.ForeColor = SystemColors.ButtonHighlight;
            lblOwnerTitle.Location = new Point(28, 28);
            lblOwnerTitle.Name = "lblOwnerTitle";
            lblOwnerTitle.Size = new Size(135, 25);
            lblOwnerTitle.TabIndex = 1;
            lblOwnerTitle.Text = "Owner Details";
            // 
            // flpOwners
            // 
            flpOwners.AutoScroll = true;
            flpOwners.Location = new Point(35, 132);
            flpOwners.Name = "flpOwners";
            flpOwners.Padding = new Padding(16);
            flpOwners.Size = new Size(1232, 730);
            flpOwners.TabIndex = 4;
            // 
            // addOwnerModal
            // 
            addOwnerModal.Anchor = AnchorStyles.None;
            addOwnerModal.BackColor = Color.WhiteSmoke;
            addOwnerModal.BorderStyle = BorderStyle.FixedSingle;
            addOwnerModal.Controls.Add(openCameraBtn);
            addOwnerModal.Controls.Add(addOwnerImageTXT);
            addOwnerModal.Controls.Add(addOwnerBrowseImageBtn);
            addOwnerModal.Controls.Add(addOwnerImg);
            addOwnerModal.Controls.Add(addOwnerAddresstxt);
            addOwnerModal.Controls.Add(label2);
            addOwnerModal.Controls.Add(addOwnerEmailtxt);
            addOwnerModal.Controls.Add(label3);
            addOwnerModal.Controls.Add(addOwnerContacttxt);
            addOwnerModal.Controls.Add(addOwnerLastnametxt);
            addOwnerModal.Controls.Add(addOwnerTitle);
            addOwnerModal.Controls.Add(addRoomCloseBtn);
            addOwnerModal.Controls.Add(labelAddRoomBh);
            addOwnerModal.Controls.Add(labelAddRoomNo);
            addOwnerModal.Controls.Add(addOwnerFirstnametxt);
            addOwnerModal.Controls.Add(labelAddRoomType);
            addOwnerModal.Controls.Add(addOwnerMiddlenametxt);
            addOwnerModal.Controls.Add(labelAddRoomCap);
            addOwnerModal.Controls.Add(addOwnerSaveBtn);
            addOwnerModal.Controls.Add(addOwnerCancelBtn);
            addOwnerModal.Location = new Point(681, 105);
            addOwnerModal.Name = "addOwnerModal";
            addOwnerModal.Size = new Size(586, 497);
            addOwnerModal.TabIndex = 6;
            addOwnerModal.Visible = false;
            // 
            // openCameraBtn
            // 
            openCameraBtn.Location = new Point(461, 100);
            openCameraBtn.Name = "openCameraBtn";
            openCameraBtn.Size = new Size(71, 25);
            openCameraBtn.TabIndex = 28;
            openCameraBtn.Text = "Camera";
            openCameraBtn.UseVisualStyleBackColor = true;
            openCameraBtn.Click += openCameraBtn_Click;
            // 
            // addOwnerImageTXT
            // 
            addOwnerImageTXT.Location = new Point(316, 68);
            addOwnerImageTXT.Multiline = true;
            addOwnerImageTXT.Name = "addOwnerImageTXT";
            addOwnerImageTXT.Size = new Size(139, 15);
            addOwnerImageTXT.TabIndex = 27;
            // 
            // addOwnerBrowseImageBtn
            // 
            addOwnerBrowseImageBtn.Location = new Point(461, 69);
            addOwnerBrowseImageBtn.Name = "addOwnerBrowseImageBtn";
            addOwnerBrowseImageBtn.Size = new Size(71, 25);
            addOwnerBrowseImageBtn.TabIndex = 26;
            addOwnerBrowseImageBtn.Text = "Browse";
            addOwnerBrowseImageBtn.UseVisualStyleBackColor = true;
            addOwnerBrowseImageBtn.Click += addOwnerBrowseImageBtn_Click_1;
            // 
            // addOwnerImg
            // 
            addOwnerImg.BackColor = Color.Silver;
            addOwnerImg.Location = new Point(316, 69);
            addOwnerImg.Name = "addOwnerImg";
            addOwnerImg.Size = new Size(139, 121);
            addOwnerImg.SizeMode = PictureBoxSizeMode.StretchImage;
            addOwnerImg.TabIndex = 25;
            addOwnerImg.TabStop = false;
            // 
            // addOwnerAddresstxt
            // 
            addOwnerAddresstxt.Location = new Point(316, 313);
            addOwnerAddresstxt.Multiline = true;
            addOwnerAddresstxt.Name = "addOwnerAddresstxt";
            addOwnerAddresstxt.Size = new Size(237, 44);
            addOwnerAddresstxt.TabIndex = 24;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(316, 290);
            label2.Name = "label2";
            label2.Size = new Size(69, 20);
            label2.TabIndex = 23;
            label2.Text = "Address :";
            // 
            // addOwnerEmailtxt
            // 
            addOwnerEmailtxt.Location = new Point(316, 229);
            addOwnerEmailtxt.Multiline = true;
            addOwnerEmailtxt.Name = "addOwnerEmailtxt";
            addOwnerEmailtxt.Size = new Size(237, 44);
            addOwnerEmailtxt.TabIndex = 22;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(316, 206);
            label3.Name = "label3";
            label3.Size = new Size(53, 20);
            label3.TabIndex = 21;
            label3.Text = "Email :";
            // 
            // addOwnerContacttxt
            // 
            addOwnerContacttxt.Location = new Point(16, 313);
            addOwnerContacttxt.Multiline = true;
            addOwnerContacttxt.Name = "addOwnerContacttxt";
            addOwnerContacttxt.Size = new Size(265, 44);
            addOwnerContacttxt.TabIndex = 19;
            addOwnerContacttxt.TextChanged += textBox2_TextChanged;
            // 
            // addOwnerLastnametxt
            // 
            addOwnerLastnametxt.Location = new Point(16, 79);
            addOwnerLastnametxt.Multiline = true;
            addOwnerLastnametxt.Name = "addOwnerLastnametxt";
            addOwnerLastnametxt.Size = new Size(260, 44);
            addOwnerLastnametxt.TabIndex = 18;
            // 
            // addOwnerTitle
            // 
            addOwnerTitle.AutoSize = true;
            addOwnerTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            addOwnerTitle.Location = new Point(16, 16);
            addOwnerTitle.Name = "addOwnerTitle";
            addOwnerTitle.Size = new Size(118, 28);
            addOwnerTitle.TabIndex = 0;
            addOwnerTitle.Text = "Add Owner";
            // 
            // addRoomCloseBtn
            // 
            addRoomCloseBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            addRoomCloseBtn.Location = new Point(1098, 16);
            addRoomCloseBtn.Name = "addRoomCloseBtn";
            addRoomCloseBtn.Size = new Size(28, 28);
            addRoomCloseBtn.TabIndex = 1;
            addRoomCloseBtn.Text = "X";
            addRoomCloseBtn.UseVisualStyleBackColor = true;
            // 
            // labelAddRoomBh
            // 
            labelAddRoomBh.AutoSize = true;
            labelAddRoomBh.Location = new Point(16, 56);
            labelAddRoomBh.Name = "labelAddRoomBh";
            labelAddRoomBh.Size = new Size(79, 20);
            labelAddRoomBh.TabIndex = 2;
            labelAddRoomBh.Text = "Lastname :";
            // 
            // labelAddRoomNo
            // 
            labelAddRoomNo.AutoSize = true;
            labelAddRoomNo.Location = new Point(16, 137);
            labelAddRoomNo.Name = "labelAddRoomNo";
            labelAddRoomNo.Size = new Size(80, 20);
            labelAddRoomNo.TabIndex = 4;
            labelAddRoomNo.Text = "Firstname :";
            // 
            // addOwnerFirstnametxt
            // 
            addOwnerFirstnametxt.Location = new Point(16, 159);
            addOwnerFirstnametxt.Multiline = true;
            addOwnerFirstnametxt.Name = "addOwnerFirstnametxt";
            addOwnerFirstnametxt.Size = new Size(260, 44);
            addOwnerFirstnametxt.TabIndex = 5;
            // 
            // labelAddRoomType
            // 
            labelAddRoomType.AutoSize = true;
            labelAddRoomType.Location = new Point(16, 214);
            labelAddRoomType.Name = "labelAddRoomType";
            labelAddRoomType.Size = new Size(100, 20);
            labelAddRoomType.TabIndex = 6;
            labelAddRoomType.Text = "Middlename :";
            // 
            // addOwnerMiddlenametxt
            // 
            addOwnerMiddlenametxt.Location = new Point(16, 236);
            addOwnerMiddlenametxt.Multiline = true;
            addOwnerMiddlenametxt.Name = "addOwnerMiddlenametxt";
            addOwnerMiddlenametxt.Size = new Size(265, 44);
            addOwnerMiddlenametxt.TabIndex = 7;
            // 
            // labelAddRoomCap
            // 
            labelAddRoomCap.AutoSize = true;
            labelAddRoomCap.Location = new Point(16, 290);
            labelAddRoomCap.Name = "labelAddRoomCap";
            labelAddRoomCap.Size = new Size(67, 20);
            labelAddRoomCap.TabIndex = 8;
            labelAddRoomCap.Text = "Contact :";
            // 
            // addOwnerSaveBtn
            // 
            addOwnerSaveBtn.Location = new Point(363, 431);
            addOwnerSaveBtn.Name = "addOwnerSaveBtn";
            addOwnerSaveBtn.Size = new Size(110, 32);
            addOwnerSaveBtn.TabIndex = 16;
            addOwnerSaveBtn.Text = "Save";
            addOwnerSaveBtn.UseVisualStyleBackColor = true;
            addOwnerSaveBtn.Click += addOwnerSaveBtn_Click_1;
            // 
            // addOwnerCancelBtn
            // 
            addOwnerCancelBtn.Location = new Point(105, 431);
            addOwnerCancelBtn.Name = "addOwnerCancelBtn";
            addOwnerCancelBtn.Size = new Size(110, 32);
            addOwnerCancelBtn.TabIndex = 17;
            addOwnerCancelBtn.Text = "Cancel";
            addOwnerCancelBtn.UseVisualStyleBackColor = true;
            addOwnerCancelBtn.Click += addOwnerCancelBtn_Click_1;
            // 
            // ownerBHModal
            // 
            ownerBHModal.Anchor = AnchorStyles.None;
            ownerBHModal.BackColor = Color.WhiteSmoke;
            ownerBHModal.BorderStyle = BorderStyle.FixedSingle;
            ownerBHModal.Controls.Add(closeBHModal);
            ownerBHModal.Controls.Add(viewBhBtn);
            ownerBHModal.Controls.Add(addNewBhBtn);
            ownerBHModal.Controls.Add(lvBoardingHouses);
            ownerBHModal.Controls.Add(label11);
            ownerBHModal.Controls.Add(button3);
            ownerBHModal.Location = new Point(76, 105);
            ownerBHModal.Name = "ownerBHModal";
            ownerBHModal.Size = new Size(586, 419);
            ownerBHModal.TabIndex = 7;
            ownerBHModal.Visible = false;
            // 
            // closeBHModal
            // 
            closeBHModal.Location = new Point(245, 383);
            closeBHModal.Name = "closeBHModal";
            closeBHModal.Size = new Size(92, 31);
            closeBHModal.TabIndex = 17;
            closeBHModal.Text = "Close";
            closeBHModal.UseVisualStyleBackColor = true;
            closeBHModal.Click += closeBHModal_Click;
            // 
            // viewBhBtn
            // 
            viewBhBtn.Location = new Point(353, 21);
            viewBhBtn.Name = "viewBhBtn";
            viewBhBtn.Size = new Size(104, 34);
            viewBhBtn.TabIndex = 10;
            viewBhBtn.Text = "View";
            viewBhBtn.UseVisualStyleBackColor = true;
            viewBhBtn.Click += viewBhBtn_Click;
            // 
            // addNewBhBtn
            // 
            addNewBhBtn.Location = new Point(463, 21);
            addNewBhBtn.Name = "addNewBhBtn";
            addNewBhBtn.Size = new Size(104, 34);
            addNewBhBtn.TabIndex = 9;
            addNewBhBtn.Text = "New +";
            addNewBhBtn.UseVisualStyleBackColor = true;
            addNewBhBtn.Click += addNewBhBtn_Click;
            // 
            // lvBoardingHouses
            // 
            lvBoardingHouses.Location = new Point(16, 68);
            lvBoardingHouses.Name = "lvBoardingHouses";
            lvBoardingHouses.Size = new Size(551, 309);
            lvBoardingHouses.TabIndex = 2;
            lvBoardingHouses.UseCompatibleStateImageBehavior = false;
            lvBoardingHouses.View = View.Details;
            lvBoardingHouses.SelectedIndexChanged += lvBoardingHouses_SelectedIndexChanged;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label11.Location = new Point(16, 16);
            label11.Name = "label11";
            label11.Size = new Size(172, 28);
            label11.TabIndex = 0;
            label11.Text = "Boarding Houses";
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button3.Location = new Point(1482, 16);
            button3.Name = "button3";
            button3.Size = new Size(28, 28);
            button3.TabIndex = 1;
            button3.Text = "X";
            button3.UseVisualStyleBackColor = true;
            // 
            // BHOwnersView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ownerBHModal);
            Controls.Add(addOwnerModal);
            Controls.Add(flpOwners);
            Controls.Add(detailsModal);
            Controls.Add(topBar);
            Name = "BHOwnersView";
            Size = new Size(1677, 975);
            Load += BHOwnersView_Load;
            topBar.ResumeLayout(false);
            topBar.PerformLayout();
            detailsModal.ResumeLayout(false);
            detailsModal.PerformLayout();
            grpQuickActions.ResumeLayout(false);
            grpDetails.ResumeLayout(false);
            grpDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)detailsOwnerImg).EndInit();
            addOwnerModal.ResumeLayout(false);
            addOwnerModal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)addOwnerImg).EndInit();
            ownerBHModal.ResumeLayout(false);
            ownerBHModal.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel topBar;
        private Label lblSearch;
        private Button btnRefresh;
        private Button btnSearch;
        private TextBox txtSearch;
        private Button addOwnerBtn;
        private Panel detailsModal;
        private GroupBox grpQuickActions;
        private Button btnMarkInactive;
        private Button btnMarkMaintenance;
        private Button btnMarkOccupied;
        private Button btnMarkAvailable;
        private GroupBox grpDetails;
        private Button updateOwnerBtn;
        private Button deleteOwnerBtn;
        private Label detailsLastname;
        private Label lblOwnerTitle;
        private FlowLayoutPanel flpOwners;
        private Panel addOwnerModal;
        private Label addOwnerTitle;
        private Button addRoomCloseBtn;
        private Label labelAddRoomBh;
        private Label labelAddRoomNo;
        private TextBox addOwnerFirstnametxt;
        private Label labelAddRoomType;
        private TextBox addOwnerMiddlenametxt;
        private Label labelAddRoomCap;
        private Button addOwnerSaveBtn;
        private Button addOwnerCancelBtn;
        private Label label1;
        private TextBox addOwnerContacttxt;
        private TextBox addOwnerLastnametxt;
        private Button addOwnerBrowseImageBtn;
        private PictureBox addOwnerImg;
        private TextBox addOwnerAddresstxt;
        private Label label2;
        private TextBox addOwnerEmailtxt;
        private Label label3;
        private Button detailsBrowseImgBtn;
        private Label label5;
        private PictureBox detailsOwnerImg;
        private TextBox detailsOwnerMiddlenametxt;
        private Label label4;
        private TextBox detailsOwnerFirstnametxt;
        private TextBox detailsOwnerLastnametxt;
        private TextBox addOwnerImageTXT;
        private TextBox detailsOwnerImgTXT;
        private TextBox detailsOwnerAddresstxt;
        private Label label8;
        private TextBox detailsOwnerEmailtxt;
        private Label label7;
        private TextBox detailsOwnerContacttxt;
        private Label label6;
        private Button detailsCameraBtn;
        private Button openCameraBtn;
        private Button viewBoardingHousesBtn;
        private Panel ownerBHModal;
        private Button addNewBhBtn;
        private ListView lvBoardingHouses;
        private Label label11;
        private Button button3;
        private Button viewBhBtn;
        private Button closeBHModal;
    }
}