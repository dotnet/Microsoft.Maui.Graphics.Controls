using System.Linq;

namespace Microsoft.Maui.Graphics.Controls
{
    public class CheckBoxHandler : GraphicsControlHandler<ICheckBoxDrawable, ICheckBox>
    {
        readonly DrawableType _drawableType;

        // Constructor for CheckBoxHandler with no arguments
        public CheckBoxHandler() : base(DrawMapper, PropertyMapper)
        {
        }

        // Constructor for CheckBoxHandler with DrawableType argument
        public CheckBoxHandler(DrawableType drawableType) : base(DrawMapper, PropertyMapper)
        {
            _drawableType = drawableType;
        }

        // PropertyMapper for ICheckBox properties
        public static PropertyMapper<ICheckBox> PropertyMapper = new PropertyMapper<ICheckBox>(ViewHandler.Mapper)
        {
            [nameof(ICheckBox.IsChecked)] = ViewHandler.MapInvalidate,
            [nameof(ICheckBox.Foreground)] = ViewHandler.MapInvalidate,
        };

        // DrawMapper for ICheckBoxDrawable and ICheckBox
        public static DrawMapper<ICheckBoxDrawable, ICheckBox> DrawMapper = new DrawMapper<ICheckBoxDrawable, ICheckBox>(ViewHandler.DrawMapper)
        {
            ["Background"] = MapDrawBackground,
            ["Mark"] = MapDrawMark,
            ["Text"] = MapDrawText
        };

        // Default drawing order for CheckBox layers
        public static string[] DefaultCheckBoxLayerDrawingOrder =
            ViewHandler.DefaultLayerDrawingOrder.ToList().InsertAfter(new string[]
            {
                "Background",
                "Mark",
            }, "Text").ToArray();

        // Override method to return the layer drawing order
        public override string[] LayerDrawingOrder() =>
            DefaultCheckBoxLayerDrawingOrder;

        // Create an ICheckBoxDrawable based on the DrawableType
        protected override ICheckBoxDrawable CreateDrawable()
        {
            switch (_drawableType)
            {
                default:
                case DrawableType.Material:
                    return new MaterialCheckBoxDrawable();
                case DrawableType.Cupertino:
                    return new CupertinoCheckBoxDrawable();
                case DrawableType.Fluent:
                    return new FluentCheckBoxDrawable();
            }
        }

        // Method to draw the background of the CheckBox
        public static void MapDrawBackground(ICanvas canvas, RectF dirtyRect, ICheckBoxDrawable drawable, ICheckBox view)
            => drawable.DrawBackground(canvas, dirtyRect, view);

        // Method to draw the check mark of the CheckBox
        public static void MapDrawMark(ICanvas canvas, RectF dirtyRect, ICheckBoxDrawable drawable, ICheckBox view)
            => drawable.DrawMark(canvas, dirtyRect, view);

        // Method to draw the text of the CheckBox
        public static void MapDrawText(ICanvas canvas, RectF dirtyRect, ICheckBoxDrawable drawable, ICheckBox view)
            => drawable.DrawText(canvas, dirtyRect, view);

        // Override method to handle user interaction with the CheckBox
        public override bool StartInteraction(PointF[] points)
        {
            if (VirtualView != null)
                VirtualView.IsChecked = !VirtualView.IsChecked;

            return base.StartInteraction(points);
        }
    }
}

