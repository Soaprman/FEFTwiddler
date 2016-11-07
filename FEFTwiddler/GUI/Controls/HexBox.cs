using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FEFTwiddler.Extensions;

namespace FEFTwiddler.GUI.Controls
{
    /// <summary>
    /// Here it is, everyone... the most ghetto multiline MaskedTextBox on the planet
    /// </summary>
    /// <remarks>Probably not gonna bother ever refactoring this code lol</remarks>
    public partial class HexBox : UserControl
    {
        private byte[] _bytes;
        private const int BytesPerRow = 0x10;

        public HexBox()
        {
            InitializeComponent();
        }

        public void SetBytes(byte[] bytes)
        {
            _bytes = (byte[])bytes.Clone();
            Resize();
        }

        public byte[] GetBytes()
        {
            return _bytes;
        }

        private new void Resize()
        {
            // All these numbers are "good enough" numbers that I pulled out my ass because I'm not good at Windows Forms
            // Especially the Height multipliers...

            var rowCount = ((_bytes.Length - 1) / BytesPerRow) + 1;
            for (var row = 0; row < rowCount; row++)
            {
                var bytesInRow = Math.Min(_bytes.Length - (row * BytesPerRow), BytesPerRow);

                var maskedBox = new HexBoxMaskedTextBox();
                maskedBox.Row = row;
                maskedBox.Width = 380;
                maskedBox.Mask = GetMaskedBoxPattern(bytesInRow);
                if (maskedBox.Mask == "") maskedBox.Enabled = false;
                maskedBox.Text = GetMaskedBoxText(row);
                maskedBox.Font = new Font("Courier New", 10);
                maskedBox.BorderStyle = BorderStyle.None;
                maskedBox.TextChanged += UpdateBytes;
                pnlRows.Controls.Add(maskedBox);
                pnlRows.Height = 2 + (int)(pnlRows.Controls.Count * (maskedBox.Height * 1.4));

                var lbl = new Label();
                lbl.Height = (int)(maskedBox.Height * 1.4);
                lbl.Font = new Font("Courier New", 9.75f);
                lbl.ForeColor = Color.Blue;
                lbl.Text = string.Format("{0:X2}", row * BytesPerRow);
                pnlRowLabels.Controls.Add(lbl);
                pnlRowLabels.Height = 2 + (int)(pnlRowLabels.Controls.Count * (lbl.Height * 1.4));

                this.Height = 2 + lblColumns.Height + pnlRows.Height;
            }
        }

        private string GetMaskedBoxPattern(int numBytes)
        {
            var pattern = "";
            for (var i = 0; i < numBytes; i++)
            {
                if (pattern.Length > 0) pattern += " ";
                pattern += "AA";
            }
            return pattern;
        }

        private string GetMaskedBoxText(int row)
        {
            var text = "";
            var firstByteIndex = row * BytesPerRow;
            var lastByteIndex = firstByteIndex + Math.Min(_bytes.Length - firstByteIndex, BytesPerRow) - 1;
            for (var i = firstByteIndex; i <= lastByteIndex; i++)
            {
                if (text.Length > 0) text += " ";
                text += string.Format("{0:X2}", _bytes[i]);
            }
            return text;
        }

        private void UpdateBytes(object sender, EventArgs e)
        {
            var maskedBox = (HexBoxMaskedTextBox)sender;
            var pattern = "^(?<first>[0-9a-fA-F]{2})( (?<rest>[0-9a-fA-F]{2}))*$";
            var matches = Regex.Matches(maskedBox.Text, pattern);
            if (matches.Count == 0) return; // Invalid

            var rowBytes = new List<byte>();
            foreach (Match match in matches)
            {
                var first = match.Groups["first"];
                foreach (Capture cap in first.Captures)
                {
                    var thisByte = new byte[1];
                    thisByte.TryParseHex(cap.Value);
                    rowBytes.Add(thisByte[0]);
                }

                var rest = match.Groups["rest"];
                var i = 1;
                foreach (Capture cap in rest.Captures)
                {
                    var thisByte = new byte[1];
                    thisByte.TryParseHex(cap.Value);
                    rowBytes.Add(thisByte[0]);
                    i++;
                }
            }

            Array.Copy(rowBytes.ToArray(), 0, _bytes, maskedBox.Row * BytesPerRow, rowBytes.Count);
        }
    }
}
