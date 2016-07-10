using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;

namespace PipeLine.Class
{
    /// <summary>
    /// 流向分析；日期：2016-6-27；冯志伟
    /// </summary>
    class FlowDirectionAnalysis
    {
        //获取几何网络
        public static IGeometricNetwork GetGeometricNetwork(ILayer layer)
        {

            IFeatureLayer pFeatureLayer = layer as IFeatureLayer;
            IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
            //IFeatureClass featureClass = ((IFeatureLayer)layer).FeatureClass;
            IFeatureDataset featureDataset = pFeatureClass.FeatureDataset;
            INetworkCollection networkCollection = featureDataset as INetworkCollection;
            IGeometricNetwork geometricNetwork = networkCollection.get_GeometricNetwork(0);
            return geometricNetwork;
        }
        public void ShowFlowDirection(IMap map,string layerName)
        {
            IGeometricNetwork geometricNetwork = GetGeometricNetwork(map.get_Layer(0));

            IFeatureClass featureClass = ((IFeatureLayer)map.get_Layer(0)).FeatureClass;
            IFeatureDataset featureDataset = featureClass.FeatureDataset;
            IWorkspace workspace = featureDataset.Workspace;
            IWorkspaceEdit workspaceEdit = workspace as IWorkspaceEdit;
            workspaceEdit.StartEditing(false);
            workspaceEdit.StartEditOperation();

            ILayer netLayer = null;
            for (int i = 0; i < map.LayerCount; i++)
            {
                if (map.get_Layer(i).Name.ToString() == layerName)
                {
                    netLayer = map.get_Layer(i);
                    break;
                }
            }

            if (netLayer != null)
            {
                IFeatureClass netFeatureClass = ((IFeatureLayer)netLayer).FeatureClass;
                IFeatureCursor featureCursor = netFeatureClass.Search(null, true);//设置全局查询游标
                IFeature edgeFeature = featureCursor.NextFeature();
                while (edgeFeature != null)
                {
                    //得到每条边的流向
                    INetwork network = geometricNetwork.Network;
                    IUtilityNetworkGEN utilityNetworkGEN = network as IUtilityNetworkGEN;

                    int edgeID = GetFeatureDID(edgeFeature, network);
                    esriFlowDirection edgeFlowDirection = utilityNetworkGEN.GetFlowDirection(edgeID);
                    DrawSymbol2FlowDirection(edgeFeature, edgeFlowDirection, map);
                    //获取下一个元素
                    edgeFeature = featureCursor.NextFeature();
                }
                workspaceEdit.StopEditOperation();
                workspaceEdit.StopEditing(true);
                ((IActiveView)map).Refresh();//刷新地图
            }
        }
        /// <summary>
        /// 得到要素的EID
        /// </summary>
        /// <param name="feature"></param>
        /// <param name="network"></param>
        /// <returns></returns>
        private static int GetFeatureDID(IFeature feature, INetwork network)
        {
            INetElements netElements = network as INetElements;
            int eID = 0;
            esriElementType elementType = esriElementType.esriETNone;
            switch (feature.FeatureType)
            {
                case esriFeatureType.esriFTSimpleEdge:
                case esriFeatureType.esriFTComplexEdge:
                    elementType = esriElementType.esriETEdge;
                    break;
                case esriFeatureType.esriFTSimpleJunction:
                case esriFeatureType.esriFTComplexJunction:
                    elementType = esriElementType.esriETJunction;
                    break;
            }
            eID = netElements.GetEID(feature.Class.ObjectClassID, feature.OID, -1, elementType);
            return eID;
        }

