using System;
using System.ComponentModel;
using System.Graphics;
using System.Graphics.Android;
using Android.Content;
using Android.Util;
using Android.Views;
using GraphicsControls.Android;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.Android;
using ACanvas = Android.Graphics.Canvas;
using APointF = Android.Graphics.PointF;
using AView = Android.Views.View;
using GColor = System.Graphics.Color;
using GraphicsView = GraphicsControls.GraphicsView;

[assembly: ExportRenderer(typeof(GraphicsView), typeof(GraphicsViewRenderer))]
namespace GraphicsControls.Android
{
    public class CustomNativeGraphicsView : AView
    {
        int _width, _height;
        readonly NativeCanvas _canvas;
        readonly ScalingCanvas _scalingCanvas;
        IDrawable _drawable;
        readonly float _scale = 1;
        GColor _backgroundColor;

        public CustomNativeGraphicsView(Context context, IAttributeSet attrs, IDrawable drawable = null) : base(context, attrs)
        {
            _scale = Resources.DisplayMetrics.Density;
            _canvas = new NativeCanvas(context);
            _scalingCanvas = new ScalingCanvas(_canvas);
            Drawable = drawable;
        }

        public CustomNativeGraphicsView(Context context, IDrawable drawable = null) : base(context)
        {
            _scale = Resources.DisplayMetrics.Density;
            _canvas = new NativeCanvas(context);
            _scalingCanvas = new ScalingCanvas(_canvas);
            Drawable = drawable;
        }

        public GColor BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                Invalidate();
            }
        }

        public IDrawable Drawable
        {
            get => _drawable;
            set
            {
                _drawable = value;
                Invalidate();
            }
        }

        public override void Draw(ACanvas androidCanvas)
        {
            if (_drawable == null) return;

            var dirtyRect = new RectangleF(0, 0, _width / _scale, _height / _scale);

            _canvas.Canvas = androidCanvas;
            if (_backgroundColor != null)
            {
                _canvas.FillColor = _backgroundColor;
                _canvas.FillRectangle(dirtyRect);
                _canvas.FillColor = Colors.White;
            }

            _scalingCanvas.ResetState();
            _scalingCanvas.Scale(_scale, _scale);
            _drawable.Draw(_scalingCanvas, dirtyRect);
            _canvas.Canvas = null;
        }

        protected override void OnSizeChanged(int width, int height, int oldWidth, int oldHeight)
        {
            base.OnSizeChanged(width, height, oldWidth, oldHeight);
            _width = width;
            _height = height;
        }
    }

    [Preserve(AllMembers = true)]
    public class GraphicsViewRenderer : ViewRenderer<GraphicsView, CustomNativeGraphicsView>
    {
        //const int MinHeight = 32;

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
                SetNativeControl(new CustomNativeGraphicsView(Context));
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

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            float density = Context.Resources.DisplayMetrics.Density;

            var width = (int)(w / density);
            var height = (int)(h / density);

            Control.Layout(0, 0, width, height);

            base.OnSizeChanged(width, height, oldw, oldh);
        }
           
        public override bool OnTouchEvent(MotionEvent e)
        {
            float density = Context.Resources.DisplayMetrics.Density;
            APointF point = new APointF(e.GetX() / density, e.GetY() / density);

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
            Control?.Invalidate();
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