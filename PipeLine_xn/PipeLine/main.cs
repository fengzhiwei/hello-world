using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Output;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.GlobeCore;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geodatabase;
using System.Windows;
using PipeLine.BaseComm;
using PipeLine.ChildWindow;
using System.Threading;
using stdole;
using PipeLine;
using PipeLine.Helper;
using PipeLine.Class;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using ESRI.ArcGIS.DataSourcesGDB;
using DevExpress.XtraBars.Helpers;
using ESRI.ArcGIS.NetworkAnalysis;
namespace PipeLine
{
    public partial class main : DevExpress.XtraBars.Ribbon.RibbonForm  //test 哈哈赵得意bghgbhb123
    {
        public static int choice = 0;
        public static double Fire_Condition_radius = 0;
        public static int number = 0;
        int flag = 0;
        public static int scene = 0;
        int my_number;
        List<string> Namelist = new List<string>();
        MeasureLine ML = new MeasureLine();
        //public static MeasureLine ML;
        MeasureArea MA = new MeasureArea();
        //TOCControl右键
        public ITOCControl2 m_TocControl; //主窗体定义一个
        public IToolbarMenu m_ToolMenuLayer;
        public ILayer layer;
        IArray m_Array;
        IFeature m_Feature;
        PolygonQuery PQ;
        property_3D PP_3D;
        BufferRadius BR;
        ConnectionSDE connectionSDE;
        SearchMapByAttribution SMBA; //定义属性查图窗体
        PipePointQuery PPQ; //定义管点查询窗体
        otherQueryForm otherqueryform;
        IGraphicsContainer m_GraphicsContainer;
        //_3DExcavation _3Dexcavation;
        Excavation3D excavation3D;
        LineAnalysis lineAnalysis;
        PointNumberQuery pointNumberQuery;
        PipeLength pipeLength;
        PipeLineStatistic pipeLineStatistic;
        RectangleSelection rectangleSelection;
        MinRoad minRoad;
        FireConditon fireContion;
        FireAnalysis fireAnalysis;
        //BurstPoint burstPoint;
        BurstPointAnalysis burstPointAnalysis;
        TempLayer tempLayer;
        connectionAnalysis connectionanalysis;
        PipeLine.ChildWindow.FieldType fieldType;
        int mm = 0;
        IGraphicsContainer3D mIGraphicsContainer3D;
        List<IPoint> my_point = new List<IPoint>();
        private IPointCollection pointCollection = null;
        public IPointCollection polygonCollection;
        public SectionAnalysis sectionAnalysis;
        public IEnvelope eve;

        IPoint target; //相机目标点
        IPoint observer; //观察点

