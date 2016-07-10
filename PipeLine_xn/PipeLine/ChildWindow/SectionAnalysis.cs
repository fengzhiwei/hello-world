using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;

namespace PipeLine.ChildWindow
{
    public partial class SectionAnalysis : DevExpress.XtraEditors.XtraForm
    {
        public List<IFeature> lFeature = new List<IFeature>();
        public IPointCollection mainPointCollection;
        private AxMapControl MainMapControl = new AxMapControl();
        private DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
        private DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
        private DevExpress.XtraCharts.BubbleSeriesLabel bubbleSeriesLabel1 = new DevExpress.XtraCharts.BubbleSeriesLabel();
        private DevExpress.XtraCharts.BubbleSeriesView bubbleSeriesView1 = new DevExpress.XtraCharts.BubbleSeriesView();//显示气泡标签
        // private DevExpress.XtraCharts.BubbleSeriesView bubbleSeriesView1 = new DevExpress.XtraCharts.BubbleSeriesView();
        private DevExpress.XtraCharts.ChartTitle chartTitle1 = new DevExpress.XtraCharts.ChartTitle();
        double maxradius = 0.1;
        double minradius = 0.8;
        //double s_depth;
        //double e_depth;
        //double Depth;
        double radius = 0;
        public SectionAnalysis(main m_main)
        {
            InitializeComponent();
            mainPointCollection = m_main.getMainPointCollection();//返回折线的点集合
            MainMapControl = m_main.getMainAxMapControl(); //返回主窗体地图控件
            chartControl1.BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(bubbleSeriesView1)).BeginInit();
            //x轴
            xyDiagram1.AxisX.WholeRange.AutoSideMargins = true;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisX.Title.Alignment = System.Drawing.StringAlignment.Far;
            xyDiagram1.AxisX.Title.Text = "距离/m";
            xyDiagram1.AxisX.Title.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            xyDiagram1.AxisX.Title.Visible = true;
            //y轴
            xyDiagram1.AxisY.WholeRange.AutoSideMargins = true;
            xyDiagram1.AxisY.Title.Alignment = System.Drawing.StringAlignment.Near;
            xyDiagram1.AxisY.Title.Text = "埋深/m";
            xyDiagram1.AxisY.Title.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            xyDiagram1.AxisY.Title.Visible = true;
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.Reverse = true;

            xyDiagram1.DefaultPane.BorderVisible = false;
            xyDiagram1.DefaultPane.Shadow.Visible = true;

