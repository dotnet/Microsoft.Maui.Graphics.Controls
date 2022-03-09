namespace Microsoft.Maui.Graphics.Controls
{
    public partial class DatePickerHandler : MixedGraphicsControlHandler<IDatePickerDrawable, IDatePicker, GraphicsDatePicker>
    {
        protected override GraphicsDatePicker CreatePlatformView()
        {
            return new GraphicsDatePicker() { GraphicsControl = this };
        }

        protected override void ConnectHandler(GraphicsDatePicker nativeView)
        {
            base.ConnectHandler(nativeView);

            nativeView.DateSelected += OnDateSelected;
        }

        protected override void DisconnectHandler(GraphicsDatePicker nativeView)
        {
            base.DisconnectHandler(nativeView);

            nativeView.DateSelected -= OnDateSelected;
        }

        public static void MapMinimumDate(DatePickerHandler handler, IDatePicker datePicker)
        {
            handler.PlatformView?.UpdateMinimumDate(datePicker);
            (handler as IGraphicsHandler)?.Invalidate();
        }

        public static void MapMaximumDate(DatePickerHandler handler, IDatePicker datePicker)
        {
            handler.PlatformView?.UpdateMaximumDate(datePicker);
            (handler as IGraphicsHandler)?.Invalidate();
        }

        public static void MapDate(DatePickerHandler handler, IDatePicker datePicker)
        {
            handler.PlatformView?.UpdateDate(datePicker);
            (handler as IGraphicsHandler)?.Invalidate();
        }

        void OnDateSelected(object? sender, DateSelectedEventArgs e)
        {
            if (VirtualView != null)
                VirtualView.Date = e.SelectedDate;

            Invalidate();
        }
    }
}