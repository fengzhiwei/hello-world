using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;

namespace PipeLine.Class
{
    /// <summary>
    /// 获取消防栓;2016-7-6
    /// </summary>
    class GetFireHydrant
    {
        public DataTable result(AxMapControl m_MapControl, IGeometry geometry)
        {
            m_MapControl.Map.ClearSelection();
            IFeatureCursor pFeatureCursor;
            ILayer layer = null;
            DataTable dataTable = new DataTable();
            //lFeature.Clear();
            //lLayer.Clear();
            for (int i = 0; i < m_MapControl.Map.LayerCount; i++)
            {
                if (m_MapControl.Map.get_Layer(i).Name.ToString() == "消火栓井")
                {
                    layer = m_MapControl.Map.get_Layer(i);
                    if (layer.Visible)
                    {
                        IFeatureLayer pFeatureLayer = layer as IFeatureLayer;
                        IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
                        ISpatialFilter pSpatialFilter = new SpatialFilterClass();
                        pSpatialFilter.Geometry = geometry;
                        pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;//relationship
                        pFeatureCursor = pFeatureClass.Search(pSpatialFilter, false);
                        IFeature pFeature = pFeatureCursor.NextFeature();
                        DataColumn dataColumn = new DataColumn();
                        if (pFeature == null)
                        {
                            continue;
                        }
                        if (pFeature != null)
                        {
                            while (dataTable.Columns.Count == 0)
                            {
                                for (int k = 0; k <= pFeature.Fields.FieldCount; k++)
                                {
                                    if (k == 0)
                                    {
                                        dataTable.Columns.Add("图层名称");
                                    }
                                    if (k > 0 && k <= pFeature.Fields.FieldCount)
                                    {
                                        dataColumn = new DataColumn(pFeatureClass.Fields.get_Field(k - 1).Name);
                                        dataTable.Columns.Add(dataColumn);
                                    }
                                }
                            }
                        }
                        while (pFeature != null)
                        {
                            //lFeature.Add(pFeature);
                            //lLayer.Add(pFeatureLayer);
                            DataRow dataRow;
                            dataRow = dataTable.NewRow();
                            m_MapControl.Map.SelectFeature(layer, pFeature);//选中要素
                            for (int j = 0; j <= pFeature.Fields.FieldCount; j++)
                            {
                                if (j == 0)
                                {
                                    dataRow[j] = pFeatureLayer.Name;
                                }
                                else
                                {
                                    //System.__ComObject
                                    if (pFeature.get_Value(j - 1).ToString() == "System.__ComObject")
                                    {
                                        dataRow[j] = "点";
                                    }
                                    else
                                    {
                                        dataRow[j] = pFeature.get_Value(j - 1).ToString();
                                    }
                                }
                            }
                            dataTable.Rows.Add(dataRow);//Datagridview
                            pFeature = pFeatureCursor.NextFeature();
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                
                
            }//for循环结束
            m_MapControl.Refresh();
            return dataTable;
        }


        public List<IFeature> getFeatures(AxMapControl m_MapControl, IGeometry geometry)
        {
            //m_MapControl.Map.ClearSelection();
            IFeatureCursor pFeatureCursor;
            ILayer layer = null;
            DataTable dataTable = new DataTable();
            List<IFeature> lFeature = new List<IFeature>();
            for (int i = 0; i < m_MapControl.Map.LayerCount; i++)
            {
                if (m_MapControl.Map.get_Layer(i).Name.ToString() == "消火栓井")
                {
                    layer = m_MapControl.Map.get_Layer(i);
                    if (layer.Visible)
                    {
                        IFeatureLayer pFeatureLayer = layer as IFeatureLayer;
                        IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
                        ISpatialFilter pSpatialFilter = new SpatialFilterClass();
                        pSpatialFilter.Geometry = geometry;
                        pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;//relationship
                        pFeatureCursor = pFeatureClass.Search(pSpatialFilter, false);
                        IFeature pFeature = pFeatureCursor.NextFeature();
                        while (pFeature != null)
                        {
                            lFeature.Add(pFeature);
                            pFeature = pFeatureCursor.NextFeature();
                        }
                    }
                    else
                    {
                        continue;
                    }
                }


            }//for循环结束
            //m_MapControl.Refresh();
            return lFeature;
        }

        public IFeature GetNearFireHydrant(ILayer layer,IPoint firePoint)
        {
            IFeature feature_fire;
            List<IPoint> lIPoint = new List<IPoint>();
            List<double> Distance_fire = new List<double>();
            List<IFeature> lFeature = new List<IFeature>();
            IFeatureLayer pFeatureLayer = layer as IFeatureLayer;
            IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
            IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, false);
            IFeature pFeature = pFeatureCursor.NextFeature();
            while (pFeature != null)
            {
                IPoint point = (IPoint)pFeature.Shape;
                lIPoint.Add(point);
                lFeature.Add(pFeature);
                pFeature = pFeatureCursor.NextFeature();
            }
            for (int i = 0; i < lIPoint.Count; i++)
            {
                Distance_fire.Add(Distance(firePoint,lIPoint[i]));
            }
            double min = Distance_fire[0];
            int test = 0;
            for (int i = 0; i < lIPoint.Count; i++)
            {
                if (min > Distance_fire[i])
                {
                    min = Distance_fire[i];
                    test = i;
                }
            }
            feature_fire = lFeature[test];
            return feature_fire;
        }

        public double Distance(IPoint point1,IPoint point2)
        {
            double distance = 0;
            double X = (point1.X - point2.X) * (point1.X - point2.X) + (point1.Y - point2.Y) * (point1.Y - point2.Y);
            distance = Math.Pow(X, 0.5);
            return distance;
        }
    }
}
