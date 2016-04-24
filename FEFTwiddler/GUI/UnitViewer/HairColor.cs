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
            //HairColorR.Text = String.Format("{0:X2}{1:X2}{2:X2}",
            //    _character.HairColor[0],
            //    _character.HairColor[1],
            //    _character.HairColor[2]);
            //HairColorG.Text = "0x " + HairColorR.Text + "FF";
            //HairColorB.Text = HairColorHex.Text + "FF";
            //haircolorg.text = string.format("0:x2",
            //    _character.haircolor[1]);
            //haircolorb.text = string.format("0:x2",
            //    _character.haircolor[2]);


            //HairColorR.Text = String.Format("{0:X2}",
            //                                    _character.HairColor[0]);
            //HairColorG.Text = String.Format("{1:X2}",
            //                                    _character.HairColor[1]);
            //HairColorB.Text = String.Format("{2:X2}",
            //                                    _character.HairColor[2]);
        }

        private void UpdateHairColor(object sender, EventArgs e)
        {
            Byte[] NewHairColor = new byte[4];
            if (NewHairColor.TryParseHex(HairColorHex.Text + "FF"))
            //if (NewHairColor.TryParseHex(HairColorHex.Text+ "FF"))
            {
                HairColorBox.BackColor = Color.FromArgb(NewHairColor[3],
                                                     NewHairColor[0],
                                                     NewHairColor[1],
                                                     NewHairColor[2]);
                _character.HairColor = NewHairColor;
            }
        }

        private void HairColor_Load(object sender, EventArgs e)
        {

        }

        private void HairColorHex_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void HairColorBox_Click(object sender, EventArgs e)
        {

        }

       
    }
}
