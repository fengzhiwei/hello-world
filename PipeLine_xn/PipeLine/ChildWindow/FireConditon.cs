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
using PipeLine.Class;

namespace PipeLine.ChildWindow
{
    public partial class FireConditon : DevExpress.XtraEditors.XtraForm
    {
        public AxMapControl m_MapControl;
        public FireConditon(AxMapControl axMapContrl)
        {
            InitializeComponent();
            m_MapControl = axMapContrl;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                main.Fire_Condition_radius = double.Parse(fireDadius.Text);
                this.Close();
                string path = Application.StartupPath + @"\pic\定位火灾.ico";
                m_MapControl.MouseIcon = new Icon(path);
                m_MapControl.MousePointer = esriControlsMousePointer.esriPointerCustom;
            }
            catch (Exception)
            {

                DevExpress.XtraEditors.XtraMessageBox.Show("输入的半径无效");
            }
            

        }

        private void fireDadius_Validated(object sender, EventArgs e)
        { 
            double test;
            Numberic numberic = new Numberic();
            if (numberic.isNumberic(fireDadius.Text, out test))
            {
                return;
            }
            else
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("输入的半径无效");
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
            main.choice = 0;
            m_MapControl.MousePointer = esriControlsMousePointer.esriPointerDefault;
        }
    }
}