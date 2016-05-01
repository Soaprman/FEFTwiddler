using System;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using FEFTwiddler.Enums;

namespace FEFTwiddler.GUI.Convoy
{
    public partial class ConvoyItemPanel : UserControl
    {
        private ToolTip _tooltip = new ToolTip();

        private Model.ChapterSave _chapterSave;
        private Model.ConvoyItem _item;

        public ConvoyItemPanel()
        {
            InitializeComponent();
            InitializeControls();
        }

        public void LoadItem(Model.ChapterSave chapterSave, Model.ConvoyItem convoyItem)
        {
            _chapterSave = chapterSave;
            _item = convoyItem;
            PopulateControls();
        }

        private void InitializeControls()
        {
            _tooltip.SetToolTip(numCharges, "The number of charges (for consumables) or forges (for weapons).");
            _tooltip.SetToolTip(numQuantity, "The number of this item in the convoy. Set to 0 to remove this item from the list.");

            numCharges.ValueChanged += ChangeCharges;
            numQuantity.ValueChanged += ChangeQuantity;
        }

        private void PopulateControls()
        {
            var itemData = Data.Database.Items.GetByID(_item.ItemID);

            if (_item.IsNamed)
            {
                lblName.ForeColor = Color.Blue;
                lblName.Text = _chapterSave.WeaponNameRegion.WeaponNames.Where((x) => x.ID == _item.WeaponNameID).First().Name;
            }
            else
            {
                lblName.Text = itemData.DisplayName;
            }

            picIcon.Image = itemData.GetIcon();

            if (itemData.Type.HasCharges())
            {
                numCharges.Maximum = itemData.MaximumUses;
                numCharges.Value = _item.Uses;
                lblPlus.Text = "";
                lblMaxCharges.Text = "/ " + itemData.MaximumUses.ToString();
            }
            else if (itemData.Type.HasForges())
            {
                numCharges.Maximum = 7;
                numCharges.Value = _item.Uses;
                lblPlus.Text = "+";
                lblMaxCharges.Text = "";
            }
            else
            {
                numCharges.Value = 0;
                numCharges.Enabled = false;
                lblPlus.Text = "";
                lblMaxCharges.Text = "";
            }

            numQuantity.Value = _item.Quantity;
        }

        private void ChangeCharges(object sender, EventArgs e)
        {
            _item.Uses = (byte)numCharges.Value;
        }

        private void ChangeQuantity(object sender, EventArgs e)
        {
            _item.Quantity = (byte)numQuantity.Value;

            if (_item.Quantity == 0)
            {
                _chapterSave.ConvoyRegion.Convoy.Remove(_item);
                ConvoyMain.GetFromHere(this).UpdateConvoyCount();
                this.Parent.Controls.Remove(this);
            }
        }
    }
}
