using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace FEFTwiddler.GUI.UnitViewer
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class Flags : UserControl
    {
        private ToolTip _tooltip = new ToolTip();
        private Model.Unit _unit;

        public Flags()
        {
            InitializeComponent();
            InitializeControls();
        }

        public void LoadCharacter(Model.Unit unit)
        {
            _unit = unit;
            PopulateControls();
        }

        private void InitializeControls()
        {
            _tooltip.SetToolTip(chkEinherjar, "I don't know entirely what side effects changing this has. Click at your own risk!");
            _tooltip.SetToolTip(chkRecruited, "I don't know entirely what side effects changing this has. Click at your own risk!");

            chkEinherjar.CheckedChanged += HandleEinherjarChecked;
            chkRecruited.CheckedChanged += HandleRecruitedChecked;
        }

        private void PopulateControls()
        {
            chkEinherjar.Checked = _unit.IsEinherjar;
            chkRecruited.Checked = _unit.IsRecruited;
        }

        public void HandleEinherjarChecked(object sender, EventArgs e)
        {
            var chk = (CheckBox)sender;
            UpdateEinherjar(chk.Checked);
        }

        private void UpdateEinherjar(bool isEinherjar)
        {
            _unit.IsEinherjar = isEinherjar;
        }

        public void HandleRecruitedChecked(object sender, EventArgs e)
        {
            var chk = (CheckBox)sender;
            UpdateRecruited(chk.Checked);
        }

        private void UpdateRecruited(bool isRecruited)
        {
            _unit.IsRecruited = isRecruited;
        }
    }
}
