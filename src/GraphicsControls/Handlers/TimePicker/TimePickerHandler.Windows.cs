namespace Microsoft.Maui.Graphics.Controls
{
    public partial class TimePickerHandler : MixedGraphicsControlHandler<ITimePickerDrawable, ITimePicker, GraphicsTimePicker>
    {
        protected override GraphicsTimePicker CreatePlatformView()
        {
            return new GraphicsTimePicker() { GraphicsControl = this };
        }

        protected override void ConnectHandler(GraphicsTimePicker platformView)
        {
            base.ConnectHandler(platformView);

            platformView.TimeSelected += OnTimeSelected;
        }

        protected override void DisconnectHandler(GraphicsTimePicker platformView)
        {
            base.DisconnectHandler(platformView);

            platformView.TimeSelected -= OnTimeSelected;
        }

        public static void MapTime(TimePickerHandler handler, ITimePicker timePicker)
        {
            handler.PlatformView?.UpdateTime(timePicker);
            (handler as IGraphicsHandler)?.Invalidate();
        }

        void OnTimeSelected(object? sender, TimeSelectedEventArgs e)
        {
            if (VirtualView != null)
                VirtualView.Time = e.SelectedTime;

            Invalidate();
        }
    }
}
