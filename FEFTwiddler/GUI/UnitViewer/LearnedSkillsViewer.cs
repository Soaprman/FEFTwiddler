using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FEFTwiddler.GUI.UnitViewer
{
    public partial class LearnedSkillsViewer : Form
    {
        private Model.Character _character;

        public LearnedSkillsViewer(Model.Character character)
        {
            _character = character;
            InitializeComponent();
        }

        private void LearnedSkills_Load(object sender, EventArgs e)
        {
            this.Text = "Learned Skills for " + Data.Database.Characters.GetByID(_character.CharacterID).DisplayName;

            var allSkills = Data.Database.Skills.GetAll().Where((x) => !x.IsPersonal && !x.IsUnlearnable && x.SkillID != Enums.Skill.None);
            foreach (var skill in allSkills)
            {
                if (_character.LearnedSkills.Contains(skill.SkillID))
                {
                    var pict = new PictureBox();
                    pict.Size = new Size(24, 24);
                    pict.Image = GetSkillImage(skill.SkillID);
                    toolTip1.SetToolTip(pict, skill.DisplayName);
                    flwSkills.Controls.Add(pict);
                }
            }
        }

        private Bitmap GetSkillImage(Enums.Skill skillId)
        {
            var img = Properties.Resources.ResourceManager.GetObject(
                        String.Format("Skill_{0:000}", (byte)skillId));

            return (Bitmap)img;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
