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
        // Model
        private Model.ChapterSaveRegions.MyCastleRegion _castleRegion;
        private Dictionary<Model.Building, Rectangle> _virtualMap;
        private Model.Building _selectedBuilding;
        private Data.Building _selectedBuildingData;

        // Numbers for drawing
        private float scale = 3.0f; // Scale from virtual size to physical size
        private int virtualCellWidth = 6;
        private int virtualCellHeight = 6;

        // Mouse stuff for event handling. Coordinates are relative to picCastle
        private Point physicalMousePosition = Point.Empty;

        public CastleMap()
        {
            InitializeComponent();
        }

        public void LoadCastleRegion(Model.ChapterSaveRegions.MyCastleRegion castleRegion)
        {
            _castleRegion = castleRegion;

            // Build out the virtual map on load. It's cheaper than doing lookups as we go
            _virtualMap = new Dictionary<Model.Building, Rectangle>();
            foreach (var building in _castleRegion.Buildings)
            {
                var data = Data.Database.Buildings.GetByID(building.BuildingID);

                _virtualMap.Add(building, new Rectangle(
                    virtualCellWidth * building.LeftPosition,
                    virtualCellHeight * building.TopPosition,
                    virtualCellWidth * data.Size,
                    virtualCellHeight * data.Size
                    ));
            }
        }

        private void CastleMap_Load(object sender, EventArgs e)
        {

        }

        private void picCastle_Paint(object sender, PaintEventArgs e)
        {
            // Otherwise problems happen in Visual Studio
            if (this.DesignMode) return;

            DrawMapBackground(e.Graphics);
            DrawBuildings(e.Graphics);
            DrawSelectionOutline(e.Graphics);
            DrawHoverHighlight(e.Graphics);

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
            Pen p = new Pen(Color.Black);
            Brush buildingBrush = new SolidBrush(Color.FromArgb(64, 0, 0, 0));
            Brush triangleBrush = new SolidBrush(Color.FromArgb(128, 0, 0, 0));

            foreach (var building in _castleRegion.Buildings)
            {
                var data = Data.Database.Buildings.GetByID(building.BuildingID);

                // Building outline
                g.FillRectangle(buildingBrush,
                    scale * (building.LeftPosition - 1) * virtualCellWidth,
                    scale * (building.TopPosition - 1) * virtualCellHeight,
                    scale * data.Size * virtualCellWidth,
                    scale * data.Size * virtualCellHeight);
                g.DrawRectangle(p,
                    scale * (building.LeftPosition - 1) * virtualCellWidth,
                    scale * (building.TopPosition - 1) * virtualCellHeight,
                    scale * data.Size * virtualCellWidth,
                    scale * data.Size * virtualCellHeight);

                // Little arrow
                switch (building.DirectionFacing)
                {
                    case 0: // Down
                        g.FillPolygon(triangleBrush, new PointF[]
                        {
                            new PointF(
                                scale * ((building.LeftPosition - 1 + (data.Size * 0.5f)) * virtualCellWidth + (virtualCellWidth * 0.0f)),
                                scale * ((building.TopPosition + data.Size - 1) * virtualCellHeight)),
                            new PointF(
                                scale * ((building.LeftPosition - 1 + (data.Size * 0.5f)) * virtualCellWidth + (virtualCellWidth * -0.25f)),
                                scale * ((building.TopPosition + data.Size - 1) * virtualCellHeight - (virtualCellHeight * 0.5f))),
                            new PointF(
                                scale * ((building.LeftPosition - 1 + (data.Size * 0.5f)) * virtualCellWidth + (virtualCellWidth * 0.25f)),
                                scale * ((building.TopPosition + data.Size - 1) * virtualCellHeight - (virtualCellHeight * 0.5f))),
                        });
                        break;
                    case 1: // Left
                        g.FillPolygon(triangleBrush, new PointF[]
                        {
                            new PointF(
                                scale * ((building.LeftPosition - 1) * virtualCellWidth),
                                scale * ((building.TopPosition - 1 + (data.Size * 0.5f)) * virtualCellHeight + (virtualCellHeight * 0.0f))),
                            new PointF(
                                scale * ((building.LeftPosition - 1) * virtualCellWidth + (virtualCellWidth * 0.5f)),
                                scale * ((building.TopPosition - 1 + (data.Size * 0.5f)) * virtualCellHeight + (virtualCellHeight * -0.25f))),
                            new PointF(
                                scale * ((building.LeftPosition - 1) * virtualCellWidth + (virtualCellWidth * 0.5f)),
                                scale * ((building.TopPosition - 1 + (data.Size * 0.5f)) * virtualCellHeight + (virtualCellHeight * 0.25f))),
                        });
                        break;
                    case 2: // Up
                        g.FillPolygon(triangleBrush, new PointF[]
                        {
                            new PointF(
                                scale * ((building.LeftPosition - 1 + (data.Size * 0.5f)) * virtualCellWidth + (virtualCellWidth * 0.0f)),
                                scale * ((building.TopPosition - 1) * virtualCellHeight)),
                            new PointF(
                                scale * ((building.LeftPosition - 1 + (data.Size * 0.5f)) * virtualCellWidth + (virtualCellWidth * -0.25f)),
                                scale * ((building.TopPosition - 1) * virtualCellHeight + (virtualCellHeight * 0.5f))),
                            new PointF(
                                scale * ((building.LeftPosition - 1 + (data.Size * 0.5f)) * virtualCellWidth + (virtualCellWidth * 0.25f)),
                                scale * ((building.TopPosition - 1) * virtualCellHeight + (virtualCellHeight * 0.5f))),
                        });
                        break;
                    case 3: // Right
                        g.FillPolygon(triangleBrush, new PointF[]
                        {
                            new PointF(
                                scale * ((building.LeftPosition + data.Size - 1) * virtualCellWidth),
                                scale * ((building.TopPosition - 1 + (data.Size * 0.5f)) * virtualCellHeight + (virtualCellHeight * 0.0f))),
                            new PointF(
                                scale * ((building.LeftPosition + data.Size - 1) * virtualCellWidth - (virtualCellWidth * 0.5f)),
                                scale * ((building.TopPosition - 1 + (data.Size * 0.5f)) * virtualCellHeight + (virtualCellHeight * -0.25f))),
                            new PointF(
                                scale * ((building.LeftPosition + data.Size - 1) * virtualCellWidth - (virtualCellWidth * 0.5f)),
                                scale * ((building.TopPosition - 1 + (data.Size * 0.5f)) * virtualCellHeight + (virtualCellHeight * 0.25f))),
                        });
                        break;
                }
            }
        }

        private void DrawHoverHighlight(Graphics g)
        {
            Brush b = new SolidBrush(Color.FromArgb(128, 255, 255, 0));

            if (physicalMousePosition != Point.Empty)
            {
                float physW = scale * virtualCellWidth;
                float physH = scale * virtualCellHeight;
                float physX = (float)Math.Floor(physicalMousePosition.X / physW) * physW;
                float physY = (float)Math.Floor(physicalMousePosition.Y / physH) * physH;

                g.FillRectangle(b, physX, physY, physW, physH);
            }
        }

        private void DrawSelectionOutline(Graphics g)
        {
            Pen p = new Pen(Color.Red, scale);

            if (_selectedBuilding != null)
            {
                float physW = scale * virtualCellWidth * _selectedBuildingData.Size;
                float physH = scale * virtualCellHeight * _selectedBuildingData.Size;
                float physX = scale * virtualCellWidth * (_selectedBuilding.LeftPosition - 1);
                float physY = scale * virtualCellHeight * (_selectedBuilding.TopPosition - 1);

                g.DrawRectangle(p, physX, physY, physW, physH);
            }
        }

        // Select a building or rotate it clockwise
        private void picCastle_MouseClick(object sender, MouseEventArgs e)
        {
            int virtX = (int)(physicalMousePosition.X / scale);
            int virtY = (int)(physicalMousePosition.Y / scale);

            // Consult map
            Model.Building selectedBuilding;
            var virtualBuilding = _virtualMap.Where(x => x.Value.Contains(virtX, virtY)).FirstOrDefault();
            if (!virtualBuilding.Equals(default(KeyValuePair<Model.Building, Rectangle>)))
            {
                selectedBuilding = virtualBuilding.Key;
            }
            else
            {
                selectedBuilding = null;
            }

            // Act
            if (selectedBuilding == null)
            {
                // Deselect
                _selectedBuilding = null;
                _selectedBuildingData = null;
            }
            else if (selectedBuilding == _selectedBuilding)
            {
                // Rotate
                if (_selectedBuilding.DirectionFacing == 0)
                {
                    _selectedBuilding.DirectionFacing = 1;
                }
                else if (_selectedBuilding.DirectionFacing == 1)
                {
                    _selectedBuilding.DirectionFacing = 2;
                }
                else if (_selectedBuilding.DirectionFacing == 2)
                {
                    _selectedBuilding.DirectionFacing = 3;
                }
                else if (_selectedBuilding.DirectionFacing == 3)
                {
                    _selectedBuilding.DirectionFacing = 0;
                }
            }
            else
            {
                // Select
                _selectedBuilding = selectedBuilding;
                _selectedBuildingData = Data.Database.Buildings.GetByID(_selectedBuilding.BuildingID);
            }
        }

        // Start showing hover outline
        private void picCastle_MouseMove(object sender, MouseEventArgs e)
        {
            physicalMousePosition = new Point(e.X - picCastle.Left, e.Y - picCastle.Top);
            picCastle.Invalidate();
        }

        // Stop showing hover outline
        private void picCastle_MouseLeave(object sender, EventArgs e)
        {
            physicalMousePosition = Point.Empty;
            picCastle.Invalidate();
        }

        #region "Deprecated"

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

        #endregion
    }
}
