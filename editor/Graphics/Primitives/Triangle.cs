using System;
using System.Drawing;
using EditorProject.Graphics.Primitives.Abstractions;

namespace EditorProject.Graphics.Primitives
{
    internal class Triangle : PointPrimitive, IDrawableHandle
    {
        public Triangle(int x, int y, float height)
        {
            X = x;
            Y = y;
            H = height;
            A = new Point(X, (int) (height * Math.Sqrt(3) / 3 + Y));
            CalculateVertices();
        }

        public Point A { get; private set; }

        public float H { get; }

        public int Y { get; }

        public int X { get; }

        public void CurrentPosition(Point location)
        {
            A = new Point(location.X, location.Y);
            Points.Clear();
            CalculateVertices();
        }

        private void CalculateVertices()
        {
            var x1 = A.X - X;
            var y1 = A.Y - Y;

            Points.Add(new Point(x1 + X, y1 + Y));

            Points.Add(new Point((int) (-0.5 * x1 - Math.Sqrt(3) * y1 / 2) + X,
                (int) (Math.Sqrt(3) * x1 / 2 - 0.5 * y1) + Y));

            Points.Add(new Point((int) (-0.5 * x1 + Math.Sqrt(3) * y1 / 2) + X,
                (int) (-Math.Sqrt(3) * x1 / 2 - 0.5 * y1) + Y));
        }
    }
}