using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

namespace FEFTwiddler.GUI.Controls
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class ColorPicker : UserControl
    {
        private ColorDialog _colorDialog = new ColorDialog();

        private Color _color;
        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                UpdatePreview();
                UpdateHex();
            }
        }

        private Action<Color> _onChange = null;
        /// <summary>
        /// After changing the color, pass the new color to this method
        /// </summary>
        public Action<Color> OnChange
        {
            get { return _onChange; }
            set { _onChange = value; }
        }

        public ColorPicker()
        {
            InitializeComponent();
            BindEvents();
        }

        private void BindEvents()
        {
            pnlPreview.Click += HandlePreviewClicked;
            txtHex.TextChanged += HandleHexChanged;
        }

        private void UnbindEvents()
        {
            pnlPreview.Click -= HandlePreviewClicked;
            txtHex.TextChanged -= HandleHexChanged;
        }

        private void HandlePreviewClicked(object sender, EventArgs e)
        {
            OpenColorDialog();
            _onChange?.Invoke(_color);
        }

        private void HandleHexChanged(object sender, EventArgs e)
        {
            ParseHex();
            _onChange?.Invoke(_color);
        }

        private void UpdatePreview()
        {
            UnbindEvents();
            pnlPreview.BackColor = _color;
            BindEvents();
        }

        private void UpdateHex()
        {
            UnbindEvents();
            txtHex.Text = string.Format("{0:X2}{1:X2}{2:X2}", _color.R, _color.G, _color.B);
            BindEvents();
        }

        private void OpenColorDialog()
        {
            UnbindEvents();
            _colorDialog.Color = _color;
            _colorDialog.ShowDialog();
            _color = Color.FromArgb(_color.A, _colorDialog.Color.R, _colorDialog.Color.G, _colorDialog.Color.B);
            BindEvents();

            UpdatePreview();
            UpdateHex();
        }

        private void ParseHex()
        {
            if (txtHex.Text.Length < 6) return;

            byte r; byte g; byte b;
            if (byte.TryParse(txtHex.Text.Substring(0, 2), System.Globalization.NumberStyles.HexNumber, null, out r) &&
                byte.TryParse(txtHex.Text.Substring(2, 2), System.Globalization.NumberStyles.HexNumber, null, out g) &&
                byte.TryParse(txtHex.Text.Substring(4, 2), System.Globalization.NumberStyles.HexNumber, null, out b))
            {
                _color = Color.FromArgb(_color.A, r, g, b);

                UpdatePreview();
            }
        }
    }
}
