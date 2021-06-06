namespace Microsoft.Maui.Graphics.Controls
{
    public partial class DatePickerHandler : MixedGraphicsControlHandler<IDatePickerDrawable, IDatePicker, GraphicsDatePicker>
	{
		protected override GraphicsDatePicker CreateNativeView()
		{
			return new GraphicsDatePicker(Context!);
		}

		public static void MapMinimumDate(DatePickerHandler handler, IDatePicker datePicker)
		{
			handler.NativeView?.UpdateMinimumDate(datePicker);
			(handler as IGraphicsHandler)?.Invalidate();
		}

		public static void MapMaximumDate(DatePickerHandler handler, IDatePicker datePicker)
		{
			handler.NativeView?.UpdateMaximumDate(datePicker);
			(handler as IGraphicsHandler)?.Invalidate();
		}

		public static void MapDate(DatePickerHandler handler, IDatePicker datePicker)
		{
			handler.NativeView?.UpdateDate(datePicker);
			(handler as IGraphicsHandler)?.Invalidate();
		}
	}
}