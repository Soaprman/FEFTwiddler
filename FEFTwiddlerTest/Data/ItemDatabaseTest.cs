using Microsoft.VisualStudio.TestTools.UnitTesting;
using FEFTwiddler.Data;
using FEFTwiddler.Enums;

namespace FEFTwiddlerTest.Data
{
    [TestClass]
    public class ItemDatabaseTest
    {
        private ItemDatabase GetInstance()
        {
            return new ItemDatabase(FEFTwiddler.Enums.Language.English);
        }

        [TestMethod]
        public void GetByID_Works()
        {
            var db = GetInstance();

            FEFTwiddler.Enums.Item itemId = FEFTwiddler.Enums.Item.BronzeSword;
            var itemData = db.GetByID(itemId);

            Assert.AreEqual(FEFTwiddler.Enums.Item.BronzeSword, itemData.ItemID);
            Assert.AreEqual("Bronze Sword", itemData.DisplayName);
            Assert.AreEqual(0, itemData.MaximumUses);
            Assert.AreEqual(ItemType.Sword, itemData.Type);
            Assert.AreEqual(false, itemData.IsNpcOnly);
        }

    }
}
