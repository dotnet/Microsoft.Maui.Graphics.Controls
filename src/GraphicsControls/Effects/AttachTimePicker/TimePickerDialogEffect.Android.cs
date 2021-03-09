using System;
using Android.Text.Format;
using Android.Views;
using GraphicsControls.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ATimePickerDialog = Android.App.TimePickerDialog;
using AView = Android.Views.View;

[assembly: ResolutionGroupName("GraphicsControls")]
[assembly: ExportEffect(typeof(TimePickerDialogPlatformEffect), nameof(GraphicsControls.Effects.TimePickerDialog))]
namespace GraphicsControls.Effects
{
    public class TimePickerDialogPlatformEffect : PlatformEffect
    {
        AView _view;
        ATimePickerDialog _dialog;

        protected override void OnAttached()
        {
            _view = Control ?? Container;

            _view.Touch += OnTouch;
        }

        protected override void OnDetached()
        {
            if (_dialog != null)
            {
                _dialog.Dispose();
                _dialog = null;
            }

            _view.Touch -= OnTouch;
            _view = null;
        }

        void OnTouch(object sender, AView.TouchEventArgs e)
        {
            if (e.Event.Action == MotionEventActions.Up)
            {
                CreateDialog();
            }
        }

        void CreateDialog()
        {
            var time = TimePickerDialog.GetTime(Element);

            if (_dialog == null)
            {
                bool is24HourFormat = DateFormat.Is24HourFormat(_view.Context);
                _dialog = new ATimePickerDialog(_view.Context, TimeSelected, time.Hours, time.Minutes, is24HourFormat);

                _dialog.SetCanceledOnTouchOutside(true);

                _dialog.DismissEvent += (ss, ee) =>
                {
                    _dialog.Dispose();
                    _dialog = null;
                };

                _dialog.Show();
            }
        }

        void TimeSelected(object sender, ATimePickerDialog.TimeSetEventArgs e)
        {
            var time = new TimeSpan(e.HourOfDay, e.Minute, 0);
            TimePickerDialog.SetTime(Element, time);
        }
    }
}