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
            pictureBox1 = new PictureBox();
            label1 = new Label();
            pictureBox2 = new PictureBox();
            usernametxt = new TextBox();
            label2 = new Label();
            label3 = new Label();
            passwordtxt = new TextBox();
            LoginBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(535, 1);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(43, 44);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold | FontStyle.Italic);
            label1.Location = new Point(41, 63);
            label1.Name = "label1";
            label1.Size = new Size(494, 46);
            label1.TabIndex = 1;
            label1.Text = "Boarding House Management";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(193, 112);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(177, 161);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 2;
            pictureBox2.TabStop = false;
            // 
            // usernametxt
            // 
            usernametxt.Font = new Font("Segoe UI", 13.8F);
            usernametxt.Location = new Point(135, 364);
            usernametxt.Multiline = true;
            usernametxt.Name = "usernametxt";
            usernametxt.PlaceholderText = "Enter Username";
            usernametxt.Size = new Size(285, 44);
            usernametxt.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label2.Location = new Point(135, 333);
            label2.Name = "label2";
            label2.Size = new Size(115, 28);
            label2.TabIndex = 5;
            label2.Text = "Username :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label3.Location = new Point(135, 439);
            label3.Name = "label3";
            label3.Size = new Size(108, 28);
            label3.TabIndex = 7;
            label3.Text = "Password :";
            // 
            // passwordtxt
            // 
            passwordtxt.Font = new Font("Segoe UI", 13.8F);
            passwordtxt.Location = new Point(135, 470);
            passwordtxt.Multiline = true;
            passwordtxt.Name = "passwordtxt";
            passwordtxt.PlaceholderText = "Enter Password";
            passwordtxt.Size = new Size(285, 44);
            passwordtxt.TabIndex = 6;
            // 
            // LoginBtn
            // 
            LoginBtn.BackColor = Color.Blue;
            LoginBtn.FlatStyle = FlatStyle.Flat;
            LoginBtn.ForeColor = SystemColors.ButtonHighlight;
            LoginBtn.Location = new Point(193, 570);
            LoginBtn.Name = "LoginBtn";
            LoginBtn.Size = new Size(177, 43);
            LoginBtn.TabIndex = 9;
            LoginBtn.Text = "Login";
            LoginBtn.UseVisualStyleBackColor = false;
            LoginBtn.Click += LoginBtn_Click;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(128, 128, 255);
            ClientSize = new Size(579, 730);
            Controls.Add(LoginBtn);
            Controls.Add(label3);
            Controls.Add(passwordtxt);
            Controls.Add(label2);
            Controls.Add(usernametxt);
            Controls.Add(pictureBox2);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label label1;
        private PictureBox pictureBox2;
        private TextBox usernametxt;
        private Label label2;
        private Label label3;
        private TextBox passwordtxt;
        private Button LoginBtn;
    }
}
