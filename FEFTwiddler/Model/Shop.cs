using System.Collections.Generic;

namespace FEFTwiddler.Model
{
    public class Shop
    {
        public byte Level { get; set; }

        private List<ShopItem> _items = new List<ShopItem>();
        public List<ShopItem> Items
        {
            get { return _items; }
        }
    }
}
