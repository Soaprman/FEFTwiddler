namespace FEFTwiddler.GUI.ChapterData
{
    partial class MegacheatsMain
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
            this.AllCharMaxStatue = new System.Windows.Forms.Button();
            this.btnMaxBoots = new System.Windows.Forms.Button();
            this.btnMaxWeaponExp = new System.Windows.Forms.Button();
            this.btnGiveEternalSeals = new System.Windows.Forms.Button();
            this.btn1Boots = new System.Windows.Forms.Button();
            this.btn0Boots = new System.Windows.Forms.Button();
            this.skills1 = new FEFTwiddler.GUI.ChapterData.Megacheats.Skills();
            this.SuspendLayout();
            // 
            // AllCharMaxStatue
            // 
            this.AllCharMaxStatue.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.AllCharMaxStatue.Location = new System.Drawing.Point(3, 330);
            this.AllCharMaxStatue.Name = "AllCharMaxStatue";
            this.AllCharMaxStatue.Size = new System.Drawing.Size(218, 62);
            this.AllCharMaxStatue.TabIndex = 13;
            this.AllCharMaxStatue.Text = "Set Battles and Victories to a minimum of 100 for all Characters (allows lvl 3 st" +
    "atues)";
            this.AllCharMaxStatue.UseVisualStyleBackColor = true;
            this.AllCharMaxStatue.Click += new System.EventHandler(this.btnAllCharMaxStatue_Click);
            // 
            // btnMaxBoots
            // 
            this.btnMaxBoots.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMaxBoots.Location = new System.Drawing.Point(3, 262);
            this.btnMaxBoots.Name = "btnMaxBoots";
            this.btnMaxBoots.Size = new System.Drawing.Size(218, 62);
            this.btnMaxBoots.TabIndex = 10;
            this.btnMaxBoots.Text = "Make all characters have 2 Boots (movement +2)";
            this.btnMaxBoots.UseVisualStyleBackColor = true;
            this.btnMaxBoots.Click += new System.EventHandler(this.btnMaxBoots_Click);
            // 
            // btnMaxWeaponExp
            // 
            this.btnMaxWeaponExp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMaxWeaponExp.Location = new System.Drawing.Point(3, 398);
            this.btnMaxWeaponExp.Name = "btnMaxWeaponExp";
            this.btnMaxWeaponExp.Size = new System.Drawing.Size(218, 62);
            this.btnMaxWeaponExp.TabIndex = 9;
            this.btnMaxWeaponExp.Text = "Give all characters S rank in all weapons";
            this.btnMaxWeaponExp.UseVisualStyleBackColor = true;
            this.btnMaxWeaponExp.Click += new System.EventHandler(this.btnMaxWeaponExp_Click);
            // 
            // btnGiveEternalSeals
            // 
            this.btnGiveEternalSeals.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnGiveEternalSeals.Location = new System.Drawing.Point(227, 398);
            this.btnGiveEternalSeals.Name = "btnGiveEternalSeals";
            this.btnGiveEternalSeals.Size = new System.Drawing.Size(218, 62);
            this.btnGiveEternalSeals.TabIndex = 8;
            this.btnGiveEternalSeals.Text = "Set all characters\' eternal seal count to 16 (allows leveling to 99)";
            this.btnGiveEternalSeals.UseVisualStyleBackColor = true;
            this.btnGiveEternalSeals.Click += new System.EventHandler(this.btnGiveEternalSeals_Click);
            // 
            // btn1Boots
            // 
            this.btn1Boots.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn1Boots.Location = new System.Drawing.Point(227, 262);
            this.btn1Boots.Name = "btn1Boots";
            this.btn1Boots.Size = new System.Drawing.Size(82, 62);
            this.btn1Boots.TabIndex = 14;
            this.btn1Boots.Text = "Just 1 Boots";
            this.btn1Boots.UseVisualStyleBackColor = true;
            this.btn1Boots.Click += new System.EventHandler(this.btn1Boots_Click);
            // 
            // btn0Boots
            // 
            this.btn0Boots.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn0Boots.Location = new System.Drawing.Point(315, 262);
            this.btn0Boots.Name = "btn0Boots";
            this.btn0Boots.Size = new System.Drawing.Size(82, 62);
            this.btn0Boots.TabIndex = 15;
            this.btn0Boots.Text = "No Boots";
            this.btn0Boots.UseVisualStyleBackColor = true;
            this.btn0Boots.Click += new System.EventHandler(this.btn0Boots_Click);
            // 
            // skills1
            // 
            this.skills1.Location = new System.Drawing.Point(3, 0);
            this.skills1.Margin = new System.Windows.Forms.Padding(0);
            this.skills1.Name = "skills1";
            this.skills1.Size = new System.Drawing.Size(557, 259);
            this.skills1.TabIndex = 16;
            // 
            // MegacheatsMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.skills1);
            this.Controls.Add(this.btn0Boots);
            this.Controls.Add(this.btn1Boots);
            this.Controls.Add(this.AllCharMaxStatue);
            this.Controls.Add(this.btnMaxBoots);
            this.Controls.Add(this.btnMaxWeaponExp);
            this.Controls.Add(this.btnGiveEternalSeals);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "MegacheatsMain";
            this.Size = new System.Drawing.Size(740, 480);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button AllCharMaxStatue;
        private System.Windows.Forms.Button btnMaxBoots;
        private System.Windows.Forms.Button btnMaxWeaponExp;
        private System.Windows.Forms.Button btnGiveEternalSeals;
        private System.Windows.Forms.Button btn1Boots;
        private System.Windows.Forms.Button btn0Boots;
        private Megacheats.Skills skills1;
    }
}
