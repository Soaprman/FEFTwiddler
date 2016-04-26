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

namespace FEFTwiddler.GUI.GlobalData
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class GlobalDataMain : UserControl
    {
        private Model.GlobalSave _globalSave;

        public GlobalDataMain()
        {
            InitializeComponent();
            InitializeControls();
        }

        public void LoadGlobalSave(Model.GlobalSave globalSave)
        {
            _globalSave = globalSave;
            PopulateControls();
        }

        private void InitializeControls()
        {
        }

        private void PopulateControls()
        {
        }

        private void btnUnlockSupportLog_Click(object sender, EventArgs e)
        {
            Model.Cheats.UnlockSupportLog(_globalSave);
            MessageBox.Show("Done!");
        }

        private void btnHairColors_Click(object sender, EventArgs e)
        {
            var popup = new HairColors(_globalSave);
            popup.ShowDialog();
        }
    }
}
