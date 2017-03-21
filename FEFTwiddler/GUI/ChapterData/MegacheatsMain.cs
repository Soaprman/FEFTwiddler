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
        private Model.IChapterSave _chapterSave;

        public MegacheatsMain()
        {
            InitializeComponent();
            InitializeControls();
        }

        public void LoadChapterSave(Model.IChapterSave chapterSave)
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

        private void btnAllCharMaxStatue_Click(object sender, EventArgs e)
        {
            foreach (var unit in _chapterSave.UnitRegion.Units)
            {
                unit.MaximizeStatues();
            }
            MessageBox.Show("Done!");
        }

        private void btnGiveEternalSeals_Click(object sender, EventArgs e)
        {
            foreach (var unit in _chapterSave.UnitRegion.Units)
            {
                unit.EternalSealsUsed = 16;
            }
            MessageBox.Show("Done!");
        }

        private void btnMaxWeaponExp_Click(object sender, EventArgs e)
        {
            foreach (var unit in _chapterSave.UnitRegion.Units)
            {
                unit.SRankAllWeapons();
            }
            MessageBox.Show("Done!");
        }

        private void btnMaxBoots_Click(object sender, EventArgs e)
        {
            foreach (var unit in _chapterSave.UnitRegion.Units)
            {
                unit.Boots = Model.Unit.MaxBoots;
            }
            MessageBox.Show("Done!");
        }

        private void btn1Boots_Click(object sender, EventArgs e)
        {
            foreach (var unit in _chapterSave.UnitRegion.Units)
            {
                unit.Boots = (byte)1;
            }
            MessageBox.Show("Done!");
        }

        private void btn0Boots_Click(object sender, EventArgs e)
        {
            foreach (var unit in _chapterSave.UnitRegion.Units)
            {
                unit.Boots = (byte)0;
            }
            MessageBox.Show("Done!");
        }

        private void btnMaxStats_Click(object sender, EventArgs e)
        {
            foreach (var unit in _chapterSave.UnitRegion.Units)
            {
                unit.GainedStats = Utils.StatUtil.CalculateStatCaps(unit);
            }
            MessageBox.Show("Done!");
        }

        private void btnGiveDragonBlood_Click(object sender, EventArgs e)
        {
            foreach (var unit in _chapterSave.UnitRegion.Units)
            {
                unit.Trait_CanUseDragonVein = true;
            }
            MessageBox.Show("Done!");
        }

        private void btnHearSealDlc_Click(object sender, EventArgs e)
        {
            foreach (var unit in _chapterSave.UnitRegion.Units)
            {
                unit.DLCClassFlags = new byte[] { 0xFF, 0xFF, 0xFF };
            }
            MessageBox.Show("Done!");
        }

        private void btnResetSupports_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("This will reset all supports and the A+ rank choice." +
                Environment.NewLine + "Child units will not cease to exist, which could cause issues if you re-establish S supports (this is untested)." +
                Environment.NewLine + "Proceed?",
                "Reset supports?",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
            if (result != DialogResult.Yes) return;

            foreach (var unit in _chapterSave.UnitRegion.Units)
            {
                // Sets all support bytes to 0x00
                var supports = new byte[unit.RawNumberOfSupports];
                unit.RawSupports = supports;

                var characterData = Data.Database.Characters.GetByID(unit.CharacterID);
                if (characterData != null && characterData.IsChild)
                {
                    unit.FatherSupport = 0;
                    unit.MotherSupport = 0;
                    unit.SiblingSupport = 0;
                }
                unit.APlusSupportCharacter = Enums.Character.None;
            }
            MessageBox.Show("Done!");
        }

        private void btnStatBonuses_Click(object sender, EventArgs e)
        {
            foreach (var unit in _chapterSave.UnitRegion.Units)
            {
                unit.TonicBonusStats = new Model.Stat { HP = 5, Str = 2, Mag = 2, Skl = 2, Spd = 2, Lck = 4, Def = 2, Res = 2 };
                unit.StatusBonusStats = new Model.Stat { HP = 0, Str = 4, Mag = 4, Skl = 4, Spd = 4, Lck = 4, Def = 4, Res = 4 };
                unit.MealBonusStats = new Model.Stat { HP = 0, Str = 2, Mag = 2, Skl = 2, Spd = 2, Lck = 2, Def = 2, Res = 0 };
            }
            MessageBox.Show("Done!");
        }

        private void btnMysteryCheat_Click(object sender, EventArgs e)
        {
            Model.Cheats.MysteryCheat(_chapterSave);

            MainForm.GetFromHere(this).LoadUnitViewer();

            MessageBox.Show("Oh my! Do you have any idea what you just did?");
        }

        private void btnUnlockAllStatues_Click(object sender, EventArgs e)
        {
            Model.Cheats.UnlockAllStatues(_chapterSave);
            MessageBox.Show("Done!");
        }
    }
}
