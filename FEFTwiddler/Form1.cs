﻿using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using FEFTwiddler.Extensions;

namespace FEFTwiddler
{
    public partial class FEFTwiddler : Form
    {
        private Model.SaveFile _saveFile;
        private Model.ChapterSave _chapterSave;
        private Model.Character _selectedCharacter;

        private Data.CharacterDatabase _characterDatabase;
        private Data.ClassDatabase _classDatabase;
        private Data.ItemDatabase _itemDatabase;
        private Data.SkillDatabase _skillDatabase;

        public FEFTwiddler()
        {
            InitializeComponent();
            InitializeDatabases();
        }

        private void InitializeDatabases()
        {
            // TODO: Let user specify language
            // Will need to call SetLanguage on all databases when switching and refresh GUI for display names
            _characterDatabase = new Data.CharacterDatabase(Enums.Language.English);
            _classDatabase = new Data.ClassDatabase(Enums.Language.English);
            _itemDatabase = new Data.ItemDatabase(Enums.Language.English);
            _skillDatabase = new Data.SkillDatabase(Enums.Language.English);
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
            Byte[] NewHairColor = {0,0,0,255};
            if(HairColorHex.Text.Length == 6 &&
               Byte.TryParse(HairColorHex.Text.Substring(0,2), System.Globalization.NumberStyles.HexNumber, null, out NewHairColor[0]) &&
               Byte.TryParse(HairColorHex.Text.Substring(2,2), System.Globalization.NumberStyles.HexNumber, null, out NewHairColor[1]) &&
               Byte.TryParse(HairColorHex.Text.Substring(4,2), System.Globalization.NumberStyles.HexNumber, null, out NewHairColor[2]))
            {
                HairColor.BackColor = Color.FromArgb(NewHairColor[3],
                                                     NewHairColor[2],
                                                     NewHairColor[1],
                                                     NewHairColor[0]);
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
                lblName.Text = _characterDatabase.GetByID(character.CharacterID).DisplayName;
            else
                lblName.Text = character.CharacterID.ToString();
            if (Enum.IsDefined(typeof(Enums.Class), character.ClassID))
                cmbClass.Text = _classDatabase.GetByID(character.ClassID).DisplayName;
            else
                cmbClass.Text = character.ClassID.ToString();

            HairColor.BackColor = Color.FromArgb( character.HairColor[3],
                                                  character.HairColor[2],
                                                  character.HairColor[1],
                                                  character.HairColor[0]);
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

            cmbSkill1.Text = _skillDatabase.GetByID(character.EquippedSkill_1).DisplayName;
            pictSkill1.Image = GetSkillImage(character.EquippedSkill_1);
            cmbSkill2.Text = _skillDatabase.GetByID(character.EquippedSkill_2).DisplayName;
            pictSkill2.Image = GetSkillImage(character.EquippedSkill_2);
            cmbSkill3.Text = _skillDatabase.GetByID(character.EquippedSkill_3).DisplayName;
            pictSkill3.Image = GetSkillImage(character.EquippedSkill_3);
            cmbSkill4.Text = _skillDatabase.GetByID(character.EquippedSkill_4).DisplayName;
            pictSkill4.Image = GetSkillImage(character.EquippedSkill_4);
            cmbSkill5.Text = _skillDatabase.GetByID(character.EquippedSkill_5).DisplayName;
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

            lblInventory1.Text = GetItemString(character.Item_1);
            lblInventory2.Text = GetItemString(character.Item_2);
            lblInventory3.Text = GetItemString(character.Item_3);
            lblInventory4.Text = GetItemString(character.Item_4);
            lblInventory5.Text = GetItemString(character.Item_5);

            txtStatBytes.Text = GetStatBytesString(character);

            numBattles.Value = character.BattleCount;
            numVictories.Value = character.VictoryCount;

            //EnableControls();
        }

        private void PopulatePickers()
        {
            cmbClass.DataSource = Enum.GetValues(typeof(Enums.Class));
            cmbSkill1.DataSource = Enum.GetValues(typeof(Enums.Skill));
            cmbSkill2.DataSource = Enum.GetValues(typeof(Enums.Skill));
            cmbSkill3.DataSource = Enum.GetValues(typeof(Enums.Skill));
            cmbSkill4.DataSource = Enum.GetValues(typeof(Enums.Skill));
            cmbSkill5.DataSource = Enum.GetValues(typeof(Enums.Skill));

            cmbHeadwear.DataSource = Enum.GetValues(typeof(Enums.Headwear));
            cmbFacewear.DataSource = Enum.GetValues(typeof(Enums.Facewear));
            cmbArmwear.DataSource = Enum.GetValues(typeof(Enums.Armwear));
            cmbUnderwear.DataSource = Enum.GetValues(typeof(Enums.Underwear));
        }

        private Bitmap GetSkillImage(Enums.Skill skillId)
        {
            var img = Properties.Resources.ResourceManager.GetObject(
                        String.Format("Skill_{0,000}", (byte)skillId));

            return (Bitmap)img;
        }

        private string GetStatBytesString(Model.Character character)
        {
            var str = "";

            if (Enum.IsDefined(typeof(Enums.Character), character.CharacterID) &&
                Enum.IsDefined(typeof(Enums.Class), character.ClassID))
            {
                var characterData = _characterDatabase.GetByID(character.CharacterID);
                var classData = _classDatabase.GetByID(character.ClassID);
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

        private void EnableControls()
        {
            cmbClass.Enabled = true;
            numLevel.Enabled = true;
            numExperience.Enabled = true;
            cmbSkill1.Enabled = true;
            cmbSkill2.Enabled = true;
            cmbSkill3.Enabled = true;
            cmbSkill4.Enabled = true;
            cmbSkill5.Enabled = true;
        }

        private void cmbSkill1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Enums.Skill skillId;
            //if (Enum.TryParse(cmbSkill1.Text, out skillId))
            //{
            //    _selectedCharacter.EquippedSkill_1 = skillId;
            //    pictSkill1.Image = GetSkillImage(_selectedCharacter.EquippedSkill_1);
            //}
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
                character.maxStatue();
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

        private string GetItemString(Model.InventoryItem item)
        {
            var data = _itemDatabase.GetByID(item.ItemID);

            var equipped = (item.IsEquipped ? "(E) " : "");
            var displayName = data.DisplayName;
            string uses;
            if (data.Type == Enums.ItemType.Sword ||
                data.Type == Enums.ItemType.Lance ||
                data.Type == Enums.ItemType.Axe ||
                data.Type == Enums.ItemType.Shuriken ||
                data.Type == Enums.ItemType.Bow ||
                data.Type == Enums.ItemType.Tome ||
                data.Type == Enums.ItemType.Stone ||
                data.Type == Enums.ItemType.NPC ||
                data.Type == Enums.ItemType.Unknown)
            {
                if (item.Uses > 0) uses = "+" + item.Uses.ToString();
                else uses = "";
            }
            else
            {
                uses = "x" + item.Uses.ToString() + "/" + data.MaximumUses.ToString();
            }
            var nameId = (item.ItemNameID > 0 ? "NameID: " + item.ItemNameID.ToString() : "");
            return String.Format("{0} {1} {2} {3}", equipped, displayName, uses, nameId);
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
