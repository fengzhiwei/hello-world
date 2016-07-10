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
using DevExpress.XtraCharts;
using PipeLine.Diagram;
using PipeLine.Class;

namespace PipeLine.ChildWindow
{
    public partial class PipeLength : DevExpress.XtraEditors.XtraForm
    {
        private AxMapControl m_axMapControl;
        DataTable dt = new DataTable();
        public PipeLength(AxMapControl axMapControl)
        {
            InitializeComponent();
            m_axMapControl = axMapControl;
        }

        private void PipeLength_Load(object sender, EventArgs e)
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
                        pipe_clbc.Items.Add(layerName);
                    }

                }
            }
            moreThan.Properties.Items.Add(">");
            moreThan.Properties.Items.Add(">=");
            lessThan.Properties.Items.Add("<");
            lessThan.Properties.Items.Add("<=");
        }

        private void allselect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < pipe_clbc.Items.Count; i++)
            {
                this.pipe_clbc.SetItemChecked(i, true);
            }
        }

        private void clearselect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < pipe_clbc.Items.Count; i++)
            {
                this.pipe_clbc.SetItemChecked(i, false);
            }
        }

        private void statisticsbt_Click(object sender, EventArgs e)
        {
            try
            {
                if (moreThan.Text == "" | lessThan.Text == "" | maxTe.Text == "" | minTe.Text == "")
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("请设置长度范围！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    this.m_axMapControl.Map.ClearSelection();
                    this.m_axMapControl.ActiveView.Refresh();
                    string selectedLayerName;
                    dt.Rows.Clear();
                    dt.Columns.Clear();
                    if (dt.Columns.Count <= 0)
                    {
                        dt.Columns.Add("管线名称");
                        dt.Columns.Add("长度范围");
                        dt.Columns.Add("个数");
                        dt.Columns.Add("实际长度");
                    }
                    
                    IFeatureLayer m_FeatureLayer;
                    IFeatureClass m_FeatureClass;
                    IFeature m_Feature;
                    int m_FeatureCount;


                    IQueryFilter m_QueryFilter = new QueryFilterClass();
                    m_QueryFilter.WhereClause = "长度" + moreThan.Text + maxTe.Text + " and " + "长度" + lessThan.Text + minTe.Text;
                    IFeature C_Feature;
                    for (int i = 0; i < pipe_clbc.Items.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        if (pipe_clbc.GetItemChecked(i))
                        {
                            selectedLayerName = pipe_clbc.Items[i].ToString();
                            for (int j = 0; j < m_axMapControl.Map.LayerCount; j++)
                            {
                                double min = 0;
                                double sum_length = 0;
                                if (selectedLayerName == m_axMapControl.Map.get_Layer(j).Name)
                                {
                                    m_FeatureLayer = m_axMapControl.Map.get_Layer(j) as IFeatureLayer;
                                    m_FeatureClass = m_FeatureLayer.FeatureClass;
                                    m_FeatureCount = m_FeatureClass.FeatureCount(m_QueryFilter);
                                    if (m_FeatureCount == 0)
                                    {
                                        DevExpress.XtraEditors.XtraMessageBox.Show(m_axMapControl.Map.get_Layer(j).Name + "没有符合要求的管线！", "管网系统信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        break;
                                    }
                                    else
                                    {
                                        //arrray_Feature.Clear();
                                        m_Feature = m_FeatureClass.GetFeature(1);
                                        for (int k = 1; k < m_FeatureCount; k++)
                                        {
                                            
                                            C_Feature = m_FeatureClass.GetFeature(k);
                                            int index = C_Feature.Fields.FindField("长度");
                                            sum_length = sum_length + (double)C_Feature.get_Value(index);
                                            this.m_axMapControl.Map.SelectFeature(m_FeatureLayer, C_Feature);
                                        }
                                        dr[0] = selectedLayerName;
                                        dr[1] = "(" + maxTe.Text + "," + minTe.Text + ")";
                                        dr[2] = m_FeatureCount;
                                        dr[3] = sum_length;
                                        dt.Rows.Add(dr);
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            continue;
                        }
                        
                    }
                    this.gridControl1.DataSource = dt;
                    this.gridView1.BestFitColumns();
                    this.m_axMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                }
            }
            catch (Exception)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("操作错误！", "管网系统信息", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        PipeLength_Chart pipeLength_Chart;
        //显示统计图
        private void DisplayChart_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["PipeLength_Chart"] == null)
            {
                Series series02 = new Series();
                List<Series> test = seriesList(dt, series02);
                pipeLength_Chart = new PipeLength_Chart(dt);
                pipeLength_Chart.chartControl1.Series.Add(test[0]);
                pipeLength_Chart.Show();
            }
            else
            {
                pipeLength_Chart.chartControl1.Series.Clear();
                Series series02 = new Series();
                List<Series> test = seriesList(dt, series02);
                pipeLength_Chart.chartControl1.Series.Add(test[0]);
                Application.OpenForms["PipeLength_Chart"].Show();

            }
        }

        public List<Series> seriesList(DataTable mdataTable, Series series02)
        {
            List<Series> series_list = new List<Series>();
            DevExpress.XtraCharts.ChartControl chartContrl = new ChartControl();
            series02 = new Series("图层", ViewType.Bar);
            SeriesPoint point;
            //DataTable dt = GetDataSource;
            for (int i = 0; i < mdataTable.Rows.Count; i++)
            {
                point = new SeriesPoint(mdataTable.Rows[i]["管线名称"].ToString(),
                    Convert.ToDouble(mdataTable.Rows[i]["个数"].ToString()));
                series02.Points.Add(point);

            }
            series_list.Add(series02);
            return series_list;
        }

        private void maxTe_Validated(object sender, EventArgs e)
        {
            double leftNumer;
            Numberic numberClass = new Numberic();
            if (numberClass.isNumberic(maxTe.Text, out leftNumer))
            {
                return;
            }
            else
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("输入无效");
            }
        }

        private void minTe_Validated(object sender, EventArgs e)
        {
            double rightNumer;
            Numberic numberClass = new Numberic();
            if (numberClass.isNumberic(minTe.Text, out rightNumer))
            {
                return;
            }
            else
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("输入无效");
            }
        }
    }
}