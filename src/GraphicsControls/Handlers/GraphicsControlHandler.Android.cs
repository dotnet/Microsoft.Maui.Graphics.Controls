using Microsoft.Maui.Graphics.Native;
using Microsoft.Maui.Handlers;

namespace Microsoft.Maui.Graphics.Controls
{
	public partial class GraphicsControlHandler<TViewDrawable, TVirtualView> : ViewHandler<TVirtualView, NativeGraphicsView>
	{
		protected override NativeGraphicsView CreateNativeView() =>
			new NativeGraphicsView((MauiContext as MauiContext)?.Context) { Drawable = this };

		public void Invalidate() =>
			NativeView?.Invalidate();
	}
}