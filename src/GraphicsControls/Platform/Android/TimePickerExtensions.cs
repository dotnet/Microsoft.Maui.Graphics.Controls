namespace Microsoft.Maui.Graphics.Controls
{
    public static class TimePickerExtensions
    {
        public static void UpdateTime(this GraphicsTimePicker nativeView, ITimePicker timePicker)
        {
            nativeView.Time = timePicker.Time;
        }
    }
}