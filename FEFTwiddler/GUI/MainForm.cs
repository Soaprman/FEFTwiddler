using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using FEFTwiddler.Extensions;

namespace FEFTwiddler.GUI
{
    public partial class MainForm : Form
    {
        private Model.SaveFile _saveFile;
        private Model.ChapterSave _chapterSave;
        private Model.GlobalSave _globalSave;
        private Model.Unit _selectedUnit;
        private Controls.Blanket _unitViewerBlanket;

        public MainForm()
        {
            InitializeComponent();
            InitializeDatabases();

            this.AllowDrop = true;
            this.DragEnter += MainForm_DragEnter;
            this.DragDrop += MainForm_DragDrop;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _unitViewerBlanket = GUI.Controls.Blanket.AttachTo(pnlUnitView);
        }

        private void InitializeDatabases()
        {
            // TODO: Let user specify language
            // Will need to call SetLanguage on all databases when switching and refresh GUI for display names
            Data.Database.SetLanguage(Enums.Language.English);
        }

        public static MainForm GetFromHere(Control ctl)
        {
            var parent = ctl.Parent;
            while (parent.GetType() != typeof(MainForm))
            {
                parent = parent.Parent;
            }
            return (MainForm)parent;
        }

        #region Load

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (paths.Length > 0)
            {
                LoadFile(paths[0]);
            }
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "'Chapter' save|*";

            var startupPath = Config.StartupPath;
            if (startupPath == "" || !Directory.Exists(startupPath)) startupPath = Application.StartupPath;
            openFileDialog1.InitialDirectory = startupPath;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Config.StartupPath = Path.GetDirectoryName(openFileDialog1.FileName);

