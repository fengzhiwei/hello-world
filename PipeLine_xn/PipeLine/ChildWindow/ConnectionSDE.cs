using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Carto;

namespace PipeLine.ChildWindow
{
    public partial class ConnectionSDE : DevExpress.XtraEditors.XtraForm
    {
        public static IPropertySet pPropertySetConnect;
        public static IWorkspace workspace;
        AxMapControl m_axMapControl;
        public ConnectionSDE(AxMapControl axmapControl)
        {
            InitializeComponent();
            m_axMapControl = axmapControl;
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        //连接SDE
        private void Loggin_Click(object sender, EventArgs e)
        {
            if (server.Text.Length == 0 | instance.Text.Length == 0 | database.Text.Length == 0 | user.Text.Length == 0 | password.Text.Length == 0 | version.Text.Length == 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("参数不完整", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Sde(server.Text, instance.Text, database.Text, user.Text, password.Text, version.Text, m_axMapControl);
                this.Close();
            }
        }
        public static void Sde(string a1, string a2, string a3, string a4, string a5, string a6, AxMapControl m_map)
        {

            pPropertySetConnect = new PropertySetClass();
            pPropertySetConnect.SetProperty("SERVER", a1);
            pPropertySetConnect.SetProperty("INSTANCE", a2);
            pPropertySetConnect.SetProperty("DATABASE", a3);
            pPropertySetConnect.SetProperty("USER", a4);
            pPropertySetConnect.SetProperty("PASSWORD", a5);
            pPropertySetConnect.SetProperty("VERSION", a6);
            try
            {
                IWorkspaceFactory pWorkspaceFactory = new SdeWorkspaceFactoryClass();
                workspace = pWorkspaceFactory.Open(pPropertySetConnect, 0);
                DevExpress.XtraEditors.XtraMessageBox.Show("连接成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //MessageBox.Show("连接成功！");

                IWorkspaceFactory Fact = new SdeWorkspaceFactoryClass();
                //IWorkspace workspace = workSpace;
                IFeatureWorkspace pSdeFeatureWorkspace = workspace as IFeatureWorkspace;
                IFeatureDataset featureDataset = pSdeFeatureWorkspace.OpenFeatureDataset("test002");

                IFeatureClassContainer featureClassContainer = featureDataset as IFeatureClassContainer;
                IEnumFeatureClass enumFeatureClass = featureClassContainer.Classes;
                IFeatureClass m_FeatureClass = enumFeatureClass.Next();

                while (m_FeatureClass != null)
                {
                    if (m_FeatureClass.ShapeType == ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon)
                    {
                        IFeatureLayer featurelayer = new FeatureLayerClass();
                        featurelayer.FeatureClass = m_FeatureClass;
                        int kk = m_FeatureClass.AliasName.ToString().Length;
                        featurelayer.Name = m_FeatureClass.AliasName.ToString().Substring(4, kk - 4);
                        addmap(featurelayer, m_map);

                    }
                    m_FeatureClass = enumFeatureClass.Next();

                }
                enumFeatureClass.Reset();
                m_FeatureClass = enumFeatureClass.Next();
                while (m_FeatureClass != null)
                {
                    if (m_FeatureClass.ShapeType == ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline)
                    {
                        IFeatureLayer featurelayer = new FeatureLayerClass();
                        featurelayer.FeatureClass = m_FeatureClass;
                        int kk = m_FeatureClass.AliasName.ToString().Length;
                        featurelayer.Name = m_FeatureClass.AliasName.ToString().Substring(4, kk - 4);
                        addmap(featurelayer, m_map);

                    }
                    m_FeatureClass = enumFeatureClass.Next();

                }
                enumFeatureClass.Reset();
                m_FeatureClass = enumFeatureClass.Next();
                while (m_FeatureClass != null)
                {
                    if (m_FeatureClass.ShapeType == ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint
                        & m_FeatureClass.AliasName.ToString() != "SDE.test002_Net_Junctions")
                    {
                        IFeatureLayer featurelayer = new FeatureLayerClass();
                        featurelayer.FeatureClass = m_FeatureClass;
                        int kk = m_FeatureClass.AliasName.ToString().Length;
                        featurelayer.Name = m_FeatureClass.AliasName.ToString().Substring(4, kk - 4);
                        addmap(featurelayer, m_map);
                        

                    }
                    m_FeatureClass = enumFeatureClass.Next();

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                pPropertySetConnect = null;
                DevExpress.XtraEditors.XtraMessageBox.Show("连接失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show("连接失败！");
            }
        }
        public static void addmap(IFeatureLayer pFLayer, AxMapControl m_map)
        {
            IGeoFeatureLayer geoFeatureLayer;
            geoFeatureLayer = pFLayer as IGeoFeatureLayer;
            m_map.AddLayer(geoFeatureLayer);
            m_map.Refresh();
        }


    }
}