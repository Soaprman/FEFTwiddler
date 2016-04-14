using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace FEFTwiddler.GUI.UnitViewer
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class LearnedSkills : UserControl
    {
        private Model.Character _character;

        public LearnedSkills()
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
        }

        private void btnAllSkills_Click(object sender, EventArgs e)
        {
            _character.LearnAllSkills();
        }

        private void btnDLCSkills_Click(object sender, EventArgs e)
        {
            _character.LearnAllSkillsDLC();
        }

        private void btnEnemySkills_Click(object sender, EventArgs e)
        {
            _character.LearnAllSkillsEnemy();
        }

        private void btnViewLearnedSkills_Click(object sender, EventArgs e)
        {
            var popup = new GUI.UnitViewer.LearnedSkillsViewer(_character);
            popup.Show();
        }
    }
}
