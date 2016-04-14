using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace FEFTwiddler.GUI.UnitViewer
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class EquippedSkills : UserControl
    {
        private Model.Character _character;

        public EquippedSkills()
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
            chkIncludeEnemyOnlySkills.Checked = false;

            cmbSkill1.DisplayMember = "DisplayName";
            cmbSkill1.ValueMember = "SkillID";
            cmbSkill1.DataSource = GetSkillDataSource();

            cmbSkill2.DisplayMember = "DisplayName";
            cmbSkill2.ValueMember = "SkillID";
            cmbSkill2.DataSource = GetSkillDataSource();

            cmbSkill3.DisplayMember = "DisplayName";
            cmbSkill3.ValueMember = "SkillID";
            cmbSkill3.DataSource = GetSkillDataSource();

            cmbSkill4.DisplayMember = "DisplayName";
            cmbSkill4.ValueMember = "SkillID";
            cmbSkill4.DataSource = GetSkillDataSource();

            cmbSkill5.DisplayMember = "DisplayName";
            cmbSkill5.ValueMember = "SkillID";
            cmbSkill5.DataSource = GetSkillDataSource();
        }

        private void PopulateControls()
        {
            CheckEnemyOnlyBoxIfSkillIsEnemyOnly(_character.EquippedSkill_1);
            cmbSkill1.SelectedValue = _character.EquippedSkill_1;
            pictSkill1.Image = GetSkillImage(_character.EquippedSkill_1);

            CheckEnemyOnlyBoxIfSkillIsEnemyOnly(_character.EquippedSkill_2);
            cmbSkill2.SelectedValue = _character.EquippedSkill_2;
            pictSkill2.Image = GetSkillImage(_character.EquippedSkill_2);

            CheckEnemyOnlyBoxIfSkillIsEnemyOnly(_character.EquippedSkill_3);
            cmbSkill3.SelectedValue = _character.EquippedSkill_3;
            pictSkill3.Image = GetSkillImage(_character.EquippedSkill_3);

            CheckEnemyOnlyBoxIfSkillIsEnemyOnly(_character.EquippedSkill_4);
            cmbSkill4.SelectedValue = _character.EquippedSkill_4;
            pictSkill4.Image = GetSkillImage(_character.EquippedSkill_4);

            CheckEnemyOnlyBoxIfSkillIsEnemyOnly(_character.EquippedSkill_5);
            cmbSkill5.SelectedValue = _character.EquippedSkill_5;
            pictSkill5.Image = GetSkillImage(_character.EquippedSkill_5);
        }

        private void CheckEnemyOnlyBoxIfSkillIsEnemyOnly(Enums.Skill skillId)
        {
            if (chkIncludeEnemyOnlySkills.Checked) return;
            if (Data.Database.Skills.GetByID(skillId).IsEnemyOnly)
            {
                chkIncludeEnemyOnlySkills.Checked = true;
            }
        }

        private IEnumerable<Data.Skill> GetSkillDataSource()
        {
            return Data.Database.Skills.GetAll()
                .Where((x) => !x.IsUnlearnable && !x.IsPersonal && !x.IsEnemyOnly)
                .OrderBy((x) => x.DisplayName)
                .ToList();
        }

        private IEnumerable<Data.Skill> GetSkillDataSourcePlusEnemyOnly()
        {
            return Data.Database.Skills.GetAll()
                .Where((x) => !x.IsUnlearnable && !x.IsPersonal)
                .OrderBy((x) => x.DisplayName)
                .ToList();
        }

        private Bitmap GetSkillImage(Enums.Skill skillId)
        {
            var img = Properties.Resources.ResourceManager.GetObject(
                        String.Format("Skill_{0:D3}", (byte)skillId));

            return (Bitmap)img;
        }

        private void cmbSkill1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_character == null) return;
            var cmb = (ComboBox)sender;

            Enums.Skill val = (cmb.SelectedValue == null ? Enums.Skill.None : (Enums.Skill)cmb.SelectedValue);
            _character.EquippedSkill_1 = val;
            pictSkill1.Image = GetSkillImage(_character.EquippedSkill_1);
        }

        private void cmbSkill2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_character == null) return;
            var cmb = (ComboBox)sender;

            Enums.Skill val = (cmb.SelectedValue == null ? Enums.Skill.None : (Enums.Skill)cmb.SelectedValue);
            _character.EquippedSkill_2 = val;
            pictSkill2.Image = GetSkillImage(_character.EquippedSkill_2);
        }

        private void cmbSkill3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_character == null) return;
            var cmb = (ComboBox)sender;

            Enums.Skill val = (cmb.SelectedValue == null ? Enums.Skill.None : (Enums.Skill)cmb.SelectedValue);
            _character.EquippedSkill_3 = val;
            pictSkill3.Image = GetSkillImage(_character.EquippedSkill_3);
        }

        private void cmbSkill4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_character == null) return;
            var cmb = (ComboBox)sender;

            Enums.Skill val = (cmb.SelectedValue == null ? Enums.Skill.None : (Enums.Skill)cmb.SelectedValue);
            _character.EquippedSkill_4 = val;
            pictSkill4.Image = GetSkillImage(_character.EquippedSkill_4);
        }

        private void cmbSkill5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_character == null) return;
            var cmb = (ComboBox)sender;

            Enums.Skill val = (cmb.SelectedValue == null ? Enums.Skill.None : (Enums.Skill)cmb.SelectedValue);
            _character.EquippedSkill_5 = val;
            pictSkill5.Image = GetSkillImage(_character.EquippedSkill_5);
        }

        private void chkIncludeEnemyOnlySkills_CheckedChanged(object sender, EventArgs e)
        {
            var chk = (CheckBox)sender; Func<IEnumerable<Data.Skill>> populate;

            if (chk.Checked) populate = GetSkillDataSourcePlusEnemyOnly;
            else populate = GetSkillDataSource;

            object skill1; object skill2; object skill3; object skill4; object skill5;

            skill1 = cmbSkill1.SelectedValue;
            cmbSkill1.DataSource = populate();
            cmbSkill1.SelectedValue = skill1;
            if (cmbSkill1.SelectedValue == null) cmbSkill1.SelectedValue = Enums.Skill.None;

            skill2 = cmbSkill2.SelectedValue;
            cmbSkill2.DataSource = populate();
            cmbSkill2.SelectedValue = skill2;
            if (cmbSkill2.SelectedValue == null) cmbSkill2.SelectedValue = Enums.Skill.None;

            skill3 = cmbSkill3.SelectedValue;
            cmbSkill3.DataSource = populate();
            cmbSkill3.SelectedValue = skill3;
            if (cmbSkill3.SelectedValue == null) cmbSkill3.SelectedValue = Enums.Skill.None;

            skill4 = cmbSkill4.SelectedValue;
            cmbSkill4.DataSource = populate();
            cmbSkill4.SelectedValue = skill4;
            if (cmbSkill4.SelectedValue == null) cmbSkill4.SelectedValue = Enums.Skill.None;

            skill5 = cmbSkill5.SelectedValue;
            cmbSkill5.DataSource = populate();
            cmbSkill5.SelectedValue = skill5;
            if (cmbSkill5.SelectedValue == null) cmbSkill5.SelectedValue = Enums.Skill.None;
        }

        private void cmbSkill1_Leave(object sender, EventArgs e)
        {
            if (_character == null) return;
            var cmb = (ComboBox)sender;

            Enums.Skill val = (cmb.SelectedValue == null ? Enums.Skill.None : (Enums.Skill)cmb.SelectedValue);
            if (val == Enums.Skill.None)
            {
                cmbSkill1.SelectedValue = cmbSkill2.SelectedValue;
                cmbSkill2.SelectedValue = cmbSkill3.SelectedValue;
                cmbSkill3.SelectedValue = cmbSkill4.SelectedValue;
                cmbSkill4.SelectedValue = cmbSkill5.SelectedValue;
                cmbSkill5.SelectedValue = Enums.Skill.None;
            }
            else
            {
                _character.LearnedSkills.Add(val);
            }
            UpdateEquippedSkills();
        }

        private void cmbSkill2_Leave(object sender, EventArgs e)
        {
            if (_character == null) return;
            var cmb = (ComboBox)sender;

            Enums.Skill val = (cmb.SelectedValue == null ? Enums.Skill.None : (Enums.Skill)cmb.SelectedValue);
            if (val == Enums.Skill.None)
            {
                cmbSkill2.SelectedValue = cmbSkill3.SelectedValue;
                cmbSkill3.SelectedValue = cmbSkill4.SelectedValue;
                cmbSkill4.SelectedValue = cmbSkill5.SelectedValue;
                cmbSkill5.SelectedValue = Enums.Skill.None;
            }
            else
            {
                _character.LearnedSkills.Add(val);
            }
            UpdateEquippedSkills();
        }

        private void cmbSkill3_Leave(object sender, EventArgs e)
        {
            if (_character == null) return;
            var cmb = (ComboBox)sender;

            Enums.Skill val = (cmb.SelectedValue == null ? Enums.Skill.None : (Enums.Skill)cmb.SelectedValue);
            if (val == Enums.Skill.None)
            {
                cmbSkill3.SelectedValue = cmbSkill4.SelectedValue;
                cmbSkill4.SelectedValue = cmbSkill5.SelectedValue;
                cmbSkill5.SelectedValue = Enums.Skill.None;
            }
            else
            {
                _character.LearnedSkills.Add(val);
            }
            UpdateEquippedSkills();
        }

        private void cmbSkill4_Leave(object sender, EventArgs e)
        {
            if (_character == null) return;
            var cmb = (ComboBox)sender;

            Enums.Skill val = (cmb.SelectedValue == null ? Enums.Skill.None : (Enums.Skill)cmb.SelectedValue);
            if (val == Enums.Skill.None)
            {
                cmbSkill4.SelectedValue = cmbSkill5.SelectedValue;
                cmbSkill5.SelectedValue = Enums.Skill.None;
            }
            else
            {
                _character.LearnedSkills.Add(val);
            }
            UpdateEquippedSkills();
        }

        private void cmbSkill5_Leave(object sender, EventArgs e)
        {
            if (_character == null) return;
            var cmb = (ComboBox)sender;

            Enums.Skill val = (cmb.SelectedValue == null ? Enums.Skill.None : (Enums.Skill)cmb.SelectedValue);
            if (val != Enums.Skill.None)
            {
                _character.LearnedSkills.Add(val);
            }
            UpdateEquippedSkills();
        }

        private void UpdateEquippedSkills()
        {
            _character.EquippedSkill_1 = (Enums.Skill)cmbSkill1.SelectedValue;
            _character.EquippedSkill_2 = (Enums.Skill)cmbSkill2.SelectedValue;
            _character.EquippedSkill_3 = (Enums.Skill)cmbSkill3.SelectedValue;
            _character.EquippedSkill_4 = (Enums.Skill)cmbSkill4.SelectedValue;
            _character.EquippedSkill_5 = (Enums.Skill)cmbSkill5.SelectedValue;
        }
    }
}
