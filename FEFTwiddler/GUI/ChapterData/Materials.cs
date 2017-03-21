using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace FEFTwiddler.GUI.ChapterData
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class Materials : UserControl
    {
        private Model.IChapterSave _chapterSave;

        public Materials()
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
            numAmber.ValueChanged += delegate (object sender, EventArgs e) {
                _chapterSave.MyCastleRegion.MaterialDiscovered_Amber = true;
                _chapterSave.MyCastleRegion.MaterialQuantity_Amber = (byte)((NumericUpDown)sender).Value;
            };
            numBeans.ValueChanged += delegate (object sender, EventArgs e) {
                _chapterSave.MyCastleRegion.MaterialDiscovered_Beans = true;
                _chapterSave.MyCastleRegion.MaterialQuantity_Beans = (byte)((NumericUpDown)sender).Value;
            };
            numBerries.ValueChanged += delegate (object sender, EventArgs e) {
                _chapterSave.MyCastleRegion.MaterialDiscovered_Berries = true;
                _chapterSave.MyCastleRegion.MaterialQuantity_Berries = (byte)((NumericUpDown)sender).Value;
            };
            numCabbage.ValueChanged += delegate (object sender, EventArgs e) {
                _chapterSave.MyCastleRegion.MaterialDiscovered_Cabbage = true;
                _chapterSave.MyCastleRegion.MaterialQuantity_Cabbage = (byte)((NumericUpDown)sender).Value;
            };
            numCoral.ValueChanged += delegate (object sender, EventArgs e) {
                _chapterSave.MyCastleRegion.MaterialDiscovered_Coral = true;
                _chapterSave.MyCastleRegion.MaterialQuantity_Coral = (byte)((NumericUpDown)sender).Value;
            };
            numCrystal.ValueChanged += delegate (object sender, EventArgs e) {
                _chapterSave.MyCastleRegion.MaterialDiscovered_Crystal = true;
                _chapterSave.MyCastleRegion.MaterialQuantity_Crystal = (byte)((NumericUpDown)sender).Value;
            };
            numDaikon.ValueChanged += delegate (object sender, EventArgs e) {
                _chapterSave.MyCastleRegion.MaterialDiscovered_Daikon = true;
                _chapterSave.MyCastleRegion.MaterialQuantity_Daikon = (byte)((NumericUpDown)sender).Value;
            };
            numEmerald.ValueChanged += delegate (object sender, EventArgs e) {
                _chapterSave.MyCastleRegion.MaterialDiscovered_Emerald = true;
                _chapterSave.MyCastleRegion.MaterialQuantity_Emerald = (byte)((NumericUpDown)sender).Value;
            };
            numFish.ValueChanged += delegate (object sender, EventArgs e) {
                _chapterSave.MyCastleRegion.MaterialDiscovered_Fish = true;
                _chapterSave.MyCastleRegion.MaterialQuantity_Fish = (byte)((NumericUpDown)sender).Value;
            };
            numJade.ValueChanged += delegate (object sender, EventArgs e) {
                _chapterSave.MyCastleRegion.MaterialDiscovered_Jade = true;
                _chapterSave.MyCastleRegion.MaterialQuantity_Jade = (byte)((NumericUpDown)sender).Value;
            };
            numLapis.ValueChanged += delegate (object sender, EventArgs e) {
                _chapterSave.MyCastleRegion.MaterialDiscovered_Lapis = true;
                _chapterSave.MyCastleRegion.MaterialQuantity_Lapis = (byte)((NumericUpDown)sender).Value;
            };
            numMeat.ValueChanged += delegate (object sender, EventArgs e) {
                _chapterSave.MyCastleRegion.MaterialDiscovered_Meat = true;
                _chapterSave.MyCastleRegion.MaterialQuantity_Meat = (byte)((NumericUpDown)sender).Value;
            };
            numMilk.ValueChanged += delegate (object sender, EventArgs e) {
                _chapterSave.MyCastleRegion.MaterialDiscovered_Milk = true;
                _chapterSave.MyCastleRegion.MaterialQuantity_Milk = (byte)((NumericUpDown)sender).Value;
            };
            numOnyx.ValueChanged += delegate (object sender, EventArgs e) {
                _chapterSave.MyCastleRegion.MaterialDiscovered_Onyx = true;
                _chapterSave.MyCastleRegion.MaterialQuantity_Onyx = (byte)((NumericUpDown)sender).Value;
            };
            numPeaches.ValueChanged += delegate (object sender, EventArgs e) {
                _chapterSave.MyCastleRegion.MaterialDiscovered_Peaches = true;
                _chapterSave.MyCastleRegion.MaterialQuantity_Peaches = (byte)((NumericUpDown)sender).Value;
            };
            numPearl.ValueChanged += delegate (object sender, EventArgs e) {
                _chapterSave.MyCastleRegion.MaterialDiscovered_Pearl = true;
                _chapterSave.MyCastleRegion.MaterialQuantity_Pearl = (byte)((NumericUpDown)sender).Value;
            };
            numQuartz.ValueChanged += delegate (object sender, EventArgs e) {
                _chapterSave.MyCastleRegion.MaterialDiscovered_Quartz = true;
                _chapterSave.MyCastleRegion.MaterialQuantity_Quartz = (byte)((NumericUpDown)sender).Value;
            };
            numRice.ValueChanged += delegate (object sender, EventArgs e) {
                _chapterSave.MyCastleRegion.MaterialDiscovered_Rice = true;
                _chapterSave.MyCastleRegion.MaterialQuantity_Rice = (byte)((NumericUpDown)sender).Value;
            };
            numRuby.ValueChanged += delegate (object sender, EventArgs e) {
                _chapterSave.MyCastleRegion.MaterialDiscovered_Ruby = true;
                _chapterSave.MyCastleRegion.MaterialQuantity_Ruby = (byte)((NumericUpDown)sender).Value;
            };
            numSapphire.ValueChanged += delegate (object sender, EventArgs e) {
                _chapterSave.MyCastleRegion.MaterialDiscovered_Sapphire = true;
                _chapterSave.MyCastleRegion.MaterialQuantity_Sapphire = (byte)((NumericUpDown)sender).Value;
            };
            numTopaz.ValueChanged += delegate (object sender, EventArgs e) {
                _chapterSave.MyCastleRegion.MaterialDiscovered_Topaz = true;
                _chapterSave.MyCastleRegion.MaterialQuantity_Topaz = (byte)((NumericUpDown)sender).Value;
            };
            numWheat.ValueChanged += delegate (object sender, EventArgs e) {
                _chapterSave.MyCastleRegion.MaterialDiscovered_Wheat = true;
                _chapterSave.MyCastleRegion.MaterialQuantity_Wheat = (byte)((NumericUpDown)sender).Value;
            };
        }

        private void PopulateControls()
        {
            numAmber.Value = _chapterSave.MyCastleRegion.MaterialQuantity_Amber;
            numBeans.Value = _chapterSave.MyCastleRegion.MaterialQuantity_Beans;
            numBerries.Value = _chapterSave.MyCastleRegion.MaterialQuantity_Berries;
            numCabbage.Value = _chapterSave.MyCastleRegion.MaterialQuantity_Cabbage;
            numCoral.Value = _chapterSave.MyCastleRegion.MaterialQuantity_Coral;
            numCrystal.Value = _chapterSave.MyCastleRegion.MaterialQuantity_Crystal;
            numDaikon.Value = _chapterSave.MyCastleRegion.MaterialQuantity_Daikon;
            numEmerald.Value = _chapterSave.MyCastleRegion.MaterialQuantity_Emerald;
            numFish.Value = _chapterSave.MyCastleRegion.MaterialQuantity_Fish;
            numJade.Value = _chapterSave.MyCastleRegion.MaterialQuantity_Jade;
            numLapis.Value = _chapterSave.MyCastleRegion.MaterialQuantity_Lapis;
            numMeat.Value = _chapterSave.MyCastleRegion.MaterialQuantity_Meat;
            numMilk.Value = _chapterSave.MyCastleRegion.MaterialQuantity_Milk;
            numOnyx.Value = _chapterSave.MyCastleRegion.MaterialQuantity_Onyx;
            numPeaches.Value = _chapterSave.MyCastleRegion.MaterialQuantity_Peaches;
            numPearl.Value = _chapterSave.MyCastleRegion.MaterialQuantity_Pearl;
            numQuartz.Value = _chapterSave.MyCastleRegion.MaterialQuantity_Quartz;
            numRice.Value = _chapterSave.MyCastleRegion.MaterialQuantity_Rice;
            numRuby.Value = _chapterSave.MyCastleRegion.MaterialQuantity_Ruby;
            numSapphire.Value = _chapterSave.MyCastleRegion.MaterialQuantity_Sapphire;
            numTopaz.Value = _chapterSave.MyCastleRegion.MaterialQuantity_Topaz;
            numWheat.Value = _chapterSave.MyCastleRegion.MaterialQuantity_Wheat;
        }

        private void btnMaxMaterials_Click(object sender, EventArgs e)
        {
            byte max = 99;
            numAmber.Value = max;
            numBeans.Value = max;
            numBerries.Value = max;
            numCabbage.Value = max;
            numCoral.Value = max;
            numCrystal.Value = max;
            numDaikon.Value = max;
            numEmerald.Value = max;
            numFish.Value = max;
            numJade.Value = max;
            numLapis.Value = max;
            numMeat.Value = max;
            numMilk.Value = max;
            numOnyx.Value = max;
            numPeaches.Value = max;
            numPearl.Value = max;
            numQuartz.Value = max;
            numRice.Value = max;
            numRuby.Value = max;
            numSapphire.Value = max;
            numTopaz.Value = max;
            numWheat.Value = max;
        }
    }
}
