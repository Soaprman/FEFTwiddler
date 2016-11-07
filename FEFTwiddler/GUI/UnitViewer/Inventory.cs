using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Windows.Forms;
using FEFTwiddler.Enums;
using FEFTwiddler.Extensions;
using FEFTwiddler.Model;

namespace FEFTwiddler.GUI.UnitViewer
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class Inventory : UserControl
    {
        private Model.Unit _unit;
        private ItemPanel[] _inventory;

        public Inventory()
        {
            InitializeComponent();
            InitializeControls();
        }

        public void LoadUnit(Model.Unit unit)
        {
            _unit = unit;
            PopulateControls();
        }

        private void InitializeControls()
        {
            _inventory = new ItemPanel[5];
            _inventory[0] = new ItemPanel(null,
                ItemPic_1, ItemNameBox_1, ItemIsEquipped_1,
                ItemForgesBox_1, ItemQuantBox_1, ItemHexBox_1);
            _inventory[1] = new ItemPanel(null,
                ItemPic_2, ItemNameBox_2, ItemIsEquipped_2,
                ItemForgesBox_2, ItemQuantBox_2, ItemHexBox_2);
            _inventory[2] = new ItemPanel(null,
                ItemPic_3, ItemNameBox_3, ItemIsEquipped_3,
                ItemForgesBox_3, ItemQuantBox_3, ItemHexBox_3);
            _inventory[3] = new ItemPanel(null,
                ItemPic_4, ItemNameBox_4, ItemIsEquipped_4,
                ItemForgesBox_4, ItemQuantBox_4, ItemHexBox_4);
            _inventory[4] = new ItemPanel(null,
                ItemPic_5, ItemNameBox_5, ItemIsEquipped_5,
                ItemForgesBox_5, ItemQuantBox_5, ItemHexBox_5);
        }

        private void PopulateControls()
        {
            _inventory[0].LoadItem(_unit.Item_1);
            _inventory[1].LoadItem(_unit.Item_2);
            _inventory[2].LoadItem(_unit.Item_3);
            _inventory[3].LoadItem(_unit.Item_4);
            _inventory[4].LoadItem(_unit.Item_5);
        }

        private void btnMaxForges_Click(object sender, EventArgs e)
        {
            foreach (ItemPanel item in _inventory)
            {
                item.SetForges(7);
            }
        }

        private void btnMaxCharges_Click(object sender, EventArgs e)
        {
            foreach (ItemPanel item in _inventory)
            {
                item.SetCharges(35);
            }
        }
    }

    class ItemPanel
    {
        private PictureBox Pic;
        private ComboBox Name;
        private CheckBox Equipped;
        private NumericUpDown Forges;
        private NumericUpDown Charges;
        private MaskedTextBox Raw;

        private InventoryItem item;

        private Data.ItemDatabase ItemDb;

        public ItemPanel(InventoryItem Item,
            PictureBox Pic, ComboBox Name, CheckBox Equipped, NumericUpDown Forges, NumericUpDown Charges, MaskedTextBox Raw)
        {
            this.Pic = Pic;
            this.Name = Name;
            this.Equipped = Equipped;
            this.Forges = Forges;
            this.Charges = Charges;
            this.Raw = Raw;
            this.ItemDb = Data.Database.Items;

            this.item = Item;

            Name.ValueMember = "ItemID";
            Name.DisplayMember = "DisplayName";
            Name.DataSource = ItemDb.GetAll()
                .Where((x) => (x.Type != ItemType.Unknown) || x.ItemID == Enums.Item.None)
                .OrderBy((x) => x.DisplayName)
                .ToList();
        }

        public void LoadItem(InventoryItem Item)
        {
            item = Item;
            UpdatePanel();
        }

        public void UpdatePanel()
        {
            EventsOff();
            var data = ItemDb.GetByID(item.ItemID);

            try
            {
                Name.SelectedValue = item.ItemID;
                Equipped.Checked = item.IsEquipped;
                Pic.Image = data.GetIcon();

                if (data.Type.HasCharges())
                {
                    Charges.Enabled = true;
                    Charges.Value = item.Uses.Clamp((byte)Charges.Minimum, (byte)Charges.Maximum);
                }
                else
                {
                    Charges.Enabled = false;
                    Charges.Value = 1;
                }

                if (data.Type.HasForges())
                {
                    Forges.Enabled = true;
                    Forges.Value = item.Uses.Clamp((byte)Forges.Minimum, (byte)Forges.Maximum);
                    Equipped.Enabled = true;
                    Equipped.Checked = item.IsEquipped;
                }
                else
                {
                    Forges.Enabled = false;
                    Forges.Value = 0;
                    Equipped.Enabled = false;
                    Equipped.Checked = false;
                    item.IsEquipped = false;
                }

                Raw.Text = item.Hex();
            }
            catch (ArgumentOutOfRangeException e)
            { }

            EventsOn();
        }

        private void LoadRaw(String hex_string)
        {
            byte[] Hex = new byte[4];
            if (Hex.TryParseHex(hex_string))
            {
                item.Reparse(Hex);
            }
        }

        private void NameChanged(object sender, EventArgs e)
        {
            item.ItemID = (Enums.Item)Name.SelectedValue;
            UpdatePanel();
        }

        private void EquippedChanged(object sender, EventArgs e)
        {
            // TODO: Enforce one item equipped at a time by unchecking the other boxes?
            item.IsEquipped = Equipped.Checked;
            UpdatePanel();
        }

        private void ForgesChanged(object sender, EventArgs e)
        {
            item.Uses = (byte)Forges.Value;
            UpdatePanel();
        }

        private void ChargesChanged(object sender, EventArgs e)
        {
            item.Uses = (byte)Charges.Value;
            UpdatePanel();
        }

        private void RawChanged(object sender, EventArgs e)
        {
            LoadRaw(Raw.Text);
            UpdatePanel();
        }

        private void EventsOn()
        {
            Name.SelectedValueChanged += NameChanged;
            Equipped.CheckedChanged += EquippedChanged;
            Forges.ValueChanged += ForgesChanged;
            Charges.ValueChanged += ChargesChanged;
            Raw.TextChanged += RawChanged;
        }

        private void EventsOff()
        {
            Name.SelectedValueChanged -= NameChanged;
            Equipped.CheckedChanged -= EquippedChanged;
            Forges.ValueChanged -= ForgesChanged;
            Charges.ValueChanged -= ChargesChanged;
            Raw.TextChanged -= RawChanged;
        }

        public void SetForges(int val)
        {
            var data = ItemDb.GetByID(item.ItemID);
            if (data.Type.HasForges())
            {
                item.Uses = (byte)Math.Max(7, Math.Min(0, val));
                UpdatePanel();
            }
        }

        public void SetCharges(int val)
        {
            var data = ItemDb.GetByID(item.ItemID);
            if (data.Type.HasCharges())
            {
                item.Uses = (byte)Math.Max(35, Math.Min(0, val));
                UpdatePanel();
            }
        }
    }
}
