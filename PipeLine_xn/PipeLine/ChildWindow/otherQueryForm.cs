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
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;

namespace PipeLine.ChildWindow
{
    public partial class otherQueryForm : DevExpress.XtraEditors.XtraForm
    {
        private AxMapControl m_axMapControl;
        //private AxMapControl m_axMapControl;
        ITopologicalOperator pTopo;
        IGeometry pGeometry;
        IFeature pFeature;
        IFeatureLayer pFeatureLayer;
        IFeatureCursor pCursor;
        ISpatialFilter pFilter;
        DataTable dataTable;
        public otherQueryForm(AxMapControl axMapControl)
        {
            InitializeComponent();
            m_axMapControl = axMapControl;
        }
        public void result_point(IPoint point)
        {
            for (int i = 0; i < m_axMapControl.Map.LayerCount; i++)
            {
                //pPoint = new PointClass();
                //pPoint.PutCoords(e.mapX, e.mapY);
                pTopo = point as ITopologicalOperator;
                double Proportion2 = Math.Round(m_axMapControl.MapScale, 0);
                double m_Radius = Proportion2 / 1428.0;
                pGeometry = pTopo.Buffer(m_Radius); // Length 为缓冲区距离，自行设置
                m_axMapControl.Map.SelectByShape(pGeometry, null, true);
                m_axMapControl.Refresh(esriViewDrawPhase.esriViewGeoSelection, null, null); //选中要素高亮显示
                pFilter = new SpatialFilterClass();
                //pFilter.GeometryField = "shape";
                pFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                pFilter.Geometry = pGeometry;
                pFeatureLayer = m_axMapControl.Map.get_Layer(i) as IFeatureLayer; // 将第3 个图层作为目标图层
                pCursor = pFeatureLayer.Search(pFilter, false);
                pFeature = pCursor.NextFeature();
                string fieldName;
                if (pFeature != null)
                //while (pFeature != null)
                {
                    dataTable = new DataTable();
                    for (int k = 0; k < 2; k++)
                    {
                        if (k == 0)
                        {
                            dataTable.Columns.Add("属性");
                        }
                        if (k == 1)
                        {
                            dataTable.Columns.Add("值");
                        }
                    }
                    DataRow datarow;
                    for (int j = 0; j < pFeature.Fields.FieldCount; j++)
                    {
                        datarow = dataTable.NewRow();
                        for (int m = 0; m < 2; m++)
                        {
                            if (m == 0)
                            {
                                fieldName = pFeature.Fields.get_Field(j).Name;
                                datarow[m] = fieldName;
                            }
                            if (m == 1)
                            {
                                if (pFeature.Fields.get_Field(j).Name == "Shape")
                                {
                                    if (pFeature.Shape.GeometryType == ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint)
                                    {
                                        datarow[m] = "点";
                                    }
                                    if (pFeature.Shape.GeometryType == ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline)
                                    {
                                        datarow[m] = "线";
                                    }
                                    if (pFeature.Shape.GeometryType == ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon)
                                    {
                                        datarow[m] = "面";
                                    }
                                }
                                else
                                {
                                    datarow[m] = pFeature.get_Value(j).ToString();
                                }
                            }
                        }
                        dataTable.Rows.Add(datarow);
                    }
                    this.dataGridView1.DataSource = dataTable;
                    this.gridView1.BestFitColumns();
                    this.layerName_dev.Text = pFeatureLayer.Name;
                    //otherqueryform.layerName_dev.Properties.ReadOnly = true;
                    this.dataGridView1.Refresh();
                    pFeature = null;
                    break;
                }
                //
            }
        }
        private void labelControl1_Click(object sender, EventArgs e)
        {
            //
        }

        private void otherQueryForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            main.choice = 0;
            m_axMapControl.Map.ClearSelection();
            m_axMapControl.ActiveView.Refresh();
            m_axMapControl.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            if (e.Info.IsRowIndicator)
            {
                if (e.RowHandle >= 0)
                {
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();
                }
                else if (e.RowHandle < 0 && e.RowHandle > -1000)
                {
                    e.Info.Appearance.BackColor = System.Drawing.Color.AntiqueWhite;
                    e.Info.DisplayText = "G" + e.RowHandle.ToString();
                }
            }
        }


    }
}