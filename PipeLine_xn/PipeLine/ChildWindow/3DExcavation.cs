using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using PipeLine.Class;

namespace PipeLine.ChildWindow
{
    public partial class _3DExcavation : Form
    {
        private AxSceneControl axSceneControl1;

        public IGraphicsContainer3D multiPatchGraphicsContainer3D_2 = null;
        private IGraphicsContainer3D multiPatchGraphicsContainer3D_1;
        private static object _missing = Type.Missing;
        public static List<IPoint> my_point;
        public int my_number = 0;

        public _3DExcavation(AxSceneControl axSceneControl, List<IPoint> myPoint)
        {
            InitializeComponent();
            this.axSceneControl1 = axSceneControl;

            my_point = myPoint;
            multiPatchGraphicsContainer3D_1 = ConstructGraphicsLayer3D("Multipath_1");
            multiPatchGraphicsContainer3D_2 = ConstructGraphicsLayer3D("Multipath_2");
            axSceneControl1.Scene.AddLayer(multiPatchGraphicsContainer3D_1 as ILayer, true);
            axSceneControl1.Scene.AddLayer(multiPatchGraphicsContainer3D_2 as ILayer, true);
            axSceneControl1.SceneGraph.RefreshViewers();


            a = ConstructPoint3D(62692.1056828161, 58831.0795524003, 2304.65);
            b = ConstructPoint3D(65356.0088348861, 58935.5876811114, 2304.65);
            c = ConstructPoint3D(62768.8527131470, 60463.8958344146, 2304.65);
            d = ConstructPoint3D(60440.5321620861, 60440.5321620861, 2304.65);

            // IGeometry geometry = GetFanGeometry(a, b, c, d);
            IGeometry geometry = GetRingGroupeGeometry(a, b, c, d);
            IElement element = ConstructMultiPatchElement(geometry, 255, 216, 216, 201);
            // multiPatchGraphicsContainer3D.DeleteAllElements();
            multiPatchGraphicsContainer3D_1.AddElement(element);

            int tt = axSceneControl.Scene.LayerCount;
            axSceneControl.Scene.get_Layer(tt - 1).Visible = false;
            my_point.Clear();
            axSceneControl1.SceneGraph.RefreshViewers();


        }
        /// <summary>
        /// 设定子窗口中的变量属性，用于主窗口中调用
        /// </summary>
        public IGraphicsContainer3D u
        {
            set { this.multiPatchGraphicsContainer3D_2 = value; }
            get { return this.multiPatchGraphicsContainer3D_2; }
        }
        public int vv
        {
            set { my_number = value; }
            get { return my_number; }

        }

        /// <summary>
        /// GraphicsContainer3D实例化方法
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>

