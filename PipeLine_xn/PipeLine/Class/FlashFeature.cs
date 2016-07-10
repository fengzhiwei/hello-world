using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;

namespace PipeLine.Class
{
    /// <summary>
    /// 同事闪烁多个要素
    /// </summary>
    class FlashFeature
    {
        /// <summary>
        /// 闪烁
        /// </summary>
        /// <param name="where">查询的字段</param>
        /// <param name="m_axMapControl">操作的对象</param>
        public void FilterLayer(string where, AxMapControl m_axMapControl)
        {
            IFeatureLayer flyr = (IFeatureLayer)m_axMapControl.get_Layer(0); //高亮第一个图层
            IFeatureClass fcls = flyr.FeatureClass;

            IQueryFilter queryFilter = new QueryFilterClass();
            queryFilter.WhereClause = where;

            // 缩放到选择结果集，并高亮显示 
            ZoomToSelectedFeature(flyr, queryFilter, m_axMapControl);

            //闪烁选中得图斑 
            IFeatureCursor featureCursor = fcls.Search(queryFilter, true);
            FlashPolygons(featureCursor, m_axMapControl);
        }
        /// <summary>
        /// 缩放到图层
        /// </summary>
        /// <param name="pFeatureLyr">要素图层</param>
        /// <param name="pQueryFilter">过滤条件</param>
        /// <param name="m_axMapControl"></param>
        private void ZoomToSelectedFeature(IFeatureLayer pFeatureLyr, IQueryFilter pQueryFilter, AxMapControl m_axMapControl)
        {
            #region 高亮显示查询到的要素集合

            //符号边线颜色 
            IRgbColor pLineColor = new RgbColor();
            pLineColor.Red = 255;
            ILineSymbol ilSymbl = new SimpleLineSymbolClass();
            ilSymbl.Color = pLineColor;
            ilSymbl.Width = 5;

            //定义选中要素的符号为红色 
            ISimpleFillSymbol ipSimpleFillSymbol = new SimpleFillSymbol();
            ipSimpleFillSymbol.Outline = ilSymbl;
            RgbColor pFillColor = new RgbColor();
            pFillColor.Green = 60;
            ipSimpleFillSymbol.Color = pFillColor;
            ipSimpleFillSymbol.Style = esriSimpleFillStyle.esriSFSForwardDiagonal;

            //选取要素集 
            IFeatureSelection pFtSelection = pFeatureLyr as IFeatureSelection;
            pFtSelection.SetSelectionSymbol = true;
            pFtSelection.SelectionSymbol = (ISymbol)ipSimpleFillSymbol;
            pFtSelection.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, false);

            #endregion

            ISelectionSet pSelectionSet = pFtSelection.SelectionSet;
            //居中显示选中要素 
            IEnumGeometry pEnumGeom = new EnumFeatureGeometry();
            IEnumGeometryBind pEnumGeomBind = pEnumGeom as IEnumGeometryBind;
            pEnumGeomBind.BindGeometrySource(null, pSelectionSet);
            IGeometryFactory pGeomFactory = new GeometryEnvironmentClass();
            IGeometry pGeom = pGeomFactory.CreateGeometryFromEnumerator(pEnumGeom);

            m_axMapControl.ActiveView.Extent = pGeom.Envelope;
            m_axMapControl.ActiveView.Refresh();
        }
        /// <summary>
        /// 闪烁代码
        /// </summary>
        /// <param name="featureCursor">要素游标</param>
        /// <param name="m_axMapControl"></param>
        private void FlashPolygons(IFeatureCursor featureCursor, AxMapControl m_axMapControl)
        {
            IArray geoArray = new ArrayClass();
            IFeature feature = null;
            while ((feature = featureCursor.NextFeature()) != null)
            {
                //feature是循环外指针，所以必须用ShapeCopy 
                geoArray.Add(feature.ShapeCopy);
            }

            //通过IHookActions闪烁要素集合 
            HookHelperClass m_pHookHelper = new HookHelperClass();
            m_pHookHelper.Hook = m_axMapControl.Object;
            IHookActions hookActions = (IHookActions)m_pHookHelper;

            hookActions.DoActionOnMultiple(geoArray, esriHookActions.esriHookActionsPan);
            Application.DoEvents();//处理所有的当前在消息队列中的Windows消息
            m_pHookHelper.ActiveView.ScreenDisplay.UpdateWindow();
            hookActions.DoActionOnMultiple(geoArray, esriHookActions.esriHookActionsFlash);
        } 
    }
}
