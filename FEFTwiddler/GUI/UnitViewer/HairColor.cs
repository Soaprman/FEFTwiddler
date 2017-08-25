using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using FEFTwiddler.Extensions;

namespace FEFTwiddler.GUI.UnitViewer
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class HairColor : UserControl
    {
        private Model.Unit _unit;

        public HairColor()
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
            HairColorBox.BackColor = Color.FromArgb(_unit.HairColor[3],
                                      _unit.HairColor[0],
                                      _unit.HairColor[1],
                                      _unit.HairColor[2]);
            HairColorHex.Text = String.Format("{0:X2}{1:X2}{2:X2}",
                                                _unit.HairColor[0],
                                                _unit.HairColor[1],
                                                _unit.HairColor[2]);
        }

        private void UpdateHairColor(object sender, EventArgs e)
        {
            Byte[] NewHairColor = new byte[4];
            if (NewHairColor.TryParseHex(HairColorHex.Text + "FF"))
            {
                HairColorBox.BackColor = Color.FromArgb(NewHairColor[3],
                                                     NewHairColor[0],
                                                     NewHairColor[1],
                                                     NewHairColor[2]);
                _unit.HairColor = NewHairColor;
                if (_unit.AvatarHairColor != null) _unit.AvatarHairColor = NewHairColor;
            }
        }

        private void PickHairColor(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK) {
                HairColorHex.Text = colorDialog.Color.R.ToString("X2") + colorDialog.Color.G.ToString("X2") + colorDialog.Color.B.ToString("X2");
                UpdateHairColor(null, null);
            }
        }
    }
}
