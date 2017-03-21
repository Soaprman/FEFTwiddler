using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace FEFTwiddler.GUI.ChapterData
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class GoldAndPoints : UserControl
    {
        private Model.IChapterSave _chapterSave;

        public GoldAndPoints()
        {
            InitializeComponent();
            InitializeControls();
        }

        public void LoadChapterSave(Model.IChapterSave chapterSave)
        {
            _chapterSave = chapterSave;
            PopulateControls();
        }

        private void InitializeControls()
        {
        }

        private void PopulateControls()
        {
            numGold.Value = _chapterSave.UserRegion.Gold;

            numDragonVeinPoints.Value = _chapterSave.MyCastleRegion.DragonVeinPoint / 100;
        }

        private void btnMaxGold_Click(object sender, EventArgs e)
        {
            numGold.Value = 999999;
        }

        private void btn99DragonVeinPoints_Click(object sender, EventArgs e)
        {
            numDragonVeinPoints.Value = 99;
        }

        private void numGold_ValueChanged(object sender, EventArgs e)
        {
            _chapterSave.UserRegion.Gold = (uint)(numGold.Value);
        }

        private void numDragonVeinPoints_ValueChanged(object sender, EventArgs e)
        {
            _chapterSave.MyCastleRegion.DragonVeinPoint = (ushort)(numDragonVeinPoints.Value * 100);
        }
    }
}
