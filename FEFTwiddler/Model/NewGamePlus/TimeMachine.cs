using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEFTwiddler.Model.NewGamePlus
{
    public class TimeMachine
    {
        private Model.IChapterSave _chapterSave;

        public TimeMachine(Model.IChapterSave chapterSave)
        {
            _chapterSave = chapterSave;
        }

        public void NewGamePlus()
        {

        }

        public void RemoveEveryoneButCorrin()
        {
            _chapterSave.UnitRegion.Units.RemoveAll((x) => !Data.Database.Characters.GetByID(x.CharacterID).IsCorrin);
        }

        public void InsertCharacters()
        {
            var characterDatas = Data.Database.Characters.GetAllNamedPlayable().Where((x) => !x.IsCorrin && x.CharacterID < Enums.Character.Kana_M);
            foreach (var characterData in characterDatas)
            {
                var unit = Model.Unit.Create(characterData.CharacterID);
                unit.UnitBlock = Enums.UnitBlock.Absent;
                _chapterSave.UnitRegion.Units.Add(unit);
            }
        }

        public void LevelUpAllUnits()
        {
            foreach (var unit in _chapterSave.UnitRegion.Units)
            {
                // TODO: Remove fixed value, determine based on which game is being played
                // Try without it this time
                //LevelUp(unit, 19);
            }
        }

        // TODO: Revert Corrin
        // Remove marriage byte (otherwise paralogue 2 stays available)

        /// <summary>
        /// Level a unit, giving them the average gains that their growth rates would dictate
        /// </summary>
        public void LevelUp(Model.Unit unit, int levels)
        {
            var characterGrowthRates = Data.Database.Characters.GetByID(unit.CharacterID).GrowthRates;
            var classGrowthRates = Data.Database.Classes.GetByID(unit.ClassID).GrowthRates;
            var combinedGrowthRates = characterGrowthRates + classGrowthRates;

            int hp; int str; int mag; int skl; int spd; int lck; int def; int res;
            hp = str = mag = skl = spd = lck = def = res = 0;
            for (var i = 0; i < levels; i++)
            {
                hp += combinedGrowthRates.HP;
                str += combinedGrowthRates.Str;
                mag += combinedGrowthRates.Mag;
                skl += combinedGrowthRates.Skl;
                spd += combinedGrowthRates.Spd;
                lck += combinedGrowthRates.Lck;
                def += combinedGrowthRates.Def;
                res += combinedGrowthRates.Res;
            }

            unit.Level += (byte)levels;
            unit.GainedStats.HP += (sbyte)(hp / 100);
            unit.GainedStats.Str += (sbyte)(str / 100);
            unit.GainedStats.Mag += (sbyte)(mag / 100);
            unit.GainedStats.Skl += (sbyte)(skl / 100);
            unit.GainedStats.Spd += (sbyte)(spd / 100);
            unit.GainedStats.Lck += (sbyte)(lck / 100);
            unit.GainedStats.Def += (sbyte)(def / 100);
            unit.GainedStats.Res += (sbyte)(res / 100);
        }

        public void EmptyConvoy()
        {
            _chapterSave.ConvoyRegion.Convoy.Clear();
        }

        public bool CanUnplayChapter(Enums.Chapter chapterId)
        {
            var chapterData = Data.Database.Chapters.GetByID(chapterId);

            foreach (var unlockedChapterId in chapterData.UnlocksChapters)
            {
                if (_chapterSave.BattlefieldRegion.Battlefields
                    .Where(x => x.ChapterID == unlockedChapterId && x.BattlefieldStatus == Enums.BattlefieldStatus.Completed)
                    .Any())
                    return false;
            }

            return true;
        }

        public void UnplayChapter(Enums.Chapter chapterId)
        {
            // Set the newly unplayed chapter to available
            _chapterSave.UserRegion.ChapterHistory.RemoveAll((x) => x.ChapterID == chapterId);

            var battlefields = _chapterSave.BattlefieldRegion.Battlefields.Where((x) => x.ChapterID == chapterId);
            foreach (var battlefield in battlefields)
            {
                battlefield.BattlefieldStatus = Enums.BattlefieldStatus.Available;
            }

            // Set any chapters beating this chapter would unlock to unavailable
            var chapterData = Data.Database.Chapters.GetByID(chapterId);

            foreach (var unlockedChapterId in chapterData.UnlocksChapters)
            {
                var dependentBattlefield = _chapterSave.BattlefieldRegion.Battlefields.Where(x => x.ChapterID == unlockedChapterId).FirstOrDefault();
                if (dependentBattlefield != null) dependentBattlefield.BattlefieldStatus = Enums.BattlefieldStatus.Unavailable;
            }

            // Update the header if we just unplayed a story chapter AND if we're not in battle or in a map save
            if (_chapterSave.GetSaveFileType() == Enums.SaveFileType.Chapter &&
                _chapterSave.Header.IsBattlePrepSave == false &&
                chapterData.Type == "Story")
            {
                UpdateHeaderChapter(chapterId);
            }

            // If we just unplayed chapter 6, do some special stuff to properly reset the branch of fate
            if (chapterId == Enums.Chapter.Birthright_Chapter6 ||
                chapterId == Enums.Chapter.Conquest_Chapter6 ||
                chapterId == Enums.Chapter.Revelation_Chapter6)
            {
                _chapterSave.UserRegion.Unknown_Block2_0xE6 = 0x00;
                _chapterSave.UserRegion.Unknown_Block2_0xE7 = 0x00;
                _chapterSave.UserRegion.Unknown_Block2_0xE8 = 0x00;
                _chapterSave.UserRegion.Unknown_Block2_0xEC = 0x04;
                _chapterSave.UserRegion.Unknown_Block2_0x10A = 0x00;

                // This flag is also used to denote whether a path has been chosen
                _chapterSave.Header.UnitsGoAbsentWhenKilled = true;
                _chapterSave.UserRegion.UnitsGoAbsentWhenKilled = true;

                // Set the game to the default (Birthright)
                _chapterSave.Header.Game = Enums.Game.Birthright;
                _chapterSave.UserRegion.Game = Enums.Game.Birthright;

                // Set the branch of fate header
                UpdateHeaderChapter(Enums.Chapter.Chapter6);

                // Set the gameplay chapter
                _chapterSave.UserRegion.CurrentChapter = Enums.Chapter.Chapter6;

                // Kill the story battlefields
                RemoveRouteSpecificStoryBattlefields();
                AddBirthrightSpecificStoryBattlefields();
            }
        }

        /// <summary>
        /// Unlocks a chapter and sets it available if it's currently set to unavailable.
        /// Mainly for amiibo chapter unlocking.
        /// </summary>
        public void UnlockChapter(Enums.Chapter chapterId)
        {
            var battlefields = _chapterSave.BattlefieldRegion.Battlefields.Where((x) => x.ChapterID == chapterId);
            foreach (var battlefield in battlefields)
            {
                if (battlefield.BattlefieldStatus == Enums.BattlefieldStatus.Unavailable)
                {
                    battlefield.BattlefieldStatus = Enums.BattlefieldStatus.Available;
                }
            }
        }

        /// <summary>
        /// Update the header with the name of the given chapter
        /// </summary>
        public void UpdateHeaderChapter(Enums.Chapter chapterId)
        {
            var chapterData = Data.Database.Chapters.GetByID(chapterId);
            _chapterSave.Header.CurrentChapter = chapterId;
            _chapterSave.Header.ChapterName1 = chapterData.DisplayName1;
            _chapterSave.Header.ChapterName2 = chapterData.DisplayName2;
        }

        public void ReturnToPrologue()
        {
            ClearBattlefields();
            // Pre-branch saves use the Birthright list.
            // Obviously, this is proof that Birthright is the One True Version of Fates!
            AddBirthrightBattlefields();

            // I don't know what most of these are.
            // This is just setting them to what they're like in a legit "new" save.
            // I will list side effects as I find them in order to figure out what these are.
            // Side effect: Tutorials show for the battle prep and support menus when opening them.
            _chapterSave.UserRegion.Unknown_Block1_0x09 = 0x00;
            _chapterSave.UserRegion.Unknown_Block1_0x0A = 0x00;
            _chapterSave.UserRegion.Unknown_Block1_0x0B = 0x00;
            _chapterSave.UserRegion.Unknown_Block2_0xDF = 0x00;
            _chapterSave.UserRegion.Unknown_Block2_0xE0 = 0x00;
            _chapterSave.UserRegion.Unknown_Block2_0xE1 = 0x00;
            _chapterSave.UserRegion.Unknown_Block2_0xE2 = 0x00;
            _chapterSave.UserRegion.Unknown_Block2_0xE3 = 0x00;
            _chapterSave.UserRegion.Unknown_Block2_0xE6 = 0x01;
            _chapterSave.UserRegion.Unknown_Block2_0xE7 = 0x00;
            _chapterSave.UserRegion.Unknown_Block2_0xE8 = 0x00;
            _chapterSave.UserRegion.Unknown_Block2_0xEC = 0x00;
            _chapterSave.UserRegion.Unknown_Block2_0xED = 0x00;
            _chapterSave.UserRegion.Unknown_Block2_0x10A = 0x00;

            _chapterSave.UserRegion.Game = Enums.Game.Birthright;

            _chapterSave.UserRegion.ChapterHistory.Clear();
            _chapterSave.UserRegion.CurrentChapter = Enums.Chapter.Prologue;

            _chapterSave.UserRegion.UnitsGoAbsentWhenKilled = true;

            // This is here too
            _chapterSave.BattlefieldRegion.CurrentChapter = Enums.Chapter.Prologue;

            // Just in case
            _chapterSave.UserRegion.Gold = 0;

            // Also refresh the header with data from the body regions
            _chapterSave.Header.UnitsGoAbsentWhenKilled = true;
            _chapterSave.Header.CurrentChapter = Enums.Chapter.Prologue;
            _chapterSave.Header.Unknown_0x07 = 0x00;
            _chapterSave.Header.Unknown_0x08 = 0x00;
            _chapterSave.Header.Game = Enums.Game.Birthright;
            _chapterSave.Header.ChapterName1 = "Prologue";
            _chapterSave.Header.ChapterName2 = "Randomized";
        }

        public void ReturnToChapter7()
        {
            foreach (var battlefield in _chapterSave.BattlefieldRegion.Battlefields)
            {
                switch (battlefield.ChapterID)
                {
                    case Enums.Chapter.Prologue:
                    case Enums.Chapter.Chapter1:
                    case Enums.Chapter.Chapter2:
                    case Enums.Chapter.Chapter3:
                    case Enums.Chapter.Chapter4:
                    case Enums.Chapter.Chapter5:
                    case Enums.Chapter.Chapter6:
                    case Enums.Chapter.Birthright_Chapter6:
                    case Enums.Chapter.Conquest_Chapter6:
                    case Enums.Chapter.Revelation_Chapter6:
                        break; // Do nothing
                    case Enums.Chapter.Birthright_Chapter7:
                    case Enums.Chapter.Conquest_Chapter7:
                    case Enums.Chapter.Revelation_Chapter7:
                        battlefield.BattlefieldStatus = Enums.BattlefieldStatus.Available;
                        _chapterSave.UserRegion.ChapterHistory.RemoveAll((x) => x.ChapterID == battlefield.ChapterID);
                        break;
                    default:
                        battlefield.BattlefieldStatus = Enums.BattlefieldStatus.Unavailable;
                        _chapterSave.UserRegion.ChapterHistory.RemoveAll((x) => x.ChapterID == battlefield.ChapterID);
                        break;
                }
            }

            switch (_chapterSave.Header.Game)
            {
                case Enums.Game.Birthright:
                    _chapterSave.UserRegion.CurrentChapter = Enums.Chapter.Birthright_Chapter7;
                    break;
                case Enums.Game.Conquest:
                    _chapterSave.UserRegion.CurrentChapter = Enums.Chapter.Conquest_Chapter7;
                    break;
                case Enums.Game.Revelation:
                    _chapterSave.UserRegion.CurrentChapter = Enums.Chapter.Revelation_Chapter7;
                    break;
            }
        }

        public void ClearBattlefields()
        {
            _chapterSave.BattlefieldRegion.Battlefields.Clear();
        }

        public void AddBirthrightBattlefields()
        {
            AddBattlefield(0x00, Enums.Chapter.Chapter1);
            AddBattlefield(0x01, Enums.Chapter.Chapter2);
            AddBattlefield(0x02, Enums.Chapter.Chapter3);
            AddBattlefield(0x03, Enums.Chapter.Chapter4);
            AddBattlefield(0x04, Enums.Chapter.Chapter5);
            AddBattlefield(0x05, Enums.Chapter.Birthright_Chapter6);
            AddBattlefield(0x06, Enums.Chapter.Birthright_Chapter7);
            AddBattlefield(0x07, Enums.Chapter.Birthright_Chapter8);
            AddBattlefield(0x08, Enums.Chapter.Birthright_Chapter9);
            AddBattlefield(0x09, Enums.Chapter.Birthright_Chapter10);
            AddBattlefield(0x0A, Enums.Chapter.Birthright_Chapter11);
            AddBattlefield(0x0B, Enums.Chapter.Birthright_Chapter12);
            AddBattlefield(0x0C, Enums.Chapter.Birthright_Chapter13);
            AddBattlefield(0x0D, Enums.Chapter.Birthright_Chapter14);
            AddBattlefield(0x0E, Enums.Chapter.Birthright_Chapter15);
            AddBattlefield(0x0F, Enums.Chapter.Birthright_Chapter16);
            AddBattlefield(0x10, Enums.Chapter.Birthright_Chapter17);
            AddBattlefield(0x11, Enums.Chapter.Birthright_Chapter18);
            AddBattlefield(0x12, Enums.Chapter.Birthright_Chapter19);
            AddBattlefield(0x13, Enums.Chapter.Birthright_Chapter20);
            AddBattlefield(0x14, Enums.Chapter.Birthright_Chapter21);
            AddBattlefield(0x15, Enums.Chapter.Birthright_Chapter22);
            AddBattlefield(0x16, Enums.Chapter.Birthright_Chapter23);
            AddBattlefield(0x17, Enums.Chapter.Birthright_Chapter24);
            AddBattlefield(0x18, Enums.Chapter.Birthright_Chapter25);
            AddBattlefield(0x19, Enums.Chapter.Birthright_Chapter26);
            AddBattlefield(0x1A, Enums.Chapter.Birthright_Chapter27);
            AddBattlefield(0x1B, Enums.Chapter.Birthright_Endgame);
            AddBattlefield(0x1C, Enums.Chapter.Paralogue1);
            AddBattlefield(0x1D, Enums.Chapter.Paralogue2);
            AddBattlefield(0x1E, Enums.Chapter.Paralogue3);
            AddBattlefield(0x1F, Enums.Chapter.Paralogue4);
            AddBattlefield(0x20, Enums.Chapter.Paralogue5);
            AddBattlefield(0x21, Enums.Chapter.Paralogue6);
            AddBattlefield(0x22, Enums.Chapter.Paralogue7);
            AddBattlefield(0x23, Enums.Chapter.Paralogue8);
            AddBattlefield(0x24, Enums.Chapter.Paralogue9);
            AddBattlefield(0x25, Enums.Chapter.Paralogue10);
            AddBattlefield(0x26, Enums.Chapter.Paralogue11);
            AddBattlefield(0x27, Enums.Chapter.Paralogue12);
            AddBattlefield(0x28, Enums.Chapter.Paralogue13);
            AddBattlefield(0x29, Enums.Chapter.Paralogue14);
            AddBattlefield(0x2A, Enums.Chapter.Birthright_Invasion1);
            AddBattlefield(0x2B, Enums.Chapter.Birthright_Invasion2);
            AddBattlefield(0x2C, Enums.Chapter.Birthright_Invasion3);
            AddBattlefield(0x2D, Enums.Chapter.DragonsGate);
            AddBattlefield(0x2E, Enums.Chapter.HeroBattle_Marth);
            AddBattlefield(0x2F, Enums.Chapter.HeroBattle_Ike);
            AddBattlefield(0x30, Enums.Chapter.HeroBattle_Lucina);
            AddBattlefield(0x31, Enums.Chapter.HeroBattle_Robin);
        }

        public void AddBirthrightSpecificStoryBattlefields()
        {
            AddBattlefield(0x05, Enums.Chapter.Birthright_Chapter6);
            AddBattlefield(0x06, Enums.Chapter.Birthright_Chapter7);
            AddBattlefield(0x07, Enums.Chapter.Birthright_Chapter8);
            AddBattlefield(0x08, Enums.Chapter.Birthright_Chapter9);
            AddBattlefield(0x09, Enums.Chapter.Birthright_Chapter10);
            AddBattlefield(0x0A, Enums.Chapter.Birthright_Chapter11);
            AddBattlefield(0x0B, Enums.Chapter.Birthright_Chapter12);
            AddBattlefield(0x0C, Enums.Chapter.Birthright_Chapter13);
            AddBattlefield(0x0D, Enums.Chapter.Birthright_Chapter14);
            AddBattlefield(0x0E, Enums.Chapter.Birthright_Chapter15);
            AddBattlefield(0x0F, Enums.Chapter.Birthright_Chapter16);
            AddBattlefield(0x10, Enums.Chapter.Birthright_Chapter17);
            AddBattlefield(0x11, Enums.Chapter.Birthright_Chapter18);
            AddBattlefield(0x12, Enums.Chapter.Birthright_Chapter19);
            AddBattlefield(0x13, Enums.Chapter.Birthright_Chapter20);
            AddBattlefield(0x14, Enums.Chapter.Birthright_Chapter21);
            AddBattlefield(0x15, Enums.Chapter.Birthright_Chapter22);
            AddBattlefield(0x16, Enums.Chapter.Birthright_Chapter23);
            AddBattlefield(0x17, Enums.Chapter.Birthright_Chapter24);
            AddBattlefield(0x18, Enums.Chapter.Birthright_Chapter25);
            AddBattlefield(0x19, Enums.Chapter.Birthright_Chapter26);
            AddBattlefield(0x1A, Enums.Chapter.Birthright_Chapter27);
            AddBattlefield(0x1B, Enums.Chapter.Birthright_Endgame);
        }

        public void AddConquestBattlefields()
        {
            AddBattlefield(0x00, Enums.Chapter.Chapter1);
            AddBattlefield(0x01, Enums.Chapter.Chapter2);
            AddBattlefield(0x02, Enums.Chapter.Chapter3);
            AddBattlefield(0x03, Enums.Chapter.Chapter4);
            AddBattlefield(0x04, Enums.Chapter.Chapter5);
            AddBattlefield(0x05, Enums.Chapter.Conquest_Chapter6);
            AddBattlefield(0x06, Enums.Chapter.Conquest_Chapter7);
            AddBattlefield(0x07, Enums.Chapter.Conquest_Chapter8);
            AddBattlefield(0x08, Enums.Chapter.Conquest_Chapter9);
            AddBattlefield(0x09, Enums.Chapter.Conquest_Chapter10);
            AddBattlefield(0x0A, Enums.Chapter.Conquest_Chapter11);
            AddBattlefield(0x0B, Enums.Chapter.Conquest_Chapter12);
            AddBattlefield(0x0C, Enums.Chapter.Conquest_Chapter13);
            AddBattlefield(0x0D, Enums.Chapter.Conquest_Chapter14);
            AddBattlefield(0x0E, Enums.Chapter.Conquest_Chapter15);
            AddBattlefield(0x0F, Enums.Chapter.Conquest_Chapter16);
            AddBattlefield(0x10, Enums.Chapter.Conquest_Chapter17);
            AddBattlefield(0x11, Enums.Chapter.Conquest_Chapter18);
            AddBattlefield(0x12, Enums.Chapter.Conquest_Chapter19);
            AddBattlefield(0x13, Enums.Chapter.Conquest_Chapter20);
            AddBattlefield(0x14, Enums.Chapter.Conquest_Chapter21);
            AddBattlefield(0x15, Enums.Chapter.Conquest_Chapter22);
            AddBattlefield(0x16, Enums.Chapter.Conquest_Chapter23);
            AddBattlefield(0x17, Enums.Chapter.Conquest_Chapter24);
            AddBattlefield(0x18, Enums.Chapter.Conquest_Chapter25);
            AddBattlefield(0x19, Enums.Chapter.Conquest_Chapter26);
            AddBattlefield(0x1A, Enums.Chapter.Conquest_Chapter27);
            AddBattlefield(0x1B, Enums.Chapter.Conquest_Endgame);
            AddBattlefield(0x1C, Enums.Chapter.Paralogue1);
            AddBattlefield(0x1D, Enums.Chapter.Paralogue2);
            AddBattlefield(0x1E, Enums.Chapter.Paralogue3);
            AddBattlefield(0x1F, Enums.Chapter.Paralogue4);
            AddBattlefield(0x20, Enums.Chapter.Paralogue5);
            AddBattlefield(0x21, Enums.Chapter.Paralogue6);
            AddBattlefield(0x22, Enums.Chapter.Paralogue15);
            AddBattlefield(0x23, Enums.Chapter.Paralogue16);
            AddBattlefield(0x24, Enums.Chapter.Paralogue17);
            AddBattlefield(0x25, Enums.Chapter.Paralogue18);
            AddBattlefield(0x26, Enums.Chapter.Paralogue19);
            AddBattlefield(0x27, Enums.Chapter.Paralogue20);
            AddBattlefield(0x28, Enums.Chapter.Paralogue21);
            AddBattlefield(0x29, Enums.Chapter.Paralogue22);
            AddBattlefield(0x2A, Enums.Chapter.Conquest_Invasion1);
            AddBattlefield(0x2B, Enums.Chapter.Conquest_Invasion2);
            AddBattlefield(0x2C, Enums.Chapter.Conquest_Invasion3);
            AddBattlefield(0x2D, Enums.Chapter.DragonsGate);
            AddBattlefield(0x2E, Enums.Chapter.HeroBattle_Marth);
            AddBattlefield(0x2F, Enums.Chapter.HeroBattle_Ike);
            AddBattlefield(0x30, Enums.Chapter.HeroBattle_Lucina);
            AddBattlefield(0x31, Enums.Chapter.HeroBattle_Robin);
        }

        public void AddRevelationBattlefields()
        {
            AddBattlefield(0x00, Enums.Chapter.Chapter1);
            AddBattlefield(0x01, Enums.Chapter.Chapter2);
            AddBattlefield(0x02, Enums.Chapter.Chapter3);
            AddBattlefield(0x03, Enums.Chapter.Chapter4);
            AddBattlefield(0x04, Enums.Chapter.Chapter5);
            AddBattlefield(0x05, Enums.Chapter.Revelation_Chapter6);
            AddBattlefield(0x06, Enums.Chapter.Revelation_Chapter7);
            AddBattlefield(0x07, Enums.Chapter.Revelation_Chapter8);
            AddBattlefield(0x08, Enums.Chapter.Revelation_Chapter9);
            AddBattlefield(0x09, Enums.Chapter.Revelation_Chapter10);
            AddBattlefield(0x0A, Enums.Chapter.Revelation_Chapter11);
            AddBattlefield(0x0B, Enums.Chapter.Revelation_Chapter12);
            AddBattlefield(0x0C, Enums.Chapter.Revelation_Chapter13);
            AddBattlefield(0x0D, Enums.Chapter.Revelation_Chapter14);
            AddBattlefield(0x0E, Enums.Chapter.Revelation_Chapter15);
            AddBattlefield(0x0F, Enums.Chapter.Revelation_Chapter16);
            AddBattlefield(0x10, Enums.Chapter.Revelation_Chapter17);
            AddBattlefield(0x11, Enums.Chapter.Revelation_Chapter18);
            AddBattlefield(0x12, Enums.Chapter.Revelation_Chapter19);
            AddBattlefield(0x13, Enums.Chapter.Revelation_Chapter20);
            AddBattlefield(0x14, Enums.Chapter.Revelation_Chapter21);
            AddBattlefield(0x15, Enums.Chapter.Revelation_Chapter22);
            AddBattlefield(0x16, Enums.Chapter.Revelation_Chapter23);
            AddBattlefield(0x17, Enums.Chapter.Revelation_Chapter24);
            AddBattlefield(0x18, Enums.Chapter.Revelation_Chapter25);
            AddBattlefield(0x19, Enums.Chapter.Revelation_Chapter26);
            AddBattlefield(0x1A, Enums.Chapter.Revelation_Chapter27);
            AddBattlefield(0x1B, Enums.Chapter.Revelation_Endgame);
            AddBattlefield(0x1C, Enums.Chapter.Paralogue1);
            AddBattlefield(0x1D, Enums.Chapter.Paralogue2);
            AddBattlefield(0x1E, Enums.Chapter.Paralogue3);
            AddBattlefield(0x1F, Enums.Chapter.Paralogue4);
            AddBattlefield(0x20, Enums.Chapter.Paralogue5);
            AddBattlefield(0x21, Enums.Chapter.Paralogue6);
            AddBattlefield(0x22, Enums.Chapter.Paralogue7);
            AddBattlefield(0x23, Enums.Chapter.Paralogue8);
            AddBattlefield(0x24, Enums.Chapter.Paralogue9);
            AddBattlefield(0x25, Enums.Chapter.Paralogue10);
            AddBattlefield(0x26, Enums.Chapter.Paralogue11);
            AddBattlefield(0x27, Enums.Chapter.Paralogue12);
            AddBattlefield(0x28, Enums.Chapter.Paralogue13);
            AddBattlefield(0x29, Enums.Chapter.Paralogue14);
            AddBattlefield(0x2A, Enums.Chapter.Paralogue15);
            AddBattlefield(0x2B, Enums.Chapter.Paralogue16);
            AddBattlefield(0x2C, Enums.Chapter.Paralogue17);
            AddBattlefield(0x2D, Enums.Chapter.Paralogue18);
            AddBattlefield(0x2E, Enums.Chapter.Paralogue19);
            AddBattlefield(0x2F, Enums.Chapter.Paralogue20);
            AddBattlefield(0x30, Enums.Chapter.Paralogue21);
            AddBattlefield(0x31, Enums.Chapter.Paralogue22);
            AddBattlefield(0x32, Enums.Chapter.Revelation_Invasion1);
            AddBattlefield(0x33, Enums.Chapter.Revelation_Invasion2);
            AddBattlefield(0x34, Enums.Chapter.Revelation_Invasion3);
            // Yes, these battlefieldIds are repeated in Revelation
            AddBattlefield(0x2D, Enums.Chapter.DragonsGate);
            AddBattlefield(0x2E, Enums.Chapter.HeroBattle_Marth);
            AddBattlefield(0x2F, Enums.Chapter.HeroBattle_Ike);
            AddBattlefield(0x30, Enums.Chapter.HeroBattle_Lucina);
            AddBattlefield(0x31, Enums.Chapter.HeroBattle_Robin);
        }

        private void RemoveRouteSpecificStoryBattlefields()
        {
            RemoveBattlefield(Enums.Chapter.Birthright_Chapter6);
            RemoveBattlefield(Enums.Chapter.Birthright_Chapter7);
            RemoveBattlefield(Enums.Chapter.Birthright_Chapter8);
            RemoveBattlefield(Enums.Chapter.Birthright_Chapter9);
            RemoveBattlefield(Enums.Chapter.Birthright_Chapter10);
            RemoveBattlefield(Enums.Chapter.Birthright_Chapter11);
            RemoveBattlefield(Enums.Chapter.Birthright_Chapter12);
            RemoveBattlefield(Enums.Chapter.Birthright_Chapter13);
            RemoveBattlefield(Enums.Chapter.Birthright_Chapter14);
            RemoveBattlefield(Enums.Chapter.Birthright_Chapter15);
            RemoveBattlefield(Enums.Chapter.Birthright_Chapter16);
            RemoveBattlefield(Enums.Chapter.Birthright_Chapter17);
            RemoveBattlefield(Enums.Chapter.Birthright_Chapter18);
            RemoveBattlefield(Enums.Chapter.Birthright_Chapter19);
            RemoveBattlefield(Enums.Chapter.Birthright_Chapter20);
            RemoveBattlefield(Enums.Chapter.Birthright_Chapter21);
            RemoveBattlefield(Enums.Chapter.Birthright_Chapter22);
            RemoveBattlefield(Enums.Chapter.Birthright_Chapter23);
            RemoveBattlefield(Enums.Chapter.Birthright_Chapter24);
            RemoveBattlefield(Enums.Chapter.Birthright_Chapter25);
            RemoveBattlefield(Enums.Chapter.Birthright_Chapter26);
            RemoveBattlefield(Enums.Chapter.Birthright_Chapter27);
            RemoveBattlefield(Enums.Chapter.Birthright_Endgame);
            RemoveBattlefield(Enums.Chapter.Conquest_Chapter6);
            RemoveBattlefield(Enums.Chapter.Conquest_Chapter7);
            RemoveBattlefield(Enums.Chapter.Conquest_Chapter8);
            RemoveBattlefield(Enums.Chapter.Conquest_Chapter9);
            RemoveBattlefield(Enums.Chapter.Conquest_Chapter10);
            RemoveBattlefield(Enums.Chapter.Conquest_Chapter11);
            RemoveBattlefield(Enums.Chapter.Conquest_Chapter12);
            RemoveBattlefield(Enums.Chapter.Conquest_Chapter13);
            RemoveBattlefield(Enums.Chapter.Conquest_Chapter14);
            RemoveBattlefield(Enums.Chapter.Conquest_Chapter15);
            RemoveBattlefield(Enums.Chapter.Conquest_Chapter16);
            RemoveBattlefield(Enums.Chapter.Conquest_Chapter17);
            RemoveBattlefield(Enums.Chapter.Conquest_Chapter18);
            RemoveBattlefield(Enums.Chapter.Conquest_Chapter19);
            RemoveBattlefield(Enums.Chapter.Conquest_Chapter20);
            RemoveBattlefield(Enums.Chapter.Conquest_Chapter21);
            RemoveBattlefield(Enums.Chapter.Conquest_Chapter22);
            RemoveBattlefield(Enums.Chapter.Conquest_Chapter23);
            RemoveBattlefield(Enums.Chapter.Conquest_Chapter24);
            RemoveBattlefield(Enums.Chapter.Conquest_Chapter25);
            RemoveBattlefield(Enums.Chapter.Conquest_Chapter26);
            RemoveBattlefield(Enums.Chapter.Conquest_Chapter27);
            RemoveBattlefield(Enums.Chapter.Conquest_Endgame);
            RemoveBattlefield(Enums.Chapter.Revelation_Chapter6);
            RemoveBattlefield(Enums.Chapter.Revelation_Chapter7);
            RemoveBattlefield(Enums.Chapter.Revelation_Chapter8);
            RemoveBattlefield(Enums.Chapter.Revelation_Chapter9);
            RemoveBattlefield(Enums.Chapter.Revelation_Chapter10);
            RemoveBattlefield(Enums.Chapter.Revelation_Chapter11);
            RemoveBattlefield(Enums.Chapter.Revelation_Chapter12);
            RemoveBattlefield(Enums.Chapter.Revelation_Chapter13);
            RemoveBattlefield(Enums.Chapter.Revelation_Chapter14);
            RemoveBattlefield(Enums.Chapter.Revelation_Chapter15);
            RemoveBattlefield(Enums.Chapter.Revelation_Chapter16);
            RemoveBattlefield(Enums.Chapter.Revelation_Chapter17);
            RemoveBattlefield(Enums.Chapter.Revelation_Chapter18);
            RemoveBattlefield(Enums.Chapter.Revelation_Chapter19);
            RemoveBattlefield(Enums.Chapter.Revelation_Chapter20);
            RemoveBattlefield(Enums.Chapter.Revelation_Chapter21);
            RemoveBattlefield(Enums.Chapter.Revelation_Chapter22);
            RemoveBattlefield(Enums.Chapter.Revelation_Chapter23);
            RemoveBattlefield(Enums.Chapter.Revelation_Chapter24);
            RemoveBattlefield(Enums.Chapter.Revelation_Chapter25);
            RemoveBattlefield(Enums.Chapter.Revelation_Chapter26);
            RemoveBattlefield(Enums.Chapter.Revelation_Chapter27);
            RemoveBattlefield(Enums.Chapter.Revelation_Endgame);
        }

        private void RemoveBattlefield(Enums.Chapter chapterId)
        {
            var battlefield = _chapterSave.BattlefieldRegion.Battlefields.Where(x => x.ChapterID == chapterId).FirstOrDefault();
            if (battlefield != null) _chapterSave.BattlefieldRegion.Battlefields.Remove(battlefield);
        }

        private void AddBattlefield(byte battlefieldId, Enums.Chapter chapterId)
        {
            var raw = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF };
            var battlefield = new Battlefield(raw);
            battlefield.BattlefieldID = battlefieldId;
            battlefield.ChapterID = chapterId;
            _chapterSave.BattlefieldRegion.Battlefields.Add(battlefield);
        }
    }
}
