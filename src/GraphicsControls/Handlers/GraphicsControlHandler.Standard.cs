using System;
using Microsoft.Maui.Handlers;

namespace Microsoft.Maui.Graphics.Controls
{
	public partial class GraphicsControlHandler<TViewDrawable, TVirtualView> : ViewHandler<TVirtualView, object>
	{
		protected override object CreatePlatformView() => throw new NotImplementedException();
		public void Invalidate() => throw new NotImplementedException();
	}
}