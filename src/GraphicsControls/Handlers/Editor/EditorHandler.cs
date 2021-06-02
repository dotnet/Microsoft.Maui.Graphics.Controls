using System.Linq;

namespace Microsoft.Maui.Graphics.Controls
{
    public partial class EditorHandler
	{
		public EditorHandler() : base(DrawMapper, PropertyMapper)
		{

		}

		public static PropertyMapper<IEditor> PropertyMapper = new PropertyMapper<IEditor>(ViewHandler.Mapper)
		{
			Actions =
			{
				[nameof(IEditor.Placeholder)] = ViewHandler.MapInvalidate,
				[nameof(IEditor.PlaceholderColor)] = ViewHandler.MapInvalidate,
				[nameof(IEditor.Text)] = ViewHandler.MapInvalidate,
				[nameof(IEditor.TextColor)] = ViewHandler.MapInvalidate
			}
		};

		public static DrawMapper<IEditorDrawable, IEditor> DrawMapper = new DrawMapper<IEditorDrawable, IEditor>(ViewHandler.DrawMapper)
		{
			["Background"] = MapDrawBackground,
			["Border"] = MapDrawBorder,
			["Placeholder"] = MapDrawPlaceholder
		};

		public static string[] DefaultEditorLayerDrawingOrder =
			ViewHandler.DefaultLayerDrawingOrder.ToList().InsertAfter(new string[]
			{
				"Background",
				"Border",
				"Placeholder",
			}, "Background").ToArray();

		public override string[] LayerDrawingOrder() =>
			DefaultEditorLayerDrawingOrder;

		protected override IEditorDrawable CreateDrawable() =>
			new MaterialEditorDrawable();

		public static void MapDrawBackground(ICanvas canvas, RectangleF dirtyRect, IEditorDrawable drawable, IEditor view)
			=> drawable.DrawBackground(canvas, dirtyRect, view);

		public static void MapDrawBorder(ICanvas canvas, RectangleF dirtyRect, IEditorDrawable drawable, IEditor view)
			=> drawable.DrawBorder(canvas, dirtyRect, view);

		public static void MapDrawPlaceholder(ICanvas canvas, RectangleF dirtyRect, IEditorDrawable drawable, IEditor view)
			=> drawable.DrawPlaceholder(canvas, dirtyRect, view);
	}
}