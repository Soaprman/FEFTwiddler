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

        private Model.Unit _unit;

        public ClassAndLevel()
        {
            InitializeComponent();
            InitializeControls();
        }

        public void LoadUnit(Model.Unit unit)
        {
            _unit = unit;
            PopulateControls();
        }

        private void InitializeControls()
        {
            _tooltip.SetToolTip(numLevel, "The unit's level. This caps at 40 for special classes, and 20 for other classes.");
            _tooltip.SetToolTip(numInternalLevel, "Set at promotion. This is added to the displayed level for use in EXP calculations.");
            _tooltip.SetToolTip(numExperience, "The unit's experience points. The degree of closeness to another disappointing level up.");
            _tooltip.SetToolTip(numEternalSeals, "The number of eternal seals used. Each seal increases this unit's level cap by 5.");
            _tooltip.SetToolTip(numBoots, "The number of shoes this unit is wearing. Barefooted units incur a movement penalty because the terrain makes their feet hurt.");

            cmbClass.DisplayMember = "DisplayName";
            cmbClass.ValueMember = "ClassID";

            BindEventHandlers();
        }

        private void PopulateControls()
        {
            this.Invalidate();
            UnbindEventHandlers();

            if (Enum.IsDefined(typeof(Enums.Class), _unit.ClassID))
            {
                cmbClass.DataSource = GetClassDataSource();
                cmbClass.SelectedValue = _unit.ClassID;
            }
            else
            {
                cmbClass.Text = _unit.ClassID.ToString();
            }

            // Set eternal seals before level, since level's range is restricted by eternal seals
            numEternalSeals.Maximum = Model.Unit.MaxEternalSealsUsed;
            numEternalSeals.Value = _unit.FixEternalSealsUsed();
            numLevel.Maximum = _unit.GetTheoreticalMaxLevel();
            numLevel.Value = _unit.FixLevel();

            numInternalLevel.Value = _unit.InternalLevel;
            numExperience.Value = _unit.Experience;
            numBoots.Value = Model.Unit.FixBoots(_unit.Boots);

            BindEventHandlers();
            this.Refresh();
        }

        private List<Data.Class> GetClassDataSource()
        {
            var classes = Data.Database.Classes.GetAll().OrderBy((x) => x.DisplayName).ToList();
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

            _unit.ClassID = (Enums.Class)cmb.SelectedValue;

            // Unlock the DLC class for heart sealing for later
            if (_unit.ClassID == Enums.Class.PegasusKnight_Minerva) _unit.HeartSeal_PegasusKnightMinerva = true;

            // If they changed to a class with a lower max level, drop the level to the new max
            var maxLevel = _unit.GetModifiedMaxLevel();
            if (_unit.Level > maxLevel)
            {
                numLevel.Value = maxLevel;
                _unit.Level = maxLevel;
            }
        }

        private void UpdateLevel(object sender, EventArgs e)
        {
            _unit.Level = (byte)numLevel.Value;

            var minEternalSeals = _unit.GetMinimumEternalSealsForCurrentLevel();
            if (_unit.EternalSealsUsed < minEternalSeals)
            {
                numEternalSeals.Value = minEternalSeals;
                _unit.EternalSealsUsed = minEternalSeals;
            }

            var maxLevel = _unit.GetModifiedMaxLevel();
            if (_unit.Level == maxLevel)
            {
                numExperience.Value = 0;
                _unit.Experience = 0;
                numExperience.Enabled = false;
            }
            else
            {
                numExperience.Enabled = true;
            }
        }

        private void UpdateInternalLevel(object sender, EventArgs e)
        {
            _unit.InternalLevel = (byte)numInternalLevel.Value;
        }

        private void UpdateEternalSeals(object sender, EventArgs e)
        {
            _unit.EternalSealsUsed = (byte)numEternalSeals.Value;

            var maxLevel = _unit.GetModifiedMaxLevel();
            if (_unit.Level > maxLevel)
            {
                numLevel.Value = maxLevel;
                _unit.Level = maxLevel;
            }

            if (_unit.Level == maxLevel)
            {
                numExperience.Value = 0;
                _unit.Experience = 0;
                numExperience.Enabled = false;
            }
            else
            {
                numExperience.Enabled = true;
            }
        }

        private void UpdateExperience(object sender, EventArgs e)
        {
            _unit.Experience = (byte)numExperience.Value;
        }

        private void UpdateBoots(object sender, EventArgs e)
        {
            _unit.Boots = (byte)numBoots.Value;
        }
    }
}
