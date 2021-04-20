using System.Drawing;
using EditorProject.Graphics.Primitives.Abstractions;

namespace EditorProject.Graphics.Primitives
{
    internal class Rectangle : PointPrimitive, IDrawableHandle
    {
        public Rectangle(int x, int y, int height, int width)
        {
            X = x;
            Y = y;
            Height = height;
            Width = width;
            CalculateVertices();
        }

        public int Y { get; }
        public int Height { get; private set; }
        public int Width { get; private set; }
        public int X { get; }

        public void CurrentPosition(Point location)
        {
            Height = location.Y;
            Width = location.X;
            Points.Clear();
            CalculateVertices();
        }

        private void CalculateVertices()
        {
            Points.Add(new Point(X, Y));
            Points.Add(new Point(Width, Y));
            Points.Add(new Point(Width, Height));
            Points.Add(new Point(X, Height));
        }
    }
}