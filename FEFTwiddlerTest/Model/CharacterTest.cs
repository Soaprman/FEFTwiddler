using System.IO;
using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FEFTwiddler.Model;

namespace FEFTwiddlerTest.Model
{
    [TestClass]
    public class CharacterTest
    {
        [TestMethod]
        public void GetModifiedMaxLevel_Prepromote_Works()
        {
            byte expected; byte actual;

            var character = Character.Create();
            character.CharacterID = FEFTwiddler.Enums.Character.Felicia;
            character.ClassID = FEFTwiddler.Enums.Class.Maid;
            character.InternalLevel = 0;

            character.EternalSealsUsed = 0;

            expected = 40;
            actual = character.GetModifiedMaxLevel();
            Assert.AreEqual(expected, actual);

            character.EternalSealsUsed = 1;

            expected = 45;
            actual = character.GetModifiedMaxLevel();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetModifiedMaxLevel_Promoted_Works()
        {
            byte expected; byte actual;

            var character = Character.Create();
            character.CharacterID = FEFTwiddler.Enums.Character.Flora;
            character.ClassID = FEFTwiddler.Enums.Class.Maid;
            character.InternalLevel = 20;

            character.EternalSealsUsed = 0;

            expected = 20;
            actual = character.GetModifiedMaxLevel();
            Assert.AreEqual(expected, actual);

            character.EternalSealsUsed = 1;

            expected = 25;
            actual = character.GetModifiedMaxLevel();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetModifiedMaxLevel_Unpromoted_Works()
        {
            byte expected; byte actual;

            var character = Character.Create();
            character.CharacterID = FEFTwiddler.Enums.Character.Elise;
            character.ClassID = FEFTwiddler.Enums.Class.Troubadour_F;
            character.InternalLevel = 0;

            character.EternalSealsUsed = 0;

            expected = 20;
            actual = character.GetModifiedMaxLevel();
            Assert.AreEqual(expected, actual);

            character.EternalSealsUsed = 1;

            expected = 20;
            actual = character.GetModifiedMaxLevel();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetMinimumEternalSealsForCurrentLevel_Prepromote_Works()
        {
            byte expected; byte actual;

            var character = Character.Create();
            character.CharacterID = FEFTwiddler.Enums.Character.Felicia;
            character.ClassID = FEFTwiddler.Enums.Class.Maid;
            character.InternalLevel = 0;

            character.Level = 1;

            expected = 0;
            actual = character.GetMinimumEternalSealsForCurrentLevel();
            Assert.AreEqual(expected, actual);

            character.Level = 40;

            expected = 0;
            actual = character.GetMinimumEternalSealsForCurrentLevel();
            Assert.AreEqual(expected, actual);

            character.Level = 41;

            expected = 1;
            actual = character.GetMinimumEternalSealsForCurrentLevel();
            Assert.AreEqual(expected, actual);

            character.Level = 45;

            expected = 1;
            actual = character.GetMinimumEternalSealsForCurrentLevel();
            Assert.AreEqual(expected, actual);

            character.Level = 46;

            expected = 2;
            actual = character.GetMinimumEternalSealsForCurrentLevel();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetMinimumEternalSealsForCurrentLevel_Promoted_Works()
        {
            byte expected; byte actual;

            var character = Character.Create();
            character.CharacterID = FEFTwiddler.Enums.Character.Flora;
            character.ClassID = FEFTwiddler.Enums.Class.Maid;
            character.InternalLevel = 20;

            character.Level = 1;

            expected = 0;
            actual = character.GetMinimumEternalSealsForCurrentLevel();
            Assert.AreEqual(expected, actual);

            character.Level = 20;

            expected = 0;
            actual = character.GetMinimumEternalSealsForCurrentLevel();
            Assert.AreEqual(expected, actual);

            character.Level = 21;

            expected = 1;
            actual = character.GetMinimumEternalSealsForCurrentLevel();
            Assert.AreEqual(expected, actual);

            character.Level = 25;

            expected = 1;
            actual = character.GetMinimumEternalSealsForCurrentLevel();
            Assert.AreEqual(expected, actual);

            character.Level = 26;

            expected = 2;
            actual = character.GetMinimumEternalSealsForCurrentLevel();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetMinimumEternalSealsForCurrentLevel_Unpromoted_Works()
        {
            byte expected; byte actual;

            var character = Character.Create();
            character.CharacterID = FEFTwiddler.Enums.Character.Elise;
            character.ClassID = FEFTwiddler.Enums.Class.Troubadour_F;
            character.InternalLevel = 0;

            character.Level = 1;

            expected = 0;
            actual = character.GetMinimumEternalSealsForCurrentLevel();
            Assert.AreEqual(expected, actual);

            character.Level = 20;

            expected = 0;
            actual = character.GetMinimumEternalSealsForCurrentLevel();
            Assert.AreEqual(expected, actual);
        }
    }
}
