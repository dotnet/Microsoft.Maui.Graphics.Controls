using System;
using System.Graphics.Android;
using Android.Content;
using Android.Views;
using GraphicsControls.Android;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.Android;
using APointF = Android.Graphics.PointF;
using GraphicsView = GraphicsControls.GraphicsView;

[assembly: ExportRenderer(typeof(GraphicsView), typeof(GraphicsViewRenderer))]
namespace GraphicsControls.Android
{
    [Preserve(AllMembers = true)]
    public class GraphicsViewRenderer : ViewRenderer<GraphicsView, NativeGraphicsView>
    {
        const int MinHeight = 32;

        public GraphicsViewRenderer(Context context) : base(context)
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
                SetNativeControl(new NativeGraphicsView(Context));
                Control.Drawable = Element;
            }
        }

        protected override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();

            Element?.Load();
            Element?.AttachComponents();
        }

        protected override void OnDetachedFromWindow()
        {
            base.OnDetachedFromWindow();

            Element?.Unload();
            Element?.DetachComponents();
        }

        public override SizeRequest GetDesiredSize(int widthConstraint, int heightConstraint)
        {
            var width = widthConstraint;
            var height = heightConstraint;

            if (double.IsInfinity(height))
                height = MinHeight;

            Control.Layout(0, 0, width, height);
            Control.InvalidateDrawable(0, 0, width, height);

            return base.GetDesiredSize(widthConstraint, heightConstraint);
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            APointF point = new APointF(e.GetX(), e.GetY());

            switch (e.Action)
            {
                case MotionEventActions.Down:
                    Element?.OnTouchDown(new Point(point.X, point.Y));
                    break;
                case MotionEventActions.Move:
                    Element?.OnTouchMove(new Point(point.X, point.Y));
                    break;
                case MotionEventActions.Up:
                case MotionEventActions.Cancel:
                    Element?.OnTouchUp(new Point(point.X, point.Y));
                    break;
                default:
                    break;
            }

            return true;
        }

        void OnDrawInvalidated(object sender, EventArgs e)
        {
            Control?.InvalidateDrawable();
        }
    }
}