                LoadFile(openFileDialog1.FileName);
            }
        }

        private void LoadFile(string path)
        {
            var saveFile = Model.SaveFile.FromPath(path);

            if (saveFile.Type != Enums.SaveFileType.Chapter && saveFile.Type != Enums.SaveFileType.Global)
            {
                MessageBox.Show("This type of save is not supported yet. Only 'Chapter' and 'Global' saves are supported right now.");
                return;
            }
            else
            {
                _saveFile = saveFile;
            }

            UpdateTitleBar(path);

            Cursor.Current = Cursors.WaitCursor;

            switch (_saveFile.Type)
            {
                case Enums.SaveFileType.Chapter:
                    _chapterSave = Model.ChapterSave.FromSaveFile(_saveFile);
                    _globalSave = null;

                    LoadChapterData();
                    LoadUnitViewer();

                    tabChapterData.Enabled = true;
                    tabUnitViewer.Enabled = true;
                    tabMegacheats.Enabled = true;
                    tabConvoy.Enabled = true;
                    tabGlobalData.Enabled = false;

                    break;
                case Enums.SaveFileType.Global:
                    _globalSave = Model.GlobalSave.FromSaveFile(_saveFile);
                    _chapterSave = null;

                    LoadGlobalData();

                    tabChapterData.Enabled = false;
                    tabUnitViewer.Enabled = false;
                    tabMegacheats.Enabled = false;
                    tabConvoy.Enabled = false;
                    tabGlobalData.Enabled = true;

                    break;
                default: break;
            }

            Cursor.Current = Cursors.AppStarting;

            tabControl1.Enabled = true;
        }

        private void UpdateTitleBar(string path)
        {
            var directory = Path.GetDirectoryName(path);
            if (directory.Length > 80) directory = "..." + directory.Right(80); // Arbitary choice that I find tends to fit
            var truncatedPath = directory + "\\" + Path.GetFileName(path);
            this.Text = "FEFTwiddler - " + truncatedPath;
        }

        #endregion

        #region Save

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_saveFile == null)
            {
                MessageBox.Show("No file is loaded");
                return;
            }

            switch (_saveFile.Type)
            {
                case Enums.SaveFileType.Chapter:
                    if (_chapterSave == null)
                    {
                        MessageBox.Show("No file is loaded");
                        return;
                    }

                    _chapterSave.Write();

                    break;
                case Enums.SaveFileType.Global:
                    if (_globalSave == null)
                    {
                        MessageBox.Show("No file is loaded");
                        return;
                    }

                    _globalSave.Write();

                    break;
                default:
                    MessageBox.Show("No file is loaded");
                    return;
            }

            MessageBox.Show("File saved. A backup was made in the source directory as well.");
        }

        #endregion

        #region Chapter Data

        private void LoadChapterData()
        {
            lblAvatarName.Text = _chapterSave.Header.AvatarName;            

            goldAndPoints1.LoadChapterSave(_chapterSave);
            materials1.LoadChapterSave(_chapterSave);
            megacheatsMain1.LoadChapterSave(_chapterSave);
            difficulty1.LoadChapterSave(_chapterSave);
            convoyMain1.LoadChapterSave(_chapterSave);
        }

        private void btnChapterHistory_Click(object sender, EventArgs e)
        {
            var popup = new GUI.ChapterData.ChapterHistory(_chapterSave);
            popup.ShowDialog();
        }

        private void btnCastleMap_Click(object sender, EventArgs e)
        {
            var popup = new GUI.ChapterData.CastleViewer(_chapterSave.MyCastleRegion);
            popup.ShowDialog();
        }

        #endregion

        #region Unit Viewer

        /// <summary>
        /// Load the unit viewer, then jump to a particular unit
        /// </summary>
        public void LoadUnitViewer(Model.Unit unit)
        {
            LoadUnitViewer();
            SelectUnit(unit);
        }

        public void LoadUnitViewer()
        {
            lstLiving.Invalidate();
            lstDead.Invalidate();

            lstLiving.DisplayMember = "DisplayName";
            lstLiving.ValueMember = "CharacterID";
            lstLiving.Items.Clear();

            lstDead.DisplayMember = "DisplayName";
            lstDead.ValueMember = "CharacterID";
            lstDead.Items.Clear();
            foreach (var unit in _chapterSave.UnitRegion.Units)
            {
                if (IsDead(unit)) lstDead.Items.Add(unit);
                else lstLiving.Items.Add(unit);
            }

            lstLiving.SelectedIndex = 0;
            lstDead.ClearSelected();

            lstLiving.Refresh();
            lstDead.Refresh();

            UpdateUnitCount();
        }

        private void UpdateUnitCount()
        {
            lblUnitCount.Text = string.Format("Units: {0}/{1}", _chapterSave.UnitRegion.Units.Count, Model.ChapterSaveRegions.UnitRegion.HardMaxUnits);
        }

        private bool IsDead(Model.Unit unit)
        {
            return unit.UnitBlock == Enums.UnitBlock.DeadByGameplay || unit.UnitBlock == Enums.UnitBlock.DeadByPlot;
        }

        public void SelectUnit(Model.Unit unit)
        {
            if (lstLiving.Items.IndexOf(unit) > -1)
            {
                lstLiving.SelectedItem = unit;
            }
            else if (lstDead.Items.IndexOf(unit) > -1)
            {
                lstDead.SelectedItem = unit;
            }
        }

        private void SelectLivingUnit(object sender, EventArgs e)
        {
            var unit = (Model.Unit)lstLiving.SelectedItem;
            if (unit == null) return;
            _selectedUnit = unit;
            LoadUnit(unit);
            lstDead.ClearSelected();
        }

        private void SelectDeadUnit(object sender, EventArgs e)
        {
            var unit = (Model.Unit)lstDead.SelectedItem;
            if (unit == null) return;
            _selectedUnit = unit;
            LoadUnit(unit);
            lstLiving.ClearSelected();
        }

        public void DeselectUnit()
        {
            lstLiving.SelectedItem = null;
            lstDead.SelectedItem = null;
            _selectedUnit = null;

            var message = "No unit is selected.";
            _unitViewerBlanket.SetMessage(message);
            _unitViewerBlanket.Cover();
        }

        private void LoadUnit(Model.Unit unit)
        {
            if (unit == null) return;

            var message = "";

            lblName.Text = unit.GetDisplayName();

            if (!Enum.IsDefined(typeof(Enums.Character), unit.CharacterID) ||
                !Enum.IsDefined(typeof(Enums.Class), unit.ClassID))
            {
                lblUsesCustomData.Show();
            }
            else
            {
                lblUsesCustomData.Hide();
            }

            try { classAndLevel1.LoadUnit(_selectedUnit); }
            catch (Exception) { message += Environment.NewLine + "Error loading Class and Level data"; }

            try { stats1.LoadUnit(_selectedUnit); }
            catch (Exception) { message += Environment.NewLine + "Error loading Stats data"; }

            try { unitBlockInfo1.LoadUnit(_chapterSave, _selectedUnit); }
            catch (Exception) { message += Environment.NewLine + "Error loading Unit Block Info data"; }

            try { flags1.LoadUnit(_selectedUnit); }
            catch (Exception) { message += Environment.NewLine + "Error loading Flags data"; }

            try { battleData1.LoadUnit(_selectedUnit); }
            catch (Exception) { message += Environment.NewLine + "Error loading Battle data"; }

            try { skills1.LoadUnit(_selectedUnit); }
            catch (Exception) { message += Environment.NewLine + "Error loading Skills data"; }

            try { inventory1.LoadUnit(_selectedUnit); }
            catch (Exception) { message += Environment.NewLine + "Error loading Inventory data"; }

            try { accessories1.LoadUnit(_selectedUnit); }
            catch (Exception) { message += Environment.NewLine + "Error loading Accessories data"; }

            try { hairColor1.LoadUnit(_selectedUnit); }
            catch (Exception) { message += Environment.NewLine + "Error loading Hair Color data"; }

            try { weaponExperience1.LoadUnit(_selectedUnit); }
            catch (Exception) { message += Environment.NewLine + "Error loading Weapon Experience data"; }

            try { dragonVein1.LoadUnit(_selectedUnit); }
            catch (Exception) { message += Environment.NewLine + "Error loading Dragon Vein data"; }

            if (message.Length > 0)
            {
                message = "One or more values is invalid for this unit. You can still use the hex editor, though." + Environment.NewLine + Environment.NewLine + message;
                _unitViewerBlanket.SetMessage(message);
                _unitViewerBlanket.Cover();

                btnOpenHexEditor.BringToFront();
            }
            else
            {
                _unitViewerBlanket.Uncover();
            }

            // Support
            var supportData = Data.Database.Characters.GetByID(_selectedUnit.CharacterID)?.SupportPool;
            if ((supportData == null) ||
                (_selectedUnit.RawNumberOfSupports != supportData.Length) ||
                (supportData.Length == 0))
            {
                btnSupport.Enabled = false;
            }
            else
                btnSupport.Enabled = true;
        }

        private void tabPage1_Enter(object sender, EventArgs e)
        {
            LoadUnit(_selectedUnit);
        }

        private void btnImportUnit_Click(object sender, EventArgs e)
        {
            if (_chapterSave.UnitRegion.Units.Count >= Model.ChapterSaveRegions.UnitRegion.HardMaxUnits)
            {
                MessageBox.Show("You already have the maximum of " + Model.ChapterSaveRegions.UnitRegion.HardMaxUnits.ToString() + " units. Please remove one before adding another.");
                return;
            }

            if (_chapterSave.UnitRegion.Units.Count == Model.ChapterSaveRegions.UnitRegion.SoftMaxUnits)
            {
                MessageBox.Show("The game normally does not allow you to recruit more than " + Model.ChapterSaveRegions.UnitRegion.SoftMaxUnits.ToString() + " units. It's possible to add more, but do so at your own risk.");
            }

            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Fire Emblem Fates Unit|*.fe14unit";

            var unitPath = Config.UnitPath;
            if (unitPath == "" || !Directory.Exists(unitPath)) unitPath = Config.StartupPath;
            if (unitPath == "" || !Directory.Exists(unitPath)) unitPath = Application.StartupPath;
            openFileDialog1.InitialDirectory = unitPath;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Config.UnitPath = Path.GetDirectoryName(openFileDialog1.FileName);

                var unit = Model.Unit.FromPath(openFileDialog1.FileName);
                // TODO: Handle units from other blocks?
                unit.UnitBlock = Enums.UnitBlock.Living;
                Utils.UnitUtil.RemoveNamesFromHeldWeaponsWithInvalidNames(_chapterSave, unit);
                Utils.UnitUtil.FixBlock(unit);
                Utils.UnitUtil.Undeploy(unit);
                _chapterSave.UnitRegion.Units.Add(unit);

                LoadUnitViewer();
                SelectUnit(unit);
            }
        }

        private void btnExportUnit_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "";
            saveFileDialog1.Filter = "Fire Emblem Fates Unit|*.fe14unit";

            var unitPath = Config.UnitPath;
            if (unitPath == "" || !Directory.Exists(unitPath)) unitPath = Config.StartupPath;
            if (unitPath == "" || !Directory.Exists(unitPath)) unitPath = Application.StartupPath;
            saveFileDialog1.InitialDirectory = unitPath;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Config.UnitPath = Path.GetDirectoryName(saveFileDialog1.FileName);

                _selectedUnit.ToPath(saveFileDialog1.FileName);
            }
        }

        private void btnDeleteUnit_Click(object sender, EventArgs e)
        {
            DialogResult result;

            result = MessageBox.Show("Are you sure you want to remove this unit?",
                "Remove unit?",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
            if (result != DialogResult.Yes) return;

            if (lstLiving.Items.Count == 1)
            {
                result = MessageBox.Show("This is your last living unit. No good can possibly come from removing this unit. Proceed anyway?",
                    "Remove your last living unit?",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button2);
                if (result != DialogResult.Yes) return;
            }
            else if (Enum.IsDefined(typeof(Enums.Character), _selectedUnit.CharacterID) &&
                Data.Database.Characters.GetByID(_selectedUnit.CharacterID).IsCorrin &&
                !_selectedUnit.IsEinherjar) {

                result = MessageBox.Show("You are about to remove a non-Einherjar Corrin. There's no telling what effect this may have on your game. Proceed?",
                    "Remove Corrin?",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button2);
                if (result != DialogResult.Yes) return;
            }

            _chapterSave.UnitRegion.Units.Remove(_selectedUnit);

            if (lstLiving.Items.IndexOf(_selectedUnit) > -1)
            {
                lstLiving.Items.Remove(_selectedUnit);
                DeselectUnit();
            }
            else if (lstDead.Items.IndexOf(_selectedUnit) > -1)
            {
                lstDead.Items.Remove(_selectedUnit);
                DeselectUnit();
            }

            UpdateUnitCount();
        }

        private void btnOpenHexEditor_Click(object sender, EventArgs e)
        {
            var hex = new GUI.UnitViewer.HexEditor(_selectedUnit);
            hex.ShowDialog();
            LoadUnit(_selectedUnit);
        }

        private void btnSupport_Click(object sender, EventArgs e)
        {
            var supportEditor = new UnitViewer.Supports(_chapterSave, _selectedUnit);
            supportEditor.ShowDialog();
            LoadUnit(_selectedUnit);
        }

        private void btnTraits_Click(object sender, EventArgs e)
        {
            var traits = new UnitViewer.Traits(_selectedUnit);
            traits.ShowDialog();
            LoadUnit(_selectedUnit);
        }

        #endregion

        #region Global Data

        private void LoadGlobalData()
        {
            globalDataMain1.LoadGlobalSave(_globalSave);
        }

        #endregion

        #region Compression

        private void decompressFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "'Chapter' or 'Global' save|*";

            var startupPath = Config.StartupPath;
            if (startupPath == "" || !Directory.Exists(startupPath)) startupPath = Application.StartupPath;
            openFileDialog1.InitialDirectory = startupPath;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Config.StartupPath = Path.GetDirectoryName(openFileDialog1.FileName);

                var saveFile = Model.SaveFile.FromPath(openFileDialog1.FileName);

                if (!(saveFile.Type == Enums.SaveFileType.Chapter || 
                    saveFile.Type == Enums.SaveFileType.Global || 
                    saveFile.Type == Enums.SaveFileType.Exchange ||
                    saveFile.Type == Enums.SaveFileType.Versus))
                {
                    MessageBox.Show("This type of save is not supported yet. Only 'Chapter', 'Global', 'Exchange', and 'Versus' saves are supported by the compression/decompression right now.");
                    return;
                }

                saveFile.Decompress();

                MessageBox.Show("Done! Decompressed save written to the original filename but with _dec on the end.");
            }
        }

        private void compressFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "'Chapter' or 'Global' save|*";

            var startupPath = Config.StartupPath;
            if (startupPath == "" || !Directory.Exists(startupPath)) startupPath = Application.StartupPath;
            openFileDialog1.InitialDirectory = startupPath;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Config.StartupPath = Path.GetDirectoryName(openFileDialog1.FileName);

                var saveFile = Model.SaveFile.FromPath(openFileDialog1.FileName);

                if (!(saveFile.Type == Enums.SaveFileType.Chapter ||
                    saveFile.Type == Enums.SaveFileType.Global ||
                    saveFile.Type == Enums.SaveFileType.Exchange ||
                    saveFile.Type == Enums.SaveFileType.Versus))
                {
                    MessageBox.Show("This type of save is not supported yet. Only 'Chapter', 'Global', 'Exchange', and 'Versus' saves are supported by the compression/decompression right now.");
                    return;
                }

                saveFile.Compress();

                MessageBox.Show("Done! Compressed save written to the original filename, with _dec removed if applicable.");
            }
        }

        #endregion
    }
}
