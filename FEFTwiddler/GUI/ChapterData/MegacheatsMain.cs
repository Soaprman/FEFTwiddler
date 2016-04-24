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
            btnMysteryCheat.Text = "Experimental New Game Plus mode. May have other effects that I haven't bothered writing on this button. Click at your own risk!";
        }

        private void PopulateControls()
        {
        }

        private void btnAllCharAllSkills_Click(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.UnitRegion.Units)
            {
                character.LearnNormalClassSkills();
            }
            MessageBox.Show("Done!");
        }

        private void btnAllCharAllSkillsDLC_Click(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.UnitRegion.Units)
            {
                character.LearnPathBonusClassSkills();
            }
            MessageBox.Show("Done!");
        }

        private void btnAllCharAllSkillsEnemy_Click(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.UnitRegion.Units)
            {
                //character.LearnEnemySkills();
            }
            MessageBox.Show("Done!");
        }

        private void btnAllCharMaxStatue_Click(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.UnitRegion.Units)
            {
                character.MaximizeStatues();
            }
            MessageBox.Show("Done!");
        }

        private void btnGiveEternalSeals_Click(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.UnitRegion.Units)
            {
                character.EternalSealsUsed = 16;
            }
            MessageBox.Show("Done!");
        }

        private void btnMaxWeaponExp_Click(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.UnitRegion.Units)
            {
                character.SRankAllWeapons();
            }
            MessageBox.Show("Done!");
        }

        private void btnMaxBoots_Click(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.UnitRegion.Units)
            {
                character.Boots = Model.Character.MaxBoots;
            }
            MessageBox.Show("Done!");
        }

        private void btn1Boots_Click(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.UnitRegion.Units)
            {
                character.Boots = (byte)1;
            }
            MessageBox.Show("Done!");
        }

        private void btn0Boots_Click(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.UnitRegion.Units)
            {
                character.Boots = (byte)0;
            }
            MessageBox.Show("Done!");
        }

        private void btnMaxStats_Click(object sender, EventArgs e)
        {
            sbyte max = 0x63;
            foreach (var character in _chapterSave.UnitRegion.Units)
            {
                character.GainedStats = new Model.Stat {
                    HP = max,
                    Str = max,
                    Mag = max,
                    Skl = max,
                    Spd = max,
                    Lck = max,
                    Def = max,
                    Res = max
                };
            }
            MessageBox.Show("Done!");
        }

        private void btnMysteryCheat_Click(object sender, EventArgs e)
        {
            Model.Cheats.MysteryCheat(_chapterSave);

            MainForm.GetFromHere(this).LoadUnitViewer();

            MessageBox.Show("Oh my! Do you have any idea what you just did?");
        }
    }
}
