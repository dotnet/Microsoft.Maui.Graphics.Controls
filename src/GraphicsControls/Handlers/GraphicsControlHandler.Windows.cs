using Microsoft.Maui.Graphics.Win2D;
using Microsoft.Maui.Handlers;

namespace Microsoft.Maui.Graphics.Controls
{
    public partial class GraphicsControlHandler<TViewDrawable, TVirtualView> : ViewHandler<TVirtualView, W2DGraphicsView>
	{
		protected override W2DGraphicsView CreateNativeView() =>
			new W2DGraphicsView() { Drawable = this };

		public void Invalidate() =>
			NativeView?.Invalidate();
	}
}