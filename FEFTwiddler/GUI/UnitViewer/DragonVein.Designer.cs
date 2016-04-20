namespace FEFTwiddler.GUI.UnitViewer
{
    partial class DragonVein
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
            this.chkDragonVein = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chkDragonVein
            // 
            this.chkDragonVein.AutoSize = true;
            this.chkDragonVein.Location = new System.Drawing.Point(4, 4);
            this.chkDragonVein.Name = "chkDragonVein";
            this.chkDragonVein.Size = new System.Drawing.Size(85, 17);
            this.chkDragonVein.TabIndex = 0;
            this.chkDragonVein.Text = "Dragon Vein";
            this.chkDragonVein.UseVisualStyleBackColor = true;
            this.chkDragonVein.CheckedChanged += new System.EventHandler(this.chkDragonVein_CheckedChanged);
            // 
            // DragonVein
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkDragonVein);
            this.Name = "DragonVein";
            this.Size = new System.Drawing.Size(93, 25);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkDragonVein;
    }
}
