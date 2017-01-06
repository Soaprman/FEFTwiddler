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
    public partial class StatEditor : Form
    {
        private Model.Unit _unit;
        private Model.Stat caps;

        public bool IsStatsChanged = false;

        public StatEditor(Model.Unit unit)
        {
            _unit = unit;
            InitializeComponent();
            SetTitle();
        }

        private void SetTitle()
        {
            this.Text = _unit.GetDisplayName() + "'s stats";
        }

        private void StatEditor_Load(object sender, EventArgs e)
        {
            // Labels
            var statData = Data.Database.Stats;
            lblHP.Text = statData.GetByID(Enums.Stat.HP).DisplayName;
            lblStr.Text = statData.GetByID(Enums.Stat.Strength).DisplayName;
            lblMag.Text = statData.GetByID(Enums.Stat.Magic).DisplayName;
            lblSkl.Text = statData.GetByID(Enums.Stat.Skill).DisplayName;
            lblSpd.Text = statData.GetByID(Enums.Stat.Speed).DisplayName;
            lblLck.Text = statData.GetByID(Enums.Stat.Luck).DisplayName;
            lblDef.Text = statData.GetByID(Enums.Stat.Defense).DisplayName;
            lblRes.Text = statData.GetByID(Enums.Stat.Resistance).DisplayName;

            // Stats
            caps = Utils.StatUtil.CalculateStatCaps(_unit);
            Model.Stat stats = Utils.StatUtil.CalculateStats(_unit);

            numHP.Maximum = caps.HP;
            numStr.Maximum = caps.Str;
            numMag.Maximum = caps.Mag;
            numSkl.Maximum = caps.Skl;
            numSpd.Maximum = caps.Spd;
            numLck.Maximum = caps.Lck;
            numDef.Maximum = caps.Def;
            numRes.Maximum = caps.Res;

            numHP.Value = stats.HP;
            numStr.Value = stats.Str;
            numMag.Value = stats.Mag;
            numSkl.Value = stats.Skl;
            numSpd.Value = stats.Spd;
            numLck.Value = stats.Lck;
            numDef.Value = stats.Def;
            numRes.Value = stats.Res;

            // Tonic
            Model.Stat tonicBonuses = _unit.TonicBonusStats;
            chkHPTonic.Checked = (tonicBonuses.HP == 5);
            chkStrTonic.Checked = (tonicBonuses.Str == 2);
            chkMagTonic.Checked = (tonicBonuses.Mag == 2);
            chkSklTonic.Checked = (tonicBonuses.Skl == 2);
            chkSpdTonic.Checked = (tonicBonuses.Spd == 2);
            chkLckTonic.Checked = (tonicBonuses.Lck == 4);
            chkDefTonic.Checked = (tonicBonuses.Def == 2);
            chkResTonic.Checked = (tonicBonuses.Res == 2);

            // Status
            Model.Stat statusBonuses = _unit.StatusBonusStats;
            chkStrStatus.Checked = (statusBonuses.Str == 4);
            chkMagStatus.Checked = (statusBonuses.Mag == 4);
            chkSklStatus.Checked = (statusBonuses.Skl == 4);
            chkSpdStatus.Checked = (statusBonuses.Spd == 4);
            chkLckStatus.Checked = (statusBonuses.Lck == 4);
            chkDefStatus.Checked = (statusBonuses.Def == 4);
            chkResStatus.Checked = (statusBonuses.Res == 4);

            // Meal
            Model.Stat mealBonuses = _unit.MealBonusStats;
            SetMealCheckBox(chkStrMeal, mealBonuses.Str);
            SetMealCheckBox(chkMagMeal, mealBonuses.Mag);
            SetMealCheckBox(chkSklMeal, mealBonuses.Skl);
            SetMealCheckBox(chkSpdMeal, mealBonuses.Spd);
            SetMealCheckBox(chkLckMeal, mealBonuses.Lck);
            SetMealCheckBox(chkDefMeal, mealBonuses.Def);
            SetMealCheckBox(chkResMeal, mealBonuses.Res);
        }

        private void SetMealCheckBox(CheckBox chk, sbyte bonus)
        {
            switch (bonus)
            {
                case 2: chk.CheckState = CheckState.Checked; break;
                case 0: chk.CheckState = CheckState.Unchecked; break;
                default: chk.CheckState = CheckState.Indeterminate; break;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Model.Stat baseStats = Utils.StatUtil.CalculateBaseStats(_unit);
            Model.Stat changes = new Model.Stat
            {
                HP = (sbyte)numHP.Value,
                Str = (sbyte)numStr.Value,
                Mag = (sbyte)numMag.Value,
                Skl = (sbyte)numSkl.Value,
                Spd = (sbyte)numSpd.Value,
                Lck = (sbyte)numLck.Value,
                Def = (sbyte)numDef.Value,
                Res = (sbyte)numRes.Value
            } - baseStats;
            Model.Stat finalStats = _unit.GainedStats;

            if (changes.HP != 0) { finalStats.HP = changes.HP; IsStatsChanged = true; }
            if (changes.Str != 0) { finalStats.Str = changes.Str; IsStatsChanged = true; }
            if (changes.Mag != 0) { finalStats.Mag = changes.Mag; IsStatsChanged = true; }
            if (changes.Skl != 0) { finalStats.Skl = changes.Skl; IsStatsChanged = true; }
            if (changes.Spd != 0) { finalStats.Spd = changes.Spd; IsStatsChanged = true; }
            if (changes.Lck != 0) { finalStats.Lck = changes.Lck; IsStatsChanged = true; }
            if (changes.Def != 0) { finalStats.Def = changes.Def; IsStatsChanged = true; }
            if (changes.Res != 0) { finalStats.Res = changes.Res; IsStatsChanged = true; }
            _unit.GainedStats = finalStats;

            // Tonic
            Model.Stat tonicBonuses = new Model.Stat()
            {
                HP = chkHPTonic.Checked ? (sbyte)5 : (sbyte)0,
                Str = chkStrTonic.Checked ? (sbyte)2 : (sbyte)0,
                Mag = chkMagTonic.Checked ? (sbyte)2 : (sbyte)0,
                Skl = chkSklTonic.Checked ? (sbyte)2 : (sbyte)0,
                Spd = chkSpdTonic.Checked ? (sbyte)2 : (sbyte)0,
                Lck = chkLckTonic.Checked ? (sbyte)4 : (sbyte)0,
                Def = chkDefTonic.Checked ? (sbyte)2 : (sbyte)0,
                Res = chkResTonic.Checked ? (sbyte)2 : (sbyte)0
            };
            _unit.TonicBonusStats = tonicBonuses;

            // Status
            Model.Stat statusBonuses = new Model.Stat()
            {
                HP = 0,
                Str = chkStrStatus.Checked ? (sbyte)4 : (sbyte)0,
                Mag = chkMagStatus.Checked ? (sbyte)4 : (sbyte)0,
                Skl = chkSklStatus.Checked ? (sbyte)4 : (sbyte)0,
                Spd = chkSpdStatus.Checked ? (sbyte)4 : (sbyte)0,
                Lck = chkLckStatus.Checked ? (sbyte)4 : (sbyte)0,
                Def = chkDefStatus.Checked ? (sbyte)4 : (sbyte)0,
                Res = chkResStatus.Checked ? (sbyte)4 : (sbyte)0
            };
            _unit.StatusBonusStats = statusBonuses;

            // Meal
            Model.Stat oldMealBonuses = _unit.MealBonusStats;
            Model.Stat newMealBonuses = new Model.Stat()
            {
                HP = 0,
                Str = GetMealCheckBox(chkStrMeal, oldMealBonuses.Str),
                Mag = GetMealCheckBox(chkMagMeal, oldMealBonuses.Mag),
                Skl = GetMealCheckBox(chkSklMeal, oldMealBonuses.Skl),
                Spd = GetMealCheckBox(chkSpdMeal, oldMealBonuses.Spd),
                Lck = GetMealCheckBox(chkLckMeal, oldMealBonuses.Lck),
                Def = GetMealCheckBox(chkDefMeal, oldMealBonuses.Def),
                Res = GetMealCheckBox(chkResMeal, oldMealBonuses.Res)
            };
            _unit.MealBonusStats = newMealBonuses;

            this.Close();
        }

        // Convert checkbox state to bonus stat
        private sbyte GetMealCheckBox(CheckBox chk, sbyte oldBonus)
        {
            sbyte newBonus;
            switch (chk.CheckState)
            {
                case CheckState.Checked: newBonus = 2; break;
                case CheckState.Unchecked: newBonus = 0; break;
                default: newBonus = oldBonus; break;
            }
            return newBonus;
        }

        private void btnMax_Click(object sender, EventArgs e)
        {
            numHP.Value = caps.HP;
            numStr.Value = caps.Str;
            numMag.Value = caps.Mag;
            numSkl.Value = caps.Skl;
            numSpd.Value = caps.Spd;
            numLck.Value = caps.Lck;
            numDef.Value = caps.Def;
            numRes.Value = caps.Res;
        }

        private void btnAllBonuses_Click(object sender, EventArgs e)
        {
            // Tonic
            chkHPTonic.Checked = true;
            chkStrTonic.Checked = true;
            chkMagTonic.Checked = true;
            chkSklTonic.Checked = true;
            chkSpdTonic.Checked = true;
            chkLckTonic.Checked = true;
            chkDefTonic.Checked = true;
            chkResTonic.Checked = true;

            // Status
            chkStrStatus.Checked = true;
            chkMagStatus.Checked = true;
            chkSklStatus.Checked = true;
            chkSpdStatus.Checked = true;
            chkLckStatus.Checked = true;
            chkDefStatus.Checked = true;
            chkResStatus.Checked = true;

            // Meal
            chkStrMeal.Checked = true;
            chkMagMeal.Checked = true;
            chkSklMeal.Checked = true;
            chkSpdMeal.Checked = true;
            chkLckMeal.Checked = true;
            chkDefMeal.Checked = true;
            chkResMeal.Checked = true;
        }
    }
}
