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
    public partial class Skills : UserControl
    {
        private Model.Character _character;

        public Skills()
        {
            InitializeComponent();
            InitializeControls();
        }

        public void LoadCharacter(Model.Character character)
        {
            if (character == null) return;
            _character = character;
            PopulateControls();
        }

        private void InitializeControls()
        {
            UnbindEvents();

            cmbSkill1.DisplayMember = "DisplayName";
            cmbSkill1.ValueMember = "SkillID";
            cmbSkill1.DataSource = GetSkillDataSource();
            cmbSkill1.SelectedValue = Enums.Skill.None;

            cmbSkill2.DisplayMember = "DisplayName";
            cmbSkill2.ValueMember = "SkillID";
            cmbSkill2.DataSource = GetSkillDataSource();
            cmbSkill2.SelectedValue = Enums.Skill.None;

            cmbSkill3.DisplayMember = "DisplayName";
            cmbSkill3.ValueMember = "SkillID";
            cmbSkill3.DataSource = GetSkillDataSource();
            cmbSkill3.SelectedValue = Enums.Skill.None;

            cmbSkill4.DisplayMember = "DisplayName";
            cmbSkill4.ValueMember = "SkillID";
            cmbSkill4.DataSource = GetSkillDataSource();
            cmbSkill4.SelectedValue = Enums.Skill.None;

            cmbSkill5.DisplayMember = "DisplayName";
            cmbSkill5.ValueMember = "SkillID";
            cmbSkill5.DataSource = GetSkillDataSource();
            cmbSkill5.SelectedValue = Enums.Skill.None;

            BindEvents();
        }

        private void PopulateControls()
        {
            // Because the DataSource now depends on a character's learned skills
            InitializeControls();

            UnbindEvents();

            cmbSkill1.SelectedValue = _character.EquippedSkill_1;
            if (cmbSkill1.Text == "") cmbSkill1.SelectedValue = Enums.Skill.None;
            UpdateSkillImage1();

            cmbSkill2.SelectedValue = _character.EquippedSkill_2;
            if (cmbSkill2.Text == "") cmbSkill2.SelectedValue = Enums.Skill.None;
            UpdateSkillImage2();

            cmbSkill3.SelectedValue = _character.EquippedSkill_3;
            if (cmbSkill3.Text == "") cmbSkill3.SelectedValue = Enums.Skill.None;
            UpdateSkillImage3();

            cmbSkill4.SelectedValue = _character.EquippedSkill_4;
            if (cmbSkill4.Text == "") cmbSkill4.SelectedValue = Enums.Skill.None;
            UpdateSkillImage4();

            cmbSkill5.SelectedValue = _character.EquippedSkill_5;
            if (cmbSkill5.Text == "") cmbSkill5.SelectedValue = Enums.Skill.None;
            UpdateSkillImage5();

            CollapseEquippedSkills();

            BindEvents();
        }

        private void UnbindEvents()
        {
            cmbSkill1.SelectedIndexChanged -= cmbSkill1_SelectedIndexChanged;
            cmbSkill2.SelectedIndexChanged -= cmbSkill2_SelectedIndexChanged;
            cmbSkill3.SelectedIndexChanged -= cmbSkill3_SelectedIndexChanged;
            cmbSkill4.SelectedIndexChanged -= cmbSkill4_SelectedIndexChanged;
            cmbSkill5.SelectedIndexChanged -= cmbSkill5_SelectedIndexChanged;
        }

        private void BindEvents()
        {
            cmbSkill1.SelectedIndexChanged += cmbSkill1_SelectedIndexChanged;
            cmbSkill2.SelectedIndexChanged += cmbSkill2_SelectedIndexChanged;
            cmbSkill3.SelectedIndexChanged += cmbSkill3_SelectedIndexChanged;
            cmbSkill4.SelectedIndexChanged += cmbSkill4_SelectedIndexChanged;
            cmbSkill5.SelectedIndexChanged += cmbSkill5_SelectedIndexChanged;
        }

        private void btnEditLearnedSkills_Click(object sender, EventArgs e)
        {
            var popup = new GUI.UnitViewer.LearnedSkillsViewer(_character);
            popup.ShowDialog();
            LoadCharacter(_character);
        }

        private IEnumerable<Data.Skill> GetSkillDataSource()
        {
            if (_character == null) return new List<Data.Skill>();

            return Data.Database.Skills.GetAllLearnable()
                .Where((x) => _character.LearnedSkills.Contains(x.SkillID) || x.SkillID == Enums.Skill.None)
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
            UpdateSkillImage1();
            CollapseEquippedSkills();
            UpdateEquippedSkills();
        }

        private void cmbSkill2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_character == null) return;
            var cmb = (ComboBox)sender;

            Enums.Skill val = (cmb.SelectedValue == null ? Enums.Skill.None : (Enums.Skill)cmb.SelectedValue);
            _character.EquippedSkill_2 = val;
            UpdateSkillImage2();
            CollapseEquippedSkills();
            UpdateEquippedSkills();
        }

        private void cmbSkill3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_character == null) return;
            var cmb = (ComboBox)sender;

            Enums.Skill val = (cmb.SelectedValue == null ? Enums.Skill.None : (Enums.Skill)cmb.SelectedValue);
            _character.EquippedSkill_3 = val;
            UpdateSkillImage3();
            CollapseEquippedSkills();
            UpdateEquippedSkills();
        }

        private void cmbSkill4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_character == null) return;
            var cmb = (ComboBox)sender;

            Enums.Skill val = (cmb.SelectedValue == null ? Enums.Skill.None : (Enums.Skill)cmb.SelectedValue);
            _character.EquippedSkill_4 = val;
            UpdateSkillImage4();
            CollapseEquippedSkills();
            UpdateEquippedSkills();
        }

        private void cmbSkill5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_character == null) return;
            var cmb = (ComboBox)sender;

            Enums.Skill val = (cmb.SelectedValue == null ? Enums.Skill.None : (Enums.Skill)cmb.SelectedValue);
            _character.EquippedSkill_5 = val;
            UpdateSkillImage5();
            CollapseEquippedSkills();
            UpdateEquippedSkills();
        }

        private void UpdateSkillImage1()
        {
            pictSkill1.Image = GetSkillImage((Enums.Skill)cmbSkill1.SelectedValue);
        }

        private void UpdateSkillImage2()
        {
            pictSkill2.Image = GetSkillImage((Enums.Skill)cmbSkill2.SelectedValue);
        }

        private void UpdateSkillImage3()
        {
            pictSkill3.Image = GetSkillImage((Enums.Skill)cmbSkill3.SelectedValue);
        }

        private void UpdateSkillImage4()
        {
            pictSkill4.Image = GetSkillImage((Enums.Skill)cmbSkill4.SelectedValue);
        }

        private void UpdateSkillImage5()
        {
            pictSkill5.Image = GetSkillImage((Enums.Skill)cmbSkill5.SelectedValue);
        }

        /// <summary>
        /// Removes any Nones from the middle of the skill list
        /// </summary>
        private void CollapseEquippedSkills()
        {
            var skills = new List<Enums.Skill>();

            skills.Add((Enums.Skill)cmbSkill1.SelectedValue);
            skills.Add((Enums.Skill)cmbSkill2.SelectedValue);
            skills.Add((Enums.Skill)cmbSkill3.SelectedValue);
            skills.Add((Enums.Skill)cmbSkill4.SelectedValue);
            skills.Add((Enums.Skill)cmbSkill5.SelectedValue);

            skills.RemoveAll((x) => x == Enums.Skill.None);

            if (skills.Count >= 5) cmbSkill5.SelectedValue = skills[4]; else cmbSkill5.SelectedValue = Enums.Skill.None;
            if (skills.Count >= 4) cmbSkill4.SelectedValue = skills[3]; else cmbSkill4.SelectedValue = Enums.Skill.None;
            if (skills.Count >= 3) cmbSkill3.SelectedValue = skills[2]; else cmbSkill3.SelectedValue = Enums.Skill.None;
            if (skills.Count >= 2) cmbSkill2.SelectedValue = skills[1]; else cmbSkill2.SelectedValue = Enums.Skill.None;
            if (skills.Count >= 1) cmbSkill1.SelectedValue = skills[0]; else cmbSkill1.SelectedValue = Enums.Skill.None;

            UpdateSkillImage1();
            UpdateSkillImage2();
            UpdateSkillImage3();
            UpdateSkillImage4();
            UpdateSkillImage5();
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
