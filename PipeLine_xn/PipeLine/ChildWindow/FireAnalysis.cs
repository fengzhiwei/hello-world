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
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using PipeLine.Class;

namespace PipeLine.ChildWindow
{
    public partial class FireAnalysis : DevExpress.XtraEditors.XtraForm
    {
        public AxMapControl m_MapControl;
        public IPoint point_fire;
        public DataTable dt;
        public int index = 0;
        public List<IFeature> lFeature = new List<IFeature>();
        public FireAnalysis(AxMapControl axmapContrl,IPoint point,DataTable datatable)
        {
            InitializeComponent();
            m_MapControl = axmapContrl;
            point_fire = point;
            dt = datatable;
        }
        //关闭命令取消操作
        private void FireAnalysis_FormClosed(object sender, FormClosedEventArgs e)
        {
            main.choice = 0;
            m_MapControl.MousePointer = esriControlsMousePointer.esriPointerDefault;
        }
        //定位火灾点
        private void PositionFire_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //m_MapControl.Map.SelectByShape(point_fire,null,true);
            //m_MapControl.Refresh(esriViewDrawPhase.esriViewGraphics, null, null);
            twinkle(point_fire as IGeometry);
        }
        private void shanshuo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                twinkleFireHydrant(lFeature[index]);
            }
            catch (Exception)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("没有找到消火栓");
            }
            
        }
        /// <summary>
        /// 高亮闪烁,并是所选要素居中
        /// </summary>
        /// <param name="feature"></param>
        private void twinkle(IGeometry geo)
        {
            IArray geoArray = new ArrayClass();
            geoArray.Add(geo);
            HookHelperClass m_pHookHelper = new HookHelperClass();
            m_pHookHelper.Hook = m_MapControl.Object;
            IHookActions hookActions = (IHookActions)m_pHookHelper;
            hookActions.DoActionOnMultiple(geoArray, esriHookActions.esriHookActionsGraphic);
            Application.DoEvents();
            m_pHookHelper.ActiveView.ScreenDisplay.UpdateWindow();
            hookActions.DoActionOnMultiple(geoArray, esriHookActions.esriHookActionsPan);
        }
        private void twinkle2(IGeometry geo)
        {
            IArray geoArray = new ArrayClass();
            geoArray.Add(geo);
            HookHelperClass m_pHookHelper = new HookHelperClass();
            m_pHookHelper.Hook = m_MapControl.Object;
            IHookActions hookActions = (IHookActions)m_pHookHelper;
            hookActions.DoActionOnMultiple(geoArray, esriHookActions.esriHookActionsFlash);
            Application.DoEvents();
            m_pHookHelper.ActiveView.ScreenDisplay.UpdateWindow();
            //hookActions.DoActionOnMultiple(geoArray, esriHookActions.esriHookActionsPan);
        }
        /// <summary>
        /// 导出报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void export_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dataGridView1.DataSource = dt; 
            TimeSpan ts1 = new TimeSpan(DateTime.Now.Ticks); 
            ImportOutExcel test = new ImportOutExcel();
            test.DataGridViewToExcel(dataGridView1);
            TimeSpan ts2 = new TimeSpan(DateTime.Now.Ticks); //get current ticks.
            string panTotalSeconds = ts2.Subtract(ts1).Duration().TotalSeconds.ToString();
            MessageBox.Show(panTotalSeconds);
        }
        //点击行
        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            int[] index_row = gridView1.GetSelectedRows();
            //twinkleFireHydrant(lFeature[index_row[0]]);
            index = index_row[0];
        }
        //闪烁消防栓
        private void twinkleFireHydrant(IFeature feature)
        {
            IArray geoArray = new ArrayClass();
            geoArray.Add(feature.ShapeCopy);
            HookHelperClass m_pHookHelper = new HookHelperClass();
            m_pHookHelper.Hook = m_MapControl.Object;
            IHookActions hookActions = (IHookActions)m_pHookHelper;
            hookActions.DoActionOnMultiple(geoArray, esriHookActions.esriHookActionsPan);
            Application.DoEvents();
            m_pHookHelper.ActiveView.ScreenDisplay.UpdateWindow();
            hookActions.DoActionOnMultiple(geoArray, esriHookActions.esriHookActionsFlash);
        }


    }
}