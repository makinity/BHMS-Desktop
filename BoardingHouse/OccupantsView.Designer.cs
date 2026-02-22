namespace BoardingHouse
{
    partial class OccupantsView
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
            pnlTop = new Panel();
            lblOccupantType = new Label();
            cbOccupantType = new ComboBox();
            pnlHost = new Panel();
            pnlTop.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.FromArgb(41, 42, 70);
            pnlTop.Controls.Add(lblOccupantType);
            pnlTop.Controls.Add(cbOccupantType);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Padding = new Padding(12, 10, 12, 10);
            pnlTop.Size = new Size(1677, 56);
            pnlTop.TabIndex = 0;
            // 
            // lblOccupantType
            // 
            lblOccupantType.AutoSize = true;
            lblOccupantType.ForeColor = SystemColors.ButtonHighlight;
            lblOccupantType.Location = new Point(15, 18);
            lblOccupantType.Name = "lblOccupantType";
            lblOccupantType.Size = new Size(107, 20);
            lblOccupantType.TabIndex = 0;
            lblOccupantType.Text = "Occupant Type";
            // 
            // cbOccupantType
            // 
            cbOccupantType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbOccupantType.FormattingEnabled = true;
            cbOccupantType.Location = new Point(126, 14);
            cbOccupantType.Name = "cbOccupantType";
            cbOccupantType.Size = new Size(181, 28);
            cbOccupantType.TabIndex = 1;
            // 
            // pnlHost
            // 
            pnlHost.Dock = DockStyle.Fill;
            pnlHost.Location = new Point(0, 56);
            pnlHost.Name = "pnlHost";
            pnlHost.Size = new Size(1677, 919);
            pnlHost.TabIndex = 1;
            // 
            // OccupantsView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlHost);
            Controls.Add(pnlTop);
            Name = "OccupantsView";
            Size = new Size(1677, 975);
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblOccupantType;
        private System.Windows.Forms.ComboBox cbOccupantType;
        private System.Windows.Forms.Panel pnlHost;
    }
}
