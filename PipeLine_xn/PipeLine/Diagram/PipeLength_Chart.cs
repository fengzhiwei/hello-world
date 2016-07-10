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
    public partial class PipeLength_Chart : DevExpress.XtraEditors.XtraForm
    {
        DataTable m_dataTable;
        public PipeLength_Chart(DataTable datatable)
        {
            InitializeComponent();
            m_dataTable = datatable;
        }
        //窗体关闭事件
        private void PipeLength_Chart_FormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show("是否保存数据？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                ChartToExcel_Length chartToExcel = new ChartToExcel_Length();
                chartToExcel.ChartToExcel_test(m_dataTable);
            }
            else
            {
                return;
            }
        }

        
    }
}