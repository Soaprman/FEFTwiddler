using System;
using System.Drawing;
using System.Windows.Forms;

namespace FEFTwiddler.GUI.GlobalData
{
    public partial class HairColors : Form
    {
        private Model.GlobalSave _globalSave;

        public HairColors(Model.GlobalSave globalSave)
        {
            _globalSave = globalSave;
            InitializeComponent();
            PopulateControls();
        }

        private void PopulateControls()
        {
            colorCorrinM.Color = _globalSave.Region1.HairColor_CorrinM;
            colorCorrinF.Color = _globalSave.Region1.HairColor_CorrinF;
            colorKanaM.Color = _globalSave.Region1.HairColor_KanaM;
            colorKanaF.Color = _globalSave.Region1.HairColor_KanaF;
            colorShigure.Color = _globalSave.Region1.HairColor_Shigure;
            colorDwyer.Color = _globalSave.Region1.HairColor_Dwyer;
            colorSophie.Color = _globalSave.Region1.HairColor_Sophie;
            colorMidori.Color = _globalSave.Region1.HairColor_Midori;
            colorShiro.Color = _globalSave.Region1.HairColor_Shiro;
            colorKiragi.Color = _globalSave.Region1.HairColor_Kiragi;
            colorAsugi.Color = _globalSave.Region1.HairColor_Asugi;
            colorSelkie.Color = _globalSave.Region1.HairColor_Selkie;
            colorHisame.Color = _globalSave.Region1.HairColor_Hisame;
            colorMitama.Color = _globalSave.Region1.HairColor_Mitama;
            colorCaeldori.Color = _globalSave.Region1.HairColor_Caeldori;
            colorRhajat.Color = _globalSave.Region1.HairColor_Rhajat;
            colorSiegbert.Color = _globalSave.Region1.HairColor_Siegbert;
            colorForrest.Color = _globalSave.Region1.HairColor_Forrest;
            colorIgnatius.Color = _globalSave.Region1.HairColor_Ignatius;
            colorVelouria.Color = _globalSave.Region1.HairColor_Velouria;
            colorPercy.Color = _globalSave.Region1.HairColor_Percy;
            colorOphelia.Color = _globalSave.Region1.HairColor_Ophelia;
            colorSoleil.Color = _globalSave.Region1.HairColor_Soleil;
            colorNina.Color = _globalSave.Region1.HairColor_Nina;

            DisableMissingColors();
        }

        /// <summary>
        /// Disables every picker after the last picker with a valid value
        /// </summary>
        /// <remarks>What a lovely hack</remarks>
        private void DisableMissingColors()
        {
            var noColor = Color.FromArgb(1, 0, 0, 0);
            if (colorNina.Color.Equals(noColor)) colorNina.Enabled = false; else return;
            if (colorSoleil.Color.Equals(noColor)) colorSoleil.Enabled = false; else return;
            if (colorOphelia.Color.Equals(noColor)) colorOphelia.Enabled = false; else return;
            if (colorPercy.Color.Equals(noColor)) colorPercy.Enabled = false; else return;
            if (colorVelouria.Color.Equals(noColor)) colorVelouria.Enabled = false; else return;
            if (colorIgnatius.Color.Equals(noColor)) colorIgnatius.Enabled = false; else return;
            if (colorForrest.Color.Equals(noColor)) colorForrest.Enabled = false; else return;
            if (colorSiegbert.Color.Equals(noColor)) colorSiegbert.Enabled = false; else return;
            if (colorRhajat.Color.Equals(noColor)) colorRhajat.Enabled = false; else return;
            if (colorCaeldori.Color.Equals(noColor)) colorCaeldori.Enabled = false; else return;
            if (colorMitama.Color.Equals(noColor)) colorMitama.Enabled = false; else return;
            if (colorHisame.Color.Equals(noColor)) colorHisame.Enabled = false; else return;
            if (colorSelkie.Color.Equals(noColor)) colorSelkie.Enabled = false; else return;
            if (colorAsugi.Color.Equals(noColor)) colorAsugi.Enabled = false; else return;
            if (colorKiragi.Color.Equals(noColor)) colorKiragi.Enabled = false; else return;
            if (colorShiro.Color.Equals(noColor)) colorShiro.Enabled = false; else return;
            if (colorMidori.Color.Equals(noColor)) colorMidori.Enabled = false; else return;
            if (colorSophie.Color.Equals(noColor)) colorSophie.Enabled = false; else return;
            if (colorDwyer.Color.Equals(noColor)) colorDwyer.Enabled = false; else return;
            if (colorShigure.Color.Equals(noColor)) colorShigure.Enabled = false; else return;
            if (colorKanaF.Color.Equals(noColor)) colorKanaF.Enabled = false; else return;
            if (colorKanaM.Color.Equals(noColor)) colorKanaM.Enabled = false; else return;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            _globalSave.Region1.HairColor_CorrinM = colorCorrinM.Color;
            _globalSave.Region1.HairColor_CorrinF = colorCorrinF.Color;
            _globalSave.Region1.HairColor_KanaM = colorKanaM.Color;
            _globalSave.Region1.HairColor_KanaF = colorKanaF.Color;
            _globalSave.Region1.HairColor_Shigure = colorShigure.Color;
            _globalSave.Region1.HairColor_Dwyer = colorDwyer.Color;
            _globalSave.Region1.HairColor_Sophie = colorSophie.Color;
            _globalSave.Region1.HairColor_Midori = colorMidori.Color;
            _globalSave.Region1.HairColor_Shiro = colorShiro.Color;
            _globalSave.Region1.HairColor_Kiragi = colorKiragi.Color;
            _globalSave.Region1.HairColor_Asugi = colorAsugi.Color;
            _globalSave.Region1.HairColor_Selkie = colorSelkie.Color;
            _globalSave.Region1.HairColor_Hisame = colorHisame.Color;
            _globalSave.Region1.HairColor_Mitama = colorMitama.Color;
            _globalSave.Region1.HairColor_Caeldori = colorCaeldori.Color;
            _globalSave.Region1.HairColor_Rhajat = colorRhajat.Color;
            _globalSave.Region1.HairColor_Siegbert = colorSiegbert.Color;
            _globalSave.Region1.HairColor_Forrest = colorForrest.Color;
            _globalSave.Region1.HairColor_Ignatius = colorIgnatius.Color;
            _globalSave.Region1.HairColor_Velouria = colorVelouria.Color;
            _globalSave.Region1.HairColor_Percy = colorPercy.Color;
            _globalSave.Region1.HairColor_Ophelia = colorOphelia.Color;
            _globalSave.Region1.HairColor_Soleil = colorSoleil.Color;
            _globalSave.Region1.HairColor_Nina = colorNina.Color;

            this.Close();
        }
    }
}
