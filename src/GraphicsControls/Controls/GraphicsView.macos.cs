using System;
using System.Graphics.CoreGraphics;
using Foundation;
using AppKit;
using CoreGraphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.MacOS;
using GraphicsControls.Mac;
using System.ComponentModel;
using GraphicsView = GraphicsControls.GraphicsView;
using Point = System.Graphics.Point;

[assembly: ExportRenderer(typeof(GraphicsView), typeof(GraphicsViewRenderer))]
namespace GraphicsControls.Mac
{
    [Preserve(AllMembers = true)]
    public class GraphicsViewRenderer : ViewRenderer<GraphicsView, NativeGraphicsView>
    {
        const double TouchOffsetX = 20;
        const double MinHeight = 32;

        const NSKeyValueObservingOptions observingOptions = NSKeyValueObservingOptions.Initial | NSKeyValueObservingOptions.OldNew | NSKeyValueObservingOptions.Prior;

        IDisposable _isLoadedObserverDisposable;

        public override bool CanBecomeKeyView => true;

        public override bool IsFlipped => true;

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

                var key = Superview == null ? "subviews" : "superview";
                _isLoadedObserverDisposable = AddObserver(key, observingOptions, OnViewLoadedObserver);

                SetNativeControl(new NativeGraphicsView());

                Control.Drawable = Element;

                UpdateContent();
                UpdateAutomationHelpText();
                UpdateAutomationName();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == ContentView.ContentProperty.PropertyName)
                UpdateContent();
            else if (e.PropertyName == AutomationProperties.HelpTextProperty.PropertyName)
                UpdateAutomationHelpText();
            else if (e.PropertyName == AutomationProperties.NameProperty.PropertyName)
                UpdateAutomationName();

            Control?.InvalidateDrawable();
        }

        public override SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint)
        {
            var width = widthConstraint;
            var height = heightConstraint;

            if (double.IsInfinity(height))
                height = MinHeight;

            Control.Frame = new CGRect(0, 0, width, height);
            Control.InvalidateDrawable(0, 0, (float)width, (float)height);

            return base.GetDesiredSize(width, height);
        }

        public override void MouseDown(NSEvent theEvent)
        {
            base.MouseDown(theEvent);

            if (Control != null)
            {
                CGPoint locationInWindow = theEvent.LocationInWindow;
                CGPoint locationInView = ConvertPointToView(locationInWindow, null);

                Element?.OnTouchDown(new Point(locationInView.X - TouchOffsetX, locationInView.Y));
            }
        }

        public override void MouseDragged(NSEvent theEvent)
        {
            base.MouseDragged(theEvent);

            if (Control != null)
            {
                CGPoint locationInWindow = theEvent.LocationInWindow;
                CGPoint locationInView = ConvertPointToView(locationInWindow, null);

                Element?.OnTouchMove(new Point(locationInView.X - TouchOffsetX, locationInView.Y));
            }
        }

        public override void MouseUp(NSEvent theEvent)
        {
            base.MouseUp(theEvent);

            if (Control != null)
            {
                CGPoint locationInWindow = theEvent.LocationInWindow;
                CGPoint locationInView = ConvertPointToView(locationInWindow, null);

                Element?.OnTouchUp(new Point(locationInView.X - TouchOffsetX, locationInView.Y));
            }
        }

        void OnViewLoadedObserver(NSObservedChange nSObservedChange)
        {
            if (!nSObservedChange?.NewValue?.Equals(NSNull.Null) ?? false)
            {
                Element?.Load();
                Element?.AttachComponents();
            }
            else if (!nSObservedChange?.OldValue?.Equals(NSNull.Null) ?? false)
            {
                Element?.Unload();
                Element?.DetachComponents();
                _isLoadedObserverDisposable.Dispose();
                _isLoadedObserverDisposable = null;
            }
        }

        void OnDrawInvalidated(object sender, EventArgs e)
        {
            Control?.InvalidateDrawable();
        }

        void UpdateContent()
        {
            if (Subviews.Length <= 1)
                return;

            var content = Subviews[0];
            content.RemoveFromSuperview();
            AddSubview(content);
        }

        void UpdateAutomationHelpText()
        {
            Control?.SetAccessibilityHint(Element);
        }

        void UpdateAutomationName()
        {
            Control?.SetAccessibilityLabel(Element);
        }
    }
}