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
        private Model.Unit _unit;

        public Accessories()
        {
            InitializeComponent();
            InitializeControls();
        }

        public void LoadUnit(Model.Unit unit)
        {
            _unit = unit;
            PopulateControls();
        }

        private void InitializeControls()
        {
        }

        private void PopulateControls()
        {
            UnbindEvents();
            BindDataSources();
            cmbHeadwear.SelectedValue = _unit.Headwear;
            cmbFacewear.SelectedValue = _unit.Facewear;
            cmbArmwear.SelectedValue = _unit.Armwear;
            cmbUnderwear.SelectedValue = _unit.Underwear;
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
            _unit.Headwear = (Enums.Accessory)cmbHeadwear.SelectedValue;
        }

        private void ChangeFacewear(object sender, EventArgs e)
        {
            _unit.Facewear = (Enums.Accessory)cmbFacewear.SelectedValue;
        }

        private void ChangeArmwear(object sender, EventArgs e)
        {
            _unit.Armwear = (Enums.Accessory)cmbArmwear.SelectedValue;
        }

        private void ChangeUnderwear(object sender, EventArgs e)
        {
            _unit.Underwear = (Enums.Accessory)cmbUnderwear.SelectedValue;
        }
    }
}
