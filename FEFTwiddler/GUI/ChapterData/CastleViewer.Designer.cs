namespace FEFTwiddler.GUI.ChapterData
{
    partial class CastleViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CastleViewer));
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBuildingList = new System.Windows.Forms.TextBox();
            this.castleMap1 = new FEFTwiddler.GUI.ChapterData.CastleMap();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(933, 726);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(585, 52);
            this.label1.TabIndex = 1;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // txtBuildingList
            // 
            this.txtBuildingList.Location = new System.Drawing.Point(16, 83);
            this.txtBuildingList.Multiline = true;
            this.txtBuildingList.Name = "txtBuildingList";
            this.txtBuildingList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBuildingList.Size = new System.Drawing.Size(345, 296);
            this.txtBuildingList.TabIndex = 2;
            // 
            // castleMap1
            // 
            this.castleMap1.Location = new System.Drawing.Point(367, 83);
            this.castleMap1.Name = "castleMap1";
            this.castleMap1.Size = new System.Drawing.Size(530, 598);
            this.castleMap1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(109, 450);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Ignore the drawing to your right!";
            // 
            // CastleViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 761);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.castleMap1);
            this.Controls.Add(this.txtBuildingList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Name = "CastleViewer";
            this.Text = "Castle Viewer";
            this.Load += new System.EventHandler(this.CastleMap_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBuildingList;
        private CastleMap castleMap1;
        private System.Windows.Forms.Label label2;
    }
}