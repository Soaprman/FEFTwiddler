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
                    (byte)(characterData.Base_HP  + classData.Base_HP  + character.Stat_HP_Gained),
                    (byte)(characterData.Base_Str + classData.Base_Str + character.Stat_Str_Gained),
                    (byte)(characterData.Base_Mag + classData.Base_Mag + character.Stat_Mag_Gained),
                    (byte)(characterData.Base_Skl + classData.Base_Skl + character.Stat_Skl_Gained),
                    (byte)(characterData.Base_Spd + classData.Base_Spd + character.Stat_Spd_Gained),
                    (byte)(characterData.Base_Lck + classData.Base_Lck + character.Stat_Lck_Gained),
                    (byte)(characterData.Base_Def + classData.Base_Def + character.Stat_Def_Gained),
                    (byte)(characterData.Base_Res + classData.Base_Res + character.Stat_Res_Gained)
                };
                byte[] caps;
                if (characterData.IsCorrin)
                {
                    var boonData = Data.Database.Stats.GetByID(character.Boon);
                    var baneData = Data.Database.Stats.GetByID(character.Bane);
                    caps = new byte[] {
                        (byte)(classData.Max_HP + boonData.BoonStats.HP + baneData.BaneStats.HP + character.Stat_HP_StatueBonus),
                        (byte)(classData.Max_Str + boonData.BoonStats.Str + baneData.BaneStats.Str + character.Stat_Str_StatueBonus),
                        (byte)(classData.Max_Mag + boonData.BoonStats.Mag + baneData.BaneStats.Mag + character.Stat_Mag_StatueBonus),
                        (byte)(classData.Max_Skl + boonData.BoonStats.Skl + baneData.BaneStats.Skl + character.Stat_Skl_StatueBonus),
                        (byte)(classData.Max_Spd + boonData.BoonStats.Spd + baneData.BaneStats.Spd + character.Stat_Spd_StatueBonus),
                        (byte)(classData.Max_Lck + boonData.BoonStats.Lck + baneData.BaneStats.Lck + character.Stat_Lck_StatueBonus),
                        (byte)(classData.Max_Def + boonData.BoonStats.Def + baneData.BaneStats.Def + character.Stat_Def_StatueBonus),
                        (byte)(classData.Max_Res + boonData.BoonStats.Res + baneData.BaneStats.Res + character.Stat_Res_StatueBonus)
                    };
                }
                else if (characterData.IsChild)
                {
                    var fatherData = Data.Database.Characters.GetByID(character.FatherID);
                    var fatherBoonData = Data.Database.Stats.GetByID(character.FatherBoon);
                    var fatherBaneData = Data.Database.Stats.GetByID(character.FatherBane);
                    var motherData = Data.Database.Characters.GetByID(character.MotherID);
                    var motherBoonData = Data.Database.Stats.GetByID(character.MotherBoon);
                    var motherBaneData = Data.Database.Stats.GetByID(character.MotherBane);
                    caps = new byte[] {
                        (byte)(classData.Max_HP + fatherData.Modifier_HP + fatherBoonData.BoonStats.HP + fatherBaneData.BaneStats.HP + motherData.Modifier_HP + motherBoonData.BoonStats.HP + motherBaneData.BaneStats.HP + character.Stat_HP_StatueBonus),
                        (byte)(classData.Max_Str + fatherData.Modifier_Str + fatherBoonData.BoonStats.Str + fatherBaneData.BaneStats.Str + motherData.Modifier_Str + motherBoonData.BoonStats.Str + motherBaneData.BaneStats.Str + 1 + character.Stat_Str_StatueBonus),
                        (byte)(classData.Max_Mag + fatherData.Modifier_Mag + fatherBoonData.BoonStats.Mag + fatherBaneData.BaneStats.Mag + motherData.Modifier_Mag + motherBoonData.BoonStats.Mag + motherBaneData.BaneStats.Mag + 1 + character.Stat_Mag_StatueBonus),
                        (byte)(classData.Max_Skl + fatherData.Modifier_Skl + fatherBoonData.BoonStats.Skl + fatherBaneData.BaneStats.Skl + motherData.Modifier_Skl + motherBoonData.BoonStats.Skl + motherBaneData.BaneStats.Skl + 1 + character.Stat_Skl_StatueBonus),
                        (byte)(classData.Max_Spd + fatherData.Modifier_Spd + fatherBoonData.BoonStats.Spd + fatherBaneData.BaneStats.Spd + motherData.Modifier_Spd + motherBoonData.BoonStats.Spd + motherBaneData.BaneStats.Spd + 1 + character.Stat_Spd_StatueBonus),
                        (byte)(classData.Max_Lck + fatherData.Modifier_Lck + fatherBoonData.BoonStats.Lck + fatherBaneData.BaneStats.Lck + motherData.Modifier_Lck + motherBoonData.BoonStats.Lck + motherBaneData.BaneStats.Lck + 1 + character.Stat_Lck_StatueBonus),
                        (byte)(classData.Max_Def + fatherData.Modifier_Def + fatherBoonData.BoonStats.Def + fatherBaneData.BaneStats.Def + motherData.Modifier_Def + motherBoonData.BoonStats.Def + motherBaneData.BaneStats.Def + 1 + character.Stat_Def_StatueBonus),
                        (byte)(classData.Max_Res + fatherData.Modifier_Res + fatherBoonData.BoonStats.Res + fatherBaneData.BaneStats.Res + motherData.Modifier_Res + motherBoonData.BoonStats.Res + motherBaneData.BaneStats.Res + 1 + character.Stat_Res_StatueBonus)
                    };
                }
                else
                {
                    caps = new byte[] {
                        (byte)(classData.Max_HP  + characterData.Modifier_HP  + character.Stat_HP_StatueBonus),
                        (byte)(classData.Max_Str + characterData.Modifier_Str + character.Stat_Str_StatueBonus),
                        (byte)(classData.Max_Mag + characterData.Modifier_Mag + character.Stat_Mag_StatueBonus),
                        (byte)(classData.Max_Skl + characterData.Modifier_Skl + character.Stat_Skl_StatueBonus),
                        (byte)(classData.Max_Spd + characterData.Modifier_Spd + character.Stat_Spd_StatueBonus),
                        (byte)(classData.Max_Lck + characterData.Modifier_Lck + character.Stat_Lck_StatueBonus),
                        (byte)(classData.Max_Def + characterData.Modifier_Def + character.Stat_Def_StatueBonus),
                        (byte)(classData.Max_Res + characterData.Modifier_Res + character.Stat_Res_StatueBonus)
                    };
                }
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
                str += string.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", 
                    character.Stat_HP_Gained,
                    character.Stat_Str_Gained,
                    character.Stat_Mag_Gained,
                    character.Stat_Skl_Gained,
                    character.Stat_Spd_Gained,
                    character.Stat_Lck_Gained,
                    character.Stat_Def_Gained,
                    character.Stat_Res_Gained);
                str += Environment.NewLine;
                str += string.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}",
                    character.Stat_HP_StatueBonus,
                    character.Stat_Str_StatueBonus,
                    character.Stat_Mag_StatueBonus,
                    character.Stat_Skl_StatueBonus,
                    character.Stat_Spd_StatueBonus,
                    character.Stat_Lck_StatueBonus,
                    character.Stat_Def_StatueBonus,
                    character.Stat_Res_StatueBonus);
            }

            return str;
        }
    }
}
