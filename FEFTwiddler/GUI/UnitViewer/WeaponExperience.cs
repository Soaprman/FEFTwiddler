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
    public partial class WeaponExperience : UserControl
    {
        private Model.Character _character;

        public WeaponExperience()
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
            numSword.Value = Model.Character.FixWeaponExperience(_character.WeaponExperience_Sword);
            numLance.Value = Model.Character.FixWeaponExperience(_character.WeaponExperience_Lance);
            numAxe.Value = Model.Character.FixWeaponExperience(_character.WeaponExperience_Axe);
            numShuriken.Value = Model.Character.FixWeaponExperience(_character.WeaponExperience_Shuriken);
            numBow.Value = Model.Character.FixWeaponExperience(_character.WeaponExperience_Bow);
            numTome.Value = Model.Character.FixWeaponExperience(_character.WeaponExperience_Tome);
            numStaff.Value = Model.Character.FixWeaponExperience(_character.WeaponExperience_Staff);

            if (Enum.IsDefined(typeof(Enums.Character), _character.CharacterID) &&
                Data.Database.Characters.GetByID(_character.CharacterID).CanUseStones)
            {
                numStone.Value = Model.Character.FixWeaponExperience(_character.WeaponExperience_Stone);
                numStone.Enabled = true;
            }
            else
            {
                numStone.Value = Model.Character.MinWeaponExperience;
                numStone.Enabled = false;
            }
        }

        private void numSword_ValueChanged(object sender, EventArgs e)
        {
            var picker = (NumericUpDown)sender;
            lblSwordRank.Text = GetWeaponRank(picker.Value);
            lblSwordRank.ForeColor = GetWeaponRankColor(picker.Value);
            _character.WeaponExperience_Sword = (byte)picker.Value;
        }

        private void numLance_ValueChanged(object sender, EventArgs e)
        {
            var picker = (NumericUpDown)sender;
            lblLanceRank.Text = GetWeaponRank(picker.Value);
            lblLanceRank.ForeColor = GetWeaponRankColor(picker.Value);
            _character.WeaponExperience_Lance = (byte)picker.Value;
        }

        private void numAxe_ValueChanged(object sender, EventArgs e)
        {
            var picker = (NumericUpDown)sender;
            lblAxeRank.Text = GetWeaponRank(picker.Value);
            lblAxeRank.ForeColor = GetWeaponRankColor(picker.Value);
            _character.WeaponExperience_Axe = (byte)picker.Value;
        }

        private void numShuriken_ValueChanged(object sender, EventArgs e)
        {
            var picker = (NumericUpDown)sender;
            lblShurikenRank.Text = GetWeaponRank(picker.Value);
            lblShurikenRank.ForeColor = GetWeaponRankColor(picker.Value);
            _character.WeaponExperience_Shuriken = (byte)picker.Value;
        }

        private void numBow_ValueChanged(object sender, EventArgs e)
        {
            var picker = (NumericUpDown)sender;
            lblBowRank.Text = GetWeaponRank(picker.Value);
            lblBowRank.ForeColor = GetWeaponRankColor(picker.Value);
            _character.WeaponExperience_Bow = (byte)picker.Value;
        }

        private void numTome_ValueChanged(object sender, EventArgs e)
        {
            var picker = (NumericUpDown)sender;
            lblTomeRank.Text = GetWeaponRank(picker.Value);
            lblTomeRank.ForeColor = GetWeaponRankColor(picker.Value);
            _character.WeaponExperience_Tome = (byte)picker.Value;
        }

        private void numStaff_ValueChanged(object sender, EventArgs e)
        {
            var picker = (NumericUpDown)sender;
            lblStaffRank.Text = GetWeaponRank(picker.Value);
            lblStaffRank.ForeColor = GetWeaponRankColor(picker.Value);
            _character.WeaponExperience_Staff = (byte)picker.Value;
        }

        private void numStone_ValueChanged(object sender, EventArgs e)
        {
            var picker = (NumericUpDown)sender;
            lblStoneRank.Text = GetWeaponRank(picker.Value);
            lblStoneRank.ForeColor = GetWeaponRankColor(picker.Value);
            _character.WeaponExperience_Stone = (byte)picker.Value;
        }

        private string GetWeaponRank(decimal weaponExp)
        {
            if (weaponExp >= 251) return "S";
            if (weaponExp >= 161) return "A";
            if (weaponExp >= 96) return "B";
            if (weaponExp >= 51) return "C";
            if (weaponExp >= 21) return "D";
            return "E";
        }

        private Color GetWeaponRankColor(decimal weaponExp)
        {
            if (weaponExp >= 251) return Color.Green;
            return Color.Black;
        }
    }
}
