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
    public partial class Class : UserControl
    {
        private Model.Character _character;

        public Class()
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
            cmbClass.DisplayMember = "DisplayName";
            cmbClass.ValueMember = "ClassID";
            cmbClass.DataSource = Data.Database.Classes.GetAll();
        }

        private void PopulateControls()
        {
            if (Enum.IsDefined(typeof(Enums.Class), _character.ClassID))
                cmbClass.SelectedValue = _character.ClassID;
            else
                cmbClass.Text = _character.ClassID.ToString();
        }
    }
}
