namespace FEFTwiddler.GUI
{
    partial class SupportPanel
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
            this.lblName = new System.Windows.Forms.Label();
            this.cmbSupport = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(-3, 3);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(36, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Jakob";
            // 
            // cmbSupport
            // 
            this.cmbSupport.FormattingEnabled = true;
            this.cmbSupport.Location = new System.Drawing.Point(77, 0);
            this.cmbSupport.Name = "cmbSupport";
            this.cmbSupport.Size = new System.Drawing.Size(106, 21);
            this.cmbSupport.TabIndex = 1;
            // 
            // SupportPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbSupport);
            this.Controls.Add(this.lblName);
            this.Name = "SupportPanel";
            this.Size = new System.Drawing.Size(185, 21);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.ComboBox cmbSupport;
    }
}
