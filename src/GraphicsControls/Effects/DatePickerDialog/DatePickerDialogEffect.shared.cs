using System;
using Xamarin.Forms;

namespace GraphicsControls.Effects
{
    public class DatePickerDialog
    {
        public static readonly BindableProperty DateProperty =
            BindableProperty.CreateAttached("Date", typeof(DateTime), typeof(DatePickerDialog), default(DateTime), defaultBindingMode: BindingMode.TwoWay);

        public static void SetDate(BindableObject view, DateTime value)
        {
            view.SetValue(DateProperty, value);
        }

        public static DateTime GetDate(BindableObject view)
        {
            return (DateTime)view.GetValue(DateProperty);
        }

        public static readonly BindableProperty MaximumDateProperty =
            BindableProperty.CreateAttached("MaximumDate", typeof(DateTime), typeof(DatePickerDialog), new DateTime(2100, 12, 31));


        public static void SetMaximumDate(BindableObject view, DateTime value)
        {
            view.SetValue(MaximumDateProperty, value);
        }

        public static DateTime GetMaximumDate(BindableObject view)
        {
            return (DateTime)view.GetValue(MaximumDateProperty);
        }

        public static readonly BindableProperty MinimumDateProperty =
            BindableProperty.CreateAttached("MinimumDate", typeof(DateTime), typeof(DatePickerDialog), new DateTime(1900, 1, 1));

        public static void SetMinimumDate(BindableObject view, DateTime value)
        {
            view.SetValue(MinimumDateProperty, value);
        }

        public static DateTime GetMinimumDate(BindableObject view)
        {
            return (DateTime)view.GetValue(MinimumDateProperty);
        }
    }

    internal class DatePickerDialogRoutingEffect : RoutingEffect
    {
        public DatePickerDialogRoutingEffect() : base("GraphicsControls." + nameof(DatePickerDialog))
        {

        }
    }
}