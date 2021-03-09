using System;
using CoreGraphics;
using Foundation;
using GraphicsControls.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("GraphicsControls")]
[assembly: ExportEffect(typeof(TimePickerDialogPlatformEffect), nameof(TimePickerDialog))]
namespace GraphicsControls.Effects
{
    [Preserve(AllMembers = true)]
    internal class NoCaretField : UITextField
    {
        public NoCaretField() : base(new CGRect())
        {
        }

        public override CGRect GetCaretRectForPosition(UITextPosition position)
        {
            return new CGRect();
        }
    }

    [Preserve(AllMembers = true)]
    public class TimePickerDialogPlatformEffect : PlatformEffect
    {
        UIDatePicker _picker;
        NoCaretField _entry;
        UIView _view;
        NSDate _preSelectedDate;

        protected override void OnAttached()
        {
            _view = Control ?? Container;

            CreatePicker();
            UpdateTime();
        }

        protected override void OnDetached()
        {
            _entry.RemoveFromSuperview();
            _entry.Dispose();
            _picker.Dispose();
            _preSelectedDate.Dispose();
        }

        protected override void OnElementPropertyChanged(System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(e);

            if (e.PropertyName == TimePickerDialog.TimeProperty.PropertyName)
            {
                UpdateTime();
            }
        }

        void CreatePicker()
        {
            _entry = new NoCaretField
            {
                BorderStyle = UITextBorderStyle.None,
                BackgroundColor = UIColor.Clear
            };

            _view.AddSubview(_entry);

            _entry.TranslatesAutoresizingMaskIntoConstraints = false;

            _entry.TopAnchor.ConstraintEqualTo(_view.TopAnchor).Active = true;
            _entry.LeftAnchor.ConstraintEqualTo(_view.LeftAnchor).Active = true;
            _entry.BottomAnchor.ConstraintEqualTo(_view.BottomAnchor).Active = true;
            _entry.RightAnchor.ConstraintEqualTo(_view.RightAnchor).Active = true;
            _entry.WidthAnchor.ConstraintEqualTo(_view.WidthAnchor).Active = true;
            _entry.HeightAnchor.ConstraintEqualTo(_view.HeightAnchor).Active = true;

            _view.UserInteractionEnabled = true;
            _view.SendSubviewToBack(_entry);

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

        void Done()
        {
            var time = _picker.Date.ToDateTime() - new DateTime(1, 1, 1);
            TimePickerDialog.SetTime(Element, time);
            _preSelectedDate = _picker.Date;
        }

        void UpdateTime()
        {
            var time = TimePickerDialog.GetTime(Element);
            _picker.Date = new DateTime(1, 1, 1).Add(time).ToNSDate();
            _preSelectedDate = _picker.Date;
        }
    }
}