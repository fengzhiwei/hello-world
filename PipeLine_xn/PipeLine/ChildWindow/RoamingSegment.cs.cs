using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Geodatabase;
using System.Threading;

namespace PipeLine.ChildWindow
{
    public partial class RoamingSegment : Form
    {
        public static bool m_bPause = false;
        AxSceneControl m_axSceneControl1;
        main mymain = new main();

        IPoint target;
        IPoint observer;
        public RoamingSegment(AxSceneControl axSceneControl1 ,IPoint topt,IPoint obopt)
        {
            InitializeComponent();
            m_axSceneControl1 = axSceneControl1;
            target = topt;
            observer = obopt;

        }

        private void RoamingSegment_Load(object sender, EventArgs e)
        {
            
            IFeatureLayer pFeatureLayer;
            for (int i = 0; i < m_axSceneControl1.SceneGraph.Scene.LayerCount; i++)
            {
                pFeatureLayer = m_axSceneControl1.SceneGraph.Scene.get_Layer(i) as IFeatureLayer;
                if (pFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)//判断图层要素是否为点要素
                {


                    checkedListBox1.Items.Add(m_axSceneControl1.SceneGraph.Scene.get_Layer(i).Name.ToString());

                }

            }
          
        }

        //取消选择函数
        private void checkelistboxclear(CheckedListBox checkedlistbox)
        {
            for (int i = 0; i < checkedlistbox.Items.Count; i++)
            {
                checkedlistbox.SetItemChecked(i, false);

            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            ThreadPool.QueueUserWorkItem(ThreadFun);
            button4.Enabled = false;
        }
        private void ThreadFun(object obj)
        {
            try
            {
                for (int q = 0; q < checkedListBox1.Items.Count; q++)
                {
                    if (checkedListBox1.GetItemChecked(q))
                    {
                        string checkedname = checkedListBox1.Items[q].ToString();
                        for (int k = 0; k < m_axSceneControl1.SceneGraph.Scene.LayerCount; k++)
                        {
                            if (checkedname == m_axSceneControl1.SceneGraph.Scene.get_Layer(k).Name)
                            {
                                ILayer layer = m_axSceneControl1.SceneGraph.Scene.get_Layer(k);
                                IFeatureLayer featurelayer = (IFeatureLayer)layer;
                                IFeatureClass featureclass = featurelayer.FeatureClass;


                                // IFeature feature = featureclass.GetFeature(0);
                                IQueryFilter m_QueryFilter = new QueryFilterClass();
                                m_QueryFilter.WhereClause = "1=1";
                                for (int j = 0; j < featureclass.FeatureCount(m_QueryFilter); j++)
                                {
                                    IFeature feature = featureclass.GetFeature(j);
                                    //漫游线段高亮显示
                                    m_axSceneControl1.SceneGraph.Scene.SelectFeature(layer, feature);
                                    //   m_axSceneControl1.SceneGraph.RefreshViewers();
                                }



                                ILayer layer1 = m_axSceneControl1.SceneGraph.Scene.get_Layer(k);
                                IFeatureLayer featurelayer1 = (IFeatureLayer)layer1;
                                IFeatureClass featureclass1 = featurelayer1.FeatureClass;


                                // IFeature feature = featureclass.GetFeature(0);
                                IQueryFilter m_QueryFilter1 = new QueryFilterClass();
                                m_QueryFilter1.WhereClause = "1=1";
                                for (int j = 0; j < featureclass1.FeatureCount(m_QueryFilter1); j++)
                                {
                                    IFeature feature = featureclass1.GetFeature(j);
                                    IPolyline polyline = (IPolyline)feature.Shape;
                                    double d = polyline.Length;
                                    IPoint point1 = new PointClass();
                                    IPoint point2 = new PointClass();
                                    IFeature m_Feature = featureclass1.GetFeature(j);

                                    if (d > 10)     //设置漫游的管线长度阈值
                                        for (int i = 2; i <= (int)d; i++)
                                        {

                                            polyline.QueryPoint(esriSegmentExtension.esriNoExtension, i, false, point1);
                                            polyline.QueryPoint(esriSegmentExtension.esriExtendAtFrom, i - 1, false, point2);


                                            //point2.Z = 2315;

                                            point2.Z = mymain.ConvertToDouble(m_Feature.get_Value(mymain.GetFieldIndex(featureclass1, "标高"))) + 1;
                                            //设置行走方向
                                            double m = point2.Y - point1.Y;
                                            double n = point2.X - point1.X;

                                            if (Math.Abs(m) > Math.Abs(n))
                                            {
                                                if (m > 0)
                                                {
                                                    point2.Y = point2.Y + 50;
                                                }
                                                else
                                                {
                                                    point2.Y = point2.Y + -50;
                                                }


                                            }
                                            else
                                            {
                                                if (n < 0)
                                                {
                                                    point2.X = point2.X + -50;
                                                }
                                                else
                                                {
                                                    point2.X = point2.X + 50;
                                                }
                                            }


                                            ICamera camera = m_axSceneControl1.SceneViewer.Camera;
                                            IPoint point3 = new PointClass();
                                            point3.X = point1.X;
                                            point3.Y = point1.Y;
                                            //point3.Z = 2315;
                                            point3.Z = mymain.ConvertToDouble(m_Feature.get_Value(mymain.GetFieldIndex(featureclass1, "标高"))) + 3;//设置摄像头高度
                                            camera.Target = point3;
                                            camera.Observer = point2;

                                            IScene pscene = m_axSceneControl1.SceneGraph.Scene;
                                            //IMarker3DSymbol pmark3dsymbol = new Marker3DSymbolClass();
                                            //pmark3dsymbol.CreateFromFile("E:\\car.3DS");
                                            //IMarkerSymbol marksy = (IMarkerSymbol)pmark3dsymbol;
                                            //marksy.Size = 10;
                                            //marksy.Angle = 10;

                                            IElement pelement = new MarkerElementClass();
                                            IMarkerElement pmarkelement = (IMarkerElement)pelement;
                                            //pmarkelement.Symbol = (IMarkerSymbol)marksy;
                                            pelement.Geometry = point1;
                                            IGraphicsLayer player = m_axSceneControl1.SceneGraph.Scene.BasicGraphicsLayer;
                                            IGraphicsContainer3D pgraphiccontainer3d = (IGraphicsContainer3D)player;
                                            pgraphiccontainer3d.DeleteAllElements();
                                            pgraphiccontainer3d.AddElement((IElement)pmarkelement);
                                            Thread.Sleep(5);
                                            Invoke(new MethodInvoker(delegate
                                            {
                                                while (m_bPause)
                                                {
                                                    Application.DoEvents();
                                                }

                                                //  this.Text = k.ToString();
                                            }));
                                            m_axSceneControl1.SceneGraph.RefreshViewers();

                                        }
                                }



                            }
                        }

                    }

                }
              
                //漫游后回到起始位置
                ICamera pCamaera = m_axSceneControl1.Camera;
                pCamaera.Azimuth = 90;
                pCamaera.Target = target;
                pCamaera.Observer = observer;
              //  m_axSceneControl1.Refresh();
                m_axSceneControl1.SceneGraph.RefreshViewers();

            }
            catch(Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("请在漫游结束后点击关闭窗体", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
             
                //漫游后回到起始位置
                ICamera pCamaera = m_axSceneControl1.Camera;
                pCamaera.Azimuth = 90;
                pCamaera.Target = target;
                pCamaera.Observer = observer;
                m_axSceneControl1.SceneGraph.RefreshViewers();
            }
        }
    
        //全选
        private void button1_Click_1(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);

            }

        }
        //反选
        private void button2_Click_1(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    checkedListBox1.SetItemChecked(i, false);
                    //break;
                }
                else
                {
                    checkedListBox1.SetItemChecked(i, true);

                }

            }

        }
        //取消
        private void button3_Click_1(object sender, EventArgs e)
        {
            checkelistboxclear(checkedListBox1);
        }
        //暂停或者继续
        private void button5_Click(object sender, EventArgs e)
        {
            if (button5.Text == "暂停")
            {
                m_bPause = true;
                button5.Text = "继续";

            }
            else
            {
                m_bPause = false;
                button5.Text = "暂停";

            }
        }
        //结束
        private void button6_Click(object sender, EventArgs e)
        {
            button4.Enabled = true;

        }

    }
}
