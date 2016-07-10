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
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using PipeLine.Class;

namespace PipeLine.ChildWindow
{
    public partial class Excavation3D : DevExpress.XtraEditors.XtraForm
    {
        private AxSceneControl axSceneControl1;
        public IGraphicsContainer3D multiPatchGraphicsContainer3D_2 = null;
        private IGraphicsContainer3D multiPatchGraphicsContainer3D_1;
        private static object _missing = Type.Missing;
        public static List<IPoint> my_point;
        public int my_number = 0;
        public Excavation3D(AxSceneControl axSceneControl, List<IPoint> myPoint)
        {
            InitializeComponent();
            this.axSceneControl1 = axSceneControl;
            textEdit2.Properties.ReadOnly = true;
            textEdit3.Properties.ReadOnly = true;
            textEdit4.Properties.ReadOnly = true;
            my_point = myPoint;
            multiPatchGraphicsContainer3D_1 = ConstructGraphicsLayer3D("Multipath_1");
            multiPatchGraphicsContainer3D_2 = ConstructGraphicsLayer3D("Multipath_2");
            axSceneControl1.Scene.AddLayer(multiPatchGraphicsContainer3D_1 as ILayer, true);
            axSceneControl1.Scene.AddLayer(multiPatchGraphicsContainer3D_2 as ILayer, true);
            axSceneControl1.SceneGraph.RefreshViewers();


            a = ConstructPoint3D(62733.8882059418, 58848.1146717463, 2304.65);
            b = ConstructPoint3D(62815.2230118052, 60455.2745644757, 2304.65);
            c = ConstructPoint3D(65151.8726836331, 60138.2233402643, 2304.65);
            d = ConstructPoint3D(65196.3520363917, 58987.952687912, 2304.65);

           
            IGeometry geometry = GetRingGroupeGeometry(a, b, c, d);
            IElement element = ConstructMultiPatchElement(geometry, 255, 216, 216, 201);
         
            multiPatchGraphicsContainer3D_1.AddElement(element);

            for (int i = 0; i < axSceneControl.Scene.LayerCount; i++)
            {
                if (axSceneControl.Scene.get_Layer(i).Name == "底图" || axSceneControl.Scene.get_Layer(i).Name == "道路")
                {
                    axSceneControl.Scene.get_Layer(i).Visible = false;
                } 


            }

            my_point.Clear();
            axSceneControl1.SceneGraph.RefreshViewers();
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
        }
        ///
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
              
