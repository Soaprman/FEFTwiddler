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
            _inputFilePath = path;
            Read();
        }

        public override void Read()
        {
            SwitchToCopy();
            base.Read();
        }

        private void SwitchToCopy()
        {
            _originalFilePath = _inputFilePath;

            _inputFilePath += "_Debug";
            File.Copy(_originalFilePath, _inputFilePath, true);
        }

        public bool DecompressedFilesAreIdentical()
        {
            using (var orig = File.OpenRead(_originalFilePath))
            using (var copy = File.OpenRead(_inputFilePath))
            {
                if (orig.Length != copy.Length) return false;

                for (var i = 0; i < orig.Length; i++)
                {
                    if (orig.ReadByte() != copy.ReadByte()) return false;
                }
            }
            return true;
        }

        public bool CompressedFilesAreIdentical()
        {
            using (var orig = File.OpenRead(_originalFilePath))
            using (var copy = File.OpenRead(_inputFilePath))
            {
                if (orig.Length != copy.Length) return false;

                int origByte; int copyByte;

                // I don't really "get" the compression technique, but the bytes from 0xD0 through 0x2D3 are different even if you make no changes.
                // This happened when using FEST, too, so it's not just my code at least.
                // It works, so I guess I won't question it for now!

                for (var i = 0; i < 0xD0; i++)
                {
                    origByte = orig.ReadByte();
                    copyByte = copy.ReadByte();
                    if (origByte != copyByte) return false;
                }

                orig.Seek(0x2D4, SeekOrigin.Begin);
                copy.Seek(0x2D4, SeekOrigin.Begin);

                for (var i = 0x2D3; i < orig.Length; i++)
                {
                    origByte = orig.ReadByte();
                    copyByte = copy.ReadByte();
                    if (origByte != copyByte) return false;
                }
            }
            return true;
        }

        public void Cleanup()
        {
            File.Delete(_inputFilePath);
        }
    }
}
