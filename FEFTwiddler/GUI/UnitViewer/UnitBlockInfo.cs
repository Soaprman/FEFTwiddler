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

namespace FEFTwiddler.GUI.UnitViewer
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class UnitBlockInfo : UserControl
    {
        private ToolTip _tooltip = new ToolTip();
        private Model.IChapterSave _chapterSave;
        private Model.Unit _unit;

        public UnitBlockInfo()
        {
            InitializeComponent();
            InitializeControls();
        }

        public void LoadUnit(Model.IChapterSave chapterSave, Model.Unit unit)
        {
            _chapterSave = chapterSave;
            _unit = unit;
            PopulateControls();
        }

        private void InitializeControls()
        {
            _tooltip.SetToolTip(rdoLiving, "This unit is alive and well. If saved on the battle prep screen, this unit is not deployed.");
            _tooltip.SetToolTip(rdoDeployed, "This unit is deployed to the map. Only applicable in saves made on the battle prep screen.");
            _tooltip.SetToolTip(rdoAbsent, "This unit was in the party at some point, but left the party. They may return later. Example: Characters who come and go before chapter 6.");
            _tooltip.SetToolTip(rdoDeadByGameplay, "This unit was killed by an enemy unit.");
            _tooltip.SetToolTip(rdoDeadByPlot, "This unit was killed by the game's writers.");

            _tooltip.SetToolTip(cmbDeathChapter, "The unit died on the map where this chapter takes place.");
            _tooltip.SetToolTip(chkChallenge, "The death occurred during a 'challenge' battle, as opposed to a story battle. Whoops!");

            cmbDeathChapter.DataSource = Enum.GetValues(typeof(Enums.Chapter));
        }

        private void PopulateControls()
        {
            UnbindEvents();

            switch (_unit.UnitBlock)
            {
                case Enums.UnitBlock.Living:
                    rdoLiving.Checked = true; break;
                case Enums.UnitBlock.Deployed:
                    rdoDeployed.Checked = true; break;
                case Enums.UnitBlock.Absent:
                    rdoAbsent.Checked = true; break;
                case Enums.UnitBlock.DeadByGameplay:
                    rdoDeadByGameplay.Checked = true; break;
                case Enums.UnitBlock.DeadByPlot:
                    rdoDeadByPlot.Checked = true; break;
            }

            chkChallenge.Checked = _unit.DiedOnChallengeMission;

            cmbDeathChapter.Text = _unit.DeathChapter.ToString();

            // Disallow killing Corrin
            if (Enum.IsDefined(typeof(Enums.Character), _unit.CharacterID) && Data.Database.Characters.GetByID(_unit.CharacterID).IsCorrin && !_unit.IsEinherjar)
            {
                DisableAll();
            }
            // Don't mess with this stuff while a battle is in progress
            else if (_chapterSave.GetSaveFileType() == Enums.SaveFileType.Map)
            {
                DisableAll();
            }
            else
            {
                EnableAll();
                if (!IsDead()) DisableDeathChapter();
            }

            BindEvents();
        }

        private void BindEvents()
        {
            rdoLiving.CheckedChanged += HandleSelectLiving;
            rdoDeployed.CheckedChanged += HandleSelectDeployed;
            rdoAbsent.CheckedChanged += HandleSelectAbsent;
            rdoDeadByGameplay.CheckedChanged += HandleSelectDeadByGameplay;
            rdoDeadByPlot.CheckedChanged += HandleSelectDeadByPlot;
            cmbDeathChapter.SelectedValueChanged += HandleChangeDeathChapter;
            chkChallenge.CheckedChanged += HandleCheckChallenge;
        }

        private void UnbindEvents()
        {
            rdoLiving.CheckedChanged -= HandleSelectLiving;
            rdoDeployed.CheckedChanged -= HandleSelectDeployed;
            rdoAbsent.CheckedChanged -= HandleSelectAbsent;
            rdoDeadByGameplay.CheckedChanged -= HandleSelectDeadByGameplay;
            rdoDeadByPlot.CheckedChanged -= HandleSelectDeadByPlot;
            cmbDeathChapter.SelectedValueChanged -= HandleChangeDeathChapter;
            chkChallenge.CheckedChanged -= HandleCheckChallenge;
        }

        private void DisableAll()
        {
            rdoLiving.Enabled = false;
            rdoDeployed.Enabled = false;
            rdoAbsent.Enabled = false;
            rdoDeadByGameplay.Enabled = false;
            rdoDeadByPlot.Enabled = false;
            chkChallenge.Enabled = false;
            cmbDeathChapter.Enabled = false;
        }

        private void EnableAll()
        {
            rdoLiving.Enabled = true;

            if (_chapterSave.Header.IsBattlePrepSave) rdoDeployed.Enabled = true;
            else rdoDeployed.Enabled = false;

            rdoAbsent.Enabled = true;
            rdoDeadByGameplay.Enabled = true;
            rdoDeadByPlot.Enabled = true;
            chkChallenge.Enabled = true;
            cmbDeathChapter.Enabled = true;
        }

        private bool IsDead()
        {
            return _unit.UnitBlock == Enums.UnitBlock.DeadByGameplay || _unit.UnitBlock == Enums.UnitBlock.DeadByPlot;
        }

        private void DisableDeathChapter()
        {
            cmbDeathChapter.Enabled = false;
            chkChallenge.Enabled = false;
        }

        private void EnableDeathChapter()
        {
            cmbDeathChapter.Enabled = true;
            chkChallenge.Enabled = true;
        }

        private void HandleSelectLiving(object sender, EventArgs e)
        {
            if (_unit.UnitBlock == Enums.UnitBlock.Living) return;

            _unit.UnitBlock = Enums.UnitBlock.Living;
            _unit.RawDeployedUnitInfo = Model.Unit.GetEmptyDeployedInfoBlock();
            _unit.IsDead = false;
            _unit.WasKilledByPlot = false;
            _unit.DeathChapter = Enums.Chapter.None;
            _unit.DiedOnChallengeMission = false;

            DisableDeathChapter();

            MainForm.GetFromHere(this).LoadUnitViewer(_unit);
        }

        private void HandleSelectDeployed(object sender, EventArgs e)
        {
            if (_unit.UnitBlock == Enums.UnitBlock.Deployed) return;

            _unit.UnitBlock = Enums.UnitBlock.Deployed;
            _unit.RawDeployedUnitInfo = Model.Unit.GetFullDeployedInfoBlock();
            _unit.IsDead = false;
            _unit.WasKilledByPlot = false;
            _unit.DeathChapter = Enums.Chapter.None;
            _unit.DiedOnChallengeMission = false;

            DisableDeathChapter();

            MainForm.GetFromHere(this).LoadUnitViewer(_unit);
        }

        private void HandleSelectAbsent(object sender, EventArgs e)
        {
            if (_unit.UnitBlock == Enums.UnitBlock.Absent) return;

            _unit.UnitBlock = Enums.UnitBlock.Absent;
            _unit.RawDeployedUnitInfo = Model.Unit.GetEmptyDeployedInfoBlock();
            _unit.IsDead = false;
            _unit.WasKilledByPlot = false;
            _unit.DeathChapter = Enums.Chapter.None;
            _unit.DiedOnChallengeMission = false;

            DisableDeathChapter();

            MainForm.GetFromHere(this).LoadUnitViewer(_unit);
        }

        private void HandleSelectDeadByGameplay(object sender, EventArgs e)
        {
            if (_unit.UnitBlock == Enums.UnitBlock.DeadByGameplay) return;

            _unit.UnitBlock = Enums.UnitBlock.DeadByGameplay;
            _unit.RawDeployedUnitInfo = Model.Unit.GetEmptyDeployedInfoBlock();
            _unit.IsDead = true;
            _unit.WasKilledByPlot = false;

            EnableDeathChapter();

            MainForm.GetFromHere(this).LoadUnitViewer(_unit);
        }

        private void HandleSelectDeadByPlot(object sender, EventArgs e)
        {
            if (_unit.UnitBlock == Enums.UnitBlock.DeadByPlot) return;

            _unit.UnitBlock = Enums.UnitBlock.DeadByPlot;
            _unit.RawDeployedUnitInfo = Model.Unit.GetEmptyDeployedInfoBlock();
            _unit.IsDead = true;
            _unit.WasKilledByPlot = true;

            EnableDeathChapter();

            MainForm.GetFromHere(this).LoadUnitViewer(_unit);
        }

        private void HandleChangeDeathChapter(object sender, EventArgs e)
        {
            _unit.DeathChapter = (Enums.Chapter)(Enum.Parse(typeof(Enums.Chapter), cmbDeathChapter.Text));

            PopulateControls();
        }

        private void HandleCheckChallenge(object sender, EventArgs e)
        {
            _unit.DiedOnChallengeMission = chkChallenge.Checked;

            PopulateControls();
        }

        private void FixData()
        {
            // TODO?
            // If alive, remove all death info
            // If dead, make sure death info is set
            // Maybe put in save reading process?
        }
    }
}
