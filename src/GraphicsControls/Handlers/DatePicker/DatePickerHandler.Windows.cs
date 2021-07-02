namespace Microsoft.Maui.Graphics.Controls
{
    public partial class DatePickerHandler : MixedGraphicsControlHandler<IDatePickerDrawable, IDatePicker, GraphicsDatePicker>
    {
        protected override GraphicsDatePicker CreateNativeView()
        {
            return new GraphicsDatePicker();
        }

        public static void MapMinimumDate(DatePickerHandler handler, IDatePicker datePicker) { }
        public static void MapMaximumDate(DatePickerHandler handler, IDatePicker datePicker) { }
        public static void MapDate(DatePickerHandler handler, IDatePicker datePicker) { }
    }
}
