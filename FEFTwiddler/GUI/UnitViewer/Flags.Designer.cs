namespace FEFTwiddler.GUI.UnitViewer
{
    partial class Flags
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
            this.chkRecruited = new System.Windows.Forms.CheckBox();
            this.chkEinherjar = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chkRecruited
            // 
            this.chkRecruited.AutoSize = true;
            this.chkRecruited.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkRecruited.Location = new System.Drawing.Point(3, 26);
            this.chkRecruited.Name = "chkRecruited";
            this.chkRecruited.Size = new System.Drawing.Size(131, 17);
            this.chkRecruited.TabIndex = 68;
            this.chkRecruited.Text = "Recruited (shield icon)";
            this.chkRecruited.UseVisualStyleBackColor = true;
            // 
            // chkEinherjar
            // 
            this.chkEinherjar.AutoSize = true;
            this.chkEinherjar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkEinherjar.Location = new System.Drawing.Point(3, 3);
            this.chkEinherjar.Name = "chkEinherjar";
            this.chkEinherjar.Size = new System.Drawing.Size(151, 17);
            this.chkEinherjar.TabIndex = 67;
            this.chkEinherjar.Text = "Einherjar (white portrait bg)";
            this.chkEinherjar.UseVisualStyleBackColor = true;
            // 
            // Flags
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkRecruited);
            this.Controls.Add(this.chkEinherjar);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Flags";
            this.Size = new System.Drawing.Size(154, 46);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox chkRecruited;
        private System.Windows.Forms.CheckBox chkEinherjar;
    }
}
