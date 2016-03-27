namespace FEFTwiddler
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.numExperience = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numLevel = new System.Windows.Forms.NumericUpDown();
            this.cmbSkill5 = new System.Windows.Forms.ComboBox();
            this.pictSkill5 = new System.Windows.Forms.PictureBox();
            this.cmbSkill4 = new System.Windows.Forms.ComboBox();
            this.pictSkill4 = new System.Windows.Forms.PictureBox();
            this.cmbSkill3 = new System.Windows.Forms.ComboBox();
            this.pictSkill3 = new System.Windows.Forms.PictureBox();
            this.cmbSkill2 = new System.Windows.Forms.ComboBox();
            this.pictSkill2 = new System.Windows.Forms.PictureBox();
            this.cmbSkill1 = new System.Windows.Forms.ComboBox();
            this.pictSkill1 = new System.Windows.Forms.PictureBox();
            this.cmbClass = new System.Windows.Forms.ComboBox();
            this.lblName = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnGiveEternalSeals = new System.Windows.Forms.Button();
            this.btnAllSkillsNoNpc = new System.Windows.Forms.Button();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numExperience)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictSkill5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictSkill4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictSkill3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictSkill2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictSkill1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(517, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem,
            this.saveFileToolStripMenuItem});
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem1.Text = "File";
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.openFileToolStripMenuItem.Text = "Open File";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // saveFileToolStripMenuItem
            // 
            this.saveFileToolStripMenuItem.Name = "saveFileToolStripMenuItem";
            this.saveFileToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.saveFileToolStripMenuItem.Text = "Save File";
            this.saveFileToolStripMenuItem.Click += new System.EventHandler(this.saveFileToolStripMenuItem_Click);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Enabled = false;
            this.tabControl1.Location = new System.Drawing.Point(13, 28);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(491, 521);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.listBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(483, 495);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Chapter Viewer";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.numExperience);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.numLevel);
            this.panel1.Controls.Add(this.cmbSkill5);
            this.panel1.Controls.Add(this.pictSkill5);
            this.panel1.Controls.Add(this.cmbSkill4);
            this.panel1.Controls.Add(this.pictSkill4);
            this.panel1.Controls.Add(this.cmbSkill3);
            this.panel1.Controls.Add(this.pictSkill3);
            this.panel1.Controls.Add(this.cmbSkill2);
            this.panel1.Controls.Add(this.pictSkill2);
            this.panel1.Controls.Add(this.cmbSkill1);
            this.panel1.Controls.Add(this.pictSkill1);
            this.panel1.Controls.Add(this.cmbClass);
            this.panel1.Controls.Add(this.lblName);
            this.panel1.Location = new System.Drawing.Point(110, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(367, 485);
            this.panel1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(288, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "EXP";
            // 
            // numExperience
            // 
            this.numExperience.Enabled = false;
            this.numExperience.Location = new System.Drawing.Point(318, 32);
            this.numExperience.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numExperience.Name = "numExperience";
            this.numExperience.Size = new System.Drawing.Size(38, 20);
            this.numExperience.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(296, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "LV";
            // 
            // numLevel
            // 
            this.numLevel.Enabled = false;
            this.numLevel.Location = new System.Drawing.Point(318, 5);
            this.numLevel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLevel.Name = "numLevel";
            this.numLevel.Size = new System.Drawing.Size(38, 20);
            this.numLevel.TabIndex = 12;
            this.numLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cmbSkill5
            // 
            this.cmbSkill5.Enabled = false;
            this.cmbSkill5.FormattingEnabled = true;
            this.cmbSkill5.Location = new System.Drawing.Point(235, 249);
            this.cmbSkill5.Name = "cmbSkill5";
            this.cmbSkill5.Size = new System.Drawing.Size(121, 21);
            this.cmbSkill5.TabIndex = 11;
            // 
            // pictSkill5
            // 
            this.pictSkill5.Location = new System.Drawing.Point(205, 247);
            this.pictSkill5.Name = "pictSkill5";
            this.pictSkill5.Size = new System.Drawing.Size(24, 24);
            this.pictSkill5.TabIndex = 10;
            this.pictSkill5.TabStop = false;
            // 
            // cmbSkill4
            // 
            this.cmbSkill4.Enabled = false;
            this.cmbSkill4.FormattingEnabled = true;
            this.cmbSkill4.Location = new System.Drawing.Point(235, 221);
            this.cmbSkill4.Name = "cmbSkill4";
            this.cmbSkill4.Size = new System.Drawing.Size(121, 21);
            this.cmbSkill4.TabIndex = 9;
            // 
            // pictSkill4
            // 
            this.pictSkill4.Location = new System.Drawing.Point(205, 219);
            this.pictSkill4.Name = "pictSkill4";
            this.pictSkill4.Size = new System.Drawing.Size(24, 24);
            this.pictSkill4.TabIndex = 8;
            this.pictSkill4.TabStop = false;
            // 
            // cmbSkill3
            // 
            this.cmbSkill3.Enabled = false;
            this.cmbSkill3.FormattingEnabled = true;
            this.cmbSkill3.Location = new System.Drawing.Point(235, 194);
            this.cmbSkill3.Name = "cmbSkill3";
            this.cmbSkill3.Size = new System.Drawing.Size(121, 21);
            this.cmbSkill3.TabIndex = 7;
            // 
            // pictSkill3
            // 
            this.pictSkill3.Location = new System.Drawing.Point(205, 192);
            this.pictSkill3.Name = "pictSkill3";
            this.pictSkill3.Size = new System.Drawing.Size(24, 24);
            this.pictSkill3.TabIndex = 6;
            this.pictSkill3.TabStop = false;
            // 
            // cmbSkill2
            // 
            this.cmbSkill2.Enabled = false;
            this.cmbSkill2.FormattingEnabled = true;
            this.cmbSkill2.Location = new System.Drawing.Point(235, 167);
            this.cmbSkill2.Name = "cmbSkill2";
            this.cmbSkill2.Size = new System.Drawing.Size(121, 21);
            this.cmbSkill2.TabIndex = 5;
            // 
            // pictSkill2
            // 
            this.pictSkill2.Location = new System.Drawing.Point(205, 165);
            this.pictSkill2.Name = "pictSkill2";
            this.pictSkill2.Size = new System.Drawing.Size(24, 24);
            this.pictSkill2.TabIndex = 4;
            this.pictSkill2.TabStop = false;
            // 
            // cmbSkill1
            // 
            this.cmbSkill1.Enabled = false;
            this.cmbSkill1.FormattingEnabled = true;
            this.cmbSkill1.Location = new System.Drawing.Point(235, 140);
            this.cmbSkill1.Name = "cmbSkill1";
            this.cmbSkill1.Size = new System.Drawing.Size(121, 21);
            this.cmbSkill1.TabIndex = 3;
            this.cmbSkill1.SelectedIndexChanged += new System.EventHandler(this.cmbSkill1_SelectedIndexChanged);
            // 
            // pictSkill1
            // 
            this.pictSkill1.Location = new System.Drawing.Point(205, 138);
            this.pictSkill1.Name = "pictSkill1";
            this.pictSkill1.Size = new System.Drawing.Size(24, 24);
            this.pictSkill1.TabIndex = 2;
            this.pictSkill1.TabStop = false;
            // 
            // cmbClass
            // 
            this.cmbClass.Enabled = false;
            this.cmbClass.FormattingEnabled = true;
            this.cmbClass.Location = new System.Drawing.Point(149, 4);
            this.cmbClass.Name = "cmbClass";
            this.cmbClass.Size = new System.Drawing.Size(121, 21);
            this.cmbClass.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(4, 4);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(139, 20);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Character Name";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(6, 6);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(98, 485);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnGiveEternalSeals);
            this.tabPage2.Controls.Add(this.btnAllSkillsNoNpc);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(483, 495);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Chapter Megacheats";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnGiveEternalSeals
            // 
            this.btnGiveEternalSeals.Location = new System.Drawing.Point(7, 84);
            this.btnGiveEternalSeals.Name = "btnGiveEternalSeals";
            this.btnGiveEternalSeals.Size = new System.Drawing.Size(218, 71);
            this.btnGiveEternalSeals.TabIndex = 1;
            this.btnGiveEternalSeals.Text = "Set all characters\' eternal seal count to 16 (allows leveling to 99)";
            this.btnGiveEternalSeals.UseVisualStyleBackColor = true;
            this.btnGiveEternalSeals.Click += new System.EventHandler(this.btnGiveEternalSeals_Click);
            // 
            // btnAllSkillsNoNpc
            // 
            this.btnAllSkillsNoNpc.Location = new System.Drawing.Point(7, 7);
            this.btnAllSkillsNoNpc.Name = "btnAllSkillsNoNpc";
            this.btnAllSkillsNoNpc.Size = new System.Drawing.Size(218, 71);
            this.btnAllSkillsNoNpc.TabIndex = 0;
            this.btnAllSkillsNoNpc.Text = "Teach all characters all skills except NPC-only skills";
            this.btnAllSkillsNoNpc.UseVisualStyleBackColor = true;
            this.btnAllSkillsNoNpc.Click += new System.EventHandler(this.btnAllSkillsNoNpc_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 561);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "FEFTwiddler";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numExperience)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictSkill5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictSkill4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictSkill3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictSkill2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictSkill1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.ComboBox cmbClass;
        private System.Windows.Forms.PictureBox pictSkill1;
        private System.Windows.Forms.ComboBox cmbSkill1;
        private System.Windows.Forms.ComboBox cmbSkill5;
        private System.Windows.Forms.PictureBox pictSkill5;
        private System.Windows.Forms.ComboBox cmbSkill4;
        private System.Windows.Forms.PictureBox pictSkill4;
        private System.Windows.Forms.ComboBox cmbSkill3;
        private System.Windows.Forms.PictureBox pictSkill3;
        private System.Windows.Forms.ComboBox cmbSkill2;
        private System.Windows.Forms.PictureBox pictSkill2;
        private System.Windows.Forms.NumericUpDown numLevel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numExperience;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripMenuItem saveFileToolStripMenuItem;
        private System.Windows.Forms.Button btnAllSkillsNoNpc;
        private System.Windows.Forms.Button btnGiveEternalSeals;
    }
}

