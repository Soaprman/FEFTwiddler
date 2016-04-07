using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using FEFTwiddler.Extensions;

namespace FEFTwiddlerTest.Extensions
{
    [TestClass]
    public class ByteArrayExtensionsTest
    {
        [TestMethod]
        public void Or_ArraysAreSameSize_Works()
        {
            var these = new byte[] { 0x01, 0x01, 0x01 };
            var those = new byte[] { 0x02, 0x04, 0x08 };

            var expected = new byte[] { 0x03, 0x05, 0x09 };
            var actual = these.Or(those);

            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }

        [TestMethod]
        public void Or_TheseAreBigger_Works()
        {
            var these = new byte[] { 0x01, 0x01, 0x01, 0x01 };
            var those = new byte[] { 0x02, 0x04, 0x08 };

            var expected = new byte[] { 0x03, 0x05, 0x09, 0x01 };
            var actual = these.Or(those);

            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }

        [TestMethod]
        public void Or_ThoseAreBigger_Works()
        {
            var these = new byte[] { 0x01, 0x01, 0x01 };
            var those = new byte[] { 0x02, 0x04, 0x08, 0x10 };

            var expected = new byte[] { 0x03, 0x05, 0x09 };
            var actual = these.Or(those);

            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException), "")]
        public void Or_ThoseIsNull_Throws()
        {
            var these = new byte[] { 0x01, 0x01, 0x01 };

            var actual = these.Or(null);
        }
    }
}
