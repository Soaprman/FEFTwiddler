using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FEFTwiddler.Extensions;

namespace FEFTwiddler.GUI.MyCastle
{
    public partial class CastleMap : Form
    {
        private Model.ChapterSaveRegions.MyCastleRegion _castleRegion;

        public CastleMap(Model.ChapterSaveRegions.MyCastleRegion castleRegion)
        {
            _castleRegion = castleRegion;
            InitializeComponent();
        }

        private void CastleMap_Load(object sender, EventArgs e)
        {
            txtBuildingList.Text = "";

            foreach (var building in _castleRegion.Buildings)
            {
                txtBuildingList.Text += building.GetCondensedInformation() + Environment.NewLine;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
