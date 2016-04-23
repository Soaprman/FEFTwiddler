using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEFTwiddler.Model.NewGamePlus
{
    public class TimeMachine
    {
        private Model.ChapterSave _chapterSave;

        public TimeMachine(Model.ChapterSave chapterSave)
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
                var unit = Model.Character.Create(characterData.CharacterID);
                unit.IsAbsent = true;
                _chapterSave.UnitRegion.Units.Add(unit);
            }
        }

        public void LevelUpAllUnits()
        {
            foreach (var unit in _chapterSave.UnitRegion.Units)
            {
                // TODO: Remove fixed value, determine based on which game is being played
                LevelUp(unit, 19);
            }
        }

        // TODO: Revert Corrin
        // Remove marriage byte (otherwise paralogue 2 stays available)

        /// <summary>
        /// Level a unit, giving them the average gains that their growth rates would dictate
        /// </summary>
        public void LevelUp(Model.Character unit, int levels)
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

        public void UnplayChapter(Enums.Chapter chapterId)
        {
            _chapterSave.UserRegion.ChapterHistory.RemoveAll((x) => x.ChapterID == chapterId);

            var battlefields = _chapterSave.BattlefieldRegion.Battlefields.Where((x) => x.ChapterID == chapterId);
            foreach (var battlefield in battlefields)
            {
                battlefield.BattlefieldStatus = Enums.BattlefieldStatus.Available;
            }
        }

        public void ReturnToPrologue()
        {
            foreach (var battlefield in _chapterSave.BattlefieldRegion.Battlefields)
            {
                battlefield.BattlefieldStatus = Enums.BattlefieldStatus.Unavailable;
                battlefield.Unknown_0x05 = 0xFF;
                battlefield.Unknown_0x06 = 0xFF;
                battlefield.RemoveEnemies();
            }
            _chapterSave.UserRegion.Unknown_Block2_0xDF = 0x00;
            _chapterSave.UserRegion.Unknown_Block2_0xE0 = 0x00;
            _chapterSave.UserRegion.Unknown_Block2_0xE1 = 0x00;
            _chapterSave.UserRegion.Unknown_Block2_0xE2 = 0x00;
            _chapterSave.UserRegion.Unknown_Block2_0xE3 = 0x00;
            _chapterSave.UserRegion.Unknown_Block2_0xEC = 0x00;
            _chapterSave.UserRegion.Unknown_Block2_0xED = 0x00;
            _chapterSave.UserRegion.Unknown_Block2_0xFA = 0x5E;
            _chapterSave.UserRegion.Unknown_Block2_0xFB = 0x5E;
            _chapterSave.UserRegion.Unknown_Block2_0xFE = 0x01;
            _chapterSave.UserRegion.Unknown_Block2_0x101 = 0x01;
            _chapterSave.UserRegion.Unknown_Block2_0x102 = 0x02;
            _chapterSave.UserRegion.Unknown_Block2_0x10A = 0x00;
            _chapterSave.UserRegion.ChapterHistory.Clear();
            _chapterSave.UserRegion.CurrentChapter = Enums.Chapter.Prologue;
            _chapterSave.Header.CurrentChapter = Enums.Chapter.Prologue;
            _chapterSave.Header.Unknown_0x07 = 0x00;
            _chapterSave.Header.Unknown_0x08 = 0x00;
            _chapterSave.Header.ChapterName = "Randomized New Game";
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
    }
}
