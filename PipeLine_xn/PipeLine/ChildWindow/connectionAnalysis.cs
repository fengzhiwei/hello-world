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
    public partial class connectionAnalysis : DevExpress.XtraEditors.XtraForm
    {
        private AxMapControl m_axMapControl;
        List<string> layerName = new List<string>();
        List<IGeometry> lGeometry = new List<IGeometry>();
        List<IFeature> lFeature = new List<IFeature>();
        List<ILayer> lLayer = new List<ILayer>();

        ITopologicalOperator pTopo;
        IGeometry pGeometry;
        IFeature pFeature;
        IFeatureLayer pFeatureLayer;
        IFeatureCursor pCursor;
        ISpatialFilter pFilter;
        public connectionAnalysis(AxMapControl axMapControl)
        {
            InitializeComponent();
            m_axMapControl = axMapControl;
        }
        private void connectionAnalysis_Load(object sender, EventArgs e)
        {
            result_tb.Enabled = false;
        }

        public void result_point(IPoint point)
        {
            for (int i = 0; i < m_axMapControl.Map.LayerCount; i++)
            {
                pTopo = point as ITopologicalOperator;
                double Proportion2 = Math.Round(m_axMapControl.MapScale, 0);
                double m_Radius = Proportion2 / 1428.0;
                pGeometry = pTopo.Buffer(m_Radius); // Length 为缓冲区距离，自行设置
                lGeometry.Add(pGeometry);
                m_axMapControl.Map.SelectByShape(pGeometry, null, true);
                m_axMapControl.Refresh(esriViewDrawPhase.esriViewGeoSelection, null, null); //选中要素高亮显示
                pFilter = new SpatialFilterClass();
                //pFilter.GeometryField = "shape";
                pFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                pFilter.Geometry = pGeometry;
                pFeatureLayer = m_axMapControl.Map.get_Layer(i) as IFeatureLayer; // 将第3 个图层作为目标图层
                pCursor = pFeatureLayer.Search(pFilter, false);
                pFeature = pCursor.NextFeature();
                if (pFeature != null)
                {
                    lLayer.Add(m_axMapControl.Map.get_Layer(i));
                    layerName.Add(pFeatureLayer.Name.ToString());
                    lFeature.Add(pFeature);
                    if (layerName.Count == 2)
                    {
                        if (layerName[0] == layerName[1])
                        {
                            result_tb.Text = "位于同一图层：" + layerName[0];
                            string startPoint, endPoint;
                            IFeatureLayer m_FeatureLayer;
                            m_FeatureLayer = lLayer[0] as IFeatureLayer;
                            IFeature SearchFeature;
                            startPoint = lFeature[0].get_Value(16).ToString();
                            endPoint = lFeature[0].get_Value(17).ToString();
                            IFeatureCursor m_FeatureCursor;
                            IQueryFilter m_QueryFilter = new QueryFilterClass();
                            m_QueryFilter.WhereClause = "startpoint=" + "'" + endPoint + "'";
                            m_FeatureCursor = m_FeatureLayer.Search(m_QueryFilter, true);
                            SearchFeature = m_FeatureCursor.NextFeature();
                            if (SearchFeature != null)
                            {
                                MessageBox.Show("取到下一个要素");
                            }
                            
                        }
                        else
                        {
                            result_tb.Text = "两点不连通";
                        }
                        m_axMapControl.ActiveView.Refresh();
                    }
                    break;
                }
                else
                {
                    continue;
                }
            }
        }
        

        private void reset_bt_Click(object sender, EventArgs e)
        {
            layerName.Clear();
            lFeature.Clear();
            lLayer.Clear();
            result_tb.Text = null;
            m_axMapControl.Map.ClearSelection();
            m_axMapControl.ActiveView.Refresh();
        }

    }
}