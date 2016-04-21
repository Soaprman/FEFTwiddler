namespace FEFTwiddler.GUI.UnitViewer
{
    partial class Stats
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
            this.lblStats = new System.Windows.Forms.Label();
            this.txtStatBytes = new System.Windows.Forms.TextBox();
            this.btnStats = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblStats
            // 
            this.lblStats.AutoSize = true;
            this.lblStats.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblStats.Location = new System.Drawing.Point(3, 8);
            this.lblStats.Name = "lblStats";
            this.lblStats.Size = new System.Drawing.Size(34, 13);
            this.lblStats.TabIndex = 62;
            this.lblStats.Text = "Stats:";
            // 
            // txtStatBytes
            // 
            this.txtStatBytes.Font = new System.Drawing.Font("Courier New", 8.25F);
            this.txtStatBytes.Location = new System.Drawing.Point(3, 32);
            this.txtStatBytes.Multiline = true;
            this.txtStatBytes.Name = "txtStatBytes";
            this.txtStatBytes.Size = new System.Drawing.Size(194, 20);
            this.txtStatBytes.TabIndex = 61;
            this.txtStatBytes.Text = "Stats";
            // 
            // btnStats
            // 
            this.btnStats.Location = new System.Drawing.Point(122, 3);
            this.btnStats.Name = "btnStats";
            this.btnStats.Size = new System.Drawing.Size(75, 23);
            this.btnStats.TabIndex = 63;
            this.btnStats.Text = "Edit...";
            this.btnStats.UseVisualStyleBackColor = true;
            this.btnStats.Click += new System.EventHandler(this.btnStats_Click);
            // 
            // Stats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnStats);
            this.Controls.Add(this.lblStats);
            this.Controls.Add(this.txtStatBytes);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Stats";
            this.Size = new System.Drawing.Size(200, 55);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.TextBox txtStatBytes;
        private System.Windows.Forms.Button btnStats;
    }
}
