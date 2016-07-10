using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using PipeLine.Class;

namespace PipeLine.Diagram
{
    public partial class PipeLineStatistic_Chart : DevExpress.XtraEditors.XtraForm
    {
        DataTable dt;

        public PipeLineStatistic_Chart(DataTable datatable)
        {
            InitializeComponent();
            dt = datatable;
        }

        private void PipeLineStatistic_Chart_FormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show("是否保存数据？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                ChartToExcel_Line chartToExcel = new ChartToExcel_Line();
                chartToExcel.ChartToExcel_test(dt);
            }
            else
            {
                return;
            }
        }
    }
}