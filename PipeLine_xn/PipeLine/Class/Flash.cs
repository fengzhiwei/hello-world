using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;

namespace PipeLine.Class
{
    class Flash
    {
        public void twinkleFireHydrant(IFeature feature, AxMapControl m_MapControl)
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

        public void Position(IGeometry geo, AxMapControl m_MapControl)
        {
            IArray geoArray = new ArrayClass();
            geoArray.Add(geo);
            HookHelperClass m_pHookHelper = new HookHelperClass();
            m_pHookHelper.Hook = m_MapControl.Object;
            IHookActions hookActions = (IHookActions)m_pHookHelper;
            hookActions.DoActionOnMultiple(geoArray, esriHookActions.esriHookActionsCallout);
            Application.DoEvents();
            m_pHookHelper.ActiveView.ScreenDisplay.UpdateWindow();
            hookActions.DoActionOnMultiple(geoArray, esriHookActions.esriHookActionsPan);
        }
    }
}
