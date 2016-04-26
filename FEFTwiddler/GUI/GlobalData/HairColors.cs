using System;
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
