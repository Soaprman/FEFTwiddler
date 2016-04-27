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
        private Model.Character _selectedCharacter;
        private Controls.Blanket _unitViewerBlanket;

        public MainForm()
        {
            InitializeComponent();
            InitializeDatabases();
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

                var saveFile = Model.SaveFile.FromPath(openFileDialog1.FileName);

                if (saveFile.Type != Enums.SaveFileType.Chapter && saveFile.Type != Enums.SaveFileType.Global)
                {
                    MessageBox.Show("This type of save is not supported yet. Only 'Chapter' and 'Global' saves are supported right now.");
                    return;
                }
                else
                {
                    _saveFile = saveFile;
                }

                UpdateTitleBar(openFileDialog1.FileName);

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
                        tabNewGamePlus.Enabled = true;
                        tabGlobalData.Enabled = false;

                        break;
                    case Enums.SaveFileType.Global:
                        _globalSave = Model.GlobalSave.FromSaveFile(_saveFile);
                        _chapterSave = null;

                        LoadGlobalData();

                        tabChapterData.Enabled = false;
                        tabUnitViewer.Enabled = false;
                        tabMegacheats.Enabled = false;
                        tabNewGamePlus.Enabled = false;
                        tabGlobalData.Enabled = true;

                        break;
                    default: break;
                }

                tabControl1.Enabled = true;
            }
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
        }

        private void btnChapterHistory_Click(object sender, EventArgs e)
        {
            var popup = new GUI.ChapterData.ChapterHistory(_chapterSave);
            popup.ShowDialog();
        }

        #endregion

        #region Unit Viewer

        public void LoadUnitViewer()
        {
            lstLiving.DisplayMember = "CharacterID";
            lstLiving.ValueMember = "CharacterID";
            lstLiving.Items.Clear();

            lstDead.DisplayMember = "CharacterID";
            lstDead.ValueMember = "CharacterID";
            lstDead.Items.Clear();
            foreach (var character in _chapterSave.UnitRegion.Units)
            {
                if (character.IsDead) lstDead.Items.Add(character);
                else lstLiving.Items.Add(character);
            }

            lstLiving.SelectedIndex = 0;
            lstDead.ClearSelected();
        }

        private void SelectLivingCharacter(object sender, EventArgs e)
        {
            var character = (Model.Character)lstLiving.SelectedItem;
            if (character == null) return;
            _selectedCharacter = character;
            LoadCharacter(character);
            lstDead.ClearSelected();
        }

        private void SelectDeadCharacter(object sender, EventArgs e)
        {
            var character = (Model.Character)lstDead.SelectedItem;
            if (character == null) return;
            _selectedCharacter = character;
            LoadCharacter(character);
            lstLiving.ClearSelected();
        }

        private void LoadCharacter(Model.Character character)
        {
            if (character == null) return;

            var message = "";

            if (Enum.IsDefined(typeof(Enums.Character), character.CharacterID))
            {
                if (character.CorrinName != null)
                {
                    lblName.Text = character.CorrinName;
                }
                else if (Data.Database.Characters.GetByID(character.CharacterID).IsPrisoner)
                {
                    lblName.Text = Data.Database.Prisoners.GetByID(character.PrisonerID).DisplayName;
                }
                else
                {
                    lblName.Text = Data.Database.Characters.GetByID(character.CharacterID).DisplayName;
                }
            }
            else
            {
                lblName.Text = character.CharacterID.ToString();
            }

            try { class1.LoadCharacter(_selectedCharacter); }
            catch (Exception) { message += Environment.NewLine + "Error loading Class data"; }

            try { levelAndExperience1.LoadCharacter(_selectedCharacter); }
            catch (Exception) { message += Environment.NewLine + "Error loading Level and Experience data"; }

            try { stats1.LoadCharacter(_selectedCharacter); }
            catch (Exception) { message += Environment.NewLine + "Error loading Stats data"; }

            try { flags1.LoadCharacter(_selectedCharacter); }
            catch (Exception) { message += Environment.NewLine + "Error loading Flags data"; }

            try { battleData1.LoadCharacter(_selectedCharacter); }
            catch (Exception) { message += Environment.NewLine + "Error loading Battle data"; }

            try { skills1.LoadCharacter(_selectedCharacter); }
            catch (Exception) { message += Environment.NewLine + "Error loading Skills data"; }

            try { inventory1.LoadCharacter(_selectedCharacter); }
            catch (Exception) { message += Environment.NewLine + "Error loading Inventory data"; }

            try { accessories1.LoadCharacter(_selectedCharacter); }
            catch (Exception) { message += Environment.NewLine + "Error loading Accessories data"; }

            try { hairColor1.LoadCharacter(_selectedCharacter); }
            catch (Exception) { message += Environment.NewLine + "Error loading Hair Color data"; }

            try { weaponExperience1.LoadCharacter(_selectedCharacter); }
            catch (Exception) { message += Environment.NewLine + "Error loading Weapon Experience data"; }

            try { dragonVein1.LoadCharacter(_selectedCharacter); }
            catch (Exception) { message += Environment.NewLine + "Error loading Dragon Vein data"; }

            if (message.Length > 0)
            {
                message = "One or more values is invalid for this character. You can still use the hex editor, though." + Environment.NewLine + Environment.NewLine + message;
                _unitViewerBlanket.SetMessage(message);
                _unitViewerBlanket.Cover();

                btnOpenHexEditor.BringToFront();
            }
            else
            {
                _unitViewerBlanket.Uncover();
            }
        }

        private void tabPage1_Enter(object sender, EventArgs e)
        {
            LoadCharacter(_selectedCharacter);
        }

        private void btnOpenHexEditor_Click(object sender, EventArgs e)
        {
            var hex = new GUI.UnitViewer.HexEditor(_selectedCharacter);
            hex.ShowDialog();
            LoadCharacter(_selectedCharacter);
        }

        #endregion

        #region Global Data

        private void LoadGlobalData()
        {
            globalDataMain1.LoadGlobalSave(_globalSave);
        }

        #endregion

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

                if (saveFile.Type != Enums.SaveFileType.Chapter && saveFile.Type != Enums.SaveFileType.Global)
                {
                    MessageBox.Show("This type of save is not supported yet. Only 'Chapter' and 'Global' saves are supported right now.");
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

                if (saveFile.Type != Enums.SaveFileType.Chapter && saveFile.Type != Enums.SaveFileType.Global)
                {
                    MessageBox.Show("This type of save is not supported yet. Only 'Chapter' and 'Global' saves are supported right now.");
                    return;
                }

                saveFile.Compress();

                MessageBox.Show("Done! Compressed save written to the original filename, with _dec removed if applicable.");
            }
        }
    }
}
