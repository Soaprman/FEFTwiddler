using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FEFTwiddler.GUI.UnitViewer
{
    public partial class HexEditor : Form
    {
        private Model.Character _character;

        public HexEditor(Model.Character character)
        {
            _character = character;
            InitializeComponent();
            SetTitle();
        }

        private void SetTitle()
        {
            if (Enum.IsDefined(typeof(Enums.Character), _character.CharacterID))
            {
                if (_character.CorrinName == null)
                    this.Text = "Hex editing: " + Data.Database.Characters.GetByID(_character.CharacterID).DisplayName;
                else
                    this.Text = "Hex editing: " + _character.CorrinName;
            }
            else
            {
                this.Text = "Hex editing: Some uknown character";
            }
        }

        private void HexEditor_Load(object sender, EventArgs e)
        {
            hexRawBlock1.SetBytes(_character.RawBlock1);
            hexRawInventory.SetBytes(_character.RawInventory);
            hexRawSupports.SetBytes(_character.RawSupports);
            lblRawNumberOfSupports.Text = string.Format("Count: {0:X2}", _character.RawNumberOfSupports);
            hexRawBlock2.SetBytes(_character.RawBlock2);
            hexRawLearnedSkills.SetBytes(_character.RawLearnedSkills);
            hexRawDeployedUnitInfo.SetBytes(_character.RawDeployedUnitInfo);
            hexRawBlock3.SetBytes(_character.RawBlock3);
            lblRawEndBlockType.Text = string.Format("Type: {0:X2}", _character.RawEndBlockType);
            hexRawEndBlock.SetBytes(_character.RawEndBlock);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _character.RawBlock1 = hexRawBlock1.GetBytes();
            _character.RawInventory = hexRawInventory.GetBytes();
            _character.RawSupports = hexRawSupports.GetBytes();
            _character.RawBlock2 = hexRawBlock2.GetBytes();
            _character.RawLearnedSkills = hexRawLearnedSkills.GetBytes();
            _character.RawDeployedUnitInfo = hexRawDeployedUnitInfo.GetBytes();
            _character.RawBlock3 = hexRawBlock3.GetBytes();
            _character.RawEndBlock = hexRawEndBlock.GetBytes();

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
