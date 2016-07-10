using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;

namespace PipeLine.BaseComm
{
    /// <summary>
    /// Summary description for TrackLine.
    /// </summary>
    [Guid("9da3471b-98ed-4d4c-9562-dcaf53c194dc")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("PipeLine.BaseComm.TrackLine")]
    public sealed class TrackLine : BaseTool
    {
        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);

            //
            // TODO: Add any COM registration code here
            //
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);

            //
            // TODO: Add any COM unregistration code here
            //
        }

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            ControlsCommands.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            ControlsCommands.Unregister(regKey);

        }

        #endregion
        #endregion

        //private IHookHelper m_hookHelper;
        private IHookHelper m_hookHelper;
        private INewLineFeedback m_LineFeedback;
        private AxMapControl m_axMapControl;
        public TrackLine(AxMapControl axMapControl)
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = ""; //localizable text 
            base.m_caption = "tianjia";  //localizable text 
            base.m_message = "";  //localizable text
            base.m_toolTip = "";  //localizable text
            base.m_name = "";   //unique id, non-localizable (e.g. "MyCategory_MyTool")
            m_axMapControl = axMapControl;
            try
            {
                //
                // TODO: change resource name if necessary
                //
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
                base.m_cursor = new System.Windows.Forms.Cursor(GetType(), GetType().Name + ".cur");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overridden Class Methods

        /// <summary>
        /// Occurs when this tool is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            if (m_hookHelper == null)
                m_hookHelper = new HookHelperClass();

            m_hookHelper.Hook = hook;

            // TODO:  Add TrackLine.OnCreate implementation
        }

        /// <summary>
        /// Occurs when this tool is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add TrackLine.OnClick implementation
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add TrackLine.OnMouseDown implementation
            // TODO:  Add TrackLine.OnMouseDown implementation
            if (Button == 2) //右键
                return;
            // TODO:  Add myAddLine.OnMouseDown implementation
            IPoint pt = m_axMapControl.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            if (m_LineFeedback == null)
            {
                m_LineFeedback = new NewLineFeedbackClass();
                m_LineFeedback.Display = m_axMapControl.ActiveView.ScreenDisplay;
                m_LineFeedback.Start(pt);

            }
            else
            {
                m_LineFeedback.AddPoint(pt);
            }
            #region 绘制结点的第一种方法,在绘制过程中添加结点符号,效果类似于画线编辑时的效果
            ////设置结点符号
            //IRgbColor pRGB = new RgbColorClass();
            //pRGB.Red = 255;
            //pRGB.Green = 0;
            //pRGB.Blue = 0;

            //ISimpleMarkerSymbol pSimpleMarkSymbol = new SimpleMarkerSymbolClass();
            //pSimpleMarkSymbol.Color = pRGB as IColor;
            //pSimpleMarkSymbol.Size = 4;
            //pSimpleMarkSymbol.Style = esriSimpleMarkerStyle.esriSMSSquare;

            //IMarkerElement pMarkerElement = new MarkerElementClass();
            //pMarkerElement.Symbol = pSimpleMarkSymbol as IMarkerSymbol;
            //IElement ele = pMarkerElement as IElement;
            //ele.Geometry = pt as IGeometry;

            ////获取结点element的范围,以便确定刷新范围
            //IEnvelope pEnvBounds = new EnvelopeClass();
            //ele.QueryBounds(m_axMapControl.ActiveView.ScreenDisplay as IDisplay, pEnvBounds);

            //IGraphicsContainer pGra = m_axMapControl.ActiveView as IGraphicsContainer;
            //pGra.AddElement(ele, 0);
            //m_axMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, ele, pEnvBounds); //局部刷新,第二次参数必须是ele,如果是null,画线过程中线就没了,被刷新没了.
            #endregion
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add TrackLine.OnMouseMove implementation
            if (m_LineFeedback == null)
                return;
            IPoint pt = m_axMapControl.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            m_LineFeedback.MoveTo(pt);
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add TrackLine.OnMouseUp implementation
        }
        public override void OnDblClick()
        {
            IPolyline pPolyline = null;
            IGroupElement pGroupElement = new GroupElementClass();

            try
            {
                if (m_LineFeedback == null)
                    return;
                pPolyline = m_LineFeedback.Stop();
                if (pPolyline == null)
                {
                    m_LineFeedback = null;
                    return;
                }
                //设置线型
                IRgbColor pRGB = new RgbColorClass();
                pRGB.Red = 0;
                pRGB.Green = 120;
                pRGB.Blue = 0;

                ISimpleLineSymbol pSimplelineSymbol = new SimpleLineSymbolClass();
                pSimplelineSymbol.Color = pRGB as IColor;
                pSimplelineSymbol.Width = 1.5;
                pSimplelineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;

                //m_LineFeedback.Symbol = pSimplelineSymbol as ISymbol;

                ILineElement pLineElement = new LineElementClass();
                pLineElement.Symbol = pSimplelineSymbol as ILineSymbol;
                IElement element = pLineElement as IElement;
                element.Geometry = pPolyline as IGeometry;
                pGroupElement.AddElement(element);

                #region 绘制结点的第二种方法,全部结束时绘制结点
                //设置结点符号
                pRGB = new RgbColorClass();
                pRGB.Red = 255;
                pRGB.Green = 0;
                pRGB.Blue = 0;

                ISimpleMarkerSymbol pSimpleMarkSymbol = new SimpleMarkerSymbolClass();
                pSimpleMarkSymbol.Color = pRGB as IColor;
                pSimpleMarkSymbol.Size = 4;
                pSimpleMarkSymbol.Style = esriSimpleMarkerStyle.esriSMSSquare;

                //获取得到结点
                IPointCollection pPointCol = (IPointCollection)pPolyline;
                for (int index = 0; index < pPointCol.PointCount; index++)
                {
                    IPoint pt = pPointCol.get_Point(index);
                    IMarkerElement pMarkerElement = new MarkerElementClass();
                    pMarkerElement.Symbol = pSimpleMarkSymbol as IMarkerSymbol;
                    IElement ele = pMarkerElement as IElement;
                    ele.Geometry = pt as IGeometry;
                    pGroupElement.AddElement(ele);
                }
                MessageBox.Show(pPointCol.PointCount.ToString());
                #endregion
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);

                return;
            }
            finally
            {

                IGraphicsContainer pGraphicsContainer = m_axMapControl.ActiveView as IGraphicsContainer;
                pGraphicsContainer.AddElement(pGroupElement as IElement, ((IPointCollection)pPolyline).PointCount);
                m_axMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, pGroupElement, pPolyline.Envelope);
                //释放变量
                m_LineFeedback = null;
                //MessageBox.Show();
            }
        }
        #endregion
    }
}