        public static IGraphicsContainer3D ConstructGraphicsLayer3D(string name)
        {
            IGraphicsContainer3D GraphicsContainer3D = new GraphicsLayer3DClass();
            ILayer layer = GraphicsContainer3D as ILayer;
            layer.Name = name;
            return GraphicsContainer3D;
        }
        /// <summary>
        /// 创建一个带有三维坐标的点
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        private static IPoint ConstructPoint3D(double x, double y, double z)
        {
            IPoint point = new PointClass();
            point.PutCoords(x, y);
            point.Z = z;
            return point;

        }
        /// <summary>
        /// 多面组合成体
        /// </summary>
        /// <returns></returns>
        public static IGeometry GetFanGeometry(IPoint a, IPoint b, IPoint c, IPoint d)
        {
            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();
            IMultiPatch multiPatch = multiPatchGeometryCollection as IMultiPatch;

            IPointCollection triangleFanPointConllection = new TriangleFanClass();
            triangleFanPointConllection.AddPoint(a, ref _missing, ref _missing);
            triangleFanPointConllection.AddPoint(b, ref _missing, ref _missing);
            triangleFanPointConllection.AddPoint(c, ref _missing, ref _missing);
            triangleFanPointConllection.AddPoint(d, ref _missing, ref _missing);

            multiPatchGeometryCollection.AddGeometry(triangleFanPointConllection as IGeometry, ref _missing, ref _missing);
            multiPatch.PutRingType(triangleFanPointConllection as IRing, esriMultiPatchRingType.esriMultiPatchOuterRing);

            return multiPatchGeometryCollection as IGeometry;

        }
        /// <summary>
        /// 将geometry转变带有symbol的Element的对象
        /// </summary>
        /// <param name="geometry"></param>
        /// <param name="tans">透明度</param>
        /// <returns></returns>
        public static IElement ConstructMultiPatchElement(IGeometry geometry, int tans, int r, int g, int b)
        {
            ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbolClass();
            simpleFillSymbol.Color = GetRGBColor(r, g, b, tans);
            IElement element = new MultiPatchElementClass();
            element.Geometry = geometry;

            IFillShapeElement fillShapeElement = element as IFillShapeElement;
            fillShapeElement.Symbol = simpleFillSymbol as IFillSymbol;
            return element;


        }
        /// <summary>
        /// 创建一个color对象
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        private static IRgbColor GetRGBColor(int r, int g, int b, int t)
        {
            IRgbColor rgbColor = new RgbColorClass();
            rgbColor.Red = r;
            rgbColor.Green = g;
            rgbColor.Blue = b;
            rgbColor.Transparency = (byte)t;
            return rgbColor;
        }
        /// <summary>
        /// 线的起始点与终止点相连
        /// </summary>
        public void line_area()
        {
            try
            {

                my_number = 0;
                DrawLine myDrawline = new DrawLine();
                IElement element = myDrawline.drawline(my_point[my_point.Count - 1], my_point[0]);
                multiPatchGraphicsContainer3D_2.AddElement(element);
                // DrawFan(multiPatchGraphicsContainer3D_1);
                axSceneControl1.SceneGraph.RefreshViewers();

            }
            catch (Exception ex)
            {
                MessageBox.Show("请选择开挖区域", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        //画面
        private void button1_Click(object sender, EventArgs e)
        {
            line_area();
            Excavation_area();
            IGeometry geometry = GetRingGroupeGeometry(ConstructPoint3D(62692.1056828161, 58831.0795524003, 2304.65),
            ConstructPoint3D(65356.0088348861, 58935.5876811114, 2304.65),
            ConstructPoint3D(62768.8527131470, 60463.8958344146, 2304.65),
             ConstructPoint3D(60440.5321620861, 60440.5321620861, 2304.65));
            IElement element = ConstructMultiPatchElement(geometry, 255, 216, 216, 201);
            multiPatchGraphicsContainer3D_1.DeleteAllElements();
            DrawFan(multiPatchGraphicsContainer3D_1);
            multiPatchGraphicsContainer3D_1.AddElement(element);
            axSceneControl1.SceneGraph.RefreshViewers();

        }
        IPoint a = null, b = null, c = null, d = null;
        double deep2, deep3;
        private void DrawFan(IGraphicsContainer3D multiPatchGraphicsContainer3D_1)
        {
            double deep = double.Parse(textBox1.Text);
            double deep1 = 2304.8 - deep;
            deep2 = deep;
            deep3 = deep1;

            // multiPatchGraphicsContainer3D_2.DeleteAllElements();
            for (int i = 0; i < my_point.Count - 1; i++)
            {

                //listBox1.Items.Add(myPoint[i].X);



                a = ConstructPoint3D(my_point[i].X, my_point[i].Y, 2304.8);
                b = ConstructPoint3D(my_point[i + 1].X, my_point[i + 1].Y, 2304.8);
                c = ConstructPoint3D(my_point[i + 1].X, my_point[i + 1].Y, deep1);
                d = ConstructPoint3D(my_point[i].X, my_point[i].Y, deep1);

                IGeometry geometry1 = GetFanGeometry(a, b, c, d);

                IElement element1 = ConstructMultiPatchElement(geometry1, 255, 135, 134, 126);

                //if (i == 0)
                //    multiPatchGraphicsContainer3D_2.DeleteAllElements();

                multiPatchGraphicsContainer3D_1.AddElement(element1);
                // axSceneControl1.SceneGraph.RefreshViewers();


            }
            a = ConstructPoint3D(my_point[0].X, my_point[0].Y, 2304.65);
            b = ConstructPoint3D(my_point[my_point.Count - 1].X, my_point[my_point.Count - 1].Y, 2304.8);
            c = ConstructPoint3D(my_point[my_point.Count - 1].X, my_point[my_point.Count - 1].Y, deep1);
            d = ConstructPoint3D(my_point[0].X, my_point[0].Y, deep1);
            IGeometry geometry2 = GetFanGeometry(a, b, c, d);
            IElement element2 = ConstructMultiPatchElement(geometry2, 255, 135, 134, 126);
            // multiPatchGraphicsContainer3D_2.DeleteAllElements();
            multiPatchGraphicsContainer3D_1.AddElement(element2);
        }

        /// <summary>
        /// 挖坑
        /// </summary>
        static double w = 2304.65, t = 200, m = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                w = w - 0.5;
                // t = t - 2;

                m = m + 0.5;

                if (double.Parse(textBox1.Text) < m || double.Parse(textBox1.Text) == m)
                    button2.Enabled = false;
                Excavation_area();

                if (m < deep2)
                {
                    label6.Text = m.ToString();
                }
                else
                {
                    label6.Text = deep2.ToString();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("请输入开挖深度", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        /// <summary>
        /// 鼠标点击的开挖区域
        /// </summary>
        private void Excavation_area()
        {
            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();
            IMultiPatch multiPatch = multiPatchGeometryCollection as IMultiPatch;
            IPointCollection triangleFanPointConllection = new TriangleFanClass();
            for (int i = 0; i < my_point.Count; i++)
            {
                triangleFanPointConllection.AddPoint(ConstructPoint3D(my_point[i].X, my_point[i].Y, w - 0.5 > deep3 ? w - 0.5 : deep3), ref _missing, ref _missing);
            }
            multiPatchGeometryCollection.AddGeometry(triangleFanPointConllection as IGeometry, ref _missing, ref _missing);
            multiPatch.PutRingType(triangleFanPointConllection as IRing, esriMultiPatchRingType.esriMultiPatchOuterRing);
            IGeometry geometry = multiPatchGeometryCollection as IGeometry;
            IArea3D mArea3D = geometry as IArea3D;
            double calculatedArea3D = mArea3D.Area3D;
            MessageBox.Show(calculatedArea3D.ToString());
            IElement element = ConstructMultiPatchElement(geometry, 255, 216, 216, 201);
            multiPatchGraphicsContainer3D_2.DeleteAllElements();
            multiPatchGraphicsContainer3D_2.AddElement(element);

            axSceneControl1.SceneGraph.RefreshViewers();

            //IArea3D area3D = multiPatchGeometry asIArea3D;
            //   double calculatedArea3D = area3D.Area3D;


        }
        /// <summary>
        /// 恢复开挖的坑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {

            label6.Text = "";
            m = 0;
            button2.Enabled = true;
            w = 2304.65;
            t = 255;
            my_point.Clear();
            IGeometry geometry = GetRingGroupeGeometry(ConstructPoint3D(62692.1056828161, 58831.0795524003, 2304.65),
            ConstructPoint3D(65356.0088348861, 58935.5876811114, 2304.65),
            ConstructPoint3D(62768.8527131470, 60463.8958344146, 2304.65),
             ConstructPoint3D(60440.5321620861, 60440.5321620861, 2304.65));
            IElement element = ConstructMultiPatchElement(geometry, 255, 216, 216, 201);
            multiPatchGraphicsContainer3D_1.DeleteAllElements();
            multiPatchGraphicsContainer3D_2.DeleteAllElements();
            multiPatchGraphicsContainer3D_1.AddElement(element);
            axSceneControl1.SceneGraph.RefreshViewers();

            axSceneControl1.SceneGraph.RefreshViewers();



        }

        private void button4_Click(object sender, EventArgs e)
        {
            my_number = 1;
            //  my_point.Clear();
        }

        private void _3DExcavation_FormClosed(object sender, FormClosedEventArgs e)
        {

            axSceneControl1.Scene.DeleteLayer(multiPatchGraphicsContainer3D_1 as ILayer);
            axSceneControl1.Scene.DeleteLayer(multiPatchGraphicsContainer3D_2 as ILayer);
            int tt = axSceneControl1.Scene.LayerCount;
            axSceneControl1.Scene.get_Layer(tt - 1).Visible = true;
            axSceneControl1.SceneGraph.RefreshViewers();


            label6.Text = "";
            m = 0;
            button2.Enabled = true;
            w = 2304.65;
            t = 255;
            my_point.Clear();
            IGeometry geometry = GetRingGroupeGeometry(ConstructPoint3D(62692.1056828161, 58831.0795524003, 2304.65),
            ConstructPoint3D(65356.0088348861, 58935.5876811114, 2304.65),
            ConstructPoint3D(62768.8527131470, 60463.8958344146, 2304.65),
             ConstructPoint3D(60440.5321620861, 60440.5321620861, 2304.65));
            IElement element = ConstructMultiPatchElement(geometry, 255, 216, 216, 201);
            multiPatchGraphicsContainer3D_1.DeleteAllElements();
            multiPatchGraphicsContainer3D_2.DeleteAllElements();
            multiPatchGraphicsContainer3D_1.AddElement(element);
            axSceneControl1.SceneGraph.RefreshViewers();

            axSceneControl1.SceneGraph.RefreshViewers();
        }
        public static IGeometry GetRingGroupeGeometry(IPoint pt1, IPoint pt2, IPoint pt3, IPoint pt4)
        {

            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();

            IMultiPatch multiPatch = multiPatchGeometryCollection as IMultiPatch;

            //Exterior Ring 1

            IPointCollection exteriorRing1PointCollection = new RingClass();
            exteriorRing1PointCollection.AddPoint(pt1, ref _missing, ref _missing);
            exteriorRing1PointCollection.AddPoint(pt2, ref _missing, ref _missing);
            exteriorRing1PointCollection.AddPoint(pt3, ref _missing, ref _missing);
            exteriorRing1PointCollection.AddPoint(pt4, ref _missing, ref _missing);

            IRing exteriorRing1 = exteriorRing1PointCollection as IRing;
            exteriorRing1.Close();

            multiPatchGeometryCollection.AddGeometry(exteriorRing1 as IGeometry, ref _missing, ref _missing);

            multiPatch.PutRingType(exteriorRing1, esriMultiPatchRingType.esriMultiPatchOuterRing);

            //Interior Ring 1
            if (my_point.Count > 0)
            {

                IPointCollection interiorRing1PointCollection = new RingClass();
                for (int i = 0; i < my_point.Count; i++)
                {
                    interiorRing1PointCollection.AddPoint(my_point[i], ref _missing, ref _missing);
                }
                IRing interiorRing1 = interiorRing1PointCollection as IRing;
                interiorRing1.Close();

                multiPatchGeometryCollection.AddGeometry(interiorRing1 as IGeometry, ref _missing, ref _missing);

                multiPatch.PutRingType(interiorRing1, esriMultiPatchRingType.esriMultiPatchInnerRing);

            }


            return multiPatchGeometryCollection as IGeometry;
        }
    }




}
