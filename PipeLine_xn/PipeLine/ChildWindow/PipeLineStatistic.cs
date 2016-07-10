using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using PipeLine.Diagram;
using DevExpress.XtraCharts;

namespace PipeLine.ChildWindow
{
    public partial class PipeLineStatistic : DevExpress.XtraEditors.XtraForm
    {
        private AxMapControl m_axMapControl;
        DataTable dt = new DataTable();
        public PipeLineStatistic(AxMapControl axMapControl)
        {
            InitializeComponent();
            this.m_axMapControl = axMapControl;
        }

        private void PipeLineStatistic_Load(object sender, EventArgs e)
        {
            string layerName;
            IFeatureLayer m_FeatureLayer;
            IFeatureClass m_FeatureClass;
            IFeature m_Feature;
            for (int i = 0; i < m_axMapControl.Map.LayerCount; i++)
            {
                m_FeatureLayer = m_axMapControl.Map.get_Layer(i) as IFeatureLayer;
                m_FeatureClass = m_FeatureLayer.FeatureClass;
                //m_Feature = m_FeatureClass.GetFeature(0);
                if (m_FeatureClass.ShapeType == ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline)
                {
                    layerName = m_axMapControl.Map.get_Layer(i).Name;
                    if (layerName == "铁路" | layerName == "直埋电缆")
                    {
                        continue;
                    }
                    else
                    {
                        this.pipe_clbc.Items.Add(layerName);
                    }
                }
            }
            this.materialclb.Items.Add("钢");
            this.usedateclb.Items.Add("2013年7月1日");
            for (int i = 0; i < this.materialclb.Items.Count; i++)
            {
                this.materialclb.SetItemChecked(i, true);
            }
            for (int i = 0; i < this.usedateclb.Items.Count; i++)
            {
                this.usedateclb.SetItemChecked(i, true);
            }
        }