            chartControl1.BackColor = System.Drawing.Color.Azure;
            chartControl1.Diagram = xyDiagram1;
            chartControl1.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Left;
            chartControl1.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.BottomOutside;
            chartControl1.Legend.EquallySpacedItems = false;
            chartControl1.Legend.Shadow.Visible = true;
            chartControl1.Location = new System.Drawing.Point(0, 0);
            bubbleSeriesLabel1.Position = DevExpress.XtraCharts.BubbleLabelPosition.Outside;
            chartTitle1.Text = "横断面图：  距离:埋深:外径";
            chartControl1.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle1});
            bubbleSeriesLabel1.LineVisible = true;
            bubbleSeriesLabel1.Angle = 27;

            bubbleSeriesView1.Color = System.Drawing.Color.Red;


            series1.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Numerical;
            series1.Name = "管线";

            series1.View = bubbleSeriesView1;
            series1.Label = bubbleSeriesLabel1;
            chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] { series1 };

            ((System.ComponentModel.ISupportInitialize)(bubbleSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            chartControl1.EndInit();
        }

        public void xiangjiao1(IGeometry geometry)
        {
            MainMapControl.Map.ClearSelection();
            IFeatureCursor pFeatureCursor;
            ILayer layer = null;
            for (int i = 0; i < MainMapControl.Map.LayerCount; i++)
            {
                IFeatureLayer mFeatureLayer = MainMapControl.Map.get_Layer(i) as IFeatureLayer;
                if (mFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                {
                    layer = MainMapControl.Map.get_Layer(i);
                    if (layer.Visible)
                    {
                        IFeatureLayer pFeatureLayer = layer as IFeatureLayer;
                        IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
                        ISpatialFilter pSpatialFilter = new SpatialFilterClass();
                        pSpatialFilter.Geometry = geometry;
                        pSpatialFilter.GeometryField = pFeatureClass.ShapeFieldName;
                        pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                        IFields pFields = pFeatureClass.Fields;
                        pFeatureCursor = pFeatureClass.Search(pSpatialFilter, false);
                        IFeature pFeature = pFeatureCursor.NextFeature();
                        if (pFeature == null)
                        {
                            continue;
                        }
                        if (pFeature.Class.AliasName == "直埋电缆" | pFeature.Class.AliasName == "铁路")
                        {
                            continue;
                        }
                        while (pFeature != null)
                        {
                            lFeature.Add(pFeature);
                            MainMapControl.Map.SelectFeature(layer, pFeature);
                            bjgx(pFeature);
                            pFeature = pFeatureCursor.NextFeature();
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            
            bubbleSeriesView1.MaxSize = maxradius;
            bubbleSeriesView1.MinSize = minradius;
            MainMapControl.Refresh();
        }
        public void bjgx(IFeature feature)
        {
            int index_depth = feature.Fields.FindField("埋深");
            double depth = Convert.ToDouble(feature.get_Value(index_depth)); 

            int j = feature.Fields.FindField("外径");//直径字段
            radius = Convert.ToDouble(feature.get_Value(j)) / 1000;//管径是直径吗？外径
            if (radius > maxradius)
            {
                maxradius = radius;
            }
            if (radius < minradius)
            {
                minradius = radius;
            }
            int x1 = feature.Fields.FindField("端点X1");
            double X1 = Convert.ToDouble(feature.get_Value(x1));
            int y1 = feature.Fields.FindField("端点Y1");
            double Y1 = Convert.ToDouble(feature.get_Value(y1));
            int x2 = feature.Fields.FindField("端点X2");
            double X2 = Convert.ToDouble(feature.get_Value(x2));
            int y2 = feature.Fields.FindField("端点Y2");
            double Y2 = Convert.ToDouble(feature.get_Value(y2));
            Double distance = getDistance(ContructPoint2D(X1, Y1), ContructPoint2D(X2, Y2));

            AddseriesPoint(distance, depth ,radius);

            this.Show();
        }
        private Double getDistance(IPoint pt1, IPoint pt2)
        {
            double x1 = pt1.X;
            double y1 = pt1.Y;
            double x2 = pt2.X;
            double y2 = pt2.Y;
            if (x1 - x2 == 0)
            {
                double x3 = mainPointCollection.get_Point(0).X;
                double y3 = mainPointCollection.get_Point(0).Y;
                double x4 = mainPointCollection.get_Point(1).X;
                double y4 = mainPointCollection.get_Point(1).Y;
                // MessageBox.Show(x3.ToString());
                double k2 = (y3 - y4) / (x3 - x4);
                double b2 = y3 - k2 * x3;
                double X = x1;
                double Y = k2 * x1 + b2;
                double distance2 = (X - x3) * (X - x3) + (Y - y3) * (Y - y3);
                double distance = Math.Sqrt(distance2);
                return distance;
            }
            else
            {
                double k1 = (y1 - y2) / (x1 - x2);
                double b1 = y1 - k1 * x1;
                //截面线的坐标
                double x3 = mainPointCollection.get_Point(0).X;
                double y3 = mainPointCollection.get_Point(0).Y;
                double x4 = mainPointCollection.get_Point(1).X;
                double y4 = mainPointCollection.get_Point(1).Y;
                double k2 = (y3 - y4) / (x3 - x4);
                double b2 = y3 - k2 * x3;
                //交点坐标
                double X = (b2 - b1) / (k1 - k2);
                double Y = k1 * X + b1;

                //距起点的水平距离
                double distance2 = (X - x3) * (X - x3) + (Y - y3) * (Y - y3);
                double distance = Math.Sqrt(distance2);
                return distance;
            }
            //埋深
            //s_depth起始点埋深
            //终止点埋深
            // double S2 = (x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2);
            // double s2 = (x1 - X) * (x1 - X) + (y1 - Y) * (y1 - Y);
            // double K = Math.Sqrt(s2) / Math.Sqrt(S2);
            //// Depth = K * (s_depth - e_depth) + s_depth;
        }
        public void AddseriesPoint(Double pArgument, double depth, double radius)
        {
            DevExpress.XtraCharts.SeriesPoint seriesPoint1 = new DevExpress.XtraCharts.SeriesPoint(pArgument, new object[] {
            ((object)(depth)),((object)(radius))});
            series1.Points.AddRange(new DevExpress.XtraCharts.SeriesPoint[] { seriesPoint1 });

        }
        /// <summary>
        /// 二维点的实例化
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static IPoint ContructPoint2D(double x, double y)
        {
            IPoint point = new PointClass();
            point.PutCoords(x, y);
            return point;

        }

        private void SectionAnalysis_FormClosed(object sender, FormClosedEventArgs e)
        {
            main.choice = 0;
        }
    }
}