namespace FEFTwiddler.GUI.UnitViewer
{
    partial class LevelAndExperience
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
            this.label21 = new System.Windows.Forms.Label();
            this.numInternalLevel = new System.Windows.Forms.NumericUpDown();
            this.label20 = new System.Windows.Forms.Label();
            this.numEternalSeals = new System.Windows.Forms.NumericUpDown();
            this.label19 = new System.Windows.Forms.Label();
            this.numBoots = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numExperience = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numLevel = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numInternalLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEternalSeals)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBoots)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExperience)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLevel)).BeginInit();
            this.SuspendLayout();
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label21.Location = new System.Drawing.Point(14, 31);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(58, 13);
            this.label21.TabIndex = 81;
            this.label21.Text = "Internal LV";
            // 
            // numInternalLevel
            // 
            this.numInternalLevel.Enabled = false;
            this.numInternalLevel.Location = new System.Drawing.Point(74, 29);
            this.numInternalLevel.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numInternalLevel.Name = "numInternalLevel";
            this.numInternalLevel.Size = new System.Drawing.Size(38, 20);
            this.numInternalLevel.TabIndex = 80;
            this.numInternalLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label20.Location = new System.Drawing.Point(3, 83);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(69, 13);
            this.label20.TabIndex = 79;
            this.label20.Text = "Eternal Seals";
            // 
            // numEternalSeals
            // 
            this.numEternalSeals.Location = new System.Drawing.Point(74, 81);
            this.numEternalSeals.Maximum = new decimal(new int[] {
            47,
            0,
            0,
            0});
            this.numEternalSeals.Name = "numEternalSeals";
            this.numEternalSeals.Size = new System.Drawing.Size(38, 20);
            this.numEternalSeals.TabIndex = 78;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label19.Location = new System.Drawing.Point(38, 109);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(34, 13);
            this.label19.TabIndex = 77;
            this.label19.Text = "Boots";
            // 
            // numBoots
            // 
            this.numBoots.Location = new System.Drawing.Point(74, 107);
            this.numBoots.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numBoots.Name = "numBoots";
            this.numBoots.Size = new System.Drawing.Size(38, 20);
            this.numBoots.TabIndex = 76;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(44, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 75;
            this.label2.Text = "EXP";
            // 
            // numExperience
            // 
            this.numExperience.Location = new System.Drawing.Point(74, 55);
            this.numExperience.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numExperience.Name = "numExperience";
            this.numExperience.Size = new System.Drawing.Size(38, 20);
            this.numExperience.TabIndex = 74;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(52, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 73;
            this.label1.Text = "LV";
            // 
            // numLevel
            // 
            this.numLevel.Location = new System.Drawing.Point(74, 3);
            this.numLevel.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numLevel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLevel.Name = "numLevel";
            this.numLevel.Size = new System.Drawing.Size(38, 20);
            this.numLevel.TabIndex = 72;
            this.numLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // LevelAndExperience
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label21);
            this.Controls.Add(this.numInternalLevel);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.numEternalSeals);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.numBoots);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numExperience);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numLevel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "LevelAndExperience";
            this.Size = new System.Drawing.Size(115, 130);
            ((System.ComponentModel.ISupportInitialize)(this.numInternalLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEternalSeals)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBoots)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExperience)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLevel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.NumericUpDown numInternalLevel;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.NumericUpDown numEternalSeals;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.NumericUpDown numBoots;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numExperience;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numLevel;
    }
}
