using System;

namespace Microsoft.Maui.Graphics.Controls
{
    public partial  class TimePickerHandler : MixedGraphicsControlHandler<ITimePickerDrawable, ITimePicker, GraphicsTimePicker>
	{
		protected override GraphicsTimePicker CreateNativeView() => throw new NotImplementedException();
	}
}