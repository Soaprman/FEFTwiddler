namespace FEFTwiddler.GUI.GlobalData
{
    partial class GlobalDataMain
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
            this.btnHairColors = new System.Windows.Forms.Button();
            this.btnUnlockSupportLog = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnHairColors
            // 
            this.btnHairColors.Location = new System.Drawing.Point(490, 70);
            this.btnHairColors.Name = "btnHairColors";
            this.btnHairColors.Size = new System.Drawing.Size(150, 147);
            this.btnHairColors.TabIndex = 1;
            this.btnHairColors.Text = "Edit Support Hair Colors";
            this.btnHairColors.UseVisualStyleBackColor = true;
            this.btnHairColors.Click += new System.EventHandler(this.btnHairColors_Click);
            // 
            // btnUnlockSupportLog
            // 
            this.btnUnlockSupportLog.Location = new System.Drawing.Point(104, 70);
            this.btnUnlockSupportLog.Name = "btnUnlockSupportLog";
            this.btnUnlockSupportLog.Size = new System.Drawing.Size(150, 147);
            this.btnUnlockSupportLog.TabIndex = 2;
            this.btnUnlockSupportLog.Text = "Unlock Full Support Log";
            this.btnUnlockSupportLog.UseVisualStyleBackColor = true;
            this.btnUnlockSupportLog.Click += new System.EventHandler(this.btnUnlockSupportLog_Click);
            // 
            // GlobalDataMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnUnlockSupportLog);
            this.Controls.Add(this.btnHairColors);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "GlobalDataMain";
            this.Size = new System.Drawing.Size(740, 480);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnHairColors;
        private System.Windows.Forms.Button btnUnlockSupportLog;
    }
}
