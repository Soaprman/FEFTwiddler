using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FEFTwiddler.Enums;

namespace FEFTwiddler.GUI.Convoy
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class ConvoyMain : UserControl
    {
        private Model.IChapterSave _chapterSave;

        public ConvoyMain()
        {
            InitializeComponent();
            InitializeControls();
        }

        public static ConvoyMain GetFromHere(Control ctl)
        {
            var parent = ctl.Parent;
            while (parent.GetType() != typeof(ConvoyMain))
            {
                parent = parent.Parent;
            }
            return (ConvoyMain)parent;
        }

        public void LoadChapterSave(Model.IChapterSave chapterSave)
        {
            _chapterSave = chapterSave;
            PopulateControls();
        }

        private void InitializeControls()
        {
            flwSword.MouseEnter += FocusOnThis;
            flwLance.MouseEnter += FocusOnThis;
            flwAxe.MouseEnter += FocusOnThis;
            flwShuriken.MouseEnter += FocusOnThis;
            flwBow.MouseEnter += FocusOnThis;
            flwTome.MouseEnter += FocusOnThis;
            flwStaff.MouseEnter += FocusOnThis;
            flwStone.MouseEnter += FocusOnThis;
            flwConsumable.MouseEnter += FocusOnThis;

            cmbItem.ValueMember = "ItemID";
            cmbItem.DisplayMember = "DisplayName";
            cmbItem.DataSource = Data.Database.Items.GetAll()
                .Where((x) => x.Type != ItemType.Unknown || x.ItemID == Item.None)
                .OrderBy((x) => x.DisplayName)
                .ToList();

            cmbItem.SelectedValueChanged += ChangeItem;
            btnAdd.Click += AddItem;

            btnEmptyConvoy.Click += EmptyConvoy;

            btnAdd.Enabled = false; // Because it starts on (None)
        }

        private void PopulateControls()
        {
            ClearAllPages();
            FillAllPages();

            UpdateConvoyCount();
        }

        private void FillAllPages()
        {
            foreach (var item in _chapterSave.ConvoyRegion.Convoy.OrderBy((x) => x.ItemID))
            {
                var panel = MakeItemPanel(item);
                var itemData = Data.Database.Items.GetByID(item.ItemID);
                var flow = GetFlowPanel(itemData.Type);
                flow.Controls.Add(panel);
            }
        }

        private void ClearAllPages()
        {
            flwSword.Controls.Clear();
            flwLance.Controls.Clear();
            flwAxe.Controls.Clear();
            flwShuriken.Controls.Clear();
            flwBow.Controls.Clear();
            flwTome.Controls.Clear();
            flwStaff.Controls.Clear();
            flwStone.Controls.Clear();
            flwConsumable.Controls.Clear();
        }

        public void FillPage(Enums.ItemType itemType)
        {
            FillPage(GetFlowPanel(itemType));
        }

        private void FillPage(FlowLayoutPanel flowPanel)
        {
            flowPanel.Invalidate();

            var items = _chapterSave.ConvoyRegion.Convoy
                .Where(GetItemTypes(flowPanel))
                .OrderBy((x) => x.ItemID);

            flowPanel.Controls.Clear();

            foreach (var item in items)
            {
                var panel = new ConvoyItemPanel();
                panel.LoadItem(_chapterSave, item);
                flowPanel.Controls.Add(panel);
            }

            flowPanel.Refresh();
        }

        private ConvoyItemPanel MakeItemPanel(Model.ConvoyItem item)
        {
            var panel = new ConvoyItemPanel();
            panel.LoadItem(_chapterSave, item);
            return panel;
        }

        private void ChangeItem(object sender, EventArgs e)
        {
            var itemId = (Enums.Item)((ComboBox)sender).SelectedValue;
            var itemData = Data.Database.Items.GetByID(itemId);
            picIcon.Image = itemData.GetIcon();

            if (itemId == Item.None)
            {
                btnAdd.Enabled = false;
                return;
            }
            else
            {
                EnforceConvoyLimits();
            }
            
            if (itemData.Type.HasForges())
            {
                numCharges.Enabled = true;
                numCharges.Minimum = Model.Item.MinForges;
                numCharges.Maximum = Model.Item.MaxForges;
                numCharges.Value = Model.Item.MinForges;
            }
            else if (itemData.Type.HasCharges())
            {
                numCharges.Enabled = true;
                numCharges.Minimum = Model.Item.MinUses;
                numCharges.Maximum = itemData.MaximumUses;
                numCharges.Value = itemData.MaximumUses;
            }
            else
            {
                numCharges.Enabled = false;
                numCharges.Minimum = 0;
                numCharges.Maximum = 0;
                numCharges.Value = 0;
            }
        }

        private void AddItem(object sender, EventArgs e)
        {
            var itemId = (Enums.Item)cmbItem.SelectedValue;
            var item = GetItem(itemId, (byte)numCharges.Value);

            if (item == null)
            {
                item = Model.ConvoyItem.Create();
                item.ItemID = itemId;
                _chapterSave.ConvoyRegion.Convoy.Add(item);
            }

            var itemData = Data.Database.Items.GetByID(itemId);
            if (itemData.Type.HasForges())
            {
                item.Uses = (byte)(Math.Min(numCharges.Value, Model.Item.MaxForges));
            }
            else if (itemData.Type.HasCharges())
            {
                item.Uses = (byte)(Math.Min(numCharges.Value, itemData.MaximumUses));
            }
            
            item.Quantity = (byte)(Math.Min(item.Quantity + numQuantity.Value, Model.Item.MaxQuantity));

            FillPage(itemData.Type);

            UpdateConvoyCount();
        }

        private Model.ConvoyItem GetItem(Enums.Item itemId, byte charges)
        {
            var item = _chapterSave.ConvoyRegion.Convoy
                .Where((x) => x.ItemID == itemId && x.Uses == charges && !x.IsNamed)
                .FirstOrDefault();

            return item;
        }

        private void EnforceConvoyLimits()
        {
            if (_chapterSave.ConvoyRegion.Convoy.Count >= Model.ChapterSaveRegions.ConvoyRegion.MaxConvoyCount)
            {
                btnAdd.Enabled = false;
            }
            else
            {
                btnAdd.Enabled = true;
            }
        }

        public void UpdateConvoyCount()
        {
            lblConvoySize.Text = string.Format("{0}/{1}", 
                _chapterSave.ConvoyRegion.Convoy.Count, 
                Model.ChapterSaveRegions.ConvoyRegion.MaxConvoyCount);
        }

        public void CombineItems(Model.ConvoyItem srcItem, Model.ConvoyItem destItem)
        {
            destItem.Quantity = (byte)Math.Min(destItem.Quantity + srcItem.Quantity, Model.Item.MaxQuantity);
            _chapterSave.ConvoyRegion.Convoy.Remove(srcItem);
            Utils.WeaponNameUtil.RemoveWeaponNameIfUnused(_chapterSave, srcItem.WeaponNameID);

            GetItemPanel(destItem).Repopulate();

            var srcPanel = GetItemPanel(srcItem);
            srcPanel.Parent.Controls.Remove(srcPanel);

            UpdateConvoyCount();
        }

        private ConvoyItemPanel GetItemPanel(Model.ConvoyItem item)
        {
            var itemType = Data.Database.Items.GetByID(item.ItemID).Type;
            var flowPanel = GetFlowPanel(itemType);
            foreach (ConvoyItemPanel itemPanel in flowPanel.Controls)
            {
                if (itemPanel.Item == item) return itemPanel;
            }
            return null;
        }

        private FlowLayoutPanel GetFlowPanel(Enums.ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.Sword:
                    return flwSword;
                case ItemType.Lance:
                    return flwLance;
                case ItemType.Axe:
                    return flwAxe;
                case ItemType.Shuriken:
                    return flwShuriken;
                case ItemType.Bow:
                    return flwBow;
                case ItemType.Tome:
                    return flwTome;
                case ItemType.Staff:
                    return flwStaff;
                case ItemType.Stone:
                case ItemType.NPC:
                    return flwStone;
                case ItemType.Consumable:
                case ItemType.Held:
                    return flwConsumable;
                default: // Should never be the case
                    return flwConsumable;
            }
        }

        private Func<Model.ConvoyItem, bool> GetItemTypes(FlowLayoutPanel flowPanel)
        {
            if (flowPanel == flwSword)
            {
                return (x) => Data.Database.Items.GetByID(x.ItemID).Type == ItemType.Sword;
            }
            else if (flowPanel == flwLance)
            {
                return (x) => Data.Database.Items.GetByID(x.ItemID).Type == ItemType.Lance;
            }
            else if (flowPanel == flwAxe)
            {
                return (x) => Data.Database.Items.GetByID(x.ItemID).Type == ItemType.Axe;
            }
            else if (flowPanel == flwShuriken)
            {
                return (x) => Data.Database.Items.GetByID(x.ItemID).Type == ItemType.Shuriken;
            }
            else if (flowPanel == flwBow)
            {
                return (x) => Data.Database.Items.GetByID(x.ItemID).Type == ItemType.Bow;
            }
            else if (flowPanel == flwTome)
            {
                return (x) => Data.Database.Items.GetByID(x.ItemID).Type == ItemType.Tome;
            }
            else if (flowPanel == flwStaff)
            {
                return (x) => Data.Database.Items.GetByID(x.ItemID).Type == ItemType.Staff;
            }
            else if (flowPanel == flwStone)
            {
                return (x) => Data.Database.Items.GetByID(x.ItemID).Type == ItemType.Stone ||
                                Data.Database.Items.GetByID(x.ItemID).Type == ItemType.NPC;
            }
            else if (flowPanel == flwConsumable)
            {
                return (x) => Data.Database.Items.GetByID(x.ItemID).Type == ItemType.Consumable ||
                                Data.Database.Items.GetByID(x.ItemID).Type == ItemType.Held;
            }
            else // Should never be the case
            {
                return (x) => true;
            }
        }

        // A hack to make mouse wheel scrolling work in the convoy tabs
        private void FocusOnThis(object sender, EventArgs e)
        {
            ((Control)sender).Focus();
        }

        private void EmptyConvoy(object sender, EventArgs e)
        {
            var result = MessageBox.Show("This will remove ALL items from your convoy, regardless of their importance to the plot or sentimental value. Proceed?",
                "Empty convoy?",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
            if (result != DialogResult.Yes) return;

            _chapterSave.ConvoyRegion.Convoy.Clear();
            Utils.WeaponNameUtil.RemoveAllUnusedWeaponNames(_chapterSave);
            PopulateControls();
        }
    }
}
