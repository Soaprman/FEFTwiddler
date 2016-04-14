using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using FEFTwiddler.Extensions;
using FEFTwiddler.Model;
using FEFTwiddler.Enums;

namespace FEFTwiddler
{
    public partial class FEFTwiddler : Form
    {
        private Model.SaveFile _saveFile;
        private Model.ChapterSave _chapterSave;
        private Model.Character _selectedCharacter;

        private ItemPanel[] _inventory;

        public FEFTwiddler()
        {
            InitializeComponent();
            InitializeDatabases();

            _inventory = new ItemPanel[5];
            _inventory[0] = new ItemPanel(null,
                ItemPic_1, ItemNameBox_1, ItemIsEquipped_1,
                ItemForgesBox_1, ItemQuantBox_1, ItemHexBox_1);
            _inventory[1] = new ItemPanel(null,
                ItemPic_2, ItemNameBox_2, ItemIsEquipped_2,
                ItemForgesBox_2, ItemQuantBox_2, ItemHexBox_2);
            _inventory[2] = new ItemPanel(null,
                ItemPic_3, ItemNameBox_3, ItemIsEquipped_3,
                ItemForgesBox_3, ItemQuantBox_3, ItemHexBox_3);
            _inventory[3] = new ItemPanel(null,
                ItemPic_4, ItemNameBox_4, ItemIsEquipped_4,
                ItemForgesBox_4, ItemQuantBox_4, ItemHexBox_4);
            _inventory[4] = new ItemPanel(null,
                ItemPic_5, ItemNameBox_5, ItemIsEquipped_5,
                ItemForgesBox_5, ItemQuantBox_5, ItemHexBox_5);
        }

        private void InitializeDatabases()
        {
            // TODO: Let user specify language
            // Will need to call SetLanguage on all databases when switching and refresh GUI for display names
            Data.Database.SetLanguage(Enums.Language.English);
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";

            var startupPath = Config.StartupPath;
            if (startupPath == "" || !Directory.Exists(startupPath)) startupPath = Application.StartupPath;
            openFileDialog1.InitialDirectory = startupPath;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Config.StartupPath = Path.GetDirectoryName(openFileDialog1.FileName);

                var saveFile = Model.SaveFile.FromPath(openFileDialog1.FileName);

                if (saveFile.Type != Enums.SaveFileType.Chapter)
                {
                    MessageBox.Show("This type of save is not supported yet. Only 'Chapter' saves are supported right now.");
                    return;
                }
                else
                {
                    _saveFile = saveFile;
                }

                UpdateTitleBar(openFileDialog1.FileName);

                _chapterSave = Model.ChapterSave.FromSaveFile(_saveFile);

                listBox1.DisplayMember = "CharacterID";
                listBox1.ValueMember = "BinaryPosition"; // Sure, why not
                listBox1.Items.Clear();
                foreach (var character in _chapterSave.Characters)
                {
                    listBox1.Items.Add(character);
                }

                PopulatePickers();
                LoadChapterData();
                BindEventHandlers();

                tabControl1.Enabled = true;

                listBox1.SelectedIndex = 0;
            }
        }

        private void UpdateHairColor(object sender, EventArgs e)
        {
            Byte[] NewHairColor = new byte[4];
            if(NewHairColor.TryParseHex(HairColorHex.Text + "FF"))
            {
                HairColor.BackColor = Color.FromArgb(NewHairColor[3],
                                                     NewHairColor[0],
                                                     NewHairColor[1],
                                                     NewHairColor[2]);
                _selectedCharacter.HairColor = NewHairColor;
            }
        }

        private void UpdateTitleBar(string path)
        {
            var directory = Path.GetDirectoryName(path);
            if (directory.Length > 36) directory = "..." + directory.Right(36); // Arbitary choice that I find tends to fit
            var truncatedPath = directory + "\\" + Path.GetFileName(path);
            this.Text = "FEFTwiddler - " + truncatedPath;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var character = (Model.Character)listBox1.SelectedItem;
            //var character = (from c in _chapterSave.Characters
            //                 where c.BinaryPosition == characterId
            //                 select c).FirstOrDefault();
            _selectedCharacter = character;
            LoadCharacter(character);
        }

