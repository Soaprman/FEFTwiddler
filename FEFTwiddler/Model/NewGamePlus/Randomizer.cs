using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FEFTwiddler.Extensions;

namespace FEFTwiddler.Model.NewGamePlus
{
    public class Randomizer
    {
        private Random _rng;
        private Model.ChapterSave _chapterSave;

        public Randomizer(Model.ChapterSave chapterSave)
        {
            _rng = new Random();
            _chapterSave = chapterSave;
        }

        public void RandomizeClasses()
        {
            foreach (var character in _chapterSave.UnitRegion.Units)
            {
                character.ClassID = GetRandomClass();
            }
        }

        public Enums.Class GetRandomClass()
        {
            // TODO: Restrict by gender
            var classes = Data.Database.Classes.GetAll().Where((x) => (byte)x.ClassID > 0x00 && (byte)x.ClassID < 0x6A);
            return classes.RandomElement(_rng).ClassID;
        }

        // TODO: Give weapons + items based on class
    }
}
