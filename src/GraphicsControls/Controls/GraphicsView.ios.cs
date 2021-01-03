using System;
using System.Graphics.CoreGraphics;
using Foundation;
using UIKit;
using CoreGraphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using GraphicsControls.iOS;
using System.ComponentModel;
using GraphicsControls.Extensions;
using GraphicsView = GraphicsControls.GraphicsView;

[assembly: ExportRenderer(typeof(GraphicsView), typeof(GraphicsViewRenderer))]
namespace GraphicsControls.iOS
{
    [Preserve(AllMembers = true)]
    public class GraphicsViewRenderer : ViewRenderer<GraphicsView, NativeGraphicsView>
    {
        const double MinHeight = 32;

        const NSKeyValueObservingOptions observingOptions = NSKeyValueObservingOptions.Initial | NSKeyValueObservingOptions.OldNew | NSKeyValueObservingOptions.Prior;

        IDisposable _isLoadedObserverDisposable;

        public override bool CanBecomeFirstResponder => true;

        public override bool CanBecomeFocused => true;

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

                Control.UserInteractionEnabled = true;
                Control.BackgroundColor = UIColor.Clear;
                Control.Drawable = Element;

                UpdateContent();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == ContentView.ContentProperty.PropertyName)
                UpdateContent();

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

        public override UIView HitTest(CGPoint point, UIEvent uievent)
        {
            if (!UserInteractionEnabled || Hidden)
                return null;

            if (PointInside(point, uievent))
                BecomeFirstResponder();
            else
                ResignFirstResponder();

            UpdateIsFocused(IsFirstResponder);

            return base.HitTest(point, uievent);
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            if (Control != null && Control.UserInteractionEnabled && touches.AnyObject is UITouch anyObject)
            {
                CGPoint point = anyObject.LocationInView(this);
                Element?.OnTouchDown(new Point(point.X, point.Y));
            }
        }

        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            base.TouchesMoved(touches, evt);

            if (Control != null && Control.UserInteractionEnabled && touches.AnyObject is UITouch anyObject)
            {
                CGPoint point = anyObject.LocationInView(this);
                Element?.OnTouchMove(new Point(point.X, point.Y));
            }
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);

            if (Control != null && Control.UserInteractionEnabled && touches.AnyObject is UITouch anyObject)
            {
                CGPoint point = anyObject.LocationInView(this);
                Element?.OnTouchUp(new Point(point.X, point.Y));
            }
        }

        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);

            if (Control != null && Control.UserInteractionEnabled && touches.AnyObject is UITouch anyObject)
            {
                CGPoint point = anyObject.LocationInView(this);
                Element?.OnTouchUp(new Point(point.X, point.Y));
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

        void UpdateIsFocused(bool isFocused)
        {
            if (Element == null)
                return;

            var isFocusedPropertyKey = Element.GetInternalField<BindablePropertyKey>("IsFocusedPropertyKey");
            ((IElementController)Element).SetValueFromRenderer(isFocusedPropertyKey, isFocused);
        }

        void UpdateContent()
        {
            if (Subviews.Length <= 1)
                return;

            var content = Subviews[0];
            BringSubviewToFront(content);
        }
    }
}