using System;
using System.Collections.Generic;
using System.Drawing;

namespace EditorProject.Graphics.Primitives.Abstractions
{
    internal abstract class PointPrimitive : IDrawable
    {
        protected List<Point> Points;
        protected List<Point> Removed;

        protected PointPrimitive()
        {
            Points = new List<Point>();
            Removed = new List<Point>();
        }

        protected virtual bool ClosePolygon { get; set; } = true;

        public virtual bool IsEmpty => Points.Count <= 1;
        public virtual bool IsErased => Points.Count < 2 && Removed.Count > 1;

        public virtual void Draw(GraphicContext context, Pen pen)
        {
            if (Points == default) return;
            if (Points.Count <= 1) return;
            for (var i = 1; i < Points.Count; i++)
            {
                if (Removed.Contains(Points[i - 1]))
                    continue;
                if (Removed.Contains(Points[i]))
                    continue;
                context.Graphics.DrawLine(pen, Points[i - 1], Points[i]);
            }

            if (ClosePolygon)
            {
                if (Removed.Contains(Points[0]))
                    return;
                if (Removed.Contains(Points[^1]))
                    return;
                context.Graphics.DrawLine(pen, Points[0], Points[^1]);
            }
        }

        public virtual void Erase(Point location, float radius)
        {
            foreach (var point in Points)
                if (InRadius(radius, (point.X, point.Y), (location.X, location.Y)))
                    if (!Removed.Contains(point))
                        Removed.Add(point);
        }

        public bool HasPointInRadius(Point location, float radius)
        {
            foreach (var point in Points)
                if (InRadius(radius, (point.X, point.Y), (location.X, location.Y)))
                    return true;

            return false;
        }

        public bool TryGetPointInRadius(Point location, float radius, out Point point)
        {
            foreach (var px in Points)
                if (InRadius(radius, (px.X, px.Y), (location.X, location.Y)))
                {
                    point = px;
                    return true;
                }

            point = default;
            return false;
        }

        protected static bool InRadius(float safeRadius, (float x, float y) a, (float x, float y) b)
        {
            return Math.Pow(b.x - a.x, 2) + Math.Pow(b.y - a.y, 2) <= Math.Pow(safeRadius, 2);
        }
    }
}