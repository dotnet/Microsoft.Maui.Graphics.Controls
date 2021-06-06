using System;

namespace Microsoft.Maui.Graphics.Controls
{
	public partial class DatePickerHandler : MixedGraphicsControlHandler<IDatePickerDrawable, IDatePicker, object>
	{
		protected override object CreateNativeView() => throw new NotImplementedException();

		public static void MapMinimumDate(DatePickerHandler handler, IDatePicker datePicker) { }
		public static void MapMaximumDate(DatePickerHandler handler, IDatePicker datePicker) { }
		public static void MapDate(DatePickerHandler handler, IDatePicker datePicker) { }
	}
}