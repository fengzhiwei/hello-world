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
    public partial class FieldType : DevExpress.XtraEditors.XtraForm
    {
        private AxMapControl m_MapControl;
        public FieldType(AxMapControl axMapControl)
        {
            InitializeComponent();
            m_MapControl = axMapControl;
        }

        private void FieldType_Load(object sender, EventArgs e)
        {
            field_Type.Items.Add("Short Integer");
            field_Type.Items.Add("Long Integer");
            field_Type.Items.Add("Float");
            field_Type.Items.Add("Double");
            field_Type.Items.Add("Text");
            field_Type.Items.Add("Date");
            field_Type.SelectedIndex = 0;
        }

        private void OK_Click(object sender, EventArgs e)
        {
            IFeatureLayer mFeatureLayer = m_MapControl.Map.get_Layer(0) as IFeatureLayer;
            IFeatureClass mFeatureClass = mFeatureLayer.FeatureClass;
            esriFieldType fieldtype;
            switch (field_Type.Text)
            {
                case "Short Integer":
                    fieldtype = esriFieldType.esriFieldTypeSmallInteger;
                    AddField(mFeatureClass, field_Name.Text, field_Name.Text, fieldtype);
                    break;
                case "Long Integer":
                    fieldtype = esriFieldType.esriFieldTypeInteger;
                    AddField(mFeatureClass, field_Name.Text, field_Name.Text, fieldtype);
                    break;
                case "Float":
                    fieldtype = esriFieldType.esriFieldTypeSingle;
                    AddField(mFeatureClass, field_Name.Text, field_Name.Text, fieldtype);
                    break;
                case "Double":
                    fieldtype = esriFieldType.esriFieldTypeDouble;
                    AddField(mFeatureClass, field_Name.Text, field_Name.Text, fieldtype);
                    break;
                case "Text":
                    fieldtype = esriFieldType.esriFieldTypeString;
                    AddField(mFeatureClass, field_Name.Text, field_Name.Text, fieldtype);
                    break;
                case "Date":
                    fieldtype = esriFieldType.esriFieldTypeDate;
                    AddField(mFeatureClass, field_Name.Text, field_Name.Text, fieldtype);
                    break;
            }

        }

        private void AddField(IFeatureClass pFeatureClass, string name, string aliasName, esriFieldType FieldType)
        {
            //若存在，则不需添加
            if (pFeatureClass.Fields.FindField(name) > -1) return;
            IField pField = new FieldClass();
            IFieldEdit pFieldEdit = pField as IFieldEdit;
            pFieldEdit.AliasName_2 = aliasName;
            pFieldEdit.Name_2 = name;
            pFieldEdit.Type_2 = FieldType;

            IClass pClass = pFeatureClass as IClass;
            pClass.AddField(pField);
        }


    }
}