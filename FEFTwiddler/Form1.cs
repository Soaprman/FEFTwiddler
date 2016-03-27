using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Resources;

namespace FEFTwiddler
{
    public partial class Form1 : Form
    {
        private Model.ChapterSave _chapterSave;
        private Dictionary<string, Model.Character> _listBoxHelper;
        private Model.Character _selectedCharacter;

        public Form1()
        {
            InitializeComponent();
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    // Main testing file
        //    var chapterSave = Model.ChapterSave.FromPath(@"E:\Games\3DS Homebrew\Backups\Saves\Fire Emblem Fates Birthright\009\Chapter0_dec");

        //    // An older birthright save
        //    //var chapterSave = Model.ChapterSave.FromPath(@"E:\Games\3DS Homebrew\Backups\Saves\Fire Emblem Fates Birthright\000 - original with no edits_dec\Chapter2_dec");

        //    //foreach (var character in chapterSave.Characters)
        //    //{
        //    //    //tblCharacters.Controls.Add(new GUI.CharacterPanelOld(character));
        //    //    tblCharacters.Controls.Add(new GUI.CharacterPanel(character));
        //    //}
        //}

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _chapterSave = Model.ChapterSave.FromPath(openFileDialog1.FileName);

                _listBoxHelper = new Dictionary<string, Model.Character>();
                foreach (var character in _chapterSave.Characters)
                {
                    _listBoxHelper.Add(character.CharacterID.ToString(), character);
                    listBox1.Items.Add(character.CharacterID.ToString());
                }

                PopulatePickers();

                tabControl1.Enabled = true;

                listBox1.SelectedIndex = 0;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var characterId = listBox1.SelectedItem.ToString();
            var character = _listBoxHelper[characterId];
            _selectedCharacter = character;
            LoadCharacter(character);
        }

        private void LoadCharacter(Model.Character character)
        {
            lblName.Text = character.CharacterID.ToString();
            cmbClass.Text = character.ClassID.ToString();
            numLevel.Value = character.Level;
            numExperience.Value = character.Experience;

            cmbSkill1.Text = character.EquippedSkill_1.ToString();
            pictSkill1.Image = GetSkillImage(character.EquippedSkill_1);
            cmbSkill2.Text = character.EquippedSkill_2.ToString();
            pictSkill2.Image = GetSkillImage(character.EquippedSkill_2);
            cmbSkill3.Text = character.EquippedSkill_3.ToString();
            pictSkill3.Image = GetSkillImage(character.EquippedSkill_3);
            cmbSkill4.Text = character.EquippedSkill_4.ToString();
            pictSkill4.Image = GetSkillImage(character.EquippedSkill_4);
            cmbSkill5.Text = character.EquippedSkill_5.ToString();
            pictSkill5.Image = GetSkillImage(character.EquippedSkill_5);

            //EnableControls();
        }

        private void PopulatePickers()
        {
            cmbClass.DataSource = Enum.GetValues(typeof(Enums.Class));
            cmbSkill1.DataSource = Enum.GetValues(typeof(Enums.Skill));
            cmbSkill2.DataSource = Enum.GetValues(typeof(Enums.Skill));
            cmbSkill3.DataSource = Enum.GetValues(typeof(Enums.Skill));
            cmbSkill4.DataSource = Enum.GetValues(typeof(Enums.Skill));
            cmbSkill5.DataSource = Enum.GetValues(typeof(Enums.Skill));
        }

        private Bitmap GetSkillImage(Enums.Skill skillId)
        {
            var id = ((byte)skillId).ToString();
            if (id.Length == 1) id = "0" + id;
            var img = Properties.Resources.ResourceManager.GetObject("fe15skill_" + id);

            return (Bitmap)img;
        }

        private void EnableControls()
        {
            cmbClass.Enabled = true;
            numLevel.Enabled = true;
            numExperience.Enabled = true;
            cmbSkill1.Enabled = true;
            cmbSkill2.Enabled = true;
            cmbSkill3.Enabled = true;
            cmbSkill4.Enabled = true;
            cmbSkill5.Enabled = true;
        }

        private void cmbSkill1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Enums.Skill skillId;
            //if (Enum.TryParse(cmbSkill1.Text, out skillId))
            //{
            //    _selectedCharacter.EquippedSkill_1 = skillId;
            //    pictSkill1.Image = GetSkillImage(_selectedCharacter.EquippedSkill_1);
            //}
        }

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

        private void btnAllSkillsNoNpc_Click(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.Characters)
            {
                character.LearnAllNonNpcSkills();
            }
        }

        private void btnGiveEternalSeals_Click(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.Characters)
            {
                character.EternalSealsUsed = 16;
            }
        }
    }
}
