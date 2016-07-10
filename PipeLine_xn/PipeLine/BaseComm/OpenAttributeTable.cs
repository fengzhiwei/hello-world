using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using System.Data;
using PipeLine.ChildWindow;
using System.Windows.Forms;
using System.Collections.Generic;
using ESRI.ArcGIS.Geometry;

namespace PipeLine.BaseComm
{
    /// <summary>
    /// Summary description for OpenAttributeTable.
    /// </summary>
    [Guid("0d428db5-9f64-4282-adfe-49e773fd6242")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("PipeLine.BaseComm.OpenAttributeTable")]
    public sealed class OpenAttributeTable : BaseCommand
    {
        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);
            //
            // TODO: Add any COM registration code here
            //
        }
        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);
            //
            // TODO: Add any COM unregistration code here
            //
        }
        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            ControlsCommands.Register(regKey);
        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            ControlsCommands.Unregister(regKey);
        }
        #endregion
        #endregion
        private AxMapControl m_MapControl = new AxMapControl();
        private IHookHelper m_hookHelper;
        private ILayer m_layer;
        
        public List<IFeature> lFeature = new List<IFeature>();
        public OpenAttributeTable(AxMapControl axMapControl, ILayer pLayer)
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = ""; //localizable text
            base.m_caption = "打开属性表";  //localizable text
            base.m_message = "";  //localizable text 
            base.m_toolTip = "";  //localizable text 
            base.m_name = "";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")
            m_MapControl = axMapControl;
            m_layer = pLayer;
            try
            {
                //
                // TODO: change bitmap name if necessary
                //
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overridden Class Methods

        /// <summary>
        /// Occurs when this command is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            // TODO:  Add OpenAttributeTable.OnCreate implementation
            if (hook == null)
                return;
            if (m_hookHelper == null)
                m_hookHelper = new HookHelperClass();
            m_hookHelper.Hook = hook;
        }
        AttributeTable AT;
        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        { 
           
            DataTable dataTable = getDataTable(m_layer);
            AT = new AttributeTable(m_MapControl, m_layer, lFeature);
            AT.gridControl1_Attribute.DataSource = dataTable;
            AT.Text = "属性表：" + m_layer.Name;
            //AT.gridView1.BestFitColumns();
            AT.Show();

        }

        public DataTable getDataTable(ILayer mLayer)
        {
            DevExpress.XtraGrid.GridControl gridContr = new DevExpress.XtraGrid.GridControl();

            IFeatureLayer pFeatureLayer = mLayer as IFeatureLayer;
            IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
            DataTable dt = new DataTable();
            if (pFeatureClass != null)
            {
                DataColumn dc;
                for (int i = 0; i < pFeatureClass.Fields.FieldCount; i++)
                {
                    dc = new DataColumn(pFeatureClass.Fields.get_Field(i).Name);
                    dt.Columns.Add(dc);
                }
                IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, false);
                IFeature pFeature = pFeatureCursor.NextFeature();
                IPoint tt = (IPoint)pFeature.Shape;
                DataRow dr;
                while (pFeature != null)
                {
                    lFeature.Add(pFeature);
                    dr = dt.NewRow();
                    for (int j = 0; j < pFeatureClass.Fields.FieldCount; j++)
                    {
                        if (pFeature.Fields.get_Field(j).Name == "Shape")
                        {
                            if (pFeature.Shape.GeometryType == ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint)
                            {
                                dr[j] = "点";
                            }
                            if (pFeature.Shape.GeometryType == ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline)
                            {
                                dr[j] = "线";
                            }
                            if (pFeature.Shape.GeometryType == ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon)
                            {
                                dr[j] = "面";
                            }
                        }
                        else
                        {
                            dr[j] = pFeature.get_Value(j).ToString();
                        }
                    }
                    dt.Rows.Add(dr);
                    pFeature = pFeatureCursor.NextFeature();
                }
                gridContr.DataSource = dt;
                //AT.Show();
            }
            return dt;
        }

        #endregion





    }
}
