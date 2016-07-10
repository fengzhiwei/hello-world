using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using System.Windows.Forms;
using stdole;

namespace PipeLine.Class
{
    /// <summary>
    /// 长度、角度的标尺丈量
    /// </summary>
    class Length
    {
        //根据RGB值得到IColor
        public static IRgbColor getColor(int R, int G,int B)
        {
            IRgbColor color = new RgbColorClass();
            color.Red = R;
            color.Green = G;
            color.Blue = B;
            return color;
        }
        public void DrawLine(AxMapControl axMapControl, IPolyline ppPolyline)
        {
            IGeometry GeometryLine;
            IGraphicsContainer pGrapgicsContainer = axMapControl.Map as IGraphicsContainer;
            IActiveView ActiveView_line = axMapControl.Map as IActiveView;
            ISimpleLineSymbol plineSybol = new SimpleLineSymbolClass();
            plineSybol.Color = getColor(0, 255,0);
            plineSybol.Width = 1;
            GeometryLine = ppPolyline;
            if (GeometryLine != null)
            {
                ILineElement pLineElement = new LineElementClass();
                pLineElement.Symbol = plineSybol;
                IElement ppElement = pLineElement as IElement;
                ppElement.Geometry = GeometryLine;
                IGraphicsContainer pGraphicsContainer = axMapControl.Map as IGraphicsContainer;
                pGrapgicsContainer.AddElement(ppElement, 0);
            }
            else
            {
                return;
            }
            IRgbColor pRGB = getColor(255,0,0);
            //pRGB = new RgbColorClass();
            //pRGB.Red = 255;
            //pRGB.Green = 0;
            //pRGB.Blue = 0;

            ISimpleMarkerSymbol pSimpleMarkSymbol = new SimpleMarkerSymbolClass();
            pSimpleMarkSymbol.Color = pRGB as IColor;
            pSimpleMarkSymbol.Size = 1.5;
            pSimpleMarkSymbol.Style = esriSimpleMarkerStyle.esriSMSSquare;

            IPointCollection pPointCol = (IPointCollection)ppPolyline; //定义节点
            List<IPolyline> PolyLineList = new List<IPolyline>();//定义每条边
            List<IPoint> midPoint = new List<IPoint>();//每条边的中点
            List<double> PolyLine_Angle = new List<double>();//每条边的角度
            List<double> Point_Angle = new List<double>();//节点处锐角
            IPolyline mPolyLine;
            //查找节点
            for (int index = 0; index < pPointCol.PointCount; index++)
            {

                if (index > 0)
                {
                    mPolyLine = new PolylineClass();
                    mPolyLine.FromPoint = pPointCol.get_Point(index - 1);
                    mPolyLine.ToPoint = pPointCol.get_Point(index);
                    PolyLineList.Add(mPolyLine);
                }

                IPoint pt = pPointCol.get_Point(index);
                IMarkerElement pMarkerElement = new MarkerElementClass();
                pMarkerElement.Symbol = pSimpleMarkSymbol as IMarkerSymbol;
                IElement ele = pMarkerElement as IElement;
                ele.Geometry = pt as IGeometry;
                double proportion = Math.Round(axMapControl.MapScale, 0);
                //节点处的圆圈
                if (index > 0 & index < pPointCol.PointCount - 1)
                {
                    DrawCircle_Graphics(axMapControl, pt, 2.0 * proportion / 1428);
                    //DrawCircle_Graphics(axMapControl, pt, 1.0 * proportion / 1428);
                }
                else
                {
                    continue;
                }
                pGrapgicsContainer.AddElement(ele, 0);
            }

            IPoint _midPoint;
            double _Angle;
            //查找中点和各线段在坐标系中的角度
            for (int k = 0; k < PolyLineList.Count; k++)
            {
                _Angle = 0;
                //求每跳边的中点
                _midPoint = new PointClass();
                PolyLineList[k].QueryPoint(esriSegmentExtension.esriNoExtension, PolyLineList[k].Length / 2, false, _midPoint);
                midPoint.Add(_midPoint);
                //求每条边的角度
                if (PolyLineList[k].FromPoint.X - PolyLineList[k].ToPoint.X == 0)
                {
                    _Angle = 90;
                }
                else
                {
                    _Angle = 180 * Math.Atan2(PolyLineList[k].FromPoint.Y - PolyLineList[k].ToPoint.Y, PolyLineList[k].FromPoint.X - PolyLineList[k].ToPoint.X) / Math.PI;
                    
                    if (_Angle < 0)
                    {
                        _Angle = _Angle + 180;
                    }
                    if (_Angle > 90 & _Angle <= 180)
                    {
                        _Angle = _Angle - 180;
                    }
                }
                PolyLine_Angle.Add(_Angle);
                AddLable(axMapControl, midPoint[k], Math.Round(PolyLineList[k].Length, 2).ToString(), PolyLine_Angle[k]);
            }
            //_Angle = 180 * Math.Atan2(1, Math.Pow(3,0.5)) / Math.PI;
            //增加距离标签
            //for (int j = 0; j < PolyLineList.Count; j++)
            //{
            //    AddLable(axMapControl, midPoint[j], Math.Round(PolyLineList[j].Length,2).ToString(), PolyLine_Angle[j]);
            //}
            //显示角度
            for (int m = 1; m < pPointCol.PointCount - 1; m++)
            {
                IPoint pt = pPointCol.get_Point(m);
                Point_Angle.Add(FromCosToAngle(pPointCol.get_Point(m-1),pPointCol.get_Point(m),pPointCol.get_Point(m+1)));
                AddLableAngle(axMapControl, pt, (Math.Round(Point_Angle[m - 1],2)).ToString(), 0);
            }
            
            ActiveView_line.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, axMapControl.Extent);
        }

