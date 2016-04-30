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
        private Model.ChapterSave _chapterSave;

        public ConvoyMain()
        {
            InitializeComponent();
            InitializeControls();
        }

        public void LoadChapterSave(Model.ChapterSave chapterSave)
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
            cmbItem.DataSource = Data.Database.Items.GetAll().OrderBy((x) => x.DisplayName).ToList();

            cmbItem.SelectedValueChanged += ChangeItem;
            btnAdd.Click += AddItem;

            btnAdd.Enabled = false; // Because it starts on (None)
        }

        private void PopulateControls()
        {
            FillPage(Enums.ItemType.Sword);
            FillPage(Enums.ItemType.Lance);
            FillPage(Enums.ItemType.Axe);
            FillPage(Enums.ItemType.Shuriken);
            FillPage(Enums.ItemType.Bow);
            FillPage(Enums.ItemType.Tome);
            FillPage(Enums.ItemType.Staff);
            FillPage(Enums.ItemType.Stone);
            FillPage(Enums.ItemType.Consumable);

            UpdateConvoyCount();
        }

        private void FillPage(Enums.ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.Sword: FillPage(flwSword, itemType); break;
                case ItemType.Lance: FillPage(flwLance, itemType); break;
                case ItemType.Axe: FillPage(flwAxe, itemType); break;
                case ItemType.Shuriken: FillPage(flwShuriken, itemType); break;
                case ItemType.Bow: FillPage(flwBow, itemType); break;
                case ItemType.Tome: FillPage(flwTome, itemType); break;
                case ItemType.Staff: FillPage(flwStaff, itemType); break;
                case ItemType.Stone: FillPage(flwStone, itemType); break;
                case ItemType.Consumable: FillPage(flwConsumable, itemType); break;
                default: break;
            }
        }

        private void FillPage(FlowLayoutPanel flowPanel, Enums.ItemType itemType)
        {
            flowPanel.Invalidate();

            var items = _chapterSave.ConvoyRegion.Convoy
                .Where((x) => Data.Database.Items.GetByID(x.ItemID).Type == itemType)
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

        private void UpdateConvoyCount()
        {
            lblConvoySize.Text = string.Format("{0}/{1}", 
                _chapterSave.ConvoyRegion.Convoy.Count, 
                Model.ChapterSaveRegions.ConvoyRegion.MaxConvoyCount);
        }

        // A hack to make mouse wheel scrolling work in the convoy tabs
        private void FocusOnThis(object sender, EventArgs e)
        {
            ((Control)sender).Focus();
        }
    }
}
