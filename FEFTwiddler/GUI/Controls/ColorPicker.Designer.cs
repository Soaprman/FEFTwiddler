namespace FEFTwiddler.GUI.Controls
{
    partial class ColorPicker
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
            this.pnlPreview = new System.Windows.Forms.Panel();
            this.txtHex = new System.Windows.Forms.MaskedTextBox();
            this.SuspendLayout();
            // 
            // pnlPreview
            // 
            this.pnlPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPreview.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlPreview.Location = new System.Drawing.Point(3, 3);
            this.pnlPreview.Name = "pnlPreview";
            this.pnlPreview.Size = new System.Drawing.Size(20, 20);
            this.pnlPreview.TabIndex = 0;
            // 
            // txtHex
            // 
            this.txtHex.AsciiOnly = true;
            this.txtHex.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.txtHex.Font = new System.Drawing.Font("Consolas", 8.25F);
            this.txtHex.HidePromptOnLeave = true;
            this.txtHex.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.txtHex.Location = new System.Drawing.Point(29, 3);
            this.txtHex.Mask = "\\0x>AA AA AA";
            this.txtHex.Name = "txtHex";
            this.txtHex.Size = new System.Drawing.Size(68, 20);
            this.txtHex.SkipLiterals = false;
            this.txtHex.TabIndex = 79;
            this.txtHex.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // ColorPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtHex);
            this.Controls.Add(this.pnlPreview);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ColorPicker";
            this.Size = new System.Drawing.Size(100, 26);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlPreview;
        private System.Windows.Forms.MaskedTextBox txtHex;
    }
}
