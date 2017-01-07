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
                    virtualCellWidth * Shift(building.LeftPosition),
                    virtualCellHeight * Shift(building.TopPosition),
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

            // TODO: Maybe cycle through virtual map
            foreach (var building in _castleRegion.Buildings)
            {
                // TODO: Maybe include in virtual map to improve performance
                var data = Data.Database.Buildings.GetByID(building.BuildingID);

                // Building outline
                // TODO: Maybe use rectangles from virtual map
                g.FillRectangle(buildingBrush,
                    scale * Shift(building.LeftPosition) * virtualCellWidth,
                    scale * Shift(building.TopPosition) * virtualCellHeight,
                    scale * data.Size * virtualCellWidth,
                    scale * data.Size * virtualCellHeight);
                g.DrawRectangle(p,
                    scale * Shift(building.LeftPosition) * virtualCellWidth,
                    scale * Shift(building.TopPosition) * virtualCellHeight,
                    scale * data.Size * virtualCellWidth,
                    scale * data.Size * virtualCellHeight);

                // Little arrow
                switch (building.DirectionFacing)
                {
                    case Enums.BuildingDirection.Down:
                        g.FillPolygon(triangleBrush, new PointF[]
                        {
                            new PointF(
                                scale * ((Shift(building.LeftPosition) + (data.Size * 0.5f)) * virtualCellWidth + (virtualCellWidth * 0.0f)),
                                scale * ((Shift(building.TopPosition) + data.Size) * virtualCellHeight)),
                            new PointF(
                                scale * ((Shift(building.LeftPosition) + (data.Size * 0.5f)) * virtualCellWidth + (virtualCellWidth * -0.25f)),
                                scale * ((Shift(building.TopPosition) + data.Size) * virtualCellHeight - (virtualCellHeight * 0.5f))),
                            new PointF(
                                scale * ((Shift(building.LeftPosition) + (data.Size * 0.5f)) * virtualCellWidth + (virtualCellWidth * 0.25f)),
                                scale * ((Shift(building.TopPosition) + data.Size) * virtualCellHeight - (virtualCellHeight * 0.5f))),
                        });
                        break;
                    case Enums.BuildingDirection.Left:
                        g.FillPolygon(triangleBrush, new PointF[]
                        {
                            new PointF(
                                scale * (Shift(building.LeftPosition) * virtualCellWidth),
                                scale * ((Shift(building.TopPosition) + (data.Size * 0.5f)) * virtualCellHeight + (virtualCellHeight * 0.0f))),
                            new PointF(
                                scale * (Shift(building.LeftPosition) * virtualCellWidth + (virtualCellWidth * 0.5f)),
                                scale * ((Shift(building.TopPosition) + (data.Size * 0.5f)) * virtualCellHeight + (virtualCellHeight * -0.25f))),
                            new PointF(
                                scale * (Shift(building.LeftPosition) * virtualCellWidth + (virtualCellWidth * 0.5f)),
                                scale * ((Shift(building.TopPosition) + (data.Size * 0.5f)) * virtualCellHeight + (virtualCellHeight * 0.25f))),
                        });
                        break;
                    case Enums.BuildingDirection.Up:
                        g.FillPolygon(triangleBrush, new PointF[]
                        {
                            new PointF(
                                scale * ((Shift(building.LeftPosition) + (data.Size * 0.5f)) * virtualCellWidth + (virtualCellWidth * 0.0f)),
                                scale * (Shift(building.TopPosition) * virtualCellHeight)),
                            new PointF(
                                scale * ((Shift(building.LeftPosition) + (data.Size * 0.5f)) * virtualCellWidth + (virtualCellWidth * -0.25f)),
                                scale * (Shift(building.TopPosition) * virtualCellHeight + (virtualCellHeight * 0.5f))),
                            new PointF(
                                scale * ((Shift(building.LeftPosition) + (data.Size * 0.5f)) * virtualCellWidth + (virtualCellWidth * 0.25f)),
                                scale * (Shift(building.TopPosition) * virtualCellHeight + (virtualCellHeight * 0.5f))),
                        });
                        break;
                    case Enums.BuildingDirection.Right:
                        g.FillPolygon(triangleBrush, new PointF[]
                        {
                            new PointF(
                                scale * ((Shift(building.LeftPosition) + data.Size) * virtualCellWidth),
                                scale * ((Shift(building.TopPosition) + (data.Size * 0.5f)) * virtualCellHeight + (virtualCellHeight * 0.0f))),
                            new PointF(
                                scale * ((Shift(building.LeftPosition) + data.Size) * virtualCellWidth - (virtualCellWidth * 0.5f)),
                                scale * ((Shift(building.TopPosition) + (data.Size * 0.5f)) * virtualCellHeight + (virtualCellHeight * -0.25f))),
                            new PointF(
                                scale * ((Shift(building.LeftPosition) + data.Size) * virtualCellWidth - (virtualCellWidth * 0.5f)),
                                scale * ((Shift(building.TopPosition) + (data.Size * 0.5f)) * virtualCellHeight + (virtualCellHeight * 0.25f))),
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
                float physX = scale * virtualCellWidth * Shift(_selectedBuilding.LeftPosition);
                float physY = scale * virtualCellHeight * Shift(_selectedBuilding.TopPosition);

                g.DrawRectangle(p, physX, physY, physW, physH);
            }
        }

        // Select a building or rotate it clockwise
        private void picCastle_MouseClick(object sender, MouseEventArgs e)
        {
            int virtX = (int)(physicalMousePosition.X / scale);
            int virtY = (int)(physicalMousePosition.Y / scale);

            // TODO: Maybe compare point with current rectangle to shortcut past the "consult map" for rotations and make this feel more responsive

            if (e.Button == MouseButtons.Left) // Select or deselect (TODO: Move)
            {
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
                    DeselectBuilding();
                }
                else
                {
                    SelectBuilding(selectedBuilding);
                }
            }
            else if (e.Button == MouseButtons.Right) // Rotate
            {
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
                if (selectedBuilding != null)
                {
                    RotateBuilding(selectedBuilding);
                }
            }
        }

        /// <summary>
        /// Rotate a building clockwise
        /// </summary>
        private void RotateBuilding(Model.Building building)
        {
            if (building.DirectionFacing == Enums.BuildingDirection.Down)
            {
                building.DirectionFacing = Enums.BuildingDirection.Left;
            }
            else if (building.DirectionFacing == Enums.BuildingDirection.Left)
            {
                building.DirectionFacing = Enums.BuildingDirection.Up;
            }
            else if (building.DirectionFacing == Enums.BuildingDirection.Up)
            {
                building.DirectionFacing = Enums.BuildingDirection.Right;
            }
            else if (building.DirectionFacing == Enums.BuildingDirection.Right)
            {
                building.DirectionFacing = Enums.BuildingDirection.Down;
            }
        }

        private void DeselectBuilding()
        {
            _selectedBuilding = null;
            lblSelectedBuilding.Text = "Selected: (none)";
            _selectedBuildingData = null;
        }

        private void SelectBuilding(Model.Building building)
        {
            _selectedBuilding = building;
            lblSelectedBuilding.Text = $"Selected: {_selectedBuilding.BuildingID.ToString()}";
            _selectedBuildingData = Data.Database.Buildings.GetByID(_selectedBuilding.BuildingID);
        }

        // Start showing hover outline
        private void picCastle_MouseMove(object sender, MouseEventArgs e)
        {
            physicalMousePosition = new Point(e.X, e.Y);
            picCastle.Invalidate();
        }

        // Stop showing hover outline
        private void picCastle_MouseLeave(object sender, EventArgs e)
        {
            physicalMousePosition = Point.Empty;
            picCastle.Invalidate();
        }

        /// <summary>
        /// Changes a one-indexed value to a zero-indexed value. For Model.Building position values, which are one-indexed
        /// </summary>
        /// <remarks>This is to cut down on all the stray "- 1" in the code</remarks>
        private int Shift(int modelBuildingPosition)
        {
            return modelBuildingPosition - 1;
        }
    }
}
