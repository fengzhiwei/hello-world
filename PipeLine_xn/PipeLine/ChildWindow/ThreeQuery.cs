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
using ESRI.ArcGIS.Geometry;

namespace PipeLine.ChildWindow
{
    public partial class ThreeQuery : DevExpress.XtraEditors.XtraForm
    {
        public AxSceneControl m_axSceneControl;
        public IFeatureLayer m_FeatureLayer;
        public IFeatureClass m_FeatureClass;
        public ThreeQuery(AxSceneControl axSceneControl)
        {
            InitializeComponent();
            this.m_axSceneControl = axSceneControl;
        }

        private void ThreeQuery_Load(object sender, EventArgs e)
        {
            if (m_axSceneControl.Scene.LayerCount < 0)
            {
                MessageBox.Show("不是有效的地图文件！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ILayer m_Layer;
            string m_LayerName;
            IFeature m_Feature;
            for (int i = 0; i < m_axSceneControl.Scene.LayerCount; i++)
            {
                m_Layer = m_axSceneControl.Scene.get_Layer(i);
                m_LayerName = m_Layer.Name;
                m_FeatureLayer = m_Layer as IFeatureLayer;
                m_FeatureClass = m_FeatureLayer.FeatureClass;
                m_Feature = m_FeatureClass.GetFeature(0);
                this.tlayercb.Items.Add(m_LayerName);
            }
            this.tlayercb.SelectedIndex = 0;

            string[] arry = { ">", "<", "=" };
            for (int i = 0; i < arry.Length; i++)
            {
                this.tconditioncb.Items.Add(arry[i]);
            }
            this.tconditioncb.SelectedIndex = 2;
        }

        private void tlayercb_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.m_axSceneControl.Scene.ClearSelection();
            this.m_axSceneControl.SceneViewer.SceneGraph.RefreshViewers();
            this.tfieldcb.Items.Clear();
            string m_FieldName;
            m_FeatureLayer = m_axSceneControl.Scene.get_Layer(tlayercb.SelectedIndex) as IFeatureLayer;
            IFeatureClass m_FeatureClass = m_FeatureLayer.FeatureClass;
            for (int l = 0; l < m_FeatureClass.Fields.FieldCount; l++)
            {
                m_FieldName = m_FeatureClass.Fields.get_Field(l).Name;
                this.tfieldcb.Items.Add(m_FieldName);
            }

            IFields m_Fields = m_FeatureClass.Fields;
            DataTable m_DataTable = new DataTable();
            string fieldName;
            string fieldValue;
            IFeature m_Feature;
            IFeatureCursor m_FeatureCursor = m_FeatureClass.Search(null, false);
            m_Feature = m_FeatureCursor.NextFeature();
            DataRow m_DataRow;
            for (int i = 0; i < m_Fields.FieldCount; i++)
            {
                fieldName = m_Fields.get_Field(i).Name;
                m_DataTable.Columns.Add(fieldName);
            }
            while (m_Feature != null)
            {
                m_DataRow = m_DataTable.NewRow();
                for (int i = 0; i < m_Fields.FieldCount; i++)
                {
                    fieldName = m_Fields.get_Field(i).Name;
                    if (fieldName == "Shape")
                    {
                        fieldValue = "点";
                    }
                    else
                    {
                        fieldValue = Convert.ToString(m_Feature.get_Value(i));
                        //m_DataRow[i] = fieldValue;
                    }
                    m_DataRow[i] = fieldValue;
                }
                m_DataTable.Rows.Add(m_DataRow);
                m_Feature = m_FeatureCursor.NextFeature();
            }
            this.dataGridView1.DataSource = m_DataTable;
            this.gridControl1.DataSource = m_DataTable;
            this.dataGridView1.Refresh();
            this.gridControl1.Refresh();
        }

        private void tfieldcb_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.tvaluecb.Items.Clear();
            string value;
            IQueryFilter q = new QueryFilterClass();
            q.WhereClause = "1=1";
            m_FeatureLayer = this.m_axSceneControl.Scene.get_Layer(tlayercb.SelectedIndex) as IFeatureLayer;
            IFeatureClass m_FeatureClass = m_FeatureLayer.FeatureClass;
            IFeature m_Feature = null;
            for (int i = 0; i < m_FeatureClass.FeatureCount(q); i++)
            {
                m_Feature = m_FeatureClass.GetFeature(i);
                value = m_Feature.get_Value(tfieldcb.SelectedIndex).ToString();
                this.tvaluecb.Items.Add(value);
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            m_axSceneControl.SceneViewer.SceneGraph.RefreshViewers();

            IQueryFilter m_QueryFilter = new QueryFilterClass();
            string selectedColumn = this.dataGridView1.Columns[0].Name;
            string value;
            int count = this.dataGridView1.SelectedRows.Count;
            if (count == 1)
            {
                value = this.dataGridView1.SelectedRows[0].Cells[selectedColumn].Value.ToString();
                m_QueryFilter.WhereClause = selectedColumn + "=" + value;
            }
            else
            {
                int i;
                string str;
                for (i = 0; i < count - 1; i++)
                {
                    value = this.dataGridView1.SelectedRows[i].Cells[selectedColumn].Value.ToString();
                    str = selectedColumn + "=" + value + "OR";
                    m_QueryFilter.WhereClause += str;
                    //.WhereClause += str;
                }
                //添加最后一个要素的条件
                value = this.dataGridView1.SelectedRows[i].Cells[selectedColumn].Value.ToString();
                str = selectedColumn + "=" + value;
                m_QueryFilter.WhereClause += str;
            }
            IFeatureLayer m_FeatureLayer = m_axSceneControl.Scene.get_Layer(this.tlayercb.SelectedIndex) as IFeatureLayer;
            IFeatureSelection m_FeatureSelection = m_FeatureLayer as IFeatureSelection;
            m_FeatureSelection.SelectFeatures(m_QueryFilter, esriSelectionResultEnum.esriSelectionResultNew, false);
            m_axSceneControl.SceneViewer.SceneGraph.RefreshViewers();
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void query_Click(object sender, EventArgs e)
        {
            if (tfieldcb.Text == "" || tvaluecb.Text == "")
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("字段和值不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                this.m_axSceneControl.Scene.ClearSelection();
                this.m_axSceneControl.SceneViewer.SceneGraph.RefreshViewers();
                IFeatureLayer m_FeatureLayer = this.m_axSceneControl.Scene.get_Layer(tlayercb.SelectedIndex) as IFeatureLayer;
                IFeatureClass m_FeatureClass = m_FeatureLayer.FeatureClass;
                IFields m_Fields = m_FeatureClass.Fields;
                IQueryFilter m_QueryFilter = new QueryFilterClass();
                m_QueryFilter.WhereClause = tfieldcb.Text + tconditioncb.Text + tvaluecb.Text;
                IFeatureCursor m_FeatureCursor = m_FeatureLayer.Search(m_QueryFilter, false);
                IFeature m_Feature = m_FeatureCursor.NextFeature();
                IPoint m_Point = m_Feature.Shape as IPoint;
                m_axSceneControl.Scene.SelectFeature(m_FeatureLayer, m_Feature);
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
                    //this.m_axMapControl.Map.SelectFeature(m_FeatureLayer, m_Feature);
                    this.m_axSceneControl.Scene.SelectFeature(m_FeatureLayer, m_Feature);
                    DataRow m_DataRow = m_DataTable.NewRow();
                    for (int i = 0; i < m_Fields.FieldCount; i++)
                    {
                        //m_Feature = m_FeatureClass.GetFeature(i);
                        fieldName = m_Fields.get_Field(i).Name;
                        //m_DataTable.Columns.Add(fieldName);
                        if (fieldName == "Shape")
                        {
                            fieldValue = "点";
                        }
                        else
                        {
                            fieldValue = Convert.ToString(m_Feature.get_Value(i));
                            //m_DataRow[i] = fieldValue;
                        }
                        m_DataRow[i] = fieldValue;
                        //m_DataTable.Rows.Add(m_DataRow[i]);
                    }
                    m_DataTable.Rows.Add(m_DataRow);
                    m_Feature = m_FeatureCursor.NextFeature();
                }
                //this.dataGridView1.DataSource = m_DataTable;
                this.gridControl1.DataSource = m_DataTable;
                //this.dataGridView1.Refresh();
                this.gridControl1.Refresh();
                this.m_axSceneControl.SceneViewer.SceneGraph.RefreshViewers();
            }
        }

        private void cancel_Click(object sender, EventArgs e)
        {

            this.m_axSceneControl.Scene.ClearSelection();
            this.m_axSceneControl.SceneViewer.SceneGraph.RefreshViewers();
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