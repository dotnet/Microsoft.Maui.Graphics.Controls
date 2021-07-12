namespace Microsoft.Maui.Graphics.Controls
{
    public partial class TimePickerHandler : MixedGraphicsControlHandler<ITimePickerDrawable, ITimePicker, GraphicsTimePicker>
    {
        protected override GraphicsTimePicker CreateNativeView()
        {
            return new GraphicsTimePicker() { GraphicsControl = this };
        }

        protected override void ConnectHandler(GraphicsTimePicker nativeView)
        {
            base.ConnectHandler(nativeView);

            nativeView.TimeSelected += OnTimeSelected;
        }

        protected override void DisconnectHandler(GraphicsTimePicker nativeView)
        {
            base.DisconnectHandler(nativeView);

            nativeView.TimeSelected -= OnTimeSelected;
        }

        public static void MapTime(TimePickerHandler handler, ITimePicker timePicker)
        {
            handler.NativeView?.UpdateTime(timePicker);
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
