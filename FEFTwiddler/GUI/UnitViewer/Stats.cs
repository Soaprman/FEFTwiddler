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
                byte[] trueStats = new byte[] {
                    (byte)(characterData.Base_HP  + classData.Base_HP  + character.StatBytes1[0]),
                    (byte)(characterData.Base_Str + classData.Base_Str + character.StatBytes1[1]),
                    (byte)(characterData.Base_Mag + classData.Base_Mag + character.StatBytes1[2]),
                    (byte)(characterData.Base_Skl + classData.Base_Skl + character.StatBytes1[3]),
                    (byte)(characterData.Base_Spd + classData.Base_Spd + character.StatBytes1[4]),
                    (byte)(characterData.Base_Lck + classData.Base_Lck + character.StatBytes1[5]),
                    (byte)(characterData.Base_Def + classData.Base_Def + character.StatBytes1[6]),
                    (byte)(characterData.Base_Res + classData.Base_Res + character.StatBytes1[7])
                };
                byte[] caps = new byte[] {
                    (byte)(classData.Max_HP  + characterData.Modifier_HP  + character.StatueBonuses[0]),
                    (byte)(classData.Max_Str + characterData.Modifier_Str + character.StatueBonuses[1]),
                    (byte)(classData.Max_Mag + characterData.Modifier_Mag + character.StatueBonuses[2]),
                    (byte)(classData.Max_Skl + characterData.Modifier_Skl + character.StatueBonuses[3]),
                    (byte)(classData.Max_Spd + characterData.Modifier_Spd + character.StatueBonuses[4]),
                    (byte)(classData.Max_Lck + characterData.Modifier_Lck + character.StatueBonuses[5]),
                    (byte)(classData.Max_Def + characterData.Modifier_Def + character.StatueBonuses[6]),
                    (byte)(classData.Max_Res + characterData.Modifier_Res + character.StatueBonuses[7])
                };
                for (int i = 0; i < 8; i++)
                {
                    if (trueStats[i] < caps[i])
                        str += trueStats[i].ToString() + '-';
                    else
                        str += caps[i].ToString() + '-';
                }
            }
            else
            {
                str += BitConverter.ToString((byte[])(Array)character.StatBytes1);
                str += Environment.NewLine;
                str += BitConverter.ToString((byte[])(Array)character.StatBytes2);
            }

            return str;
        }
    }
}
