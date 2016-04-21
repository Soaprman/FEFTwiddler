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
        private Model.Character _character;
        private Model.Stat caps;

        public bool IsStatsChanged = false;

        public StatEditor(Model.Character character)
        {
            _character = character;
            InitializeComponent();
            SetTitle();
        }

        private void SetTitle()
        {
            if (Enum.IsDefined(typeof(Enums.Character), _character.CharacterID))
            {
                if (_character.CorrinName == null)
                    this.Text = "Stat editing: " + Data.Database.Characters.GetByID(_character.CharacterID).DisplayName;
                else
                    this.Text = "Stat editing: " + _character.CorrinName;
            }
            else
            {
                this.Text = "Stat editing: Some unknown character";
            }
        }

        private void StatEditor_Load(object sender, EventArgs e)
        {
            caps = Utils.StatUtil.CalculateStatCaps(_character);
            Model.Stat stats = Utils.StatUtil.CalculateStats(_character);

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
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Model.Stat baseStats = Utils.StatUtil.CalculateBaseStats(_character);
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
            Model.Stat finalStats = _character.GainedStats;

            if (changes.HP != 0) { finalStats.HP = changes.HP; IsStatsChanged = true; }
            if (changes.Str != 0) { finalStats.Str = changes.Str; IsStatsChanged = true; }
            if (changes.Mag != 0) { finalStats.Mag = changes.Mag; IsStatsChanged = true; }
            if (changes.Skl != 0) { finalStats.Skl = changes.Skl; IsStatsChanged = true; }
            if (changes.Spd != 0) { finalStats.Spd = changes.Spd; IsStatsChanged = true; }
            if (changes.Lck != 0) { finalStats.Lck = changes.Lck; IsStatsChanged = true; }
            if (changes.Def != 0) { finalStats.Def = changes.Def; IsStatsChanged = true; }
            if (changes.Res != 0) { finalStats.Res = changes.Res; IsStatsChanged = true; }
            _character.GainedStats = finalStats;
            this.Close();
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
    }
}
