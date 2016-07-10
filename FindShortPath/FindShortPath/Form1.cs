using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.NetworkAnalysis;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Display;

namespace FindShortPath
{
    public partial class Form1 : Form
    {

        private IActiveView m_ipActiveView;
        private IMap m_ipMap;//地图控件中地图
        private IGraphicsContainer pGC;//图形对象
        private bool clicked = false;
        int clickedcount = 0;
        private double m_dblPathCost = 0;
        private IGeometricNetwork m_ipGeometricNetwork;
        private IPointCollection m_ipPoints;//输入点集合
        private IPointToEID m_ipPointToEID;
        private IEnumNetEID m_ipEnumNetEID_Junctions;
        private IEnumNetEID m_ipEnumNetEID_Edges;
        private IPolyline m_ipPolyline;
        private IMapControl3 mapctrlMainMap = null;
        private ILayer mLayer;
        List<ILayer> lLayer = new List<ILayer>();
        Connection m_Connection;
        MinRoad minroad;

        public List<int> index_Feature = new List<int>();
        public List<IFeature> lFeature = new List<IFeature>();
        public List<IGeometry> lGeometry = new List<IGeometry>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string MapPath = @"F:\钢企\\Geometry_Net\geo_Network.mxd";
            axMapControl1.LoadMxFile(MapPath);

            mapctrlMainMap = (IMapControl3)this.axMapControl1.Object;
            m_ipActiveView = axMapControl1.ActiveView;
            m_ipMap = m_ipActiveView.FocusMap;
            clicked = false;
            pGC = m_ipMap as IGraphicsContainer;
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