        private void BindEventHandlers()
        {
            numGold.ValueChanged += delegate (object sender, EventArgs e) { _chapterSave.Gold = (uint)((NumericUpDown)sender).Value; };

            numAmber.ValueChanged += delegate (object sender, EventArgs e) { _chapterSave.MaterialQuantity_Amber = (byte)((NumericUpDown)sender).Value; };
            numBeans.ValueChanged += delegate (object sender, EventArgs e) { _chapterSave.MaterialQuantity_Beans = (byte)((NumericUpDown)sender).Value; };
            numBerries.ValueChanged += delegate (object sender, EventArgs e) { _chapterSave.MaterialQuantity_Berries = (byte)((NumericUpDown)sender).Value; };
            numCabbage.ValueChanged += delegate (object sender, EventArgs e) { _chapterSave.MaterialQuantity_Cabbage = (byte)((NumericUpDown)sender).Value; };
            numCoral.ValueChanged += delegate (object sender, EventArgs e) { _chapterSave.MaterialQuantity_Coral = (byte)((NumericUpDown)sender).Value; };
            numCrystal.ValueChanged += delegate (object sender, EventArgs e) { _chapterSave.MaterialQuantity_Crystal = (byte)((NumericUpDown)sender).Value; };
            numDaikon.ValueChanged += delegate (object sender, EventArgs e) { _chapterSave.MaterialQuantity_Daikon = (byte)((NumericUpDown)sender).Value; };
            numEmerald.ValueChanged += delegate (object sender, EventArgs e) { _chapterSave.MaterialQuantity_Emerald = (byte)((NumericUpDown)sender).Value; };
            numFish.ValueChanged += delegate (object sender, EventArgs e) { _chapterSave.MaterialQuantity_Fish = (byte)((NumericUpDown)sender).Value; };
            numJade.ValueChanged += delegate (object sender, EventArgs e) { _chapterSave.MaterialQuantity_Jade = (byte)((NumericUpDown)sender).Value; };
            numLapis.ValueChanged += delegate (object sender, EventArgs e) { _chapterSave.MaterialQuantity_Lapis = (byte)((NumericUpDown)sender).Value; };
            numMeat.ValueChanged += delegate (object sender, EventArgs e) { _chapterSave.MaterialQuantity_Meat = (byte)((NumericUpDown)sender).Value; };
            numMilk.ValueChanged += delegate (object sender, EventArgs e) { _chapterSave.MaterialQuantity_Milk = (byte)((NumericUpDown)sender).Value; };
            numOnyx.ValueChanged += delegate (object sender, EventArgs e) { _chapterSave.MaterialQuantity_Onyx = (byte)((NumericUpDown)sender).Value; };
            numPeaches.ValueChanged += delegate (object sender, EventArgs e) { _chapterSave.MaterialQuantity_Peaches = (byte)((NumericUpDown)sender).Value; };
            numPearl.ValueChanged += delegate (object sender, EventArgs e) { _chapterSave.MaterialQuantity_Pearl = (byte)((NumericUpDown)sender).Value; };
            numQuartz.ValueChanged += delegate (object sender, EventArgs e) { _chapterSave.MaterialQuantity_Quartz = (byte)((NumericUpDown)sender).Value; };
            numRice.ValueChanged += delegate (object sender, EventArgs e) { _chapterSave.MaterialQuantity_Rice = (byte)((NumericUpDown)sender).Value; };
            numRuby.ValueChanged += delegate (object sender, EventArgs e) { _chapterSave.MaterialQuantity_Ruby = (byte)((NumericUpDown)sender).Value; };
            numSapphire.ValueChanged += delegate (object sender, EventArgs e) { _chapterSave.MaterialQuantity_Sapphire = (byte)((NumericUpDown)sender).Value; };
            numTopaz.ValueChanged += delegate (object sender, EventArgs e) { _chapterSave.MaterialQuantity_Topaz = (byte)((NumericUpDown)sender).Value; };
            numWheat.ValueChanged += delegate (object sender, EventArgs e) { _chapterSave.MaterialQuantity_Wheat = (byte)((NumericUpDown)sender).Value; };
        }

