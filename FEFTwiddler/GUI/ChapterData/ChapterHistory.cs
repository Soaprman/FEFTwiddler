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
        private Model.IChapterSave _chapterSave;
        private Model.NewGamePlus.TimeMachine _timeMachine;

        public ChapterHistory(Model.IChapterSave chapterSave)
        {
            _chapterSave = chapterSave;
            _timeMachine = new Model.NewGamePlus.TimeMachine(_chapterSave);

            InitializeComponent();
        }

        private void ChapterHistory_Load(object sender, EventArgs e)
        {
            PopulateHistoryPanel();
            UpdateAvailableBattlefields();
        }
        
        private void PopulateHistoryPanel()
        {
            foreach (var historyEntry in _chapterSave.UserRegion.ChapterHistory)
            {
                var panel = new ChapterHistoryPanel(historyEntry);
                flwChaptersCompleted.Controls.Add(panel);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void UnplayChapter(Enums.Chapter chapterId)
        {
            _timeMachine.UnplayChapter(chapterId);
            UpdateAvailableBattlefields();
        }

        public bool CanUnplayChapter(Enums.Chapter chapterId)
        {
            return _timeMachine.CanUnplayChapter(chapterId);
        }

        public static ChapterHistory GetFromHere(Control ctl)
        {
            var parent = ctl.Parent;
            while (parent.GetType() != typeof(ChapterHistory))
            {
                parent = parent.Parent;
            }
            return (ChapterHistory)parent;
        }

        private void UpdateAvailableBattlefields()
        {
            flwBattlefieldsAvailable.Controls.Clear();

            foreach (var battlefield in _chapterSave.BattlefieldRegion.Battlefields
                .Where(x => x.BattlefieldStatus == Enums.BattlefieldStatus.Available))
            {
                flwBattlefieldsAvailable.Controls.Add(new BattlefieldPanel(battlefield));
            }
        }

        private void btnUnlockAmiiboChapters_Click(object sender, EventArgs e)
        {
            _timeMachine.UnlockChapter(Enums.Chapter.HeroBattle_Marth);
            _timeMachine.UnlockChapter(Enums.Chapter.HeroBattle_Ike);
            _timeMachine.UnlockChapter(Enums.Chapter.HeroBattle_Lucina);
            _timeMachine.UnlockChapter(Enums.Chapter.HeroBattle_Robin);

            UpdateAvailableBattlefields();

            MessageBox.Show("Done!");
        }
    }
}
