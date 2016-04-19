using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FEFTwiddler.Extensions;

namespace FEFTwiddler.GUI.UnitViewer
{
    public partial class LearnedSkillsViewer : Form
    {
        private Model.Character _character;
        private Model.LearnedSkills _tempLearnedSkills;

        public LearnedSkillsViewer(Model.Character character)
        {
            _character = character;
            _tempLearnedSkills = new Model.LearnedSkills(_character.LearnedSkills.Raw.ToArray());
            InitializeComponent();
        }

        private void LearnedSkills_Load(object sender, EventArgs e)
        {
            this.Text = "Learned Skills for " + Data.Database.Characters.GetByID(_character.CharacterID).DisplayName;

            var allSkills = Data.Database.Skills.GetAllLearnable().Where((x) => x.SkillID != Enums.Skill.None).OrderBy((x) => x.DisplayName);
            IEnumerable<Data.Skill> someSkills;

            someSkills = allSkills.Where((x) => x.IsNormalClassSkill);
            foreach (var skill in someSkills)
            {
                var icon = new SkillIcon(_tempLearnedSkills, skill);
                toolTip1.SetToolTip(icon, skill.DisplayName);
                flwNormalClassSkills.Controls.Add(icon);
            }

            someSkills = allSkills.Where((x) => x.IsCorrinOnlySkill);
            foreach (var skill in someSkills)
            {
                var icon = new SkillIcon(_tempLearnedSkills, skill);
                toolTip1.SetToolTip(icon, skill.DisplayName);
                flwCorrinOnlySkills.Controls.Add(icon);
            }

            someSkills = allSkills.Where((x) => x.IsAzuraOnlySkill);
            foreach (var skill in someSkills)
            {
                var icon = new SkillIcon(_tempLearnedSkills, skill);
                toolTip1.SetToolTip(icon, skill.DisplayName);
                flwAzuraOnlySkills.Controls.Add(icon);
            }

            someSkills = allSkills.Where((x) => x.IsKitsuneOnlySkill && x.IsWolfskinOnlySkill);
            foreach (var skill in someSkills)
            {
                var icon = new SkillIcon(_tempLearnedSkills, skill);
                toolTip1.SetToolTip(icon, skill.DisplayName);
                flwBeastOnlySkills.Controls.Add(icon);
            }

            someSkills = allSkills.Where((x) => x.IsKitsuneOnlySkill && !x.IsWolfskinOnlySkill);
            foreach (var skill in someSkills)
            {
                var icon = new SkillIcon(_tempLearnedSkills, skill);
                toolTip1.SetToolTip(icon, skill.DisplayName);
                flwKitsuneOnlySkills.Controls.Add(icon);
            }

            someSkills = allSkills.Where((x) => x.IsWolfskinOnlySkill && !x.IsKitsuneOnlySkill);
            foreach (var skill in someSkills)
            {
                var icon = new SkillIcon(_tempLearnedSkills, skill);
                toolTip1.SetToolTip(icon, skill.DisplayName);
                flwWolfskinOnlySkills.Controls.Add(icon);
            }

            someSkills = allSkills.Where((x) => x.IsVillagerOnlySkill);
            foreach (var skill in someSkills)
            {
                var icon = new SkillIcon(_tempLearnedSkills, skill);
                toolTip1.SetToolTip(icon, skill.DisplayName);
                flwVillagerOnlySkills.Controls.Add(icon);
            }

            someSkills = allSkills.Where((x) => x.IsPathBonusClassSkill);
            foreach (var skill in someSkills)
            {
                var icon = new SkillIcon(_tempLearnedSkills, skill);
                toolTip1.SetToolTip(icon, skill.DisplayName);
                flwPathBonusClassSkills.Controls.Add(icon);
            }

            someSkills = allSkills.Where((x) => x.IsDlcClassSkill && !x.IsAmiiboClassSkill);
            foreach (var skill in someSkills)
            {
                var icon = new SkillIcon(_tempLearnedSkills, skill);
                toolTip1.SetToolTip(icon, skill.DisplayName);
                flwDlcClassSkills.Controls.Add(icon);
            }

            someSkills = allSkills.Where((x) => x.IsAmiiboClassSkill);
            foreach (var skill in someSkills)
            {
                var icon = new SkillIcon(_tempLearnedSkills, skill);
                toolTip1.SetToolTip(icon, skill.DisplayName);
                flwAmiiboClassSkills.Controls.Add(icon);
            }

            someSkills = allSkills.Where((x) => x.IsEnemyAndNpcSkill);
            foreach (var skill in someSkills)
            {
                var icon = new SkillIcon(_tempLearnedSkills, skill);
                toolTip1.SetToolTip(icon, skill.DisplayName);
                flwEnemyAndNpcSkills.Controls.Add(icon);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _character.LearnedSkills = _tempLearnedSkills;
            this.Close();
        }

        private void btnUnlearnNormalClassSkills_Click(object sender, EventArgs e)
        {
            foreach (SkillIcon skillIcon in flwNormalClassSkills.Controls)
            {
                skillIcon.Disable();
            }
        }

        private void btnLearnNormalClassSkills_Click(object sender, EventArgs e)
        {
            foreach (SkillIcon skillIcon in flwNormalClassSkills.Controls)
            {
                skillIcon.Enable();
            }
        }

        private void btnUnlearnCorrinOnlySkills_Click(object sender, EventArgs e)
        {
            foreach (SkillIcon skillIcon in flwCorrinOnlySkills.Controls)
            {
                skillIcon.Disable();
            }
        }

        private void btnLearnCorrinOnlySkills_Click(object sender, EventArgs e)
        {
            foreach (SkillIcon skillIcon in flwCorrinOnlySkills.Controls)
            {
                skillIcon.Enable();
            }
        }

        private void btnUnlearnAzuraOnlySkills_Click(object sender, EventArgs e)
        {
            foreach (SkillIcon skillIcon in flwAzuraOnlySkills.Controls)
            {
                skillIcon.Disable();
            }
        }

        private void btnLearnAzuraOnlySkills_Click(object sender, EventArgs e)
        {
            foreach (SkillIcon skillIcon in flwAzuraOnlySkills.Controls)
            {
                skillIcon.Enable();
            }
        }

        private void btnUnlearnBeastOnlySkills_Click(object sender, EventArgs e)
        {
            foreach (SkillIcon skillIcon in flwBeastOnlySkills.Controls)
            {
                skillIcon.Disable();
            }
        }

        private void btnLearnBeastOnlySkills_Click(object sender, EventArgs e)
        {
            foreach (SkillIcon skillIcon in flwBeastOnlySkills.Controls)
            {
                skillIcon.Enable();
            }
        }

        private void btnUnlearnKitsuneOnlySkills_Click(object sender, EventArgs e)
        {
            foreach (SkillIcon skillIcon in flwKitsuneOnlySkills.Controls)
            {
                skillIcon.Disable();
            }
        }

        private void btnLearnKitsuneOnlySkills_Click(object sender, EventArgs e)
        {
            foreach (SkillIcon skillIcon in flwKitsuneOnlySkills.Controls)
            {
                skillIcon.Enable();
            }
        }

        private void btnUnlearnWolfskinOnlySkills_Click(object sender, EventArgs e)
        {
            foreach (SkillIcon skillIcon in flwWolfskinOnlySkills.Controls)
            {
                skillIcon.Disable();
            }
        }

        private void btnLearnWolfskinOnlySkills_Click(object sender, EventArgs e)
        {
            foreach (SkillIcon skillIcon in flwWolfskinOnlySkills.Controls)
            {
                skillIcon.Enable();
            }
        }

        private void btnUnlearnVillagerOnlySkills_Click(object sender, EventArgs e)
        {
            foreach (SkillIcon skillIcon in flwVillagerOnlySkills.Controls)
            {
                skillIcon.Disable();
            }
        }

        private void btnLearnVillagerOnlySkills_Click(object sender, EventArgs e)
        {
            foreach (SkillIcon skillIcon in flwVillagerOnlySkills.Controls)
            {
                skillIcon.Enable();
            }
        }

        private void btnUnlearnPathBonusClassSkills_Click(object sender, EventArgs e)
        {
            foreach (SkillIcon skillIcon in flwPathBonusClassSkills.Controls)
            {
                skillIcon.Disable();
            }
        }

        private void btnLearnPathBonusClassSkills_Click(object sender, EventArgs e)
        {
            foreach (SkillIcon skillIcon in flwPathBonusClassSkills.Controls)
            {
                skillIcon.Enable();
            }
        }

        private void btnUnlearnDlcClassSkills_Click(object sender, EventArgs e)
        {
            foreach (SkillIcon skillIcon in flwDlcClassSkills.Controls)
            {
                skillIcon.Disable();
            }
        }

        private void btnLearnDlcClassSkills_Click(object sender, EventArgs e)
        {
            foreach (SkillIcon skillIcon in flwDlcClassSkills.Controls)
            {
                skillIcon.Enable();
            }
        }

        private void btnUnlearnAmiiboClassSkills_Click(object sender, EventArgs e)
        {
            foreach (SkillIcon skillIcon in flwAmiiboClassSkills.Controls)
            {
                skillIcon.Disable();
            }
        }

        private void btnLearnAmiiboClassSkills_Click(object sender, EventArgs e)
        {
            foreach (SkillIcon skillIcon in flwAmiiboClassSkills.Controls)
            {
                skillIcon.Enable();
            }
        }

        private void btnUnlearnEnemyAndNpcSkills_Click(object sender, EventArgs e)
        {
            foreach (SkillIcon skillIcon in flwEnemyAndNpcSkills.Controls)
            {
                skillIcon.Disable();
            }
        }

        private void btnLearnEnemyAndNpcSkills_Click(object sender, EventArgs e)
        {
            foreach (SkillIcon skillIcon in flwEnemyAndNpcSkills.Controls)
            {
                skillIcon.Enable();
            }
        }
    }

