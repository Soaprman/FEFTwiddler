using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace FEFTwiddler.GUI.UnitViewer
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class Accessories : UserControl
    {
        private Model.Character _character;

        public Accessories()
        {
            InitializeComponent();
            InitializeControls();
        }

        public void LoadCharacter(Model.Character character)
        {
            _character = character;
            PopulateControls();
        }

        private void InitializeControls()
        {
            cmbHeadwear.DataSource = Enum.GetValues(typeof(Enums.Headwear));
            cmbFacewear.DataSource = Enum.GetValues(typeof(Enums.Facewear));
            cmbArmwear.DataSource = Enum.GetValues(typeof(Enums.Armwear));
            cmbUnderwear.DataSource = Enum.GetValues(typeof(Enums.Underwear));
        }

        private void PopulateControls()
        {
            cmbHeadwear.Text = _character.Headwear.ToString();
            cmbFacewear.Text = _character.Facewear.ToString();
            cmbArmwear.Text = _character.Armwear.ToString();
            cmbUnderwear.Text = _character.Underwear.ToString();
        }
    }
}
