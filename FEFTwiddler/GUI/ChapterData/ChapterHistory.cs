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

        //private List<Model.ChapterHistoryEntry> _backupChapterHistory;
        //private List<Model.Battlefield> _backupBattlefields;

        public ChapterHistory(Model.IChapterSave chapterSave)
        {
            _chapterSave = chapterSave;
            _timeMachine = new Model.NewGamePlus.TimeMachine(_chapterSave);

            //CreateBackups();

            InitializeComponent();
        }

        //private void CreateBackups()
        //{
        //    _backupChapterHistory = new List<Model.ChapterHistoryEntry>();
        //    foreach (var item in _chapterSave.UserRegion.ChapterHistory)
        //    {
        //        _backupChapterHistory.Add(item);
        //    }

        //    _backupBattlefields = new List<Model.Battlefield>();
        //    foreach (var item in _chapterSave.BattlefieldRegion.Battlefields)
        //    {
        //        _backupBattlefields.Add(item);
        //    }
        //}

        //private void RestoreBackups()
        //{
        //    _chapterSave.UserRegion.ChapterHistory = _backupChapterHistory;
        //    _chapterSave.BattlefieldRegion.Battlefields = _backupBattlefields;
        //}

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
            //RestoreBackups();

            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void UnplayChapter(Enums.Chapter chapterId)
        {
            _timeMachine.UnplayChapter(chapterId);
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

        private void btnUnlockAmiiboChapters_Click(object sender, EventArgs e)
        {
            _timeMachine.UnlockChapter(Enums.Chapter.HeroBattle_Marth);
            _timeMachine.UnlockChapter(Enums.Chapter.HeroBattle_Ike);
            _timeMachine.UnlockChapter(Enums.Chapter.HeroBattle_Lucina);
            _timeMachine.UnlockChapter(Enums.Chapter.HeroBattle_Robin);

            MessageBox.Show("Done! (Trust me, they're there!)");
        }
    }
}
