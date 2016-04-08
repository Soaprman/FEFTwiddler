using System;
using System.IO;

namespace FEFTwiddlerTest.Model
{
    public class SaveFile : FEFTwiddler.Model.SaveFile
    {
        private string _originalFilePath;

        public static new SaveFile FromPath(string path)
        {
            var saveFile = new SaveFile();
            saveFile.ReadFromPath(path);
            return saveFile;
        }

        private void ReadFromPath(string path)
        {
            _filePath = path;
            Read();
        }

        public override void Read()
        {
            SwitchToCopy();
            base.Read();
        }

        private void SwitchToCopy()
        {
            _originalFilePath = _filePath;

            _filePath += "_Debug";
            File.Copy(_originalFilePath, _filePath, true);
        }

        public bool AreOriginalAndCopyTheSame()
        {
            using (var orig = File.OpenRead(_originalFilePath))
            using (var copy = File.OpenRead(_filePath))
            {
                if (orig.Length != copy.Length) return false;

                for (var i = 0; i < orig.Length; i++)
                {
                    if (orig.ReadByte() != copy.ReadByte()) return false;
                }
            }
            return true;
        }

        public void Cleanup()
        {
            File.Delete(_filePath);
        }
    }
}
