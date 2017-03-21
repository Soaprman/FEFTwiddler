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

        private Model.IChapterSave _chapterSave;

        private Model.ConvoyItem _item;
        public Model.ConvoyItem Item { get { return _item; } }

        public ConvoyItemPanel()
        {
            InitializeComponent();
            InitializeControls();
        }

        public void LoadItem(Model.IChapterSave chapterSave, Model.ConvoyItem convoyItem)
        {
            _chapterSave = chapterSave;
            _item = convoyItem;
            PopulateControls();
        }

        private void InitializeControls()
        {
            _tooltip.SetToolTip(numCharges, "The number of charges (for consumables) or forges (for weapons).");
            _tooltip.SetToolTip(numQuantity, "The number of this item in the convoy. Set to 0 to remove this item from the list.");

            BindEvents();
        }

        private void PopulateControls()
        {
            UnbindEvents();

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
                if (_item.Uses < Model.Item.MinUses || _item.Uses > itemData.MaximumUses)
                {
                    numCharges.Minimum = 0;
                    numCharges.Maximum = 255;
                    MarkChargesIllegal();
                }
                else
                {
                    numCharges.Minimum = Model.Item.MinUses;
                    numCharges.Maximum = itemData.MaximumUses;
                    if (numCharges.Minimum == numCharges.Maximum) numCharges.Enabled = false;
                }
                numCharges.Value = _item.Uses;

                lblPlus.Text = "";
                lblMaxCharges.Text = "/ " + itemData.MaximumUses.ToString();
            }
            else if (itemData.Type.HasForges())
            {
                if (_item.Uses < Model.Item.MinForges || _item.Uses > Model.Item.MaxForges)
                {
                    numCharges.Minimum = 0;
                    numCharges.Maximum = 255;
                    MarkChargesIllegal();
                }
                else
                {
                    numCharges.Minimum = Model.Item.MinForges;
                    numCharges.Maximum = Model.Item.MaxForges;
                    if (numCharges.Minimum == numCharges.Maximum) numCharges.Enabled = false;
                }
                numCharges.Value = _item.Uses;

                lblPlus.Text = "+";
                lblMaxCharges.Text = "";
            }
            else
            {
                if (_item.Uses > 0)
                {
                    numCharges.Minimum = 0;
                    numCharges.Maximum = 255;
                    MarkChargesIllegal();
                }
                else
                {
                    numCharges.Minimum = 0;
                    numCharges.Maximum = 0;
                }
                numCharges.Value = _item.Uses;
                numCharges.Enabled = false;

                lblPlus.Text = "";
                lblMaxCharges.Text = "";
            }

            numQuantity.Value = _item.Quantity;

            BindEvents();
        }

        public void Repopulate()
        {
            PopulateControls();
        }

        private void BindEvents()
        {
            numCharges.ValueChanged += ChangeCharges;
            numCharges.Leave += CombineChargesAfterEdit;
            numQuantity.ValueChanged += ChangeQuantity;
        }

        private void UnbindEvents()
        {
            numCharges.ValueChanged -= ChangeCharges;
            numCharges.Leave -= CombineChargesAfterEdit;
            numQuantity.ValueChanged -= ChangeQuantity;
        }

        private void ChangeCharges(object sender, EventArgs e)
        {
            _item.Uses = (byte)numCharges.Value;
        }

        private void CombineChargesAfterEdit(object sender, EventArgs e)
        {
            var sameItem = _chapterSave.ConvoyRegion.Convoy
                .Where((x) => x != _item && x.ItemID == _item.ItemID && x.Uses == _item.Uses && x.WeaponNameID == _item.WeaponNameID && x.IsNamed == _item.IsNamed)
                .FirstOrDefault();

            if (sameItem != null)
            {
                ConvoyMain.GetFromHere(this).CombineItems(_item, sameItem);
            }
        }

        private void ChangeQuantity(object sender, EventArgs e)
        {
            _item.Quantity = (byte)numQuantity.Value;

            if (_item.Quantity == 0)
            {
                RemoveThisItem();
            }
        }

        private void RemoveThisItem()
        {
            _chapterSave.ConvoyRegion.Convoy.Remove(_item);
            Utils.WeaponNameUtil.RemoveWeaponNameIfUnused(_chapterSave, _item.WeaponNameID);
            ConvoyMain.GetFromHere(this).UpdateConvoyCount();

            this.Parent.Controls.Remove(this);
        }

        private void MarkChargesIllegal()
        {
            numCharges.BackColor = Color.LightCoral;
        }

        private void MarkChargesLegal()
        {
            numCharges.BackColor = SystemColors.Window;
        }
    }
}
