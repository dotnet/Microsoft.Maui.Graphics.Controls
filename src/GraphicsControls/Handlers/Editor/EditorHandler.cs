using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Animations;
using System.Linq;

namespace Microsoft.Maui.Graphics.Controls
{
    public partial class EditorHandler
    {
        readonly DrawableType _drawableType;
        IAnimationManager? _animationManager;

        public EditorHandler() : base(DrawMapper, PropertyMapper)
        {

        }

        public EditorHandler(DrawableType drawableType) : base(DrawMapper, PropertyMapper)
        {
            _drawableType = drawableType;
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

        protected override IEditorDrawable CreateDrawable()
        {
            switch (_drawableType)
            {
                default:
                case DrawableType.Material:
                    return new MaterialEditorDrawable();
                case DrawableType.Cupertino:
                    return new CupertinoEditorDrawable();
                case DrawableType.Fluent:
                    return new FluentEditorDrawable();
            }
        }

        public static void MapDrawBackground(ICanvas canvas, RectF dirtyRect, IEditorDrawable drawable, IEditor view)
            => drawable.DrawBackground(canvas, dirtyRect, view);

        public static void MapDrawBorder(ICanvas canvas, RectF dirtyRect, IEditorDrawable drawable, IEditor view)
            => drawable.DrawBorder(canvas, dirtyRect, view);

        public static void MapDrawPlaceholder(ICanvas canvas, RectF dirtyRect, IEditorDrawable drawable, IEditor view)
            => drawable.DrawPlaceholder(canvas, dirtyRect, view);

        internal void AnimatePlaceholder()
        {
            if (!(Drawable is MaterialEditorDrawable))
                return;

            bool hasText = !string.IsNullOrEmpty(VirtualView.Text);

            if (hasText)
                return;

            if (_animationManager == null)
                _animationManager = MauiContext?.Services.GetRequiredService<IAnimationManager>();

            float start = Drawable.HasFocus ? 0 : 1;
            float end = Drawable.HasFocus ? 1 : 0;

            _animationManager?.Add(new Animation(callback: (progress) =>
            {
                Drawable.AnimationPercent = start.Lerp(end, progress);
                Invalidate();
            }, duration: 0.1, easing: Easing.Linear));
        }
    }
}