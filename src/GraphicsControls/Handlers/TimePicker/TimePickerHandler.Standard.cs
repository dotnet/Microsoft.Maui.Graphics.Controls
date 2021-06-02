using System;

namespace Microsoft.Maui.Graphics.Controls
{
	public partial class TimePickerHandler : MixedGraphicsControlHandler<ITimePicker, ITimePickerDrawable, object>
	{
		protected override object CreateNativeView() => throw new NotImplementedException();
	}
}