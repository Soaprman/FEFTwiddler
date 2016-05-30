using System;
using System.Windows.Forms;

namespace FEFTwiddler.GUI.UnitViewer
{
    public partial class Traits : Form
    {
        private Model.Unit _unit;

        public Traits(Model.Unit unit)
        {
            _unit = unit;
            InitializeComponent();
            PopulateControls();
            BindEvents();
        }

        public void PopulateControls()
        {
            chkFlag_00_01.Checked = _unit.Flag_00_01;
            chkFlag_00_02.Checked = _unit.Flag_00_02;
            chkFlag_00_04.Checked = _unit.Flag_00_04;
            chkFlag_00_08.Checked = _unit.Flag_00_08;
            chkFlag_00_10.Checked = _unit.Flag_00_10;
            chkFlag_00_20.Checked = _unit.Flag_00_20;
            chkFlag_00_40.Checked = _unit.Flag_00_40;
            chkFlag_00_80.Checked = _unit.Flag_00_80;
            chkFlag_01_01.Checked = _unit.Flag_01_01;
            chkFlag_01_02.Checked = _unit.Flag_01_02;
            chkFlag_01_04.Checked = _unit.Flag_01_04;
            chkFlag_01_08.Checked = _unit.Flag_01_08;
            chkFlag_01_10.Checked = _unit.Flag_01_10;
            chkFlag_01_20.Checked = _unit.Flag_01_20;
            chkFlag_01_40.Checked = _unit.Flag_01_40;
            chkFlag_01_80.Checked = _unit.Flag_01_80;
            chkFlag_02_01.Checked = _unit.Flag_02_01;
            chkFlag_02_02.Checked = _unit.Flag_02_02;
            chkFlag_02_04.Checked = _unit.Flag_02_04;
            chkFlag_02_08.Checked = _unit.Flag_02_08;
            chkFlag_02_10.Checked = _unit.Flag_02_10;
            chkFlag_02_20.Checked = _unit.Flag_02_20;
            chkFlag_02_40.Checked = _unit.Flag_02_40;
            chkFlag_02_80.Checked = _unit.Flag_02_80;
            chkFlag_03_01.Checked = _unit.Flag_03_01;
            chkFlag_03_02.Checked = _unit.Flag_03_02;
            chkFlag_03_04.Checked = _unit.Flag_03_04;
            chkFlag_03_08.Checked = _unit.Flag_03_08;
            chkFlag_03_10.Checked = _unit.Flag_03_10;
            chkFlag_03_20.Checked = _unit.Flag_03_20;
            chkFlag_03_40.Checked = _unit.Flag_03_40;
            chkFlag_03_80.Checked = _unit.Flag_03_80;
            chkFlag_04_01.Checked = _unit.Flag_04_01;
            chkFlag_04_02.Checked = _unit.Flag_04_02;
            chkFlag_04_04.Checked = _unit.Flag_04_04;
            chkFlag_04_08.Checked = _unit.Flag_04_08;
            chkFlag_04_10.Checked = _unit.Flag_04_10;
            chkFlag_04_20.Checked = _unit.Flag_04_20;
            chkFlag_04_40.Checked = _unit.Flag_04_40;
            chkFlag_04_80.Checked = _unit.Flag_04_80;
            chkFlag_05_01.Checked = _unit.Flag_05_01;
            chkFlag_05_02.Checked = _unit.Flag_05_02;
            chkFlag_05_04.Checked = _unit.Flag_05_04;
            chkFlag_05_08.Checked = _unit.Flag_05_08;
            chkFlag_05_10.Checked = _unit.Flag_05_10;
            chkFlag_05_20.Checked = _unit.Flag_05_20;
            chkFlag_05_40.Checked = _unit.Flag_05_40;
            chkFlag_05_80.Checked = _unit.Flag_05_80;
            chkFlag_06_01.Checked = _unit.Flag_06_01;
            chkFlag_06_02.Checked = _unit.Flag_06_02;
            chkFlag_06_04.Checked = _unit.Flag_06_04;
            chkFlag_06_08.Checked = _unit.Flag_06_08;
            chkFlag_06_10.Checked = _unit.Flag_06_10;
            chkFlag_06_20.Checked = _unit.Flag_06_20;
            chkFlag_06_40.Checked = _unit.Flag_06_40;
            chkFlag_06_80.Checked = _unit.Flag_06_80;
            chkFlag_07_01.Checked = _unit.Flag_07_01;
            chkFlag_07_02.Checked = _unit.Flag_07_02;
            chkFlag_07_04.Checked = _unit.Flag_07_04;
            chkFlag_07_08.Checked = _unit.Flag_07_08;
            chkFlag_07_10.Checked = _unit.Flag_07_10;
            chkFlag_07_20.Checked = _unit.Flag_07_20;
            chkFlag_07_40.Checked = _unit.Flag_07_40;
            chkFlag_07_80.Checked = _unit.Flag_07_80;
        }

