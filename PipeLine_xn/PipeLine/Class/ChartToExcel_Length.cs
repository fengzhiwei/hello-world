using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Office.Interop.Excel;

namespace PipeLine.Class
{
    class ChartToExcel_Length
    {
        //管线长度统计导出Excel
        Microsoft.Office.Interop.Excel.Application ThisApplication = null;
        Microsoft.Office.Interop.Excel.Workbooks m_objBooks = null;
        Microsoft.Office.Interop.Excel._Workbook ThisWorkbook = null;
        Microsoft.Office.Interop.Excel.Worksheet xlSheet = null;

        public void ChartToExcel_test(System.Data.DataTable dataTable)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = "xls";
            dlg.Filter = "EXCEL文件(*.XLS)|*.xls";
            dlg.InitialDirectory = Directory.GetCurrentDirectory();
            if (dlg.ShowDialog() == DialogResult.Cancel) return;
            string fileNameString = dlg.FileName;
            if (fileNameString.Trim() == "") return;
            FileInfo file = new FileInfo(fileNameString);
            if (file.Exists)
            {
                try
                {
                    file.Delete();
                }
                catch (Exception error)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(error.Message, "删除失败 ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            try
            {


                ThisApplication = new Microsoft.Office.Interop.Excel.Application();
                m_objBooks = (Microsoft.Office.Interop.Excel.Workbooks)ThisApplication.Workbooks;
                ThisWorkbook = (Microsoft.Office.Interop.Excel._Workbook)(m_objBooks.Add(Type.Missing));

                ThisApplication.DisplayAlerts = false;

                this.DeleteSheet();
                this.AddDatasheet();
                this.LoadData(dataTable);

                CreateChart();
                //C:\Users\Administrator\Desktop
                ThisWorkbook.SaveAs(fileNameString, Type.Missing, Type.Missing,
                            Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
            }
            finally
            {
                ThisWorkbook.Close(Type.Missing, Type.Missing, Type.Missing);
                ThisApplication.Workbooks.Close();

                ThisApplication.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(ThisWorkbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(ThisApplication);
                ThisWorkbook = null;
                ThisApplication = null;
                GC.Collect();
                //this.Close();
            }

        }
        /**/
        /// 
        /// 删除多余的Sheet
        /// 
        private void DeleteSheet()
        {
            foreach (Microsoft.Office.Interop.Excel.Worksheet ws in ThisWorkbook.Worksheets)
                if (ws != ThisApplication.ActiveSheet)
                {
                    ws.Delete();
                }
            foreach (Microsoft.Office.Interop.Excel.Chart cht in ThisWorkbook.Charts)
                cht.Delete();

        }
        private void AddDatasheet()
        {
            xlSheet = (Microsoft.Office.Interop.Excel.Worksheet)ThisWorkbook.Worksheets.Add(Type.Missing, ThisWorkbook.ActiveSheet,
                Type.Missing, Type.Missing);

            xlSheet.Name = "数据";
        }
        /**/
        /// 
        /// 想新建的Sheet插入数据
        /// 
        private void LoadData(System.Data.DataTable dataTable)
        {
            System.Data.DataTable dt_test = dataTable;
            for (int i = 1; i <= dt_test.Rows.Count; i++)
            {
                string kk = dt_test.Rows[i - 1][0].ToString();
                string kk2 = dt_test.Rows[i - 1][2].ToString();
                xlSheet.Cells[i, 1] = dt_test.Rows[i - 1][0].ToString();
                xlSheet.Cells[i, 2] = dt_test.Rows[i - 1][2].ToString();
            }
        }
        private void CreateChart()
        {
            Microsoft.Office.Interop.Excel.Chart xlChart = (Microsoft.Office.Interop.Excel.Chart)ThisWorkbook.Charts.
                Add(Type.Missing, xlSheet, Type.Missing, Type.Missing);

            Microsoft.Office.Interop.Excel.Range cellRange = (Microsoft.Office.Interop.Excel.Range)xlSheet.Cells[1, 1];
            xlChart.ChartWizard(cellRange.CurrentRegion,
                Microsoft.Office.Interop.Excel.XlChartType.xlColumnClustered, Type.Missing,
                Microsoft.Office.Interop.Excel.XlRowCol.xlColumns, 1, 0, true,
                "管线数量统计图", "图层名称", "数量", "");

            xlChart.Name = "统计图";

            Microsoft.Office.Interop.Excel.ChartGroup grp = (Microsoft.Office.Interop.Excel.ChartGroup)xlChart.ChartGroups(1);
            grp.GapWidth = 20;
            grp.VaryByCategories = true;


            Microsoft.Office.Interop.Excel.Series s = (Microsoft.Office.Interop.Excel.Series)grp.SeriesCollection(1);
            s.BarShape = XlBarShape.xlCylinder;
            s.HasDataLabels = true;
            s.Name = "数量";

            xlChart.Legend.Position = XlLegendPosition.xlLegendPositionTop;
            //xlChart.Legend.
            xlChart.ChartTitle.Font.Size = 24;
            xlChart.ChartTitle.Shadow = true;
            xlChart.ChartTitle.Border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

            Microsoft.Office.Interop.Excel.Axis valueAxis = (Microsoft.Office.Interop.Excel.Axis)xlChart.Axes(Microsoft.Office.Interop.Excel.XlAxisType.xlValue, XlAxisGroup.xlPrimary);
            valueAxis.AxisTitle.Orientation = -90;

            Microsoft.Office.Interop.Excel.Axis categoryAxis = (Microsoft.Office.Interop.Excel.Axis)xlChart.Axes(Microsoft.Office.Interop.Excel.XlAxisType.xlCategory, XlAxisGroup.xlPrimary);
            categoryAxis.AxisTitle.Font.Name = "MS UI Gothic";
        }


    }
}
