namespace Microsoft.Maui.Graphics.Controls
{
    public partial class TimePickerHandler : MixedGraphicsControlHandler<ITimePickerDrawable, ITimePicker, GraphicsTimePicker>
    {
        protected override GraphicsTimePicker CreateNativeView()
        {
            return new GraphicsTimePicker();
        }

        public static void MapTime(TimePickerHandler handler, ITimePicker timePicker) { }
    }
}
