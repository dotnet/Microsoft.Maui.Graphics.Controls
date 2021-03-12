using GraphicsControls;
using GraphicsControls.GTK;
using System.ComponentModel;
using System.Graphics;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.GTK;
using SkiaSharp.Views.Gtk;
using SkiaSharp.Views.Desktop;
using System.Graphics.Skia;
using Gdk;
using GPoint = System.Graphics.Point;

[assembly: ExportRenderer(typeof(GraphicsView), typeof(GraphicsViewRenderer))]
namespace GraphicsControls.GTK
{
    public class NativeGraphicsView : SKWidget
    {
        IDrawable _drawable;
        readonly SkiaCanvas _canvas;
        readonly ScalingCanvas _scalingCanvas;

        public NativeGraphicsView(IDrawable drawable = null)
        {
            _canvas = new SkiaCanvas();
            _scalingCanvas = new ScalingCanvas(_canvas);
            Drawable = drawable;
        }

        public IDrawable Drawable
        {
            get => _drawable;
            set
            {
                _drawable = value;
                QueueDraw();
            }
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            if (_drawable == null) return;

            var skiaCanvas = e.Surface.Canvas;
            skiaCanvas.Clear();

            _canvas.Canvas = skiaCanvas;
            _drawable.Draw(_scalingCanvas, new RectangleF(0, 0, CanvasSize.Width, CanvasSize.Height));
        }
    }

    [Preserve(AllMembers = true)]
    public class GraphicsViewRenderer : ViewRenderer<GraphicsView, NativeGraphicsView>
    {
        [Preserve(AllMembers = true)]
        public static void Init()
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<GraphicsView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                e.OldElement.Invalidated -= OnDrawInvalidated;
            }

            if (e.NewElement != null)
            {
                e.NewElement.Invalidated += OnDrawInvalidated;

                SetNativeControl(new NativeGraphicsView(Element));

                Control.ShowAll();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        }

        protected override bool OnButtonPressEvent(EventButton evnt)
        {
            Element?.OnTouchDown(new GPoint(evnt.X, evnt.Y));

            return base.OnButtonPressEvent(evnt);
        }

        protected override bool OnButtonReleaseEvent(EventButton evnt)
        {
            Element?.OnTouchUp(new GPoint(evnt.X, evnt.Y));

            return base.OnButtonReleaseEvent(evnt);
        }

        void OnDrawInvalidated(object sender, System.EventArgs e)
        {
            Control?.QueueDraw();
        }
    }
}