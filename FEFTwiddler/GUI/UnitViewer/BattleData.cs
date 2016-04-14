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
    public partial class BattleData : UserControl
    {
        private Model.Character _character;

        public BattleData()
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
            numBattles.Value = _character.BattleCount;
            numVictories.Value = _character.VictoryCount;
        }

        private void numBattles_ValueChanged(object sender, EventArgs e)
        {
            _character.BattleCount = (ushort)numBattles.Value;
        }

        private void numVictories_ValueChanged(object sender, EventArgs e)
        {
            _character.VictoryCount = (ushort)numVictories.Value;
        }
    }
}
