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
    public partial class Stats : UserControl
    {
        private Model.Character _character;

        public Stats()
        {
            InitializeComponent();
            InitializeControls();
        }

        public void LoadCharacter(Model.Character character)
        {
            _character = character;
            PopulateControls();
        }

        private void InitializeControls()
        {
        }

        private void PopulateControls()
        {
            Model.Stat stats = Utils.StatUtil.CalculateStats(_character);
            if (stats == null)
            {
                lblStats.Text = "Stats (raw):";
                txtStatBytes.Text = _character.GainedStats.ToString();
                btnStats.Enabled = false;
            }
            else
            {
                lblStats.Text = "Stats:";
                txtStatBytes.Text = stats.ToString();
                btnStats.Enabled = true;
            }
        }

        private void btnStats_Click(object sender, EventArgs e)
        {
            StatEditor statEditor = new StatEditor(_character);
            statEditor.ShowDialog();
            if (statEditor.IsStatsChanged) PopulateControls();
        }
    }
}
