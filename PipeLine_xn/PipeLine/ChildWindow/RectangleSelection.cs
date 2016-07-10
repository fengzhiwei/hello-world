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
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;

namespace PipeLine.ChildWindow
{
    public partial class RectangleSelection : DevExpress.XtraEditors.XtraForm
    {
        private AxMapControl m_MapControl = new AxMapControl();
        public List<IFeature> lFeature = new List<IFeature>();
        public RectangleSelection(AxMapControl axMapControl)
        {
            InitializeComponent();
            m_MapControl = axMapControl;
            //@"\2016-03-14\最新数据\西宁特钢模型.sxd";
            string path_icon = Application.StartupPath + @"\Image_ICon\blue_rectangle.ico";
            this.Icon = new Icon(path_icon);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="geometry"></param>
        public void result(IGeometry geometry)
        {
            m_MapControl.Map.ClearSelection();
            IFeatureCursor pFeatureCursor;
            ILayer layer = null;
            DataTable dataTable = new DataTable();
            lFeature.Clear();
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
                        if (pFeature.Class.AliasName == "直埋电缆" | pFeature.Class.AliasName == "铁路")
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
                            lFeature.Add(pFeature);
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
            gridControl1.DataSource = dataTable;
            this.gridView1.BestFitColumns();
            this.gridView1.GroupPanelText = "结果:";
            if (dataTable.Rows.Count > 99)
            {
                this.gridView1.IndicatorWidth = 35;
            }
            else
            {
                this.gridView1.IndicatorWidth = 30;
            }
            FlashPolygons(lFeature, m_MapControl);
            m_MapControl.Refresh();
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            int[] index = gridView1.GetSelectedRows();
            //MessageBox.Show(index[0].ToString());
            twinkle(lFeature[index[0]]);
            m_MapControl.Refresh();
        }
        private void twinkle(IFeature feature)
        {
            IArray geoArray = new ArrayClass();
            geoArray.Add(feature.ShapeCopy);
            HookHelperClass m_pHookHelper = new HookHelperClass();
            m_pHookHelper.Hook = m_MapControl.Object;
            IHookActions hookActions = (IHookActions)m_pHookHelper;
            Application.DoEvents();
            m_pHookHelper.ActiveView.ScreenDisplay.UpdateWindow();
            hookActions.DoActionOnMultiple(geoArray, esriHookActions.esriHookActionsFlash);
        }
        private void FlashPolygons(List<IFeature> lFeature, AxMapControl m_axMapControl)
        {
            IArray geoArray = new ArrayClass();
            for (int i = 0; i < lFeature.Count; i++)
            {
                geoArray.Add(lFeature[i].ShapeCopy);
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

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            if (e.Info.IsRowIndicator)
            {
                if (e.RowHandle >= 0)
                {
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();
                }
                else if (e.RowHandle < 0 && e.RowHandle > -1000)
                {
                    e.Info.Appearance.BackColor = System.Drawing.Color.AntiqueWhite;
                    e.Info.DisplayText = "G" + e.RowHandle.ToString();
                }
            }
        }

        private void RectangleSelection_FormClosed(object sender, FormClosedEventArgs e)
        {
            main.choice = 0;
        }



    }
}