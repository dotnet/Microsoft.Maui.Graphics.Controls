﻿using System;

namespace Microsoft.Maui.Graphics.Controls
{
    public partial class GraphicsViewHandler : ViewHandler<IGraphicsView, object>
	{
		protected override object CreatePlatformView() => throw new NotImplementedException();
	}
}