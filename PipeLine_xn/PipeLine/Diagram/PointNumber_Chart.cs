using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraCharts;
using PipeLine.Class;

namespace PipeLine.Diagram
{
    public partial class PointNumber_Chart : DevExpress.XtraEditors.XtraForm
    {
        DataTable m_dataTable;
        ChartControl m_ChartControl;
        List<Series> series_list;
        public PointNumber_Chart(DataTable datatable,List<Series> serieslist)
        {
            InitializeComponent();
            m_dataTable = datatable;
            series_list = serieslist;
        }
        
        //加载统计图
        private void PointNumber_Chart_Load(object sender, EventArgs e)
        {
            #region
            //chartControl1.Series
            #endregion
        }

        
        //关闭窗体事件
        private void PointNumber_Chart_FormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show("是否保存数据？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                ChartToExcel chartToExcel = new ChartToExcel();
                chartToExcel.ChartToExcel_test(m_dataTable);
            }
            else
            {
                return;
            }
        }



    }
}