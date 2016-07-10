using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using PipeLine.Class;

namespace PipeLine.ChildWindow
{
    public partial class AttributeTable : DevExpress.XtraEditors.XtraForm
    {
        ILayer mLayer;
        public List<IFeature> lFeature = new List<IFeature>();
        private AxMapControl m_MapControl = new AxMapControl();
        public AttributeTable(AxMapControl axMapControl, ILayer layer, List<IFeature> lFeature_list)
        {
            InitializeComponent();
            mLayer = layer;
            m_MapControl = axMapControl;
            lFeature = lFeature_list;
        }
        public DataTable dataTable
        {
            get { return dataTable; }
            set { dataTable = value; }
        }
        private void gridControl1_Attribute_Click(object sender, EventArgs e)
        {
            //
        }

        private void AttributeTable_Load(object sender, EventArgs e)
        {
            
            //this.Text = "属性表：" + mLayer.Name;
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            int[] index = gridView1.GetSelectedRows();
            twinkle2(lFeature[index[0]]);
        }
        /// <summary>
        /// 居中闪烁
        /// </summary>
        /// <param name="feature"></param>
        private void twinkle2(IFeature feature)
        {
            IArray geoArray = new ArrayClass();
            geoArray.Add(feature.ShapeCopy);
            HookHelperClass m_pHookHelper = new HookHelperClass();
            m_pHookHelper.Hook = m_MapControl.Object;
            IHookActions hookActions = (IHookActions)m_pHookHelper;
            hookActions.DoActionOnMultiple(geoArray, esriHookActions.esriHookActionsPan);
            //处理当前消息队列中的所有windows消息
            Application.DoEvents();
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

        
    }
}