using Microsoft.Maui.Graphics.Platform;
using Microsoft.Maui.Handlers;

namespace Microsoft.Maui.Graphics.Controls
{
	public partial class GraphicsControlHandler<TViewDrawable, TVirtualView> : ViewHandler<TVirtualView, PlatformGraphicsView>
	{
		protected override PlatformGraphicsView CreateNativeView() =>
			new NativeGraphicsControlView { GraphicsControl = this };

		public void Invalidate() =>
			NativeView?.InvalidateDrawable();
	}
}