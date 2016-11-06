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

namespace FEFTwiddler.GUI.ChapterData
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class CastleMap : UserControl
    {
        private Model.ChapterSaveRegions.MyCastleRegion _castleRegion;

        public CastleMap()
        {
            InitializeComponent();
        }

        public void LoadCastleRegion(Model.ChapterSaveRegions.MyCastleRegion castleRegion)
        {
            _castleRegion = castleRegion;
        }

        private void CastleMap_Load(object sender, EventArgs e)
        {

        }

        private void picCastle_Paint(object sender, PaintEventArgs e)
        {
            picCastle.BackColor = Color.FromArgb(255, 198, 154, 90);

            DrawGrid(e.Graphics);
        }

        private void DrawGrid(Graphics g)
        {
            // Grid size
            int horizontalCells = 29;
            int cellWidth = 6;

            int verticalCells = 30;
            int cellHeight = 6;

            int zoom = 3;

            // Draw lightened areas
            Brush darkBrush = new SolidBrush(Color.FromArgb(255, 222, 182, 123));
            g.FillRectangle(darkBrush,
                zoom * cellWidth * 1,
                zoom * cellHeight * 1,
                zoom * cellWidth * 6,
                zoom * cellHeight * 28);
            g.FillRectangle(darkBrush,
                zoom * cellWidth * 7,
                zoom * cellHeight * 4,
                zoom * cellWidth * 3,
                zoom * cellHeight * 25);
            g.FillRectangle(darkBrush,
                zoom * cellWidth * 10,
                zoom * cellHeight * 5,
                zoom * cellWidth * 2,
                zoom * cellHeight * 18);
            g.FillRectangle(darkBrush,
                zoom * cellWidth * 12,
                zoom * cellHeight * 7,
                zoom * cellWidth * 5,
                zoom * cellHeight * 16);
            g.FillRectangle(darkBrush,
                zoom * cellWidth * 17,
                zoom * cellHeight * 5,
                zoom * cellWidth * 2,
                zoom * cellHeight * 18);
            g.FillRectangle(darkBrush,
                zoom * cellWidth * 19,
                zoom * cellHeight * 4,
                zoom * cellWidth * 3,
                zoom * cellHeight * 25);
            g.FillRectangle(darkBrush,
                zoom * cellWidth * 22,
                zoom * cellHeight * 1,
                zoom * cellWidth * 6,
                zoom * cellHeight * 28);

            // Draw grid lines
            //Pen gridPen = new Pen(Color.FromArgb(255, 198, 158, 107));
            Pen gridPen = new Pen(Color.FromArgb(255, 173, 130, 74));
            gridPen.Width = zoom;

            for (int x = 0; x < horizontalCells; x++)
            {
                // Vertical line
                g.DrawLine(gridPen,
                    zoom * x * cellWidth,
                    zoom * 0,
                    zoom * x * cellWidth,
                    zoom * verticalCells * cellHeight);
            }

            for (int y = 0; y < verticalCells; y++)
            {
                // Horizontal line
                g.DrawLine(gridPen,
                    zoom * 0,
                    zoom * y * cellHeight,
                    zoom * horizontalCells * cellWidth,
                    zoom * y * cellHeight);
            }

            // Draw border
            Pen borderPen = new Pen(Color.FromArgb(255, 99, 77, 45));
            borderPen.Width = zoom;

            g.DrawRectangle(borderPen,
                zoom * 0,
                zoom * 0,
                zoom * horizontalCells * cellWidth - 1,
                zoom * verticalCells * cellHeight - 1);
        }
    }
}
