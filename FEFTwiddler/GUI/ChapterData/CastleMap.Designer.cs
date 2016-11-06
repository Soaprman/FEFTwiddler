namespace FEFTwiddler.GUI.ChapterData
{
    partial class CastleMap
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
            this.picCastle = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picCastle)).BeginInit();
            this.SuspendLayout();
            // 
            // picCastle
            // 
            this.picCastle.BackColor = System.Drawing.SystemColors.Control;
            this.picCastle.Location = new System.Drawing.Point(3, 3);
            this.picCastle.Name = "picCastle";
            this.picCastle.Size = new System.Drawing.Size(522, 540);
            this.picCastle.TabIndex = 0;
            this.picCastle.TabStop = false;
            this.picCastle.Paint += new System.Windows.Forms.PaintEventHandler(this.picCastle_Paint);
            // 
            // CastleMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.picCastle);
            this.Name = "CastleMap";
            this.Size = new System.Drawing.Size(529, 547);
            this.Load += new System.EventHandler(this.CastleMap_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picCastle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picCastle;
    }
}
