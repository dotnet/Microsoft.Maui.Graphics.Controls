namespace Microsoft.Maui.Graphics.Controls
{
    public static class TimePickerExtensions
    {
        public static void UpdateTime(this GraphicsTimePicker platformView, ITimePicker timePicker)
        {
            platformView.Time = timePicker.Time;
        }
    }
}