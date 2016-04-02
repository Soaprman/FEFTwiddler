using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Xml.Linq;
using FEFTwiddler.Extensions;

namespace FEFTwiddlerTest.Extensions
{
    [TestClass]
    public class XElementExtensionsTest
    {
        [TestMethod]
        public void GetAttribute_HandlesUshort()
        {
            var xml = "<item id=\"277\" displayName=\"Vulnerary\" type=\"Consumable\" maxUses=\"3\" />";
            var xe = XElement.Parse(xml);

            ushort expected = 0x115;
            var actual = xe.GetAttribute<ushort>("id");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetAttribute_HandlesDefaultValues_Specified()
        {
            var xml = "<item id=\"277\" displayName=\"Vulnerary\" type=\"Consumable\" maxUses=\"3\" />";
            var xe = XElement.Parse(xml);

            ushort expected = 0x115;
            var actual = xe.GetAttribute<ushort>("doesntExist", 0x115);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetAttribute_HandlesDefaultValues_NotSpecified()
        {
            var xml = "<item id=\"277\" displayName=\"Vulnerary\" type=\"Consumable\" maxUses=\"3\" />";
            var xe = XElement.Parse(xml);

            byte expected = 0;
            var actual = xe.GetAttribute<byte>("doesntExist");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetAttribute_HandlesDefaultValues_TreatsEmptyStringAsDefault()
        {
            var xml = "<item id=\"1\" displayName=\"Bronze Sword\" type=\"Sword\" maxUses=\"\" />";
            var xe = XElement.Parse(xml);

            byte expected = 0;
            var actual = xe.GetAttribute<byte>("maxUses");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetAttribute_HandlesNullableTypes()
        {
            var xml = "<item id=\"277\" displayName=\"Vulnerary\" type=\"Consumable\" maxUses=\"3\" />";
            var xe = XElement.Parse(xml);

            int? expected = null;
            var actual = xe.GetAttribute<int?>("doesntExist");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetAttribute_HandlesString()
        {
            var xml = "<item id=\"277\" displayName=\"Vulnerary\" type=\"Consumable\" maxUses=\"3\" />";
            var xe = XElement.Parse(xml);

            string expected = "Vulnerary";
            var actual = xe.GetAttribute("displayName");

            Assert.AreEqual(expected, actual);
        }
    }
}
