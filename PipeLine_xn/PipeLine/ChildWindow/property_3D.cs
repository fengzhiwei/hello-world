using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Controls;

namespace PipeLine.ChildWindow
{
    public partial class property_3D : DevExpress.XtraEditors.XtraForm
    {
        public IDisplay3D pDisplay3d;
        AxSceneControl m_SceneControl;
        public property_3D(AxSceneControl axSceneControl)
        {
            InitializeComponent();
            m_SceneControl = axSceneControl;
        }
        //显示结果集合
        public void refeshView(IHit3DSet pHit3Dset)
        {
            m_SceneControl.Scene.ClearSelection();
            //用tree控件显示查询结果
            mTreeView.BeginUpdate();
            //清空tree控件的内容
            mTreeView.Nodes.Clear();
            //pDisplay3d = null;
            IHit3D pHit3D;
            //遍历结果集
            for (int i = 0; i < pHit3Dset.Hits.Count; i++)
            {
                pHit3D = pHit3Dset.Hits.get_Element(i) as IHit3D;
                if (pHit3D.Owner is ILayer)
                {
                    ILayer pLayer = pHit3D.Owner as ILayer;
                    if (pHit3D.Object != null)
                    {
                        if (pHit3D.Object is IFeature)
                        {
                            IFeature pFeature = (IFeature)pHit3D.Object;
                            //显示Feature中的内容
                            if (pFeature.Fields.FieldCount > 5)
                            {
                                TreeNode node = mTreeView.Nodes.Add(pLayer.Name);
                                node.Nodes.Add("X=" + pHit3D.Point.X.ToString());
                                node.Nodes.Add("Y=" + pHit3D.Point.Y.ToString());
                                node.Nodes.Add("Z=" + pHit3D.Point.Z.ToString());
                                for (int j = 0; j < pFeature.Fields.FieldCount; j++)
                                {
                                    node.Nodes.Add(pFeature.Fields.get_Field(j).Name + ":" + pFeature.get_Value(j).ToString());
                                }
                                m_SceneControl.Scene.SelectFeature(pLayer, pFeature);

                            }
                            else
                            {
                                continue;
                            }

                        }
                    }
                }
            }
            mTreeView.EndUpdate();
            mTreeView.ExpandAll();
            m_SceneControl.Scene.SceneGraph.RefreshViewers();

        }

        private void property_3D_FormClosed(object sender, FormClosedEventArgs e)
        {
            main.scene = 0;
            main.number = 0;
            m_SceneControl.CurrentTool = null;
            m_SceneControl.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
        }


    }
}