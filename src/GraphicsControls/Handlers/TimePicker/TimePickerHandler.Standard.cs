﻿using System;

namespace Microsoft.Maui.Graphics.Controls
{
	public partial class TimePickerHandler : MixedGraphicsControlHandler<ITimePickerDrawable, ITimePicker, object>
	{
		protected override object CreatePlatformView() => throw new NotImplementedException();

		public static void MapTime(TimePickerHandler handler, ITimePicker timePicker) { }
	}
}