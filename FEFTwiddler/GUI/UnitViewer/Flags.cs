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

            chkEinherjar.CheckedChanged += HandleEinherjarChecked;
            chkRecruited.CheckedChanged += HandleRecruitedChecked;
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

        public void HandleEinherjarChecked(object sender, EventArgs e)
        {
            var chk = (CheckBox)sender;
            UpdateEinherjar(chk.Checked);
        }

        private void UpdateEinherjar(bool isEinherjar)
        {
            _character.IsEinherjar = isEinherjar;
        }

        public void HandleRecruitedChecked(object sender, EventArgs e)
        {
            var chk = (CheckBox)sender;
            UpdateRecruited(chk.Checked);
        }

        private void UpdateRecruited(bool isRecruited)
        {
            _character.IsRecruited = isRecruited;
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
