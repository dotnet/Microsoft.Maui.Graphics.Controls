﻿using Microsoft.Maui.Graphics.Platform;
using Microsoft.Maui.Handlers;

namespace Microsoft.Maui.Graphics.Controls
{
	public partial class GraphicsControlHandler<TViewDrawable, TVirtualView> : ViewHandler<TVirtualView, PlatformGraphicsView>
	{
		protected override PlatformGraphicsView CreatePlatformView() =>
			new PlatformGraphicsControlView { GraphicsControl = this };

		public void Invalidate() =>
			PlatformView?.InvalidateDrawable();
	}
}