        /// <summary>
        /// 给边线要素添加流向标志
        /// </summary>
        /// <param name="edgeFeature"></param>
        /// <param name="edgeFlowDirection"></param>
        /// <param name="map"></param>
        private static void DrawSymbol2FlowDirection(IFeature edgeFeature, esriFlowDirection edgeFlowDirection, IMap map)
        {
            //IEIDInfo ipEIDInfo;
            IPolyline polyline = edgeFeature.Shape as IPolyline;
            //找到线段的中点
            IPoint midPoint = new PointClass();
            polyline.QueryPoint(esriSegmentExtension.esriNoExtension, polyline.Length / 2, false, midPoint);
            IArrowMarkerSymbol arrowMarkerSymbol = new ArrowMarkerSymbolClass();
            IMarkerElement markerElement = new MarkerElementClass();
            IElement element;
            IMarkerSymbol markerSymbol;
            switch (edgeFlowDirection)
            {
                //存在正向流向(数字化方向)
                case esriFlowDirection.esriFDWithFlow:
                    arrowMarkerSymbol.Color = getColor(255, 0, 0);
                    arrowMarkerSymbol.Size = 10;
                    arrowMarkerSymbol.Style = esriArrowMarkerStyle.esriAMSPlain;
                    arrowMarkerSymbol.Angle = GetDirectionAngle(polyline.FromPoint, polyline.ToPoint);
                    markerElement.Symbol = arrowMarkerSymbol;
                    element = markerElement as IElement;
                    element.Geometry = midPoint;
                    ((IGraphicsContainer)map).AddElement(element, 0);
                    break;
                //逆向流向
                case esriFlowDirection.esriFDAgainstFlow:
                    //edgeFlowDirection = esriFlowDirection.esriFDWithFlow;
                    arrowMarkerSymbol.Color = getColor(255, 0, 0);
                    arrowMarkerSymbol.Size = 10;
                    arrowMarkerSymbol.Style = esriArrowMarkerStyle.esriAMSPlain;
                    arrowMarkerSymbol.Angle = GetDirectionAngle(polyline.ToPoint, polyline.FromPoint);
                    markerElement.Symbol = arrowMarkerSymbol;
                    element = markerElement as IElement;
                    element.Geometry = midPoint;
                    ((IGraphicsContainer)map).AddElement(element, 0);
                    break;
                //不确定流
                case esriFlowDirection.esriFDIndeterminate:
                    markerSymbol = new SimpleMarkerSymbolClass();
                    markerSymbol.Color = getColor(255, 0, 0);
                    markerSymbol.Size = 10;
                    markerElement.Symbol = markerSymbol;
                    element = markerElement as IElement;
                    element.Geometry = midPoint;
                    ((IGraphicsContainer)map).AddElement(element, 0);
                    break;
                //未实例化的流
                case esriFlowDirection.esriFDUninitialized:
                    markerSymbol = new SimpleMarkerSymbolClass();
                    markerSymbol.Color = getColor(255, 0, 0);
                    markerSymbol.Size = 10;
                    markerElement.Symbol = markerSymbol;
                    element = markerElement as IElement;
                    element.Geometry = midPoint;
                    ((IGraphicsContainer)map).AddElement(element, 0);
                    break;
            }
        }
        /// <summary>
        /// 三原色获取颜色
        /// </summary>
        /// <param name="R"></param>
        /// <param name="G"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static IRgbColor getColor(int R, int G, int B)
        {
            IRgbColor color = new RgbColorClass();
            color.Red = R;
            color.Green = G;
            color.Blue = B;
            return color;
        }
        /// <summary>
        /// 通过线段的起点和终点来确定线段的流向方向
        /// 我的理解是流向和数字化方向挂钩，所以就有正向数字化和逆向数字化之分
        /// 所以在求线段流向方向时，根据实际情况来调用线段的起点和终点作为流向的起点和终点
        /// </summary>
        /// <param name="startPoint">流向起点</param>
        /// <param name="endPoint">流向终点</param>
        /// <returns></returns>
        public static double GetDirectionAngle(IPoint startPoint, IPoint endPoint)
        {
            //弧度
            double radian;
            //角度
            double angle = 0;

            if (startPoint.X == endPoint.X)
            {
                if (startPoint.Y > endPoint.Y)
                    angle = 270;
                else if (startPoint.Y < endPoint.Y)
                    angle = 90;
            }
            else if (startPoint.X > endPoint.X)
            {
                if (startPoint.Y == endPoint.Y)
                    angle = 180;
                else if (startPoint.Y > endPoint.Y)
                {
                    radian = Math.Atan((startPoint.Y - endPoint.Y) / (startPoint.X - endPoint.X));
                    angle = radian * (180 / Math.PI) + 180;
                }
                else if (startPoint.Y < endPoint.Y)
                {
                    radian = Math.Atan((startPoint.X - endPoint.X) / (endPoint.Y - startPoint.Y));
                    angle = radian * (180 / Math.PI) + 90;
                }
            }
            else if (startPoint.X < endPoint.X)
            {
                if (startPoint.Y == endPoint.Y)
                    angle = 0;
                else if (startPoint.Y < endPoint.Y)
                {
                    radian = Math.Atan((endPoint.Y - startPoint.Y) / (endPoint.X - startPoint.X));
                    angle = radian * (180 / Math.PI);
                }
                else if (startPoint.Y > endPoint.Y)
                {
                    radian = Math.Atan((startPoint.Y - endPoint.Y) / (endPoint.X - startPoint.X));
                    angle = 360 - (radian * (180 / Math.PI));
                }
            }
            return angle;
        }
    }
}
