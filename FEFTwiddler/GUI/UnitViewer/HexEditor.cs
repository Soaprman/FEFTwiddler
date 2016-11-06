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
        private Model.Unit _unit;

        public HexEditor(Model.Unit unit)
        {
            _unit = unit;
            InitializeComponent();
            SetTitle();
        }

        private void SetTitle()
        {
            this.Text = "Hex editing: " + _unit.GetDisplayName();
        }

        private void HexEditor_Load(object sender, EventArgs e)
        {
            hexRawBlock1.SetBytes(_unit.RawBlock1);
            hexRawInventory.SetBytes(_unit.RawInventory);
            hexRawSupports.SetBytes(_unit.RawSupports);
            lblRawNumberOfSupports.Text = string.Format("Count: {0:X2}", _unit.RawNumberOfSupports);
            hexRawBlock2.SetBytes(_unit.RawBlock2);
            hexRawLearnedSkills.SetBytes(_unit.RawLearnedSkills);
            hexRawDeployedUnitInfo.SetBytes(_unit.RawDeployedUnitInfo);
            hexRawBlock3.SetBytes(_unit.RawBlock3);
            lblRawEndBlockType.Text = string.Format("Type: {0:X2}", _unit.RawEndBlockType);
            hexRawEndBlock.SetBytes(_unit.RawEndBlock);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _unit.RawBlock1 = hexRawBlock1.GetBytes();
            _unit.RawInventory = hexRawInventory.GetBytes();
            _unit.RawSupports = hexRawSupports.GetBytes();
            _unit.RawBlock2 = hexRawBlock2.GetBytes();
            _unit.RawLearnedSkills = hexRawLearnedSkills.GetBytes();
            _unit.RawDeployedUnitInfo = hexRawDeployedUnitInfo.GetBytes();
            _unit.RawBlock3 = hexRawBlock3.GetBytes();
            _unit.RawEndBlock = hexRawEndBlock.GetBytes();

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
