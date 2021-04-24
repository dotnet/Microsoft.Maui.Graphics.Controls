using System;

namespace Microsoft.Maui.Graphics.Controls
{
	public interface IControlState
	{
		ControlState CurrentState { get; set; }
		Action<ControlState> StateChanged { get; }
	}
}