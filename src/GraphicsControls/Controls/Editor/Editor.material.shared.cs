using System.Graphics;
using GraphicsControls.Extensions;
using GraphicsControls.Helpers;
using Xamarin.Forms;
using GColor = System.Graphics.Color;

namespace GraphicsControls
{
    public partial class Editor
    {
        const float FocusedMaterialPlaceholderFontSize = 12f;
        const float UnfocusedMaterialPlaceholderFontSize = 16f;

        const float FocusedMaterialPlaceholderPosition = 6f;
        const float UnfocusedMaterialPlaceholderPosition = 22f;

        float _placeholderY;
        float _placeholderFontSize;

        float PlaceholderY
        {
            get { return _placeholderY; }
            set
            {
                _placeholderY = value;
                InvalidateDraw();
            }
        }

        float PlaceholderFontSize
        {
            get { return _placeholderFontSize; }
            set
            {
                _placeholderFontSize = value;
                InvalidateDraw();
            }
        }

        void DrawMaterialEditorBackground(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            canvas.FillColor = BackgroundColor.ToGraphicsColor(Material.Color.Gray5, Material.Color.Dark);

            var width = dirtyRect.Width;

            var vBuilder = new PathBuilder();
            var path =
                vBuilder.BuildPath(
                    $"M0 4C0 1.79086 1.79086 0 4 0H{width - 4}C{width - 2}.209 0 {width} 1.79086 {width} 4V114.95H0V4Z");

            canvas.FillPath(path);

            canvas.RestoreState();
        }

        void DrawMaterialEditorBorder(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            var strokeWidth = 1.0f;
            canvas.FillColor = ColorHelper.GetGraphicsColor(Material.Color.Black, Material.Color.White);

            if (IsFocused)
            {
                strokeWidth = 2.0f;
                canvas.FillColor = new GColor(Material.Color.Blue);
            }

            var x = dirtyRect.X;
            var y = 112.91f;

            var width = dirtyRect.Width;
            var height = strokeWidth;

            canvas.FillRectangle(x, y, width, height);

            canvas.RestoreState();
        }

        void DrawMaterialEditorPlaceholder(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            canvas.FontColor = PlaceholderColor.ToGraphicsColor(Material.Color.Dark, Material.Color.Light);
            canvas.FontSize = PlaceholderFontSize;

            float margin = 12f;

            var horizontalAlignment = HorizontalAlignment.Left;

            var x = dirtyRect.X + margin;

            if (FlowDirection == FlowDirection.RightToLeft)
            {
                x = dirtyRect.X;
                horizontalAlignment = HorizontalAlignment.Right;
            }

            var height = dirtyRect.Height;
            var width = dirtyRect.Width;

            canvas.DrawString(Placeholder, x, PlaceholderY, width - margin, height, horizontalAlignment, VerticalAlignment.Top);

            canvas.RestoreState();
        }

        void AnimateMaterialPlaceholder(bool isFocused)
        {
            if (IsFocused && !string.IsNullOrEmpty(Text))
                return;

            var materialPlaceholderAnimation = new Animation();

            float startFontSize = isFocused ? UnfocusedMaterialPlaceholderFontSize : FocusedMaterialPlaceholderFontSize;
            float endFontSize = (isFocused || !string.IsNullOrEmpty(Text)) ? FocusedMaterialPlaceholderFontSize : UnfocusedMaterialPlaceholderFontSize;
            var fontSizeAnimation = new Animation(v => PlaceholderFontSize = (int)v, startFontSize, endFontSize, easing: Easing.Linear);

            float startPosition = isFocused ? UnfocusedMaterialPlaceholderPosition : FocusedMaterialPlaceholderPosition;
            float endPosition = (isFocused || !string.IsNullOrEmpty(Text)) ? FocusedMaterialPlaceholderPosition : UnfocusedMaterialPlaceholderPosition;
            var placeholderPositionAnimation = new Animation(v => PlaceholderY = (int)v, startPosition, endPosition, easing: Easing.Linear);

            materialPlaceholderAnimation.Add(0, 1, fontSizeAnimation);
            materialPlaceholderAnimation.Add(0, 1, placeholderPositionAnimation);

            materialPlaceholderAnimation.Commit(this, "MaterialPlaceholderAnimation", length: 100, finished: (l, c) => materialPlaceholderAnimation = null);
        }
    }
}