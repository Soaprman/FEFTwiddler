namespace FEFTwiddler.GUI.ChapterData
{
    partial class ChapterHistory
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
            this.flwChaptersCompleted = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnUnlockAmiiboChapters = new System.Windows.Forms.Button();
            this.flwBattlefieldsAvailable = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // flwChaptersCompleted
            // 
            this.flwChaptersCompleted.AutoScroll = true;
            this.flwChaptersCompleted.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flwChaptersCompleted.Location = new System.Drawing.Point(12, 25);
            this.flwChaptersCompleted.Name = "flwChaptersCompleted";
            this.flwChaptersCompleted.Size = new System.Drawing.Size(586, 278);
            this.flwChaptersCompleted.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(604, 380);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 45);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Close";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnUnlockAmiiboChapters
            // 
            this.btnUnlockAmiiboChapters.Location = new System.Drawing.Point(604, 325);
            this.btnUnlockAmiiboChapters.Name = "btnUnlockAmiiboChapters";
            this.btnUnlockAmiiboChapters.Size = new System.Drawing.Size(94, 45);
            this.btnUnlockAmiiboChapters.TabIndex = 4;
            this.btnUnlockAmiiboChapters.Text = "Unlock amiibo chapters";
            this.btnUnlockAmiiboChapters.UseVisualStyleBackColor = true;
            this.btnUnlockAmiiboChapters.Click += new System.EventHandler(this.btnUnlockAmiiboChapters_Click);
            // 
            // flwBattlefieldsAvailable
            // 
            this.flwBattlefieldsAvailable.AutoScroll = true;
            this.flwBattlefieldsAvailable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flwBattlefieldsAvailable.Location = new System.Drawing.Point(12, 325);
            this.flwBattlefieldsAvailable.Name = "flwBattlefieldsAvailable";
            this.flwBattlefieldsAvailable.Size = new System.Drawing.Size(586, 100);
            this.flwBattlefieldsAvailable.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 309);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(173, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Available (not completed) chapters:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Completed chapters:";
            // 
            // ChapterHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 437);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.flwBattlefieldsAvailable);
            this.Controls.Add(this.btnUnlockAmiiboChapters);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.flwChaptersCompleted);
            this.Name = "ChapterHistory";
            this.Text = "Chapter History";
            this.Load += new System.EventHandler(this.ChapterHistory_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flwChaptersCompleted;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnUnlockAmiiboChapters;
        private System.Windows.Forms.FlowLayoutPanel flwBattlefieldsAvailable;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}