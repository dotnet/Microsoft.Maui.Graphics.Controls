using System.Linq;

namespace Microsoft.Maui.Graphics.Controls
{
    public class LabelHandler : GraphicsControlHandler<ILabelDrawable, ILabel>
    {
        public LabelHandler() : base(DrawMapper, PropertyMapper)
        {

        }

        public static PropertyMapper<ILabel> PropertyMapper = new PropertyMapper<ILabel>(ViewHandler.Mapper)
        {
            [nameof(ILabel.Text)] = ViewHandler.MapInvalidate,
            [nameof(ILabel.TextColor)] = ViewHandler.MapInvalidate,
            [nameof(ILabel.Font)] = ViewHandler.MapInvalidate,
            [nameof(ILabel.CharacterSpacing)] = ViewHandler.MapInvalidate,
            [nameof(ILabel.HorizontalTextAlignment)] = ViewHandler.MapInvalidate,
            [nameof(ILabel.VerticalTextAlignment)] = ViewHandler.MapInvalidate,
        };

        public static DrawMapper<ILabelDrawable, ILabel> DrawMapper = new DrawMapper<ILabelDrawable, ILabel>(ViewHandler.DrawMapper)
        {
            ["Background"] = MapDrawBackground,
            ["Text"] = MapDrawText
        };

        public static string[] DefaultLabelLayerDrawingOrder =
            ViewHandler.DefaultLayerDrawingOrder.ToList().InsertAfter(new string[]
            {
                "Background",
                "Text",
            }, "Text").ToArray();

        public override string[] LayerDrawingOrder() =>
            DefaultLabelLayerDrawingOrder;

        protected override ILabelDrawable CreateDrawable()
        {
            return new LabelDrawable();
        }

        public static void MapDrawBackground(ICanvas canvas, RectF dirtyRect, ILabelDrawable drawable, ILabel view)
            => drawable.DrawBackground(canvas, dirtyRect, view);

        public static void MapDrawText(ICanvas canvas, RectF dirtyRect, ILabelDrawable drawable, ILabel view) 
            => drawable.DrawText(canvas, dirtyRect, view);
    }
}
