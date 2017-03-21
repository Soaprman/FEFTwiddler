using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace FEFTwiddler.GUI.ChapterData
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class Difficulty : UserControl
    {
        private Model.IChapterSave _chapterSave;

        public Difficulty()
        {
            InitializeComponent();
            InitializeControls();
        }

        public void LoadChapterSave(Model.IChapterSave chapterSave)
        {
            _chapterSave = chapterSave;
            UnbindEvents();
            PopulateControls();
            BindEvents();
        }

        private void InitializeControls()
        {
        }

        private void PopulateControls()
        {
            switch (_chapterSave.UserRegion.Difficulty)
            {
                case Enums.Difficulty.Normal:
                    rdoNormal.Checked = true; break;
                case Enums.Difficulty.Hard:
                    rdoHard.Checked = true; break;
                case Enums.Difficulty.Lunatic:
                    rdoLunatic.Checked = true; break;
            }

            if (_chapterSave.UserRegion.UnitsReviveAfterTurn) rdoPhoenix.Checked = true;
            else if (_chapterSave.UserRegion.UnitsReviveAfterChapter) rdoCasual.Checked = true;
            else rdoClassic.Checked = true;
        }

        private void UnbindEvents()
        {
            rdoNormal.CheckedChanged -= SetNormal;
            rdoHard.CheckedChanged -= SetHard;
            rdoLunatic.CheckedChanged -= SetLunatic;
            rdoPhoenix.CheckedChanged -= SetPhoenix;
            rdoCasual.CheckedChanged -= SetCasual;
            rdoClassic.CheckedChanged -= SetClassic;
        }

        private void BindEvents()
        {
            rdoNormal.CheckedChanged += SetNormal;
            rdoHard.CheckedChanged += SetHard;
            rdoLunatic.CheckedChanged += SetLunatic;
            rdoPhoenix.CheckedChanged += SetPhoenix;
            rdoCasual.CheckedChanged += SetCasual;
            rdoClassic.CheckedChanged += SetClassic;
        }

        private void SetNormal(object sender, EventArgs e)
        {
            if (!IsChecked(sender)) return;
            _chapterSave.UserRegion.Difficulty = Enums.Difficulty.Normal;
            _chapterSave.Header.Difficulty = Enums.Difficulty.Normal;
        }

        private void SetHard(object sender, EventArgs e)
        {
            if (!IsChecked(sender)) return;
            _chapterSave.UserRegion.Difficulty = Enums.Difficulty.Hard;
            _chapterSave.Header.Difficulty = Enums.Difficulty.Hard;
        }

        private void SetLunatic(object sender, EventArgs e)
        {
            if (!IsChecked(sender)) return;
            _chapterSave.UserRegion.Difficulty = Enums.Difficulty.Lunatic;
            _chapterSave.Header.Difficulty = Enums.Difficulty.Lunatic;
        }

        private void SetPhoenix(object sender, EventArgs e)
        {
            if (!IsChecked(sender)) return;
            _chapterSave.UserRegion.UnitsReviveAfterTurn = true;
            _chapterSave.UserRegion.UnitsReviveAfterChapter = true;
            _chapterSave.Header.UnitsReviveAfterTurn = true;
            _chapterSave.Header.UnitsReviveAfterChapter = true;
        }

        private void SetCasual(object sender, EventArgs e)
        {
            if (!IsChecked(sender)) return;
            _chapterSave.UserRegion.UnitsReviveAfterTurn = false;
            _chapterSave.UserRegion.UnitsReviveAfterChapter = true;
            _chapterSave.Header.UnitsReviveAfterTurn = false;
            _chapterSave.Header.UnitsReviveAfterChapter = true;
        }

        private void SetClassic(object sender, EventArgs e)
        {
            if (!IsChecked(sender)) return;
            _chapterSave.UserRegion.UnitsReviveAfterTurn = false;
            _chapterSave.UserRegion.UnitsReviveAfterChapter = false;
            _chapterSave.Header.UnitsReviveAfterTurn = false;
            _chapterSave.Header.UnitsReviveAfterChapter = false;
        }

        /// <summary>
        /// This might not be needed but I'm kind of new to "radio buttons" lol
        /// </summary>
        private bool IsChecked(object sender)
        {
            var rdo = (RadioButton)sender;
            return rdo.Checked;
        }
    }
}