                axSceneControl1.SceneGraph.RefreshViewers();

            }
            catch (Exception ex)
            {
                MessageBox.Show("请选择开挖区域", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        //画面

        IPoint a = null, b = null, c = null, d = null;
        double deep2, deep3;
        private void DrawFan(IGraphicsContainer3D multiPatchGraphicsContainer3D_1)
        {
            try
            {
                if (my_point.Count > 2)
                {
                    double deep = double.Parse(textBox1.Text);
                    double deep1 = 2304.8 - deep;
                    deep2 = deep;
                    deep3 = deep1;

                    // multiPatchGraphicsContainer3D_2.DeleteAllElements();
                    for (int i = 0; i < my_point.Count - 1; i++)
                    {
                        a = ConstructPoint3D(my_point[i].X, my_point[i].Y, 2304.8);
                        b = ConstructPoint3D(my_point[i + 1].X, my_point[i + 1].Y, 2304.8);
                        c = ConstructPoint3D(my_point[i + 1].X, my_point[i + 1].Y, deep1);
                        d = ConstructPoint3D(my_point[i].X, my_point[i].Y, deep1);

                        IGeometry geometry1 = GetFanGeometry(a, b, c, d);

                        IElement element1 = ConstructMultiPatchElement(geometry1, 255, 135, 134, 126);
                        multiPatchGraphicsContainer3D_1.AddElement(element1);
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
                else
                {
                    MessageBox.Show("所选点的数目必须大于3个，才能构成封闭区域");
                    button2.Enabled = false;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        static double w = 2304.65, t = 200, m = 0;

        /// <summary>
        /// 鼠标点击的开挖区域
        /// </summary>
        IGeometry geometry;
        private int Excavation_area()
        {
            try
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
                geometry = multiPatchGeometryCollection as IGeometry;
                //MessageBox.Show(calculatedArea3D.ToString());
                IElement element = ConstructMultiPatchElement(geometry, 255, 216, 216, 201);
                multiPatchGraphicsContainer3D_2.DeleteAllElements();
                multiPatchGraphicsContainer3D_2.AddElement(element);
                axSceneControl1.SceneGraph.RefreshViewers();
                int D = 1;
                return D;
            }
            catch (Exception)
            {
                int d = 0;
                return d;
                throw;
            }

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
        //选择开挖区域
        private void button4_Click(object sender, EventArgs e)
        {
            button4.Enabled = false;
            button1.Enabled = true;
            button3.Enabled = true;
            my_number = 1;
        }
        //确定开挖区域,并挖坑
        private void button1_Click(object sender, EventArgs e)
        {


            line_area();
            int i = Excavation_area();
            if (i == 1)
            {
                button1.Enabled = false;
                button2.Enabled = true;
                textEdit4.Text = calculate_area().ToString("0.00") + "平方米";
                IGeometry geometry = GetRingGroupeGeometry(ConstructPoint3D(62733.8882059418, 58848.1146717463, 2304.65),
            ConstructPoint3D(62815.2230118052, 60455.2745644757, 2304.65), ConstructPoint3D(65151.8726836331, 60138.2233402643, 2304.65), ConstructPoint3D(65196.3520363917, 58987.952687912, 2304.65));
                IElement element = ConstructMultiPatchElement(geometry, 255, 216, 216, 201);
                multiPatchGraphicsContainer3D_1.DeleteAllElements();
                DrawFan(multiPatchGraphicsContainer3D_1);
                multiPatchGraphicsContainer3D_1.AddElement(element);
                axSceneControl1.SceneGraph.RefreshViewers();
            }

        }
        /// <summary>
        /// 开挖
        /// </summary>
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
                    textEdit2.Text = m.ToString();
                    textEdit3.Text = (m * calculate_area()).ToString("0.00") + "立方米";
                }
                else
                {
                    button2.Enabled = false;
                    button3.Enabled = true;
                    textEdit2.Text = deep2.ToString();
                    textEdit3.Text = (deep2 * calculate_area()).ToString("0.00") + "立方米";
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("请输入开挖深度", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        //恢复开挖区域 
        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            button4.Enabled = true;
            button2.Enabled = false;
            textEdit3.Text = "";
            textEdit4.Text = "";
            textEdit2.Text = "";
            m = 0;
            w = 2304.65;
            t = 255;
            my_point.Clear();
            IGeometry geometry = GetRingGroupeGeometry(ConstructPoint3D(62733.8882059418, 58848.1146717463, 2304.65),
            ConstructPoint3D(62815.2230118052, 60455.2745644757, 2304.65), ConstructPoint3D(65151.8726836331, 60138.2233402643, 2304.65), ConstructPoint3D(65196.3520363917, 58987.952687912, 2304.65));
            IElement element = ConstructMultiPatchElement(geometry, 255, 216, 216, 201);
            multiPatchGraphicsContainer3D_1.DeleteAllElements();
            multiPatchGraphicsContainer3D_2.DeleteAllElements();
            multiPatchGraphicsContainer3D_1.AddElement(element);
            axSceneControl1.SceneGraph.RefreshViewers();

            axSceneControl1.SceneGraph.RefreshViewers();

        }
        //窗体关闭事件！
        private void Excavation3D_FormClosed(object sender, FormClosedEventArgs e)
        {
            axSceneControl1.Scene.DeleteLayer(multiPatchGraphicsContainer3D_1 as ILayer);
            axSceneControl1.Scene.DeleteLayer(multiPatchGraphicsContainer3D_2 as ILayer);
            int tt = axSceneControl1.Scene.LayerCount;
            axSceneControl1.Scene.get_Layer(tt - 1).Visible = true;
            axSceneControl1.SceneGraph.RefreshViewers();
            textEdit2.Text = "";
            m = 0;
            button2.Enabled = true;
            w = 2304.65;
            t = 255;
            my_point.Clear();
            IGeometry geometry = GetRingGroupeGeometry(ConstructPoint3D(62733.8882059418, 58848.1146717463, 2304.65),
            ConstructPoint3D(62815.2230118052, 60455.2745644757, 2304.65), ConstructPoint3D(65151.8726836331, 60138.2233402643, 2304.65), ConstructPoint3D(65196.3520363917, 58987.952687912, 2304.65));
            IElement element = ConstructMultiPatchElement(geometry, 255, 216, 216, 201);
            multiPatchGraphicsContainer3D_1.DeleteAllElements();
            multiPatchGraphicsContainer3D_2.DeleteAllElements();
            multiPatchGraphicsContainer3D_1.AddElement(element);
            axSceneControl1.SceneGraph.RefreshViewers();
            for (int i = 0; i < axSceneControl1.Scene.LayerCount; i++)
            {
                if (axSceneControl1.Scene.get_Layer(i).Name == "底图" || axSceneControl1.Scene.get_Layer(i).Name == "道路")
                {
                    axSceneControl1.Scene.get_Layer(i).Visible = true;
                }


            }
            axSceneControl1.SceneGraph.RefreshViewers();
        }
        private double calculate_area()
        {
            IArea3D mArea3D = geometry as IArea3D;
            double calculatedArea3D = mArea3D.Area3D;
            return calculatedArea3D;

        }
    }
}