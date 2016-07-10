using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;

namespace PipeLine.BaseComm
{
    /// <summary>
    /// Summary description for DeleteLayer.
    /// </summary>
    [Guid("d3667570-7407-4739-af6f-43b1d0dde56e")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("PipeLine.BaseComm.DeleteLayer")]
    public sealed class DeleteLayer : BaseCommand
    {
        private IMapControl3 m_MapControl;
        private ILayer m_Layer;
        public DeleteLayer(ILayer layer)
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = ""; //localizable text
            base.m_caption = "É¾³ý";  //localizable text
            base.m_message = "";  //localizable text 
            base.m_toolTip = "";  //localizable text 
            base.m_name = "";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

            m_Layer = layer;
            
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

        #region Overridden Class Methods

        /// <summary>
        /// Occurs when this command is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            // TODO:  Add DeleteLayer.OnCreate implementation
            m_MapControl = (IMapControl3)hook;
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add DeleteLayer.OnClick implementation
            m_MapControl.Map.DeleteLayer(m_Layer);
        }

        #endregion
    }
}
