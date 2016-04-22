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
        private Model.Character _character;

        public DragonVein()
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
            if (Enum.IsDefined(typeof(Enums.Character), _character.CharacterID))
            {
                var isDefault = Data.Database.Characters.GetByID(_character.CharacterID).CanUseDragonVein;
                chkDragonVein.Checked = _character.CanUseDragonVein || isDefault;
                chkDragonVein.Enabled = !isDefault;
            }
            else
            {
                chkDragonVein.Checked = _character.CanUseDragonVein;
            }
        }

        private void chkDragonVein_CheckedChanged(object sender, EventArgs e)
        {
            // If a character can't use Dragon Vein by default
            if (chkDragonVein.Checked && !Data.Database.Characters.GetByID(_character.CharacterID).CanUseDragonVein)
                _character.CanUseDragonVein = true;
            else
                _character.CanUseDragonVein = false;
        }
    }
}
