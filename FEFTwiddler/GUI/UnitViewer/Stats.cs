﻿using System;
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
        private Model.Unit _unit;

        public Stats()
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
        }

        private void PopulateControls()
        {
            Model.Stat stats = Utils.StatUtil.CalculateStats(_unit);
            if (stats == null)
            {
                lblStats.Text = "Stats (raw):";
                txtStatBytes.Text = _unit.GainedStats.ToString();
                btnStats.Enabled = false;
            }
            else
            {
                lblStats.Text = "Stats:";
                txtStatBytes.Text = stats.ToString();
                btnStats.Enabled = true;
            }
            if (Data.Database.Characters.GetByID(_unit.CharacterID).IsCustomDLC)
            {
                btnStats.Enabled = false;
            }
        }

        private void btnStats_Click(object sender, EventArgs e)
        {
            StatEditor statEditor = new StatEditor(_unit);
            statEditor.ShowDialog();
            if (statEditor.IsStatsChanged) PopulateControls();
        }
    }
}