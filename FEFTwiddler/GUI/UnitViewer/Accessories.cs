using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
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
        }

        private void PopulateControls()
        {
            UnbindEvents();
            BindDataSources();
            cmbHeadwear.SelectedValue = _character.Headwear;
            cmbFacewear.SelectedValue = _character.Facewear;
            cmbArmwear.SelectedValue = _character.Armwear;
            cmbUnderwear.SelectedValue = _character.Underwear;
            BindEvents();
        }

        private void BindDataSources()
        {
            var accessories = Data.Database.Accessories.GetAll();

            cmbHeadwear.DisplayMember = "DisplayName";
            cmbHeadwear.ValueMember = "AccessoryID";
            cmbHeadwear.DataSource = accessories
                .Where((x) => (x.Type == Enums.AccessoryType.Headwear || x.Type == Enums.AccessoryType.None))
                .OrderBy((x) => x.DisplayName)
                .ToList();

            cmbFacewear.DisplayMember = "DisplayName";
            cmbFacewear.ValueMember = "AccessoryID";
            cmbFacewear.DataSource = accessories
                .Where((x) => (x.Type == Enums.AccessoryType.Facewear || x.Type == Enums.AccessoryType.None))
                .OrderBy((x) => x.DisplayName)
                .ToList();

            cmbArmwear.DisplayMember = "DisplayName";
            cmbArmwear.ValueMember = "AccessoryID";
            cmbArmwear.DataSource = accessories
                .Where((x) => (x.Type == Enums.AccessoryType.Armwear || x.Type == Enums.AccessoryType.None))
                .OrderBy((x) => x.DisplayName)
                .ToList();

            cmbUnderwear.DisplayMember = "DisplayName";
            cmbUnderwear.ValueMember = "AccessoryID";
            cmbUnderwear.DataSource = accessories
                .Where((x) => (x.Type == Enums.AccessoryType.Underwear || x.Type == Enums.AccessoryType.None))
                .OrderBy((x) => x.DisplayName)
                .ToList();
        }

        private void UnbindEvents()
        {
            cmbHeadwear.SelectedIndexChanged -= ChangeHeadwear;
            cmbFacewear.SelectedIndexChanged -= ChangeFacewear;
            cmbArmwear.SelectedIndexChanged -= ChangeArmwear;
            cmbUnderwear.SelectedIndexChanged -= ChangeUnderwear;
        }

        private void BindEvents()
        {
            cmbHeadwear.SelectedIndexChanged += ChangeHeadwear;
            cmbFacewear.SelectedIndexChanged += ChangeFacewear;
            cmbArmwear.SelectedIndexChanged += ChangeArmwear;
            cmbUnderwear.SelectedIndexChanged += ChangeUnderwear;
        }

        private void ChangeHeadwear(object sender, EventArgs e)
        {
            _character.Headwear = (Enums.Accessory)cmbHeadwear.SelectedValue;
        }

        private void ChangeFacewear(object sender, EventArgs e)
        {
            _character.Facewear = (Enums.Accessory)cmbFacewear.SelectedValue;
        }

        private void ChangeArmwear(object sender, EventArgs e)
        {
            _character.Armwear = (Enums.Accessory)cmbArmwear.SelectedValue;
        }

        private void ChangeUnderwear(object sender, EventArgs e)
        {
            _character.Underwear = (Enums.Accessory)cmbUnderwear.SelectedValue;
        }
    }
}
