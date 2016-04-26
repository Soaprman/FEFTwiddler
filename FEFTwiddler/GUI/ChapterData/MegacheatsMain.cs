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
                unit.Boots = Model.Character.MaxBoots;
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
                unit.CanUseDragonVein = true;
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
