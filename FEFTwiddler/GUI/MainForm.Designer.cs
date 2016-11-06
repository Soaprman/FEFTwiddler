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
            this.decompressorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.decompressFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compressFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabChapterData = new System.Windows.Forms.TabPage();
            this.btnCastleMap = new System.Windows.Forms.Button();
            this.btnChapterHistory = new System.Windows.Forms.Button();
            this.lblAvatarName = new System.Windows.Forms.Label();
            this.tabUnitViewer = new System.Windows.Forms.TabPage();
            this.lblUnitCount = new System.Windows.Forms.Label();
            this.btnImportUnit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lstDead = new System.Windows.Forms.ListBox();
            this.pnlUnitView = new System.Windows.Forms.Panel();
            this.lblUsesCustomData = new System.Windows.Forms.Label();
            this.btnTraits = new System.Windows.Forms.Button();
            this.btnExportUnit = new System.Windows.Forms.Button();
            this.btnSupport = new System.Windows.Forms.Button();
            this.btnDeleteUnit = new System.Windows.Forms.Button();
            this.btnOpenHexEditor = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lstLiving = new System.Windows.Forms.ListBox();
            this.tabMegacheats = new System.Windows.Forms.TabPage();
            this.tabNewGamePlus = new System.Windows.Forms.TabPage();
            this.tabGlobalData = new System.Windows.Forms.TabPage();
            this.tabConvoy = new System.Windows.Forms.TabPage();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.difficulty1 = new FEFTwiddler.GUI.ChapterData.Difficulty();
            this.goldAndPoints1 = new FEFTwiddler.GUI.ChapterData.GoldAndPoints();
            this.materials1 = new FEFTwiddler.GUI.ChapterData.Materials();
            this.unitBlockInfo1 = new FEFTwiddler.GUI.UnitViewer.UnitBlockInfo();
            this.flags1 = new FEFTwiddler.GUI.UnitViewer.Flags();
            this.battleData1 = new FEFTwiddler.GUI.UnitViewer.BattleData();
            this.classAndLevel1 = new FEFTwiddler.GUI.UnitViewer.ClassAndLevel();
            this.dragonVein1 = new FEFTwiddler.GUI.UnitViewer.DragonVein();
            this.accessories1 = new FEFTwiddler.GUI.UnitViewer.Accessories();
            this.stats1 = new FEFTwiddler.GUI.UnitViewer.Stats();
            this.weaponExperience1 = new FEFTwiddler.GUI.UnitViewer.WeaponExperience();
            this.hairColor1 = new FEFTwiddler.GUI.UnitViewer.HairColor();
            this.inventory1 = new FEFTwiddler.GUI.UnitViewer.Inventory();
            this.skills1 = new FEFTwiddler.GUI.UnitViewer.Skills();
            this.megacheatsMain1 = new FEFTwiddler.GUI.ChapterData.MegacheatsMain();
            this.globalDataMain1 = new FEFTwiddler.GUI.GlobalData.GlobalDataMain();
            this.convoyMain1 = new FEFTwiddler.GUI.Convoy.ConvoyMain();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabChapterData.SuspendLayout();
            this.tabUnitViewer.SuspendLayout();
            this.pnlUnitView.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabMegacheats.SuspendLayout();
            this.tabGlobalData.SuspendLayout();
            this.tabConvoy.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1,
            this.decompressorToolStripMenuItem});
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
            // decompressorToolStripMenuItem
            // 
            this.decompressorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.decompressFileToolStripMenuItem,
            this.compressFileToolStripMenuItem});
            this.decompressorToolStripMenuItem.Name = "decompressorToolStripMenuItem";
            resources.ApplyResources(this.decompressorToolStripMenuItem, "decompressorToolStripMenuItem");
            // 
            // decompressFileToolStripMenuItem
            // 
            this.decompressFileToolStripMenuItem.Name = "decompressFileToolStripMenuItem";
            resources.ApplyResources(this.decompressFileToolStripMenuItem, "decompressFileToolStripMenuItem");
            this.decompressFileToolStripMenuItem.Click += new System.EventHandler(this.decompressFileToolStripMenuItem_Click);
            // 
            // compressFileToolStripMenuItem
            // 
            this.compressFileToolStripMenuItem.Name = "compressFileToolStripMenuItem";
            resources.ApplyResources(this.compressFileToolStripMenuItem, "compressFileToolStripMenuItem");
            this.compressFileToolStripMenuItem.Click += new System.EventHandler(this.compressFileToolStripMenuItem_Click);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabChapterData);
            this.tabControl1.Controls.Add(this.tabUnitViewer);
            this.tabControl1.Controls.Add(this.tabMegacheats);
            this.tabControl1.Controls.Add(this.tabNewGamePlus);
            this.tabControl1.Controls.Add(this.tabGlobalData);
            this.tabControl1.Controls.Add(this.tabConvoy);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabChapterData
            // 
            this.tabChapterData.Controls.Add(this.btnCastleMap);
            this.tabChapterData.Controls.Add(this.difficulty1);
            this.tabChapterData.Controls.Add(this.btnChapterHistory);
            this.tabChapterData.Controls.Add(this.goldAndPoints1);
            this.tabChapterData.Controls.Add(this.materials1);
            this.tabChapterData.Controls.Add(this.lblAvatarName);
            resources.ApplyResources(this.tabChapterData, "tabChapterData");
            this.tabChapterData.Name = "tabChapterData";
            this.tabChapterData.UseVisualStyleBackColor = true;
            // 
            // btnCastleMap
            // 
            resources.ApplyResources(this.btnCastleMap, "btnCastleMap");
            this.btnCastleMap.Name = "btnCastleMap";
            this.btnCastleMap.UseVisualStyleBackColor = true;
            this.btnCastleMap.Click += new System.EventHandler(this.btnCastleMap_Click);
            // 
            // btnChapterHistory
            // 
            resources.ApplyResources(this.btnChapterHistory, "btnChapterHistory");
            this.btnChapterHistory.Name = "btnChapterHistory";
            this.btnChapterHistory.UseVisualStyleBackColor = true;
            this.btnChapterHistory.Click += new System.EventHandler(this.btnChapterHistory_Click);
            // 
            // lblAvatarName
            // 
            resources.ApplyResources(this.lblAvatarName, "lblAvatarName");
            this.lblAvatarName.Name = "lblAvatarName";
            // 
            // tabUnitViewer
            // 
            this.tabUnitViewer.Controls.Add(this.lblUnitCount);
            this.tabUnitViewer.Controls.Add(this.btnImportUnit);
            this.tabUnitViewer.Controls.Add(this.label1);
            this.tabUnitViewer.Controls.Add(this.lstDead);
            this.tabUnitViewer.Controls.Add(this.pnlUnitView);
            this.tabUnitViewer.Controls.Add(this.lstLiving);
            resources.ApplyResources(this.tabUnitViewer, "tabUnitViewer");
            this.tabUnitViewer.Name = "tabUnitViewer";
            this.tabUnitViewer.UseVisualStyleBackColor = true;
            this.tabUnitViewer.Enter += new System.EventHandler(this.tabPage1_Enter);
            // 
            // lblUnitCount
            // 
            resources.ApplyResources(this.lblUnitCount, "lblUnitCount");
            this.lblUnitCount.Name = "lblUnitCount";
            // 
            // btnImportUnit
            // 
            resources.ApplyResources(this.btnImportUnit, "btnImportUnit");
            this.btnImportUnit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnImportUnit.Name = "btnImportUnit";
            this.btnImportUnit.UseVisualStyleBackColor = true;
            this.btnImportUnit.Click += new System.EventHandler(this.btnImportUnit_Click);
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
            this.lstDead.SelectedIndexChanged += new System.EventHandler(this.SelectDeadUnit);
            // 
            // pnlUnitView
            // 
            this.pnlUnitView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlUnitView.Controls.Add(this.lblUsesCustomData);
            this.pnlUnitView.Controls.Add(this.btnTraits);
            this.pnlUnitView.Controls.Add(this.btnExportUnit);
            this.pnlUnitView.Controls.Add(this.btnSupport);
            this.pnlUnitView.Controls.Add(this.btnDeleteUnit);
            this.pnlUnitView.Controls.Add(this.unitBlockInfo1);
            this.pnlUnitView.Controls.Add(this.flags1);
            this.pnlUnitView.Controls.Add(this.battleData1);
            this.pnlUnitView.Controls.Add(this.classAndLevel1);
            this.pnlUnitView.Controls.Add(this.dragonVein1);
            this.pnlUnitView.Controls.Add(this.btnOpenHexEditor);
            this.pnlUnitView.Controls.Add(this.accessories1);
            this.pnlUnitView.Controls.Add(this.stats1);
            this.pnlUnitView.Controls.Add(this.weaponExperience1);
            this.pnlUnitView.Controls.Add(this.hairColor1);
            this.pnlUnitView.Controls.Add(this.inventory1);
            this.pnlUnitView.Controls.Add(this.groupBox1);
            this.pnlUnitView.Controls.Add(this.lblName);
            resources.ApplyResources(this.pnlUnitView, "pnlUnitView");
            this.pnlUnitView.Name = "pnlUnitView";
            // 
            // lblUsesCustomData
            // 
            resources.ApplyResources(this.lblUsesCustomData, "lblUsesCustomData");
            this.lblUsesCustomData.ForeColor = System.Drawing.Color.Magenta;
            this.lblUsesCustomData.Name = "lblUsesCustomData";
            // 
            // btnTraits
            // 
            resources.ApplyResources(this.btnTraits, "btnTraits");
            this.btnTraits.Name = "btnTraits";
            this.btnTraits.UseVisualStyleBackColor = true;
            this.btnTraits.Click += new System.EventHandler(this.btnTraits_Click);
            // 
            // btnExportUnit
            // 
            resources.ApplyResources(this.btnExportUnit, "btnExportUnit");
            this.btnExportUnit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnExportUnit.Name = "btnExportUnit";
            this.btnExportUnit.UseVisualStyleBackColor = true;
            this.btnExportUnit.Click += new System.EventHandler(this.btnExportUnit_Click);
            // 
            // btnSupport
            // 
            resources.ApplyResources(this.btnSupport, "btnSupport");
            this.btnSupport.Name = "btnSupport";
            this.btnSupport.UseVisualStyleBackColor = true;
            this.btnSupport.Click += new System.EventHandler(this.btnSupport_Click);
            // 
            // btnDeleteUnit
            // 
            resources.ApplyResources(this.btnDeleteUnit, "btnDeleteUnit");
            this.btnDeleteUnit.ForeColor = System.Drawing.Color.Red;
            this.btnDeleteUnit.Name = "btnDeleteUnit";
            this.btnDeleteUnit.UseVisualStyleBackColor = true;
            this.btnDeleteUnit.Click += new System.EventHandler(this.btnDeleteUnit_Click);
            // 
            // btnOpenHexEditor
            // 
            resources.ApplyResources(this.btnOpenHexEditor, "btnOpenHexEditor");
            this.btnOpenHexEditor.Name = "btnOpenHexEditor";
            this.btnOpenHexEditor.UseVisualStyleBackColor = true;
            this.btnOpenHexEditor.Click += new System.EventHandler(this.btnOpenHexEditor_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.skills1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
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
            this.lstLiving.SelectedIndexChanged += new System.EventHandler(this.SelectLivingUnit);
            // 
            // tabMegacheats
            // 
            this.tabMegacheats.Controls.Add(this.megacheatsMain1);
            resources.ApplyResources(this.tabMegacheats, "tabMegacheats");
            this.tabMegacheats.Name = "tabMegacheats";
            this.tabMegacheats.UseVisualStyleBackColor = true;
            // 
            // tabNewGamePlus
            // 
            resources.ApplyResources(this.tabNewGamePlus, "tabNewGamePlus");
            this.tabNewGamePlus.Name = "tabNewGamePlus";
            this.tabNewGamePlus.UseVisualStyleBackColor = true;
            // 
            // tabGlobalData
            // 
            this.tabGlobalData.Controls.Add(this.globalDataMain1);
            resources.ApplyResources(this.tabGlobalData, "tabGlobalData");
            this.tabGlobalData.Name = "tabGlobalData";
            this.tabGlobalData.UseVisualStyleBackColor = true;
            // 
            // tabConvoy
            // 
            this.tabConvoy.Controls.Add(this.convoyMain1);
            resources.ApplyResources(this.tabConvoy, "tabConvoy");
            this.tabConvoy.Name = "tabConvoy";
            this.tabConvoy.UseVisualStyleBackColor = true;
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            resources.ApplyResources(this.saveToolStripMenuItem, "saveToolStripMenuItem");
            // 
            // difficulty1
            // 
            resources.ApplyResources(this.difficulty1, "difficulty1");
            this.difficulty1.Name = "difficulty1";
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
            // unitBlockInfo1
            // 
            resources.ApplyResources(this.unitBlockInfo1, "unitBlockInfo1");
            this.unitBlockInfo1.Name = "unitBlockInfo1";
            // 
            // flags1
            // 
            resources.ApplyResources(this.flags1, "flags1");
            this.flags1.Name = "flags1";
            // 
            // battleData1
            // 
            resources.ApplyResources(this.battleData1, "battleData1");
            this.battleData1.Name = "battleData1";
            // 
            // classAndLevel1
            // 
            resources.ApplyResources(this.classAndLevel1, "classAndLevel1");
            this.classAndLevel1.Name = "classAndLevel1";
            // 
            // dragonVein1
            // 
            resources.ApplyResources(this.dragonVein1, "dragonVein1");
            this.dragonVein1.Name = "dragonVein1";
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
            // skills1
            // 
            resources.ApplyResources(this.skills1, "skills1");
            this.skills1.Name = "skills1";
            // 
            // megacheatsMain1
            // 
            resources.ApplyResources(this.megacheatsMain1, "megacheatsMain1");
            this.megacheatsMain1.Name = "megacheatsMain1";
            // 
            // globalDataMain1
            // 
            resources.ApplyResources(this.globalDataMain1, "globalDataMain1");
            this.globalDataMain1.Name = "globalDataMain1";
            // 
            // convoyMain1
            // 
            resources.ApplyResources(this.convoyMain1, "convoyMain1");
            this.convoyMain1.Name = "convoyMain1";
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
            this.tabChapterData.ResumeLayout(false);
            this.tabChapterData.PerformLayout();
            this.tabUnitViewer.ResumeLayout(false);
            this.tabUnitViewer.PerformLayout();
            this.pnlUnitView.ResumeLayout(false);
            this.pnlUnitView.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tabMegacheats.ResumeLayout(false);
            this.tabGlobalData.ResumeLayout(false);
            this.tabConvoy.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabUnitViewer;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ListBox lstLiving;
        private System.Windows.Forms.Panel pnlUnitView;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.TabPage tabMegacheats;
        private System.Windows.Forms.ToolStripMenuItem saveFileToolStripMenuItem;
        private System.Windows.Forms.TabPage tabChapterData;
        private System.Windows.Forms.Label lblAvatarName;
        private System.Windows.Forms.GroupBox groupBox1;
        private UnitViewer.Inventory inventory1;
        private UnitViewer.HairColor hairColor1;
        private UnitViewer.WeaponExperience weaponExperience1;
        private ChapterData.Materials materials1;
        private UnitViewer.Stats stats1;
        private UnitViewer.Accessories accessories1;
        private ChapterData.MegacheatsMain megacheatsMain1;
        private ChapterData.GoldAndPoints goldAndPoints1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstDead;
        private System.Windows.Forms.Button btnOpenHexEditor;
        private UnitViewer.Skills skills1;
        private System.Windows.Forms.Button btnChapterHistory;
        private System.Windows.Forms.TabPage tabNewGamePlus;
        private UnitViewer.DragonVein dragonVein1;
        private ChapterData.Difficulty difficulty1;
        private System.Windows.Forms.ToolStripMenuItem decompressorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem decompressFileToolStripMenuItem;
        private System.Windows.Forms.TabPage tabGlobalData;
        private GlobalData.GlobalDataMain globalDataMain1;
        private System.Windows.Forms.ToolStripMenuItem compressFileToolStripMenuItem;
        private UnitViewer.ClassAndLevel classAndLevel1;
        private UnitViewer.BattleData battleData1;
        private UnitViewer.Flags flags1;
        private UnitViewer.UnitBlockInfo unitBlockInfo1;
        private System.Windows.Forms.TabPage tabConvoy;
        private Convoy.ConvoyMain convoyMain1;
        private System.Windows.Forms.Button btnDeleteUnit;
        private System.Windows.Forms.Button btnExportUnit;
        private System.Windows.Forms.Button btnImportUnit;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label lblUnitCount;
        private System.Windows.Forms.Button btnSupport;
        private System.Windows.Forms.Button btnTraits;
        private System.Windows.Forms.Label lblUsesCustomData;
        private System.Windows.Forms.Button btnCastleMap;
    }
}

