using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using FEFTwiddler.Utils;

namespace FEFTwiddlerTest.Utils
{
    [TestClass]
    public class TypeConverterTest
    {
        [TestMethod]
        public void ToByteArray_Works()
        {
            var str = "Soap";
            var numberOfCharacters = 12;

            var expected = new byte[] {
                0x53, 0x00, 0x6F, 0x00, 0x61, 0x00, 0x70, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            var actual = TypeConverter.ToByteArray(str, numberOfCharacters);

            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }

        [TestMethod]
        public void ToString_Works()
        {
            var byteArray = new byte[] {
                0x53, 0x00, 0x6F, 0x00, 0x61, 0x00, 0x70, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

            var expected = "Soap";
            var actual = TypeConverter.ToString(byteArray);

            Assert.AreEqual(expected, actual);
        }
    }
}
