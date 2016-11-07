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
            // Otherwise problems happen
            if (this.DesignMode) return;

            DrawMapBackground(e.Graphics);
            DrawBuildings(e.Graphics);

            //picCastle.BackColor = Color.FromArgb(255, 198, 154, 90);

            //DrawGrid(e.Graphics);
        }

        private void DrawMapBackground(Graphics g)
        {
            Bitmap mapBg;

            switch (_castleRegion.CastleMap)
            {
                case Enums.CastleMap.Hoshidan: mapBg = Properties.Resources.Map_HoshidanStyle; break;
                case Enums.CastleMap.WindTribe: mapBg = Properties.Resources.Map_WindTribeStyle; break;
                case Enums.CastleMap.Izumite: mapBg = Properties.Resources.Map_IzumiteStyle; break;
                case Enums.CastleMap.Nohrian: mapBg = Properties.Resources.Map_NohrianStyle; break;
                case Enums.CastleMap.Chevois: mapBg = Properties.Resources.Map_ChevoisStyle; break;
                case Enums.CastleMap.Nestrian: mapBg = Properties.Resources.Map_NestrianStyle; break;
                default: mapBg = Properties.Resources.Map_HoshidanStyle; break; // Shouldn't happen
            }

            // Dimensions have to be specified. Otherwise, it draws it too big for some reason
            g.DrawImage(mapBg, 0, 0, picCastle.Width, picCastle.Height);
        }

        private void DrawBuildings(Graphics g)
        {
            int scale = 3;
            int cellWidth = 6;
            int cellHeight = 6;

            Pen p = new Pen(Color.Black);
            Brush buildingBrush = new SolidBrush(Color.FromArgb(64, 0, 0, 0));
            Brush triangleBrush = new SolidBrush(Color.FromArgb(128, 0, 0, 0));

            foreach (var building in _castleRegion.Buildings)
            {
                var data = Data.Database.Buildings.GetByID(building.BuildingID);

                // Building outline
                g.FillRectangle(buildingBrush,
                    scale * (building.LeftPosition - 1) * cellWidth,
                    scale * (building.TopPosition - 1) * cellHeight,
                    scale * data.Size * cellWidth,
                    scale * data.Size * cellHeight);
                g.DrawRectangle(p,
                    scale * (building.LeftPosition - 1) * cellWidth,
                    scale * (building.TopPosition - 1) * cellHeight,
                    scale * data.Size * cellWidth,
                    scale * data.Size * cellHeight);

                // Little arrow
                switch (building.DirectionFacing)
                {
                    case 0: // Down
                        g.FillPolygon(triangleBrush, new PointF[]
                        {
                            new PointF(
                                scale * ((building.LeftPosition - 1 + (data.Size * 0.5f)) * cellWidth + (cellWidth * 0.0f)),
                                scale * ((building.TopPosition + data.Size - 1) * cellHeight)),
                            new PointF(
                                scale * ((building.LeftPosition - 1 + (data.Size * 0.5f)) * cellWidth + (cellWidth * -0.25f)),
                                scale * ((building.TopPosition + data.Size - 1) * cellHeight - (cellHeight * 0.5f))),
                            new PointF(
                                scale * ((building.LeftPosition - 1 + (data.Size * 0.5f)) * cellWidth + (cellWidth * 0.25f)),
                                scale * ((building.TopPosition + data.Size - 1) * cellHeight - (cellHeight * 0.5f))),
                        });
                        break;
                    case 1: // Left
                        g.FillPolygon(triangleBrush, new PointF[]
                        {
                            new PointF(
                                scale * ((building.LeftPosition - 1) * cellWidth),
                                scale * ((building.TopPosition - 1 + (data.Size * 0.5f)) * cellHeight + (cellHeight * 0.0f))),
                            new PointF(
                                scale * ((building.LeftPosition - 1) * cellWidth + (cellWidth * 0.5f)),
                                scale * ((building.TopPosition - 1 + (data.Size * 0.5f)) * cellHeight + (cellHeight * -0.25f))),
                            new PointF(
                                scale * ((building.LeftPosition - 1) * cellWidth + (cellWidth * 0.5f)),
                                scale * ((building.TopPosition - 1 + (data.Size * 0.5f)) * cellHeight + (cellHeight * 0.25f))),
                        });
                        break;
                    case 2: // Up
                        g.FillPolygon(triangleBrush, new PointF[]
                        {
                            new PointF(
                                scale * ((building.LeftPosition - 1 + (data.Size * 0.5f)) * cellWidth + (cellWidth * 0.0f)),
                                scale * ((building.TopPosition - 1) * cellHeight)),
                            new PointF(
                                scale * ((building.LeftPosition - 1 + (data.Size * 0.5f)) * cellWidth + (cellWidth * -0.25f)),
                                scale * ((building.TopPosition - 1) * cellHeight + (cellHeight * 0.5f))),
                            new PointF(
                                scale * ((building.LeftPosition - 1 + (data.Size * 0.5f)) * cellWidth + (cellWidth * 0.25f)),
                                scale * ((building.TopPosition - 1) * cellHeight + (cellHeight * 0.5f))),
                        });
                        break;
                    case 3: // Right
                        g.FillPolygon(triangleBrush, new PointF[]
                        {
                            new PointF(
                                scale * ((building.LeftPosition + data.Size - 1) * cellWidth),
                                scale * ((building.TopPosition - 1 + (data.Size * 0.5f)) * cellHeight + (cellHeight * 0.0f))),
                            new PointF(
                                scale * ((building.LeftPosition + data.Size - 1) * cellWidth - (cellWidth * 0.5f)),
                                scale * ((building.TopPosition - 1 + (data.Size * 0.5f)) * cellHeight + (cellHeight * -0.25f))),
                            new PointF(
                                scale * ((building.LeftPosition + data.Size - 1) * cellWidth - (cellWidth * 0.5f)),
                                scale * ((building.TopPosition - 1 + (data.Size * 0.5f)) * cellHeight + (cellHeight * 0.25f))),
                        });
                        break;
                }
            }
        }

        /// <summary>
        /// Deprecated, but here as a code reference for now
        /// </summary>
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
