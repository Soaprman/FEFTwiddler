namespace FEFTwiddler.GUI.ChapterData
{
    partial class Difficulty
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
            this.grpDifficulty = new System.Windows.Forms.GroupBox();
            this.grpDeathPenalty = new System.Windows.Forms.GroupBox();
            this.rdoNormal = new System.Windows.Forms.RadioButton();
            this.rdoHard = new System.Windows.Forms.RadioButton();
            this.rdoLunatic = new System.Windows.Forms.RadioButton();
            this.rdoCasual = new System.Windows.Forms.RadioButton();
            this.rdoClassic = new System.Windows.Forms.RadioButton();
            this.rdoPhoenix = new System.Windows.Forms.RadioButton();
            this.grpDifficulty.SuspendLayout();
            this.grpDeathPenalty.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpDifficulty
            // 
            this.grpDifficulty.Controls.Add(this.rdoLunatic);
            this.grpDifficulty.Controls.Add(this.rdoHard);
            this.grpDifficulty.Controls.Add(this.rdoNormal);
            this.grpDifficulty.Location = new System.Drawing.Point(4, 4);
            this.grpDifficulty.Name = "grpDifficulty";
            this.grpDifficulty.Size = new System.Drawing.Size(215, 42);
            this.grpDifficulty.TabIndex = 0;
            this.grpDifficulty.TabStop = false;
            this.grpDifficulty.Text = "Difficulty";
            // 
            // grpDeathPenalty
            // 
            this.grpDeathPenalty.Controls.Add(this.rdoPhoenix);
            this.grpDeathPenalty.Controls.Add(this.rdoClassic);
            this.grpDeathPenalty.Controls.Add(this.rdoCasual);
            this.grpDeathPenalty.Location = new System.Drawing.Point(4, 52);
            this.grpDeathPenalty.Name = "grpDeathPenalty";
            this.grpDeathPenalty.Size = new System.Drawing.Size(215, 42);
            this.grpDeathPenalty.TabIndex = 1;
            this.grpDeathPenalty.TabStop = false;
            this.grpDeathPenalty.Text = "Death Penalty";
            // 
            // rdoNormal
            // 
            this.rdoNormal.AutoSize = true;
            this.rdoNormal.Location = new System.Drawing.Point(6, 19);
            this.rdoNormal.Name = "rdoNormal";
            this.rdoNormal.Size = new System.Drawing.Size(58, 17);
            this.rdoNormal.TabIndex = 0;
            this.rdoNormal.TabStop = true;
            this.rdoNormal.Text = "Normal";
            this.rdoNormal.UseVisualStyleBackColor = true;
            // 
            // rdoHard
            // 
            this.rdoHard.AutoSize = true;
            this.rdoHard.Location = new System.Drawing.Point(79, 19);
            this.rdoHard.Name = "rdoHard";
            this.rdoHard.Size = new System.Drawing.Size(48, 17);
            this.rdoHard.TabIndex = 1;
            this.rdoHard.TabStop = true;
            this.rdoHard.Text = "Hard";
            this.rdoHard.UseVisualStyleBackColor = true;
            // 
            // rdoLunatic
            // 
            this.rdoLunatic.AutoSize = true;
            this.rdoLunatic.Location = new System.Drawing.Point(143, 19);
            this.rdoLunatic.Name = "rdoLunatic";
            this.rdoLunatic.Size = new System.Drawing.Size(60, 17);
            this.rdoLunatic.TabIndex = 2;
            this.rdoLunatic.TabStop = true;
            this.rdoLunatic.Text = "Lunatic";
            this.rdoLunatic.UseVisualStyleBackColor = true;
            // 
            // rdoCasual
            // 
            this.rdoCasual.AutoSize = true;
            this.rdoCasual.Location = new System.Drawing.Point(79, 19);
            this.rdoCasual.Name = "rdoCasual";
            this.rdoCasual.Size = new System.Drawing.Size(57, 17);
            this.rdoCasual.TabIndex = 3;
            this.rdoCasual.TabStop = true;
            this.rdoCasual.Text = "Casual";
            this.rdoCasual.UseVisualStyleBackColor = true;
            // 
            // rdoClassic
            // 
            this.rdoClassic.AutoSize = true;
            this.rdoClassic.Location = new System.Drawing.Point(145, 19);
            this.rdoClassic.Name = "rdoClassic";
            this.rdoClassic.Size = new System.Drawing.Size(58, 17);
            this.rdoClassic.TabIndex = 4;
            this.rdoClassic.TabStop = true;
            this.rdoClassic.Text = "Classic";
            this.rdoClassic.UseVisualStyleBackColor = true;
            // 
            // rdoPhoenix
            // 
            this.rdoPhoenix.AutoSize = true;
            this.rdoPhoenix.Location = new System.Drawing.Point(6, 19);
            this.rdoPhoenix.Name = "rdoPhoenix";
            this.rdoPhoenix.Size = new System.Drawing.Size(63, 17);
            this.rdoPhoenix.TabIndex = 5;
            this.rdoPhoenix.TabStop = true;
            this.rdoPhoenix.Text = "Phoenix";
            this.rdoPhoenix.UseVisualStyleBackColor = true;
            // 
            // Difficulty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpDeathPenalty);
            this.Controls.Add(this.grpDifficulty);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Difficulty";
            this.Size = new System.Drawing.Size(223, 98);
            this.grpDifficulty.ResumeLayout(false);
            this.grpDifficulty.PerformLayout();
            this.grpDeathPenalty.ResumeLayout(false);
            this.grpDeathPenalty.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpDifficulty;
        private System.Windows.Forms.GroupBox grpDeathPenalty;
        private System.Windows.Forms.RadioButton rdoLunatic;
        private System.Windows.Forms.RadioButton rdoHard;
        private System.Windows.Forms.RadioButton rdoNormal;
        private System.Windows.Forms.RadioButton rdoPhoenix;
        private System.Windows.Forms.RadioButton rdoClassic;
        private System.Windows.Forms.RadioButton rdoCasual;
    }
}
