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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChapterHistory));
            this.flwChaptersCompleted = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnUnlockAmiiboChapters = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // flwChaptersCompleted
            // 
            this.flwChaptersCompleted.AutoScroll = true;
            this.flwChaptersCompleted.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flwChaptersCompleted.Location = new System.Drawing.Point(12, 65);
            this.flwChaptersCompleted.Name = "flwChaptersCompleted";
            this.flwChaptersCompleted.Size = new System.Drawing.Size(586, 296);
            this.flwChaptersCompleted.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(604, 296);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 65);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Close";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(426, 52);
            this.label1.TabIndex = 3;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // btnUnlockAmiiboChapters
            // 
            this.btnUnlockAmiiboChapters.Location = new System.Drawing.Point(604, 65);
            this.btnUnlockAmiiboChapters.Name = "btnUnlockAmiiboChapters";
            this.btnUnlockAmiiboChapters.Size = new System.Drawing.Size(94, 65);
            this.btnUnlockAmiiboChapters.TabIndex = 4;
            this.btnUnlockAmiiboChapters.Text = "Unlock amiibo chapters";
            this.btnUnlockAmiiboChapters.UseVisualStyleBackColor = true;
            this.btnUnlockAmiiboChapters.Click += new System.EventHandler(this.btnUnlockAmiiboChapters_Click);
            // 
            // ChapterHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 368);
            this.Controls.Add(this.btnUnlockAmiiboChapters);
            this.Controls.Add(this.label1);
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnUnlockAmiiboChapters;
    }
}