    public class SkillIcon : PictureBox
    {
        private Model.LearnedSkills _tempLearnedSkills;
        private Data.Skill _skill;
        private bool _enabled;

        public SkillIcon(Model.LearnedSkills tempLearnedSkills, Data.Skill skill)
        {
            _tempLearnedSkills = tempLearnedSkills;
            _skill = skill;

            this.Image = GetSkillImage();
            // Set the reverse, then toggle to update the image as well
            _enabled = !_tempLearnedSkills.Contains(skill.SkillID);

            ApplyStyle();
            BindEvents();
            Toggle();
        }

        private void ApplyStyle()
        {
            this.Size = new Size(24, 24);
            this.Cursor = Cursors.Hand;
        }

        private void BindEvents()
        {
            this.Click += HandleClick;
        }

        public void HandleClick(object sender, EventArgs e)
        {
            Toggle();
        }

        public void Toggle()
        {
            if (_enabled) Disable();
            else Enable();
        }

        public void Enable()
        {
            _enabled = true;
            _tempLearnedSkills.Add(_skill.SkillID);
            var bmp = (Bitmap)(this.Image);
            bmp.SetOpacity(255, 126);
            this.Image = bmp;
        }

        public void Disable()
        {
            _enabled = false;
            _tempLearnedSkills.Remove(_skill.SkillID);
            var bmp = (Bitmap)(this.Image);
            bmp.SetOpacity(127, 126);
            this.Image = bmp;
        }

        private Bitmap GetSkillImage()
        {
            var img = Properties.Resources.ResourceManager.GetObject(
                        String.Format("Skill_{0:000}", (byte)_skill.SkillID));

            return (Bitmap)img;
        }
    }
}
