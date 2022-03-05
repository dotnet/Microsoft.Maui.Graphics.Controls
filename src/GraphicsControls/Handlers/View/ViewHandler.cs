using Microsoft.Maui.Platform;

#if __IOS__ || MACCATALYST
using PlatformView = UIKit.UIView;
#elif MONOANDROID
using PlatformView = Android.Views.View;
#elif WINDOWS
using PlatformView = Microsoft.UI.Xaml.FrameworkElement;
#elif NETSTANDARD
using PlatformView = System.Object;
# endif

namespace Microsoft.Maui.Graphics.Controls
{
    public abstract class ViewHandler
	{
		public static string[] DefaultLayerDrawingOrder = new string[]
		{
			"Clip",
			"Background",
			"Border",
			"Text",
			"Overlay"
		};

		public static DrawMapper<IViewDrawable, IView> DrawMapper = new DrawMapper<IViewDrawable, IView>()
		{
			["Clip"] = DrawClip,
			["Background"] = DrawBackground,
			["Border"] = DrawBorder,
			["Text"] = DrawText,
			["Overlay"] = DrawOverlay
		};

		public static void DrawClip(ICanvas canvas, RectF dirtyRect, IViewDrawable drawable, IView view) =>
			drawable.DrawClip(canvas, dirtyRect, view);

		public static void DrawBackground(ICanvas canvas, RectF dirtyRect, IViewDrawable drawable, IView view) =>
			drawable.DrawBackground(canvas, dirtyRect, view);

		public static void DrawBorder(ICanvas canvas, RectF dirtyRect, IViewDrawable drawable, IView view) =>
			drawable.DrawBorder(canvas, dirtyRect, view);

		public static void DrawText(ICanvas canvas, RectF dirtyRect, IViewDrawable drawable, IView view)
		{
			if(view is IText text)
				drawable.DrawText(canvas, dirtyRect, text);
		}

		public static void DrawOverlay(ICanvas canvas, RectF dirtyRect, IViewDrawable drawable, IView view) =>
			drawable.DrawOverlay(canvas, dirtyRect, view);

		public static readonly PropertyMapper<IView, IElementHandler> Mapper = new PropertyMapper<IView, IElementHandler>(Handlers.ViewHandler.ViewMapper)
		{
			[nameof(IView.AutomationId)] = MapAutomationId,
			[nameof(IView.Background)] = MapInvalidate,
			[nameof(IView.IsEnabled)] = MapIsEnabled,
			[nameof(IView.Frame)] = MapInvalidate,
			[nameof(IView.Semantics)] = MapSemantics,
			[nameof(IText.Text)] = MapInvalidate,
			[nameof(IText.Font)] = MapInvalidate
		};

		public static void MapInvalidate(IElementHandler handler, IView view) =>
			(handler as IGraphicsHandler)?.Invalidate();

		public static void MapAutomationId(IElementHandler handler, IView view)
		{
			((PlatformView?)handler.PlatformView)?.UpdateAutomationId(view);
		}
		
		public static void MapIsEnabled(IElementHandler handler, IView view)
        {
			((PlatformView?)handler.PlatformView)?.UpdateIsEnabled(view);

			(handler as IGraphicsHandler)?.Invalidate();
		}

		public static void MapSemantics(IElementHandler handler, IView view)
		{
			((PlatformView?)handler.PlatformView)?.UpdateSemantics(view);
		}
	}
}