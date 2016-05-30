using System;
using System.Windows.Forms;

namespace FEFTwiddler.GUI.UnitViewer
{
    public partial class Traits : Form
    {
        private Model.Unit _unit;

        public Traits(Model.Unit unit)
        {
            _unit = unit;
            InitializeComponent();
            PopulateControls();
            BindEvents();
        }

        public void PopulateControls()
        {
            chkTrait_00_01.Checked = _unit.Trait_IsFemale;
            chkTrait_00_02.Checked = _unit.Trait_Hero;
            chkTrait_00_04.Checked = _unit.Trait_Player;
            chkTrait_00_08.Checked = _unit.Trait_AdvancedClasses;
            chkTrait_00_10.Checked = _unit.Trait_Leader;
            chkTrait_00_20.Checked = _unit.Trait_DefeatCondition;
            chkTrait_00_40.Checked = _unit.Trait_MovementBan;
            chkTrait_00_80.Checked = _unit.Trait_HitBan;
            chkTrait_01_01.Checked = _unit.Trait_CriticalBan;
            chkTrait_01_02.Checked = _unit.Trait_AvoidBan;
            chkTrait_01_04.Checked = _unit.Trait_ForceHit;
            chkTrait_01_08.Checked = _unit.Trait_ForceCritical;
            chkTrait_01_10.Checked = _unit.Trait_ForceAvoid;
            chkTrait_01_20.Checked = _unit.Trait_ForceDodge;
            chkTrait_01_40.Checked = _unit.Trait_ResistStatus;
            chkTrait_01_80.Checked = _unit.Trait_ImmuneStatus;
            chkTrait_02_01.Checked = _unit.Trait_NegateLethality;
            chkTrait_02_02.Checked = _unit.Trait_02_02;
            chkTrait_02_04.Checked = _unit.Trait_02_04;
            chkTrait_02_08.Checked = _unit.Trait_DoubleExpWhenDefeated;
            chkTrait_02_10.Checked = _unit.Trait_HalfExpWhenDefeated;
            chkTrait_02_20.Checked = _unit.Trait_RareFacelessExp;
            chkTrait_02_40.Checked = _unit.Trait_ExpCorrection;
            chkTrait_02_80.Checked = _unit.Trait_IsManakete;
            chkTrait_03_01.Checked = _unit.Trait_IsBeast;
            chkTrait_03_02.Checked = _unit.Trait_Sing;
            chkTrait_03_04.Checked = _unit.Trait_DestroysVillages;
            chkTrait_03_08.Checked = _unit.Trait_EnemyOnly;
            chkTrait_03_10.Checked = _unit.Trait_03_10;
            chkTrait_03_20.Checked = _unit.Trait_03_20;
            chkTrait_03_40.Checked = _unit.Trait_Takumi;
            chkTrait_03_80.Checked = _unit.Trait_Ryoma;
            chkTrait_04_01.Checked = _unit.Trait_Leo;
            chkTrait_04_02.Checked = _unit.Trait_Xander;
            chkTrait_04_04.Checked = _unit.Trait_CannotUseSpecialWeapon;
            chkTrait_04_08.Checked = _unit.Trait_CanUseDragonVein;
            chkTrait_04_10.Checked = _unit.Trait_CannotUseAttackStance;
            chkTrait_04_20.Checked = _unit.Trait_CannotDoubleAttack;
            chkTrait_04_40.Checked = _unit.Trait_CannotBeInherited;
            chkTrait_04_80.Checked = _unit.Trait_CannotBeObtainedViaSupport;
            chkTrait_05_01.Checked = _unit.Trait_RouteLimited;
            chkTrait_05_02.Checked = _unit.Trait_05_02;
            chkTrait_05_04.Checked = _unit.Trait_CanUseStaff;
            chkTrait_05_08.Checked = _unit.Trait_CannotBeTraded;
            chkTrait_05_10.Checked = _unit.Trait_CannotObtainExp;
            chkTrait_05_20.Checked = _unit.Trait_CannotWarp;
            chkTrait_05_40.Checked = _unit.Trait_SalespersonInMyCastle;
            chkTrait_05_80.Checked = _unit.Trait_DefeatConditionWithdrawal;
            chkTrait_06_01.Checked = _unit.Trait_Ophelia;
            chkTrait_06_02.Checked = _unit.Trait_CannotTriggerOffensiveSkills;
            chkTrait_06_04.Checked = _unit.Trait_TriggerOffensiveSkills;
            chkTrait_06_08.Checked = _unit.Trait_Ties;
            chkTrait_06_10.Checked = _unit.Trait_CapturedUnit;
            chkTrait_06_20.Checked = _unit.Trait_AvoidMinus10;
            chkTrait_06_40.Checked = _unit.Trait_AvoidMinus20;
            chkTrait_06_80.Checked = _unit.Trait_AvoidPlus10;
            chkTrait_07_01.Checked = _unit.Trait_AvoidPlus20;
            chkTrait_07_02.Checked = _unit.Trait_HitPlus10;
            chkTrait_07_04.Checked = _unit.Trait_HitPlus20;
            chkTrait_07_08.Checked = _unit.Trait_HitPlus30;
            chkTrait_07_10.Checked = _unit.Trait_07_10;
            chkTrait_07_20.Checked = _unit.Trait_CannotBePromoted;
            chkTrait_07_40.Checked = _unit.Trait_IsAmiibo;
            chkTrait_07_80.Checked = _unit.Trait_07_80;
        }