        private IActiveView m_ipActiveView;
        private IMap m_ipMap;
        private IGraphicsContainer pGC;
        private bool clicked = false;
        int clickedcount = 0;
        private double m_dblPathCost = 0;
        private IGeometricNetwork m_ipGeometricNetwork;
        private IPointCollection m_ipPoints; //
        private IPointToEID m_ipPointToEID;
        private IEnumNetEID m_ipEnumNetEID_Junctions;
        private IEnumNetEID m_ipEnumNetEID_Edges;
        private IPolyline m_ipPolyline;
        private IMapControl3 mapctrlMainMap = null;
        private ILayer mLayer;
        List<ILayer> lLayer = new List<ILayer>();
        public List<int> index_Feature = new List<int>();
        public List<IFeature> lFeature = new List<IFeature>();
        public List<IGeometry> lGeometry = new List<IGeometry>();
        public List<IPoint> lPoint = new List<IPoint>();
        private string LayerName;
        public main()
        {
            InitializeComponent();
            bar1.Visible = false;
            timer1.Enabled = true;
            InitSkinGallery();
            splashScreenManager1.ShowWaitForm();
            System.Threading.Thread.Sleep(1000);
            splashScreenManager1.CloseWaitForm();
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.axSceneControl2_wheel);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.axSceneControl1_wheel);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.axMapControl1_wheel);

            ribbonControl1.Manager.UseF10KeyForMenu = false;
            ribbonControl1.Manager.UseAltKeyForMenu = false;
            ribbonControl1.Manager.AllowShowToolbarsPopup = false;
        }
        void InitSkinGallery()
        {
            SkinHelper.InitSkinGallery(GalleryBarItem_SkinSet, true);
            SkinHelper.InitSkinGallery(GalleryBarItem_SkinSet, true);
        }

        private void main_Load(object sender, EventArgs e)
        {
            //获得相对路径,
            //string mapPath = Application.StartupPath + @"\XNTG\XNTG.mxd";  //@"\test02.gdb\XXX.mxd";
            // mapPath = Application.StartupPath + @"\2016-03-22\XNGX.mxd";
            string mapPath = @"F:\钢企\Geometry_Net\geo_Network.mxd";
            axMapControl1.LoadMxFile(mapPath);
            string mapPath2 = Application.StartupPath + @"\2016-03-14\最新数据\西宁特钢模型.sxd";
            axSceneControl1.LoadSxFile(mapPath2);
            string mapPath3 = Application.StartupPath + @"\最新数据\管线模型.sxd";
            axSceneControl2.LoadSxFile(mapPath3);

            axTOCControl1.SetBuddyControl(axMapControl1);
            axTOCControl2.SetBuddyControl(axSceneControl1);
            axTOCControl3.SetBuddyControl(axSceneControl2);

            m_TocControl = (ITOCControl2)axTOCControl1.Object;//绑定数据
            m_ToolMenuLayer = new ToolbarMenuClass();
            m_ToolMenuLayer.SetHook(axMapControl1);
            axToolbarControl1.Hide();
            axToolbarControl2.Hide();
            //listBox1.Hide();
            eve = axMapControl1.Extent;
            target = axSceneControl1.Camera.Target;
            observer = axSceneControl1.Camera.Observer;


            mapctrlMainMap = (IMapControl3)this.axMapControl1.Object;
            m_ipActiveView = axMapControl1.ActiveView;
            m_ipMap = m_ipActiveView.FocusMap;
            clicked = false;
            pGC = m_ipMap as IGraphicsContainer;


        }


        //与其他窗体进行交互
        public IPointCollection getMainPointCollection()
        {
            return pointCollection;
        }
        public AxMapControl getMainAxMapControl()
        {
            return axMapControl1;
        }
        private void axMapControl1_OnMouseUp(object sender, IMapControlEvents2_OnMouseUpEvent e)
        {
            ///
        }
        //调用图层字段属性
        private void PropertiesText(IGeometry pGeometry, string properties)
        {
            IRgbColor color = new RgbColorClass();
            color.Red = 255;
            color.Green = 0;
            color.Blue = 0;
            ITextSymbol txtSystem = new TextSymbolClass();
            txtSystem.Color = color;
            object symbol = txtSystem;
            axMapControl1.DrawText(pGeometry, properties, ref symbol);

        }
        //添加鹰眼
        private void axMapControl1_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {
            IMap pMap;
            pMap = axMapControl1.Map;
            for (int i = 0; i <= axMapControl1.LayerCount - 1; i++)
            {
                axMapControl2.AddLayer(axMapControl1.get_Layer(i));
            }
            axMapControl2.Refresh();
            axMapControl2.Extent = axMapControl2.FullExtent;
        }
        /// <summary>
        /// 鹰眼得到新范围
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void axMapControl1_OnExtentUpdated(object sender, IMapControlEvents2_OnExtentUpdatedEvent e)
        {
            IEnvelope pEnv;
            pEnv = e.newEnvelope as IEnvelope;
            IGraphicsContainer pGraphicsContainer;
            IActiveView pActiveView;
            pGraphicsContainer = axMapControl2.Map as IGraphicsContainer;
            pActiveView = pGraphicsContainer as IActiveView;
            pGraphicsContainer.DeleteAllElements();//绘制前清楚map中所有的图形元素
            IElement pEle = new RectangleElementClass();
            pEle.Geometry = pEnv;
            IRgbColor pColor;
            pColor = new RgbColorClass();
            pColor.RGB = 255;
            pColor.Transparency = 255;
            ILineSymbol OutlineSymbol = new SimpleLineSymbolClass();
            OutlineSymbol.Width = 2;
            OutlineSymbol.Color = pColor;

            pColor = new RgbColorClass();
            pColor.Transparency = 0;
            IFillSymbol fillSymbol = new SimpleFillSymbolClass();
            fillSymbol.Color = pColor;

            fillSymbol.Outline = OutlineSymbol;
            IFillShapeElement pFillShapeElement = pEle as IFillShapeElement;
            pFillShapeElement.Symbol = fillSymbol;
            pEle = pFillShapeElement as IElement;
            pGraphicsContainer.AddElement(pEle, 0);
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);

        }
        private void axMapControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent e)
        {
            //清除其他信息和选择信息
            //listBox1.Hide();

            double mapScale = axMapControl1.MapScale;
            axMapControl1.Refresh();
            IGraphicsContainer pGrapgicsContainer;
            IMap pMap = axMapControl1.Map;
            pGrapgicsContainer = pMap as IGraphicsContainer;
            pGrapgicsContainer.DeleteAllElements();
            if (e.button == 1)
            {
                switch (choice)
                {
                    //基本操作
                    case 1:
                        {
                            IEnvelope toolEnvelope;
                            switch (flag)
                            {
                                case 1:
                                    toolEnvelope = axMapControl1.TrackRectangle();
                                    axMapControl1.Extent = toolEnvelope;
                                    break;
                                case 2:
                                    toolEnvelope = axMapControl1.TrackRectangle();
                                    toolEnvelope = axMapControl1.Extent;
                                    toolEnvelope.Expand(2, 2, true);
                                    axMapControl1.Extent = toolEnvelope;
                                    break;
                                case 3:
                                    toolEnvelope = axMapControl1.Extent;
                                    axMapControl1.Pan();
                                    break;
                            }
                        }
                        break;
                    case 2:
                        IPolyline measureLinePolyline = null;
                        measureLinePolyline = (IPolyline)axMapControl1.TrackLine();

                        //screenLineLength = Math.Abs(Math.Round(measureLinePolyline.Length / 100 * mapScale, 2));
                        //MessageBox.Show(measureLinePolyline.Length.ToString());
                        double faultDistance = Math.Abs(Math.Round(measureLinePolyline.Length, 4));
                        ML.Distance = faultDistance.ToString() + "米";
                        Length length = new Length();
                        length.DrawLine(axMapControl1, measureLinePolyline);
                        break;
                    case 3:
                        IPolygon measureAreaPolygon = null;
                        measureAreaPolygon = (IPolygon)axMapControl1.TrackPolygon();
                        IArea area = (IArea)measureAreaPolygon;
                        double faultArea = Math.Abs(Math.Round(area.Area, 4));
                        MA.PolygonArea = faultArea.ToString() + "平方米";

                        IActiveView pActive3 = axMapControl1.Map as IActiveView;
                        ILineSymbol OutlineSymbol3 = new SimpleLineSymbolClass();
                        OutlineSymbol3.Width = 3;
                        OutlineSymbol3.Color = getColor(0,0,255);
                        ISimpleFillSymbol fillsymbol3 = new SimpleFillSymbolClass();
                        fillsymbol3.Style = esriSimpleFillStyle.esriSFSNull;
                        fillsymbol3.Color = getColor(0,0,255);
                        fillsymbol3.Outline = OutlineSymbol3;
                        IFillShapeElement pFillElement3 = new PolygonElementClass();
                        pFillElement3.Symbol = fillsymbol3;
                        IElement pEle3 = pFillElement3 as IElement;
                        pEle3.Geometry = measureAreaPolygon;
                        IGraphicsContainer pContainer3 = axMapControl1.Map as IGraphicsContainer;
                        pContainer3.AddElement(pEle3, 0);
                        axMapControl1.Refresh();
                        break;
                    case 4:
                        IGeometry multi_Geometry4 = null;
                        multi_Geometry4 = axMapControl1.TrackPolygon();
                        IActiveView pActive4 = axMapControl1.Map as IActiveView;
                        ILineSymbol OutlineSymbol4 = new SimpleLineSymbolClass();
                        OutlineSymbol4.Width = 3;
                        OutlineSymbol4.Color = getColor(0, 255);
                        ISimpleFillSymbol fil1symbol4 = new SimpleFillSymbolClass();
                        fil1symbol4.Style = esriSimpleFillStyle.esriSFSNull;
                        fil1symbol4.Color = getColor(0, 255);
                        fil1symbol4.Outline = OutlineSymbol4;
                        IFillShapeElement pFillElement4 = new PolygonElementClass();
                        pFillElement4.Symbol = fil1symbol4;
                        IElement pEle4 = pFillElement4 as IElement;
                        pEle4.Geometry = multi_Geometry4;
                        IGraphicsContainer pContainer4 = axMapControl1.Map as IGraphicsContainer;
                        pContainer4.AddElement(pEle4, 0);
                        //pActive4.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);

                        if (multi_Geometry4 != null)
                        {
                            if (Application.OpenForms["PolygonQuery"] == null)
                            {
                                PQ = new PolygonQuery(axMapControl1);
                                PQ.Show();
                            }
                            else
                            {
                                Application.OpenForms["PolygonQuery"].Show();
                            }
                            PQ.result(multi_Geometry4);
                            //lineAnalysis.Show();
                            if (PQ.lFeature.Count == 0)
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show("没有选中任何管线！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                lineAnalysis.Close();
                            }
                        }
                        else
                        {
                            return;
                        }
                        break;
                    case 5:
                        //点选高亮显示
                        IGraphicsContainer ppGrapgicsContainer = axMapControl1.Map as IGraphicsContainer;
                        ESRI.ArcGIS.Geometry.Point pPoint = new ESRI.ArcGIS.Geometry.Point();
                        pPoint.X = e.mapX; //在axMapControl_OnMouseDown事件中
                        pPoint.Y = e.mapY;

                        IGeometry GGeometry = pPoint as IGeometry;
                        double Proportion = Math.Round(axMapControl1.MapScale, 0);
                        double db = Proportion / 2000.0;
                        ITopologicalOperator pTop;
                        pTop = pPoint as ITopologicalOperator;
                        GGeometry = pTop.Buffer(db);
                        axMapControl1.Map.SelectByShape(GGeometry, null, true);
                        //axMapControl1.FlashShape(GGeometry);
                        axMapControl1.Refresh(esriViewDrawPhase.esriViewGeoSelection, null, null); //选中要素高亮显示
                        //string pPicturePath = Application.StartupPath + @"\pic\test.bmp";
                        //IPictureMarkerSymbol pPicMarkerSymbol = new PictureMarkerSymbolClass();
                        //pPicMarkerSymbol.CreateMarkerSymbolFromFile(esriIPictureType.esriIPictureEMF, pPicturePath);
                        //pPicMarkerSymbol.Size = 20;
                        //IMarkerElement pMarkElement;
                        //pMarkElement = new MarkerElementClass();
                        //IElement ppElement;
                        //ppElement = pMarkElement as IElement;
                        ////得到Element的接口对象，用于设置元素的Geometry
                        //ppElement.Geometry = pPoint;
                        ////把符号赋给元素
                        //pMarkElement.Symbol = pPicMarkerSymbol;

                        //ppGrapgicsContainer.AddElement(pMarkElement as IElement, 0);
                        //axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);

                        break;
                    case 6:
                        try
                        {
                            if (Application.OpenForms["otherQueryForm"] == null)
                            {
                                otherqueryform = new otherQueryForm(axMapControl1);
                                pPoint = new PointClass();
                                pPoint.PutCoords(e.mapX, e.mapY);
                                otherqueryform.result_point(pPoint);
                                otherqueryform.Show();
                            }
                            else
                            {
                                pPoint = new PointClass();
                                pPoint.PutCoords(e.mapX, e.mapY);
                                otherqueryform.result_point(pPoint);
                                Application.OpenForms["otherQueryForm"].Show();

                            }
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                        break;
                    case 7:
                        IGeometry multi_Geometry2 = null;
                        multi_Geometry2 = axMapControl1.TrackPolygon();
                        axMapControl1.Map.SelectByShape(multi_Geometry2, null, false);
                        axMapControl1.Refresh();
                        break;
                    case 8:
                        //覆土分析
                        try
                        {
                            ITopologicalOperator pTopo;
                            IGeometry pGeometry;
                            IFeature pFeature;
                            IFeatureLayer pFeatureLayer;
                            IFeatureCursor pCursor;
                            ISpatialFilter pFilter;
                            for (int i = 0; i < axMapControl1.Map.LayerCount; i++)
                            {
                                if (axMapControl1.Map.get_Layer(i).Visible)
                                {
                                    pFeatureLayer = axMapControl1.Map.get_Layer(i) as IFeatureLayer; // 将第3 个图层作为目标图层
                                    IFeatureLayer featurelayer = (IFeatureLayer)pFeatureLayer;
                                    IFeatureClass featureclass = featurelayer.FeatureClass;
                                    pPoint = new PointClass();
                                    pPoint.PutCoords(e.mapX, e.mapY);
                                    pTopo = pPoint as ITopologicalOperator;
                                    double Proportion8 = Math.Round(axMapControl1.MapScale, 0);
                                    double m_Radius = Proportion8 / 1428.0;
                                    pGeometry = pTopo.Buffer(m_Radius); // Length 为缓冲区距离，自行设置
                                    axMapControl1.Map.SelectByShape(pGeometry, null, true);
                                    axMapControl1.Refresh(esriViewDrawPhase.esriViewGeoSelection, null, null); //选中要素高亮显示
                                    pFilter = new SpatialFilterClass();
                                    pFilter.GeometryField = "shape";
                                    pFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                                    pFilter.Geometry = pGeometry;
                                    pCursor = pFeatureLayer.Search(pFilter, false);

                                    pFeature = pCursor.NextFeature();
                                    if (pFeature != null)
                                    {
                                        IPolyline polyline = (IPolyline)pFeature.Shape;
                                        IPoint midPoint = new PointClass();
                                        polyline.QueryPoint(esriSegmentExtension.esriNoExtension, polyline.Length / 2, false, midPoint);
                                        //IPoint pPnt = polyline.FromPoint;
                                        DrawLine_futu(axMapControl1, midPoint);
                                        AddLable(axMapControl1, pFeature, "埋深", midPoint, pFeatureLayer);
                                        break;
                                    }
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("请选择管道", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            this.axMapControl1.Map.ClearSelection();
                            axMapControl1.Refresh();
                        }
                        break;
                    case 9:
                        try
                        {
                            ITopologicalOperator pTopo;
                            IGeometry pGeometry;
                            IFeature pFeature;
                            IFeatureLayer pFeatureLayer;
                            IFeatureCursor pCursor;
                            ISpatialFilter pFilter;
                            for (int i = 0; i < axMapControl1.Map.LayerCount; i++)
                            {
                                if (axMapControl1.Map.get_Layer(i).Visible)
                                {
                                    pFeatureLayer = axMapControl1.Map.get_Layer(i) as IFeatureLayer;
                                    IFeatureLayer featurelayer = (IFeatureLayer)pFeatureLayer;
                                    IFeatureClass featureclass = featurelayer.FeatureClass;
                                    pPoint = new PointClass();
                                    pPoint.PutCoords(e.mapX, e.mapY);
                                    IPoint pt = axMapControl1.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
                                    pTopo = pPoint as ITopologicalOperator;
                                    double Proportion9 = Math.Round(axMapControl1.MapScale, 0);
                                    double m_Radius = Proportion9 / 1428.0;
                                    pGeometry = pTopo.Buffer(m_Radius); // Length 为缓冲区距离，自行设置
                                    axMapControl1.Map.SelectByShape(pGeometry, null, true);
                                    //axMapControl1.Refresh(esriViewDrawPhase.esriViewGeoSelection, null, null); //选中要素高亮显示

                                    pFilter = new SpatialFilterClass();
                                    pFilter.GeometryField = "SHAPE";
                                    pFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                                    pFilter.Geometry = pGeometry;
                                    pCursor = pFeatureLayer.Search(pFilter, false);

                                    pFeature = pCursor.NextFeature();
                                    if (pFeature != null)
                                    {
                                        //DrawLine(axMapControl1, pPoint);
                                        //AddLable2(axMapControl1, pFeature, "外径", pPoint, pFeatureLayer);
                                        int index = pFeature.Fields.FindField("外径");
                                        string text = pFeatureLayer.Name + ":外径" + pFeature.get_Value(index).ToString();
                                        AddText addtext = new AddText();
                                        ITextElement te = addtext.createTextElement(axMapControl1, e.mapX, e.mapY, text);
                                        axMapControl1.ActiveView.GraphicsContainer.AddElement(te as IElement, 1);
                                        axMapControl1.Refresh(esriViewDrawPhase.esriViewGraphics, null, null);
                                        break;
                                    }



                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show("请选择管道", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            axMapControl1.Map.ClearSelection();
                            axMapControl1.ActiveView.Refresh();
                        }
                        break;
                    case 10:
                        try
                        {
                            ITopologicalOperator pTopo;
                            IGeometry pGeometry;
                            IFeature pFeature;
                            IFeatureLayer pFeatureLayer;
                            IFeatureCursor pCursor;
                            ISpatialFilter pFilter;
                            for (int i = 0; i < axMapControl1.Map.LayerCount; i++)
                            {
                                if (axMapControl1.Map.get_Layer(i).Visible)
                                {
                                    pFeatureLayer = axMapControl1.Map.get_Layer(i) as IFeatureLayer;
                                    IFeatureLayer featurelayer = (IFeatureLayer)pFeatureLayer;
                                    IFeatureClass featureclass = featurelayer.FeatureClass;
                                    pPoint = new PointClass();
                                    pPoint.PutCoords(e.mapX, e.mapY);
                                    pTopo = pPoint as ITopologicalOperator;
                                    double Proportion10 = Math.Round(axMapControl1.MapScale, 0);
                                    double m_Radius = Proportion10 / 1428.0;
                                    pGeometry = pTopo.Buffer(m_Radius); // Length 为缓冲区距离，自行设置
                                    axMapControl1.Map.SelectByShape(pGeometry, null, true);
                                    axMapControl1.Refresh(esriViewDrawPhase.esriViewGeoSelection, null, null); //选中要素高亮显示

                                    pFilter = new SpatialFilterClass();
                                    pFilter.GeometryField = "shape";
                                    pFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                                    pFilter.Geometry = pGeometry;
                                    pCursor = pFeatureLayer.Search(pFilter, false);

                                    pFeature = pCursor.NextFeature();
                                    if (pFeature != null)
                                    {
                                        
                                        //DrawLine(axMapControl1, pPoint);
                                        //AddLable(axMapControl1, pFeature, "标高", pPoint, pFeatureLayer);
                                        int index = pFeature.Fields.FindField("材质");
                                        string text = pFeatureLayer.Name + ":" + pFeature.get_Value(index).ToString();
                                        AddText addtext = new AddText();
                                        ITextElement te = addtext.createTextElement(axMapControl1, e.mapX, e.mapY, text);
                                        axMapControl1.ActiveView.GraphicsContainer.AddElement(te as IElement, 1);
                                        axMapControl1.Refresh(esriViewDrawPhase.esriViewGraphics, null, null);
                                        break;
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show("请选择管道", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            axMapControl1.Map.ClearSelection();
                            axMapControl1.ActiveView.Refresh();
                        }
                        break;
                    case 11:
                        m_GraphicsContainer = axMapControl1.Map as IGraphicsContainer;
                        m_GraphicsContainer.DeleteAllElements();
                        IActiveView m_ActiveView = axMapControl1.Map as IActiveView;
                        ESRI.ArcGIS.Geometry.Point m_Point = new ESRI.ArcGIS.Geometry.PointClass();
                        m_Point.X = e.mapX;
                        m_Point.Y = e.mapY;
                        IGeometry b_Geometry = m_Point as IGeometry;
                        double Proportion11 = Math.Round(axMapControl1.MapScale, 0);
                        double db11 = Proportion11 / 1428.0;
                        ITopologicalOperator p_Top;
                        p_Top = m_Point as ITopologicalOperator;
                        b_Geometry = p_Top.Buffer(db11);
                        axMapControl1.Map.SelectByShape(b_Geometry, null, false);
                        axMapControl1.Refresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                        m_ActiveView.Refresh();
                        ISelection pSelection = axMapControl1.Map.FeatureSelection;
                        IEnumFeatureSetup pEnumFeatureSetup = pSelection as IEnumFeatureSetup;
                        pEnumFeatureSetup.AllFields = true;

                        //IEnumFeature m_EnumFeature = axMapControl1.Map.FeatureSelection as IEnumFeature;
                        IEnumFeature m_EnumFeature = pSelection as IEnumFeature;
                        IFeature m_Feature = m_EnumFeature.Next();
                        //m_Feature.
                        string s = BR.bufferradiustb_dev.Text;
                        double radius;

                        Numberic numberClass = new Numberic();
                        if (numberClass.isNumberic(s, out radius))
                        {
                            if (radius.ToString() == "0")
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show("半径为0，请重新输入", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            else
                            {
                                if (m_Feature != null)
                                {
                                    IGeometry s_Geometry = m_Feature.Shape;
                                    ITopologicalOperator m_TopologicalOperator = s_Geometry as ITopologicalOperator;
                                    IGeometry r_Geometry = m_TopologicalOperator.Buffer(radius);
                                    if (r_Geometry != null)
                                    {
                                        BR.gridControl1.Visible = true;
                                        BR.gridControl1.DataSource = result(axMapControl1, r_Geometry);
                                        BR.gridView1.BestFitColumns();
                                        BR.gridView1.GroupPanelText = "结果";
                                    }
                                    PipeLine.Class.PolygonElement pElement = new PipeLine.Class.PolygonElement();
                                    pElement.Geometry = r_Geometry;     //获取得到的缓冲区
                                    pElement.Symbol = getFillSSymbol();
                                    pElement.Opacity = 50;
                                    m_GraphicsContainer.AddElement(pElement, 0); //显示缓冲区
                                    m_ActiveView.Refresh();
                                    //MessageBox.Show(m_Feature.get_Value(0).ToString());
                                }
                            }
                        }
                        else
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show("请输入有效值", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            axMapControl1.Map.ClearSelection();
                            axMapControl1.Refresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                        }
                        break;
                    case 12:
                        IGeometry pgeometry;
                        pgeometry = axMapControl1.TrackLine();
                        IPointCollection pPointcol = pgeometry as IPointCollection;
                        pointCollection = pPointcol;
                        try
                        {
                            sectionAnalysis = new SectionAnalysis(this);
                            sectionAnalysis.xiangjiao1(pgeometry);
                            sectionAnalysis.Show();
                        }
                        catch (Exception ex)
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(ex.ToString());
                        }
                        if (sectionAnalysis.lFeature.Count == 0)
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show("请输入有效值", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            sectionAnalysis.Close();
                        }
                        break;
                    case 13:
                        IGeometry multi_Geometry13 = null;
                        multi_Geometry13 = axMapControl1.TrackLine();
                        IActiveView pActive = axMapControl1.Map as IActiveView;
                        ISimpleLineSymbol pLineSymbol = new SimpleLineSymbolClass();  //设置Symbol属性
                        pLineSymbol.Color = getColor(0, 255);
                        pLineSymbol.Width = 3;
                        ILineElement pLineElement = new LineElementClass();
                        IElement pEle = pLineElement as IElement;
                        pLineElement.Symbol = pLineSymbol;
                        pEle.Geometry = multi_Geometry13;
                        IGraphicsContainer pContainer = pMap as IGraphicsContainer;
                        pContainer.AddElement(pEle, 0);

                        if (multi_Geometry13 != null)
                        {

                            if (Application.OpenForms["LineAnalysis"] == null)
                            {
                                lineAnalysis = new LineAnalysis(axMapControl1);
                                lineAnalysis.Show();
                            }
                            else
                            {
                                Application.OpenForms["LineAnalysis"].Show();
                            }
                            lineAnalysis.result(multi_Geometry13);
                            //lineAnalysis.Show();
                            if (lineAnalysis.lFeature.Count == 0)
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show("没有选中任何管线！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                lineAnalysis.Close();
                            }
                        }
                        else
                        {
                            return;
                        }
                        break;
                    case 14:
                        IGeometry multi_Geometry14 = null;
                        multi_Geometry14 = axMapControl1.TrackRectangle();
                        IActiveView pActive14 = axMapControl1.Map as IActiveView;
                        ILineSymbol OutlineSymbol = new SimpleLineSymbolClass();
                        OutlineSymbol.Width = 3;
                        OutlineSymbol.Color = getColor(0, 255);
                        ISimpleFillSymbol fil1symbol14 = new SimpleFillSymbolClass();
                        fil1symbol14.Style = esriSimpleFillStyle.esriSFSNull;
                        fil1symbol14.Color = getColor(0, 255);
                        fil1symbol14.Outline = OutlineSymbol;
                        IFillShapeElement pFillElement14 = new RectangleElementClass();
                        pFillElement14.Symbol = fil1symbol14;
                        IElement pEle14 = pFillElement14 as IElement;
                        pEle14.Geometry = multi_Geometry14;
                        IGraphicsContainer pContainer14 = axMapControl1.Map as IGraphicsContainer;
                        pContainer14.AddElement(pEle14, 0);
                        //pActive14.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                        if (multi_Geometry14 != null)
                        {
                            if (Application.OpenForms["RectangleSelection"] == null)
                            {
                                rectangleSelection = new RectangleSelection(axMapControl1);
                                rectangleSelection.Show();
                            }
                            else
                            {
                                Application.OpenForms["RectangleSelection"].Show();
                            }
                            rectangleSelection.result(multi_Geometry14);
                            if (rectangleSelection.lFeature.Count == 0)
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show("没有选中任何管线！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                rectangleSelection.Close();
                            }
                        }
                        break;
                    case 15:
                        IPoint pPoint15 = new PointClass();
                        pPoint15.PutCoords(e.mapX, e.mapY);
                        //List<IPoint> lPoint = new List<IPoint>();
                        //lPoint.Add(pPoint15);
                        DrawLine(axMapControl1, pPoint15);
                        break;
                    case 16:

                        IPoint ipNew = new PointClass();
                        //ipNew = m_ipActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
                        for (int i = 0; i < axMapControl1.Map.LayerCount; i++)
                        {
                            IGeometry pGeometry;
                            IFeatureLayer pFeatureLayer;
                            ITopologicalOperator mTopo;
                            IFeatureCursor pCursor;
                            double Proportion2 = Math.Round(axMapControl1.MapScale, 0);
                            double m_Radius = Proportion2 / 1428.0;
                            ISpatialFilter pFilter;

                            if (clicked != true)
                                return;

                            if (m_ipPoints == null)
                            {
                                m_ipPoints = new Multipoint();
                            }
                            ipNew.PutCoords(e.mapX, e.mapY);
                            mTopo = ipNew as ITopologicalOperator;
                            pGeometry = mTopo.Buffer(m_Radius);
                            pFilter = new SpatialFilterClass();
                            pFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                            pFilter.Geometry = pGeometry;
                            pFeatureLayer = axMapControl1.Map.get_Layer(i) as IFeatureLayer;
                            pCursor = pFeatureLayer.Search(pFilter, false);
                            IFeature pFeature = pCursor.NextFeature();
                            if (pFeature != null)
                            {
                                lLayer.Add(axMapControl1.Map.get_Layer(i));
                                //MessageBox.Show(axMapControl1.Map.get_Layer(i).Name);
                            }
                        }
                        ipNew.PutCoords(e.mapX, e.mapY);
                        lPoint.Add(ipNew);

                        IElement element;
                        ITextElement textelement = new TextElementClass();
                        element = textelement as IElement;

                        if (clickedcount == 1)
                        {
                            textelement.Text = "起点";
                        }
                        if (clickedcount == 2)
                        {
                            textelement.Text = "终点";
                        }
                        IRgbColor color = new RgbColorClass();
                        color.Red = 255;
                        color.Blue = 0;
                        color.Green = 0;
                        ITextSymbol pTextSymbol = new TextSymbolClass();
                        pTextSymbol.Color = color;
                        textelement.Symbol = pTextSymbol;
                        clickedcount++;
                        //textelement.Text = clickedcount.ToString();
                        element.Geometry = m_ipActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
                        pGC.AddElement(element, 0);
                        //m_ipActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                        break;
                    case 17:
                        IPoint ipNew17 = new PointClass();
                        //ipNew = m_ipActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
                        for (int i = 0; i < axMapControl1.Map.LayerCount; i++)
                        {
                            IGeometry pGeometry;
                            IFeatureLayer pFeatureLayer;
                            ITopologicalOperator mTopo;
                            IFeatureCursor pCursor;
                            double Proportion2 = Math.Round(axMapControl1.MapScale, 0);
                            double m_Radius = Proportion2 / 1428.0;
                            ISpatialFilter pFilter;

                            if (clicked != true)
                                return;

                            if (m_ipPoints == null)
                            {
                                m_ipPoints = new Multipoint();
                            }
                            ipNew17.PutCoords(e.mapX, e.mapY);
                            mTopo = ipNew17 as ITopologicalOperator;
                            pGeometry = mTopo.Buffer(m_Radius);
                            pFilter = new SpatialFilterClass();
                            pFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                            pFilter.Geometry = pGeometry;
                            pFeatureLayer = axMapControl1.Map.get_Layer(i) as IFeatureLayer;
                            pCursor = pFeatureLayer.Search(pFilter, false);
                            IFeature pFeature = pCursor.NextFeature();
                            if (pFeature != null)
                            {
                                lLayer.Add(axMapControl1.Map.get_Layer(i));
                                //MessageBox.Show(axMapControl1.Map.get_Layer(i).Name);
                            }
                        }
                        ipNew17.PutCoords(e.mapX, e.mapY);
                        lPoint.Add(ipNew17);

                        IPoint Point_burst = new PointClass();
                        Point_burst.PutCoords(e.mapX, e.mapY);

                        IPictureMarkerSymbol pPicMarkerSymbol = new PictureMarkerSymbolClass();
                        string pPicturePath = Application.StartupPath + @"\pic\选中.gif";
                        pPicMarkerSymbol.CreateMarkerSymbolFromFile(esriIPictureType.esriIPictureGIF, pPicturePath);
                        pPicMarkerSymbol.Size = 20;
                        IMarkerElement pMarkerElement = new MarkerElementClass();
                        IElement ppElement;
                        ppElement = pMarkerElement as IElement;
                        clickedcount++;
                        ppElement.Geometry = Point_burst;
                        pMarkerElement.Symbol = pPicMarkerSymbol;

                        pGC.AddElement(ppElement, 0);
                        m_ipActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                        break;
                    case 18:
                        IPoint ipNew18 = new PointClass();
                        for (int i = 0; i < axMapControl1.Map.LayerCount; i++)
                        {
                            IGeometry pGeometry;
                            IFeatureLayer pFeatureLayer;
                            ITopologicalOperator mTopo;
                            IFeatureCursor pCursor;
                            double Proportion2 = Math.Round(axMapControl1.MapScale, 0);
                            double m_Radius = Proportion2 / 1428.0;
                            ISpatialFilter pFilter;

                            if (clicked != true)
                                return;

                            if (m_ipPoints == null)
                            {
                                m_ipPoints = new Multipoint();
                            }
                            ipNew18.PutCoords(e.mapX, e.mapY);
                            mTopo = ipNew18 as ITopologicalOperator;
                            pGeometry = mTopo.Buffer(m_Radius);
                            pFilter = new SpatialFilterClass();
                            pFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                            pFilter.Geometry = pGeometry;
                            pFeatureLayer = axMapControl1.Map.get_Layer(i) as IFeatureLayer;
                            pCursor = pFeatureLayer.Search(pFilter, false);
                            IFeature pFeature = pCursor.NextFeature();
                            if (pFeature != null)
                            {
                                lLayer.Add(axMapControl1.Map.get_Layer(i));
                            }
                        }
                        ipNew18.PutCoords(e.mapX, e.mapY);
                        lPoint.Add(ipNew18);
                        IPoint Point_burst18 = new PointClass();
                        Point_burst18.PutCoords(e.mapX, e.mapY);

                        IPictureMarkerSymbol pPicMarkerSymbol18 = new PictureMarkerSymbolClass();
                        string pPicturePath18 = Application.StartupPath + @"\pic\test_burst.gif";
                        pPicMarkerSymbol18.CreateMarkerSymbolFromFile(esriIPictureType.esriIPictureGIF, pPicturePath18);
                        pPicMarkerSymbol18.Size = 20;
                        IMarkerElement pMarkerElement18 = new MarkerElementClass();
                        IElement ppElement18;
                        ppElement18 = pMarkerElement18 as IElement;
                        double proportion_Line18 = Math.Round(axMapControl1.MapScale, 0);
                        double gapX18 = 5 * proportion_Line18 / 1428;
                        double gapY18 = 5 * proportion_Line18 / 1428;
                        Point_burst18.X += gapX18;
                        Point_burst18.Y += gapY18;
                        clickedcount++;
                        ppElement18.Geometry = Point_burst18;
                        pMarkerElement18.Symbol = pPicMarkerSymbol18;

                        pGC.AddElement(ppElement18, 0);
                        m_ipActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                        break;
                    case 19:
                        IPoint point_fire = new PointClass();
                        point_fire.PutCoords(e.mapX,e.mapY);
                        IGeometry test = point_fire as IGeometry;
                        m_GraphicsContainer = axMapControl1.Map as IGraphicsContainer;
                        m_GraphicsContainer.DeleteAllElements();
                        IActiveView m_ActiveView19 = axMapControl1.Map as IActiveView;
                        ITopologicalOperator Topological_fire = point_fire as ITopologicalOperator;
                        ILayer layer19 = null;
                        for (int i = 0; i < axMapControl1.Map.LayerCount; i++)
                        {
                            if (axMapControl1.Map.get_Layer(i).Name.ToString() == "消火栓井")
                            {
                                layer19 = axMapControl1.Map.get_Layer(i);
                            }
                        }
                        if (test != null & Fire_Condition_radius > 0)
                        {
                            //double radius19 = double.Parse(fireContion.fireDadius.Text);
                            IGeometry Geometry_fire = Topological_fire.Buffer(Fire_Condition_radius);
                            if (Geometry_fire.IsEmpty)
                            {
                                return;
                            }
                            else
                            {
                                PipeLine.Class.PolygonElement Element_fire = new Class.PolygonElement();
                                Element_fire.Geometry = Geometry_fire;//确定位置
                                Element_fire.Symbol = getFillSSymbol();//确定填充符号
                                Element_fire.Opacity = 50;
                                m_GraphicsContainer.AddElement(Element_fire, 0);
                                m_ActiveView19.Refresh();
                                if (Application.OpenForms["FireAnalysis"] == null)
                                {
                                    GetFireHydrant getFireHydrant = new GetFireHydrant();
                                    fireAnalysis = new FireAnalysis(axMapControl1, point_fire, getFireHydrant.result(axMapControl1, Geometry_fire));                                  
                                    fireAnalysis.lFeature = getFireHydrant.getFeatures(axMapControl1, Geometry_fire);
                                    if (fireAnalysis.lFeature.Count > 0)
                                    {
                                        fireAnalysis.gridControl1.DataSource = getFireHydrant.result(axMapControl1, Geometry_fire);
                                        fireAnalysis.gridView1.BestFitColumns();
                                        fireAnalysis.Show();
                                    }
                                    else
                                    {
                                        DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show("是否搜索最近的消防栓？", "此范围无消防栓", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                        if (result == DialogResult.Yes)
                                        {
                                            //fireAnalysis.gridControl1.DataSource = getFireHydrant.result(axMapControl1, Geometry_fire);
                                            //fireAnalysis.gridView1.BestFitColumns();
                                            fireAnalysis.point_fire = point_fire;
                                            
                                            IFeature fea = getFireHydrant.GetNearFireHydrant(layer19, point_fire);
                                            axMapControl1.Map.SelectFeature(layer19, fea);
                                            Flash flash = new Flash();
                                            flash.twinkleFireHydrant(fea, axMapControl1);
                                            //axMapControl1.Refresh();
                                            
                                            fireAnalysis.Show();
                                        }
                                        else
                                        {
                                            return;
                                        }
                                    }
                                    
                                }
                                else
                                {
                                    GetFireHydrant getFireHydrant = new GetFireHydrant();
                                    fireAnalysis.lFeature = getFireHydrant.getFeatures(axMapControl1, Geometry_fire);
                                    fireAnalysis.gridControl1.DataSource = getFireHydrant.result(axMapControl1, Geometry_fire);
                                    fireAnalysis.gridView1.BestFitColumns();
                                    if (fireAnalysis.lFeature.Count > 0)
                                    {
                                        fireAnalysis.dt = getFireHydrant.result(axMapControl1, Geometry_fire);
                                        fireAnalysis.point_fire = point_fire;
                                        fireAnalysis.gridControl1.DataSource = getFireHydrant.result(axMapControl1, Geometry_fire);
                                        fireAnalysis.gridView1.BestFitColumns();
                                        Application.OpenForms["FireAnalysis"].Show();
                                    }
                                    else
                                    {
                                        DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show("是否搜索最近的消防栓？", "此范围无消防栓", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                        if (result == DialogResult.Yes)
                                        {
                                            fireAnalysis.point_fire = point_fire;
                                            IFeature fea = getFireHydrant.GetNearFireHydrant(layer19, point_fire);
                                            axMapControl1.Map.SelectFeature(layer19, fea);
                                            Flash flash = new Flash();
                                            flash.twinkleFireHydrant(fea, axMapControl1);
                                            //axMapControl1.Refresh();
                                            fireAnalysis.Show();
                                        }
                                        else
                                        {
                                            return;
                                        }
                                    }
                                    
                                }
                            }

                        }
                        else
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show("操作有误");
                        }
                        break;
                    case 20:
                        IGeometry multi_Geometry20 = null;
                        multi_Geometry20 = axMapControl1.TrackRectangle();
                        axMapControl1.Map.SelectByShape(multi_Geometry20, null, false);
                        axMapControl1.Refresh();
                        break;
                    case 21:
                        IGeometry multi_Geometry21 = null;
                        multi_Geometry21 = axMapControl1.TrackCircle();
                        axMapControl1.Map.SelectByShape(multi_Geometry21, null, false);
                        axMapControl1.Refresh();
                        break;
                    case 22:
                        IPoint point22 = new PointClass();
                        point22.PutCoords(e.mapX,e.mapY);
                        IGeometry Position_geo = point22 as IGeometry;
                        Flash flash22 = new Flash();
                        flash22.Position(Position_geo,axMapControl1);
                        break;
                }
            }
            if (e.button == 2)
            {
                choice = 0;
                axMapControl1.CurrentTool = null;
                /////地图视窗鼠标事件
                IToolbarMenu mapPopMenu = null;
                mapPopMenu = new ToolbarMenu();
                mapPopMenu.AddItem(new ControlsMapPanTool(), -1, 0, false, esriCommandStyles.esriCommandStyleIconAndText);
                mapPopMenu.AddItem(new ControlsMapFullExtentCommand(), -1, 1, false, esriCommandStyles.esriCommandStyleIconAndText);
                mapPopMenu.AddItem(new ControlsMapIdentifyTool(), -1, 2, false, esriCommandStyles.esriCommandStyleIconAndText);//识别工具
                mapPopMenu.AddItem(new ControlsMapZoomInFixedCommand(), -1, 3, false, esriCommandStyles.esriCommandStyleIconAndText);//
                mapPopMenu.AddItem(new ControlsMapZoomOutFixedCommand(), -1, 4, false, esriCommandStyles.esriCommandStyleIconAndText);
                mapPopMenu.AddItem(new ControlsSelectFeaturesTool(), -1, 5, false, esriCommandStyles.esriCommandStyleIconAndText);//选择要素工具
                mapPopMenu.AddItem(new ControlsClearSelectionCommand(), -1, 6, false, esriCommandStyles.esriCommandStyleIconAndText);//缩放所选要素
                mapPopMenu.AddItem(new ControlsZoomToSelectedCommand(), -1, 7, false, esriCommandStyles.esriCommandStyleIconAndText);
                mapPopMenu.AddItem(new ControlsMapZoomToLastExtentBackCommand(), -1, 8, false, esriCommandStyles.esriCommandStyleIconAndText);
                mapPopMenu.AddItem(new ControlsMapZoomToLastExtentForwardCommand(), -1, 9, false, esriCommandStyles.esriCommandStyleIconAndText);

                mapPopMenu.SetHook(axMapControl1);//// 得到地图视窗右键菜单
                mapPopMenu.PopupMenu(e.x, e.y, axMapControl1.hWnd);//弹出显示菜单
            }
        }
        //根据RGB值得到IColor
        public static IRgbColor getColor(int R, int G)
        {
            IRgbColor color = new RgbColorClass();
            color.Red = R;
            color.Green = G;
            return color;
        }
        //根据多边形得到填充element
        public static IFillSymbol getFillSSymbol()
        {
            ISimpleLineSymbol symbol = new SimpleLineSymbolClass();
            symbol.Color = getColor(255, 0);
            symbol.Width = 1;
            symbol.Style = esriSimpleLineStyle.esriSLSSolid;
            ISimpleFillSymbol fillSymbol = new SimpleFillSymbolClass();
            fillSymbol.Outline = (ILineSymbol)symbol;
            fillSymbol.Color = getColor(255, 255);
            fillSymbol.Style = esriSimpleFillStyle.esriSFSSolid;

            return (IFillSymbol)fillSymbol;
        }
        //标高中画出来的斜线
        public static void DrawLine(AxMapControl axMapControl, IPoint pPoint)
        {
            double proportion_Line = Math.Round(axMapControl.MapScale, 0);

            ITextElement pText = new TextElementClass();
            pText.Text = "坐标X:" + Math.Round(pPoint.X, 1) + "\n" + "坐标Y:" + Math.Round(pPoint.Y, 1);

            List<IGeometry> lGeometry = new List<IGeometry>();
            IGeometry GeometryLine, GeometryLine2, GeometryLine3;
            IGraphicsContainer pGrapgicsContainer = axMapControl.Map as IGraphicsContainer;
            IActiveView ActiveView_line = axMapControl.Map as IActiveView;
            IPolyline ppPolyline = new PolylineClass();
            IPolyline ppPolyline2 = new PolylineClass();
            IPolyline ppPolyline3 = new PolylineClass();
            ISimpleLineSymbol plineSybol = new SimpleLineSymbolClass();
            plineSybol.Color = getColor(255, 0);
            plineSybol.Width = 0.5;

            //MessageBox.Show(proportion_Line.ToString());
            ppPolyline.FromPoint = pPoint;
            double Xgap = 15 * proportion_Line / 1428;
            double Ygap = 25 * proportion_Line / 1428;
            pPoint.X = pPoint.X + Xgap;
            pPoint.Y = pPoint.Y + Ygap;
            ppPolyline.ToPoint = pPoint;
            IPoint point = pPoint;
            ppPolyline2.FromPoint = pPoint;
            ppPolyline3.FromPoint = pPoint;
            double Ygap2 = 20 * proportion_Line / 1428;
            pPoint.Y = pPoint.Y + Ygap2;
            ppPolyline2.ToPoint = pPoint;
            double Xgap3 = 50 * proportion_Line / 1428;
            point.X = point.X + Xgap3;
            point.Y = ppPolyline.ToPoint.Y;
            ppPolyline3.ToPoint = point;
            GeometryLine = ppPolyline;
            lGeometry.Add(GeometryLine);
            GeometryLine2 = ppPolyline2;
            lGeometry.Add(GeometryLine2);
            GeometryLine3 = ppPolyline3;
            lGeometry.Add(GeometryLine3);

            IMap pMap15 = axMapControl.Map;
            IActiveView pActiveView = pMap15 as IActiveView;

            IPoint point_location = new PointClass();
            point_location.X = ppPolyline2.FromPoint.X;
            point_location.Y = ppPolyline2.FromPoint.Y;

            double Xgap_locatioon = 25 * proportion_Line / 1428;
            double Ygap_location = 10 * proportion_Line / 1428;
            point_location.X += Xgap_locatioon;
            point_location.Y += Ygap_location;

            IElement pEle15 = pText as IElement;
            pEle15.Geometry = point_location;
            IGraphicsContainer pgraphic = axMapControl.Map as IGraphicsContainer;
            pgraphic.AddElement(pEle15, 0);
            if (GeometryLine != null)
            {
                for (int i = 0; i < lGeometry.Count; i++)
                {
                    ILineElement pLineElement = new LineElementClass();
                    IElement ppElement = pLineElement as IElement;
                    pLineElement.Symbol = plineSybol;
                    ppElement.Geometry = lGeometry[i];
                    IGraphicsContainer pGraphicsContainer = axMapControl.Map as IGraphicsContainer;
                    pGrapgicsContainer.AddElement(ppElement, 0);
                }

            }
            else
            {
                return;
            }
            ActiveView_line.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, axMapControl.Extent);

        }
        //添加材质标注
        public static void AddLable(AxMapControl axMapControl, IFeature pFeature, string fieldName, IPoint pPoint, IFeatureLayer pFeatureLayer)
        {
            IActiveView pActiveView = axMapControl.Map as IActiveView;
            ITextElement pTextElement = new TextElementClass();
            int index = pFeature.Fields.FindField(fieldName);//要标注的字段的索引
            pTextElement.Text = pFeatureLayer.Name + "的埋深：" + pFeature.get_Value(index).ToString() + "米";

            IElement pElement = pTextElement as IElement;

            IFormattedTextSymbol pFormttedTextSymbol = new TextSymbolClass();
            ICallout pCallout = new BalloonCalloutClass();
            pCallout.AnchorPoint = pPoint;
            pFormttedTextSymbol.Background = pCallout as ITextBackground;
            pTextElement.Symbol = pFormttedTextSymbol;
            pElement.Geometry = pPoint;
            IGraphicsContainer pGraphicsContainer = axMapControl.Map as IGraphicsContainer;
            pGraphicsContainer.AddElement(pElement, 0);
            //pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, axMapControl.Extent);
        }

        public static void DrawLine_futu(AxMapControl axMapControl, IPoint pPoint)
        {
            IGeometry GeometryLine;
            IGraphicsContainer pGrapgicsContainer = axMapControl.Map as IGraphicsContainer;
            IActiveView ActiveView_line = axMapControl.Map as IActiveView;
            IPolyline ppPolyline = new PolylineClass();
            ISimpleLineSymbol plineSybol = new SimpleLineSymbolClass();
            plineSybol.Color = getColor(255, 255);
            plineSybol.Width = 2;
            double proportion_Line = Math.Round(axMapControl.MapScale, 0);
            ppPolyline.FromPoint = pPoint;
            double Xgap = 15 * proportion_Line / 1428;
            double Ygap = 25 * proportion_Line / 1428;
            pPoint.X = pPoint.X + Xgap;
            pPoint.Y = pPoint.Y + Ygap;
            ppPolyline.ToPoint = pPoint;
            GeometryLine = ppPolyline;
            if (GeometryLine != null)
            {
                ILineElement pLineElement = new LineElementClass();
                IElement ppElement = pLineElement as IElement;
                pLineElement.Symbol = plineSybol;
                ppElement.Geometry = GeometryLine;
                IGraphicsContainer pGraphicsContainer = axMapControl.Map as IGraphicsContainer;
                pGrapgicsContainer.AddElement(ppElement, 0);
            }
            else
            {
                return;
            }
            ActiveView_line.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, axMapControl.Extent);
        }

        private void selectbt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axMapControl1.CurrentTool = null;
            axSceneControl1.CurrentTool = null;
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerArrow;
            choice = 0;
            flag = 0;
            string strpath = Application.StartupPath;

            //MessageBox.Show(GetIP());
            //ILayer layer = axMapControl1.Map.get_Layer(1);
            //IFeatureLayer pFeatureLayer =(IFeatureLayer);
            //IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
            //MessageBox.Show(DateTime.Now.ToLocalTime().ToString());
            //MessageBox.Show(DateTime.Now.ToLongDateString().ToString() + "，"+DateTime.Now.ToLongTimeString().ToString());
            //StreamWriter sw = File.AppendText(Application.StartupPath + @"\AdminLog.txt"); //@"\XNTG\XNTG.mxd"
            //string w = DateTime.Now.ToLongDateString().ToString() + "，" + DateTime.Now.ToLongTimeString().ToString() + "\r\n";
            //sw.Write(w);
            //sw.Close();
            //System.Net.IPHostEntry myEntry = System.Net.Dns.GetHostEntry("192.168.72.183");
            //string ipAddress = myEntry.AddressList[1].ToString();
            //MessageBox.Show(ipAddress);

            //IFeatureLayer mFeatureLayer = axMapControl1.Map.get_Layer(0) as IFeatureLayer;
            //if (DeleteField(mFeatureLayer, "标高"))
            //{
            //    MessageBox.Show("删除成功");
            //}
            //else
            //{
            //    MessageBox.Show("删除失败");
            //}

        }
        /// <summary>
        /// 删除字段函数，返回布尔类型!
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static bool DeleteField(IFeatureLayer layer, string fieldName)
        {
            try
            {
                ITable pTable = (ITable)layer;
                IFields pfields;
                IField pfield;
                pfields = pTable.Fields;
                int fieldIndex = pfields.FindField(fieldName);
                pfield = pfields.get_Field(fieldIndex);
                pTable.DeleteField(pfield);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //放大
        private void zoomInbt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axMapControl1.CurrentTool = null;
            axSceneControl1.CurrentTool = null;
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerZoomIn;
            choice = 1;
            flag = 1;
        }
        //缩小
        private void zoomOutbt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axMapControl1.CurrentTool = null;
            axSceneControl1.CurrentTool = null;
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerPageZoomOut;
            choice = 1;
            flag = 2;
        }
        //平移
        private void panbt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axMapControl1.CurrentTool = null;
            axSceneControl1.CurrentTool = null;
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerPagePan;
            choice = 1;
            flag = 3;
        }
        //全图
        private void fullExtentbt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axMapControl1.CurrentTool = null;
            axSceneControl1.CurrentTool = null;
            choice = 0;
            axMapControl1.Map.ClearSelection();
            axMapControl1.ActiveView.Refresh();
            axMapControl1.Extent = axMapControl1.FullExtent;
        }
        //测量长度
        private void measureLinebt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axSceneControl1.CurrentTool = null;
            if (ML == null || ML.IsDisposed)
            {
                ML = new MeasureLine();
            }
            ML.Show();
            choice = 2;
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
        }
        //测量面积
        private void measureAerabt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axSceneControl1.CurrentTool = null;
            if (MA == null || MA.IsDisposed)
            {
                MA = new MeasureArea();
            }
            choice = 3;
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
            MA.TopMost = true;
            MA.Show();
        }

        private void exportInShpbt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axSceneControl1.CurrentTool = null;
            //创建工作空间工厂！
            choice = 0;
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;

            IWorkspaceFactory pWorkspaceFactory = new ShapefileWorkspaceFactory();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "shaperfile(*.shp)|*.shp";
            openFileDialog1.Title = "加载shape数据";
            openFileDialog1.Multiselect = false;
            DialogResult pDialogResult = openFileDialog1.ShowDialog();
            if (pDialogResult != DialogResult.OK)
                return;
            string pPath = openFileDialog1.FileName;
            string pFolder = System.IO.Path.GetDirectoryName(pPath);
            string pFileName = System.IO.Path.GetFileName(pPath);
            //打开shape工作空间！
            IWorkspace pWorkspace = pWorkspaceFactory.OpenFromFile(pFolder, 0);
            IFeatureWorkspace pFeatureWorkspace = pWorkspace as IFeatureWorkspace;
            //打开要素类！
            IFeatureClass pFC = pFeatureWorkspace.OpenFeatureClass(pFileName);
            // 创建要素图层！
            IFeatureLayer pFLayer = new FeatureLayer();
            pFLayer.FeatureClass = pFC;
            // 关联到地图！
            pFLayer.Name = pFC.AliasName;
            ILayer pLayer = pFLayer as ILayer;
            IMap pMap = axMapControl1.Map;
            // 添加到地图空间！
            pMap.AddLayer(pLayer);
            axMapControl1.ActiveView.Refresh();
        }

        private void exportOutLayerbt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axSceneControl1.CurrentTool = null;
            choice = 0;
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
            SaveDocument();
        }
        //保存成mxd格式文件
        private void SaveDocument()
        {
            try
            {
                IMxdContents pMxdC;
                pMxdC = axMapControl1.Map as IMxdContents;
                IMapDocument pMapDocument = new MapDocument();
                pMapDocument.Open(axMapControl1.DocumentFilename, "");
                IActiveView pActiveView = axMapControl1.Map as IActiveView;
                pMapDocument.ReplaceContents(pMxdC);
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "mxd（*.mxd）|*.mxd";
                saveFileDialog.RestoreDirectory = true;
                //saveFileDialog.ShowDialog();
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string localFilePath = saveFileDialog.FileName.ToString();
                    pMapDocument.SaveAs(localFilePath, true, true);
                    DevExpress.XtraEditors.XtraMessageBox.Show("保存成功！", "管网系统信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("保存文档失败！！！" + e.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportOutCADbt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axSceneControl1.CurrentTool = null;
            choice = 0;
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;

            IWorkspaceFactory pWorkspaceFactory;
            IFeatureWorkspace pFeatureWorkspace;
            IFeatureLayer pFeatureLayer;
            IFeatureDataset pFeatureDataset;
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "CAD(*.dwg)|*.dwg|All Files(*.*)|*.*";
            dlg.Title = "打开CAD文件";
            dlg.ShowDialog();
            string strFullPath = dlg.FileName;
            if (strFullPath == "") return;
            int Index = strFullPath.LastIndexOf("\\");
            string filePath = strFullPath.Substring(0, Index);
            string fileName = strFullPath.Substring(Index + 1);
            //打开CAD数据集
            pWorkspaceFactory = new CadWorkspaceFactoryClass();
            pFeatureWorkspace = (IFeatureWorkspace)pWorkspaceFactory.OpenFromFile(filePath, 0);
            //打开一个要素集
            pFeatureDataset = pFeatureWorkspace.OpenFeatureDataset(fileName);
            //IFeaturClassContainer可以管理IFeatureDataset中的每个要素类 
            IFeatureClassContainer pFeatClassContainer = (IFeatureClassContainer)pFeatureDataset;
            //对CAD文件中的要素进行遍历处理 
            for (int i = 0; i < pFeatClassContainer.ClassCount - 1; i++)
            {
                IFeatureClass pFeatClass = pFeatClassContainer.get_Class(i);
                if (pFeatClass.FeatureType == esriFeatureType.esriFTCoverageAnnotation)
                {
                    //如果是注记，则添加注记层
                    pFeatureLayer = new CadAnnotationLayerClass();

                }
                else
                {
                    pFeatureLayer = new FeatureLayerClass();
                    pFeatureLayer.Name = pFeatClass.AliasName;
                    pFeatureLayer.FeatureClass = pFeatClass;
                    axMapControl1.Map.AddLayer(pFeatureLayer);
                    axMapControl1.ActiveView.Refresh();

                }
            }
        }

        private void axTOCControl1_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            if (e.button != 2)
            {
                //axMapControl1.Map.ClearSelection();
                //axMapControl1.ActiveView.Refresh();
                return;
            }
            esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;
            IBasicMap map = new MapClass();
            layer = new FeatureLayerClass();
            object other = new object();
            object index = new object();
            axTOCControl1.HitTest(e.x, e.y, ref item, ref map, ref layer, ref other, ref index);
            //axTOCControl1.Update();
            if (item == esriTOCControlItem.esriTOCControlItemLayer)
            {
                m_ToolMenuLayer.AddItem(new OpenAttributeTable(axMapControl1, layer), -1, 0, true, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconAndText);
                m_ToolMenuLayer.AddItem(new DeleteLayer(layer), -1, 0, true, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconAndText);
                m_ToolMenuLayer.AddItem(new FullExtent(axMapControl1), -1, 0, true, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconAndText);
                m_ToolMenuLayer.AddItem(new Owntolayer(layer, axMapControl1, eve), -1, 0, true, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconAndText);
                //m_ToolMenuLayer.AddItem(new TrackLine(axMapControl1), -1, 0, true, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconAndText);
                m_ToolMenuLayer.PopupMenu(e.x, e.y, m_TocControl.hWnd);
                m_ToolMenuLayer.RemoveAll();

            }

        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            choice = 0;
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
            ICommand m_Command1 = new ControlsSceneSelectFeaturesTool();
            m_Command1.OnCreate(axSceneControl1.Object);
            this.axSceneControl1.CurrentTool = m_Command1 as ESRI.ArcGIS.SystemUI.ITool;
            //scene = 0;
            //ICommand m_Command1 = new ControlsSceneSelectFeaturesTool();
            //m_Command1.OnCreate(axSceneControl2.Object);
            //this.axSceneControl2.CurrentTool = m_Command1 as ESRI.ArcGIS.SystemUI.ITool;
            //choice = 0;
            //axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
            //Operation_3D(6, axSceneControl1);
            //axSceneControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
        }
        //tab联动
        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (tabControl2.SelectedIndex == 0)
            //{
            //    tabControl1.SelectedIndex = 0;
            //}
            //if (tabControl2.SelectedIndex == 1)
            //{
            //    tabControl1.SelectedIndex = 1;
            //    axSceneControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
            //}
            //if (tabControl2.SelectedIndex == 2)
            //{
            //    tabControl1.SelectedIndex = 2;
            //}
        }
        //tab联动
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (tabControl1.SelectedIndex == 0)
            //{
            //    tabControl2.SelectedIndex = 0;
            //}
            //if (tabControl1.SelectedIndex == 1)
            //{
            //    tabControl2.SelectedIndex = 1;
            //    axSceneControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
            //}
            //if (tabControl1.SelectedIndex == 2)
            //{
            //    tabControl2.SelectedIndex = 2;
            //}
        }

        private void axMapControl1_wheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //MessageBox.Show("滚轮");
        }
        //axSceneControl的滚轮事件
        private void axSceneControl1_wheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //listBox1.Hide();
            listBox3D.Hide();
            axMapControl1.Map.ClearSelection();
            axMapControl1.ActiveView.Refresh();
            axSceneControl2.Scene.ClearSelection();
            axSceneControl2.Scene.SceneGraph.RefreshViewers();
            try
            {

                System.Drawing.Point point1 = axSceneControl1.PointToScreen(this.axSceneControl1.Location);
                System.Drawing.Point point2 = this.PointToScreen(e.Location);
                if (point2.X < point1.X | point2.X > point1.X + axSceneControl1.Width | point2.Y < point1.Y | point2.Y > point1.Y + axSceneControl1.Height)
                {
                    return;
                }
                double scale = 0.2;
                if (e.Delta < 0) scale = -0.2;
                ICamera mCamera = axSceneControl1.Camera;
                IPoint mPointObserver = mCamera.Observer;
                IPoint mPointTarget = mCamera.Target;
                mPointObserver.X += (mPointObserver.X - mPointTarget.X) * scale;
                mPointObserver.Y += (mPointObserver.Y - mPointTarget.Y) * scale;
                mPointObserver.Z += (mPointObserver.Z - mPointTarget.Z) * scale;
                mCamera.Observer = mPointObserver;
                axSceneControl1.SceneGraph.RefreshViewers();
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.ToString());
            }
        }
        private void axSceneControl2_wheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //listBox1.Hide();
            listBox3D.Hide();
            axMapControl1.Map.ClearSelection();
            axMapControl1.ActiveView.Refresh();
            try
            {

                System.Drawing.Point point1 = axSceneControl2.PointToScreen(this.axSceneControl2.Location);
                System.Drawing.Point point2 = this.PointToScreen(e.Location);
                if (point2.X < point1.X | point2.X > point1.X + axSceneControl2.Width | point2.Y < point1.Y | point2.Y > point1.Y + axSceneControl2.Height)
                {
                    return;
                }
                double scale = 0.2;
                if (e.Delta < 0) scale = -0.2;
                ICamera mCamera = axSceneControl2.Camera;
                IPoint mPointObserver = mCamera.Observer;
                IPoint mPointTarget = mCamera.Target;
                mPointObserver.X += (mPointObserver.X - mPointTarget.X) * scale;
                mPointObserver.Y += (mPointObserver.Y - mPointTarget.Y) * scale;
                mPointObserver.Z += (mPointObserver.Z - mPointTarget.Z) * scale;
                mCamera.Observer = mPointObserver;
                axSceneControl2.SceneGraph.RefreshViewers();
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.ToString());
            }
        }
        private void jiemianbt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axMapControl1.CurrentTool = null;
            axSceneControl1.CurrentTool = null;
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
            choice = 4;

        }
        private void Cancelbt_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            choice = 0;
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerArrow;
            axMapControl1.Map.ClearSelection();
            axMapControl1.ActiveView.Refresh();

        }

        private void attributeQuerybt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            axSceneControl1.CurrentTool = null;
            choice = 0;
            if (SMBA == null || SMBA.IsDisposed)
            {
                SMBA = new SearchMapByAttribution(axMapControl1);
                SMBA.Show();
            }
            else
            {
                SMBA.Activate();
            }
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
        }
        public double ConvertToDouble(object value)
        {
            return Convert.ToDouble(value);
        }
        //2015015添加的函数:得到字段的索引值
        public int GetFieldIndex(IFeatureClass feaClass, string fieldName)
        {
            return feaClass.FindField(fieldName);
        }
        private void animationbt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axSceneControl1.CurrentTool = null;
            choice = 0;
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
            scene = 1;
            Operation_3D(0, axSceneControl2);
            //tabControl1.SelectedIndex = 1;//设置窗体为axscenecontrol界面
            xtraTabControl1.SelectedTabPageIndex = 1;
            xtraTabControl2.SelectedTabPageIndex = 1;
            RoamingSegment myRoamingSegment = new RoamingSegment(axSceneControl1, target, observer);
            myRoamingSegment.Show();

            ICamera pCamaera = axSceneControl1.Camera;
            pCamaera.Azimuth = 90;
            pCamaera.Target = target;
            pCamaera.Observer = observer;

            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;

        }

        private void axMapControl1_OnDoubleClick(object sender, IMapControlEvents2_OnDoubleClickEvent e)
        {
            switch (choice)
            {
                case 1:
                    break;
                case 2:
                    DevExpress.XtraEditors.XtraMessageBox.Show("请不要双击", "管网系统信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
        }

        private void axTOCControl1_OnDoubleClick(object sender, ITOCControlEvents_OnDoubleClickEvent e)
        {
            esriTOCControlItem itemType = esriTOCControlItem.esriTOCControlItemNone;
            IBasicMap basicMap = null;
            ILayer layer = null;
            object unk = null;
            object data = null;
            axTOCControl1.HitTest(e.x, e.y, ref itemType, ref basicMap, ref layer, ref unk, ref data);
            if (e.button == 1)
            {

                if (itemType == esriTOCControlItem.esriTOCControlItemLegendClass)
                {
                    //取得图例
                    ILegendClass pLegendClass = ((ILegendGroup)unk).get_Class((int)data);
                    //创建符号选择器SymbolSelector实例
                    SymbolSelectorFrm tt = new SymbolSelectorFrm(pLegendClass, layer);
                    if (tt.ShowDialog() == DialogResult.OK)
                    {
                        //axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                        //设置新的符号
                        pLegendClass.Symbol = tt.pSymbol;
                        //更新主Map控件和图层控件
                        this.axMapControl1.ActiveView.Refresh();
                        this.axTOCControl1.Refresh();
                        tt.Close();

                    }

                }
            }

        }
        //点选
        private void pointSelectbt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axSceneControl1.CurrentTool = null;
            axMapControl1.CurrentTool = null;
            choice = 5;
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerIdentify;
        }

        private void otherQuerybt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axSceneControl1.CurrentTool = null;
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerArrowQuestion;
            choice = 6;
        }

        private void pipepointInfobt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axSceneControl1.CurrentTool = null;
            choice = 0;
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
            //PipePointQuery PPQ = new PipePointQuery(axMapControl1);
            if (PPQ == null || PPQ.IsDisposed)
            {
                PPQ = new PipePointQuery(axMapControl1);
                PPQ.Show();
            }
            else
            {
                PPQ.Activate();
            }
        }
        //管径标注
        private void radiusLablebt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axMapControl1.CurrentTool = null;
            axSceneControl1.CurrentTool = null;
            choice = 9;
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
            //choose = 1;
        }
        //标注材质
        private void materialLabelbt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axMapControl1.CurrentTool = null;
            axSceneControl1.CurrentTool = null;
            choice = 10;
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
        }
        //坐标标注
        private void coordinateLabel_bt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axMapControl1.CurrentTool = null;
            axSceneControl1.CurrentTool = null;
            choice = 15;
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
        }
        //取消标注
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerArrow;
            axMapControl1.Map.ClearSelection();
            axMapControl1.ActiveView.Refresh();

            IGraphicsContainer pGrapgicsContainer;
            IMap pMap = axMapControl1.Map;
            pGrapgicsContainer = pMap as IGraphicsContainer;
            pGrapgicsContainer.DeleteAllElements();

            //axMapControl1.Map.ClearSelection();
            //m_GraphicsContainer.DeleteAllElements();
            //axMapControl1.Refresh();
            choice = 0;
            //choose = 0;
            flag = 0;
        }
        //多边形选择
        private void poligonSelectbt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axMapControl1.CurrentTool = null;
            axSceneControl1.CurrentTool = null;
            choice = 7;
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
        }

        private void earthingbt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axSceneControl1.CurrentTool = null;
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerIdentify;
            choice = 8;
        }

        private void axMapControl1_ClientSizeChanged(object sender, EventArgs e)
        {
            //listBox1.Hide();
            //axMapControl1.Map.ClearSelection();
            //axMapControl1.ActiveView.Refresh();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pointStatisticsbt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axSceneControl1.CurrentTool = null;
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
            //PipePointc pPipePoint = new PipePointc(axMapControl1);
            //pPipePoint.Show(axMapControl1);
            pointNumberQuery = new PointNumberQuery(axMapControl1);
            pointNumberQuery.Show();
        }

        private void lineStatisticsbt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axSceneControl1.CurrentTool = null;
            choice = 0;
            //PipeStatistics PS = new PipeStatistics(axMapControl1);
            //PS.Show();
            if (Application.OpenForms["PipeLineStatistic"] == null)
            {
                pipeLineStatistic = new PipeLineStatistic(axMapControl1);
                pipeLineStatistic.Show();
            }
            else
            {
                Application.OpenForms["PipeLineStatistic"].Show();
            }
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
        }

        private void lengthbt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axSceneControl1.CurrentTool = null;
            choice = 0;
            //LengthStatistics LS = new LengthStatistics(axMapControl1);
            //LS.Show();
            if (Application.OpenForms["PipeLength"] == null)
            {
                pipeLength = new PipeLength(axMapControl1);
                pipeLength.Show();
            }
            else
            {
                Application.OpenForms["PipeLength"].Show();
            }
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
        }
        //鼠标移动事件
        private void axMapControl1_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            #region 显示坐标
            this.currentCoodinate.Caption = "坐标：" + e.mapX.ToString() + "   " + e.mapY.ToString();
            #endregion
            #region 显示比例尺
            double S = Math.Round(axMapControl1.MapScale, 0);   //保留0位小数
            //curr.Text = "比列尺: 1:" + S.ToString() + "          ";
            currentScale.Caption = "比列尺: 1:" + S.ToString() + "";
            Curr_scale.Caption = "比列尺: 1:" + S.ToString() + "";
            #endregion
        }
        //净距分析
        private void distancebt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axSceneControl1.CurrentTool = null;
            choice = 0;
            //BR.Hide();
            if (BR == null || BR.IsDisposed)
            {
                BR = new BufferRadius(axMapControl1);
            }
            choice = 11;
            BR.Show();
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
        }
        /// <summary>
        /// 三维属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void attribure3D_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //axSceneControl1.CurrentTool = null;
            Operation_3D(6, axSceneControl1);
            number = 2;
            axSceneControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerIdentify;
            choice = 0;
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;

            //Operation_3D(6, axSceneControl1);
        }
        private void axSceneControl2_OnMouseDown(object sender, ISceneControlEvents_OnMouseDownEvent e)
        {
            switch (scene)
            {
                case 1:
                    axSceneControl2.SceneGraph.IsNavigating = false;
                    IHit3DSet pHit3DSet;
                    axSceneControl2.SceneGraph.LocateMultiple(axSceneControl2.SceneGraph.ActiveViewer, e.x, e.y, esriScenePickMode.esriScenePickAll, false, out pHit3DSet);

                    //pHit3DSet.OnePerLayer();
                    if (pHit3DSet == null)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("没有选中任何模型！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (pHit3DSet.Hits.Count == 0)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("当前点未能查找到任何要素", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    //IDisplay3D pDisplay3D = (IDisplay3D)axSceneControl1.Scene.SceneGraph;
                    //PP_3D.pDisplay3d = pDisplay3D;
                    //if (PP_3D == null || PP_3D.IsDisposed)
                    //{
                    //    PP_3D = new property_3D();
                    //}
                    //else
                    //{
                    //    PP_3D.Activate();
                    //}
                    //PP_3D.Show();
                    //PP_3D.refeshView(pHit3DSet);
                    //axSceneControl2.Refresh();
                    //.Refresh();
                    break;
                case 2:
                    IPoint _locatePoint = new PointClass();
                    ISceneViewer _sceneView = axSceneControl2.SceneViewer;
                    ITopologicalOperator pTopo;
                    IGeometry pGeometry;
                    IFeature pFeature;
                    IFeatureLayer pFeatureLayer;
                    IFeatureCursor pCursor;
                    ISpatialFilter pFilter;
                    object _owner;
                    object _object;
                    try
                    {
                        axSceneControl2.SceneGraph.Locate(_sceneView, e.x, e.y, esriScenePickMode.esriScenePickPlane, true, out _locatePoint, out _owner, out _object);
                    }
                    catch (Exception ex)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(ex.ToString());
                    }
                    if (_locatePoint == null)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("请选择图层要素!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;

                    }
                    //获取三维坐标！
                    double _locateX = _locatePoint.X;
                    double _locateY = _locatePoint.Y;
                    double _locateZ = _locatePoint.Z;
                    try
                    {
                        for (int i = 1; i < axSceneControl2.Scene.LayerCount; i++)
                        {
                            //pFeatureLayer = axMapControl1.Map.get_Layer(i) as IFeatureLayer; // 将第3 个图层作为目标图层
                            pFeatureLayer = axSceneControl2.Scene.get_Layer(i) as IFeatureLayer;
                            IFeatureLayer featurelayer = (IFeatureLayer)pFeatureLayer;
                            IFeatureClass featureclass = featurelayer.FeatureClass;
                            //点选
                            pTopo = _locatePoint as ITopologicalOperator;
                            _locatePoint.PutCoords(_locateX, _locateY);
                            pGeometry = pTopo.Buffer(0.001);
                            axSceneControl2.Scene.SelectByShape(pGeometry, null, true);
                            //axSceneControl2.Scene.SceneGraph.RefreshViewers();
                            //选择图层
                            pFilter = new SpatialFilterClass();
                            pFilter.GeometryField = "shape";
                            pFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                            pFilter.Geometry = pGeometry;
                            pCursor = pFeatureLayer.Search(pFilter, false);


                            pFeature = pCursor.NextFeature();
                            if (pFeature != null)
                            {

                                int index = pFeature.Fields.FindField("FID");//要标注的字段的索引
                                string property = pFeature.get_Value(index).ToString();
                                this.listBox3D.Items.Clear();
                                listBox3D.Height = 20;
                                listBox3D.Width = 70;
                                listBox3D.Visible = true;
                                this.listBox3D.Location = new System.Drawing.Point((int)e.x - 30, (int)e.y - 30);
                                //if (listBox3D.Bottom > tabControl1.Height)
                                //{
                                //    this.listBox1.Location = new System.Drawing.Point((int)e.x + 5, (int)e.y + 5);//移动listbox的位置
                                //}
                                this.listBox3D.Items.Add(pFeatureLayer.FeatureClass.Fields.get_Field(0).AliasName + " = " + property);
                                //MessageBox.Show(property);
                                axSceneControl2.Scene.SceneGraph.RefreshViewers();
                                break;
                            }
                            else
                            {
                                listBox3D.Items.Clear();
                                listBox3D.Visible = false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(ex.ToString());
                    }
                    //.Refresh();
                    break;
            }
        }

        private void tCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
            axSceneControl2.MousePointer = esriControlsMousePointer.esriPointerArrow;
            scene = 0;
            axSceneControl2.Refresh();
        }

        private void timage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axSceneControl1.CurrentTool = null;
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
            try
            {
                string s_FileName;
                SaveFileDialog m_SaveFileDialog = new SaveFileDialog();
                m_SaveFileDialog.Title = "保存场景图片";
                m_SaveFileDialog.Filter = "JPEG Files | *.jpg";
                //m_SaveFileDialog.Filter = "BMP图片（*.bmp）｜*.bmp｜JPEG图片（*.jpg）｜*.jpg｜TIF图片（*.tif）｜*.tif";
                m_SaveFileDialog.ShowDialog();
                s_FileName = m_SaveFileDialog.FileName;

                if (xtraTabControl1.SelectedTabPageIndex == 1)
                {
                    axSceneControl1.SceneViewer.GetScreenShot(esri3DOutputImageType.JPEG, s_FileName);
                }
                else
                {
                    if (xtraTabControl1.SelectedTabPageIndex == 1)
                    {
                        axSceneControl2.SceneViewer.GetScreenShot(esri3DOutputImageType.JPEG, s_FileName);
                    }
                    else
                    {
                        MessageBox.Show("请切换至三维地图视图！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }

                MessageBox.Show("成功保存图片至:" + s_FileName, "管网系统消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("出现错误返回", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pointTDbt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            choice = 0;
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
            ICommand m_Command1 = new ControlsSceneSelectFeaturesTool();
            m_Command1.OnCreate(axSceneControl1.Object);
            this.axSceneControl1.CurrentTool = m_Command1 as ESRI.ArcGIS.SystemUI.ITool;
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
        }
        //三维属性查询
        private void attributebt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            choice = 0;
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
            scene = 1;
            Operation_3D(0, axSceneControl2);
            ThreeQuery TQ = new ThreeQuery(axSceneControl2);
            TQ.Show();
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
        }
        int m_pClick = 0;
        DateTime m_pStart, m_pEnd;
        //动画录制
        private void biAniRecord_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axSceneControl1.CurrentTool = null;
            choice = 0;
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
            scene = 1;
            Operation_3D(0, axSceneControl2);
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
            IAnimationTrack pAT = PipeLine.Helper.AnimationTracksHelper.CreateAnimationTrack(this.axSceneControl1.Scene, "Capture View");
            PipeLine.Helper.AnimationTracksHelper.CreateKeyFrame(this.axSceneControl1.Scene, pAT, "Capture View");

            if (this.biAniRecord.Caption == "动画录制")
            {
                m_pStart = DateTime.Now;
                this.biAniRecord.Caption = "录制结束";
            }
            else
            {
                this.biAniRecord.Caption = "动画录制";
                this.biAniPlay.Enabled = true;
                //this.biAniPause.Enabled = true;
                //this.biAniStop.Enabled = true;
                m_pEnd = DateTime.Now;
                context = true;
            }
            m_pClick++;
        }

        private void biAniPlay_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (context)
            {
                axSceneControl1.CurrentTool = null;
                choice = 0;
                axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
                scene = 1;
                Operation_3D(0, axSceneControl2);
                axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
                if (m_pClick % 2 == 0)
                {
                    TimeSpan sSpan = m_pEnd.Subtract(m_pStart);
                    double duration = sSpan.TotalSeconds;
                    PipeLine.Helper.AnimationTracksHelper.PlayAnimationTrack(this.axSceneControl1.Scene, duration);
                }
            }
            else
            {

                DevExpress.XtraEditors.XtraMessageBox.Show("未找到动画数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void excavatebt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axSceneControl1.CurrentTool = null;
            choice = 0;
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
            scene = 1;
            axSceneControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerCrosshair;
            Operation_3D(0, axSceneControl2);

            //_3Dexcavation = new _3DExcavation(axSceneControl1, my_point);
            //_3Dexcavation.Show();
            excavation3D = new Excavation3D(axSceneControl1, my_point);
            excavation3D.Show();
            mIGraphicsContainer3D = excavation3D.u;
            mm = 1;

        }

        private void axSceneControl1_OnMouseDown(object sender, ISceneControlEvents_OnMouseDownEvent e)
        {

            if (mm == 1)
            {
                my_number = excavation3D.vv;
                number = my_number;
            }

            switch (number)
            {
                case 1:

                    IPoint _locatePoint = new PointClass();
                    ISceneViewer _sceneView = axSceneControl1.SceneViewer;
                    object _owner;
                    object _object;
                    try
                    {

                        axSceneControl1.SceneGraph.Locate(_sceneView, e.x, e.y, esriScenePickMode.esriScenePickPlane, true, out _locatePoint, out _owner, out _object);
                    }

                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.ToString(), "管网系统信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    my_point.Add(_locatePoint);

                    //画跟踪线
                    if (my_point.Count > 1)
                    {
                        DrawLine myDrawline = new DrawLine();
                        IElement element = myDrawline.drawline(my_point[my_point.Count - 2], _locatePoint);
                        mIGraphicsContainer3D.AddElement(element);
                        axSceneControl1.SceneGraph.RefreshViewers();
                    }
                    break;
                case 2:
                    axSceneControl1.SceneGraph.IsNavigating = false;
                    IHit3DSet pHit3DSet = null;
                    axSceneControl1.SceneGraph.LocateMultiple(axSceneControl1.SceneGraph.ActiveViewer, e.x, e.y, esriScenePickMode.esriScenePickPlane, false, out pHit3DSet);

                    //pHit3DSet.OnePerLayer();
                    if (pHit3DSet == null)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("没有选中任何模型！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (pHit3DSet.Hits.Count == 0)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("当前点未能查找到任何要素", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (Application.OpenForms["property_3D"] == null)
                    {
                        PP_3D = new property_3D(axSceneControl1);
                    }
                    else
                    {
                        Application.OpenForms["property_3D"].Show();
                    }
                    PP_3D.refeshView(pHit3DSet);
                    PP_3D.Show();
                    break;
                case 3:
                    axSceneControl1.SceneGraph.IsNavigating = false;

                    IPoint _locatePoint3 = new PointClass();
                    ISceneViewer _sceneView3 = axSceneControl1.SceneViewer;
                    object _owner3;
                    object _object3;
                    try
                    {

                        axSceneControl1.SceneGraph.Locate(_sceneView3, e.x, e.y, esriScenePickMode.esriScenePickPlane, true, out _locatePoint3, out _owner3, out _object3);
                    }

                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.ToString(), "管网系统信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    IHit3DSet mHit3DSet = null;
                    IHit3D mHit3D = null;
                    IGraphicsLayer graphicsLayer;
                    IText3DElement text3DElement;
                    IGraphicsContainer3D graphicsContainer3D = null;
                    graphicsLayer = new GraphicsLayer3DClass();
                    text3DElement = new Text3DElementClass();
                    int count = 0;
                    axSceneControl1.SceneGraph.LocateMultiple(axSceneControl1.SceneGraph.ActiveViewer, e.x, e.y, esriScenePickMode.esriScenePickPlane, false, out mHit3DSet);
                    if (mHit3DSet == null & mHit3DSet.Hits.Count == 0)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("没有选中任何模型！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else
                    {
                        for (int i = 0; i < mHit3DSet.Hits.Count; i++)
                        {
                            mHit3D = mHit3DSet.Hits.get_Element(i) as IHit3D;
                            if (mHit3D.Owner is ILayer)
                            {
                                ILayer mLayer = mHit3D.Owner as ILayer;
                                string name = mLayer.Name;
                                if (mHit3D.Object != null & mHit3D.Object is IFeature)
                                {
                                    IFeature mFeature = mHit3D.Object as IFeature;
                                    if (mFeature.Fields.FieldCount > 5)
                                    {
                                        //AddText addText = new AddText();
                                        //addText.Init3DText(_locatePoint3,axSceneControl1);
                                        int index03 = mFeature.Fields.FindField("材质");
                                        string text = mFeature.get_Value(index03).ToString();
                                        count++;
                                        graphicsContainer3D = graphicsLayer as IGraphicsContainer3D;
                                        graphicsContainer3D.DeleteAllElements();
                                        text3DElement.FontName = "name";
                                        text3DElement.Text = "图层：" + name + "\n" + "材质:" + text;
                                        text3DElement.AnchorPoint = _locatePoint3;
                                        text3DElement.Depth = 0.1;
                                        text3DElement.Height = 0.5;
                                        text3DElement.BoldFont = true;
                                        text3DElement.RotationAngle = 360;
                                        ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbol();
                                        IRgbColor color = new RgbColorClass();
                                        color.Red = 255;
                                        simpleFillSymbol.Color = color;
                                        IFillShapeElement fillShapeElement = text3DElement as IFillShapeElement;
                                        fillShapeElement.Symbol = simpleFillSymbol as IFillSymbol;
                                        graphicsContainer3D.AddElement(text3DElement as IElement);
                                        text3DElement.Update();//这里如果不添加update就显示不出来了
                                        axSceneControl1.Scene.AddLayer(graphicsContainer3D as ILayer, false);
                                        //axSceneControl1.SceneGraph.Invalidate(graphicsLayer, false, false);
                                        axSceneControl1.SceneGraph.RefreshViewers();
                                        //MessageBox.Show(graphicsContainer3D.ElementCount.ToString());

                                    }
                                }
                            }
                            if (i == mHit3DSet.Hits.Count - 1 & count == 0)
                            {
                                MessageBox.Show("123");
                            }
                        }
                    }
                    break;
            }
        }
        //放大、缩小三维标注消失
        private void axSceneControl2_SizeChanged(object sender, EventArgs e)
        {
            listBox3D.Hide();
            axSceneControl2.Scene.ClearSelection();
            axSceneControl2.Scene.SceneGraph.RefreshViewers();
        }
        //三维标注
        private void tlablebt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axSceneControl1.CurrentTool = null;
            choice = 0;
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;

            Operation_3D(0, axSceneControl2);
            axSceneControl2.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerIdentify;
            number = 3;
        }

        private void CrossSectionbt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axSceneControl1.CurrentTool = null;
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
            choice = 12;
            pointCollection = new PolylineClass();
            polygonCollection = new PolygonClass();
        }

        private void contentUsbt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axSceneControl1.CurrentTool = null;
            AboutUs AU = new AboutUs();
            AU.Show();
        }

        private void aboutbt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axSceneControl1.CurrentTool = null;
            System.Diagnostics.Process.Start(Application.StartupPath.ToString() + "\\帮助文档.CHM");
        }

        public static void Operation_3D(int Opt, AxSceneControl axSceneControl)
        {
            int Operation_3D = Opt;
            switch (Operation_3D)
            {
                case 0:
                    ICommand i = new ControlsSceneSelectGraphicsTool();
                    i.OnCreate(axSceneControl.Object);
                    axSceneControl.CurrentTool = i as ESRI.ArcGIS.SystemUI.ITool;
                    axSceneControl.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerCrosshair;
                    break;
                case 1: //放大
                    ICommand m_Command_3D = new ControlsSceneZoomInTool();
                    m_Command_3D.OnCreate(axSceneControl.Object);
                    axSceneControl.CurrentTool = m_Command_3D as ESRI.ArcGIS.SystemUI.ITool;
                    break;
                case 2: //缩小
                    ICommand m_Command_3D_ZoomOut = new ControlsSceneZoomOutTool();
                    m_Command_3D_ZoomOut.OnCreate(axSceneControl.Object);
                    axSceneControl.CurrentTool = m_Command_3D_ZoomOut as ESRI.ArcGIS.SystemUI.ITool;
                    break;
                case 3: //平移
                    ICommand m_Command_3D_Pan = new ControlsScenePanTool();
                    m_Command_3D_Pan.OnCreate(axSceneControl.Object);
                    axSceneControl.CurrentTool = m_Command_3D_Pan as ESRI.ArcGIS.SystemUI.ITool;
                    break;
                case 4: //全图
                    //Get the scene graph
                    ISceneGraph pSceneGraph = axSceneControl.SceneGraph as ISceneGraph;
                    //Get the scene viewer's camera
                    ICamera pCamera = axSceneControl.Camera as ICamera;
                    //Position the camera to see the full extent of the scene graph
                    pCamera.SetDefaultsMBB(pSceneGraph.Extent);
                    //Redraw the scene viewer
                    pSceneGraph.ActiveViewer.Redraw(true);
                    break;
                case 5: //旋转
                    ICommand pSceneNav = new ControlsSceneNavigateToolClass();
                    pSceneNav.OnCreate(axSceneControl.Object);
                    axSceneControl.CurrentTool = pSceneNav as ITool;
                    break;
                case 6: //点选
                    //  ICommand m_Command1 = new ControlsSceneSelectFeaturesTool();
                    // ICommand m_Command1 = new ControlsSelectToolClass();
                    ICommand m_Command1 = new ControlsSceneNarrowFOVCommandClass();
                    m_Command1.OnCreate(axSceneControl.Object);
                    axSceneControl.CurrentTool = m_Command1 as ESRI.ArcGIS.SystemUI.ITool;
                    axSceneControl.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
                    break;
            }

        }
        //三维放大！
        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            choice = 0;
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
            scene = 0;
            //取消标注
            listBox3D.Items.Clear();
            listBox3D.Visible = false;
            Operation_3D(1, axSceneControl2);
            Operation_3D(1, axSceneControl1);
            axSceneControl2.Scene.SceneGraph.RefreshViewers();
        }
        //三维缩小
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            choice = 0;
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
            scene = 0;
            //取消标注
            listBox3D.Items.Clear();
            listBox3D.Visible = false;
            Operation_3D(2, axSceneControl2);
            Operation_3D(2, axSceneControl1);
            axSceneControl2.Scene.SceneGraph.RefreshViewers();
        }
        //三维平移
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            choice = 0;
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
            scene = 0;
            //取消标注
            listBox3D.Items.Clear();
            listBox3D.Visible = false;
            Operation_3D(3, axSceneControl2);
            Operation_3D(3, axSceneControl1);
            axSceneControl2.Scene.SceneGraph.RefreshViewers();
        }
        //三维全图
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            choice = 0;
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
            scene = 0;
            //取消标注
            listBox3D.Items.Clear();
            listBox3D.Visible = false;
            Operation_3D(4, axSceneControl1);
            Operation_3D(4, axSceneControl2);
            axSceneControl2.Scene.SceneGraph.RefreshViewers();
        }
        //三维旋转
        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            choice = 0;
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
            scene = 0;
            //取消标注
            listBox3D.Items.Clear();
            listBox3D.Visible = false;
            Operation_3D(5, axSceneControl1);
            Operation_3D(5, axSceneControl2);
            axSceneControl2.Scene.SceneGraph.RefreshViewers();
        }

        private void axSceneControl1_OnDoubleClick(object sender, ISceneControlEvents_OnDoubleClickEvent e)
        {
            if (my_point.Count > 2)
            {
                excavation3D.line_area();
            }
        }
        /// <summary>
        /// 拉线选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem3_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axMapControl1.CurrentTool = null;
            axSceneControl1.CurrentTool = null;
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
            choice = 13;
        }
        /// <summary>
        /// 矩形选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rectangle_Selection_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axMapControl1.CurrentTool = null;
            axSceneControl1.CurrentTool = null;
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
            choice = 14;
        }
        #region 快捷工具
        private void Arrow_quick_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerArrow;
            choice = 0;
            flag = 0;
        }

        private void ZoomIn_quick_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerZoomIn;
            choice = 1;
            flag = 1;
        }

        private void zoomOut_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerPageZoomOut;
            choice = 1;
            flag = 2;
        }

        private void Pan_quick_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerPagePan;
            choice = 1;
            flag = 3;
        }

        private void Extend_quick_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
            choice = 0;
            axMapControl1.Map.ClearSelection();
            axMapControl1.ActiveView.Refresh();
            axMapControl1.Extent = axMapControl1.FullExtent;
        }
        #endregion

        private void ribbonControl1_SelectedPageChanged(object sender, EventArgs e)
        {
            if (ribbonControl1.SelectedPage.Text == "三维管线")
            {
                xtraTabControl1.SelectedTabPageIndex = 1;
                axSceneControl1.CurrentTool = null;
            }
            if (ribbonControl1.SelectedPage.Text == "基本工具" | ribbonControl1.SelectedPage.Text == "管线查询" | ribbonControl1.SelectedPage.Text == "管线分析")
            {
                xtraTabControl1.SelectedTabPageIndex = 0;
            }
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (context)
            {
                try
                {
                    SaveFileDialog saveVedioFile = new SaveFileDialog();
                    saveVedioFile.Filter = "视频文件(*.avi)|*.avi";
                    saveVedioFile.Title = "输出AVI文件";
                    saveVedioFile.ShowDialog();
                    ISceneExporter3d p3DExporter = new AVIExporterClass();
                    p3DExporter.ExportFileName = saveVedioFile.FileName;
                    ISceneVideoExporter pExproter = p3DExporter as ISceneVideoExporter;
                    pExproter.Viewer = axSceneControl1.SceneGraph.ActiveViewer;
                    pExproter.VideoDuration = 2;
                    pExproter.FrameRate = 20;
                    IAVIExporter pAVIExporter = p3DExporter as IAVIExporter;
                    pAVIExporter.Quality = 100;
                    p3DExporter.ExportScene(axSceneControl1.Scene);

                    DevExpress.XtraEditors.XtraMessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
                catch (Exception)
                {

                    DevExpress.XtraEditors.XtraMessageBox.Show("保存失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    throw;
                }
            }
            else
            {


                DevExpress.XtraEditors.XtraMessageBox.Show("无动画数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        Boolean context = false;

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            context = false;
            PipeLine.Helper.AnimationTracksHelper.RemoveAnimationTrack(axSceneControl1.Scene, "Capture View");
        }
        public static IGeometricNetwork GetGeometricNetwork(ILayer layer)
        {
            IFeatureClass featureClass = ((IFeatureLayer)layer).FeatureClass;
            IFeatureDataset featureDataset = featureClass.FeatureDataset;
            INetworkCollection networkCollection = featureDataset as INetworkCollection;
            IGeometricNetwork geometricNetwork = networkCollection.get_GeometricNetwork(0);
            return geometricNetwork;
        }


        private void axSceneControl1_OnMouseMove(object sender, ISceneControlEvents_OnMouseMoveEvent e)
        {
            if (e.button == 4)
            {
                Operation_3D(3, axSceneControl1);
                axSceneControl2.Scene.SceneGraph.RefreshViewers();
            }
        }
        private void axSceneControl1_OnMouseUp(object sender, ISceneControlEvents_OnMouseUpEvent e)
        {
            if (e.button == 4)
            {
                Operation_3D(6, axSceneControl1);
                axSceneControl2.Scene.SceneGraph.RefreshViewers();
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="pFeatureClass"></param>
        /// <param name="name"></param>
        /// <param name="aliasName"></param>
        /// <param name="FieldType"></param>
        private void AddField(IFeatureClass pFeatureClass, string name, string aliasName, esriFieldType FieldType)
        {
            //若存在，则不需添加
            if (pFeatureClass.Fields.FindField(name) > -1) return;
            IField pField = new FieldClass();
            IFieldEdit pFieldEdit = pField as IFieldEdit;
            pFieldEdit.AliasName_2 = aliasName;
            pFieldEdit.Name_2 = name;
            pFieldEdit.Type_2 = FieldType;

            IClass pClass = pFeatureClass as IClass;
            pClass.AddField(pField);
        }

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
                IFeatureLayer mFeatureLayer = m_MapControl.Map.get_Layer(i) as IFeatureLayer;
                if (mFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
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
                            m_MapControl.Map.SelectFeature(layer, pFeature);
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
                                        dataRow[j] = "线";
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
            }
            //gridControl1.DataSource = dataTable;
            m_MapControl.Refresh();
            return dataTable;
        }


        private void ConnSde_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            connectionSDE = new ConnectionSDE(axMapControl1);
            connectionSDE.Show();
        }
        /// <summary>
        /// 连通性分析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Connection_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            choice = 0;
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
            List<string> lLayerName = new List<string>();
            if (!bar1.Visible)
            {
                bar1.Visible = true;
                for (int i = 0; i < axMapControl1.Map.LayerCount; i++)
                {
                    ILayer layer = axMapControl1.Map.get_Layer(i);
                    IFeatureLayer mFeatureLayer = axMapControl1.Map.get_Layer(i) as IFeatureLayer;
                    if (mFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                    {
                        if (layer.Name.ToString() == "铁路" | layer.Name.ToString() == "直埋电缆")
                        {
                            continue;
                        }
                        else
                        {
                            lLayerName.Add(layer.Name.ToString());
                        }
                    }

                }
                string[] Name_layer_burst = new string[lLayerName.Count];
                for (int i = 0; i < lLayerName.Count; i++)
                {
                    Name_layer_burst[i] = lLayerName[i];
                }
                this.repositoryItemComboBox1.Items.AddRange(Name_layer_burst);
            }
            else
            {
                return;
            }
          
        }

        private void xtraTabControl2_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTabControl2.SelectedTabPageIndex == 0)
            {
                xtraTabControl1.SelectedTabPageIndex = 0;
            }
            if (xtraTabControl2.SelectedTabPageIndex == 1)
            {
                xtraTabControl1.SelectedTabPageIndex = 1;
            }
            if (xtraTabControl2.SelectedTabPageIndex == 2)
            {
                xtraTabControl1.SelectedTabPageIndex = 2;
            }
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                xtraTabControl2.SelectedTabPageIndex = 0;
            }
            if (xtraTabControl1.SelectedTabPageIndex == 1)
            {
                xtraTabControl2.SelectedTabPageIndex = 1;
            }
            if (xtraTabControl1.SelectedTabPageIndex == 2)
            {
                xtraTabControl2.SelectedTabPageIndex = 2;
            }
        }

        private void axMapControl2_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            IRubberBand pBand = new RubberEnvelopeClass();

            IGeometry pGeometry = pBand.TrackNew(axMapControl2.ActiveView.ScreenDisplay, null);

            if (pGeometry.IsEmpty)
            {
                IPoint pPt = new PointClass();
                pPt.PutCoords(e.mapX, e.mapY);
                //改变主控件的视图范围
                axMapControl1.CenterAt(pPt);
            }
            else
            {
                axMapControl1.Extent = pGeometry.Envelope;
                axMapControl1.ActiveView.Refresh();

            }
        }
        private void MinPath_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
            bar1.Visible = true;
            choice = 0;

        }

        private void barButtonItem18_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            choice = 0;
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
            List<string> lLayerName = new List<string>();
            if (!bar1.Visible)
            {
                bar1.Visible = true;
                for (int i = 0; i < axMapControl1.Map.LayerCount; i++)
                {   
                    ILayer layer = axMapControl1.Map.get_Layer(i);
                    IFeatureLayer mFeatureLayer = axMapControl1.Map.get_Layer(i) as IFeatureLayer;
                    if (mFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                    {
                        if (layer.Name.ToString() == "铁路" | layer.Name.ToString() == "直埋电缆")
                        {
                            continue;
                        }
                        else
                        {
                            lLayerName.Add(layer.Name.ToString());
                        }
                    }
                    
                }
                string[] Name_layer_burst = new string[lLayerName.Count];
                for (int i = 0; i < lLayerName.Count; i++)
                {
                    Name_layer_burst[i] = lLayerName[i];
                }
                this.repositoryItemComboBox1.Items.AddRange(Name_layer_burst);
            }
            else
            {
                return;
            }
        }
        //设置起点和终点
        private void GetPoint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
            if (axMapControl1.LayerCount == 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("请加载数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            m_ipActiveView = axMapControl1.ActiveView;
            m_ipMap = m_ipActiveView.FocusMap;
            clicked = false;
            pGC = m_ipMap as IGraphicsContainer;
            ILayer ipLayer = m_ipMap.get_Layer(0);
            IFeatureLayer ipFeaturelayer = ipLayer as IFeatureLayer;
            IFeatureDataset ipFDS = ipFeaturelayer.FeatureClass.FeatureDataset;

            OpenFeatureDatasetNetwork(ipFDS);
            clicked = true;
            clickedcount = 0;
            pGC.DeleteAllElements();
            choice = 16;

        }
        //解决路径
        private void SolverPath_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
            if (SolvePathGan("长度"))
            {
                IPolyline ipPolyResult = PathPolyLine();
                clicked = false;
                DataTable dataTable = new DataTable();
                if (ipPolyResult.IsEmpty)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("没有路径可到", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    DataColumn dataColumn = new DataColumn();

                    mLayer = lLayer[1];
                    IFeatureLayer mFeatureLayer = mLayer as IFeatureLayer;
                    IFeatureClass mFeatureClass = mFeatureLayer.FeatureClass;
                    IQueryFilter m_QueryFilter = new QueryFilterClass();
                    for (int i = 0; i < index_Feature.Count; i++)
                    {
                        m_QueryFilter.WhereClause = "OBJECTID=" + index_Feature[i];
                        IFeatureCursor featureCursor = mFeatureClass.Search(m_QueryFilter, false);
                        IFeature Search_Featuer = featureCursor.NextFeature();
                        if (Search_Featuer != null)
                        {
                            while (dataTable.Columns.Count == 0)
                            {
                                for (int k = 0; k < Search_Featuer.Fields.FieldCount; k++)
                                {
                                    dataColumn = new DataColumn(mFeatureClass.Fields.get_Field(k).Name);
                                    dataTable.Columns.Add(dataColumn);
                                }
                            }
                            DataRow dataRow;
                            dataRow = dataTable.NewRow();
                            for (int j = 0; j < Search_Featuer.Fields.FieldCount; j++)
                            {
                                if (Search_Featuer.get_Value(j).ToString().Contains("System.__ComObject"))
                                {
                                    dataRow[j] = "线";
                                }
                                else
                                {
                                    dataRow[j] = Search_Featuer.get_Value(j).ToString();
                                }
                            }
                            dataTable.Rows.Add(dataRow);
                        }
                    }
                    if (Application.OpenForms["MinRoad"] == null)
                    {
                        minRoad = new MinRoad();
                        minRoad.textEdit1.Text = "找到路径" + "；" + " 路径经过" + m_ipEnumNetEID_Edges.Count + "条线；" + "所在图层：" + lLayer[0].Name;
                        minRoad.textEdit2.Text = m_ipPolyline.Length.ToString() + " 米";
                        minRoad.gridView1.BestFitColumns();
                        minRoad.gridControl1.DataSource = dataTable;
                        minRoad.Show();
                    }
                    else
                    {
                        minRoad.textEdit1.Text = "找到路径" + "；" + " 路径经过" + m_ipEnumNetEID_Edges.Count + "条线；" + "所在图层：" + lLayer[0].Name;
                        minRoad.textEdit2.Text = m_ipPolyline.Length.ToString() + " 米";
                        minRoad.gridView1.BestFitColumns();
                        minRoad.gridControl1.DataSource = dataTable;
                        minRoad.Show();
                    }
                    IRgbColor color = new RgbColorClass();
                    color.Red = 0;
                    color.Blue = 255;
                    color.Green = 0;
                    LineElementClass element = new LineElementClass();
                    ILineSymbol linesymbol = new SimpleLineSymbolClass();
                    linesymbol.Color = color as IColor;
                    linesymbol.Width = 3;
                    element.Geometry = m_ipPolyline;
                    element.Symbol = linesymbol;
                    pGC.AddElement(element, 0);
                    //object iSymbol = Type.Missing;
                    m_ipActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                    axMapControl1.FlashShape(ipPolyResult);
                    DevExpress.XtraEditors.XtraMessageBox.Show("路径经过" + m_ipEnumNetEID_Edges.Count + "条线" + "\r\n" + "经过节点数为" + (m_ipEnumNetEID_Junctions.Count - 1) + "\r\n" + "路线长度为" + m_ipPolyline.Length.ToString("#######.##") + "\r\n", "几何路径信息");
                }
            }

        }
        //缩放到图层
        private void ZoomToPath_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (m_ipPolyline.IsEmpty)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("当前没有执行路径查询！！！请确认！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                this.axMapControl1.Extent = m_ipPolyline.Envelope;
            }
        }
        //连通性取点
        private void getPoint_connection_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            choice = 17;
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
            if (axMapControl1.LayerCount == 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("请加载数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            m_ipActiveView = axMapControl1.ActiveView;
            m_ipMap = m_ipActiveView.FocusMap;
            clicked = false;
            pGC = m_ipMap as IGraphicsContainer;
            ILayer ipLayer = m_ipMap.get_Layer(0);
            IFeatureLayer ipFeaturelayer = ipLayer as IFeatureLayer;
            IFeatureDataset ipFDS = ipFeaturelayer.FeatureClass.FeatureDataset;

            OpenFeatureDatasetNetwork(ipFDS);
            clicked = true;
            clickedcount = 0;
            pGC.DeleteAllElements();

        }
        //判断连通性
        private void IsConncetion_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
            if (SolvePathGan_Connection("长度"))
            {
                IPolyline ipPolyResult = PathPolyLine();
                clicked = false;
                DataTable dataTable = new DataTable();
                if (ipPolyResult.IsEmpty)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("没有路径可到", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    IRgbColor color = new RgbColorClass();
                    color.Red = 0;
                    color.Blue = 255;
                    color.Green = 0;
                    LineElementClass element = new LineElementClass();
                    ILineSymbol linesymbol = new SimpleLineSymbolClass();
                    linesymbol.Color = color as IColor;
                    linesymbol.Width = 3;
                    element.Geometry = m_ipPolyline;
                    element.Symbol = linesymbol;
                    pGC.AddElement(element, 0);
                    //object iSymbol = Type.Missing;
                    m_ipActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                    axMapControl1.FlashShape(ipPolyResult);
                }
            }
        }

        /// <summary>
        /// 拾取爆管点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void getBurstPoint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axMapControl1.CurrentTool = null;
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
            if (axMapControl1.LayerCount == 0)
            {
                MessageBox.Show("请先加载几何网络数据！");
                return;
            }
            m_ipActiveView = axMapControl1.ActiveView;
            m_ipMap = m_ipActiveView.FocusMap;
            clicked = false;
            pGC = m_ipMap as IGraphicsContainer;
            ILayer ipLayer = m_ipMap.get_Layer(0);
            IFeatureLayer ipFeatureLayer = ipLayer as IFeatureLayer;
            IFeatureDataset ipFDS = ipFeatureLayer.FeatureClass.FeatureDataset;

            OpenFeatureDatasetNetwork(ipFDS);
            clicked = true;
            clickedcount = 0;
            pGC.DeleteAllElements();

            choice = 18;
        }


        //显示需要关闭的阀门
        private void display_Value_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
            //pGC.DeleteAllElements();
            if (SolvePathGan_burstPoint("长度"))//先解析路径
            {
                List<int> result_int = list_int();
                DataTable dataTable = new DataTable();
                clicked = false;
                if (result_int.Count == 0)
                {
                    MessageBox.Show("没有路径可到!!");
                }
                else
                {
                    ILayer layer;
                    for (int m = 0; m < axMapControl1.Map.LayerCount; m++)
                    {

                        string Name1 = axMapControl1.Map.get_Layer(m).Name.ToString();
                        string Name2 = LayerName.ToString().Substring(4, LayerName.ToString().Length - 4);
                        if (axMapControl1.Map.get_Layer(m).Name.ToString() == LayerName.ToString())
                        {
                            layer = axMapControl1.Map.get_Layer(m);
                            DataColumn dataColumn = new DataColumn();
                            mLayer = lLayer[0];
                            IFeatureLayer mFeatureLayer = layer as IFeatureLayer;
                            IFeatureClass mFeatureClass = mFeatureLayer.FeatureClass;
                            IQueryFilter m_QueryFilter = new QueryFilterClass();
                            for (int i = 0; i < index_Feature.Count; i++)
                            {
                                m_QueryFilter.WhereClause = "OBJECTID=" + index_Feature[i];
                                IFeatureCursor featureCursor = mFeatureClass.Search(m_QueryFilter, false);
                                IFeature Search_Feature = featureCursor.NextFeature();
                                if (Search_Feature != null)
                                {
                                    double proportion = Math.Round(axMapControl1.MapScale, 0);
                                    IPoint point = Search_Feature.Shape as IPoint;
                                    double radius = Math.Round(axMapControl1.MapScale, 0) / 600;
                                    //DrawCircle_Graphics(axMapControl1, point, 1);
                                    IPictureMarkerSymbol pPicMarkerSymbol = new PictureMarkerSymbolClass();
                                    string pPicturePath = Application.StartupPath + @"\pic\关阀分析.gif";
                                    pPicMarkerSymbol.CreateMarkerSymbolFromFile(esriIPictureType.esriIPictureGIF, pPicturePath);
                                    //double size = proportion / 80;
                                    pPicMarkerSymbol.Size = 15;
                                    IMarkerElement pMarkerElement = new MarkerElementClass();
                                    IElement ppElement;
                                    ppElement = pMarkerElement as IElement;
                                    double proportion_Line = Math.Round(axMapControl1.MapScale, 0);

                                    clickedcount++;
                                    ppElement.Geometry = point;
                                    pMarkerElement.Symbol = pPicMarkerSymbol;
                                    pGC.AddElement(ppElement, 0);

                                    //axMapControl1.Map.SelectFeature(layer, Search_Feature);
                                    while (dataTable.Columns.Count == 0)
                                    {
                                        for (int k = 0; k < Search_Feature.Fields.FieldCount; k++)
                                        {
                                            dataColumn = new DataColumn(mFeatureClass.Fields.get_Field(k).Name);
                                            dataTable.Columns.Add(dataColumn);
                                        }
                                    }
                                    DataRow dataRow;
                                    dataRow = dataTable.NewRow();
                                    for (int j = 0; j < Search_Feature.Fields.FieldCount; j++)
                                    {
                                        if (Search_Feature.get_Value(j).ToString().Contains("System.__ComObject"))
                                        {
                                            dataRow[j] = "点";
                                        }
                                        else
                                        {
                                            dataRow[j] = Search_Feature.get_Value(j).ToString();
                                        }
                                    }
                                    dataTable.Rows.Add(dataRow);//Datagridview
                                }
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                    if (Application.OpenForms["BurstPointAnalysis"] == null)
                    {
                        burstPointAnalysis = new BurstPointAnalysis();
                        burstPointAnalysis.layerInformation.Text = lLayer[0].Name;
                        burstPointAnalysis.burstPointLocaion.Text = "X：" + lPoint[0].X + "；Y：" + lPoint[0].Y;
                        burstPointAnalysis.gridControl1.DataSource = dataTable;
                        burstPointAnalysis.gridView1.BestFitColumns();
                        burstPointAnalysis.Show();
                    }
                    axMapControl1.Refresh();
                }


            }

        }

        #region 几何网络分析
        //关闭工作空间
        private void CloseWorkspace()
        {
            m_ipGeometricNetwork = null;
            m_ipPoints = null;
            lPoint.Clear();
            m_ipPointToEID = null;
            m_ipEnumNetEID_Junctions = null;
            m_ipEnumNetEID_Edges = null;
            m_ipPolyline = null;
            index_Feature.Clear();
            lLayer.Clear();
            pGC.DeleteAllElements();
        }
        private bool InitializeNetworkAndMap(IFeatureDataset FeatureDataset)
        {
            //IFeatureClassContainer ipFeatureClassContainer;
            //IFeatureClass ipFeatureClass;
            IGeoDataset ipGeoDataset;
            ILayer ipLayer;
            IFeatureLayer ipFeatureLayer;
            IEnvelope ipEnvelope, ipMaxEnvelope;
            double dblSearchTol;
            INetworkCollection ipNetworkCollection = FeatureDataset as INetworkCollection;
            int count = ipNetworkCollection.GeometricNetworkCount;
            //获取第一个几何网络工作空间
            m_ipGeometricNetwork = ipNetworkCollection.get_GeometricNetwork(0);//获取第一个几何网络
            //MessageBox.Show(FeatureDataset.Name);
            INetwork ipNetwork = m_ipGeometricNetwork.Network;
            count = m_ipMap.LayerCount;
            ipMaxEnvelope = new EnvelopeClass();
            for (int i = 0; i < count; i++)
            {
                ipLayer = m_ipMap.get_Layer(i);
                ipFeatureLayer = ipLayer as IFeatureLayer;
                ipGeoDataset = ipFeatureLayer as IGeoDataset;
                ipEnvelope = ipGeoDataset.Extent;
                ipMaxEnvelope.Union(ipEnvelope);
            }
            m_ipPointToEID = new PointToEIDClass() as IPointToEID;
            m_ipPointToEID.SourceMap = m_ipMap;
            m_ipPointToEID.GeometricNetwork = m_ipGeometricNetwork;
            double dblWidth = ipMaxEnvelope.Width;
            double dblHeight = ipMaxEnvelope.Height;
            if (dblWidth > dblHeight)
                dblSearchTol = dblWidth / 100;
            else
                dblSearchTol = dblHeight / 100;
            m_ipPointToEID.SnapTolerance = dblSearchTol;
            return true;
        }
        public void OpenFeatureDatasetNetwork(IFeatureDataset FeatureDataset)
        {
            CloseWorkspace();
            if (!InitializeNetworkAndMap(FeatureDataset))
            //Console.WriteLine("打开network出错");
            {
                MessageBox.Show("打开network出错");
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="WeightName"></param>
        /// <returns></returns>
        public bool SolvePathGan(string WeightName)
        {
            try
            {
                int intJunctionUserClassID;
                int intJunctionUserID;
                int intJunctionUserSubID;
                int intJunctionID;
                IPoint ipFoundJunctionPoint;
                ITraceFlowSolverGEN ipTraceFlowSolver = new TraceFlowSolver() as ITraceFlowSolverGEN;
                INetSolver ipNetSolver = ipTraceFlowSolver as INetSolver;
                if (m_ipGeometricNetwork == null)
                {
                    return false;
                }

                IFeatureLayer pFeatureLayer = lLayer[0] as IFeatureLayer;
                IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
                //IFeatureClass featureClass = ((IFeatureLayer)layer).FeatureClass;
                IFeatureDataset featureDataset = pFeatureClass.FeatureDataset;
                INetworkCollection networkCollection = featureDataset as INetworkCollection;
                IGeometricNetwork geometricNetwork = networkCollection.get_GeometricNetwork(0);
                INetwork ipNetwork = geometricNetwork.Network;
                ipNetSolver.SourceNetwork = ipNetwork;

                INetElements ipNetElements = ipNetwork as INetElements;
                if (lPoint.Count == 0)
                {
                    MessageBox.Show("请选择点!!");
                    return false;
                }
                int intCount = lPoint.Count;//这里的points有值吗？
                ////定义一个边线旗数组
                //*********最近点***************//////////
                IJunctionFlag[] pJunctionFlagList = new JunctionFlag[intCount];
                for (int i = 0; i < intCount; i++)
                {
                    INetFlag ipNetFlag = new JunctionFlag() as INetFlag;
                    IPoint ipJunctionPoint = lPoint[i];
                    //查找输入点的最近的网络点
                    m_ipPointToEID.GetNearestJunction(ipJunctionPoint, out intJunctionID, out ipFoundJunctionPoint);
                    ipNetElements.QueryIDs(intJunctionID, esriElementType.esriETJunction, out intJunctionUserClassID, out intJunctionUserID, out intJunctionUserSubID);
                    ipNetFlag.UserClassID = intJunctionUserClassID;
                    ipNetFlag.UserID = intJunctionUserID;
                    ipNetFlag.UserSubID = intJunctionUserSubID;
                    IJunctionFlag pTemp = (IJunctionFlag)(ipNetFlag as IJunctionFlag);
                    pJunctionFlagList[i] = pTemp;
                }
                ipTraceFlowSolver.PutJunctionOrigins(ref pJunctionFlagList);
                INetSchema ipNetSchema = ipNetwork as INetSchema;
                INetWeight ipNetWeight = ipNetSchema.get_WeightByName(WeightName);
                INetSolverWeights ipNetSolverWeights = ipTraceFlowSolver as INetSolverWeights;
                ipNetSolverWeights.FromToEdgeWeight = ipNetWeight;//开始边线的权重
                ipNetSolverWeights.ToFromEdgeWeight = ipNetWeight;//终止边线的权重
                object[] vaRes = new object[intCount - 1];
                //通过findpath得到边线和交汇点的集合
                ipTraceFlowSolver.FindPath(esriFlowMethod.esriFMConnected,
                esriShortestPathObjFn.esriSPObjFnMinSum, out m_ipEnumNetEID_Junctions, out m_ipEnumNetEID_Edges, intCount - 1, ref vaRes);
                m_ipPolyline = null;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }


        }

        //返回路径的几何体
        public IPolyline PathPolyLine()
        {
            IEIDInfo ipEIDInfo;
            IGeometry ipGeometry;
            if (m_ipPolyline != null)
                return m_ipPolyline;
            m_ipPolyline = new PolylineClass();
            IGeometryCollection ipNewGeometryColl = m_ipPolyline as IGeometryCollection;//引用传递
            ISpatialReference ipSpatialReference = m_ipMap.SpatialReference;
            IEIDHelper ipEIDHelper = new EIDHelper();
            ipEIDHelper.GeometricNetwork = m_ipGeometricNetwork;

            ipEIDHelper.OutputSpatialReference = ipSpatialReference;
            ipEIDHelper.ReturnGeometries = true;
            ipEIDHelper.ReturnFeatures = true;
            //IEnumEIDInfo ipEnumEIDInfo = ipEIDHelper.CreateEnumEIDInfo(m_ipEnumNetEID_Edges);
            IEnumEIDInfo ipEnumEIDInfo = ipEIDHelper.CreateEnumEIDInfo(m_ipEnumNetEID_Edges);
            int count = ipEnumEIDInfo.Count;
            ipEnumEIDInfo.Reset();


            for (int i = 0; i < count; i++)
            {
                axMapControl1.Map.ClearSelection();
                ipEIDInfo = ipEnumEIDInfo.Next();
                index_Feature.Add((int)(ipEIDInfo.Feature.OID));
                ipGeometry = ipEIDInfo.Geometry;
                lGeometry.Add(ipGeometry);
                ipNewGeometryColl.AddGeometryCollection(ipGeometry as IGeometryCollection);
            }
            return m_ipPolyline;
        }

        public bool SolvePathGan_Connection(string WeightName)
        {
            try
            {
                int intJunctionUserClassID;
                int intJunctionUserID;
                int intJunctionUserSubID;
                int intJunctionID;
                IPoint ipFoundJunctionPoint;
                ITraceFlowSolverGEN ipTraceFlowSolver = new TraceFlowSolver() as ITraceFlowSolverGEN;
                INetSolver ipNetSolver = ipTraceFlowSolver as INetSolver;
                if (m_ipGeometricNetwork == null)
                {
                    return false;
                }

                IFeatureLayer pFeatureLayer = lLayer[0] as IFeatureLayer;
                IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
                //IFeatureClass featureClass = ((IFeatureLayer)layer).FeatureClass;
                IFeatureDataset featureDataset = pFeatureClass.FeatureDataset;
                INetworkCollection networkCollection = featureDataset as INetworkCollection;
                IGeometricNetwork geometricNetwork = networkCollection.get_GeometricNetwork(0);
                INetwork ipNetwork = geometricNetwork.Network;
                ipNetSolver.SourceNetwork = ipNetwork;

                INetElements ipNetElements = ipNetwork as INetElements;
                if (lPoint.Count == 0)
                {
                    MessageBox.Show("请选择点!!");
                    return false;
                }
                int intCount = lPoint.Count;//这里的points有值吗？
                ////定义一个边线旗数组
                //*********最近点***************//////////
                IJunctionFlag[] pJunctionFlagList = new JunctionFlag[intCount];
                for (int i = 0; i < intCount; i++)
                {
                    INetFlag ipNetFlag = new JunctionFlag() as INetFlag;
                    IPoint ipJunctionPoint = lPoint[i];
                    //查找输入点的最近的网络点
                    m_ipPointToEID.GetNearestJunction(ipJunctionPoint, out intJunctionID, out ipFoundJunctionPoint);
                    ipNetElements.QueryIDs(intJunctionID, esriElementType.esriETJunction, out intJunctionUserClassID, out intJunctionUserID, out intJunctionUserSubID);
                    ipNetFlag.UserClassID = intJunctionUserClassID;
                    ipNetFlag.UserID = intJunctionUserID;
                    ipNetFlag.UserSubID = intJunctionUserSubID;
                    IJunctionFlag pTemp = (IJunctionFlag)(ipNetFlag as IJunctionFlag);
                    pJunctionFlagList[i] = pTemp;
                }
                ipTraceFlowSolver.PutJunctionOrigins(ref pJunctionFlagList);
                INetSchema ipNetSchema = ipNetwork as INetSchema;
                INetWeight ipNetWeight = ipNetSchema.get_WeightByName(WeightName);
                INetSolverWeights ipNetSolverWeights = ipTraceFlowSolver as INetSolverWeights;
                ipNetSolverWeights.FromToEdgeWeight = ipNetWeight;//开始边线的权重
                ipNetSolverWeights.ToFromEdgeWeight = ipNetWeight;//终止边线的权重
                object[] vaRes = new object[intCount - 1];
                //通过findpath得到边线和交汇点的集合
                ipTraceFlowSolver.FindFlowElements(esriFlowMethod.esriFMConnected, esriFlowElements.esriFEEdges
                    , out m_ipEnumNetEID_Junctions, out m_ipEnumNetEID_Edges);
                m_ipPolyline = null;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }


        }

        public bool SolvePathGan_burstPoint(string WeightName)
        {
            try
            {
                int intJunctionUserClassID;
                int intJunctionUserID;
                int intJunctionUserSubID;
                int intJunctionID;
                IPoint ipFoundJunctionPoint;
                ITraceFlowSolverGEN ipTraceFlowSolver = new TraceFlowSolver() as ITraceFlowSolverGEN;
                INetSolver ipNetSolver = ipTraceFlowSolver as INetSolver;
                if (m_ipGeometricNetwork == null)
                {
                    return false;
                }

                IFeatureLayer pFeatureLayer = lLayer[0] as IFeatureLayer;
                IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
                //IFeatureClass featureClass = ((IFeatureLayer)layer).FeatureClass;
                IFeatureDataset featureDataset = pFeatureClass.FeatureDataset;
                INetworkCollection networkCollection = featureDataset as INetworkCollection;
                IGeometricNetwork geometricNetwork = networkCollection.get_GeometricNetwork(0);
                INetwork ipNetwork = geometricNetwork.Network;
                ipNetSolver.SourceNetwork = ipNetwork;

                INetElements ipNetElements = ipNetwork as INetElements;
                if (lPoint.Count == 0)
                {
                    MessageBox.Show("请选择点!!");
                    return false;
                }
                int intCount = lPoint.Count;//这里的points有值吗？
                ////定义一个边线旗数组
                //*********最近点***************//////////
                IJunctionFlag[] pJunctionFlagList = new JunctionFlag[intCount];
                for (int i = 0; i < intCount; i++)
                {
                    INetFlag ipNetFlag = new JunctionFlag() as INetFlag;
                    IPoint ipJunctionPoint = lPoint[i];
                    //查找输入点的最近的网络点
                    m_ipPointToEID.GetNearestJunction(ipJunctionPoint, out intJunctionID, out ipFoundJunctionPoint);
                    ipNetElements.QueryIDs(intJunctionID, esriElementType.esriETJunction, out intJunctionUserClassID, out intJunctionUserID, out intJunctionUserSubID);
                    ipNetFlag.UserClassID = intJunctionUserClassID;
                    ipNetFlag.UserID = intJunctionUserID;
                    ipNetFlag.UserSubID = intJunctionUserSubID;
                    IJunctionFlag pTemp = (IJunctionFlag)(ipNetFlag as IJunctionFlag);
                    pJunctionFlagList[i] = pTemp;
                }
                ipTraceFlowSolver.PutJunctionOrigins(ref pJunctionFlagList);
                INetSchema ipNetSchema = ipNetwork as INetSchema;
                INetWeight ipNetWeight = ipNetSchema.get_WeightByName(WeightName);
                INetSolverWeights ipNetSolverWeights = ipTraceFlowSolver as INetSolverWeights;
                ipNetSolverWeights.FromToEdgeWeight = ipNetWeight;//开始边线的权重
                ipNetSolverWeights.ToFromEdgeWeight = ipNetWeight;//终止边线的权重
                object[] vaRes = new object[intCount - 1];

                ipTraceFlowSolver.FindFlowElements(esriFlowMethod.esriFMUpstream, esriFlowElements.esriFEJunctions
                    , out m_ipEnumNetEID_Junctions, out m_ipEnumNetEID_Edges);

                m_ipPolyline = null;
                return true;
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        //返回路径的几何体
        public List<int> list_int()
        {
            IEIDInfo ipEIDInfo;
            m_ipPolyline = new PolylineClass();
            IGeometryCollection ipNewGeometryColl = m_ipPolyline as IGeometryCollection;//引用传递
            ISpatialReference ipSpatialReference = m_ipMap.SpatialReference;
            IEIDHelper ipEIDHelper = new EIDHelper();
            ipEIDHelper.GeometricNetwork = m_ipGeometricNetwork;

            ipEIDHelper.OutputSpatialReference = ipSpatialReference;
            ipEIDHelper.ReturnGeometries = true;
            ipEIDHelper.ReturnFeatures = true;
            //IEnumEIDInfo ipEnumEIDInfo = ipEIDHelper.CreateEnumEIDInfo(m_ipEnumNetEID_Edges);
            IEnumEIDInfo ipEnumEIDInfo = ipEIDHelper.CreateEnumEIDInfo(m_ipEnumNetEID_Junctions);
            int count = ipEnumEIDInfo.Count;
            ipEnumEIDInfo.Reset();


            for (int i = 0; i < count; i++)
            {
                axMapControl1.Map.ClearSelection();
                ipEIDInfo = ipEnumEIDInfo.Next();
                index_Feature.Add((int)(ipEIDInfo.Feature.OID));
                IFeature feature = ipEIDInfo.Feature;
                LayerName = feature.Class.AliasName;
            }

            return index_Feature;
        }


        //根据RGB值得到IColor
        public static IRgbColor getColor(int R, int G, int B)
        {
            IRgbColor color = new RgbColorClass();
            color.Red = R;
            color.Green = G;
            color.Blue = B;
            return color;
        }

        public void DrawCircle_Graphics(AxMapControl axMapControl, IPoint pPoint, double radius)
        {
            //定义颜色
            IRgbColor pColor = getColor(255, 0, 0);
            //pColor.RGB = System.Drawing.Color.FromArgb(255, 0, 0).ToArgb();
            pColor.Transparency = 255;
            ILineSymbol pLineSymbol = new SimpleLineSymbolClass();
            pLineSymbol.Width = 2;
            pLineSymbol.Color = pColor;

            pColor = getColor(0, 0, 255);
            pColor.Transparency = 1;
            IFillSymbol pFillSymbol = new SimpleFillSymbolClass();
            pFillSymbol.Color = pColor;
            pFillSymbol.Outline = pLineSymbol;
            IConstructCircularArc pConstructCircularArc = new CircularArcClass();
            pConstructCircularArc.ConstructCircle(pPoint, radius, false);
            //circularArc.PutCoordsByAngle(origin, Math.PI / 3, Math.PI / 2, 100);
            ICircularArc pArc = pConstructCircularArc as ICircularArc;
            //pArc.PutCoordsByAngle(pPoint, Math.PI / -2, Math.PI / 2, 1);
            ISegment pSegment = pArc as ISegment;
            ISegmentCollection pSegmentCollection = new RingClass();
            object obj = Type.Missing;
            pSegmentCollection.AddSegment(pSegment, ref obj, ref obj);

            IRing pRing = pSegmentCollection as IRing;
            pRing.Close();

            IGeometryCollection pGeometryColl = new PolygonClass();
            pGeometryColl.AddGeometry(pRing, ref obj, ref obj);
            IElement pElement = new CircleElementClass();
            pElement.Geometry = pGeometryColl as IGeometry;

            IFillShapeElement pFillShapeElment = pElement as IFillShapeElement;
            pFillShapeElment.Symbol = pFillSymbol;
            IGraphicsContainer pGC = axMapControl.ActiveView.GraphicsContainer;
            pGC.AddElement(pElement, 0);
            //axMapControl.Refresh();
        }

        #endregion
        //流向分析
        private void FlowAnalysis_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            choice = 0;
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
            List<string> lLayerName = new List<string>();
            if (!bar1.Visible)
            {
                bar1.Visible = true;
                for (int i = 0; i < axMapControl1.Map.LayerCount; i++)
                {
                    ILayer layer = axMapControl1.Map.get_Layer(i);
                    IFeatureLayer mFeatureLayer = axMapControl1.Map.get_Layer(i) as IFeatureLayer;
                    if (mFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                    {
                        if (layer.Name.ToString() == "铁路" | layer.Name.ToString() == "直埋电缆")
                        {
                            continue;
                        }
                        else
                        {
                            lLayerName.Add(layer.Name.ToString());
                        }
                    }

                }
                string[] Name_layer_burst = new string[lLayerName.Count];
                for (int i = 0; i < lLayerName.Count; i++)
                {
                    Name_layer_burst[i] = lLayerName[i];
                }
                this.repositoryItemComboBox1.Items.AddRange(Name_layer_burst);
            }
            else
            {
                return;
            }
        }
        //x显示流向
        private void displayFolw_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string FlowLayer = burstlayerName.EditValue.ToString();
            FlowDirectionAnalysis flowDirectionAnalysis = new FlowDirectionAnalysis();
            flowDirectionAnalysis.ShowFlowDirection(axMapControl1.Map, FlowLayer);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Dayofweek dayofweek = new Dayofweek();

            DateTime dt = DateTime.Now.ToLocalTime();
            string da24 = dt.ToString("yyyy-MM-dd HH:mm:ss");
            currenTime.Caption = "时间：" + da24 + " " + dayofweek.Week();
            //waiting.Show(this);
        }
        /// <summary>
        /// 火灾抢险分析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FireDisaster_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axMapControl1.CurrentTool = null;
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
            //fireContion = new FireConditon(axMapControl1);
            //fireContion.Show();
            if (Application.OpenForms["FireConditon"] == null)
            {
                fireContion = new FireConditon(axMapControl1);
                fireContion.Show();
            }
            else
            {
                Application.OpenForms["FireConditon"].Show();
            }
            choice = 19;
        }
        //框选
        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axMapControl1.CurrentTool = null;
            choice = 20;
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
        }
        //圆形选择
        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axMapControl1.CurrentTool = null;
            choice = 21;
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
        }

        private void PointQuery_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axSceneControl1.CurrentTool = null;
            axMapControl1.CurrentTool = null;
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerArrowQuestion;
            choice = 6;
        }

        private void attributeQuery_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axMapControl1.CurrentTool = null;
            axSceneControl1.CurrentTool = null;
            choice = 0; 
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
            if (SMBA == null || SMBA.IsDisposed)
            {
                SMBA = new SearchMapByAttribution(axMapControl1);
                SMBA.Show();
            }
            else
            {
                SMBA.Activate();
            }
            
        }

        private void coordinatePosition_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axMapControl1.CurrentTool = null;
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
            choice = 22;
        }


    }
}
