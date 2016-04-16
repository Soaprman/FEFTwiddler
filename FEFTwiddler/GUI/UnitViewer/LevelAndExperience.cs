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
    public partial class LevelAndExperience : UserControl
    {
        private Model.Character _character;

        public LevelAndExperience()
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
            BindEventHandlers();
        }

        private void PopulateControls()
        {
            UnbindEventHandlers();

            // Set eternal seals before level, since level's range is restricted by eternal seals
            numEternalSeals.Maximum = _character.GetMaxEternalSealsUsed();
            numEternalSeals.Value = _character.FixEternalSealsUsed();
            numLevel.Maximum = _character.GetTheoreticalMaxLevel();
            numLevel.Value = _character.FixLevel();

            numInternalLevel.Value = _character.InternalLevel;
            numExperience.Value = _character.Experience;
            numBoots.Value = Model.Character.FixBoots(_character.Boots);

            BindEventHandlers();
        }

        private void BindEventHandlers()
        {
            numLevel.ValueChanged += UpdateLevel;
            numEternalSeals.ValueChanged += UpdateEternalSeals;
            numExperience.ValueChanged += UpdateExperience;
            numBoots.ValueChanged += UpdateBoots;
        }

        private void UnbindEventHandlers()
        {
            numLevel.ValueChanged -= UpdateLevel;
            numEternalSeals.ValueChanged -= UpdateEternalSeals;
            numExperience.ValueChanged -= UpdateExperience;
            numBoots.ValueChanged -= UpdateBoots;
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
