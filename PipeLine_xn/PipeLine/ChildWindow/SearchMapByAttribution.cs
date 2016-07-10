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
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;

namespace PipeLine.ChildWindow
{
    public partial class SearchMapByAttribution : DevExpress.XtraEditors.XtraForm
    {
        private AxMapControl m_axMapControl;
        private IFeatureLayer m_FeatureLayer;
        public SearchMapByAttribution(AxMapControl axMapControl)
        {
            InitializeComponent();
            this.m_axMapControl = axMapControl;
        }

        private void SearchMapByAttribution_Load(object sender, EventArgs e)
        {
            ILayer m_Layer;
            string m_layerName;
            if (this.m_axMapControl.LayerCount < 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("不是有效的文件", "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            for (int i = 0; i < this.m_axMapControl.LayerCount; i++)
            {
                m_Layer = this.m_axMapControl.get_Layer(i);
                m_layerName = m_Layer.Name;
                this.layerNamecb_dev.Properties.Items.Add(m_layerName);
            }
            this.layerNamecb_dev.SelectedIndex = 0; // 图层默认为第一个！
            m_FeatureLayer = this.m_axMapControl.get_Layer(layerNamecb_dev.SelectedIndex) as IFeatureLayer;
            IFeatureClass m_FeatureClass = m_FeatureLayer.FeatureClass;
            string m_fieldName;
            for (int i = 0; i < m_FeatureClass.Fields.FieldCount; i++)
            {
                m_fieldName = m_FeatureClass.Fields.get_Field(i).Name;
                this.queryFieldcb_dev.Properties.Items.Add(m_fieldName);
            }
            this.queryFieldcb_dev.SelectedIndex = 0;
            this.queryConditoncb_dev.SelectedIndex = 0;
        }
        /// <summary>
        /// 字段下拉框的值改变时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void queryFieldcb_dev_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.queryConditoncb_dev.Properties.Items.Clear();
            string queryValue;
            IQueryFilter m_QueryFilter = new QueryFilterClass();
            m_QueryFilter.WhereClause = "1=1";
            m_FeatureLayer = this.m_axMapControl.get_Layer(layerNamecb_dev.SelectedIndex) as IFeatureLayer;
            IFeatureClass m_FeaureClass = m_FeatureLayer.FeatureClass;
            IFeature m_Feature;
            for (int i = 1; i < m_FeaureClass.FeatureCount(m_QueryFilter); i++)
            {
                m_Feature = m_FeaureClass.GetFeature(i);
                queryValue = m_Feature.get_Value(queryFieldcb_dev.SelectedIndex).ToString();
                queryConditoncb_dev.Properties.Items.Add(queryValue);
            }
            queryConditoncb_dev.SelectedIndex = 0;
        }
        /// <summary>
        /// 查询!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void querybt_dev_Click(object sender, EventArgs e)
        {
            if (queryConditoncb_dev.Text.Equals(""))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("查询值不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                this.m_axMapControl.Map.ClearSelection();
                this.m_axMapControl.ActiveView.Refresh();
                IFeatureCursor m_FeatureCursor;
                IQueryFilter m_QueryFilter = new QueryFilterClass();
                IFeature m_Feature;
                m_QueryFilter.WhereClause = queryFieldcb_dev.Text + "=" + queryConditoncb_dev.Text;
                m_FeatureCursor = m_FeatureLayer.Search(m_QueryFilter,true);
                m_Feature = m_FeatureCursor.NextFeature(); //即将游标移动到结果集下一个要素并返回当前要素
                if (m_Feature != null)
                {
                    this.m_axMapControl.Map.SelectFeature(m_FeatureLayer, m_Feature);
                    for (int i = 0; i < 3; i++)
                    {
                        twinkle(m_Feature);
                    }
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("没有找到相关要素！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        /// <summary>
        /// 高亮闪烁,并是所选要素居中
        /// </summary>
        /// <param name="feature"></param>
        private void twinkle(IFeature feature)
        {
            IArray geoArray = new ArrayClass();
            geoArray.Add(feature.ShapeCopy);
            HookHelperClass m_pHookHelper = new HookHelperClass();
            m_pHookHelper.Hook = m_axMapControl.Object;
            IHookActions hookActions = (IHookActions)m_pHookHelper;
            hookActions.DoActionOnMultiple(geoArray, esriHookActions.esriHookActionsPan);
            Application.DoEvents();
            m_pHookHelper.ActiveView.ScreenDisplay.UpdateWindow();
            hookActions.DoActionOnMultiple(geoArray, esriHookActions.esriHookActionsFlash);
        }
        /// <summary>
        /// 点击取消，刷新地图，并关闭当前窗口！
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelbt_dev_Click(object sender, EventArgs e)
        {
            this.m_axMapControl.Map.ClearSelection();
            this.m_axMapControl.ActiveView.Refresh();
            this.Close();
            this.m_axMapControl.Extent = this.m_axMapControl.FullExtent;
        }

        private void SearchMapByAttribution_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.m_axMapControl.Map.ClearSelection();
            this.m_axMapControl.ActiveView.Refresh();
            this.m_axMapControl.Extent = this.m_axMapControl.FullExtent;
        }
    }
}