using System;
using System.Diagnostics;
using CoreGraphics;
using Microsoft.Maui.Graphics.Native;
using UIKit;

namespace Microsoft.Maui.Graphics.Controls
{
    public class GraphicsEditor : UITextView, IMixedNativeView
    {
        readonly NativeCanvas _canvas;
        readonly UITapGestureRecognizer _tapGesture;

        CGColorSpace? _colorSpace;
        IMixedGraphicsHandler? _graphicsControl;
        IDrawable? _drawable;
        CGRect _lastBounds;
        UIEdgeInsets _eddgeInsets;

        public GraphicsEditor()
        {
            _canvas = new NativeCanvas(() => CGColorSpace.CreateDeviceRGB());

            EdgeInsets = UIEdgeInsets.Zero;
            ClipsToBounds = true;
            BackgroundColor = UIColor.Clear; 
            
            _tapGesture = new UITapGestureRecognizer(OnTap)
            {
                NumberOfTapsRequired = 1
            };
            AddGestureRecognizer(_tapGesture);
        }

        public IMixedGraphicsHandler? GraphicsControl
        {
            get => _graphicsControl;
            set => Drawable = _graphicsControl = value;
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

        public UIEdgeInsets EdgeInsets
        {
            get => _eddgeInsets;
            set
            {
                _eddgeInsets = value;

                TextContainerInset = _eddgeInsets;
            }
        }

        static readonly string[] DefaultNativeLayers = new[] { nameof(IEntry.Text) };

        public string[] NativeLayers => DefaultNativeLayers;

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
                
        public void DrawBaseLayer(RectangleF dirtyRect)
        {
            base.Draw(dirtyRect);
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

        void OnTap()
        {
            try
            {
                if (!IsFirstResponder)
                    BecomeFirstResponder();

                var locationInView = _tapGesture.LocationInView(_tapGesture.View);
                PointF interceptPoint = locationInView.AsPointF();
                PointF[] tapPoints = new PointF[] { interceptPoint };
                GraphicsControl?.StartInteraction(tapPoints);
            }
            catch (Exception exc)
            {
                Debug.WriteLine("An unexpected error occured handling a touch event within the control.", exc);
            }
        }
    }
}