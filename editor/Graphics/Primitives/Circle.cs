using System;
using System.Drawing;
using System.Numerics;
using EditorProject.Graphics.Primitives.Abstractions;

namespace EditorProject.Graphics.Primitives
{
    internal class Circle : PointPrimitive, IDrawableHandle
    {
        public Circle(int x, int y, float radius)
        {
            X = x;
            Y = y;
            R = radius;

            CalculateVertices();
        }

        public float R { get; private set; }

        public int Y { get; }

        public int X { get; }

        public void CurrentPosition(Point location)
        {
            R = (new Vector2(location.X, location.Y) - new Vector2(X, Y)).Length();
            Points.Clear();
            CalculateVertices();
        }

        private void CalculateVertices()
        {
            var step = 2 * Math.PI / 32;
            for (var theta = 0d; theta < 2 * Math.PI; theta += step)
                Points.Add(new Point((int) (X + R * Math.Cos(theta)), (int) (Y - R * Math.Sin(theta))));
        }
    }
}