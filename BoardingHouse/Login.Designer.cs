namespace BoardingHouse
{
    partial class Login
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            panelLoginCard = new Panel();
            label5 = new Label();
            label4 = new Label();
            LoginBtn = new Button();
            label3 = new Label();
            passwordtxt = new TextBox();
            label2 = new Label();
            usernametxt = new TextBox();
            pictureBox2 = new PictureBox();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            footerPanel = new Panel();
            lblCopyright = new Label();
            lblPhone = new Label();
            lblEmail = new Label();
            lblContactTitle = new Label();
            footerSeparator = new Panel();
            pictureBox3 = new PictureBox();
            label6 = new Label();
            panelLoginCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            footerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // panelLoginCard
            // 
            panelLoginCard.BackColor = Color.White;
            panelLoginCard.Controls.Add(label5);
            panelLoginCard.Controls.Add(label4);
            panelLoginCard.Controls.Add(LoginBtn);
            panelLoginCard.Controls.Add(label3);
            panelLoginCard.Controls.Add(passwordtxt);
            panelLoginCard.Controls.Add(label2);
            panelLoginCard.Controls.Add(usernametxt);
            panelLoginCard.Controls.Add(pictureBox2);
            panelLoginCard.Controls.Add(label1);
            panelLoginCard.Controls.Add(pictureBox1);
            panelLoginCard.Dock = DockStyle.Right;
            panelLoginCard.Location = new Point(948, 0);
            panelLoginCard.Name = "panelLoginCard";
            panelLoginCard.Size = new Size(976, 1055);
            panelLoginCard.TabIndex = 10;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Cursor = Cursors.Hand;
            label5.ForeColor = Color.FromArgb(128, 128, 255);
            label5.Location = new Point(551, 840);
            label5.Name = "label5";
            label5.Size = new Size(99, 20);
            label5.TabIndex = 11;
            label5.Text = "Register Here";
            label5.Click += label5_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(344, 840);
            label4.Name = "label4";
            label4.Size = new Size(201, 20);
            label4.TabIndex = 10;
            label4.Text = "Doesn't have an account yet?";
            // 
            // LoginBtn
            // 
            LoginBtn.BackColor = Color.FromArgb(41, 42, 70);
            LoginBtn.FlatStyle = FlatStyle.Flat;
            LoginBtn.ForeColor = SystemColors.ButtonFace;
            LoginBtn.Location = new Point(290, 744);
            LoginBtn.Name = "LoginBtn";
            LoginBtn.Size = new Size(423, 50);
            LoginBtn.TabIndex = 9;
            LoginBtn.Text = "Login";
            LoginBtn.UseVisualStyleBackColor = false;
            LoginBtn.Click += LoginBtn_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label3.ForeColor = SystemColors.ActiveCaptionText;
            label3.Location = new Point(290, 557);
            label3.Name = "label3";
            label3.Size = new Size(108, 28);
            label3.TabIndex = 7;
            label3.Text = "Password :";
            // 
            // passwordtxt
            // 
            passwordtxt.BackColor = SystemColors.InactiveBorder;
            passwordtxt.Font = new Font("Segoe UI", 13.8F);
            passwordtxt.ForeColor = SystemColors.ActiveCaptionText;
            passwordtxt.Location = new Point(290, 588);
            passwordtxt.Multiline = true;
            passwordtxt.Name = "passwordtxt";
            passwordtxt.PasswordChar = '*';
            passwordtxt.PlaceholderText = "Enter Password";
            passwordtxt.Size = new Size(423, 44);
            passwordtxt.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label2.ForeColor = SystemColors.ActiveCaptionText;
            label2.Location = new Point(290, 443);
            label2.Name = "label2";
            label2.Size = new Size(115, 28);
            label2.TabIndex = 5;
            label2.Text = "Username :";
            // 
            // usernametxt
            // 
            usernametxt.BackColor = SystemColors.InactiveBorder;
            usernametxt.Font = new Font("Segoe UI", 13.8F);
            usernametxt.ForeColor = SystemColors.ActiveCaptionText;
            usernametxt.Location = new Point(290, 474);
            usernametxt.Multiline = true;
            usernametxt.Name = "usernametxt";
            usernametxt.PlaceholderText = "Enter Username";
            usernametxt.Size = new Size(423, 44);
            usernametxt.TabIndex = 4;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(344, 147);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(322, 236);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 2;
            pictureBox2.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold | FontStyle.Italic);
            label1.ForeColor = SystemColors.ActiveCaptionText;
            label1.Location = new Point(411, 84);
            label1.Name = "label1";
            label1.Size = new Size(204, 46);
            label1.TabIndex = 1;
            label1.Text = "Welcome!!!";
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox1.Cursor = Cursors.Hand;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(930, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(43, 44);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Visible = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // footerPanel
            // 
            footerPanel.BackColor = Color.FromArgb(50, 52, 90);
            footerPanel.Controls.Add(lblCopyright);
            footerPanel.Controls.Add(lblPhone);
            footerPanel.Controls.Add(lblEmail);
            footerPanel.Controls.Add(lblContactTitle);
            footerPanel.Controls.Add(footerSeparator);
            footerPanel.Dock = DockStyle.Bottom;
            footerPanel.Location = new Point(0, 907);
            footerPanel.Name = "footerPanel";
            footerPanel.Padding = new Padding(40, 18, 40, 18);
            footerPanel.Size = new Size(948, 148);
            footerPanel.TabIndex = 13;
            // 
            // lblCopyright
            // 
            lblCopyright.AutoSize = true;
            lblCopyright.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCopyright.ForeColor = Color.FromArgb(154, 154, 176);
            lblCopyright.Location = new Point(40, 110);
            lblCopyright.Name = "lblCopyright";
            lblCopyright.Size = new Size(261, 20);
            lblCopyright.TabIndex = 3;
            lblCopyright.Text = "© 2026 BoardEase. All rights reserved.";
            // 
            // lblPhone
            // 
            lblPhone.AutoSize = true;
            lblPhone.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPhone.ForeColor = Color.FromArgb(207, 207, 214);
            lblPhone.Location = new Point(40, 87);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(145, 23);
            lblPhone.TabIndex = 2;
            lblPhone.Text = "+63 908 818 4444";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblEmail.ForeColor = Color.FromArgb(207, 207, 214);
            lblEmail.Location = new Point(40, 64);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(201, 23);
            lblEmail.TabIndex = 1;
            lblEmail.Text = "support@boardease.com";
            // 
            // lblContactTitle
            // 
            lblContactTitle.AutoSize = true;
            lblContactTitle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            lblContactTitle.ForeColor = Color.White;
            lblContactTitle.Location = new Point(43, 39);
            lblContactTitle.Name = "lblContactTitle";
            lblContactTitle.Size = new Size(105, 25);
            lblContactTitle.TabIndex = 0;
            lblContactTitle.Text = "Contact Us";
            // 
            // footerSeparator
            // 
            footerSeparator.BackColor = Color.FromArgb(80, 80, 110);
            footerSeparator.Dock = DockStyle.Top;
            footerSeparator.Location = new Point(40, 18);
            footerSeparator.Name = "footerSeparator";
            footerSeparator.Size = new Size(868, 1);
            footerSeparator.TabIndex = 4;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(165, 166);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(609, 435);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 11;
            pictureBox3.TabStop = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.FromArgb(207, 207, 214);
            label6.Location = new Point(259, 526);
            label6.Name = "label6";
            label6.Size = new Size(399, 25);
            label6.TabIndex = 12;
            label6.Text = "Streamlining Your Boarding House Operations";
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(50, 52, 90);
            ClientSize = new Size(1924, 1055);
            Controls.Add(label6);
            Controls.Add(pictureBox3);
            Controls.Add(footerPanel);
            Controls.Add(panelLoginCard);
            Name = "Login";
            Text = "Form1";
            WindowState = FormWindowState.Maximized;
            panelLoginCard.ResumeLayout(false);
            panelLoginCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            footerPanel.ResumeLayout(false);
            footerPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelLoginCard;
        private Panel footerPanel;
        private Panel footerSeparator;
        private PictureBox pictureBox1;
        private Label label1;
        private PictureBox pictureBox2;
        private TextBox usernametxt;
        private Label label2;
        private Label label3;
        private TextBox passwordtxt;
        private Button LoginBtn;
        private Label label5;
        private Label label4;
        private PictureBox pictureBox3;
        private Label label6;
        private Label lblContactTitle;
        private Label lblEmail;
        private Label lblPhone;
        private Label lblCopyright;
    }
}
