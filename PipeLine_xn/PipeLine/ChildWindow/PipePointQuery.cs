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

namespace PipeLine.ChildWindow
{
    public partial class PipePointQuery : DevExpress.XtraEditors.XtraForm
    {
        public AxMapControl m_axMapControl;
        public IFeatureLayer m_FeatureLayer;
        public IFeatureClass m_FeatureClass;
        public PipePointQuery(AxMapControl axMapControl)
        {
            InitializeComponent();
            this.m_axMapControl = axMapControl;
        }

        private void PipePointQuery_Load(object sender, EventArgs e)
        {
            if (m_axMapControl.LayerCount < 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("不是有效的文件", "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ILayer m_Layer;
            string m_LayerName;
            IFeature m_Feature;
            for (int i = 0; i < m_axMapControl.LayerCount; i++)
            {
                m_Layer = m_axMapControl.get_Layer(i);
                m_LayerName = m_Layer.Name;
                m_FeatureLayer = m_Layer as IFeatureLayer;
                m_FeatureClass = m_FeatureLayer.FeatureClass;
                m_Feature = m_FeatureClass.GetFeature(1);
                //int feng = 3;
                if (m_Feature.Shape.GeometryType == ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint)
                {
                    this.PipePointcb_dev.Properties.Items.Add(m_LayerName);
                }
            }
            this.PipePointcb_dev.SelectedIndex = 0;

            string[] arry = { ">", "<", "=" };
            for (int i = 0; i < arry.Length; i++)
            {
                this.queryconditoncb_dev.Properties.Items.Add(arry[i]);
            }
            this.queryconditoncb_dev.SelectedIndex = 2;
            this.queryfiled_dev.SelectedIndex = 0;
            this.queryvaluecb_dev.SelectedIndex = 0;
        }

        private void PipePointcb_dev_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.m_axMapControl.Map.ClearSelection();
            this.m_axMapControl.ActiveView.Refresh();
            this.queryfiled_dev.Properties.Items.Clear();
            //this.queryconditoncb_dev.Properties.Items.Clear();
            string m_FieldName;
            m_FeatureLayer = this.m_axMapControl.get_Layer(PipePointcb_dev.SelectedIndex) as IFeatureLayer;
            IFeatureClass m_FeatureClass = m_FeatureLayer.FeatureClass;
            for (int i = 0; i < m_FeatureClass.Fields.FieldCount - 2; i++)
            {
                m_FieldName = m_FeatureClass.Fields.get_Field(i).Name;
                this.queryfiled_dev.Properties.Items.Add(m_FieldName);
            }

            IFields m_Fields = m_FeatureClass.Fields;
            DataTable m_DataTable = new DataTable();
            string fieldName;
            string fieldValue;
            IFeature m_Feature;
            IFeatureCursor m_FeatureCursor = m_FeatureClass.Search(null,false);
            m_Feature = m_FeatureCursor.NextFeature();
            DataRow m_DataRow;
            for (int i = 0; i < m_Fields.FieldCount; i++)
            {
                fieldName = m_Fields.get_Field(i).Name;
                m_DataTable.Columns.Add(fieldName);
            }
            while (m_Feature != null)
            {
                m_DataRow = m_DataTable.NewRow(); //初始化行
                for (int i = 0; i <m_Fields.FieldCount ; i++)
                {
                    fieldName = m_Fields.get_Field(i).Name;
                    if (fieldName.Equals("Shape"))
                    {
                        fieldValue = "点";
                    }
                    else
                    {
                        fieldValue = Convert.ToString(m_Feature.get_Value(i));
                    }
                    m_DataRow[i] = fieldValue;
                }
                m_DataTable.Rows.Add(m_DataRow);
                m_Feature = m_FeatureCursor.NextFeature();
            }
            this.resultdata_dev.DataSource = m_DataTable;
            this.resultdata_dev.Refresh();
        }

        private void queryfiled_dev_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.queryvaluecb_dev.Properties.Items.Clear();
            string value;
            IQueryFilter query = new QueryFilterClass();
            query.WhereClause = "1=1";
            m_FeatureLayer = this.m_axMapControl.get_Layer(PipePointcb_dev.SelectedIndex) as FeatureLayer;
            IFeatureClass m_FeatureClass = m_FeatureLayer.FeatureClass;
            IFeature m_Feature = null;
            for (int i = 1; i < m_FeatureClass.FeatureCount(query); i++)
            {
                m_Feature = m_FeatureClass.GetFeature(i);
                value = m_Feature.get_Value(queryfiled_dev.SelectedIndex).ToString();
                this.queryvaluecb_dev.Properties.Items.Add(value);
            }
        }

        private void ensurebt_dev_Click(object sender, EventArgs e)
        {
            if (queryfiled_dev.Text.Equals("") || queryvaluecb_dev.Text.Equals(""))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("字段和值不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                this.m_axMapControl.Map.ClearSelection();
                this.m_axMapControl.ActiveView.Refresh();
                IFeatureLayer m_FeatureLayer = this.m_axMapControl.get_Layer(PipePointcb_dev.SelectedIndex) as FeatureLayer;
                IFeatureClass m_FeatureClass = m_FeatureLayer.FeatureClass;
                IFields m_Fields = m_FeatureClass.Fields;
                IQueryFilter m_QueryFilter = new QueryFilterClass();
                m_QueryFilter.WhereClause = queryfiled_dev.Text + queryconditoncb_dev.Text + queryvaluecb_dev.Text;
                IFeatureCursor m_FeatureCursor = m_FeatureLayer.Search(m_QueryFilter,false);
                IFeature m_Feature = m_FeatureCursor.NextFeature();
                string fieldName;
                string fieldValue;
                DataTable m_DataTable = new DataTable();
                for (int i = 0; i < m_Fields.FieldCount; i++)
                {
                    //m_Feature = m_FeatureClass.GetFeature(i);
                    fieldName = m_Fields.get_Field(i).Name;
                    m_DataTable.Columns.Add(fieldName);
                }
                while (m_Feature != null)
                {
                    this.m_axMapControl.Map.SelectFeature(m_FeatureLayer,m_Feature);
                    DataRow m_DataRow = m_DataTable.NewRow();
                    for (int i = 0; i < m_Fields.FieldCount; i++)
                    {
                        fieldName = m_Fields.get_Field(i).Name;
                        if (fieldName.Equals("Shape"))
                        {
                            fieldValue = "点";
                        }
                        else
                        {
                            fieldValue = Convert.ToString(m_Feature.get_Value(i));
                        }
                        m_DataRow[i] = fieldValue;
                    }
                    m_DataTable.Rows.Add(m_DataRow);
                    m_Feature = m_FeatureCursor.NextFeature();//当要素为空时，作为跳出循环的条件！
                }
                this.resultdata_dev.DataSource = m_DataTable;
                this.resultdata_dev.Refresh();
                this.m_axMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
            }
        }

        private void cancelbt_dev_Click(object sender, EventArgs e)
        {
            this.m_axMapControl.Map.ClearSelection();
            this.m_axMapControl.ActiveView.Refresh();
            this.Close();
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
