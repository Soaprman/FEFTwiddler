using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using FEFTwiddler.Extensions;

namespace FEFTwiddlerTest.Extensions
{
    [TestClass]
    public class StringExtensionsTest
    {
        [TestMethod]
        public void Right_Works()
        {
            var str = "Basketball";

            var expected = "ball";
            var actual = str.Right(4);

            Assert.AreEqual(expected, actual);
        }
    }
}
