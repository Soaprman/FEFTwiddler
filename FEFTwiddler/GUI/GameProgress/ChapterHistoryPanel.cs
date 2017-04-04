using System;
using System.Drawing;
using System.Windows.Forms;

namespace FEFTwiddler.GUI.GameProgress
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

            var label1 = new Label();
            label1.TextAlign = ContentAlignment.MiddleLeft;
            label1.Top = 0;
            label1.Left = 40;
            label1.Width = 260;
            label1.Height = this.Height;
            label1.Text = GetLabel1Text(_historyEntry);
            this.Controls.Add(label1);

            var label2 = new Label();
            label2.TextAlign = ContentAlignment.MiddleLeft;
            label2.Top = 0;
            label2.Left = 300;
            label2.Width = this.Width - 300;
            label2.Height = this.Height;
            label2.Text = GetLabel2Text(_historyEntry);
            this.Controls.Add(label2);
        }

        private string GetLabel1Text(Model.ChapterHistoryEntry historyEntry)
        {
            return Data.Database.Chapters.GetByID(historyEntry.ChapterID).DisplayName;
        }

        private string GetLabel2Text(Model.ChapterHistoryEntry historyEntry)
        {
            string hero1 = Data.Database.Characters.GetByID(historyEntry.HeroCharacterID_1).DisplayName;
            string hero2 = Data.Database.Characters.GetByID(historyEntry.HeroCharacterID_2).DisplayName;

            return $"Turns: {historyEntry.TurnCount} / Heroes: {hero1}, {hero2}";
        }

        public void RemoveEntry(object sender, EventArgs e)
        {
            var gameProgressPanel = GameProgressMain.GetFromHere(this);

            if (gameProgressPanel.CanUnplayChapter(_historyEntry.ChapterID))
            {
                gameProgressPanel.UnplayChapter(_historyEntry.ChapterID);
                this.Parent.Controls.Remove(this);
            }
            else
            {
                MessageBox.Show("Before unplaying this chapter, unplay any played chapters that were unlocked by playing it.");
            }
        }
    }
}
