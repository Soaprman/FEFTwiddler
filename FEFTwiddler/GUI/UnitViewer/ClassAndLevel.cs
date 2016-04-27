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
    public partial class ClassAndLevel : UserControl
    {
        private ToolTip _tooltip = new ToolTip();

        private Model.Character _character;

        public ClassAndLevel()
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
            _tooltip.SetToolTip(numLevel, "The unit's level. This caps at 40 for special classes, and 20 for other classes.");
            _tooltip.SetToolTip(numInternalLevel, "Set at promotion. This is added to the displayed level for use in EXP calculations.");
            _tooltip.SetToolTip(numExperience, "The unit's experience points. The degree of closeness to another disappointing level up.");
            _tooltip.SetToolTip(numEternalSeals, "The number of eternal seals used. Each seal increases this unit's level cap by 5.");
            _tooltip.SetToolTip(numBoots, "The number of shoes this unit is wearing. Barefooted units incur a movement penalty because the terrain makes their feet hurt.");

            BindEventHandlers();
        }

        private void PopulateControls()
        {
            UnbindEventHandlers();

            if (Enum.IsDefined(typeof(Enums.Class), _character.ClassID))
            {
                cmbClass.DisplayMember = "DisplayName";
                cmbClass.ValueMember = "ClassID";
                cmbClass.DataSource = GetClassDataSource();

                cmbClass.Text = ""; // Just prevents some visual jumpiness to Nohr Noble when switching units

                cmbClass.SelectedValue = _character.ClassID;
            }
            else
            {
                cmbClass.Text = _character.ClassID.ToString();
            }

            // Set eternal seals before level, since level's range is restricted by eternal seals
            numEternalSeals.Maximum = Model.Character.MaxEternalSealsUsed;
            numEternalSeals.Value = _character.FixEternalSealsUsed();
            numLevel.Maximum = _character.GetTheoreticalMaxLevel();
            numLevel.Value = _character.FixLevel();

            numInternalLevel.Value = _character.InternalLevel;
            numExperience.Value = _character.Experience;
            numBoots.Value = Model.Character.FixBoots(_character.Boots);

            BindEventHandlers();
        }

        private List<Data.Class> GetClassDataSource()
        {
            var characterData = Data.Database.Characters.GetByID(_character.CharacterID);
            var classes = Data.Database.Classes.GetAll().Where((x) => x.Gender == characterData.Gender).ToList();
            return classes;
        }

        private void BindEventHandlers()
        {
            cmbClass.SelectedValueChanged += UpdateClass;
            numLevel.ValueChanged += UpdateLevel;
            numInternalLevel.ValueChanged += UpdateInternalLevel;
            numEternalSeals.ValueChanged += UpdateEternalSeals;
            numExperience.ValueChanged += UpdateExperience;
            numBoots.ValueChanged += UpdateBoots;
        }

        private void UnbindEventHandlers()
        {
            cmbClass.SelectedValueChanged -= UpdateClass;
            numLevel.ValueChanged -= UpdateLevel;
            numInternalLevel.ValueChanged -= UpdateInternalLevel;
            numEternalSeals.ValueChanged -= UpdateEternalSeals;
            numExperience.ValueChanged -= UpdateExperience;
            numBoots.ValueChanged -= UpdateBoots;
        }

        private void UpdateClass(object sender, EventArgs e)
        {
            var cmb = (ComboBox)sender;

            _character.ClassID = (Enums.Class)cmb.SelectedValue;

            // If they changed to a class with a lower max level, drop the level to the new max
            var maxLevel = _character.GetModifiedMaxLevel();
            if (_character.Level > maxLevel)
            {
                numLevel.Value = maxLevel;
                _character.Level = maxLevel;
            }
        }

        private void UpdateLevel(object sender, EventArgs e)
        {
            _character.Level = (byte)numLevel.Value;

            var minEternalSeals = _character.GetMinimumEternalSealsForCurrentLevel();
            if (_character.EternalSealsUsed < minEternalSeals)
            {
                numEternalSeals.Value = minEternalSeals;
                _character.EternalSealsUsed = minEternalSeals;
            }

            var maxLevel = _character.GetModifiedMaxLevel();
            if (_character.Level == maxLevel)
            {
                numExperience.Value = 0;
                _character.Experience = 0;
                numExperience.Enabled = false;
            }
            else
            {
                numExperience.Enabled = true;
            }
        }

        private void UpdateInternalLevel(object sender, EventArgs e)
        {
            _character.InternalLevel = (byte)numInternalLevel.Value;
        }

        private void UpdateEternalSeals(object sender, EventArgs e)
        {
            _character.EternalSealsUsed = (byte)numEternalSeals.Value;

            var maxLevel = _character.GetModifiedMaxLevel();
            if (_character.Level > maxLevel)
            {
                numLevel.Value = maxLevel;
                _character.Level = maxLevel;
            }

            if (_character.Level == maxLevel)
            {
                numExperience.Value = 0;
                _character.Experience = 0;
                numExperience.Enabled = false;
            }
            else
            {
                numExperience.Enabled = true;
            }
        }

        private void UpdateExperience(object sender, EventArgs e)
        {
            _character.Experience = (byte)numExperience.Value;
        }

        private void UpdateBoots(object sender, EventArgs e)
        {
            _character.Boots = (byte)numBoots.Value;
        }
    }
}
