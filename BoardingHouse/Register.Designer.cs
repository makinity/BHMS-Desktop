namespace BoardingHouse
{
    partial class Register
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Register));
            panelRegisterCard = new Panel();
            lblLoginHere = new Label();
            lblHaveAccount = new Label();
            RegisterBtn = new Button();
            lblConfirmPassword = new Label();
            confirmPasswordtxt = new TextBox();
            lblPassword = new Label();
            passwordtxt = new TextBox();
            lblUsername = new Label();
            usernametxt = new TextBox();
            lblFullName = new Label();
            fullNametxt = new TextBox();
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
            panelRegisterCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            footerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // panelRegisterCard
            // 
            panelRegisterCard.BackColor = Color.White;
            panelRegisterCard.Controls.Add(lblLoginHere);
            panelRegisterCard.Controls.Add(lblHaveAccount);
            panelRegisterCard.Controls.Add(RegisterBtn);
            panelRegisterCard.Controls.Add(lblConfirmPassword);
            panelRegisterCard.Controls.Add(confirmPasswordtxt);
            panelRegisterCard.Controls.Add(lblPassword);
            panelRegisterCard.Controls.Add(passwordtxt);
            panelRegisterCard.Controls.Add(lblUsername);
            panelRegisterCard.Controls.Add(usernametxt);
            panelRegisterCard.Controls.Add(lblFullName);
            panelRegisterCard.Controls.Add(fullNametxt);
            panelRegisterCard.Controls.Add(pictureBox2);
            panelRegisterCard.Controls.Add(label1);
            panelRegisterCard.Controls.Add(pictureBox1);
            panelRegisterCard.Dock = DockStyle.Left;
            panelRegisterCard.Location = new Point(0, 0);
            panelRegisterCard.Name = "panelRegisterCard";
            panelRegisterCard.Size = new Size(976, 1055);
            panelRegisterCard.TabIndex = 10;
            // 
            // lblLoginHere
            // 
            lblLoginHere.AutoSize = true;
            lblLoginHere.Cursor = Cursors.Hand;
            lblLoginHere.ForeColor = Color.FromArgb(128, 128, 255);
            lblLoginHere.Location = new Point(552, 925);
            lblLoginHere.Name = "lblLoginHere";
            lblLoginHere.Size = new Size(82, 20);
            lblLoginHere.TabIndex = 13;
            lblLoginHere.Text = "Login Here";
            lblLoginHere.Click += lblLoginHere_Click;
            // 
            // lblHaveAccount
            // 
            lblHaveAccount.AutoSize = true;
            lblHaveAccount.Location = new Point(343, 925);
            lblHaveAccount.Name = "lblHaveAccount";
            lblHaveAccount.Size = new Size(178, 20);
            lblHaveAccount.TabIndex = 12;
            lblHaveAccount.Text = "Already have an account?";
            // 
            // RegisterBtn
            // 
            RegisterBtn.BackColor = Color.FromArgb(41, 42, 70);
            RegisterBtn.FlatStyle = FlatStyle.Flat;
            RegisterBtn.ForeColor = SystemColors.ButtonFace;
            RegisterBtn.Location = new Point(290, 829);
            RegisterBtn.Name = "RegisterBtn";
            RegisterBtn.Size = new Size(423, 50);
            RegisterBtn.TabIndex = 11;
            RegisterBtn.Text = "Register";
            RegisterBtn.UseVisualStyleBackColor = false;
            // 
            // lblConfirmPassword
            // 
            lblConfirmPassword.AutoSize = true;
            lblConfirmPassword.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblConfirmPassword.ForeColor = SystemColors.ActiveCaptionText;
            lblConfirmPassword.Location = new Point(290, 670);
            lblConfirmPassword.Name = "lblConfirmPassword";
            lblConfirmPassword.Size = new Size(187, 28);
            lblConfirmPassword.TabIndex = 9;
            lblConfirmPassword.Text = "Confirm Password :";
            // 
            // confirmPasswordtxt
            // 
            confirmPasswordtxt.BackColor = SystemColors.InactiveBorder;
            confirmPasswordtxt.Font = new Font("Segoe UI", 13.8F);
            confirmPasswordtxt.ForeColor = SystemColors.ActiveCaptionText;
            confirmPasswordtxt.Location = new Point(290, 701);
            confirmPasswordtxt.Multiline = true;
            confirmPasswordtxt.Name = "confirmPasswordtxt";
            confirmPasswordtxt.PasswordChar = '*';
            confirmPasswordtxt.PlaceholderText = "Confirm Password";
            confirmPasswordtxt.Size = new Size(423, 44);
            confirmPasswordtxt.TabIndex = 10;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblPassword.ForeColor = SystemColors.ActiveCaptionText;
            lblPassword.Location = new Point(290, 562);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(108, 28);
            lblPassword.TabIndex = 7;
            lblPassword.Text = "Password :";
            // 
            // passwordtxt
            // 
            passwordtxt.BackColor = SystemColors.InactiveBorder;
            passwordtxt.Font = new Font("Segoe UI", 13.8F);
            passwordtxt.ForeColor = SystemColors.ActiveCaptionText;
            passwordtxt.Location = new Point(290, 593);
            passwordtxt.Multiline = true;
            passwordtxt.Name = "passwordtxt";
            passwordtxt.PasswordChar = '*';
            passwordtxt.PlaceholderText = "Enter Password";
            passwordtxt.Size = new Size(423, 44);
            passwordtxt.TabIndex = 8;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblUsername.ForeColor = SystemColors.ActiveCaptionText;
            lblUsername.Location = new Point(290, 467);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(115, 28);
            lblUsername.TabIndex = 5;
            lblUsername.Text = "Username :";
            // 
            // usernametxt
            // 
            usernametxt.BackColor = SystemColors.InactiveBorder;
            usernametxt.Font = new Font("Segoe UI", 13.8F);
            usernametxt.ForeColor = SystemColors.ActiveCaptionText;
            usernametxt.Location = new Point(290, 498);
            usernametxt.Multiline = true;
            usernametxt.Name = "usernametxt";
            usernametxt.PlaceholderText = "Enter Username";
            usernametxt.Size = new Size(423, 44);
            usernametxt.TabIndex = 6;
            // 
            // lblFullName
            // 
            lblFullName.AutoSize = true;
            lblFullName.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblFullName.ForeColor = SystemColors.ActiveCaptionText;
            lblFullName.Location = new Point(290, 369);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(115, 28);
            lblFullName.TabIndex = 3;
            lblFullName.Text = "Full Name :";
            // 
            // fullNametxt
            // 
            fullNametxt.BackColor = SystemColors.InactiveBorder;
            fullNametxt.Font = new Font("Segoe UI", 13.8F);
            fullNametxt.ForeColor = SystemColors.ActiveCaptionText;
            fullNametxt.Location = new Point(290, 400);
            fullNametxt.Multiline = true;
            fullNametxt.Name = "fullNametxt";
            fullNametxt.PlaceholderText = "Enter Full Name";
            fullNametxt.Size = new Size(423, 44);
            fullNametxt.TabIndex = 4;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(327, 120);
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
            label1.Location = new Point(358, 71);
            label1.Name = "label1";
            label1.Size = new Size(263, 46);
            label1.TabIndex = 1;
            label1.Text = "Create Account";
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
            footerPanel.Location = new Point(976, 907);
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
            lblEmail.Location = new Point(40, 55);
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
            lblContactTitle.Location = new Point(40, 30);
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
            pictureBox3.Location = new Point(1172, 164);
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
            label6.Location = new Point(1266, 524);
            label6.Name = "label6";
            label6.Size = new Size(399, 25);
            label6.TabIndex = 12;
            label6.Text = "Streamlining Your Boarding House Operations";
            // 
            // Register
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(50, 52, 90);
            ClientSize = new Size(1924, 1055);
            Controls.Add(label6);
            Controls.Add(pictureBox3);
            Controls.Add(footerPanel);
            Controls.Add(panelRegisterCard);
            Name = "Register";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Register";
            WindowState = FormWindowState.Maximized;
            panelRegisterCard.ResumeLayout(false);
            panelRegisterCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            footerPanel.ResumeLayout(false);
            footerPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelRegisterCard;
        private Panel footerPanel;
        private Panel footerSeparator;
        private PictureBox pictureBox3;
        private Label label6;
        private Label lblContactTitle;
        private Label lblEmail;
        private Label lblPhone;
        private Label lblCopyright;
        private Label label1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
        private Label lblFullName;
        private TextBox fullNametxt;
        private Label lblUsername;
        private TextBox usernametxt;
        private Label lblPassword;
        private TextBox passwordtxt;
        private Label lblConfirmPassword;
        private TextBox confirmPasswordtxt;
        private Button RegisterBtn;
        private Label lblHaveAccount;
        private Label lblLoginHere;
    }
}
