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
        private Model.Character _selectedCharacter;

        public MainForm()
        {
            InitializeComponent();
            InitializeDatabases();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void InitializeDatabases()
        {
            // TODO: Let user specify language
            // Will need to call SetLanguage on all databases when switching and refresh GUI for display names
            Data.Database.SetLanguage(Enums.Language.English);
        }

        #region Load

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";

            var startupPath = Config.StartupPath;
            if (startupPath == "" || !Directory.Exists(startupPath)) startupPath = Application.StartupPath;
            openFileDialog1.InitialDirectory = startupPath;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Config.StartupPath = Path.GetDirectoryName(openFileDialog1.FileName);

                var saveFile = Model.SaveFile.FromPath(openFileDialog1.FileName);

                if (saveFile.Type != Enums.SaveFileType.Chapter)
                {
                    MessageBox.Show("This type of save is not supported yet. Only 'Chapter' saves are supported right now.");
                    return;
                }
                else
                {
                    _saveFile = saveFile;
                }

                UpdateTitleBar(openFileDialog1.FileName);

                _chapterSave = Model.ChapterSave.FromSaveFile(_saveFile);

                listBox1.DisplayMember = "CharacterID";
                listBox1.ValueMember = "BinaryPosition"; // Sure, why not
                listBox1.Items.Clear();
                foreach (var character in _chapterSave.Characters)
                {
                    listBox1.Items.Add(character);
                }

                LoadChapterData();

                tabControl1.Enabled = true;

                listBox1.SelectedIndex = 0;
            }
        }

        private void UpdateTitleBar(string path)
        {
            var directory = Path.GetDirectoryName(path);
            if (directory.Length > 50) directory = "..." + directory.Right(50); // Arbitary choice that I find tends to fit
            var truncatedPath = directory + "\\" + Path.GetFileName(path);
            this.Text = "FEFTwiddler - " + truncatedPath;
        }

        #endregion

        #region Save

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_chapterSave == null)
            {
                MessageBox.Show("No file is loaded");
                return;
            }

            _chapterSave.Write();

            MessageBox.Show("File saved. Hope you made a backup!");
        }

        #endregion

        #region Event Handlers - Chapter Data

        private void LoadChapterData()
        {
            lblAvatarName.Text = Utils.TypeConverter.ToString(_chapterSave.AvatarName);

            goldAndPoints1.LoadChapterSave(_chapterSave);
            materials1.LoadChapterSave(_chapterSave);
            megacheats1.LoadChapterSave(_chapterSave);
        }

        #endregion

        #region Event Handlers - Unit Viewer

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var character = (Model.Character)listBox1.SelectedItem;
            //var character = (from c in _chapterSave.Characters
            //                 where c.BinaryPosition == characterId
            //                 select c).FirstOrDefault();
            _selectedCharacter = character;
            LoadCharacter(character);
        }

        private void LoadCharacter(Model.Character character)
        {
            if (Enum.IsDefined(typeof(Enums.Character), character.CharacterID))
                lblName.Text = Data.Database.Characters.GetByID(character.CharacterID).DisplayName;
            else
                lblName.Text = character.CharacterID.ToString();

            class1.LoadCharacter(_selectedCharacter);
            levelAndExperience1.LoadCharacter(_selectedCharacter);
            stats1.LoadCharacter(_selectedCharacter);
            flags1.LoadCharacter(_selectedCharacter);
            battleData1.LoadCharacter(_selectedCharacter);
            equippedSkills1.LoadCharacter(_selectedCharacter);
            learnedSkills1.LoadCharacter(_selectedCharacter);
            inventory1.LoadCharacter(_selectedCharacter);
            accessories1.LoadCharacter(_selectedCharacter);
            hairColor1.LoadCharacter(_selectedCharacter);
            weaponExperience1.LoadCharacter(_selectedCharacter);
        }

        private void tabPage1_Enter(object sender, EventArgs e)
        {
            LoadCharacter(_selectedCharacter);
        }

        #endregion
    }
}
