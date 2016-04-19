using System;
using System.Drawing;
using System.Windows.Forms;

namespace FEFTwiddler.GUI.ChapterData
{
    public class ChapterHistoryPanel : Panel
    {
        private Model.ChapterHistoryEntry _historyEntry;
        public Model.ChapterHistoryEntry HistoryEntry
        {
            get { return _historyEntry; }
        }

        public ChapterHistoryPanel(Model.ChapterHistoryEntry historyEntry)
        {
            _historyEntry = historyEntry;

            this.Width = 500;
            this.Height = 20;
            this.Padding = new Padding(0);
            this.Margin = new Padding(0);
            //this.BorderStyle = BorderStyle.FixedSingle;

            var removeButton = new Button();
            removeButton.Top = 0;
            removeButton.Left = 0;
            removeButton.Width = 40;
            removeButton.Height = this.Height;
            removeButton.Text = "X";
            removeButton.ForeColor = Color.Red;
            removeButton.Click += RemoveEntry;
            this.Controls.Add(removeButton);

            var label = new Label();
            label.TextAlign = ContentAlignment.MiddleLeft;
            label.Top = 0;
            label.Left = 40;
            label.Width = this.Width - 40;
            label.Height = this.Height;
            label.Text = _historyEntry.ToString();
            this.Controls.Add(label);
        }

        public void RemoveEntry(object sender, EventArgs e)
        {
            this.Parent.Controls.Remove(this);
        }
    }
}
