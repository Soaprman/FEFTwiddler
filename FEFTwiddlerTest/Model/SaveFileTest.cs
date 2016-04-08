using System.IO;
using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FEFTwiddlerTest.Model
{
    [TestClass]
    public class SaveFileTest
    {
        [TestMethod]
        public void SaveFile_Decompressed_NoChangesMade_FileDoesNotChange()
        {
            var savePaths = GetChapterTestSaves();
            foreach (var path in savePaths)
            {
                var sf = FEFTwiddlerTest.Model.SaveFile.FromPath(path);
                var cs = FEFTwiddler.Model.ChapterSave.FromSaveFile(sf);

                cs.Write();
                
                Assert.IsTrue(sf.AreOriginalAndCopyTheSame(), "File changed when it shouldn't have: " + path);

                sf.Cleanup();
            }
        }

        private string[] GetChapterTestSaves()
        {
            var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return Directory.GetFiles(dir + "\\TestSaves\\Chapter");
        }
    }
}