        public void BindEvents()
        {
            chkFlag_00_01.Click += delegate (object sender, EventArgs e) { _unit.Flag_00_01 = ((CheckBox)sender).Checked; };
            chkFlag_00_02.Click += delegate (object sender, EventArgs e) { _unit.Flag_00_02 = ((CheckBox)sender).Checked; };
            chkFlag_00_04.Click += delegate (object sender, EventArgs e) { _unit.Flag_00_04 = ((CheckBox)sender).Checked; };
            chkFlag_00_08.Click += delegate (object sender, EventArgs e) { _unit.Flag_00_08 = ((CheckBox)sender).Checked; };
            chkFlag_00_10.Click += delegate (object sender, EventArgs e) { _unit.Flag_00_10 = ((CheckBox)sender).Checked; };
            chkFlag_00_20.Click += delegate (object sender, EventArgs e) { _unit.Flag_00_20 = ((CheckBox)sender).Checked; };
            chkFlag_00_40.Click += delegate (object sender, EventArgs e) { _unit.Flag_00_40 = ((CheckBox)sender).Checked; };
            chkFlag_00_80.Click += delegate (object sender, EventArgs e) { _unit.Flag_00_80 = ((CheckBox)sender).Checked; };
            chkFlag_01_01.Click += delegate (object sender, EventArgs e) { _unit.Flag_01_01 = ((CheckBox)sender).Checked; };
            chkFlag_01_02.Click += delegate (object sender, EventArgs e) { _unit.Flag_01_02 = ((CheckBox)sender).Checked; };
            chkFlag_01_04.Click += delegate (object sender, EventArgs e) { _unit.Flag_01_04 = ((CheckBox)sender).Checked; };
            chkFlag_01_08.Click += delegate (object sender, EventArgs e) { _unit.Flag_01_08 = ((CheckBox)sender).Checked; };
            chkFlag_01_10.Click += delegate (object sender, EventArgs e) { _unit.Flag_01_10 = ((CheckBox)sender).Checked; };
            chkFlag_01_20.Click += delegate (object sender, EventArgs e) { _unit.Flag_01_20 = ((CheckBox)sender).Checked; };
            chkFlag_01_40.Click += delegate (object sender, EventArgs e) { _unit.Flag_01_40 = ((CheckBox)sender).Checked; };
            chkFlag_01_80.Click += delegate (object sender, EventArgs e) { _unit.Flag_01_80 = ((CheckBox)sender).Checked; };
            chkFlag_02_01.Click += delegate (object sender, EventArgs e) { _unit.Flag_02_01 = ((CheckBox)sender).Checked; };
            chkFlag_02_02.Click += delegate (object sender, EventArgs e) { _unit.Flag_02_02 = ((CheckBox)sender).Checked; };
            chkFlag_02_04.Click += delegate (object sender, EventArgs e) { _unit.Flag_02_04 = ((CheckBox)sender).Checked; };
            chkFlag_02_08.Click += delegate (object sender, EventArgs e) { _unit.Flag_02_08 = ((CheckBox)sender).Checked; };
            chkFlag_02_10.Click += delegate (object sender, EventArgs e) { _unit.Flag_02_10 = ((CheckBox)sender).Checked; };
            chkFlag_02_20.Click += delegate (object sender, EventArgs e) { _unit.Flag_02_20 = ((CheckBox)sender).Checked; };
            chkFlag_02_40.Click += delegate (object sender, EventArgs e) { _unit.Flag_02_40 = ((CheckBox)sender).Checked; };
            chkFlag_02_80.Click += delegate (object sender, EventArgs e) { _unit.Flag_02_80 = ((CheckBox)sender).Checked; };
            chkFlag_03_01.Click += delegate (object sender, EventArgs e) { _unit.Flag_03_01 = ((CheckBox)sender).Checked; };
            chkFlag_03_02.Click += delegate (object sender, EventArgs e) { _unit.Flag_03_02 = ((CheckBox)sender).Checked; };
            chkFlag_03_04.Click += delegate (object sender, EventArgs e) { _unit.Flag_03_04 = ((CheckBox)sender).Checked; };
            chkFlag_03_08.Click += delegate (object sender, EventArgs e) { _unit.Flag_03_08 = ((CheckBox)sender).Checked; };
            chkFlag_03_10.Click += delegate (object sender, EventArgs e) { _unit.Flag_03_10 = ((CheckBox)sender).Checked; };
            chkFlag_03_20.Click += delegate (object sender, EventArgs e) { _unit.Flag_03_20 = ((CheckBox)sender).Checked; };
            chkFlag_03_40.Click += delegate (object sender, EventArgs e) { _unit.Flag_03_40 = ((CheckBox)sender).Checked; };
            chkFlag_03_80.Click += delegate (object sender, EventArgs e) { _unit.Flag_03_80 = ((CheckBox)sender).Checked; };
            chkFlag_04_01.Click += delegate (object sender, EventArgs e) { _unit.Flag_04_01 = ((CheckBox)sender).Checked; };
            chkFlag_04_02.Click += delegate (object sender, EventArgs e) { _unit.Flag_04_02 = ((CheckBox)sender).Checked; };
            chkFlag_04_04.Click += delegate (object sender, EventArgs e) { _unit.Flag_04_04 = ((CheckBox)sender).Checked; };
            chkFlag_04_08.Click += delegate (object sender, EventArgs e) { _unit.Flag_04_08 = ((CheckBox)sender).Checked; };
            chkFlag_04_10.Click += delegate (object sender, EventArgs e) { _unit.Flag_04_10 = ((CheckBox)sender).Checked; };
            chkFlag_04_20.Click += delegate (object sender, EventArgs e) { _unit.Flag_04_20 = ((CheckBox)sender).Checked; };
            chkFlag_04_40.Click += delegate (object sender, EventArgs e) { _unit.Flag_04_40 = ((CheckBox)sender).Checked; };
            chkFlag_04_80.Click += delegate (object sender, EventArgs e) { _unit.Flag_04_80 = ((CheckBox)sender).Checked; };
            chkFlag_05_01.Click += delegate (object sender, EventArgs e) { _unit.Flag_05_01 = ((CheckBox)sender).Checked; };
            chkFlag_05_02.Click += delegate (object sender, EventArgs e) { _unit.Flag_05_02 = ((CheckBox)sender).Checked; };
            chkFlag_05_04.Click += delegate (object sender, EventArgs e) { _unit.Flag_05_04 = ((CheckBox)sender).Checked; };
            chkFlag_05_08.Click += delegate (object sender, EventArgs e) { _unit.Flag_05_08 = ((CheckBox)sender).Checked; };
            chkFlag_05_10.Click += delegate (object sender, EventArgs e) { _unit.Flag_05_10 = ((CheckBox)sender).Checked; };
            chkFlag_05_20.Click += delegate (object sender, EventArgs e) { _unit.Flag_05_20 = ((CheckBox)sender).Checked; };
            chkFlag_05_40.Click += delegate (object sender, EventArgs e) { _unit.Flag_05_40 = ((CheckBox)sender).Checked; };
            chkFlag_05_80.Click += delegate (object sender, EventArgs e) { _unit.Flag_05_80 = ((CheckBox)sender).Checked; };
            chkFlag_06_01.Click += delegate (object sender, EventArgs e) { _unit.Flag_06_01 = ((CheckBox)sender).Checked; };
            chkFlag_06_02.Click += delegate (object sender, EventArgs e) { _unit.Flag_06_02 = ((CheckBox)sender).Checked; };
            chkFlag_06_04.Click += delegate (object sender, EventArgs e) { _unit.Flag_06_04 = ((CheckBox)sender).Checked; };
            chkFlag_06_08.Click += delegate (object sender, EventArgs e) { _unit.Flag_06_08 = ((CheckBox)sender).Checked; };
            chkFlag_06_10.Click += delegate (object sender, EventArgs e) { _unit.Flag_06_10 = ((CheckBox)sender).Checked; };
            chkFlag_06_20.Click += delegate (object sender, EventArgs e) { _unit.Flag_06_20 = ((CheckBox)sender).Checked; };
            chkFlag_06_40.Click += delegate (object sender, EventArgs e) { _unit.Flag_06_40 = ((CheckBox)sender).Checked; };
            chkFlag_06_80.Click += delegate (object sender, EventArgs e) { _unit.Flag_06_80 = ((CheckBox)sender).Checked; };
            chkFlag_07_01.Click += delegate (object sender, EventArgs e) { _unit.Flag_07_01 = ((CheckBox)sender).Checked; };
            chkFlag_07_02.Click += delegate (object sender, EventArgs e) { _unit.Flag_07_02 = ((CheckBox)sender).Checked; };
            chkFlag_07_04.Click += delegate (object sender, EventArgs e) { _unit.Flag_07_04 = ((CheckBox)sender).Checked; };
            chkFlag_07_08.Click += delegate (object sender, EventArgs e) { _unit.Flag_07_08 = ((CheckBox)sender).Checked; };
            chkFlag_07_10.Click += delegate (object sender, EventArgs e) { _unit.Flag_07_10 = ((CheckBox)sender).Checked; };
            chkFlag_07_20.Click += delegate (object sender, EventArgs e) { _unit.Flag_07_20 = ((CheckBox)sender).Checked; };
            chkFlag_07_40.Click += delegate (object sender, EventArgs e) { _unit.Flag_07_40 = ((CheckBox)sender).Checked; };
            chkFlag_07_80.Click += delegate (object sender, EventArgs e) { _unit.Flag_07_80 = ((CheckBox)sender).Checked; };

            btnClose.Click += delegate (object sender, EventArgs e) { Close(); };
        }
    }
}
