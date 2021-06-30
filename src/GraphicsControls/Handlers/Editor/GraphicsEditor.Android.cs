using Android.Content;
using Android.Graphics;
using AndroidX.AppCompat.Widget;
using Microsoft.Maui.Graphics.Native;

namespace Microsoft.Maui.Graphics.Controls
{
    public class GraphicsEditor : AppCompatEditText, IMixedNativeView
    {
        readonly NativeCanvas _canvas;
        readonly ScalingCanvas _scalingCanvas;
        readonly float _scale;

        int _width, _height;
        Color? _backgroundColor;
        IDrawable? _drawable;

        public GraphicsEditor(Context context) : base(context)
        {
            _scale = Resources?.DisplayMetrics?.Density ?? 1;
            _canvas = new NativeCanvas(context);
            _scalingCanvas = new ScalingCanvas(_canvas);

            Background = null;
        }

        public Color? BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                Invalidate();
            }
        }

        public IDrawable? Drawable
        {
            get => _drawable;
            set
            {
                _drawable = value;
                Invalidate();
            }
        }

        static readonly string[] DefaultNativeLayers = new[] { nameof(IEntry.Text) };

        public string[] NativeLayers => DefaultNativeLayers;

        public void DrawBaseLayer(RectangleF dirtyRect) { }

        public override void Draw(Canvas? androidCanvas)
        {
            if (_drawable == null)
                return;

            var dirtyRect = new RectangleF(0, 0, _width, _height);

            _canvas.Canvas = androidCanvas;

            if (_backgroundColor != null)
            {
                _canvas.FillColor = _backgroundColor;
                _canvas.FillRectangle(dirtyRect);
                _canvas.FillColor = Colors.White;
            }

            _scalingCanvas.ResetState();
            _scalingCanvas.Scale(_scale, _scale);

            dirtyRect.Height /= _scale;
            dirtyRect.Width /= _scale;
            _drawable.Draw(_scalingCanvas, dirtyRect);
            _canvas.Canvas = null;

            base.Draw(androidCanvas);
        }

        protected override void OnSizeChanged(int width, int height, int oldWidth, int oldHeight)
        {
            base.OnSizeChanged(width, height, oldWidth, oldHeight);
            _width = width;
            _height = height;
        }
    }
}