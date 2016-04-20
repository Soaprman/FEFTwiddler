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
    public partial class Stats : UserControl
    {
        private Model.Character _character;

        public Stats()
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
        }

        private void PopulateControls()
        {
            txtStatBytes.Text = GetStatBytesString(_character);
        }

        private string GetStatBytesString(Model.Character character)
        {
            var str = "";

            if (Enum.IsDefined(typeof(Enums.Character), character.CharacterID) &&
                Enum.IsDefined(typeof(Enums.Class), character.ClassID))
            {
                var characterData = Data.Database.Characters.GetByID(character.CharacterID);
                var classData = Data.Database.Classes.GetByID(character.ClassID);

                // Calculate "true" stats
                Model.Stat trueStats;
                if (!Enum.IsDefined(typeof(Enums.Stat), character.Boon) ||
                    !Enum.IsDefined(typeof(Enums.Stat), character.Bane))
                {
                    trueStats = characterData.BaseStats + classData.BaseStats + character.GainedStats;
                    this.Enabled = false;
                    return trueStats.HP.ToString() + "-" +
                        trueStats.Str.ToString() + "-" +
                        trueStats.Mag.ToString() + "-" +
                        trueStats.Skl.ToString() + "-" +
                        trueStats.Spd.ToString() + "-" +
                        trueStats.Lck.ToString() + "-" +
                        trueStats.Def.ToString() + "-" +
                        trueStats.Res.ToString() + "\n(incorrect)";
                }
                if (character.CorrinName != null) // Corrin and bond units
                    trueStats = characterData.BaseStats +
                        Data.Database.Stats.GetByID(character.Boon).BaseBoonModifiers +
                        Data.Database.Stats.GetByID(character.Bane).BaseBaneModifiers +
                        classData.BaseStats + character.GainedStats;
                else
                    trueStats = characterData.BaseStats + classData.BaseStats + character.GainedStats;

                // Calculate stat caps
                Model.Stat caps;
                if (characterData.IsCorrin)
                {
                    caps = classData.MaximumStats +
                        Data.Database.Stats.GetByID(character.Boon).MaxBoonModifiers +
                        Data.Database.Stats.GetByID(character.Bane).MaxBaneModifiers +
                        character.StatueBonusStats;
                }
                else if (characterData.IsChild)
                {
                    var childBonusStats = new Model.Stat { HP = 0, Str = 1, Mag = 1, Skl = 1, Spd = 1, Lck = 1, Def = 1, Res = 1 };
                    caps = classData.MaximumStats +
                        Data.Database.Characters.GetByID(character.FatherID).Modifiers +
                        Data.Database.Stats.GetByID(character.FatherBoon).MaxBoonModifiers +
                        Data.Database.Stats.GetByID(character.FatherBane).MaxBaneModifiers +
                        Data.Database.Characters.GetByID(character.MotherID).Modifiers +
                        Data.Database.Stats.GetByID(character.MotherBoon).MaxBoonModifiers +
                        Data.Database.Stats.GetByID(character.MotherBane).MaxBaneModifiers +
                        childBonusStats +
                        character.StatueBonusStats;
                }
                else
                {
                    caps = classData.MaximumStats + characterData.Modifiers + character.StatueBonusStats;
                }

                // Print
                if (trueStats.HP < 0) str += "0-"; else if (trueStats.HP < caps.HP) str += trueStats.HP.ToString() + "-"; else str += caps.HP.ToString() + "-";
                if (trueStats.Str < 0) str += "0-"; else if (trueStats.Str < caps.Str) str += trueStats.Str.ToString() + "-"; else str += caps.Str.ToString() + "-";
                if (trueStats.Mag < 0) str += "0-"; else if (trueStats.Mag < caps.Mag) str += trueStats.Mag.ToString() + "-"; else str += caps.Mag.ToString() + "-";
                if (trueStats.Skl < 0) str += "0-"; else if (trueStats.Skl < caps.Skl) str += trueStats.Skl.ToString() + "-"; else str += caps.Skl.ToString() + "-";
                if (trueStats.Spd < 0) str += "0-"; else if (trueStats.Spd < caps.Spd) str += trueStats.Spd.ToString() + "-"; else str += caps.Spd.ToString() + "-";
                if (trueStats.Lck < 0) str += "0-"; else if (trueStats.Lck < caps.Lck) str += trueStats.Lck.ToString() + "-"; else str += caps.Lck.ToString() + "-";
                if (trueStats.Def < 0) str += "0-"; else if (trueStats.Def < caps.Def) str += trueStats.Def.ToString() + "-"; else str += caps.Def.ToString() + "-";
                if (trueStats.Res < 0) str += "0"; else if (trueStats.Res < caps.Res) str += trueStats.Res.ToString(); else str += caps.Res.ToString();
                this.Enabled = true;
            }
            else
            {
                str += string.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", 
                    character.GainedStats.HP,
                    character.GainedStats.Str,
                    character.GainedStats.Mag,
                    character.GainedStats.Skl,
                    character.GainedStats.Spd,
                    character.GainedStats.Lck,
                    character.GainedStats.Def,
                    character.GainedStats.Res);
                str += Environment.NewLine;
                str += string.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}",
                    character.StatueBonusStats.HP,
                    character.StatueBonusStats.Str,
                    character.StatueBonusStats.Mag,
                    character.StatueBonusStats.Skl,
                    character.StatueBonusStats.Spd,
                    character.StatueBonusStats.Lck,
                    character.StatueBonusStats.Def,
                    character.StatueBonusStats.Res);
                this.Enabled = false;
            }

            return str;
        }
    }
}
