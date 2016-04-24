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
        private Model.Character _character;

        public HairColor()
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
            HairColorBox.BackColor = Color.FromArgb(_character.HairColor[3],
                                      _character.HairColor[0],
                                      _character.HairColor[1],
                                      _character.HairColor[2]);
            HairColorHex.Text = String.Format("{0:X2}{1:X2}{2:X2}",
                                                _character.HairColor[0],
                                                _character.HairColor[1],
                                                _character.HairColor[2]);
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
                _character.HairColor = NewHairColor;
                if (_character.AvatarHairColor != null) _character.AvatarHairColor = NewHairColor;
            }
        }
    }
}
