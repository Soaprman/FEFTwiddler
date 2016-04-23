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
            this.chkDeployed = new System.Windows.Forms.CheckBox();
            this.chkRecruited = new System.Windows.Forms.CheckBox();
            this.chkEinherjar = new System.Windows.Forms.CheckBox();
            this.chkDead = new System.Windows.Forms.CheckBox();
            this.cmbDeathChapter = new System.Windows.Forms.ComboBox();
            this.chkAbsent = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chkDeployed
            // 
            this.chkDeployed.AutoSize = true;
            this.chkDeployed.Enabled = false;
            this.chkDeployed.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkDeployed.Location = new System.Drawing.Point(3, 3);
            this.chkDeployed.Name = "chkDeployed";
            this.chkDeployed.Size = new System.Drawing.Size(106, 17);
            this.chkDeployed.TabIndex = 69;
            this.chkDeployed.Text = "Deployed to map";
            this.chkDeployed.UseVisualStyleBackColor = true;
            // 
            // chkRecruited
            // 
            this.chkRecruited.AutoSize = true;
            this.chkRecruited.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkRecruited.Location = new System.Drawing.Point(3, 72);
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
            this.chkEinherjar.Location = new System.Drawing.Point(3, 49);
            this.chkEinherjar.Name = "chkEinherjar";
            this.chkEinherjar.Size = new System.Drawing.Size(151, 17);
            this.chkEinherjar.TabIndex = 67;
            this.chkEinherjar.Text = "Einherjar (white portrait bg)";
            this.chkEinherjar.UseVisualStyleBackColor = true;
            // 
            // chkDead
            // 
            this.chkDead.AutoSize = true;
            this.chkDead.Enabled = false;
            this.chkDead.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkDead.Location = new System.Drawing.Point(3, 26);
            this.chkDead.Name = "chkDead";
            this.chkDead.Size = new System.Drawing.Size(52, 17);
            this.chkDead.TabIndex = 66;
            this.chkDead.Text = "Dead";
            this.chkDead.UseVisualStyleBackColor = true;
            this.chkDead.CheckedChanged += new System.EventHandler(this.chkDead_CheckedChanged);
            // 
            // cmbDeathChapter
            // 
            this.cmbDeathChapter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeathChapter.Enabled = false;
            this.cmbDeathChapter.FormattingEnabled = true;
            this.cmbDeathChapter.Location = new System.Drawing.Point(58, 23);
            this.cmbDeathChapter.Name = "cmbDeathChapter";
            this.cmbDeathChapter.Size = new System.Drawing.Size(139, 21);
            this.cmbDeathChapter.TabIndex = 70;
            // 
            // chkAbsent
            // 
            this.chkAbsent.AutoSize = true;
            this.chkAbsent.Enabled = false;
            this.chkAbsent.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkAbsent.Location = new System.Drawing.Point(3, 95);
            this.chkAbsent.Name = "chkAbsent";
            this.chkAbsent.Size = new System.Drawing.Size(194, 17);
            this.chkAbsent.TabIndex = 71;
            this.chkAbsent.Text = "Absent (left at ch 6, hasn\'t returned)";
            this.chkAbsent.UseVisualStyleBackColor = true;
            // 
            // Flags
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkAbsent);
            this.Controls.Add(this.cmbDeathChapter);
            this.Controls.Add(this.chkDeployed);
            this.Controls.Add(this.chkRecruited);
            this.Controls.Add(this.chkEinherjar);
            this.Controls.Add(this.chkDead);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Flags";
            this.Size = new System.Drawing.Size(200, 115);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkDeployed;
        private System.Windows.Forms.CheckBox chkRecruited;
        private System.Windows.Forms.CheckBox chkEinherjar;
        private System.Windows.Forms.CheckBox chkDead;
        private System.Windows.Forms.ComboBox cmbDeathChapter;
        private System.Windows.Forms.CheckBox chkAbsent;
    }
}
