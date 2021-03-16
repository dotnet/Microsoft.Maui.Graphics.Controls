using System;
using System.Graphics.Skia;
using GraphicsControls;
using GraphicsControls.GTK;
using System.ComponentModel;
using System.Graphics;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.GTK;
using SkiaSharp.Views.Gtk;
using SkiaSharp.Views.Desktop;
using Gdk;
using Gtk;
using Xamarin.Forms.Platform.GTK.Extensions;
using GPoint = System.Graphics.Point;
using XColor = Xamarin.Forms.Color;

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
            _drawable.Draw(_scalingCanvas, new RectangleF(0, 0, e.Info.Width, e.Info.Height));
        }
    }

    [Preserve(AllMembers = true)]
    public class GraphicsViewRenderer : ViewRenderer<GraphicsView, NativeGraphicsView>
    {
        string _defaultAccessibilityLabel;
        string _defaultAccessibilityHint;

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

                SetNativeControl(new NativeGraphicsView());

                Control.Drawable = Element;

                Control.AddEvents((int)EventMask.AllEventsMask);

                UpdateBackground();
                UpdateAutomationHelpText();
                UpdateAutomationName();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == VisualElement.BackgroundColorProperty.PropertyName)
                UpdateBackground();
            else if (e.PropertyName == AutomationProperties.HelpTextProperty.PropertyName)
                UpdateAutomationHelpText();
            else if (e.PropertyName == AutomationProperties.NameProperty.PropertyName)
                UpdateAutomationName();

            Control?.QueueDraw();
        }

        protected override void OnParentSet(Widget previous_parent)
        {
            base.OnParentSet(previous_parent);

            Element?.Load();
            Element?.AttachComponents();
        }

        public override void Destroy()
        {
            base.Destroy();

            Element?.Unload();
            Element?.DetachComponents();
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

        void UpdateBackground()
        {
            if (Element.BackgroundColor == XColor.Default)
            {
                Control.ModifyBg(StateType.Normal, XColor.Transparent.ToGtkColor());
                return;
            }

            Control.ModifyBg(StateType.Normal, Element.BackgroundColor.ToGtkColor());
        }

        void UpdateAutomationHelpText()
        {
            if (Element == null)
                return;

            if (_defaultAccessibilityHint == null)
                _defaultAccessibilityHint = Control.Accessible.Name;

            var helpText = (string)Element.GetValue(AutomationProperties.HelpTextProperty) ?? _defaultAccessibilityHint;

            if (!string.IsNullOrEmpty(helpText))
            {
                Control.Accessible.Name = helpText;
            }
        }

        void UpdateAutomationName()
        {
            if (Element == null)
                return;

            if (_defaultAccessibilityLabel == null)
                _defaultAccessibilityLabel = Control.Accessible.Description;

            var name = (string)Element.GetValue(AutomationProperties.NameProperty) ?? _defaultAccessibilityLabel;

            if (!string.IsNullOrEmpty(name))
            {
                Control.Accessible.Description = name;
            }
        }

        void OnDrawInvalidated(object sender, EventArgs e)
        {
            Control?.QueueDraw();
        }
    }
}