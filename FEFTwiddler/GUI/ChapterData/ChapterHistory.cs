using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FEFTwiddler.GUI.ChapterData
{
    public partial class ChapterHistory : Form
    {
        private Model.ChapterSave _chapterSave;

        public ChapterHistory(Model.ChapterSave chapterSave)
        {
            _chapterSave = chapterSave;
            InitializeComponent();
        }

        private void ChapterHistory_Load(object sender, EventArgs e)
        {
            PopulateHistoryPanel();
        }
        
        private void PopulateHistoryPanel()
        {
            foreach (var historyEntry in _chapterSave.UserRegion.ChapterHistory)
            {
                var panel = new ChapterHistoryPanel(historyEntry);
                flwChaptersCompleted.Controls.Add(panel);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _chapterSave.UserRegion.ChapterHistory = new List<Model.ChapterHistoryEntry>();
            foreach (ChapterHistoryPanel panel in flwChaptersCompleted.Controls)
            {
                _chapterSave.UserRegion.ChapterHistory.Add(panel.HistoryEntry);
            }
            this.Close();
        }
    }
}