        private void LoadChapterData()
        {
            lblAvatarName.Text = Utils.TypeConverter.ToString(_chapterSave.AvatarName);

            numGold.Value = _chapterSave.Gold;

            numDragonVeinPoints.Value = _chapterSave.DragonVeinPoint / 100;

            numAmber.Value = _chapterSave.MaterialQuantity_Amber;
            numBeans.Value = _chapterSave.MaterialQuantity_Beans;
            numBerries.Value = _chapterSave.MaterialQuantity_Berries;
            numCabbage.Value = _chapterSave.MaterialQuantity_Cabbage;
            numCoral.Value = _chapterSave.MaterialQuantity_Coral;
            numCrystal.Value = _chapterSave.MaterialQuantity_Crystal;
            numDaikon.Value = _chapterSave.MaterialQuantity_Daikon;
            numEmerald.Value = _chapterSave.MaterialQuantity_Emerald;
            numFish.Value = _chapterSave.MaterialQuantity_Fish;
            numJade.Value = _chapterSave.MaterialQuantity_Jade;
            numLapis.Value = _chapterSave.MaterialQuantity_Lapis;
            numMeat.Value = _chapterSave.MaterialQuantity_Meat;
            numMilk.Value = _chapterSave.MaterialQuantity_Milk;
            numOnyx.Value = _chapterSave.MaterialQuantity_Onyx;
            numPeaches.Value = _chapterSave.MaterialQuantity_Peaches;
            numPearl.Value = _chapterSave.MaterialQuantity_Pearl;
            numQuartz.Value = _chapterSave.MaterialQuantity_Quartz;
            numRice.Value = _chapterSave.MaterialQuantity_Rice;
            numRuby.Value = _chapterSave.MaterialQuantity_Ruby;
            numSapphire.Value = _chapterSave.MaterialQuantity_Sapphire;
            numTopaz.Value = _chapterSave.MaterialQuantity_Topaz;
            numWheat.Value = _chapterSave.MaterialQuantity_Wheat;

            // TODO: Add these after fixing ChapterSave
            //numBattlePoints.Value = _chapterSave.BattlePoints;
            //numVisitPoints.Value = _chapterSave.VisitPoints;
        }

        private void LoadCharacter(Model.Character character)
        {
            if (Enum.IsDefined(typeof(Enums.Character), character.CharacterID))
                lblName.Text = Data.Database.Characters.GetByID(character.CharacterID).DisplayName;
            else
                lblName.Text = character.CharacterID.ToString();
            if (Enum.IsDefined(typeof(Enums.Class), character.ClassID))
                cmbClass.SelectedValue = character.ClassID;
            else
                cmbClass.Text = character.ClassID.ToString();

            HairColor.BackColor = Color.FromArgb( character.HairColor[3],
                                                  character.HairColor[0],
                                                  character.HairColor[1],
                                                  character.HairColor[2]);
            HairColorHex.Text = String.Format(  "{0:X2}{1:X2}{2:X2}",
                                                character.HairColor[0],
                                                character.HairColor[1],
                                                character.HairColor[2]);

            // Set eternal seals before level, since level's range is restricted by eternal seals
            numEternalSeals.Maximum = character.GetMaxEternalSealsUsed();
            numEternalSeals.Value = character.FixEternalSealsUsed();
            numLevel.Maximum = character.GetTheoreticalMaxLevel();
            numLevel.Value = character.FixLevel();

            numInternalLevel.Value = character.InternalLevel;
            numExperience.Value = character.Experience;
            numBoots.Value = Model.Character.FixBoots(character.Boots);

            chkDeployed.Checked = character.IsDeployed;
            chkDead.Checked = character.IsDead;
            chkEinherjar.Checked = character.IsEinherjar;
            chkRecruited.Checked = character.IsRecruited;

            cmbSkill1.SelectedValue = character.EquippedSkill_1;
            pictSkill1.Image = GetSkillImage(character.EquippedSkill_1);
            cmbSkill2.SelectedValue = character.EquippedSkill_2;
            pictSkill2.Image = GetSkillImage(character.EquippedSkill_2);
            cmbSkill3.SelectedValue = character.EquippedSkill_3;
            pictSkill3.Image = GetSkillImage(character.EquippedSkill_3);
            cmbSkill4.SelectedValue = character.EquippedSkill_4;
            pictSkill4.Image = GetSkillImage(character.EquippedSkill_4);
            cmbSkill5.SelectedValue = character.EquippedSkill_5;
            pictSkill5.Image = GetSkillImage(character.EquippedSkill_5);

            cmbHeadwear.Text = character.Headwear.ToString();
            cmbFacewear.Text = character.Facewear.ToString();
            cmbArmwear.Text = character.Armwear.ToString();
            cmbUnderwear.Text = character.Underwear.ToString();

            numSword.Value = Model.Character.FixWeaponExperience(character.WeaponExperience_Sword);
            numLance.Value = Model.Character.FixWeaponExperience(character.WeaponExperience_Lance);
            numAxe.Value = Model.Character.FixWeaponExperience(character.WeaponExperience_Axe);
            numShuriken.Value = Model.Character.FixWeaponExperience(character.WeaponExperience_Shuriken);
            numBow.Value = Model.Character.FixWeaponExperience(character.WeaponExperience_Bow);
            numTome.Value = Model.Character.FixWeaponExperience(character.WeaponExperience_Tome);
            numStaff.Value = Model.Character.FixWeaponExperience(character.WeaponExperience_Staff);
            numStone.Value = Model.Character.FixWeaponExperience(character.WeaponExperience_Stone);

            _inventory[0].LoadItem(character.Item_1);
            _inventory[1].LoadItem(character.Item_2);
            _inventory[2].LoadItem(character.Item_3);
            _inventory[3].LoadItem(character.Item_4);
            _inventory[4].LoadItem(character.Item_5);

            txtStatBytes.Text = GetStatBytesString(character);

            numBattles.Value = character.BattleCount;
            numVictories.Value = character.VictoryCount;
        }

