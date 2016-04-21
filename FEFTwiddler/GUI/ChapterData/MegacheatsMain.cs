using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FEFTwiddler.GUI.ChapterData
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class MegacheatsMain : UserControl
    {
        private Model.ChapterSave _chapterSave;

        public MegacheatsMain()
        {
            InitializeComponent();
            InitializeControls();
        }

        public void LoadChapterSave(Model.ChapterSave chapterSave)
        {
            _chapterSave = chapterSave;

            skills1.LoadChapterSave(_chapterSave);

            PopulateControls();
        }

        private void InitializeControls()
        {
        }

        private void PopulateControls()
        {
        }

        private void btnAllCharAllSkills_Click(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.Characters)
            {
                character.LearnNormalClassSkills();
            }
            MessageBox.Show("Done!");
        }

        private void btnAllCharAllSkillsDLC_Click(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.Characters)
            {
                character.LearnPathBonusClassSkills();
            }
            MessageBox.Show("Done!");
        }

        private void btnAllCharAllSkillsEnemy_Click(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.Characters)
            {
                //character.LearnEnemySkills();
            }
            MessageBox.Show("Done!");
        }

        private void btnAllCharMaxStatue_Click(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.Characters)
            {
                character.MaximizeStatues();
            }
            MessageBox.Show("Done!");
        }

        private void btnGiveEternalSeals_Click(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.Characters)
            {
                character.EternalSealsUsed = 16;
            }
            MessageBox.Show("Done!");
        }

        private void btnMaxWeaponExp_Click(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.Characters)
            {
                character.SRankAllWeapons();
            }
            MessageBox.Show("Done!");
        }

        private void btnMaxBoots_Click(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.Characters)
            {
                character.Boots = Model.Character.MaxBoots;
            }
            MessageBox.Show("Done!");
        }

        private void btn1Boots_Click(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.Characters)
            {
                character.Boots = (byte)1;
            }
            MessageBox.Show("Done!");
        }

        private void btn0Boots_Click(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.Characters)
            {
                character.Boots = (byte)0;
            }
            MessageBox.Show("Done!");
        }

        private void btnNewGamePlus_Click(object sender, EventArgs e)
        {
            foreach (var battlefield in _chapterSave.Battlefields)
            {
                switch (battlefield.ChapterID)
                {
                    case Enums.Chapter.Prologue:
                    case Enums.Chapter.Chapter1:
                    case Enums.Chapter.Chapter2:
                    case Enums.Chapter.Chapter3:
                    case Enums.Chapter.Chapter4:
                    case Enums.Chapter.Chapter5:
                    case Enums.Chapter.Chapter6:
                    case Enums.Chapter.Birthright_Chapter6:
                    case Enums.Chapter.Conquest_Chapter6:
                    case Enums.Chapter.Revelation_Chapter6:
                        break; // Do nothing
                    case Enums.Chapter.Birthright_Chapter7:
                    case Enums.Chapter.Conquest_Chapter7:
                    case Enums.Chapter.Revelation_Chapter7:
                        battlefield.BattlefieldStatus = Enums.BattlefieldStatus.Available;
                        break;
                    default:
                        battlefield.BattlefieldStatus = Enums.BattlefieldStatus.Unavailable;
                        break;
                }
            }
            MessageBox.Show("Done! Remember... whatever happens, you asked for it!");
        }
    }
}
