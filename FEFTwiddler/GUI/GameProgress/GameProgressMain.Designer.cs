namespace FEFTwiddler.GUI.GameProgress
{
    partial class GameProgressMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameProgressMain));
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.flwBattlefieldsAvailable = new System.Windows.Forms.FlowLayoutPanel();
            this.btnUnlockAmiiboChapters = new System.Windows.Forms.Button();
            this.flwChaptersCompleted = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Completed chapters:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 303);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(173, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Available (not completed) chapters:";
            // 
            // flwBattlefieldsAvailable
            // 
            this.flwBattlefieldsAvailable.AutoScroll = true;
            this.flwBattlefieldsAvailable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flwBattlefieldsAvailable.Location = new System.Drawing.Point(3, 319);
            this.flwBattlefieldsAvailable.Name = "flwBattlefieldsAvailable";
            this.flwBattlefieldsAvailable.Size = new System.Drawing.Size(586, 158);
            this.flwBattlefieldsAvailable.TabIndex = 11;
            // 
            // btnUnlockAmiiboChapters
            // 
            this.btnUnlockAmiiboChapters.Location = new System.Drawing.Point(595, 319);
            this.btnUnlockAmiiboChapters.Name = "btnUnlockAmiiboChapters";
            this.btnUnlockAmiiboChapters.Size = new System.Drawing.Size(94, 100);
            this.btnUnlockAmiiboChapters.TabIndex = 10;
            this.btnUnlockAmiiboChapters.Text = "Unlock amiibo chapters";
            this.btnUnlockAmiiboChapters.UseVisualStyleBackColor = true;
            this.btnUnlockAmiiboChapters.Click += new System.EventHandler(this.btnUnlockAmiiboChapters_Click);
            // 
            // flwChaptersCompleted
            // 
            this.flwChaptersCompleted.AutoScroll = true;
            this.flwChaptersCompleted.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flwChaptersCompleted.Location = new System.Drawing.Point(3, 19);
            this.flwChaptersCompleted.Name = "flwChaptersCompleted";
            this.flwChaptersCompleted.Size = new System.Drawing.Size(586, 278);
            this.flwChaptersCompleted.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(595, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 156);
            this.label1.TabIndex = 14;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // GameProgressMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.flwBattlefieldsAvailable);
            this.Controls.Add(this.btnUnlockAmiiboChapters);
            this.Controls.Add(this.flwChaptersCompleted);
            this.Name = "GameProgressMain";
            this.Size = new System.Drawing.Size(740, 480);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel flwBattlefieldsAvailable;
        private System.Windows.Forms.Button btnUnlockAmiiboChapters;
        private System.Windows.Forms.FlowLayoutPanel flwChaptersCompleted;
        private System.Windows.Forms.Label label1;
    }
}
