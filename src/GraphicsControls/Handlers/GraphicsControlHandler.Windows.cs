using Microsoft.Maui.Handlers;

namespace Microsoft.Maui.Graphics.Controls
{
    public partial class GraphicsControlHandler<TViewDrawable, TVirtualView> : ViewHandler<TVirtualView, NativeGraphicsControlView>
	{
		protected override NativeGraphicsControlView CreateNativeView() =>
			new NativeGraphicsControlView { GraphicsControl = this };

		public void Invalidate()
		{
			NativeView?.Invalidate();
		}
	}
}