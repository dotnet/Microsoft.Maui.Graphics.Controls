using System;

namespace Microsoft.Maui.Graphics.Controls
{
	public partial class DatePickerHandler : MixedGraphicsControlHandler<IDatePicker, IDatePickerDrawable, object>
	{
		protected override object CreateNativeView() => throw new NotImplementedException();
	}
}