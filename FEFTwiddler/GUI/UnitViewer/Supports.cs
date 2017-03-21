using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FEFTwiddler.GUI.UnitViewer
{
    public partial class Supports : Form
    {
        private Model.Unit _unit;
        private Model.IChapterSave _chapterSave;

        public Supports(Model.IChapterSave chapterSave, Model.Unit unit)
        {
            _chapterSave = chapterSave;
            _unit = unit;
            InitializeComponent();
            SetTitle();
            BindEvents();
        }

        private void SetTitle()
        {
            this.Text = _unit.GetDisplayName() + "'s supports";
        }

        private void Supports_Load(object sender, EventArgs e)
        {
            UnbindEvents();

            byte supportCount = _unit.RawNumberOfSupports;
            var supportData = Data.Database.Characters.GetByID(_unit.CharacterID).SupportPool;
            for (int i = 0; i < supportCount; i++)
            {
                // Search for partner unit in unit list.
                Model.Unit partnerUnit = null;
                foreach (var unit in _chapterSave.UnitRegion.Units)
                {
                    if (unit.CharacterID == supportData[i].CharacterID && !unit.IsRecruited)
                    {
                        partnerUnit = unit;
                        break;
                    }
                }
                SupportPanel panel = MakeMainSupportPanel(partnerUnit, i);
                flwSupport.Controls.Add(panel);
            }
            
            // Family support (for children)
            if (Data.Database.Characters.GetByID(_unit.CharacterID).IsChild)
            {
                flwSupport.Controls.Add(MakeFatherSupportPanel()); // Father
                flwSupport.Controls.Add(MakeMotherSupportPanel()); // Mother
                foreach (var unit in _chapterSave.UnitRegion.Units)
                {
                    if (Data.Database.Characters.GetByID(unit.CharacterID).IsChild &&
                        unit.CharacterID != _unit.CharacterID &&
                        !unit.IsRecruited &&
                        (unit.FatherID == _unit.FatherID || unit.MotherID == _unit.MotherID))
                    {
                        flwSupport.Controls.Add(MakeSiblingSupportPanel(unit)); // Sibling
                    }
                }
            }
            // Child support (for parents)
            else
            {
                foreach (var unit in _chapterSave.UnitRegion.Units)
                {
                    if (Data.Database.Characters.GetByID(unit.CharacterID).IsChild &&
                        !unit.IsRecruited &&
                        (unit.FatherID == _unit.CharacterID || unit.MotherID == _unit.CharacterID))
                    {
                        flwSupport.Controls.Add(MakeChildSupportPanel(unit));
                    }
                }
            }

            // A+ partner
            if (!Data.Database.Characters.GetByID(_unit.CharacterID).IsCorrin)
            {
                var characters = Data.Database.Characters.GetAll();
                cmbAPlus.DisplayMember = "DisplayName";
                cmbAPlus.ValueMember = "CharacterID";
                var aPlusCandidateIDs = supportData
                    .Where(support => (support.HasSSupport && !Data.Database.Characters.GetByID(support.CharacterID).IsCorrin))
                    .Select(support => support.CharacterID);
                cmbAPlus.DataSource = characters
                    .Where(character => aPlusCandidateIDs.Any(id => id == character.CharacterID) || character.CharacterID == Enums.Character.None)
                    .OrderBy(character => character.DisplayName)
                    .ToList();
                cmbAPlus.SelectedValue = _unit.APlusSupportCharacter;
                cmbAPlus.Enabled = true;
            }
            else
            {
                cmbAPlus.Enabled = false;
            }

            BindEvents();
        }

        private SupportPanel MakeMainSupportPanel(Model.Unit partnerUnit, int supportIndex)
        {
            var panel = new SupportPanel();
            panel.LoadMainSupport(_unit, partnerUnit, supportIndex);
            return panel;
        }

        private SupportPanel MakeFatherSupportPanel()
        {
            var panel = new SupportPanel();
            panel.LoadFatherSupport(_unit);
            return panel;
        }

        private SupportPanel MakeMotherSupportPanel()
        {
            var panel = new SupportPanel();
            panel.LoadMotherSupport(_unit);
            return panel;
        }

        private SupportPanel MakeSiblingSupportPanel(Model.Unit siblingUnit)
        {
            var panel = new SupportPanel();
            panel.LoadSiblingSupport(_unit, siblingUnit);
            return panel;
        }

        private SupportPanel MakeChildSupportPanel(Model.Unit childUnit)
        {
            var panel = new SupportPanel();
            panel.LoadChildSupport(_unit, childUnit);
            return panel;
        }

        private void BindEvents()
        {
            cmbAPlus.SelectedValueChanged += cmbAPlus_SelectedValueChanged;
        }

        private void UnbindEvents()
        {
            cmbAPlus.SelectedValueChanged -= cmbAPlus_SelectedValueChanged;
        }

        /// <summary>
        /// Remove all S support, except one.
        /// </summary>
        /// <param name="supportIndex">Index of the support that must be kept.</param>
        public void RemoveAllSSupportExceptOne(int supportIndex)
        {
            if (chkPolygamy.Checked) return;
            for (int i = 0; i < flwSupport.Controls.Count; i++)
            {
                SupportPanel sp = (SupportPanel)flwSupport.Controls[i];
                if (i != supportIndex && sp.IsSSupport)
                    sp.MaxSupportWithConversation();
            }
        }

        private void btnMaxSupports_Click(object sender, EventArgs e)
        {
            foreach (SupportPanel sp in flwSupport.Controls)
            {
                sp.MaxSupport();
            }
        }

        private void btnMaxSupportsConversation_Click(object sender, EventArgs e)
        {
            foreach (SupportPanel sp in flwSupport.Controls)
            {
                sp.MaxSupportWithConversation();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbAPlus_SelectedValueChanged(object sender, EventArgs e)
        {
            _unit.APlusSupportCharacter = (Enums.Character)cmbAPlus.SelectedValue;
        }
    }
}
