namespace FEFTwiddler.GUI
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnChapterHistory = new System.Windows.Forms.Button();
            this.goldAndPoints1 = new FEFTwiddler.GUI.ChapterData.GoldAndPoints();
            this.materials1 = new FEFTwiddler.GUI.ChapterData.Materials();
            this.lblAvatarName = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.lstDead = new System.Windows.Forms.ListBox();
            this.pnlUnitView = new System.Windows.Forms.Panel();
            this.btnOpenHexEditor = new System.Windows.Forms.Button();
            this.battleData1 = new FEFTwiddler.GUI.UnitViewer.BattleData();
            this.class1 = new FEFTwiddler.GUI.UnitViewer.Class();
            this.flags1 = new FEFTwiddler.GUI.UnitViewer.Flags();
            this.accessories1 = new FEFTwiddler.GUI.UnitViewer.Accessories();
            this.stats1 = new FEFTwiddler.GUI.UnitViewer.Stats();
            this.levelAndExperience1 = new FEFTwiddler.GUI.UnitViewer.LevelAndExperience();
            this.weaponExperience1 = new FEFTwiddler.GUI.UnitViewer.WeaponExperience();
            this.hairColor1 = new FEFTwiddler.GUI.UnitViewer.HairColor();
            this.inventory1 = new FEFTwiddler.GUI.UnitViewer.Inventory();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.skills1 = new FEFTwiddler.GUI.UnitViewer.Skills();
            this.lblName = new System.Windows.Forms.Label();
            this.lstLiving = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.megacheatsMain1 = new FEFTwiddler.GUI.ChapterData.MegacheatsMain();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.pnlUnitView.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem,
            this.saveFileToolStripMenuItem});
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            resources.ApplyResources(this.fileToolStripMenuItem1, "fileToolStripMenuItem1");
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            resources.ApplyResources(this.openFileToolStripMenuItem, "openFileToolStripMenuItem");
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // saveFileToolStripMenuItem
            // 
            this.saveFileToolStripMenuItem.Name = "saveFileToolStripMenuItem";
            resources.ApplyResources(this.saveFileToolStripMenuItem, "saveFileToolStripMenuItem");
            this.saveFileToolStripMenuItem.Click += new System.EventHandler(this.saveFileToolStripMenuItem_Click);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnChapterHistory);
            this.tabPage3.Controls.Add(this.goldAndPoints1);
            this.tabPage3.Controls.Add(this.materials1);
            this.tabPage3.Controls.Add(this.lblAvatarName);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnChapterHistory
            // 
            resources.ApplyResources(this.btnChapterHistory, "btnChapterHistory");
            this.btnChapterHistory.Name = "btnChapterHistory";
            this.btnChapterHistory.UseVisualStyleBackColor = true;
            this.btnChapterHistory.Click += new System.EventHandler(this.btnChapterHistory_Click);
            // 
            // goldAndPoints1
            // 
            resources.ApplyResources(this.goldAndPoints1, "goldAndPoints1");
            this.goldAndPoints1.Name = "goldAndPoints1";
            // 
            // materials1
            // 
            resources.ApplyResources(this.materials1, "materials1");
            this.materials1.Name = "materials1";
            // 
            // lblAvatarName
            // 
            resources.ApplyResources(this.lblAvatarName, "lblAvatarName");
            this.lblAvatarName.Name = "lblAvatarName";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.lstDead);
            this.tabPage1.Controls.Add(this.pnlUnitView);
            this.tabPage1.Controls.Add(this.lstLiving);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Enter += new System.EventHandler(this.tabPage1_Enter);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // lstDead
            // 
            this.lstDead.FormattingEnabled = true;
            resources.ApplyResources(this.lstDead, "lstDead");
            this.lstDead.Name = "lstDead";
            this.lstDead.SelectedIndexChanged += new System.EventHandler(this.SelectDeadCharacter);
            // 
            // pnlUnitView
            // 
            this.pnlUnitView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlUnitView.Controls.Add(this.btnOpenHexEditor);
            this.pnlUnitView.Controls.Add(this.battleData1);
            this.pnlUnitView.Controls.Add(this.class1);
            this.pnlUnitView.Controls.Add(this.flags1);
            this.pnlUnitView.Controls.Add(this.accessories1);
            this.pnlUnitView.Controls.Add(this.stats1);
            this.pnlUnitView.Controls.Add(this.levelAndExperience1);
            this.pnlUnitView.Controls.Add(this.weaponExperience1);
            this.pnlUnitView.Controls.Add(this.hairColor1);
            this.pnlUnitView.Controls.Add(this.inventory1);
            this.pnlUnitView.Controls.Add(this.groupBox1);
            this.pnlUnitView.Controls.Add(this.lblName);
            resources.ApplyResources(this.pnlUnitView, "pnlUnitView");
            this.pnlUnitView.Name = "pnlUnitView";
            // 
            // btnOpenHexEditor
            // 
            resources.ApplyResources(this.btnOpenHexEditor, "btnOpenHexEditor");
            this.btnOpenHexEditor.Name = "btnOpenHexEditor";
            this.btnOpenHexEditor.UseVisualStyleBackColor = true;
            this.btnOpenHexEditor.Click += new System.EventHandler(this.btnOpenHexEditor_Click);
            // 
            // battleData1
            // 
            resources.ApplyResources(this.battleData1, "battleData1");
            this.battleData1.Name = "battleData1";
            // 
            // class1
            // 
            resources.ApplyResources(this.class1, "class1");
            this.class1.Name = "class1";
            // 
            // flags1
            // 
            resources.ApplyResources(this.flags1, "flags1");
            this.flags1.Name = "flags1";
            // 
            // accessories1
            // 
            resources.ApplyResources(this.accessories1, "accessories1");
            this.accessories1.Name = "accessories1";
            // 
            // stats1
            // 
            resources.ApplyResources(this.stats1, "stats1");
            this.stats1.Name = "stats1";
            // 
            // levelAndExperience1
            // 
            resources.ApplyResources(this.levelAndExperience1, "levelAndExperience1");
            this.levelAndExperience1.Name = "levelAndExperience1";
            // 
            // weaponExperience1
            // 
            resources.ApplyResources(this.weaponExperience1, "weaponExperience1");
            this.weaponExperience1.Name = "weaponExperience1";
            // 
            // hairColor1
            // 
            resources.ApplyResources(this.hairColor1, "hairColor1");
            this.hairColor1.Name = "hairColor1";
            // 
            // inventory1
            // 
            resources.ApplyResources(this.inventory1, "inventory1");
            this.inventory1.Name = "inventory1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.skills1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // skills1
            // 
            resources.ApplyResources(this.skills1, "skills1");
            this.skills1.Name = "skills1";
            // 
            // lblName
            // 
            resources.ApplyResources(this.lblName, "lblName");
            this.lblName.Name = "lblName";
            // 
            // lstLiving
            // 
            this.lstLiving.FormattingEnabled = true;
            resources.ApplyResources(this.lstLiving, "lstLiving");
            this.lstLiving.Name = "lstLiving";
            this.lstLiving.SelectedIndexChanged += new System.EventHandler(this.SelectLivingCharacter);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.megacheatsMain1);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // megacheatsMain1
            // 
            resources.ApplyResources(this.megacheatsMain1, "megacheatsMain1");
            this.megacheatsMain1.Name = "megacheatsMain1";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            resources.ApplyResources(this.saveToolStripMenuItem, "saveToolStripMenuItem");
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.pnlUnitView.ResumeLayout(false);
            this.pnlUnitView.PerformLayout();
            this.groupBox1.ResumeLayout(false);
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
        private System.Windows.Forms.ListBox lstLiving;
        private System.Windows.Forms.Panel pnlUnitView;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripMenuItem saveFileToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label lblAvatarName;
        private System.Windows.Forms.GroupBox groupBox1;
        private UnitViewer.Inventory inventory1;
        private UnitViewer.HairColor hairColor1;
        private UnitViewer.WeaponExperience weaponExperience1;
        private ChapterData.Materials materials1;
        private UnitViewer.LevelAndExperience levelAndExperience1;
        private UnitViewer.Stats stats1;
        private UnitViewer.Accessories accessories1;
        private ChapterData.MegacheatsMain megacheatsMain1;
        private UnitViewer.Flags flags1;
        private UnitViewer.Class class1;
        private UnitViewer.BattleData battleData1;
        private ChapterData.GoldAndPoints goldAndPoints1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstDead;
        private System.Windows.Forms.Button btnOpenHexEditor;
        private UnitViewer.Skills skills1;
        private System.Windows.Forms.Button btnChapterHistory;
    }
}

