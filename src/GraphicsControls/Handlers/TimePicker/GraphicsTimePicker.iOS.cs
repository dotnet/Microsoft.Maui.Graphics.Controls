using System;
using System.Diagnostics;
using CoreGraphics;
using Foundation;
using Microsoft.Maui.Graphics.Native;
using UIKit;

namespace Microsoft.Maui.Graphics.Controls
{
    public class GraphicsTimePicker : UIView, IMixedNativeView
    {
        TimeSpan _time;

        IMixedGraphicsHandler? _graphicsControl;
        readonly NativeCanvas _canvas;

        UIDatePicker? _picker;
        NoCaretField? _entry;
        NSDate? _preSelectedDate;

        CGColorSpace? _colorSpace;
        IDrawable? _drawable;
        CGRect _lastBounds;

        public GraphicsTimePicker()
        {
            _canvas = new NativeCanvas(() => CGColorSpace.CreateDeviceRGB());

            BackgroundColor = UIColor.Clear;

            CreatePicker();
        }

        public TimeSpan Time
        {
            get { return _time; }
            set
            {
                _time = value;
                UpdateTime(_time);
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

        public event EventHandler<TimeSelectedEventArgs>? TimeSelected;

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

            _picker = new UIDatePicker { Mode = UIDatePickerMode.Time, TimeZone = new NSTimeZone("UTC") };

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

        void UpdateTime(TimeSpan time)
        {
            if (_picker == null)
                return;

            _picker.Date = new DateTime(1, 1, 1).Add(time).ToNSDate();
            _preSelectedDate = _picker.Date;

            TimeSelected?.Invoke(this, new TimeSelectedEventArgs(time));
        }

        void Done()
        {
            var time = _picker?.Date.ToDateTime() - new DateTime(1, 1, 1);

            if (time != null)
                UpdateTime(time.Value);

            _preSelectedDate = _picker?.Date;
        }
    }
}