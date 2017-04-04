using System;
using System.Drawing;
using System.Windows.Forms;

namespace FEFTwiddler.GUI.GameProgress
{
    public class BattlefieldPanel : Panel
    {
        private Model.Battlefield _battlefield;
        public Model.Battlefield Battlefield
        {
            get { return _battlefield; }
        }

        public BattlefieldPanel(Model.Battlefield battlefield)
        {
            _battlefield = battlefield;

            this.Width = 500;
            this.Height = 20;
            this.Padding = new Padding(0);
            this.Margin = new Padding(0);

            var chapterData = Data.Database.Chapters.GetByID(_battlefield.ChapterID);

            var label1 = new Label();
            label1.TextAlign = ContentAlignment.MiddleLeft;
            label1.Top = 0;
            label1.Left = 0;
            label1.Width = 400;
            label1.Height = this.Height;
            label1.Text = chapterData.DisplayName;
            this.Controls.Add(label1);
        }
    }
}
