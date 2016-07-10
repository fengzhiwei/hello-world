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
    public partial class MeasureArea : DevExpress.XtraEditors.XtraForm
    {
        public MeasureArea()
        {
            InitializeComponent();
        }
        public String PolygonArea
        {
            get { return areaTB.Text; }
            set { areaTB.Text = value; }
        }

        private void MeasureArea_FormClosed(object sender, FormClosedEventArgs e)
        {
            main.choice = 0;
        }
    }
}