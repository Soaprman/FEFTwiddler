using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FEFTwiddler.GUI
{
    public partial class SupportPanel : UserControl
    {
        private Model.Unit _unit;
        private Model.Unit _partnerUnit;
        private Model.Unit _siblingUnit;
        private Model.Unit _childUnit;
        private int _supportIndex;
        private static readonly string[] TypeA = { "-", "C (conversation)", "C",
            "B (conversation)", "B", "A (conversation)", "A" };
        private static readonly string[] TypeS = { "-", "C (conversation)", "C",
            "B (conversation)", "B", "A (conversation)", "A", "S (conversation)", "S" };
        private sbyte[] supportRange;
        private static readonly sbyte[] familySupportRange = { 0, 0, 1, 4, 5, 9, 10 };

        public SupportPanel()
        {
            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
        }

        /// <summary>
        /// Load support level from raw support bytes.
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="supportIndex">Support index.</param>
        public void LoadMainSupport(Model.Unit unit, Model.Unit partnerUnit, int supportIndex)
        {
            _unit = unit;
            _partnerUnit = partnerUnit;
            _supportIndex = supportIndex;
            BindEventsMain();
            PopulateControlsMain();
        }

        public void LoadFatherSupport(Model.Unit unit)
        {
            _unit = unit;
            BindEventsFather();
            PopulateControlsFather();
        }

        public void LoadMotherSupport(Model.Unit unit)
        {
            _unit = unit;
            BindEventsMother();
            PopulateControlsMother();
        }

        public void LoadSiblingSupport(Model.Unit unit, Model.Unit siblingUnit)
        {
            _unit = unit;
            _siblingUnit = siblingUnit;
            BindEventsSibling();
            PopulateControlsSibling();
        }

        public void LoadChildSupport(Model.Unit unit, Model.Unit childUnit)
        {
            _unit = unit;
            _childUnit = childUnit;
            BindEventsChild();
            PopulateControlsChild();
        }

        private void PopulateControlsMain()
        {
            UnbindEventsMain();

            var supportData = Data.Database.Characters.GetByID(_unit.CharacterID).SupportPool;
            if (_supportIndex < supportData.Length)
            {
                lblName.Text = Data.Database.Characters.GetByID(supportData[_supportIndex].CharacterID).DisplayName;
                if (supportData[_supportIndex].HasSSupport)
                {
                    cmbSupport.Items.AddRange(TypeA);
                    supportRange = new sbyte[] {
                        0,
                        (sbyte)(supportData[_supportIndex].C - 1),
                        supportData[_supportIndex].C,
                        (sbyte)(supportData[_supportIndex].B - 1),
                        supportData[_supportIndex].B,
                        (sbyte)(supportData[_supportIndex].A - 1),
                        supportData[_supportIndex].A
                    };
                }
                else
                {
                    cmbSupport.Items.AddRange(TypeS);
                    supportRange = new sbyte[] {
                        0,
                        (sbyte)(supportData[_supportIndex].C - 1),
                        supportData[_supportIndex].C,
                        (sbyte)(supportData[_supportIndex].B - 1),
                        supportData[_supportIndex].B,
                        (sbyte)(supportData[_supportIndex].A - 1),
                        supportData[_supportIndex].A,
                        (sbyte)(supportData[_supportIndex].S - 1),
                        supportData[_supportIndex].S
                    };
                }

                // Calculate support level
                sbyte supportPoint = (sbyte)_unit.RawSupports[_supportIndex];
                int i = 0;
                while ((i < supportRange.Length - 1) && (supportPoint >= supportRange[i + 1])) i++;
                cmbSupport.SelectedIndex = i;
            }
            else
            {
                throw new ArgumentException("Invalid support index.");
            }

            BindEventsMain();
        }

        private void PopulateControlsFather()
        {
            UnbindEventsFather();

            if (_unit.CorrinName != null)
                lblName.Text = _unit.CorrinName;
            else
                lblName.Text = Data.Database.Characters.GetByID(_unit.FatherID).DisplayName;
            cmbSupport.Items.AddRange(TypeA);
            int i = 1;
            while ((i < familySupportRange.Length - 1) && (_unit.FatherSupport >= familySupportRange[i + 1])) i++;
            cmbSupport.SelectedIndex = i;

            BindEventsFather();
        }

        private void PopulateControlsMother()
        {
            UnbindEventsMother();

            if (_unit.CorrinName != null)
                lblName.Text = _unit.CorrinName;
            else
                lblName.Text = Data.Database.Characters.GetByID(_unit.MotherID).DisplayName;
            cmbSupport.Items.AddRange(TypeA);
            int i = 1;
            while ((i < familySupportRange.Length - 1) && (_unit.MotherSupport >= familySupportRange[i + 1])) i++;
            cmbSupport.SelectedIndex = i;

            BindEventsMother();
        }

        private void PopulateControlsSibling()
        {
            UnbindEventsSibling();

            lblName.Text = Data.Database.Characters.GetByID(_siblingUnit.CharacterID).DisplayName;
            int i = 1;
            while ((i < familySupportRange.Length - 1) && (_unit.SiblingSupport >= familySupportRange[i + 1])) i++;
            cmbSupport.Items.AddRange(TypeA);
            cmbSupport.SelectedIndex = i;

            BindEventsSibling();
        }

        private void PopulateControlsChild()
        {
            UnbindEventsChild();

            lblName.Text = Data.Database.Characters.GetByID(_childUnit.CharacterID).DisplayName;

            int i = 1;
            if (_childUnit.FatherID == _unit.CharacterID)
                while ((i < familySupportRange.Length - 1) && (_childUnit.FatherSupport >= familySupportRange[i + 1])) i++;
            else
                while ((i < familySupportRange.Length - 1) && (_childUnit.MotherSupport >= familySupportRange[i + 1])) i++;
            cmbSupport.Items.AddRange(TypeA);
            cmbSupport.SelectedIndex = i;
            
            BindEventsChild();
        }

        #region Event

        private void BindEventsMain()
        {
            cmbSupport.SelectedIndexChanged += SupportChanged;
        }

        private void UnbindEventsMain()
        {
            cmbSupport.SelectedIndexChanged -= SupportChanged;
        }

        private void SupportChanged(object sender, EventArgs e)
        {
            var supportData = Data.Database.Characters.GetByID(_unit.CharacterID).SupportPool;

            // Remove all other S support if this support level was changed to S
            if (IsSSupport)
            {
                if (this.Parent == null || this.Parent.GetType() != typeof(FlowLayoutPanel)) return;
                if (this.Parent.Parent == null || this.Parent.Parent.GetType() != typeof(UnitViewer.Supports)) return;
                var parentControl = (UnitViewer.Supports)this.Parent.Parent;
                parentControl.RemoveAllSSupportExceptOne(_supportIndex);
            }

            // Change the support point of this unit
            byte[] rawSupports = _unit.RawSupports;
            rawSupports[_supportIndex] = (byte)supportRange[cmbSupport.SelectedIndex];
            _unit.RawSupports = rawSupports;

            // The support point of his / her partner is also need to be edited
            if (_partnerUnit != null)
            {
                var partnerData = Data.Database.Characters.GetByID(supportData[_supportIndex].CharacterID);
                int i = 0; // Partner's support index
                while (i < partnerData.SupportPool.Length &&
                    partnerData.SupportPool[i].CharacterID != _unit.CharacterID) i++;
                rawSupports = _partnerUnit.RawSupports; // Partner's raw support bytes.
                rawSupports[i] = (byte)supportRange[cmbSupport.SelectedIndex];
                _partnerUnit.RawSupports = rawSupports;
            }
        }

        private void BindEventsFather()
        {
            cmbSupport.SelectedIndexChanged += FatherSupportChanged;
        }

        private void UnbindEventsFather()
        {
            cmbSupport.SelectedIndexChanged -= FatherSupportChanged;
        }

        private void FatherSupportChanged(object sender, EventArgs e)
        {
            _unit.FatherSupport = familySupportRange[cmbSupport.SelectedIndex];
        }

        private void BindEventsMother()
        {
            cmbSupport.SelectedIndexChanged += MotherSupportChanged;
        }

        private void UnbindEventsMother()
        {
            cmbSupport.SelectedIndexChanged -= MotherSupportChanged;
        }

        private void MotherSupportChanged(object sender, EventArgs e)
        {
            _unit.MotherSupport = familySupportRange[cmbSupport.SelectedIndex];
        }

        private void BindEventsSibling()
        {
            cmbSupport.SelectedIndexChanged += SiblingSupportChanged;
        }

        private void UnbindEventsSibling()
        {
            cmbSupport.SelectedIndexChanged -= SiblingSupportChanged;
        }

        private void SiblingSupportChanged(object sender, EventArgs e)
        {
            _unit.SiblingSupport = familySupportRange[cmbSupport.SelectedIndex];
            _siblingUnit.SiblingSupport = familySupportRange[cmbSupport.SelectedIndex];
        }

        private void BindEventsChild()
        {
            cmbSupport.SelectedIndexChanged += ChildSupportChanged;
        }

        private void UnbindEventsChild()
        {
            cmbSupport.SelectedIndexChanged -= ChildSupportChanged;
        }

        private void ChildSupportChanged(object sender, EventArgs e)
        {
            if (_unit.CharacterID == _childUnit.FatherID)
                _childUnit.FatherSupport = familySupportRange[cmbSupport.SelectedIndex];
            else
                _childUnit.MotherSupport = familySupportRange[cmbSupport.SelectedIndex];
        }

        #endregion

        /// <summary>
        /// Increase the support point to maximum.
        /// </summary>
        public void MaxSupport()
        {
            cmbSupport.SelectedIndex = cmbSupport.Items.Count - 1;
        }

        /// <summary>
        /// Increase the support point to nearly maximum, which allows viewing
        /// support conversation in the game.
        /// </summary>
        public void MaxSupportWithConversation()
        {
            cmbSupport.SelectedIndex = cmbSupport.Items.Count - 2;
        }

        /// <summary>
        /// Check if current panel are displaying S support or not.
        /// </summary>
        public bool IsSSupport
        {
            get { return (cmbSupport.SelectedIndex == TypeS.Length - 1); } // Workaround
        }
    }
}
