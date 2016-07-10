using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;

namespace PipeLine.Class
{
    class DrawLine
    {
        private static object _missing = Type.Missing;
        public IElement drawline(IPoint pt1,IPoint pt2) 
        { 
                IPointCollection axisPointCollection = new PolylineClass();
                axisPointCollection.AddPoint(pt1 , ref _missing, ref _missing);
                axisPointCollection.AddPoint(pt2, ref _missing, ref _missing);

                IZAware zAware = axisPointCollection as IZAware;
                zAware.ZAware = true;
                IGeometry geometry = axisPointCollection as IGeometry;
                IElement element = new LineElementClass();
                element.Geometry = geometry;
                return element;
        }
    }
}
