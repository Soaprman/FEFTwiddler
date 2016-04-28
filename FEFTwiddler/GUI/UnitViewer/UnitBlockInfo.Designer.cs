namespace FEFTwiddler.GUI.UnitViewer
{
    partial class UnitBlockInfo
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
            this.grpUnitBlock = new System.Windows.Forms.GroupBox();
            this.chkChallenge = new System.Windows.Forms.CheckBox();
            this.cmbDeathChapter = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rdoDeadByPlot = new System.Windows.Forms.RadioButton();
            this.rdoDeadByGameplay = new System.Windows.Forms.RadioButton();
            this.rdoAbsent = new System.Windows.Forms.RadioButton();
            this.rdoDeployed = new System.Windows.Forms.RadioButton();
            this.rdoLiving = new System.Windows.Forms.RadioButton();
            this.grpUnitBlock.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpUnitBlock
            // 
            this.grpUnitBlock.Controls.Add(this.chkChallenge);
            this.grpUnitBlock.Controls.Add(this.cmbDeathChapter);
            this.grpUnitBlock.Controls.Add(this.label1);
            this.grpUnitBlock.Controls.Add(this.rdoDeadByPlot);
            this.grpUnitBlock.Controls.Add(this.rdoDeadByGameplay);
            this.grpUnitBlock.Controls.Add(this.rdoAbsent);
            this.grpUnitBlock.Controls.Add(this.rdoDeployed);
            this.grpUnitBlock.Controls.Add(this.rdoLiving);
            this.grpUnitBlock.Location = new System.Drawing.Point(1, 1);
            this.grpUnitBlock.Name = "grpUnitBlock";
            this.grpUnitBlock.Size = new System.Drawing.Size(257, 136);
            this.grpUnitBlock.TabIndex = 0;
            this.grpUnitBlock.TabStop = false;
            this.grpUnitBlock.Text = "Unit Status:";
            // 
            // chkChallenge
            // 
            this.chkChallenge.AutoSize = true;
            this.chkChallenge.Location = new System.Drawing.Point(151, 110);
            this.chkChallenge.Name = "chkChallenge";
            this.chkChallenge.Size = new System.Drawing.Size(103, 17);
            this.chkChallenge.TabIndex = 72;
            this.chkChallenge.Text = "Challenge Battle";
            this.chkChallenge.UseVisualStyleBackColor = true;
            // 
            // cmbDeathChapter
            // 
            this.cmbDeathChapter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeathChapter.FormattingEnabled = true;
            this.cmbDeathChapter.Location = new System.Drawing.Point(6, 108);
            this.cmbDeathChapter.Name = "cmbDeathChapter";
            this.cmbDeathChapter.Size = new System.Drawing.Size(139, 21);
            this.cmbDeathChapter.TabIndex = 71;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Died on:";
            // 
            // rdoDeadByPlot
            // 
            this.rdoDeadByPlot.AutoSize = true;
            this.rdoDeadByPlot.Location = new System.Drawing.Point(140, 68);
            this.rdoDeadByPlot.Name = "rdoDeadByPlot";
            this.rdoDeadByPlot.Size = new System.Drawing.Size(86, 17);
            this.rdoDeadByPlot.TabIndex = 4;
            this.rdoDeadByPlot.TabStop = true;
            this.rdoDeadByPlot.Text = "Dead by Plot";
            this.rdoDeadByPlot.UseVisualStyleBackColor = true;
            // 
            // rdoDeadByGameplay
            // 
            this.rdoDeadByGameplay.AutoSize = true;
            this.rdoDeadByGameplay.Location = new System.Drawing.Point(7, 68);
            this.rdoDeadByGameplay.Name = "rdoDeadByGameplay";
            this.rdoDeadByGameplay.Size = new System.Drawing.Size(115, 17);
            this.rdoDeadByGameplay.TabIndex = 3;
            this.rdoDeadByGameplay.TabStop = true;
            this.rdoDeadByGameplay.Text = "Dead by Gameplay";
            this.rdoDeadByGameplay.UseVisualStyleBackColor = true;
            // 
            // rdoAbsent
            // 
            this.rdoAbsent.AutoSize = true;
            this.rdoAbsent.Location = new System.Drawing.Point(7, 44);
            this.rdoAbsent.Name = "rdoAbsent";
            this.rdoAbsent.Size = new System.Drawing.Size(107, 17);
            this.rdoAbsent.TabIndex = 2;
            this.rdoAbsent.TabStop = true;
            this.rdoAbsent.Text = "Absent from party";
            this.rdoAbsent.UseVisualStyleBackColor = true;
            // 
            // rdoDeployed
            // 
            this.rdoDeployed.AutoSize = true;
            this.rdoDeployed.Location = new System.Drawing.Point(84, 20);
            this.rdoDeployed.Name = "rdoDeployed";
            this.rdoDeployed.Size = new System.Drawing.Size(140, 17);
            this.rdoDeployed.TabIndex = 1;
            this.rdoDeployed.TabStop = true;
            this.rdoDeployed.Text = "Alive + Deployed to map";
            this.rdoDeployed.UseVisualStyleBackColor = true;
            // 
            // rdoLiving
            // 
            this.rdoLiving.AutoSize = true;
            this.rdoLiving.Location = new System.Drawing.Point(7, 20);
            this.rdoLiving.Name = "rdoLiving";
            this.rdoLiving.Size = new System.Drawing.Size(48, 17);
            this.rdoLiving.TabIndex = 0;
            this.rdoLiving.TabStop = true;
            this.rdoLiving.Text = "Alive";
            this.rdoLiving.UseVisualStyleBackColor = true;
            // 
            // UnitBlockInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpUnitBlock);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UnitBlockInfo";
            this.Size = new System.Drawing.Size(260, 138);
            this.grpUnitBlock.ResumeLayout(false);
            this.grpUnitBlock.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpUnitBlock;
        private System.Windows.Forms.RadioButton rdoDeadByPlot;
        private System.Windows.Forms.RadioButton rdoDeadByGameplay;
        private System.Windows.Forms.RadioButton rdoAbsent;
        private System.Windows.Forms.RadioButton rdoDeployed;
        private System.Windows.Forms.RadioButton rdoLiving;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDeathChapter;
        private System.Windows.Forms.CheckBox chkChallenge;
    }
}