        private void PopulatePickers()
        {
            cmbClass.DisplayMember = "DisplayName";
            cmbClass.ValueMember = "ClassID";
            cmbClass.DataSource = Data.Database.Classes.GetAll();

            cmbSkill1.DisplayMember = "DisplayName";
            cmbSkill1.ValueMember = "SkillID";
            cmbSkill1.DataSource = GetSkillDataSource();

            cmbSkill2.DisplayMember = "DisplayName";
            cmbSkill2.ValueMember = "SkillID";
            cmbSkill2.DataSource = GetSkillDataSource();

            cmbSkill3.DisplayMember = "DisplayName";
            cmbSkill3.ValueMember = "SkillID";
            cmbSkill3.DataSource = GetSkillDataSource();

            cmbSkill4.DisplayMember = "DisplayName";
            cmbSkill4.ValueMember = "SkillID";
            cmbSkill4.DataSource = GetSkillDataSource();

            cmbSkill5.DisplayMember = "DisplayName";
            cmbSkill5.ValueMember = "SkillID";
            cmbSkill5.DataSource = GetSkillDataSource();

            cmbHeadwear.DataSource = Enum.GetValues(typeof(Enums.Headwear));
            cmbFacewear.DataSource = Enum.GetValues(typeof(Enums.Facewear));
            cmbArmwear.DataSource = Enum.GetValues(typeof(Enums.Armwear));
            cmbUnderwear.DataSource = Enum.GetValues(typeof(Enums.Underwear));
        }

        private IEnumerable<Data.Skill> GetSkillDataSource()
        {
            return Data.Database.Skills.GetAll()
                .Where((x) => !x.IsUnlearnable && !x.IsPersonal && !x.IsEnemyOnly)
                .OrderBy((x) => x.DisplayName)
                .ToList();
        }

        private IEnumerable<Data.Skill> GetSkillDataSourcePlusEnemyOnly()
        {
            return Data.Database.Skills.GetAll()
                .Where((x) => !x.IsUnlearnable && !x.IsPersonal)
                .OrderBy((x) => x.DisplayName)
                .ToList();
        }

        private Bitmap GetSkillImage(Enums.Skill skillId)
        {
            var img = Properties.Resources.ResourceManager.GetObject(
                        String.Format("Skill_{0:D3}", (byte)skillId));

            return (Bitmap)img;
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

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_chapterSave == null)
            {
                MessageBox.Show("No file is loaded");
                return;
            }

            _chapterSave.Write();

