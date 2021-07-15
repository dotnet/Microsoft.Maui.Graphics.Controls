#if __IOS__ || MACCATALYST
using NativeView = UIKit.UIView;
#elif MONOANDROID
using NativeView = Android.Views.View;
#elif WINDOWS
using NativeView = Microsoft.UI.Xaml.FrameworkElement;
#elif NETSTANDARD
using NativeView = System.Object;
# endif

namespace Microsoft.Maui.Graphics.Controls
{
    public class ViewHandler
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

		public static void DrawClip(ICanvas canvas, RectangleF dirtyRect, IViewDrawable drawable, IView view) =>
			drawable.DrawClip(canvas, dirtyRect, view);

		public static void DrawBackground(ICanvas canvas, RectangleF dirtyRect, IViewDrawable drawable, IView view) =>
			drawable.DrawBackground(canvas, dirtyRect, view);

		public static void DrawBorder(ICanvas canvas, RectangleF dirtyRect, IViewDrawable drawable, IView view) =>
			drawable.DrawBorder(canvas, dirtyRect, view);

		public static void DrawText(ICanvas canvas, RectangleF dirtyRect, IViewDrawable drawable, IView view)
		{
			if(view is IText text)
				drawable.DrawText(canvas, dirtyRect, text);
		}

		public static void DrawOverlay(ICanvas canvas, RectangleF dirtyRect, IViewDrawable drawable, IView view) =>
			drawable.DrawOverlay(canvas, dirtyRect, view);

		public static readonly PropertyMapper<IView> Mapper = new PropertyMapper<IView>(Handlers.ViewHandler.ViewMapper)
		{
			[nameof(IView.AutomationId)] = MapAutomationId,
			[nameof(IView.Background)] = MapInvalidate,
			[nameof(IView.IsEnabled)] = MapIsEnabled,
			[nameof(IView.Frame)] = MapInvalidate,
			[nameof(IView.Semantics)] = MapSemantics,
			[nameof(IText.Text)] = MapInvalidate,
			[nameof(IText.Font)] = MapInvalidate
		};

		public static void MapInvalidate(IViewHandler handler, IView view) =>
			(handler as IGraphicsHandler)?.Invalidate();

		public static void MapAutomationId(IViewHandler handler, IView view)
		{
			((NativeView?)handler.NativeView)?.UpdateAutomationId(view);
		}
		
		public static void MapIsEnabled(IViewHandler handler, IView view)
        {
			((NativeView?)handler.NativeView)?.UpdateIsEnabled(view);

			(handler as IGraphicsHandler)?.Invalidate();
		}

		public static void MapSemantics(IViewHandler handler, IView view)
		{
			((NativeView?)handler.NativeView)?.UpdateSemantics(view);
		}
	}
}