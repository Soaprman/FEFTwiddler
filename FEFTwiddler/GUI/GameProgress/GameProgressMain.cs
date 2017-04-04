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

namespace FEFTwiddler.GUI.GameProgress
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class GameProgressMain : UserControl
    {
        private Model.IChapterSave _chapterSave;
        private Model.NewGamePlus.TimeMachine _timeMachine;

        public GameProgressMain()
        {
            InitializeComponent();
            InitializeControls();
        }

        public void LoadChapterSave(Model.IChapterSave chapterSave)
        {
            _chapterSave = chapterSave;
            _timeMachine = new Model.NewGamePlus.TimeMachine(_chapterSave);

            PopulateControls();
        }

        private void InitializeControls()
        {
        }

        private void PopulateControls()
        {
            PopulateHistoryPanel();
            UpdateAvailableBattlefields();
        }

        private void PopulateHistoryPanel()
        {
            flwChaptersCompleted.Controls.Clear();

            foreach (var historyEntry in _chapterSave.UserRegion.ChapterHistory)
            {
                var panel = new ChapterHistoryPanel(historyEntry);
                flwChaptersCompleted.Controls.Add(panel);
            }
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

        public static GameProgressMain GetFromHere(Control ctl)
        {
            var parent = ctl.Parent;
            while (parent.GetType() != typeof(GameProgressMain))
            {
                parent = parent.Parent;
            }
            return (GameProgressMain)parent;
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
