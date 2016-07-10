using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace PipeLine.ChildWindow
{
    public partial class MeasureLine : DevExpress.XtraEditors.XtraForm
    {
        public MeasureLine()
        {
            InitializeComponent();
        }
        public String Distance
        {
            get { return lineLengthTB.Text; }
            set { lineLengthTB.Text = value; }
        }

        private void MeasureLine_FormClosed(object sender, FormClosedEventArgs e)
        {
            main.choice = 0;
        }
    }
}