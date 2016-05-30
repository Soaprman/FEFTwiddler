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

namespace FEFTwiddler.GUI.UnitViewer
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class DragonVein : UserControl
    {
        private Model.Unit _unit;

        public DragonVein()
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
            if (Enum.IsDefined(typeof(Enums.Character), _unit.CharacterID))
            {
                var isDefault = Data.Database.Characters.GetByID(_unit.CharacterID).CanUseDragonVein;
                chkDragonVein.Checked = _unit.Trait_CanUseDragonVein || isDefault;
                chkDragonVein.Enabled = !isDefault;
            }
            else
            {
                chkDragonVein.Checked = _unit.Trait_CanUseDragonVein;
            }
        }

        private void chkDragonVein_CheckedChanged(object sender, EventArgs e)
        {
            // If a character can't use Dragon Vein by default
            if (chkDragonVein.Checked && !Data.Database.Characters.GetByID(_unit.CharacterID).CanUseDragonVein)
                _unit.Trait_CanUseDragonVein = true;
            else
                _unit.Trait_CanUseDragonVein = false;
        }
    }
}
