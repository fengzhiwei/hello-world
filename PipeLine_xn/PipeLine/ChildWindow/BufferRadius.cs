using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;

namespace PipeLine.ChildWindow
{
    public partial class BufferRadius : DevExpress.XtraEditors.XtraForm
    {
        private AxMapControl m_MapControl = new AxMapControl();
        public BufferRadius(AxMapControl axMapControl)
        {
            InitializeComponent();
            m_MapControl = axMapControl;
        }

        private void BufferRadius_FormClosed(object sender, FormClosedEventArgs e)
        {
            main.choice = 0;
        }

        
        
    }
}







