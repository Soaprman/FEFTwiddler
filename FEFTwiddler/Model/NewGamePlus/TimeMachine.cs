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
            foreach (var character in _chapterSave.UnitRegion.Units)
            {
                if (!Data.Database.Characters.GetByID(character.CharacterID).IsCorrin)
                {
                    _chapterSave.UnitRegion.Units.Remove(character);
                }
            }
        }

        public void InsertCharacters()
        {
            var characterDatas = Data.Database.Characters.GetAllNamedPlayable().Where((x) => !x.IsCorrin);
            foreach (var characterData in characterDatas)
            {
                var character = Model.Character.Create(characterData.CharacterID);
                character.IsAbsent = true;
                _chapterSave.UnitRegion.Units.Add(character);
            }
        }

        // TODO: Level up characters to where they should be
        // Required: Growth rates in database

        public void UnplayChapter(Enums.Chapter chapterId)
        {
            _chapterSave.UserRegion.ChapterHistory.RemoveAll((x) => x.ChapterID == chapterId);

            var battlefields = _chapterSave.BattlefieldRegion.Battlefields.Where((x) => x.ChapterID == chapterId);
            foreach (var battlefield in battlefields)
            {
                battlefield.BattlefieldStatus = Enums.BattlefieldStatus.Available;
            }
        }

        private void ReturnToChapter7()
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
                        break;
                    default:
                        battlefield.BattlefieldStatus = Enums.BattlefieldStatus.Unavailable;
                        break;
                }
            }
        }
    }
}