        private void allselect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < pipe_clbc.Items.Count; i++)
            {
                this.pipe_clbc.SetItemChecked(i, true);
            }
        }

        private void allclear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < pipe_clbc.Items.Count; i++)
            {
                this.pipe_clbc.SetItemChecked(i, false);
            }
        }
        //统计
        private void statistic_Click(object sender, EventArgs e)
        {
            this.m_axMapControl.Map.ClearSelection();
            this.m_axMapControl.ActiveView.Refresh();
            int mCheckedCount = this.materialclb.CheckedItems.Count;
            int uCheckedCount = this.usedateclb.CheckedItems.Count;
            dt.Rows.Clear();
            dt.Columns.Clear();
            if (mCheckedCount > 0 & uCheckedCount > 0)
            {
                string selectLayerName;

                if (dt.Columns.Count <= 0)
                {
                    dt.Columns.Add("管线名称");
                    dt.Columns.Add("管线数量");
                    dt.Columns.Add("材质");
                    dt.Columns.Add("最大值");
                    dt.Columns.Add("最小值");
                    dt.Columns.Add("总长度");
                    dt.Columns.Add("使用日期");
                }

                IFeatureLayer m_FeatureLayer;
                IFeatureClass m_FeatureClass;
                IFeature m_Feature;
                int m_FeatureCount;
                IQueryFilter m_QueryFilter = new QueryFilterClass();
                string queryFilterString;

                //材质的查询条件语句
                string materialFilter;
                string m = null;
                if (mCheckedCount == 1)
                {
                    materialFilter = "材质='" + this.materialclb.CheckedItems[0].ToString() + "'";
                }
                else
                {
                    for (int i = 0; i < this.materialclb.CheckedItems.Count - 1; i++)
                    {
                        m = m + "材质=" + "'" + this.materialclb.CheckedItems[i].ToString() + "'" + " OR ";
                    }
                    materialFilter = m + "材质=" + "'" + this.materialclb.CheckedItems[mCheckedCount - 1].ToString() + "'";
                }

                //使用日期的查询条件语句
                string useDateFilter;
                string u = null;
                if (uCheckedCount == 1)
                {
                    useDateFilter = "使用日期=" + "'" + this.usedateclb.CheckedItems[0].ToString() + "'";
                }
                else
                {
                    for (int i = 0; i < this.usedateclb.CheckedItems.Count - 1; i++)
                    {
                        u = u + "使用日期=" + "'" + this.usedateclb.CheckedItems[i].ToString() + "'" + " OR ";
                    }
                    useDateFilter = u + "使用日期=" + "'" + this.usedateclb.CheckedItems[uCheckedCount - 1].ToString() + "'";
                }

                queryFilterString = "(" + materialFilter + ")" + "and" + "(" + useDateFilter + ")";
                m_QueryFilter.WhereClause = queryFilterString;
                IFeature c_Feature;

                for (int i = 0; i < pipe_clbc.Items.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    if (pipe_clbc.GetItemChecked(i))
                    {
                        selectLayerName = pipe_clbc.Items[i].ToString();
                        for (int j = 0; j < m_axMapControl.Map.LayerCount; j++)
                        {
                            if (selectLayerName == m_axMapControl.Map.get_Layer(j).Name)
                            {
                                double lineLength = 0;
                                double sum = 0;
                                double max = 0;
                                double min = 0;
                                m_FeatureLayer = m_axMapControl.get_Layer(j) as IFeatureLayer;
                                m_FeatureClass = m_FeatureLayer.FeatureClass;

                                m_FeatureCount = m_FeatureClass.FeatureCount(m_QueryFilter);
                                m_Feature = m_FeatureClass.GetFeature(1);
                                for (int l = 1; l < m_FeatureCount; l++)
                                {
                                    c_Feature = m_FeatureClass.GetFeature(l);
                                    this.m_axMapControl.Map.SelectFeature(m_FeatureLayer, c_Feature);
                                }
                                //获取管线长度的最值
                                double compareMax = (double)m_Feature.get_Value(m_Feature.Fields.FindField("长度"));
                                double compareMin = (double)m_Feature.get_Value(m_Feature.Fields.FindField("长度"));
                                max = compareMax;
                                min = compareMax;
                                string material = null;
                                string useDate = null;
                                for (int f = 1; f < m_FeatureCount; f++)
                                {
                                    m_Feature = m_FeatureClass.GetFeature(f);
                                    lineLength = (double)m_Feature.get_Value(m_Feature.Fields.FindField("长度"));
                                    material = (string)m_Feature.get_Value(m_Feature.Fields.FindField("材质"));
                                    useDate = (string)m_Feature.get_Value(m_Feature.Fields.FindField("使用日期"));
                                    sum = sum + lineLength;
                                    if (lineLength > compareMax)
                                    {
                                        max = lineLength;
                                        compareMax = lineLength;
                                    }
                                    if (lineLength < compareMin)
                                    {
                                        min = lineLength;
                                        compareMin = lineLength;
                                    }
                                    //MessageBox.Show(sum.ToString());
                                }
                                if (m_FeatureCount == 0)
                                {
                                    dr[3] = 0;
                                    dr[4] = 0;
                                }
                                else
                                {
                                    dr[3] = max;
                                    dr[4] = min;
                                }
                                dr[0] = selectLayerName;
                                dr[1] = m_FeatureCount;
                                dr[2] = material;
                                dr[5] = sum;
                                dr[6] = useDate;
                                dt.Rows.Add(dr);
                                break;

                            }
                        }
                    }
                    
                }
                this.gridControl1.DataSource = dt;
                this.gridView1.BestFitColumns();
                this.m_axMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                DevExpress.XtraEditors.XtraMessageBox.Show("查询完成！", "管网系统信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void addmaterialbt_Click(object sender, EventArgs e)
        {
            AddInCLB(materialclb, addmaterialtb.Text, "该材质已存在！");
        }

        private void addusedatebt_Click(object sender, EventArgs e)
        {
            AddInCLB(usedateclb, usedateTime.Text, "日期已存在！");
        }
        public void AddInCLB(CheckedListBoxControl clb, string info, string mes)
        {
            string addName = info;
            int count = 0;
            for (int i = 0; i < clb.Items.Count; i++)
            {
                if (clb.Items[i].ToString() == addName)
                {
                    MessageBox.Show(mes);
                    break;
                }
                count++;
                //MessageBox.Show(materialclb.Items[i].ToString());
            }
            if (count == clb.Items.Count)
            {
                clb.Items.Add(addName);
            }
        }

        private void usedateTime_EditValueChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 显示统计图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        PipeLineStatistic_Chart pipeLineStatistic_chart;
        private void DisplayChart_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["PipeLineStatistic_Chart"] == null)
            {
                Series series02 = new Series();
                Series series03 = new Series();
                List<Series> test = seriesList(dt, series02, series03);
                pipeLineStatistic_chart = new PipeLineStatistic_Chart(dt);
                pipeLineStatistic_chart.chartControl1.Series.Add(test[0]);
                pipeLineStatistic_chart.Show();
            }
            else
            {
                pipeLineStatistic_chart.chartControl1.Series.Clear();
                Series series02 = new Series();
                Series series03 = new Series();
                List<Series> test = seriesList(dt, series02, series03);
                pipeLineStatistic_chart.chartControl1.Series.Add(test[0]);
                Application.OpenForms["PipeLineStatistic_Chart"].Show();
            }
        }


        public List<Series> seriesList(DataTable mdataTable, Series series02, Series series03)
        {
            List<Series> series_list = new List<Series>();
            DevExpress.XtraCharts.ChartControl chartContrl = new ChartControl();
            series02 = new Series("管点(2)", ViewType.Bar);
            series03 = new Series("管点(3)", ViewType.Bar);
            SeriesPoint point;
            //DataTable dt = GetDataSource;
            for (int i = 0; i < mdataTable.Rows.Count; i++)
            {
                point = new SeriesPoint(mdataTable.Rows[i]["管线名称"].ToString(),
                    Convert.ToDouble(mdataTable.Rows[i]["管线数量"].ToString()));
                series02.Points.Add(point);

                point = new SeriesPoint(mdataTable.Rows[i]["管线名称"].ToString(),
                    Convert.ToDouble(mdataTable.Rows[i]["管线数量"].ToString()));
                series03.Points.Add(point);

            }
            series_list.Add(series02);
            series_list.Add(series03);
            return series_list;
        }

    }
}