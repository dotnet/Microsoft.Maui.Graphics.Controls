using System.Linq;

namespace Microsoft.Maui.Graphics.Controls
{
    public partial class EditorHandler
	{
		public EditorHandler() : base(DrawMapper, PropertyMapper)
		{

		}

		public static PropertyMapper<IEditor, EditorHandler> PropertyMapper = new PropertyMapper<IEditor, EditorHandler>(ViewHandler.Mapper)
		{
			[nameof(IEditor.Background)] = ViewHandler.MapInvalidate,
			[nameof(IEditor.Text)] = MapText,
			[nameof(IEditor.TextColor)] = MapTextColor,
			[nameof(IEditor.Placeholder)] = ViewHandler.MapInvalidate,
			[nameof(IEditor.PlaceholderColor)] = ViewHandler.MapInvalidate,
			[nameof(IEditor.CharacterSpacing)] = MapCharacterSpacing,
			[nameof(IEditor.Font)] = MapFont,
			[nameof(IEditor.IsReadOnly)] = MapIsReadOnly,
			[nameof(IEditor.IsTextPredictionEnabled)] = MapIsTextPredictionEnabled,
			[nameof(IEditor.MaxLength)] = MapMaxLength,
			[nameof(IEditor.Keyboard)] = MapKeyboard
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