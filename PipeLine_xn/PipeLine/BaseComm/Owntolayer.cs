using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System.Threading;
using System.Drawing;

namespace PipeLine.BaseComm
{
    class Owntolayer : BaseCommand
    {
        private ILayer mlayer;
        AxMapControl axmapcontrol;
        IEnvelope meve;
        public Owntolayer(ILayer pSl, AxMapControl axmap, IEnvelope eve)
        {
            base.m_caption = "显示";
            mlayer = pSl;
            axmapcontrol = axmap;
            meve = eve;
            axmapcontrol.Map.ClearSelection();

            try
            {

                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }
        //重写BaseCommand基类的虚拟方法OnClick()   
        public override void OnClick()
        {
            IFeatureLayer mFeaturelayer = mlayer as IFeatureLayer;
            IQueryFilter m_QueryFilter = new QueryFilterClass();
            m_QueryFilter.WhereClause = "1=1";
            IFeatureCursor m_FeatureCursor = mFeaturelayer.Search(m_QueryFilter, true);
            IFeature m_Feature = m_FeatureCursor.NextFeature();
            while (m_Feature != null)
            {
                axmapcontrol.Map.SelectFeature(mlayer, m_Feature);
                axmapcontrol.ActiveView.Refresh();
                m_Feature = m_FeatureCursor.NextFeature();
            }
            axmapcontrol.Extent = meve;
        }
        public override void OnCreate(object hook)
        {

        }
    }
}
