using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace FEFTwiddler.GUI.ChapterData
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class GoldAndPoints : UserControl
    {
        private Model.ChapterSave _chapterSave;

        public GoldAndPoints()
        {
            InitializeComponent();
            InitializeControls();
        }

        public void LoadChapterSave(Model.ChapterSave chapterSave)
        {
            _chapterSave = chapterSave;
            PopulateControls();
        }

        private void InitializeControls()
        {
        }

        private void PopulateControls()
        {
            numGold.Value = _chapterSave.Gold;

            numDragonVeinPoints.Value = _chapterSave.DragonVeinPoint / 100;

            // TODO: Add these after fixing ChapterSave
            //numBattlePoints.Value = _chapterSave.BattlePoints;
            //numVisitPoints.Value = _chapterSave.VisitPoints;
        }

        private void btnMaxGold_Click(object sender, EventArgs e)
        {
            numGold.Value = 999999;
        }

        private void btn99DragonVeinPoints_Click(object sender, EventArgs e)
        {
            numDragonVeinPoints.Value = 99;
        }

        private void btn9999BattlePoints_Click(object sender, EventArgs e)
        {
            numBattlePoints.Value = 9999;
        }

        private void btn9999VisitPoints_Click(object sender, EventArgs e)
        {
            numVisitPoints.Value = 9999;
        }

        private void numGold_ValueChanged(object sender, EventArgs e)
        {
            _chapterSave.Gold = (uint)(numGold.Value);
        }

        private void numDragonVeinPoints_ValueChanged(object sender, EventArgs e)
        {
            _chapterSave.DragonVeinPoint = (ushort)(numDragonVeinPoints.Value * 100);
        }

        private void numBattlePoints_ValueChanged(object sender, EventArgs e)
        {
            _chapterSave.BattlePoints = (uint)numBattlePoints.Value;
        }

        private void numVisitPoints_ValueChanged(object sender, EventArgs e)
        {
            _chapterSave.VisitPoints = (uint)numVisitPoints.Value;
        }
    }
}
