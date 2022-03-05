using Microsoft.Maui.Handlers;

namespace Microsoft.Maui.Graphics.Controls
{
    public partial class GraphicsControlHandler<TViewDrawable, TVirtualView> : ViewHandler<TVirtualView, PlatformGraphicsControlView>
	{
		protected override PlatformGraphicsControlView CreatePlatformView() =>
			new PlatformGraphicsControlView { GraphicsControl = this };

		public void Invalidate()
		{
			PlatformView?.Invalidate();
		}
	}
}