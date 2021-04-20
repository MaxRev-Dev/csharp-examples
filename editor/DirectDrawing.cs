using System;
using System.Drawing;
using System.Windows.Forms;
using EditorProject.Graphics.Primitives;
using EditorProject.Graphics.Primitives.Abstractions;

namespace EditorProject
{
    public partial class Playground
    {
        #region Direct Drawing

        private void Playground_SizeChanged(object sender, EventArgs e)
        {
            CreatePlayground(); 
            _updateRequired = true;
        }

        private void CreatePlayground()
        {
            _bitmap?.Dispose();
            _context?.Graphics?.Dispose();
            if (!GraphicsValid) return;
            _bitmap = new Bitmap(drawingCanvas.Bounds.Width, drawingCanvas.Bounds.Height); 
            _context?.Reset(_bitmap);
            _updateRequired = true;
        }

        private void InitModelAndFrameTick()
        {
            var timer = new Timer {Interval = 10};
            timer.Start();
            timer.Tick += OnFrame;
        }

        private void OnFrame(object sender, EventArgs e)
        {
            if (!GraphicsValid || !_updateRequired) return;

            // This will clear everything in current layer
            // Even the loaded image...
            // So just assume we are using vector editor
            _context.Graphics.Clear(drawingCanvas.BackColor);

            foreach (var drawable in _drawablePool)
                drawable.Draw(_context, _drawPen);
            drawingCanvas.Invalidate();
            _updateRequired = false;

            for (var i = 0; i < _drawablePool.Count; i++)
                if (_drawablePool[i].IsErased)
                    _drawablePool.RemoveAt(i);
        }
        
        private bool GraphicsValid=>
            !(drawingCanvas.Bounds.IsEmpty ||
              drawingCanvas.Bounds.Height == 0 ||
              drawingCanvas.Bounds.Width == 0);

        private void DrawingCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isPenDown)
            {
                HandlePosition(e);
                _updateRequired = true;
            }
        }

        private void DrawingCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            _isPenDown = false;
            HandlePosition(e);
            _updateRequired = true;
        }

        private void DrawingCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            _isPenDown = true;
            _updateRequired = true;

            if (e.Button == MouseButtons.Left)
            {
                _currentPenContext = GetDrawableType(e);
                _drawablePool.Add(_currentPenContext);
            }

            HandlePosition(e);
        }

        private PointPrimitive GetDrawableType(MouseEventArgs e)
        {
            if (_currentTool != default)
                return _currentTool(e.Location.X, e.Location.Y);
            return new Curve();
        }

        private void HandlePosition(MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    switch (_currentPenContext)
                    {
                        case Curve c:
                            c.Add(e.Location);
                            break;
                        case IDrawableHandle handle:
                            handle.CurrentPosition(
                                TrySnap(e.Location, 10, out var p)
                                    ? p
                                    : e.Location);
                            break;
                    }

                    break;
                case MouseButtons.None:
                    break;
                case MouseButtons.Right:
                    Erase(e.Location);
                    break;
                case MouseButtons.Middle:
                    break;
                case MouseButtons.XButton1:
                    break;
                case MouseButtons.XButton2:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private bool TrySnap(Point location, float snapDist, out Point result)
        {
            foreach (var drawable in _drawablePool)
                if (drawable.TryGetPointInRadius(location, snapDist, out var p))
                {
                    result = p;
                    return true;
                }

            result = default;
            return false;
        }

        private void Erase(Point location)
        {
            foreach (var drawable in _drawablePool) drawable.Erase(location, 5);
        }

        #endregion
    }
}