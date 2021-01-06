using System;
using System.ComponentModel;
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

                UpdateAutomationProperties();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == AutomationProperties.HelpTextProperty.PropertyName ||
                e.PropertyName == AutomationProperties.NameProperty.PropertyName)
                UpdateAutomationProperties();
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

        void UpdateAutomationProperties()
        {
            if (Control == null)
                return;

            var defaultContentDescription = Control.ContentDescription;

            string value = ConcatenateNameAndHelpText(Element);

            var contentDescription = !string.IsNullOrWhiteSpace(value) ? value : defaultContentDescription;

            if (string.IsNullOrWhiteSpace(contentDescription) && Element is Element element)
                contentDescription = element.AutomationId;

            Control.ContentDescription = contentDescription;
        }

        internal static string ConcatenateNameAndHelpText(BindableObject Element)
        {
            var name = (string)Element.GetValue(AutomationProperties.NameProperty);
            var helpText = (string)Element.GetValue(AutomationProperties.HelpTextProperty);

            if (string.IsNullOrWhiteSpace(name))
                return helpText;
            if (string.IsNullOrWhiteSpace(helpText))
                return name;

            return $"{name}. {helpText}";
        }
    }
}