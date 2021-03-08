using System.ComponentModel;
using CoreGraphics;
using Foundation;
using GraphicsControls.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(DatePickerDialogPlatformEffect), nameof(DatePickerDialog))]
namespace GraphicsControls.Effects
{
    public class DatePickerDialogPlatformEffect : PlatformEffect
    {
        UIView _view;
        UIDatePicker _picker;
        NoCaretField _entry;
        NSDate _preSelectedDate;

        protected override void OnAttached()
        {
            _view = Control ?? Container;

            CreatePicker();
            UpdateDate();
            UpdateMaximumDate();
            UpdateMinimumDate();
        }

        protected override void OnDetached()
        {
            _entry.RemoveFromSuperview();
            _entry.Dispose();
            _picker.Dispose();
            _preSelectedDate.Dispose();
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(e);

            if (e.PropertyName == DatePickerDialog.DateProperty.PropertyName)
                UpdateDate();
            else if (e.PropertyName == DatePickerDialog.MaximumDateProperty.PropertyName)
                UpdateMaximumDate();
            else if (e.PropertyName == DatePickerDialog.MinimumDateProperty.PropertyName)
                UpdateMinimumDate();
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

        void UpdateDate()
        {
            var date = DatePickerDialog.GetDate(Element).ToNSDate();
            _picker.SetDate(date, false);
            _preSelectedDate = date;
        }

        void UpdateMaximumDate()
        {
            _picker.MaximumDate = DatePickerDialog.GetMaximumDate(Element).ToNSDate();
        }

        void UpdateMinimumDate()
        {
            _picker.MinimumDate = DatePickerDialog.GetMinimumDate(Element).ToNSDate();
        }

        void Done()
        {
            var date = _picker.Date.ToDateTime().Date;
            DatePickerDialog.SetDate(Element, date);
            _preSelectedDate = _picker.Date;
        }
    }
}