        //关闭工作空间
        private void CloseWorkspace()
        {
            m_ipGeometricNetwork = null;
            m_ipPoints = null;
            m_ipPointToEID = null;
            m_ipEnumNetEID_Junctions = null;
            m_ipEnumNetEID_Edges = null;
            m_ipPolyline = null;
            index_Feature.Clear();
            lLayer.Clear();
        }
        //初始化几何网络和地图
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
                if (m_ipPoints == null)
                {
                    MessageBox.Show("请选择点!!");
                    return false;
                }
                int intCount = m_ipPoints.PointCount;//这里的points有值吗？
                ////定义一个边线旗数组
                //*********最近点***************//////////
                IJunctionFlag[] pJunctionFlagList = new JunctionFlag[intCount];
                for (int i = 0; i < intCount; i++)
                {
                    INetFlag ipNetFlag = new JunctionFlag() as INetFlag;
                    IPoint ipJunctionPoint = m_ipPoints.get_Point(i);
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
                //ipTraceFlowSolver.FindPath(esriFlowMethod.esriFMConnected,
                //esriShortestPathObjFn.esriSPObjFnMinSum, out m_ipEnumNetEID_Junctions, out m_ipEnumNetEID_Edges, intCount - 1, ref vaRes);
                //ipTraceFlowSolver.FindPath(esriFlowMethod.esriFMConnected,
                //    esriShortestPathObjFn.esriSPObjFnMinSum, out m_ipEnumNetEID_Junctions, out m_ipEnumNetEID_Edges, intCount - 1, ref vaRes);
                //ipTraceFlowSolver.FindPath(esriFlowMethod.esriFMConnected, 
                //    esriShortestPathObjFn.esriSPObjFnMinSum, out m_ipEnumNetEID_Junctions, out m_ipEnumNetEID_Edges, intCount - 1, ref vaRes);
                ipTraceFlowSolver.FindFlowElements(esriFlowMethod.esriFMConnected,esriFlowElements.esriFEEdges
                    ,out m_ipEnumNetEID_Junctions,out m_ipEnumNetEID_Edges);
                //m_dblPathCost = 0;
                //for (int i = 0; i < vaRes.Length; i++)
                //{
                //    double m_Va = (double)vaRes[i];
                //    m_dblPathCost = m_dblPathCost + m_Va;
                //}
                m_ipPolyline = null;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }


        }
        //设置起点和终点
        private void Findpath_Click(object sender, EventArgs e)
        {
            mapctrlMainMap.CurrentTool = null;

            //设置鼠标样式
            axMapControl1.MouseIcon = new Icon("flag_test.ico");
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerCustom;


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
        }
        private void SolverPath_Click(object sender, EventArgs e)
        {
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;

            if (SolvePathGan("长度"))//先解析路径
            {
                IPolyline ipPolyResult = PathPolyLine();//最后返回最短路径
                clicked = false;
                DataTable dataTable = new DataTable();
                if (ipPolyResult.IsEmpty)
                {
                    MessageBox.Show("没有路径可到!!");
                }
                else
                {
                    IGeometry tt = ipPolyResult as IGeometry;
                    //axMapControl1.Map.SelectByShape(tt, null, false);
                    DataColumn dataColumn = new DataColumn();
                    mLayer = lLayer[0];
                    IFeatureLayer mFeatureLayer = mLayer as IFeatureLayer;
                    IFeatureClass mFeatureClass = mFeatureLayer.FeatureClass;
                    IQueryFilter m_QueryFilter = new QueryFilterClass();
                    for (int i = 0; i < index_Feature.Count; i++)
                    {
                        m_QueryFilter.WhereClause = "OBJECTID=" + index_Feature[i];
                        IFeatureCursor featureCursor = mFeatureClass.Search(m_QueryFilter, false);
                        IFeature Search_Feature = featureCursor.NextFeature();
                        if (Search_Feature != null)
                        {
                            //MessageBox.Show(Search_Feature.get_Value(4).ToString());
                            //axMapControl1.Map.SelectFeature(mLayer, Search_Feature);
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
                                    dataRow[j] = "线";
                                }
                                else
                                {
                                    dataRow[j] = Search_Feature.get_Value(j).ToString();
                                }
                            }
                            dataTable.Rows.Add(dataRow);//Datagridview
                        }
                    }
                    if (Application.OpenForms["MinRoad"] == null)
                    {
                        minroad = new MinRoad();
                        minroad.textEdit1.Text = "找到路径" + "；" + " 路径经过" + m_ipEnumNetEID_Edges.Count + "条线；" + "所在图层：" + lLayer[0].Name;
                        minroad.textEdit2.Text = m_ipPolyline.Length.ToString() + " 米";
                        minroad.gridView1.BestFitColumns();
                        minroad.gridControl1.DataSource = dataTable;
                        //minroad.groupControl2.Text = lLayer[0].Name.ToString();
                        minroad.Show();

                    }
                    else
                    {
                        minroad.textEdit1.Text = "找到路径" + "；" + " 路径经过" + m_ipEnumNetEID_Edges.Count + "条线；" + "所在图层：" + lLayer[0].Name;
                        minroad.textEdit2.Text = m_ipPolyline.Length.ToString() + " 米";
                        minroad.gridView1.BestFitColumns();
                        minroad.gridControl1.DataSource = dataTable;
                        //minroad.groupControl2.Text = lLayer[0].Name.ToString();
                        Application.OpenForms["MinRoad"].Show();
                    }
                    minroad.Show();
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

                    
                    //axMapControl1.Map.SelectByShape(ipPolyResult, null, false);
                    //MessageBox.Show("路径经过" + m_ipEnumNetEID_Edges.Count + "条线" + "\r\n" + "经过节点数为" + (m_ipEnumNetEID_Junctions.Count - 1) + "\r\n" + "路线长度为" + m_ipPolyline.Length.ToString("#######.##") + "\r\n", "几何路径信息");
                }
            }

        }

        private void SuoFang_Click(object sender, EventArgs e)
        {
            if (m_ipPolyline == null)
            {
                MessageBox.Show("当前没有执行路径查询！！！请确认！");
            }
            else
            {
                this.axMapControl1.Extent = m_ipPolyline.Envelope;
            }

        }


        private void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            IPoint ipNew = new PointClass();
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
            //ipNew = m_ipActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
            object o = Type.Missing;
            m_ipPoints.AddPoint(ipNew, ref o, ref o);

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
            m_ipActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);

        }





    }
}
