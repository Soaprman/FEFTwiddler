namespace FEFTwiddler.GUI.ChapterData
{
    partial class GoldAndPoints
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
            this.btn99DragonVeinPoints = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.numDragonVeinPoints = new System.Windows.Forms.NumericUpDown();
            this.btnMaxGold = new System.Windows.Forms.Button();
            this.numGold = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numDragonVeinPoints)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGold)).BeginInit();
            this.SuspendLayout();
            // 
            // btn99DragonVeinPoints
            // 
            this.btn99DragonVeinPoints.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn99DragonVeinPoints.Location = new System.Drawing.Point(167, 31);
            this.btn99DragonVeinPoints.Name = "btn99DragonVeinPoints";
            this.btn99DragonVeinPoints.Size = new System.Drawing.Size(75, 23);
            this.btn99DragonVeinPoints.TabIndex = 73;
            this.btn99DragonVeinPoints.Text = "99";
            this.btn99DragonVeinPoints.UseVisualStyleBackColor = true;
            this.btn99DragonVeinPoints.Click += new System.EventHandler(this.btn99DragonVeinPoints_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label16.Location = new System.Drawing.Point(3, 36);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(101, 13);
            this.label16.TabIndex = 72;
            this.label16.Text = "Dragon Vein Points:";
            // 
            // numDragonVeinPoints
            // 
            this.numDragonVeinPoints.Location = new System.Drawing.Point(110, 34);
            this.numDragonVeinPoints.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numDragonVeinPoints.Name = "numDragonVeinPoints";
            this.numDragonVeinPoints.Size = new System.Drawing.Size(50, 20);
            this.numDragonVeinPoints.TabIndex = 71;
            this.numDragonVeinPoints.ValueChanged += new System.EventHandler(this.numDragonVeinPoints_ValueChanged);
            // 
            // btnMaxGold
            // 
            this.btnMaxGold.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMaxGold.Location = new System.Drawing.Point(167, 2);
            this.btnMaxGold.Name = "btnMaxGold";
            this.btnMaxGold.Size = new System.Drawing.Size(75, 23);
            this.btnMaxGold.TabIndex = 62;
            this.btnMaxGold.Text = "Maximize";
            this.btnMaxGold.UseVisualStyleBackColor = true;
            this.btnMaxGold.Click += new System.EventHandler(this.btnMaxGold_Click);
            // 
            // numGold
            // 
            this.numGold.Location = new System.Drawing.Point(40, 3);
            this.numGold.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numGold.Name = "numGold";
            this.numGold.Size = new System.Drawing.Size(120, 20);
            this.numGold.TabIndex = 61;
            this.numGold.ValueChanged += new System.EventHandler(this.numGold_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(2, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 60;
            this.label3.Text = "Gold:";
            // 
            // GoldAndPoints
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn99DragonVeinPoints);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.numDragonVeinPoints);
            this.Controls.Add(this.btnMaxGold);
            this.Controls.Add(this.numGold);
            this.Controls.Add(this.label3);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "GoldAndPoints";
            this.Size = new System.Drawing.Size(268, 146);
            ((System.ComponentModel.ISupportInitialize)(this.numDragonVeinPoints)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGold)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn99DragonVeinPoints;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.NumericUpDown numDragonVeinPoints;
        private System.Windows.Forms.Button btnMaxGold;
        private System.Windows.Forms.NumericUpDown numGold;
        private System.Windows.Forms.Label label3;
    }
}
