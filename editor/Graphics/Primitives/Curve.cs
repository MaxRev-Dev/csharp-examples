using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using EditorProject.Graphics.Primitives.Abstractions;

namespace EditorProject.Graphics.Primitives
{
    internal class Curve : PointPrimitive
    {
        public Curve(IEnumerable<Point> points)
        {
            Points = points.ToList();
        }

        public Curve()
        {
        }

        protected override bool ClosePolygon { get; set; } = false;

        public void Add(Point p)
        {
            Points.Add(p);
        }
    }
}