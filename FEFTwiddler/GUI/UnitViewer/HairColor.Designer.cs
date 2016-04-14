namespace FEFTwiddler.GUI.UnitViewer
{
    partial class HairColor
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
            this.HairColorHex = new System.Windows.Forms.MaskedTextBox();
            this.HairColorBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.HairColorBox)).BeginInit();
            this.SuspendLayout();
            // 
            // HairColorHex
            // 
            this.HairColorHex.AsciiOnly = true;
            this.HairColorHex.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.HairColorHex.Font = new System.Drawing.Font("Consolas", 8.25F);
            this.HairColorHex.HidePromptOnLeave = true;
            this.HairColorHex.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.HairColorHex.Location = new System.Drawing.Point(55, 28);
            this.HairColorHex.Mask = "\\0x>AA AA AA";
            this.HairColorHex.Name = "HairColorHex";
            this.HairColorHex.Size = new System.Drawing.Size(68, 20);
            this.HairColorHex.SkipLiterals = false;
            this.HairColorHex.TabIndex = 78;
            this.HairColorHex.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.HairColorHex.TextChanged += new System.EventHandler(this.UpdateHairColor);
            // 
            // HairColorBox
            // 
            this.HairColorBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HairColorBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.HairColorBox.Location = new System.Drawing.Point(2, 2);
            this.HairColorBox.Name = "HairColorBox";
            this.HairColorBox.Size = new System.Drawing.Size(46, 46);
            this.HairColorBox.TabIndex = 77;
            this.HairColorBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(62, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 79;
            this.label1.Text = "Hair Color";
            // 
            // HairColor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.HairColorHex);
            this.Controls.Add(this.HairColorBox);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "HairColor";
            this.Size = new System.Drawing.Size(125, 50);
            ((System.ComponentModel.ISupportInitialize)(this.HairColorBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox HairColorHex;
        private System.Windows.Forms.PictureBox HairColorBox;
        private System.Windows.Forms.Label label1;
    }
}
