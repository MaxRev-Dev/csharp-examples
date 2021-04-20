using System.Drawing;

namespace EditorProject.Graphics.Primitives.Abstractions
{
    internal interface IDrawable
    {
        bool IsEmpty { get; }
        bool IsErased { get; }
        void Draw(GraphicContext context, Pen pen);
        void Erase(Point location, float radius);
        bool HasPointInRadius(Point location, float radius);
        bool TryGetPointInRadius(Point location, float radius, out Point point);
    }
}