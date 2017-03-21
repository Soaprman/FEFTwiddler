using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace FEFTwiddler.GUI.ChapterData.Megacheats
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class Skills : UserControl
    {
        private Model.IChapterSave _chapterSave;

        public Skills()
        {
            InitializeComponent();
            InitializeControls();
        }

        public void LoadChapterSave(Model.IChapterSave chapterSave)
        {
            _chapterSave = chapterSave;
            PopulateControls();
        }

        private void InitializeControls()
        {
            BindEvents();
        }

        private void PopulateControls()
        {
        }

        private void BindEvents()
        {
            btnLearnNormalClassSkills.Click += LearnNormalClassSkills;
            btnLearnCorrinOnlySkills.Click += LearnCorrinOnlySkills;
            btnLearnAzuraOnlySkills.Click += LearnAzuraOnlySkills;
            btnLearnBeastOnlySkills.Click += LearnBeastOnlySkills;
            btnLearnKitsuneOnlySkills.Click += LearnKitsuneOnlySkills;
            btnLearnWolfskinOnlySkills.Click += LearnWolfskinOnlySkills;
            btnLearnVillagerOnlySkills.Click += LearnVillagerOnlySkills;
            btnLearnPathBonusClassSkills.Click += LearnPathBonusClassSkills;
            btnLearnDlcClassSkills.Click += LearnDlcClassSkills;
            btnLearnAmiiboClassSkills.Click += LearnAmiiboClassSkills;
            btnLearnEnemyAndNpcSkills.Click += LearnEnemyAndNpcSkills;
            btnLearnAllSkills.Click += LearnAllSkills;

            btnUnlearnNormalClassSkills.Click += UnlearnNormalClassSkills;
            btnUnlearnCorrinOnlySkills.Click += UnlearnCorrinOnlySkills;
            btnUnlearnAzuraOnlySkills.Click += UnlearnAzuraOnlySkills;
            btnUnlearnBeastOnlySkills.Click += UnlearnBeastOnlySkills;
            btnUnlearnKitsuneOnlySkills.Click += UnlearnKitsuneOnlySkills;
            btnUnlearnWolfskinOnlySkills.Click += UnlearnWolfskinOnlySkills;
            btnUnlearnVillagerOnlySkills.Click += UnlearnVillagerOnlySkills;
            btnUnlearnPathBonusClassSkills.Click += UnlearnPathBonusClassSkills;
            btnUnlearnDlcClassSkills.Click += UnlearnDlcClassSkills;
            btnUnlearnAmiiboClassSkills.Click += UnlearnAmiiboClassSkills;
            btnUnlearnEnemyAndNpcSkills.Click += UnlearnEnemyAndNpcSkills;
            btnUnlearnAllSkills.Click += UnlearnAllSkills;
        }

        #region Event Handlers

        public void LearnNormalClassSkills(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.UnitRegion.Units) character.LearnNormalClassSkills();
            MessageBox.Show("Done!");
        }

        public void LearnCorrinOnlySkills(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.UnitRegion.Units) character.LearnCorrinOnlySkills();
            MessageBox.Show("Done!");
        }

        public void LearnAzuraOnlySkills(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.UnitRegion.Units) character.LearnAzuraOnlySkills();
            MessageBox.Show("Done!");
        }

        public void LearnBeastOnlySkills(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.UnitRegion.Units) character.LearnBeastOnlySkills();
            MessageBox.Show("Done!");
        }

        public void LearnKitsuneOnlySkills(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.UnitRegion.Units) character.LearnKitsuneOnlySkills();
            MessageBox.Show("Done!");
        }

        public void LearnWolfskinOnlySkills(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.UnitRegion.Units) character.LearnWolfskinOnlySkills();
            MessageBox.Show("Done!");
        }

        public void LearnVillagerOnlySkills(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.UnitRegion.Units) character.LearnVillagerOnlySkills();
            MessageBox.Show("Done!");
        }

        public void LearnPathBonusClassSkills(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.UnitRegion.Units) character.LearnPathBonusClassSkills();
            MessageBox.Show("Done!");
        }

        public void LearnDlcClassSkills(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.UnitRegion.Units) character.LearnDlcClassSkills();
            MessageBox.Show("Done!");
        }

        public void LearnAmiiboClassSkills(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.UnitRegion.Units) character.LearnAmiiboClassSkills();
            MessageBox.Show("Done!");
        }

        public void LearnEnemyAndNpcSkills(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.UnitRegion.Units) character.LearnEnemyAndNpcSkills();
            MessageBox.Show("Done!");
        }

        public void LearnAllSkills(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.UnitRegion.Units) character.LearnAllSkills();
            MessageBox.Show("Done!");
        }

        public void UnlearnNormalClassSkills(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.UnitRegion.Units) character.UnlearnNormalClassSkills();
            MessageBox.Show("Done!");
        }

        public void UnlearnCorrinOnlySkills(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.UnitRegion.Units) character.UnlearnCorrinOnlySkills();
            MessageBox.Show("Done!");
        }

        public void UnlearnAzuraOnlySkills(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.UnitRegion.Units) character.UnlearnAzuraOnlySkills();
            MessageBox.Show("Done!");
        }

        public void UnlearnBeastOnlySkills(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.UnitRegion.Units) character.UnlearnBeastOnlySkills();
            MessageBox.Show("Done!");
        }

        public void UnlearnKitsuneOnlySkills(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.UnitRegion.Units) character.UnlearnKitsuneOnlySkills();
            MessageBox.Show("Done!");
        }

        public void UnlearnWolfskinOnlySkills(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.UnitRegion.Units) character.UnlearnWolfskinOnlySkills();
            MessageBox.Show("Done!");
        }

        public void UnlearnVillagerOnlySkills(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.UnitRegion.Units) character.UnlearnVillagerOnlySkills();
            MessageBox.Show("Done!");
        }

        public void UnlearnPathBonusClassSkills(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.UnitRegion.Units) character.UnlearnPathBonusClassSkills();
            MessageBox.Show("Done!");
        }

        public void UnlearnDlcClassSkills(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.UnitRegion.Units) character.UnlearnDlcClassSkills();
            MessageBox.Show("Done!");
        }

        public void UnlearnAmiiboClassSkills(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.UnitRegion.Units) character.UnlearnAmiiboClassSkills();
            MessageBox.Show("Done!");
        }

        public void UnlearnEnemyAndNpcSkills(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.UnitRegion.Units) character.UnlearnEnemyAndNpcSkills();
            MessageBox.Show("Done!");
        }

        public void UnlearnAllSkills(object sender, EventArgs e)
        {
            foreach (var character in _chapterSave.UnitRegion.Units) character.UnlearnAllSkills();
            MessageBox.Show("Done!");
        }

        #endregion
    }
}
