namespace FEFTwiddler.GUI.UnitViewer
{
    partial class BattleData
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
            this.numVictories = new System.Windows.Forms.NumericUpDown();
            this.label18 = new System.Windows.Forms.Label();
            this.numBattles = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numVictories)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBattles)).BeginInit();
            this.SuspendLayout();
            // 
            // numVictories
            // 
            this.numVictories.Location = new System.Drawing.Point(56, 29);
            this.numVictories.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numVictories.Name = "numVictories";
            this.numVictories.Size = new System.Drawing.Size(57, 20);
            this.numVictories.TabIndex = 68;
            this.numVictories.ValueChanged += new System.EventHandler(this.numVictories_ValueChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label18.Location = new System.Drawing.Point(4, 31);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(47, 13);
            this.label18.TabIndex = 67;
            this.label18.Text = "Victories";
            // 
            // numBattles
            // 
            this.numBattles.Location = new System.Drawing.Point(56, 3);
            this.numBattles.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numBattles.Name = "numBattles";
            this.numBattles.Size = new System.Drawing.Size(57, 20);
            this.numBattles.TabIndex = 66;
            this.numBattles.ValueChanged += new System.EventHandler(this.numBattles_ValueChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label17.Location = new System.Drawing.Point(4, 5);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(39, 13);
            this.label17.TabIndex = 65;
            this.label17.Text = "Battles";
            // 
            // BattleData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numVictories);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.numBattles);
            this.Controls.Add(this.label17);
            this.Name = "BattleData";
            this.Size = new System.Drawing.Size(118, 54);
            ((System.ComponentModel.ISupportInitialize)(this.numVictories)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBattles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numVictories;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.NumericUpDown numBattles;
        private System.Windows.Forms.Label label17;
    }
}