        public void BindEvents()
        {
            chkTrait_00_01.Click += delegate (object sender, EventArgs e) { _unit.Trait_IsFemale = ((CheckBox)sender).Checked; };
            chkTrait_00_02.Click += delegate (object sender, EventArgs e) { _unit.Trait_Hero = ((CheckBox)sender).Checked; };
            chkTrait_00_04.Click += delegate (object sender, EventArgs e) { _unit.Trait_Player = ((CheckBox)sender).Checked; };
            chkTrait_00_08.Click += delegate (object sender, EventArgs e) { _unit.Trait_AdvancedClasses = ((CheckBox)sender).Checked; };
            chkTrait_00_10.Click += delegate (object sender, EventArgs e) { _unit.Trait_Leader = ((CheckBox)sender).Checked; };
            chkTrait_00_20.Click += delegate (object sender, EventArgs e) { _unit.Trait_DefeatCondition = ((CheckBox)sender).Checked; };
            chkTrait_00_40.Click += delegate (object sender, EventArgs e) { _unit.Trait_MovementBan = ((CheckBox)sender).Checked; };
            chkTrait_00_80.Click += delegate (object sender, EventArgs e) { _unit.Trait_HitBan = ((CheckBox)sender).Checked; };
            chkTrait_01_01.Click += delegate (object sender, EventArgs e) { _unit.Trait_CriticalBan = ((CheckBox)sender).Checked; };
            chkTrait_01_02.Click += delegate (object sender, EventArgs e) { _unit.Trait_AvoidBan = ((CheckBox)sender).Checked; };
            chkTrait_01_04.Click += delegate (object sender, EventArgs e) { _unit.Trait_ForceHit = ((CheckBox)sender).Checked; };
            chkTrait_01_08.Click += delegate (object sender, EventArgs e) { _unit.Trait_ForceCritical = ((CheckBox)sender).Checked; };
            chkTrait_01_10.Click += delegate (object sender, EventArgs e) { _unit.Trait_ForceAvoid = ((CheckBox)sender).Checked; };
            chkTrait_01_20.Click += delegate (object sender, EventArgs e) { _unit.Trait_ForceDodge = ((CheckBox)sender).Checked; };
            chkTrait_01_40.Click += delegate (object sender, EventArgs e) { _unit.Trait_ResistStatus = ((CheckBox)sender).Checked; };
            chkTrait_01_80.Click += delegate (object sender, EventArgs e) { _unit.Trait_ImmuneStatus = ((CheckBox)sender).Checked; };
            chkTrait_02_01.Click += delegate (object sender, EventArgs e) { _unit.Trait_NegateLethality = ((CheckBox)sender).Checked; };
            chkTrait_02_02.Click += delegate (object sender, EventArgs e) { _unit.Trait_02_02 = ((CheckBox)sender).Checked; };
            chkTrait_02_04.Click += delegate (object sender, EventArgs e) { _unit.Trait_02_04 = ((CheckBox)sender).Checked; };
            chkTrait_02_08.Click += delegate (object sender, EventArgs e) { _unit.Trait_DoubleExpWhenDefeated = ((CheckBox)sender).Checked; };
            chkTrait_02_10.Click += delegate (object sender, EventArgs e) { _unit.Trait_HalfExpWhenDefeated = ((CheckBox)sender).Checked; };
            chkTrait_02_20.Click += delegate (object sender, EventArgs e) { _unit.Trait_RareFacelessExp = ((CheckBox)sender).Checked; };
            chkTrait_02_40.Click += delegate (object sender, EventArgs e) { _unit.Trait_ExpCorrection = ((CheckBox)sender).Checked; };
            chkTrait_02_80.Click += delegate (object sender, EventArgs e) { _unit.Trait_IsManakete = ((CheckBox)sender).Checked; };
            chkTrait_03_01.Click += delegate (object sender, EventArgs e) { _unit.Trait_IsBeast = ((CheckBox)sender).Checked; };
            chkTrait_03_02.Click += delegate (object sender, EventArgs e) { _unit.Trait_Sing = ((CheckBox)sender).Checked; };
            chkTrait_03_04.Click += delegate (object sender, EventArgs e) { _unit.Trait_DestroysVillages = ((CheckBox)sender).Checked; };
            chkTrait_03_08.Click += delegate (object sender, EventArgs e) { _unit.Trait_EnemyOnly = ((CheckBox)sender).Checked; };
            chkTrait_03_10.Click += delegate (object sender, EventArgs e) { _unit.Trait_03_10 = ((CheckBox)sender).Checked; };
            chkTrait_03_20.Click += delegate (object sender, EventArgs e) { _unit.Trait_03_20 = ((CheckBox)sender).Checked; };
            chkTrait_03_40.Click += delegate (object sender, EventArgs e) { _unit.Trait_Takumi = ((CheckBox)sender).Checked; };
            chkTrait_03_80.Click += delegate (object sender, EventArgs e) { _unit.Trait_Ryoma = ((CheckBox)sender).Checked; };
            chkTrait_04_01.Click += delegate (object sender, EventArgs e) { _unit.Trait_Leo = ((CheckBox)sender).Checked; };
            chkTrait_04_02.Click += delegate (object sender, EventArgs e) { _unit.Trait_Xander = ((CheckBox)sender).Checked; };
            chkTrait_04_04.Click += delegate (object sender, EventArgs e) { _unit.Trait_CannotUseSpecialWeapon = ((CheckBox)sender).Checked; };
            chkTrait_04_08.Click += delegate (object sender, EventArgs e) { _unit.Trait_CanUseDragonVein = ((CheckBox)sender).Checked; };
            chkTrait_04_10.Click += delegate (object sender, EventArgs e) { _unit.Trait_CannotUseAttackStance = ((CheckBox)sender).Checked; };
            chkTrait_04_20.Click += delegate (object sender, EventArgs e) { _unit.Trait_CannotDoubleAttack = ((CheckBox)sender).Checked; };
            chkTrait_04_40.Click += delegate (object sender, EventArgs e) { _unit.Trait_CannotBeInherited = ((CheckBox)sender).Checked; };
            chkTrait_04_80.Click += delegate (object sender, EventArgs e) { _unit.Trait_CannotBeObtainedViaSupport = ((CheckBox)sender).Checked; };
            chkTrait_05_01.Click += delegate (object sender, EventArgs e) { _unit.Trait_RouteLimited = ((CheckBox)sender).Checked; };
            chkTrait_05_02.Click += delegate (object sender, EventArgs e) { _unit.Trait_05_02 = ((CheckBox)sender).Checked; };
            chkTrait_05_04.Click += delegate (object sender, EventArgs e) { _unit.Trait_CanUseStaff = ((CheckBox)sender).Checked; };
            chkTrait_05_08.Click += delegate (object sender, EventArgs e) { _unit.Trait_CannotBeTraded = ((CheckBox)sender).Checked; };
            chkTrait_05_10.Click += delegate (object sender, EventArgs e) { _unit.Trait_CannotObtainExp = ((CheckBox)sender).Checked; };
            chkTrait_05_20.Click += delegate (object sender, EventArgs e) { _unit.Trait_CannotWarp = ((CheckBox)sender).Checked; };
            chkTrait_05_40.Click += delegate (object sender, EventArgs e) { _unit.Trait_SalespersonInMyCastle = ((CheckBox)sender).Checked; };
            chkTrait_05_80.Click += delegate (object sender, EventArgs e) { _unit.Trait_DefeatConditionWithdrawal = ((CheckBox)sender).Checked; };
            chkTrait_06_01.Click += delegate (object sender, EventArgs e) { _unit.Trait_Ophelia = ((CheckBox)sender).Checked; };
            chkTrait_06_02.Click += delegate (object sender, EventArgs e) { _unit.Trait_CannotTriggerOffensiveSkills = ((CheckBox)sender).Checked; };
            chkTrait_06_04.Click += delegate (object sender, EventArgs e) { _unit.Trait_TriggerOffensiveSkills = ((CheckBox)sender).Checked; };
            chkTrait_06_08.Click += delegate (object sender, EventArgs e) { _unit.Trait_Ties = ((CheckBox)sender).Checked; };
            chkTrait_06_10.Click += delegate (object sender, EventArgs e) { _unit.Trait_CapturedUnit = ((CheckBox)sender).Checked; };
            chkTrait_06_20.Click += delegate (object sender, EventArgs e) { _unit.Trait_AvoidMinus10 = ((CheckBox)sender).Checked; };
            chkTrait_06_40.Click += delegate (object sender, EventArgs e) { _unit.Trait_AvoidMinus20 = ((CheckBox)sender).Checked; };
            chkTrait_06_80.Click += delegate (object sender, EventArgs e) { _unit.Trait_AvoidPlus10 = ((CheckBox)sender).Checked; };
            chkTrait_07_01.Click += delegate (object sender, EventArgs e) { _unit.Trait_AvoidPlus20 = ((CheckBox)sender).Checked; };
            chkTrait_07_02.Click += delegate (object sender, EventArgs e) { _unit.Trait_HitPlus10 = ((CheckBox)sender).Checked; };
            chkTrait_07_04.Click += delegate (object sender, EventArgs e) { _unit.Trait_HitPlus20 = ((CheckBox)sender).Checked; };
            chkTrait_07_08.Click += delegate (object sender, EventArgs e) { _unit.Trait_HitPlus30 = ((CheckBox)sender).Checked; };
            chkTrait_07_10.Click += delegate (object sender, EventArgs e) { _unit.Trait_07_10 = ((CheckBox)sender).Checked; };
            chkTrait_07_20.Click += delegate (object sender, EventArgs e) { _unit.Trait_CannotBePromoted = ((CheckBox)sender).Checked; };
            chkTrait_07_40.Click += delegate (object sender, EventArgs e) { _unit.Trait_IsAmiibo = ((CheckBox)sender).Checked; };
            chkTrait_07_80.Click += delegate (object sender, EventArgs e) { _unit.Trait_07_80 = ((CheckBox)sender).Checked; };

            btnClose.Click += delegate (object sender, EventArgs e) { Close(); };
        }
    }
}