            MessageBox.Show("File saved. Hope you made a backup!");
        }

        private void btnAllCharAllSkills_Click(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.Characters)
            {
                character.LearnAllSkills();
            }
            MessageBox.Show("Done!");
        }

        private void btnAllCharAllSkillsDLC_Click(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.Characters)
            {
                character.LearnAllSkillsDLC();
            }
            MessageBox.Show("Done!");
        }

        private void btnAllCharAllSkillsEnemy_Click(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.Characters)
            {
                character.LearnAllSkillsEnemy();
            }
            MessageBox.Show("Done!");
        }

        private void btnAllCharMaxStatue_Click(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.Characters)
            {
                character.MaximizeStatues();
            }
            MessageBox.Show("Done!");
        }

        private void btnGiveEternalSeals_Click(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.Characters)
            {
                character.EternalSealsUsed = 16;
            }
            MessageBox.Show("Done!");
        }

        private void btnMaxWeaponExp_Click(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.Characters)
            {
                character.SRankAllWeapons();
            }
            MessageBox.Show("Done!");
        }

        private void btnMaxBoots_Click(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.Characters)
            {
                character.Boots = Model.Character.MaxBoots;
            }
            MessageBox.Show("Done!");
        }

        private void btnMaxMaterials_Click(object sender, EventArgs e)
        {
            byte max = 99;
            numAmber.Value = max;
            numBeans.Value = max;
            numBerries.Value = max;
            numCabbage.Value = max;
            numCoral.Value = max;
            numCrystal.Value = max;
            numDaikon.Value = max;
            numEmerald.Value = max;
            numFish.Value = max;
            numJade.Value = max;
            numLapis.Value = max;
            numMeat.Value = max;
            numMilk.Value = max;
            numOnyx.Value = max;
            numPeaches.Value = max;
            numPearl.Value = max;
            numQuartz.Value = max;
            numRice.Value = max;
            numRuby.Value = max;
            numSapphire.Value = max;
            numTopaz.Value = max;
            numWheat.Value = max;
        }

        private void btnMaxGold_Click(object sender, EventArgs e)
        {
            numGold.Value = 999999;
        }

        private void numSword_ValueChanged(object sender, EventArgs e)
        {
            lblSwordRank.Text = GetWeaponRank(numSword.Value);
            lblSwordRank.ForeColor = GetWeaponRankColor(numSword.Value);
        }

        private void numLance_ValueChanged(object sender, EventArgs e)
        {
            lblLanceRank.Text = GetWeaponRank(numLance.Value);
            lblLanceRank.ForeColor = GetWeaponRankColor(numLance.Value);
        }

        private void numAxe_ValueChanged(object sender, EventArgs e)
        {
            lblAxeRank.Text = GetWeaponRank(numAxe.Value);
            lblAxeRank.ForeColor = GetWeaponRankColor(numAxe.Value);
        }

        private void numShuriken_ValueChanged(object sender, EventArgs e)
        {
            lblShurikenRank.Text = GetWeaponRank(numShuriken.Value);
            lblShurikenRank.ForeColor = GetWeaponRankColor(numShuriken.Value);
        }

        private void numBow_ValueChanged(object sender, EventArgs e)
        {
            lblBowRank.Text = GetWeaponRank(numBow.Value);
            lblBowRank.ForeColor = GetWeaponRankColor(numBow.Value);
        }

        private void numTome_ValueChanged(object sender, EventArgs e)
        {
            lblTomeRank.Text = GetWeaponRank(numTome.Value);
            lblTomeRank.ForeColor = GetWeaponRankColor(numTome.Value);
        }

        private void numStaff_ValueChanged(object sender, EventArgs e)
        {
            lblStaffRank.Text = GetWeaponRank(numStaff.Value);
            lblStaffRank.ForeColor = GetWeaponRankColor(numStaff.Value);
        }

        private void numStone_ValueChanged(object sender, EventArgs e)
        {
            lblStoneRank.Text = GetWeaponRank(numStone.Value);
            lblStoneRank.ForeColor = GetWeaponRankColor(numStone.Value);
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

        private void btn99DragonVeinPoints_Click(object sender, EventArgs e)
        {
            numDragonVeinPoints.Value = 99;
        }

        private void btn9999BattlePoints_Click(object sender, EventArgs e)
        {
            numBattlePoints.Value = 9999;
        }

        private void btn9999VisitPoints_Click(object sender, EventArgs e)
        {
            numVisitPoints.Value = 9999;
        }

        private void numDragonVeinPoints_ValueChanged(object sender, EventArgs e)
        {
            _chapterSave.DragonVeinPoint = (ushort)(numDragonVeinPoints.Value * 100);
        }

        private void numBattlePoints_ValueChanged(object sender, EventArgs e)
        {
            _chapterSave.BattlePoints = (uint)numBattlePoints.Value;
        }

        private void numBattles_ValueChanged(object sender, EventArgs e)
        {
            _selectedCharacter.BattleCount = (ushort)numBattles.Value;
        }

        private void numVictories_ValueChanged(object sender, EventArgs e)
        {
            _selectedCharacter.VictoryCount = (ushort)numVictories.Value;
        }

        private void numVisitPoints_ValueChanged(object sender, EventArgs e)
        {
            _chapterSave.VisitPoints = (uint)numVisitPoints.Value;
        }

        private void tabPage1_Enter(object sender, EventArgs e)
        {
            LoadCharacter(_selectedCharacter);
        }

        private void numBoots_ValueChanged(object sender, EventArgs e)
        {
            _selectedCharacter.Boots = (byte)numBoots.Value;
        }

        private void numLevel_ValueChanged(object sender, EventArgs e)
        {
            _selectedCharacter.Level = (byte)numLevel.Value;

            var minEternalSeals = _selectedCharacter.GetMinimumEternalSealsForCurrentLevel();
            if (_selectedCharacter.EternalSealsUsed < minEternalSeals)
            {
                numEternalSeals.Value = minEternalSeals;
                _selectedCharacter.EternalSealsUsed = minEternalSeals;
            }

            var maxLevel = _selectedCharacter.GetModifiedMaxLevel();
            if (_selectedCharacter.Level == maxLevel)
            {
                numExperience.Value = 0;
                _selectedCharacter.Experience = 0;
                numExperience.Enabled = false;
            }
            else
            {
                numExperience.Enabled = true;
            }
        }

        private void numEternalSeals_ValueChanged(object sender, EventArgs e)
        {
            _selectedCharacter.EternalSealsUsed = (byte)numEternalSeals.Value;

            var maxLevel = _selectedCharacter.GetModifiedMaxLevel();
            if (_selectedCharacter.Level > maxLevel)
            {
                numLevel.Value = maxLevel;
                _selectedCharacter.Level = maxLevel;
            }

            if (_selectedCharacter.Level == maxLevel)
            {
                numExperience.Value = 0;
                _selectedCharacter.Experience = 0;
                numExperience.Enabled = false;
            }
            else
            {
                numExperience.Enabled = true;
            }
        }

        private void numExperience_ValueChanged(object sender, EventArgs e)
        {
            _selectedCharacter.Experience = (byte)numExperience.Value;
        }

        private void btnAllSkills_Click(object sender, EventArgs e)
        {
            _selectedCharacter.LearnAllSkills();
        }

        private void btnDLCSkills_Click(object sender, EventArgs e)
        {
            _selectedCharacter.LearnAllSkillsDLC();
        }

        private void btnEnemySkills_Click(object sender, EventArgs e)
        {
            _selectedCharacter.LearnAllSkillsEnemy();
        }

        private void btnMaxForges_Click(object sender, EventArgs e)
        {
            foreach (ItemPanel item in _inventory)
            {
                item.SetForges(7);
            }
        }

        private void btnMaxCharges_Click(object sender, EventArgs e)
        {
            foreach(ItemPanel item in _inventory)
            {
                item.SetCharges(35);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void cmbSkill1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_selectedCharacter == null) return;
            var cmb = (ComboBox)sender;

            Enums.Skill val = (cmb.SelectedValue == null ? Enums.Skill.None : (Enums.Skill)cmb.SelectedValue);
            _selectedCharacter.EquippedSkill_1 = val;
            pictSkill1.Image = GetSkillImage(_selectedCharacter.EquippedSkill_1);
        }

        private void cmbSkill2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_selectedCharacter == null) return;
            var cmb = (ComboBox)sender;

            Enums.Skill val = (cmb.SelectedValue == null ? Enums.Skill.None : (Enums.Skill)cmb.SelectedValue);
            _selectedCharacter.EquippedSkill_2 = val;
            pictSkill2.Image = GetSkillImage(_selectedCharacter.EquippedSkill_2);
        }

        private void cmbSkill3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_selectedCharacter == null) return;
            var cmb = (ComboBox)sender;

            Enums.Skill val = (cmb.SelectedValue == null ? Enums.Skill.None : (Enums.Skill)cmb.SelectedValue);
            _selectedCharacter.EquippedSkill_3 = val;
            pictSkill3.Image = GetSkillImage(_selectedCharacter.EquippedSkill_3);
        }

        private void cmbSkill4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_selectedCharacter == null) return;
            var cmb = (ComboBox)sender;

            Enums.Skill val = (cmb.SelectedValue == null ? Enums.Skill.None : (Enums.Skill)cmb.SelectedValue);
            _selectedCharacter.EquippedSkill_4 = val;
            pictSkill4.Image = GetSkillImage(_selectedCharacter.EquippedSkill_4);
        }

        private void cmbSkill5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_selectedCharacter == null) return;
            var cmb = (ComboBox)sender;

            Enums.Skill val = (cmb.SelectedValue == null ? Enums.Skill.None : (Enums.Skill)cmb.SelectedValue);
            _selectedCharacter.EquippedSkill_5 = val;
            pictSkill5.Image = GetSkillImage(_selectedCharacter.EquippedSkill_5);
        }

        private void chkIncludeEnemyOnlySkills_CheckedChanged(object sender, EventArgs e)
        {
            var chk = (CheckBox)sender; Func<IEnumerable<Data.Skill>> populate;

            if (chk.Checked) populate = GetSkillDataSourcePlusEnemyOnly;
            else populate = GetSkillDataSource;

            object skill1; object skill2; object skill3; object skill4; object skill5;

            skill1 = cmbSkill1.SelectedValue;
            cmbSkill1.DataSource = populate();
            cmbSkill1.SelectedValue = skill1;
            if (cmbSkill1.SelectedValue == null) cmbSkill1.SelectedValue = Enums.Skill.None;

            skill2 = cmbSkill2.SelectedValue;
            cmbSkill2.DataSource = populate();
            cmbSkill2.SelectedValue = skill2;
            if (cmbSkill2.SelectedValue == null) cmbSkill2.SelectedValue = Enums.Skill.None;

            skill3 = cmbSkill3.SelectedValue;
            cmbSkill3.DataSource = populate();
            cmbSkill3.SelectedValue = skill3;
            if (cmbSkill3.SelectedValue == null) cmbSkill3.SelectedValue = Enums.Skill.None;

            skill4 = cmbSkill4.SelectedValue;
            cmbSkill4.DataSource = populate();
            cmbSkill4.SelectedValue = skill4;
            if (cmbSkill4.SelectedValue == null) cmbSkill4.SelectedValue = Enums.Skill.None;

            skill5 = cmbSkill5.SelectedValue;
            cmbSkill5.DataSource = populate();
            cmbSkill5.SelectedValue = skill5;
            if (cmbSkill5.SelectedValue == null) cmbSkill5.SelectedValue = Enums.Skill.None;
        }

        private void btnViewLearnedSkills_Click(object sender, EventArgs e)
        {
            var popup = new GUI.UnitViewer.LearnedSkillsViewer(_selectedCharacter);
            popup.Show();
        }

        private void cmbSkill1_Leave(object sender, EventArgs e)
        {
            if (_selectedCharacter == null) return;
            var cmb = (ComboBox)sender;

            Enums.Skill val = (cmb.SelectedValue == null ? Enums.Skill.None : (Enums.Skill)cmb.SelectedValue);
            if (val == Enums.Skill.None)
            {
                cmbSkill1.SelectedValue = cmbSkill2.SelectedValue;
                cmbSkill2.SelectedValue = cmbSkill3.SelectedValue;
                cmbSkill3.SelectedValue = cmbSkill4.SelectedValue;
                cmbSkill4.SelectedValue = cmbSkill5.SelectedValue;
                cmbSkill5.SelectedValue = Enums.Skill.None;
            }
            else
            {
                _selectedCharacter.LearnedSkills.Add(val);
            }
        }

        private void cmbSkill2_Leave(object sender, EventArgs e)
        {
            if (_selectedCharacter == null) return;
            var cmb = (ComboBox)sender;

            Enums.Skill val = (cmb.SelectedValue == null ? Enums.Skill.None : (Enums.Skill)cmb.SelectedValue);
            if (val == Enums.Skill.None)
            {
                cmbSkill2.SelectedValue = cmbSkill3.SelectedValue;
                cmbSkill3.SelectedValue = cmbSkill4.SelectedValue;
                cmbSkill4.SelectedValue = cmbSkill5.SelectedValue;
                cmbSkill5.SelectedValue = Enums.Skill.None;
            }
            else
            {
                _selectedCharacter.LearnedSkills.Add(val);
            }
        }

        private void cmbSkill3_Leave(object sender, EventArgs e)
        {
            if (_selectedCharacter == null) return;
            var cmb = (ComboBox)sender;

            Enums.Skill val = (cmb.SelectedValue == null ? Enums.Skill.None : (Enums.Skill)cmb.SelectedValue);
            if (val == Enums.Skill.None)
            {
                cmbSkill3.SelectedValue = cmbSkill4.SelectedValue;
                cmbSkill4.SelectedValue = cmbSkill5.SelectedValue;
                cmbSkill5.SelectedValue = Enums.Skill.None;
            }
            else
            {
                _selectedCharacter.LearnedSkills.Add(val);
            }
        }

        private void cmbSkill4_Leave(object sender, EventArgs e)
        {
            if (_selectedCharacter == null) return;
            var cmb = (ComboBox)sender;

            Enums.Skill val = (cmb.SelectedValue == null ? Enums.Skill.None : (Enums.Skill)cmb.SelectedValue);
            if (val == Enums.Skill.None)
            {
                cmbSkill4.SelectedValue = cmbSkill5.SelectedValue;
                cmbSkill5.SelectedValue = Enums.Skill.None;
            }
            else
            {
                _selectedCharacter.LearnedSkills.Add(val);
            }
        }

        private void cmbSkill5_Leave(object sender, EventArgs e)
        {
            if (_selectedCharacter == null) return;
            var cmb = (ComboBox)sender;

            Enums.Skill val = (cmb.SelectedValue == null ? Enums.Skill.None : (Enums.Skill)cmb.SelectedValue);
            if (val != Enums.Skill.None)
            {
                _selectedCharacter.LearnedSkills.Add(val);
            }
        }
    }

    class ItemPanel
    {
        private PictureBox Pic;
        private ComboBox Name;
        private CheckBox Equipped;
        private NumericUpDown Forges;
        private NumericUpDown Charges;
        private MaskedTextBox Raw;

        private InventoryItem item;

        private Data.ItemDatabase ItemDb;

        public ItemPanel(InventoryItem Item,
            PictureBox Pic, ComboBox Name, CheckBox Equipped, NumericUpDown Forges, NumericUpDown Charges, MaskedTextBox Raw)
        {
            this.Pic = Pic;
            this.Name = Name;
            this.Equipped = Equipped;
            this.Forges = Forges;
            this.Charges = Charges;
            this.Raw = Raw;
            this.ItemDb = Data.Database.Items;

            this.item = Item;

            Name.DataSource = Enum.GetValues(typeof(Enums.Item));

            Equipped.Enabled = false;
            Name.Enabled = false;
        }

        public void LoadItem(InventoryItem Item)
        {
            item = Item;
            UpdatePanel();
        }

        public void UpdatePanel()
        {
            EventsOff();
            var data = ItemDb.GetByID(item.ItemID);
            Name.Text = data.DisplayName;

            try
            {
                if (data.Type.HasCharges())
                {
                    Charges.Enabled = true;
                    Charges.Value = item.Uses;
                }
                else
                {
                    Charges.Enabled = false;
                    Charges.Value = 1;
                }

                if (data.Type.HasForges())
                {
                    Forges.Enabled = true;
                    Forges.Value = item.Uses;
                    Equipped.Checked = item.IsEquipped;
                }
                else
                {
                    Forges.Enabled = false;
                    Forges.Value = 0;
                    Equipped.Checked = false;
                }
                Raw.Text = item.Hex();
            }
            catch(ArgumentOutOfRangeException e)
            { }
            
            EventsOn();
        }

        private void LoadRaw(String hex_string)
        {
            byte[] Hex = new byte[4];
            if (Hex.TryParseHex(hex_string))
            {
                item.Reparse(Hex);
            }
        }

        private void ForgesChanged(object sender, EventArgs e)
        {
            item.Uses = (byte)Forges.Value;
            UpdatePanel();
        }

        private void ChargesChanged(object sender, EventArgs e)
        {
            item.Uses = (byte)Charges.Value;
            UpdatePanel();
        }

        private void RawChanged(object sender, EventArgs e)
        {
            LoadRaw(Raw.Text);
            UpdatePanel();
        }

        private void EventsOn()
        {
            Forges.ValueChanged += ForgesChanged;
            Charges.ValueChanged += ChargesChanged;
            Raw.TextChanged += RawChanged;
        }

        private void EventsOff()
        {
            Forges.ValueChanged -= ForgesChanged;
            Charges.ValueChanged -= ChargesChanged;
            Raw.TextChanged -= RawChanged;
        }

        public void SetForges(int val)
        {
            var data = ItemDb.GetByID(item.ItemID);
            if (data.Type.HasForges())
            {
                item.Uses = (byte) Math.Max(7, Math.Min(0, val));
                UpdatePanel();
            }
        }

        public void SetCharges(int val)
        {
            var data = ItemDb.GetByID(item.ItemID);
            if (data.Type.HasCharges())
            {
                item.Uses = (byte)Math.Max(35, Math.Min(0, val));
                UpdatePanel();
            }
        }
    }
}
