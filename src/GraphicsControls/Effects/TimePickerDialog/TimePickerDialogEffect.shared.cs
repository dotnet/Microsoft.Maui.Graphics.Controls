using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace GraphicsControls.Effects
{
    public static class TimePickerDialog
    {
        public static readonly BindableProperty TimeProperty =
            BindableProperty.CreateAttached("Time", typeof(TimeSpan), typeof(TimePickerDialog), default(TimeSpan), defaultBindingMode: BindingMode.TwoWay);

        public static void SetTime(BindableObject view, TimeSpan value)
        {
            view.SetValue(TimeProperty, value);
        }

        public static TimeSpan GetTime(BindableObject view)
        {
            return (TimeSpan)view.GetValue(TimeProperty);
        }
    }

    public class TimePickerDialogRoutingEffect : RoutingEffect
    {
        public TimePickerDialogRoutingEffect() : base("GraphicsControls." + nameof(TimePickerDialog))
        {

        }
    }
}