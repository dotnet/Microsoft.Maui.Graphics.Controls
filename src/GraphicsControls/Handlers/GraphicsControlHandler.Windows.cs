using Microsoft.Maui.Handlers;

namespace Microsoft.Maui.Graphics.Controls
{
    public partial class GraphicsControlHandler<TViewDrawable, TVirtualView> : ViewHandler<TVirtualView, NativeGraphicsControlView>
	{
		protected override NativeGraphicsControlView CreatePlatformView() =>
			new NativeGraphicsControlView { GraphicsControl = this };

		public void Invalidate()
		{
			PlatformView?.Invalidate();
		}
	}
}