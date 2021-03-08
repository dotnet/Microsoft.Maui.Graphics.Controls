using System;
using System.ComponentModel;
using Android.Views;
using GraphicsControls.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static Android.Views.View;
using ADatePickerDialog = Android.App.DatePickerDialog;
using AView = Android.Views.View;

[assembly: ExportEffect(typeof(DatePickerDialogPlatformEffect), nameof(DatePickerDialog))]
namespace GraphicsControls.Effects
{
    public class DatePickerDialogPlatformEffect : PlatformEffect
    {
        AView _view;
        ADatePickerDialog _dialog;

        protected override void OnAttached()
        {
            _view = Control ?? Container;

            _view.Touch += OnTouch;
        }

        protected override void OnDetached()
        {
            var renderer = Container as IVisualElementRenderer;

            if (_view != null)
                _view.Touch -= OnTouch;

            if (_dialog != null)
            {
                _dialog.Dispose();
                _dialog = null;
            }
            _view = null;
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(e);

            if (e.PropertyName == DatePickerDialog.MaximumDateProperty.PropertyName)
                UpdateMaximumDate();
            else if (e.PropertyName == DatePickerDialog.MinimumDateProperty.PropertyName)
                UpdateMinimumDate();
        }

        void OnTouch(object sender, TouchEventArgs e)
        {
            if (e.Event.Action != MotionEventActions.Up)
                return;

            if (_dialog != null)
                _dialog.Dispose();

            CreateDialog();
            UpdateMinimumDate();
            UpdateMaximumDate();

            _dialog.CancelEvent += OnCancelButtonClicked;

            _dialog.Show();
        }

        void CreateDialog()
        {
            var date = DatePickerDialog.GetDate(Element);

            _dialog = new ADatePickerDialog(_view.Context, (o, e) =>
            {
                DatePickerDialog.SetDate(Element, e.Date);
                _view.ClearFocus();
                _dialog.CancelEvent -= OnCancelButtonClicked;

                _dialog = null;
            }, date.Year, date.Month - 1, date.Day);

            _dialog.SetCanceledOnTouchOutside(true);
        }

        void OnCancelButtonClicked(object sender, EventArgs e)
        {
            _view.ClearFocus();
        }

        void UpdateMaximumDate()
        {
            if (_dialog != null)
                _dialog.DatePicker.MaxDate = (long)DatePickerDialog.GetMaximumDate(Element).ToUniversalTime().Subtract(DateTime.MinValue.AddYears(1969)).TotalMilliseconds;
        }

        void UpdateMinimumDate()
        {
            if (_dialog != null)
                _dialog.DatePicker.MinDate = (long)DatePickerDialog.GetMinimumDate(Element).ToUniversalTime().Subtract(DateTime.MinValue.AddYears(1969)).TotalMilliseconds;
        }
    }
}