        public void AddLable(AxMapControl axMapControl, IPoint pPoint, string PolyLine_length, double angle)
        {
            IRgbColor pColor = new RgbColorClass()
            {
                Red = 255,
                Blue = 0,
                Green = 0
            };
            IFontDisp pFont = new StdFont()
            {
                Name = "宋体",
                Size = 25
            } as IFontDisp;

            ITextSymbol pTextSymbol = new TextSymbolClass()
            {
                Color = pColor,
                Font = pFont,
                Size = 13
            };
            pTextSymbol.Angle = angle;
            IGraphicsContainer pGraContainer = axMapControl.Map as IGraphicsContainer;
            //IEnvelope pEnv = null;
            ITextElement pTextElment = null;
            IElement pEle = null;
            pTextElment = new TextElementClass()
            {
                Symbol = pTextSymbol,
                ScaleText = true,
                Text = PolyLine_length + "米"
            };
            pEle = pTextElment as IElement;
            pEle.Geometry = pPoint;
            //添加标注
            pGraContainer.AddElement(pEle, 0);
            //(axMapControl.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, axMapControl.Extent);
        }
        public void AddLableAngle(AxMapControl axMapControl, IPoint pPoint, string _angle, double angle)
        {
            IRgbColor pColor = new RgbColorClass()
            {
                Red = 255,
                Blue = 0,
                Green = 0
            };
            IFontDisp pFont = new StdFont()
            {
                Name = "宋体",
                Size = 10
            } as IFontDisp;

            ITextSymbol pTextSymbol = new TextSymbolClass()
            {
                Color = pColor,
                Font = pFont,
                Size = 8
            };
            pTextSymbol.Angle = angle;
            IGraphicsContainer pGraContainer = axMapControl.Map as IGraphicsContainer;
            //IEnvelope pEnv = null;
            ITextElement pTextElment = null;
            IElement pEle = null;
            pTextElment = new TextElementClass()
            {
                Symbol = pTextSymbol,
                ScaleText = true,
                Text = "           " + _angle + "°"
            };
            pEle = pTextElment as IElement;
            pEle.Geometry = pPoint;
            //添加标注
            pGraContainer.AddElement(pEle, 0);
            //(axMapControl.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, axMapControl.Extent);
        }
        /// <summary>
        /// 生成圆
        /// </summary>
        /// <param name="pPoint"></param>
        /// <param name="radius"></param>
        public void DrawCircle_Graphics(AxMapControl axMapControl, IPoint pPoint, double radius)
        {
            //定义颜色
            IRgbColor pColor = getColor(255,0,0);
            //pColor.RGB = System.Drawing.Color.FromArgb(255, 0, 0).ToArgb();
            pColor.Transparency = 255;
            ILineSymbol pLineSymbol = new SimpleLineSymbolClass();
            pLineSymbol.Width = 1;
            pLineSymbol.Color = pColor;

            pColor = getColor(0, 0,255 );
            pColor.Transparency = 30;
            IFillSymbol pFillSymbol = new SimpleFillSymbolClass();
            pFillSymbol.Color = pColor;
            pFillSymbol.Outline = pLineSymbol;

            IConstructCircularArc pConstructCircularArc = new CircularArcClass();
            pConstructCircularArc.ConstructCircle(pPoint, radius, false);
            //circularArc.PutCoordsByAngle(origin, Math.PI / 3, Math.PI / 2, 100);
            ICircularArc pArc = pConstructCircularArc as ICircularArc;
            //pArc.PutCoordsByAngle(pPoint, Math.PI / -2, Math.PI / 2, 1);
            ISegment pSegment = pArc as ISegment;
            ISegmentCollection pSegmentCollection = new RingClass();
            object obj = Type.Missing;
            pSegmentCollection.AddSegment(pSegment, ref obj, ref obj);

            IRing pRing = pSegmentCollection as IRing;
            pRing.Close();

            IGeometryCollection pGeometryColl = new PolygonClass();
            pGeometryColl.AddGeometry(pRing, ref obj, ref obj);
            IElement pElement = new CircleElementClass();
            pElement.Geometry = pGeometryColl as IGeometry;

            IFillShapeElement pFillShapeElment = pElement as IFillShapeElement;
            pFillShapeElment.Symbol = pFillSymbol;
            IGraphicsContainer pGC = axMapControl.ActiveView.GraphicsContainer;
            pGC.AddElement(pElement, 0);
            axMapControl.Refresh();
        }
        /// <summary>
        /// 已知A、O、B，三点，求角AOB的大小
        /// </summary>
        /// <param name="PointA"></param>
        /// <param name="PointO"></param>
        /// <param name="PointB"></param>
        /// <returns></returns>
        public double FromCosToAngle(IPoint PointA, IPoint PointO, IPoint PointB)
        {
            double angle = 0;
            double xA = PointA.X; double yA = PointA.Y;
            double xO = PointO.X; double yO = PointO.Y;
            double xB = PointB.X; double yB = PointB.Y;
            double dot_product = (xA * xB - xO * (xA + xB) + xO * xO) + (yA * yB - yO * (yA + yB) + yO * yO);
            double dot_length = Math.Pow((Math.Pow(xA - xO, 2) + Math.Pow(yA - yO, 2)), 0.5) * Math.Pow((Math.Pow(xB - xO, 2) + Math.Pow(yB - yO, 2)), 0.5);
            double Cos = dot_product / dot_length;
            if(Cos >= -1 & Cos <= 1)
            {
                angle = 180 * Math.Acos(Cos) / Math.PI;
            }
            return angle;
        }
       

    }
}
