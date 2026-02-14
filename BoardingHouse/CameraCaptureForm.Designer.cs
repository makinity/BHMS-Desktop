namespace BoardingHouse
{
    partial class CameraCaptureForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.ComboBox cboDevices;
        private System.Windows.Forms.PictureBox pbPreview;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnCapture;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblDevice;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            cboDevices = new ComboBox();
            pbPreview = new PictureBox();
            btnStart = new Button();
            btnCapture = new Button();
            btnClose = new Button();
            lblDevice = new Label();
            ((System.ComponentModel.ISupportInitialize)pbPreview).BeginInit();
            SuspendLayout();
            // 
            // lblDevice
            // 
            lblDevice.AutoSize = true;
            lblDevice.Location = new Point(12, 15);
            lblDevice.Name = "lblDevice";
            lblDevice.Size = new Size(58, 20);
            lblDevice.TabIndex = 0;
            lblDevice.Text = "Device:";
            // 
            // cboDevices
            // 
            cboDevices.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDevices.FormattingEnabled = true;
            cboDevices.Location = new Point(76, 12);
            cboDevices.Name = "cboDevices";
            cboDevices.Size = new Size(500, 28);
            cboDevices.TabIndex = 1;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(582, 11);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(90, 30);
            btnStart.TabIndex = 2;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // btnCapture
            // 
            btnCapture.Location = new Point(678, 11);
            btnCapture.Name = "btnCapture";
            btnCapture.Size = new Size(100, 30);
            btnCapture.TabIndex = 3;
            btnCapture.Text = "Capture";
            btnCapture.UseVisualStyleBackColor = true;
            btnCapture.Click += btnCapture_Click;
            // 
            // pbPreview
            // 
            pbPreview.BackColor = Color.Black;
            pbPreview.BorderStyle = BorderStyle.FixedSingle;
            pbPreview.Location = new Point(12, 52);
            pbPreview.Name = "pbPreview";
            pbPreview.Size = new Size(766, 340);
            pbPreview.SizeMode = PictureBoxSizeMode.StretchImage;
            pbPreview.TabIndex = 4;
            pbPreview.TabStop = false;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(678, 404);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(100, 34);
            btnClose.TabIndex = 5;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // CameraCaptureForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnClose);
            Controls.Add(pbPreview);
            Controls.Add(btnCapture);
            Controls.Add(btnStart);
            Controls.Add(cboDevices);
            Controls.Add(lblDevice);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CameraCaptureForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Camera";
            Load += CameraCaptureForm_Load;
            FormClosing += CameraCaptureForm_FormClosing;
            ((System.ComponentModel.ISupportInitialize)pbPreview).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
