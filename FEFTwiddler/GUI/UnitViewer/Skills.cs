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
        }

        private void PopulateControls()
        {
            UnbindEvents();
            BindDataSources();
            ReadEquippedSkills();
            BindEvents();
        }

        private void BindDataSources()
        {
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
        }

        private void UnbindEvents()
        {
            cmbSkill1.SelectedIndexChanged -= ChangeSkill1;
            cmbSkill2.SelectedIndexChanged -= ChangeSkill2;
            cmbSkill3.SelectedIndexChanged -= ChangeSkill3;
            cmbSkill4.SelectedIndexChanged -= ChangeSkill4;
            cmbSkill5.SelectedIndexChanged -= ChangeSkill5;
        }

        private void BindEvents()
        {
            cmbSkill1.SelectedIndexChanged += ChangeSkill1;
            cmbSkill2.SelectedIndexChanged += ChangeSkill2;
            cmbSkill3.SelectedIndexChanged += ChangeSkill3;
            cmbSkill4.SelectedIndexChanged += ChangeSkill4;
            cmbSkill5.SelectedIndexChanged += ChangeSkill5;
        }

        private void btnEditLearnedSkills_Click(object sender, EventArgs e)
        {
            var popup = new GUI.UnitViewer.LearnedSkillsViewer(_character);
            popup.ShowDialog();
            _character.UnequipUnlearnedSkills();
            PopulateControls();
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

        private void ChangeSkill1(object sender, EventArgs e)
        {
            UnbindEvents();
            WriteEquippedSkills();
            ReadEquippedSkills();
            BindEvents();
        }

        private void ChangeSkill2(object sender, EventArgs e)
        {
            UnbindEvents();
            WriteEquippedSkills();
            ReadEquippedSkills();
            BindEvents();
        }

        private void ChangeSkill3(object sender, EventArgs e)
        {
            UnbindEvents();
            WriteEquippedSkills();
            ReadEquippedSkills();
            BindEvents();
        }

        private void ChangeSkill4(object sender, EventArgs e)
        {
            UnbindEvents();
            WriteEquippedSkills();
            ReadEquippedSkills();
            BindEvents();
        }

        private void ChangeSkill5(object sender, EventArgs e)
        {
            UnbindEvents();
            WriteEquippedSkills();
            ReadEquippedSkills();
            BindEvents();
        }

        #region Model to GUI

        private void ReadEquippedSkills()
        {
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

        #endregion

        #region GUI to Model

        private void WriteEquippedSkills()
        {
            _character.EquippedSkill_1 = (Enums.Skill)cmbSkill1.SelectedValue;
            _character.EquippedSkill_2 = (Enums.Skill)cmbSkill2.SelectedValue;
            _character.EquippedSkill_3 = (Enums.Skill)cmbSkill3.SelectedValue;
            _character.EquippedSkill_4 = (Enums.Skill)cmbSkill4.SelectedValue;
            _character.EquippedSkill_5 = (Enums.Skill)cmbSkill5.SelectedValue;

            _character.CollapseEquippedSkills();
        }

        #endregion
    }
}
