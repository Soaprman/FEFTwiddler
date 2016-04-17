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
    public partial class Flags : UserControl
    {
        private Model.Character _character;

        public Flags()
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
            cmbDeathChapter.DataSource = Enum.GetValues(typeof(Enums.Chapter));
        }

        private void PopulateControls()
        {
            chkDeployed.Checked = _character.IsDeployed;
            chkDead.Checked = _character.IsDead;
            chkEinherjar.Checked = _character.IsEinherjar;
            chkRecruited.Checked = _character.IsRecruited;
            chkAbsent.Checked = _character.IsAbsent;

            cmbDeathChapter.Text = _character.DeathChapter.ToString();
        }

        private void chkDead_CheckedChanged(object sender, EventArgs e)
        {
            var chk = (CheckBox)sender;
            if (chk.Checked)
            {
                // Kill
            }
            else
            {
                // Revive
            }
        }
    }
}
