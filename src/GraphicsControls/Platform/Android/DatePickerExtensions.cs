namespace Microsoft.Maui.Graphics.Controls
{
    public static class DatePickerExtensions
    {
        public static void UpdateMinimumDate(this GraphicsDatePicker platformView, IDatePicker datePicker)
        {
            platformView.MinimumDate = datePicker.MinimumDate;
        }

        public static void UpdateMaximumDate(this GraphicsDatePicker platformView, IDatePicker datePicker)
        {
            platformView.MaximumDate = datePicker.MaximumDate;
        }

        public static void UpdateDate(this GraphicsDatePicker platformView, IDatePicker datePicker)
        {
            platformView.Date = datePicker.Date;
        }
    }
}