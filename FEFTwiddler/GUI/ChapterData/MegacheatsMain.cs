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
        private Model.Stat caps;

        private System.Windows.Forms.Label lblHP;
        private System.Windows.Forms.NumericUpDown numHP;
        private System.Windows.Forms.Label lblStr;
        private System.Windows.Forms.NumericUpDown numStr;
        private System.Windows.Forms.Label lblMag;
        private System.Windows.Forms.NumericUpDown numMag;
        private System.Windows.Forms.Label lblSkl;
        private System.Windows.Forms.NumericUpDown numSkl;
        private System.Windows.Forms.Label lblSpd;
        private System.Windows.Forms.NumericUpDown numSpd;
        private System.Windows.Forms.Label lblLck;
        private System.Windows.Forms.NumericUpDown numLck;
        private System.Windows.Forms.Label lblDef;
        private System.Windows.Forms.NumericUpDown numDef;
        private System.Windows.Forms.Label lblRes;
        private System.Windows.Forms.NumericUpDown numRes;

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
            btnNewGamePlus.Text = "Experimental New Game Plus mode. May have other effects that I haven't bothered writing on this button. Click at your own risk!";
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

        private void btnDragonVein_Click(object sender, EventArgs e)
        {
         //   MessageBox.Show("Starting!");
            foreach (var character in _chapterSave.UnitRegion.Units)
            {
                character.CanUseDragonVein.Equals(true);
            }
            MessageBox.Show("Done!");
        }

        private void btnNewGamePlus_Click(object sender, EventArgs e)
        {
            var timeMachine = new Model.NewGamePlus.TimeMachine(_chapterSave);
            var randomizer = new Model.NewGamePlus.Randomizer(_chapterSave);

            timeMachine.RemoveEveryoneButCorrin();
            timeMachine.InsertCharacters();
            randomizer.RandomizeClasses();
            randomizer.EquipWeapons();
            timeMachine.LevelUpAllUnits();
            //timeMachine.ReturnToChapter7();
            timeMachine.ReturnToPrologue();

            MainForm.GetFromHere(this).LoadUnitViewer();

            MessageBox.Show("Done! Remember... whatever happens, you asked for it!");
        }

        private void btnDragonVein_Click_1(object sender, EventArgs e)
        {
            //MessageBox.Show("Starting!");
            foreach (var character in _chapterSave.UnitRegion.Units)
            {
                character.CanUseDragonVein = true;
            }
            MessageBox.Show("Done!");
        }

        private void ultraCheat_Click(object sender, EventArgs e)
        {
            
            foreach (var character in _chapterSave.UnitRegion.Units)
            {
                character.CanUseDragonVein = true;
                character.Boots = Model.Character.MaxBoots;
                character.SRankAllWeapons();
                character.EternalSealsUsed = 16;
                caps = Utils.StatUtil.CalculateStatCaps(character);
                Model.Stat stats = Utils.StatUtil.CalculateStats(character);

                numHP.Maximum = caps.HP;
                numStr.Maximum = caps.Str;
                numMag.Maximum = caps.Mag;
                numSkl.Maximum = caps.Skl;
                numSpd.Maximum = caps.Spd;
                numLck.Maximum = caps.Lck;
                numDef.Maximum = caps.Def;
                numRes.Maximum = caps.Res;
                character.GainedStats = new Model.Stat
                {

                    HP = caps.HP,
                    Str = caps.Str,
                    Mag = caps.Mag,
                    Skl = caps.Skl,
                    Spd = caps.Spd,
                    Lck = caps.Lck,
                    Def = caps.Def,
                    Res = caps.Res
                };
            }

        }
    }
}
