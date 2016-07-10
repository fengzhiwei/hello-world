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
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using System.IO;
using System.Reflection;
using PipeLine.Diagram;
using PipeLine.Class;
using DevExpress.XtraCharts;

namespace PipeLine.ChildWindow
{
    public partial class PointNumberQuery : DevExpress.XtraEditors.XtraForm
    {
        AxMapControl m_axMapControl;
        
        DataTable dt = new DataTable();
        List<Series> lSeries = new List<Series>();
        public PointNumberQuery(AxMapControl axMapControl)
        {
            InitializeComponent();
            m_axMapControl = axMapControl;
        }
        //全选
        private void AllSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < LineListBoxControl1.Items.Count; i++)
            {
                LineListBoxControl1.SetItemChecked(i, true);
            }
        }
        //清空
        private void AllClear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < LineListBoxControl1.Items.Count; i++)
            {
                LineListBoxControl1.SetItemChecked(i, false);
            }
        }

        private void PointNumberQuery_Load(object sender, EventArgs e)
        {
            IFeatureLayer pFeatureLayer;
            for (int i = 0; i < m_axMapControl.Map.LayerCount; i++)
            {
                pFeatureLayer = m_axMapControl.Map.get_Layer(i) as IFeatureLayer;
                if (pFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)//判断图层要素是否为点要素
                {
                    if (pFeatureLayer.Name.ToString() == "铁路" | pFeatureLayer.Name.ToString() == "直埋电缆")
                    {
                        continue;
                    }
                    else
                    {
                        LineListBoxControl1.Items.Add(m_axMapControl.Map.get_Layer(i).Name.ToString());
                    }

                }

            }
        }
        //统计 
        private void Statistic_Click(object sender, EventArgs e)
        {
            int flag = 0;
            string type = "类型";
            gridControl1.DataSource = null;
            dt.Columns.Clear();
            dt.Rows.Clear();
            string CLBC1_item, type_value;
            m_axMapControl.Map.ClearSelection();
            m_axMapControl.ActiveView.Refresh();
            
            for (int i = 0; i < LineListBoxControl1.Items.Count; i++)
            {
                if (LineListBoxControl1.GetItemChecked(i))
                {
                    flag = flag + 1;
                }
                else
                {
                    continue;
                }
            }
            if (flag > 0) //线选择
            {
                dt.Columns.Add("图层");
                dt.Columns.Add("二通管点数目");
                dt.Columns.Add("三通管点数目");
                dt.Columns.Add("四通管点数目");
                for (int i = 0; i < LineListBoxControl1.Items.Count; i++)
                {
                    int two = 0;
                    int three = 0;
                    int four = 0;
                    if (LineListBoxControl1.GetItemChecked(i))
                    {
                        if (TypeListBoxControl2.GetItemChecked(0) & !TypeListBoxControl2.GetItemChecked(1) & !TypeListBoxControl2.GetItemChecked(2))
                        {
                            CLBC1_item = LineListBoxControl1.Items[i].ToString();
                            for (int j = 0; j < m_axMapControl.LayerCount; j++)
                            {
                                type_value = null;
                                if (m_axMapControl.Map.get_Layer(j).Name.Contains(CLBC1_item) && CLBC1_item != m_axMapControl.Map.get_Layer(j).Name)
                                {
                                    IFeatureLayer pFeatureLayer = m_axMapControl.Map.get_Layer(j) as IFeatureLayer;
                                    IQueryFilter m_QueryFilter = new QueryFilterClass();
                                    m_QueryFilter.WhereClause = "1=1";
                                    for (int k = 0; k < pFeatureLayer.FeatureClass.FeatureCount(m_QueryFilter); k++)
                                    {
                                        IFeature m_Feature = pFeatureLayer.FeatureClass.GetFeature(k);
                                        int index = m_Feature.Fields.FindField(type);
                                        if (index < 0)
                                            break;
                                        type_value = m_Feature.get_Value(index).ToString();
                                        int type_valueInt = int.Parse(type_value);
                                        if (type_valueInt == 2)
                                        {
                                            two = two + 1;
                                            m_axMapControl.Map.SelectFeature(m_axMapControl.Map.get_Layer(j), m_Feature);
                                        }
                                    }
                                }
                            }
                            dt.Rows.Add(CLBC1_item, two, 0, 0);
                            m_axMapControl.Refresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                        }
                        if (!TypeListBoxControl2.GetItemChecked(0) & TypeListBoxControl2.GetItemChecked(1) & !TypeListBoxControl2.GetItemChecked(2))
                        {
                            CLBC1_item = LineListBoxControl1.Items[i].ToString();
                            for (int j = 0; j < m_axMapControl.LayerCount; j++)
                            {
                                type_value = null;
                                if (m_axMapControl.Map.get_Layer(j).Name.Contains(CLBC1_item) && CLBC1_item != m_axMapControl.Map.get_Layer(j).Name)
                                {
                                    IFeatureLayer pFeatureLayer = m_axMapControl.Map.get_Layer(j) as IFeatureLayer;
                                    IQueryFilter m_QueryFilter = new QueryFilterClass();
                                    m_QueryFilter.WhereClause = "1=1";
                                    for (int k = 0; k < pFeatureLayer.FeatureClass.FeatureCount(m_QueryFilter); k++)
                                    {
                                        IFeature m_Feature = pFeatureLayer.FeatureClass.GetFeature(k);
                                        int index = m_Feature.Fields.FindField(type);
                                        if (index < 0)
                                            break;
                                        type_value = m_Feature.get_Value(index).ToString();
                                        int type_valueInt = int.Parse(type_value);
                                        if (type_valueInt == 3)
                                        {
                                            three = three + 1;
                                            m_axMapControl.Map.SelectFeature(m_axMapControl.Map.get_Layer(j), m_Feature);
                                        }
                                    }
                                }
                            }
                            dt.Rows.Add(CLBC1_item, 0, three, 0);
                            m_axMapControl.Refresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                        }
                        if (!TypeListBoxControl2.GetItemChecked(0) & !TypeListBoxControl2.GetItemChecked(1) & TypeListBoxControl2.GetItemChecked(2))
                        {
                            CLBC1_item = LineListBoxControl1.Items[i].ToString();
                            for (int j = 0; j < m_axMapControl.LayerCount; j++)
                            {
                                type_value = null;
                                if (m_axMapControl.Map.get_Layer(j).Name.Contains(CLBC1_item) && CLBC1_item != m_axMapControl.Map.get_Layer(j).Name)
                                {
                                    IFeatureLayer pFeatureLayer = m_axMapControl.Map.get_Layer(j) as IFeatureLayer;
                                    IQueryFilter m_QueryFilter = new QueryFilterClass();
                                    m_QueryFilter.WhereClause = "1=1";
                                    for (int k = 0; k < pFeatureLayer.FeatureClass.FeatureCount(m_QueryFilter); k++)
                                    {
                                        IFeature m_Feature = pFeatureLayer.FeatureClass.GetFeature(k);
                                        int index = m_Feature.Fields.FindField(type);
                                        if (index < 0)
                                            break;
                                        type_value = m_Feature.get_Value(index).ToString();
                                        int type_valueInt = int.Parse(type_value);
                                        if (type_valueInt == 4)
                                        {
                                            four = four + 1;
                                            m_axMapControl.Map.SelectFeature(m_axMapControl.Map.get_Layer(j), m_Feature);
                                        }
                                    }
                                }
                            }
                            dt.Rows.Add(CLBC1_item, 0, 0, four);
                            m_axMapControl.Refresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                        }
                        if (TypeListBoxControl2.GetItemChecked(0) & TypeListBoxControl2.GetItemChecked(1) & !TypeListBoxControl2.GetItemChecked(2))
                        {
                            CLBC1_item = LineListBoxControl1.Items[i].ToString();
                            for (int j = 0; j < m_axMapControl.LayerCount; j++)
                            {
                                type_value = null;
                                if (m_axMapControl.Map.get_Layer(j).Name.Contains(CLBC1_item) && CLBC1_item != m_axMapControl.Map.get_Layer(j).Name)
                                {
                                    IFeatureLayer pFeatureLayer = m_axMapControl.Map.get_Layer(j) as IFeatureLayer;
                                    IQueryFilter m_QueryFilter = new QueryFilterClass();
                                    m_QueryFilter.WhereClause = "1=1";
                                    for (int k = 1; k < pFeatureLayer.FeatureClass.FeatureCount(m_QueryFilter); k++)
                                    {
                                        IFeature m_Feature = pFeatureLayer.FeatureClass.GetFeature(k);
                                        int index = m_Feature.Fields.FindField(type);
                                        if (index < 0)
                                            break;
                                        type_value = m_Feature.get_Value(index).ToString();
                                        int type_valueInt = int.Parse(type_value);
                                        if (type_valueInt == 2)
                                        {
                                            two = two + 1;
                                            m_axMapControl.Map.SelectFeature(m_axMapControl.Map.get_Layer(j), m_Feature);
                                        }
                                        if (type_valueInt == 3)
                                        {
                                            three = three + 1;
                                            m_axMapControl.Map.SelectFeature(m_axMapControl.Map.get_Layer(j), m_Feature);
                                        }
                                    }
                                }
                            }
                            dt.Rows.Add(CLBC1_item, two, three, 0);
                            m_axMapControl.Refresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                        }
                        if (TypeListBoxControl2.GetItemChecked(0) & !TypeListBoxControl2.GetItemChecked(1) & TypeListBoxControl2.GetItemChecked(2))
                        {
                            CLBC1_item = LineListBoxControl1.Items[i].ToString();
                            for (int j = 0; j < m_axMapControl.LayerCount; j++)
                            {
                                type_value = null;
                                if (m_axMapControl.Map.get_Layer(j).Name.Contains(CLBC1_item) && CLBC1_item != m_axMapControl.Map.get_Layer(j).Name)
                                {
                                    IFeatureLayer pFeatureLayer = m_axMapControl.Map.get_Layer(j) as IFeatureLayer;
                                    IQueryFilter m_QueryFilter = new QueryFilterClass();
                                    m_QueryFilter.WhereClause = "1=1";
                                    for (int k = 1; k < pFeatureLayer.FeatureClass.FeatureCount(m_QueryFilter); k++)
                                    {
                                        IFeature m_Feature = pFeatureLayer.FeatureClass.GetFeature(k);
                                        int index = m_Feature.Fields.FindField(type);
                                        if (index < 0)
                                            break;
                                        type_value = m_Feature.get_Value(index).ToString();
                                        int type_valueInt = int.Parse(type_value);
                                        if (type_valueInt == 2)
                                        {
                                            two = two + 1;
                                            m_axMapControl.Map.SelectFeature(m_axMapControl.Map.get_Layer(j), m_Feature);
                                        }
                                        if (type_valueInt == 4)
                                        {
                                            four = four + 1;
                                            m_axMapControl.Map.SelectFeature(m_axMapControl.Map.get_Layer(j), m_Feature);
                                        }
                                    }
                                }
                            }
                            dt.Rows.Add(CLBC1_item, two, 0, four);
                            m_axMapControl.Refresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                        }
                        if (!TypeListBoxControl2.GetItemChecked(0) & TypeListBoxControl2.GetItemChecked(1) & TypeListBoxControl2.GetItemChecked(2))
                        {
                            CLBC1_item = LineListBoxControl1.Items[i].ToString();
                            for (int j = 0; j < m_axMapControl.LayerCount; j++)
                            {
                                type_value = null;
                                if (m_axMapControl.Map.get_Layer(j).Name.Contains(CLBC1_item) && CLBC1_item != m_axMapControl.Map.get_Layer(j).Name)
                                {
                                    IFeatureLayer pFeatureLayer = m_axMapControl.Map.get_Layer(j) as IFeatureLayer;
                                    IQueryFilter m_QueryFilter = new QueryFilterClass();
                                    m_QueryFilter.WhereClause = "1=1";
                                    for (int k = 1; k < pFeatureLayer.FeatureClass.FeatureCount(m_QueryFilter); k++)
                                    {
                                        IFeature m_Feature = pFeatureLayer.FeatureClass.GetFeature(k);
                                        int index = m_Feature.Fields.FindField(type);
                                        if (index < 0)
                                            break;
                                        type_value = m_Feature.get_Value(index).ToString();
                                        int type_valueInt = int.Parse(type_value);
                                        if (type_valueInt == 3)
                                        {
                                            three = three + 1;
                                            m_axMapControl.Map.SelectFeature(m_axMapControl.Map.get_Layer(j), m_Feature);
                                        }
                                        if (type_valueInt == 4)
                                        {
                                            four = four + 1;
                                            m_axMapControl.Map.SelectFeature(m_axMapControl.Map.get_Layer(j), m_Feature);
                                        }
                                    }
                                }
                            }
                            dt.Rows.Add(CLBC1_item, 0, three, four);
                            m_axMapControl.Refresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                        }
                        if (TypeListBoxControl2.GetItemChecked(0) & TypeListBoxControl2.GetItemChecked(1) & TypeListBoxControl2.GetItemChecked(2))
                        {
                            CLBC1_item = LineListBoxControl1.Items[i].ToString();
                            for (int j = 0; j < m_axMapControl.LayerCount; j++)
                            {
                                type_value = null;
                                if (m_axMapControl.Map.get_Layer(j).Name.Contains(CLBC1_item) && CLBC1_item != m_axMapControl.Map.get_Layer(j).Name)
                                {
                                    IFeatureLayer pFeatureLayer = m_axMapControl.Map.get_Layer(j) as IFeatureLayer;
                                    IQueryFilter m_QueryFilter = new QueryFilterClass();
                                    m_QueryFilter.WhereClause = "1=1";
                                    for (int k = 1; k < pFeatureLayer.FeatureClass.FeatureCount(m_QueryFilter); k++)
                                    {
                                        IFeature m_Feature = pFeatureLayer.FeatureClass.GetFeature(k);
                                        int index = m_Feature.Fields.FindField(type);
                                        if (index < 0)
                                            break;
                                        type_value = m_Feature.get_Value(index).ToString();
                                        int type_valueInt = int.Parse(type_value);
                                        if (type_valueInt == 2)
                                        {
                                            two = two + 1;
                                            m_axMapControl.Map.SelectFeature(m_axMapControl.Map.get_Layer(j), m_Feature);
                                        }
                                        if (type_valueInt == 3)
                                        {
                                            three = three + 1;
                                            m_axMapControl.Map.SelectFeature(m_axMapControl.Map.get_Layer(j), m_Feature);
                                        }
                                        if (type_valueInt == 4)
                                        {
                                            four = four + 1;
                                            m_axMapControl.Map.SelectFeature(m_axMapControl.Map.get_Layer(j), m_Feature);
                                        }
                                    }
                                }
                            }
                            dt.Rows.Add(CLBC1_item, two, three, four);
                            m_axMapControl.Refresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                        }
                        if (!TypeListBoxControl2.GetItemChecked(0) & !TypeListBoxControl2.GetItemChecked(1) & !TypeListBoxControl2.GetItemChecked(2))
                        {
                            CLBC1_item = LineListBoxControl1.Items[i].ToString();
                            for (int j = 0; j < m_axMapControl.LayerCount; j++)
                            {
                                type_value = null;
                                if (m_axMapControl.Map.get_Layer(j).Name.Contains(CLBC1_item) && CLBC1_item != m_axMapControl.Map.get_Layer(j).Name)
                                {
                                    IFeatureLayer pFeatureLayer = m_axMapControl.Map.get_Layer(j) as IFeatureLayer;
                                    IQueryFilter m_QueryFilter = new QueryFilterClass();
                                    m_QueryFilter.WhereClause = "1=1";
                                    for (int k = 1; k < pFeatureLayer.FeatureClass.FeatureCount(m_QueryFilter); k++)
                                    {
                                        IFeature m_Feature = pFeatureLayer.FeatureClass.GetFeature(k);
                                        int index = m_Feature.Fields.FindField(type);
                                        if (index < 0)
                                            break;
                                        type_value = m_Feature.get_Value(index).ToString();
                                        int type_valueInt = int.Parse(type_value);
                                        if (type_valueInt == 2)
                                        {
                                            two = two + 1;
                                            m_axMapControl.Map.SelectFeature(m_axMapControl.Map.get_Layer(j), m_Feature);
                                        }
                                        if (type_valueInt == 3)
                                        {
                                            three = three + 1;
                                            m_axMapControl.Map.SelectFeature(m_axMapControl.Map.get_Layer(j), m_Feature);
                                        }
                                        if (type_valueInt == 4)
                                        {
                                            four = four + 1;
                                            m_axMapControl.Map.SelectFeature(m_axMapControl.Map.get_Layer(j), m_Feature);
                                        }
                                    }
                                }
                            }
                            dt.Rows.Add(CLBC1_item, two, three, four);
                            m_axMapControl.Refresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                        }

                    }
                    m_axMapControl.Refresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                }
            }//if end
            if (flag == 0)
            {
                int m = 0;
                int n = 0;
                int z = 0;
                int s1 = 0;
                int s2 = 0;
                int s3 = 0;
                m_axMapControl.Map.ClearSelection();
                m_axMapControl.ActiveView.Refresh();
                dt.Columns.Add("二通管点数目");
                dt.Columns.Add("三通管点数目");
                dt.Columns.Add("四通管点数目");
                for (int i = 0; i < TypeListBoxControl2.Items.Count; i++)
                {
                    if (TypeListBoxControl2.GetItemChecked(i))
                    {
                        if (i == 0)
                        {
                            for (int k = 0; k < m_axMapControl.LayerCount; k++)
                            {
                                m = StatistisPoint(2, k);
                                s1 = m + s1;
                            }
                        }
                        if (i == 1)
                        {
                            for (int k = 0; k < m_axMapControl.LayerCount; k++)
                            {
                                n = StatistisPoint(3, k);
                                s2 = n + s2;
                            }
                        }
                        if (i == 2)
                        {
                            for (int k = 0; k < m_axMapControl.LayerCount; k++)
                            {
                                z = StatistisPoint(4, k);
                                s3 = z + s3;
                            }
                        }

                    }
                }
                dt.Rows.Add(s1, s2, s3);
            }
            gridControl1.DataSource = dt;
            this.gridView1.BestFitColumns();
        }

        private int StatistisPoint(int number, int Layer_Number)
        {
            string type;
            int PointNumber = 0;
            IFeatureLayer pFeatureLayer = m_axMapControl.Map.get_Layer(Layer_Number) as IFeatureLayer;
            if (pFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPoint)
            {
                IQueryFilter mQueryFilter = new QueryFilterClass();
                mQueryFilter.WhereClause = "1=1";
                for (int i = 0; i < pFeatureLayer.FeatureClass.FeatureCount(mQueryFilter); i++)
                {
                    IFeature mFeature = pFeatureLayer.FeatureClass.GetFeature(i);
                    int index = mFeature.Fields.FindField("类型");
                    if (index < 0)
                        break;
                    type = mFeature.get_Value(index).ToString();
                    if (int.Parse(type) == number)
                    {
                        PointNumber = PointNumber + 1;
                        m_axMapControl.Map.SelectFeature(m_axMapControl.Map.get_Layer(Layer_Number), mFeature);
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            this.m_axMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
            return PointNumber;
        }
        PointNumber_Chart point_Number_Chart;
        //显示统计图
        private void DiaplayChart_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["PointNumber_Chart"] == null)
            {
                Series series02 = new Series();
                Series series03 = new Series();
                List<Series> test = seriesList(dt, series02, series03);
                point_Number_Chart = new PointNumber_Chart(dt, test);
                point_Number_Chart.chartControl1.Series.Add(test[0]);
                point_Number_Chart.chartControl1.Series.Add(test[1]);
                point_Number_Chart.Show();
            }
            else
            {
                point_Number_Chart.chartControl1.Series.Clear();
                Series series02 = new Series();
                Series series03 = new Series();
                List<Series> test = seriesList(dt, series02, series03);
                point_Number_Chart.chartControl1.Series.Add(test[0]);
                point_Number_Chart.chartControl1.Series.Add(test[1]);
                Application.OpenForms["PointNumber_Chart"].Show();
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
                point = new SeriesPoint(mdataTable.Rows[i]["图层"].ToString(),
                    Convert.ToDouble(mdataTable.Rows[i]["二通管点数目"].ToString()));
                series02.Points.Add(point);

                point = new SeriesPoint(mdataTable.Rows[i]["图层"].ToString(),
                    Convert.ToDouble(mdataTable.Rows[i]["三通管点数目"].ToString()));
                series03.Points.Add(point);

            }
            series_list.Add(series02);
            series_list.Add(series03);
            return series_list;
        }



    }
}