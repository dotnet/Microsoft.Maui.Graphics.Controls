namespace Microsoft.Maui.Graphics.Controls
{
    public static class DatePickerExtensions
    {
        public static void UpdateMinimumDate(this GraphicsDatePicker nativeView, IDatePicker datePicker)
        {
            nativeView.MinimumDate = datePicker.MinimumDate;
        }

        public static void UpdateMaximumDate(this GraphicsDatePicker nativeView, IDatePicker datePicker)
        {
            nativeView.MaximumDate = datePicker.MaximumDate;
        }

        public static void UpdateDate(this GraphicsDatePicker nativeView, IDatePicker datePicker)
        {
            nativeView.Date = datePicker.Date;
        }
    }
}