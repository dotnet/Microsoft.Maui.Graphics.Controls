using System.Graphics;
using GraphicsControls.Extensions;
using GraphicsControls.Helpers;
using Xamarin.Forms;
using GColor = System.Graphics.Color;

namespace GraphicsControls
{
    public partial class Entry
    {
        const string MaterialEntryIndicatorIcon = "M9.295 7.885C9.68436 8.27436 9.68436 8.90564 9.295 9.295C8.90564 9.68436 8.27436 9.68436 7.885 9.295L5 6.41L2.115 9.295C1.72564 9.68436 1.09436 9.68436 0.705 9.295C0.315639 8.90564 0.315639 8.27436 0.705 7.885L3.59 5L0.705 2.115C0.315639 1.72564 0.31564 1.09436 0.705 0.705C1.09436 0.315639 1.72564 0.315639 2.115 0.705L5 3.59L7.885 0.705C8.27436 0.315639 8.90564 0.31564 9.295 0.705C9.68436 1.09436 9.68436 1.72564 9.295 2.115L6.41 5L9.295 7.885Z";

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

        void DrawMaterialEntryBackground(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            canvas.FillColor = BackgroundColor.ToGraphicsColor(Material.Color.Gray5, Material.Color.Dark);

            var width = dirtyRect.Width;

            var vBuilder = new PathBuilder();
            var path =
                vBuilder.BuildPath(
                    $"M0 4C0 1.79086 1.79086 0 4 0H{width - 4}C{width - 2}.209 0 {width} 1.79086 {width} 4V56H0V4Z");

            canvas.FillPath(path);

            canvas.RestoreState();
        }

        void DrawMaterialEntryBorder(ICanvas canvas, RectangleF dirtyRect)
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
            var y = 53.91f;

            var width = dirtyRect.Width;
            var height = strokeWidth;

            canvas.FillRectangle(x, y, width, height);

            canvas.RestoreState();
        }

        void DrawMaterialEntryPlaceholder(ICanvas canvas, RectangleF dirtyRect)
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

        void DrawMaterialEntryIndicators(ICanvas canvas, RectangleF dirtyRect)
        {
            if (!string.IsNullOrEmpty(Text))
            {
                canvas.SaveState();

                float radius = 12f;

                var backgroundMarginX = 24;
                var backgroundMarginY = 28;

                var x = dirtyRect.Width - backgroundMarginX;
                var y = dirtyRect.Y + backgroundMarginY;

                if (FlowDirection == FlowDirection.RightToLeft)
                    x = backgroundMarginX;

                canvas.FillColor = BackgroundColor.ToGraphicsColor(Material.Color.Black, Material.Color.White);
                canvas.Alpha = Application.Current?.RequestedTheme == OSAppTheme.Light ? 0.12f : 0.24f;

                canvas.FillCircle(x, y, radius);

                canvas.RestoreState();

                _indicatorRect = new RectangleF(x - radius, y - radius, radius * 2, radius * 2);

                canvas.SaveState();

                var iconMarginX = 29;
                var iconMarginY = 23;

                var tX = dirtyRect.Width - iconMarginX;
                var tY = dirtyRect.Y + iconMarginY;

                if (FlowDirection == FlowDirection.RightToLeft)
                {
                    iconMarginX = 19;
                    tX = iconMarginX;
                }

                canvas.Translate(tX, tY);

                var vBuilder = new PathBuilder();
                var path = vBuilder.BuildPath(MaterialEntryIndicatorIcon);

                canvas.FillColor = BackgroundColor.ToGraphicsColor(Material.Color.Black, Material.Color.White);
                canvas.FillPath(path);

                canvas.RestoreState();
            }
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