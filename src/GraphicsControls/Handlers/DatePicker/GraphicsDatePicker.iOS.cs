using System;
using System.Diagnostics;
using CoreGraphics;
using Foundation;
using Microsoft.Maui.Graphics.Native;
using UIKit;

namespace Microsoft.Maui.Graphics.Controls
{
    public class GraphicsDatePicker : UIView, IMixedNativeView
    {
        DateTime _date;
        DateTime _minimumDate;
        DateTime _maximumDate;

        IMixedGraphicsHandler? _graphicsControl;
        readonly NativeCanvas _canvas;

        UIDatePicker? _picker;
        NoCaretField? _entry;
        NSDate? _preSelectedDate;

        CGColorSpace? _colorSpace;
        IDrawable? _drawable;
        CGRect _lastBounds;

        public GraphicsDatePicker()
        {
            _canvas = new NativeCanvas(() => CGColorSpace.CreateDeviceRGB());

            BackgroundColor = UIColor.Clear;

            CreatePicker();
        }

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                UpdateDate(_date);
            }
        }

        public DateTime MinimumDate
        {
            get { return _minimumDate; }
            set
            {
                _minimumDate = value;
                UpdateMinimumDate(_minimumDate);
            }
        }

        public DateTime MaximumDate
        {
            get { return _maximumDate; }
            set
            {
                _maximumDate = value;
                UpdateMaximumDate(_maximumDate);
            }
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

        public event EventHandler<DateSelectedEventArgs>? DateSelected;

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

        static readonly string[] DefaultNativeLayers = new string[] { };

        public string[] NativeLayers => DefaultNativeLayers;

        public void Invalidate()
        {
            SetNeedsDisplay();
        }

        public void DrawBaseLayer(RectangleF dirtyRect)
        {
            base.Draw(dirtyRect);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                _entry?.RemoveFromSuperview();
                _entry?.Dispose();
                _picker?.Dispose();
                _preSelectedDate?.Dispose();
            }
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

        public override void TouchesBegan(NSSet touches, UIEvent? evt)
        {
            try
            {
                if (!IsFirstResponder)
                    BecomeFirstResponder();

                var viewPoints = this.GetPointsInView(evt);
                GraphicsControl?.StartInteraction(viewPoints);
            }
            catch (Exception exc)
            {
                Debug.WriteLine("An unexpected error occured handling a touch event within the control.", exc);
            }
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

        void CreatePicker()
        {
            _entry = new NoCaretField
            {
                BorderStyle = UITextBorderStyle.None,
                BackgroundColor = UIColor.Clear
            };

            AddSubview(_entry);

            _entry.TranslatesAutoresizingMaskIntoConstraints = false;

            _entry.TopAnchor.ConstraintEqualTo(TopAnchor).Active = true;
            _entry.LeftAnchor.ConstraintEqualTo(LeftAnchor).Active = true;
            _entry.BottomAnchor.ConstraintEqualTo(BottomAnchor).Active = true;
            _entry.RightAnchor.ConstraintEqualTo(RightAnchor).Active = true;
            _entry.WidthAnchor.ConstraintEqualTo(WidthAnchor).Active = true;
            _entry.HeightAnchor.ConstraintEqualTo(HeightAnchor).Active = true;

            UserInteractionEnabled = true;
            SendSubviewToBack(_entry);

            _picker = new UIDatePicker { Mode = UIDatePickerMode.Date, TimeZone = new Foundation.NSTimeZone("UTC") };

            if (UIDevice.CurrentDevice.CheckSystemVersion(14, 0))
            {
                _picker.PreferredDatePickerStyle = UIDatePickerStyle.Wheels;
            }

            var width = UIScreen.MainScreen.Bounds.Width;
            var toolbar = new UIToolbar(new CGRect(0, 0, (float)width, 44)) { BarStyle = UIBarStyle.Default, Translucent = true };

            var cancelButton = new UIBarButtonItem(UIBarButtonSystemItem.Cancel, (o, e) =>
            {
                _entry.ResignFirstResponder();

                if (_preSelectedDate != null)
                    _picker.Date = _preSelectedDate;
            });

            var spacer = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace);

            var doneButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, (o, a) =>
            {
                _entry.ResignFirstResponder();
                Done();
            });

            toolbar.SetItems(new[] { cancelButton, spacer, doneButton }, false);

            _entry.InputView = _picker;
            _entry.InputAccessoryView = toolbar;
        }

        void UpdateDate(DateTime date)
        {
            var nsDate = date.ToNSDate();
            _picker?.SetDate(nsDate, false);
            _preSelectedDate = nsDate;

            DateSelected?.Invoke(this, new DateSelectedEventArgs(date));
        }

        void UpdateMaximumDate(DateTime maximumDate)
        {
            if (_picker != null)
                _picker.MaximumDate = maximumDate.ToNSDate();
        }

        void UpdateMinimumDate(DateTime minimumDate)
        {
            if (_picker != null)
                _picker.MinimumDate = minimumDate.ToNSDate();
        }

        void Done()
        {
            var date = _picker?.Date.ToDateTime().Date;

            if (date != null)
                UpdateDate(date.Value);

            _preSelectedDate = _picker?.Date;
        }
    }
}