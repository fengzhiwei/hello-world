using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;

namespace PipeLine.Class
{
    class PointSelection
    {
        //private AxMapControl m_axMapControl;
        ITopologicalOperator pTopo;
        IGeometry pGeometry;
        IFeature pFeature;
        IFeatureLayer pFeatureLayer;
        IFeatureCursor pCursor;
        ISpatialFilter pFilter;
        public string result_point(IPoint point, AxMapControl m_axMapControl)
        {
            string Name = null;
            for (int i = 0; i < m_axMapControl.Map.LayerCount; i++)
            {
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
                if(pFeature != null & pFeature.Shape.GeometryType ==  ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint)
                {
                    Name = pFeatureLayer.Name.ToString();
                    break;
                }
                
            }
            return Name;
        }



    }
}
