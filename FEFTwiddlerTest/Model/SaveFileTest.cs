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
            var savePaths = GetChapterDecompressedTestSaves();
            foreach (var path in savePaths)
            {
                var sf = FEFTwiddlerTest.Model.SaveFile.FromPath(path);
                var cs = FEFTwiddler.Model.ChapterSave.FromSaveFile(sf);

                cs.Write();
                
                Assert.IsTrue(sf.DecompressedFilesAreIdentical(), "File changed when it shouldn't have: " + path);

                sf.Cleanup();
            }
        }

        private string[] GetChapterDecompressedTestSaves()
        {
            var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return Directory.GetFiles(dir + "\\TestSaves\\Chapter\\Decompressed");
        }

        [TestMethod]
        public void SaveFile_Compressed_NoChangesMade_FileDoesNotChange()
        {
            var savePaths = GetChapterCompressedTestSaves();
            foreach (var path in savePaths)
            {
                var sf = FEFTwiddlerTest.Model.SaveFile.FromPath(path);
                var cs = FEFTwiddler.Model.ChapterSave.FromSaveFile(sf);

                cs.Write();

                Assert.IsTrue(sf.CompressedFilesAreIdentical(), "File changed when it shouldn't have: " + path);

                sf.Cleanup();
            }
        }

        private string[] GetChapterCompressedTestSaves()
        {
            var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return Directory.GetFiles(dir + "\\TestSaves\\Chapter\\Compressed");
        }
    }
}
