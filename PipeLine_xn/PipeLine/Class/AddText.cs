using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Analyst3D;

namespace PipeLine.Class
{
    class AddText
    {
        public IBalloonCallout createBalloonCallout(double x, double y)
        {
            IRgbColor rgb = new RgbColorClass();
            {
                rgb.Red = 255;
                rgb.Green = 255;
                rgb.Blue = 200;
            }
            ISimpleFillSymbol sfs = new SimpleFillSymbolClass();
            {
                sfs.Color = rgb;
                sfs.Style = esriSimpleFillStyle.esriSFSSolid;
            }
            IPoint p = new PointClass();
            {
                p.PutCoords(x, y);
            }
            IBalloonCallout bc = new BalloonCalloutClass();
            {
                bc.Style = esriBalloonCalloutStyle.esriBCSRoundedRectangle;
                bc.Symbol = sfs;
                bc.AnchorPoint = p;
            }
            return bc;
        }

        public ITextElement createTextElement(AxMapControl m_MapControl, double x, double y, string text)
        {
            IBalloonCallout bc = createBalloonCallout(x, y);
            IRgbColor rgb = new RgbColorClass();
            {
                rgb.Red = 255;
                rgb.Green = 0;
                rgb.Blue = 0;
            }
            ITextSymbol ts = new TextSymbolClass();
            {
                ts.Color = rgb;
            }
            IFormattedTextSymbol fts = ts as IFormattedTextSymbol;
            {
                fts.Background = bc as ITextBackground;
            }
            ts.Size = 10;
            IPoint point = new PointClass();
            {
                double width = m_MapControl.Extent.Width / 20;
                double height = m_MapControl.Extent.Height / 30;
                point.PutCoords(x + width, y + height);
            }
            ITextElement te = new TextElementClass();
            {
                te.Symbol = ts;
                te.Text = text;
            }
            IElement e = te as IElement;
            {
                e.Geometry = point;
            }
            return te;
        }

        public void Init3DText(IPoint _locatePoint3, AxSceneControl m_axSceneConctrol)
        {

            IGraphicsLayer graphicsLayer;
            IText3DElement text3DElement;
            IGraphicsContainer3D graphicsContainer3D = null;
            graphicsLayer = new GraphicsLayer3DClass();
            text3DElement = new Text3DElementClass();
            graphicsContainer3D = graphicsLayer as IGraphicsContainer3D;

            text3DElement.FontName = "name";
            text3DElement.Text = "开始";
            text3DElement.AnchorPoint = _locatePoint3;
            text3DElement.Depth = 0.1;
            text3DElement.Height = 0.5;
            text3DElement.BoldFont = true;
            text3DElement.RotationAngle = 360;
            ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbol();
            IRgbColor color = new RgbColorClass();
            color.Red = 255;
            simpleFillSymbol.Color = color;
            IFillShapeElement fillShapeElement = text3DElement as IFillShapeElement;
            fillShapeElement.Symbol = simpleFillSymbol as IFillSymbol;
            graphicsContainer3D.AddElement(text3DElement as IElement);
            text3DElement.Update();//这里如果不添加update就显示不出来了
            m_axSceneConctrol.Scene.AddLayer(graphicsLayer as ILayer);
            m_axSceneConctrol.SceneGraph.Invalidate(graphicsLayer, true, false);
            m_axSceneConctrol.SceneGraph.RefreshViewers();

        }


    }
}
