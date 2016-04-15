namespace FEFTwiddler.GUI.UnitViewer
{
    partial class LearnedSkills
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
            this.btnViewLearnedSkills = new System.Windows.Forms.Button();
            this.btnAllSkills = new System.Windows.Forms.Button();
            this.btnAllSkillsDLC = new System.Windows.Forms.Button();
            this.AllSkillsEnemy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnViewLearnedSkills
            // 
            this.btnViewLearnedSkills.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnViewLearnedSkills.Location = new System.Drawing.Point(3, 3);
            this.btnViewLearnedSkills.Name = "btnViewLearnedSkills";
            this.btnViewLearnedSkills.Size = new System.Drawing.Size(147, 23);
            this.btnViewLearnedSkills.TabIndex = 75;
            this.btnViewLearnedSkills.Text = "View Learned Skills";
            this.btnViewLearnedSkills.UseVisualStyleBackColor = true;
            this.btnViewLearnedSkills.Click += new System.EventHandler(this.btnViewLearnedSkills_Click);
            // 
            // btnAllSkills
            // 
            this.btnAllSkills.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAllSkills.Location = new System.Drawing.Point(3, 38);
            this.btnAllSkills.Name = "btnAllSkills";
            this.btnAllSkills.Size = new System.Drawing.Size(147, 21);
            this.btnAllSkills.TabIndex = 76;
            this.btnAllSkills.Text = "Learn All Skills!";
            this.btnAllSkills.UseVisualStyleBackColor = true;
            this.btnAllSkills.Click += new System.EventHandler(this.btnAllSkills_Click);
            // 
            // btnAllSkillsDLC
            // 
            this.btnAllSkillsDLC.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAllSkillsDLC.Location = new System.Drawing.Point(3, 63);
            this.btnAllSkillsDLC.Name = "btnAllSkillsDLC";
            this.btnAllSkillsDLC.Size = new System.Drawing.Size(147, 22);
            this.btnAllSkillsDLC.TabIndex = 77;
            this.btnAllSkillsDLC.Text = "DLC Skills too!";
            this.btnAllSkillsDLC.UseVisualStyleBackColor = true;
            this.btnAllSkillsDLC.Click += new System.EventHandler(this.btnDLCSkills_Click);
            // 
            // AllSkillsEnemy
            // 
            this.AllSkillsEnemy.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.AllSkillsEnemy.Location = new System.Drawing.Point(3, 91);
            this.AllSkillsEnemy.Name = "AllSkillsEnemy";
            this.AllSkillsEnemy.Size = new System.Drawing.Size(147, 21);
            this.AllSkillsEnemy.TabIndex = 78;
            this.AllSkillsEnemy.Text = "Even Enemy Skills!";
            this.AllSkillsEnemy.UseVisualStyleBackColor = true;
            this.AllSkillsEnemy.Click += new System.EventHandler(this.btnEnemySkills_Click);
            // 
            // LearnedSkills
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnViewLearnedSkills);
            this.Controls.Add(this.btnAllSkills);
            this.Controls.Add(this.btnAllSkillsDLC);
            this.Controls.Add(this.AllSkillsEnemy);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "LearnedSkills";
            this.Size = new System.Drawing.Size(155, 117);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnViewLearnedSkills;
        private System.Windows.Forms.Button btnAllSkills;
        private System.Windows.Forms.Button btnAllSkillsDLC;
        private System.Windows.Forms.Button AllSkillsEnemy;
    }
}
