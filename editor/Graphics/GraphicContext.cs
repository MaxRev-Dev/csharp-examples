using System.Drawing;

namespace EditorProject.Graphics
{
    internal class GraphicContext
    {
        private System.Drawing.Graphics _graphics;

        public System.Drawing.Graphics Graphics =>
            _graphics ??= GetBitmapGraphics(Bitmap);

        public Bitmap Bitmap { get; private set; }

        public float DrawableWidth => Graphics?.VisibleClipBounds.Width ?? 0;

        public float DrawableHeight => Graphics?.VisibleClipBounds.Height ?? 0;

        public void Reset(Bitmap bitmap)
        {
            if (bitmap == default) return;
            Bitmap = bitmap;
            _graphics?.Dispose();
            _graphics = GetBitmapGraphics(bitmap);
        }

        private static System.Drawing.Graphics GetBitmapGraphics(Image bitmap)
        {
            return bitmap != default ? System.Drawing.Graphics.FromImage(bitmap) : default;
        }
    }
}