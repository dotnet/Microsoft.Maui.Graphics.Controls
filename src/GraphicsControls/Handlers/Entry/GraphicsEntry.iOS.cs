using System;
using CoreGraphics;
using Microsoft.Maui.Graphics.Native;
using UIKit;

namespace Microsoft.Maui.Graphics.Controls
{
    public class GraphicsEntry : UITextField, IInvalidate
    {
        readonly NativeCanvas _canvas;
        CGColorSpace? _colorSpace;
        IDrawable? _drawable;
        CGRect _lastBounds;

        public GraphicsEntry()
        {
            _canvas = new NativeCanvas(() => CGColorSpace.CreateDeviceRGB());

            EdgeInsets = UIEdgeInsets.Zero;
            BorderStyle = UITextBorderStyle.None;
            ClipsToBounds = true;
            BackgroundColor = UIColor.Clear;
        }

        public IDrawable? Drawable
        {
            get => _drawable;
            set
            {
                _drawable = value;

                if (_drawable != null)
                    SetNeedsDisplay();
            }
        }

        public UIEdgeInsets EdgeInsets { get; set; }

        public void Invalidate()
        {
            SetNeedsDisplay();
        }

        public override CGRect Bounds
        {
            get => base.Bounds;

            set
            {
                var newBounds = value;

                if (_lastBounds.Width != newBounds.Width || _lastBounds.Height != newBounds.Height)
                {
                    base.Bounds = value;

                    Invalidate();

                    _lastBounds = newBounds;
                }
            }
        }

        public override CGRect TextRect(CGRect forBounds)
        {
            return base.TextRect(InsetRect(forBounds, EdgeInsets));
        }

        public override CGRect EditingRect(CGRect forBounds)
        {
            return base.EditingRect(InsetRect(forBounds, EdgeInsets));
        }

        public static CGRect InsetRect(CGRect rect, UIEdgeInsets insets)
        {
            return new CGRect(
                rect.X + insets.Left,
                rect.Y + insets.Top,
                rect.Width - insets.Left - insets.Right,
                rect.Height - insets.Top - insets.Bottom);
        }

        public override void Draw(CGRect dirtyRect)
        {
            base.Draw(dirtyRect);

            if (_drawable == null)
                return;

            var coreGraphics = UIGraphics.GetCurrentContext();

            if (_colorSpace == null)
                 _colorSpace = CGColorSpace.CreateDeviceRGB();
            
            coreGraphics.SetFillColorSpace(_colorSpace);
            coreGraphics.SetStrokeColorSpace(_colorSpace);

            Draw(coreGraphics, dirtyRect.AsRectangleF());
        }

        void Draw(CGContext coreGraphics, RectangleF dirtyRect)
        {
            _canvas.Context = coreGraphics;

            try
            {
                _drawable?.Draw(_canvas, dirtyRect);
            }
            catch (Exception exc)
            {
                Logger.Error("An unexpected error occurred rendering the drawing.", exc);
            }
            finally
            {
                _canvas.Context = null;
            }
        }
    }
}