﻿using Microsoft.Maui.Animations;
using Microsoft.Maui.Controls;

namespace Microsoft.Maui.Graphics.Controls
{
    public class MaterialEntryDrawable : ViewDrawable<IEntry>, IEntryDrawable
    {
        const string MaterialEntryIndicatorIcon = "M9.295 7.885C9.68436 8.27436 9.68436 8.90564 9.295 9.295C8.90564 9.68436 8.27436 9.68436 7.885 9.295L5 6.41L2.115 9.295C1.72564 9.68436 1.09436 9.68436 0.705 9.295C0.315639 8.90564 0.315639 8.27436 0.705 7.885L3.59 5L0.705 2.115C0.315639 1.72564 0.31564 1.09436 0.705 0.705C1.09436 0.315639 1.72564 0.315639 2.115 0.705L5 3.59L7.885 0.705C8.27436 0.315639 8.90564 0.31564 9.295 0.705C9.68436 1.09436 9.68436 1.72564 9.295 2.115L6.41 5L9.295 7.885Z";

        const float FocusedPlaceholderFontSize = 12f;
        const float UnfocusedPlaceholderFontSize = 16f;

        const float FocusedPlaceholderPosition = 6f;
        const float UnfocusedPlaceholderPosition = 22f;

        RectF indicatorRect = new RectF();
        public RectF IndicatorRect => indicatorRect;

        public bool HasFocus { get; set; }

        public double AnimationPercent { get; set; }

        public void DrawBackground(ICanvas canvas, RectF dirtyRect, IEntry entry)
        {
            canvas.SaveState();

            if (entry.Background != null)
                canvas.SetFillPaint(entry.Background, dirtyRect);
            else
            {
                if (Application.Current?.RequestedTheme == AppTheme.Light)
                    canvas.FillColor = entry.IsEnabled ? Material.Color.Light.Gray5.ToColor() : Material.Color.Light.Gray3.ToColor();
                else
                    canvas.FillColor = entry.IsEnabled ? Material.Color.Dark.Gray5.ToColor() : Material.Color.Dark.Gray3.ToColor();
            }

            const float cornerRadius = 4.0f;

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var height = dirtyRect.Height;
            var width = dirtyRect.Width;

            canvas.FillRoundedRectangle(x, y, width, height, cornerRadius, cornerRadius, 0, 0);

            canvas.RestoreState();
        }

        public void DrawBorder(ICanvas canvas, RectF dirtyRect, IEntry entry)
        {
            canvas.SaveState();

            var strokeWidth = 1.0f;
            canvas.FillColor = (Application.Current?.RequestedTheme == AppTheme.Light) ? Material.Color.Black.ToColor() : Material.Color.Light.Gray6.ToColor().WithAlpha(0.5f);

            if (entry.IsEnabled && HasFocus)
            {
                strokeWidth = 2.0f;
                canvas.FillColor = Material.Color.Blue.ToColor();
            }

            var x = dirtyRect.X;
            var y = 53.91f;

            var width = dirtyRect.Width;
            var height = strokeWidth;

            canvas.FillRectangle(x, y, width, height);

            canvas.RestoreState();
        }

        public void DrawIndicator(ICanvas canvas, RectF dirtyRect, IEntry entry)
        {
            if (!string.IsNullOrEmpty(entry.Text))
            {
                canvas.SaveState();

                float radius = 12f;

                var backgroundMarginX = 24;
                var backgroundMarginY = 28;

                var x = dirtyRect.Width - backgroundMarginX;
                var y = dirtyRect.Y + backgroundMarginY;

                if (entry.FlowDirection == FlowDirection.RightToLeft)
                    x = backgroundMarginX;

                if (entry.Background != null)
                {
                    var background = entry.Background;

                    if (background is SolidPaint solidPaint)
                    {
                        var color = solidPaint.Color.Darker();
                        canvas.FillColor = color;
                    }
                    else
                        canvas.SetFillPaint(entry.Background, dirtyRect);
                }
                else
                    canvas.FillColor = Material.Color.Black.ToColor();

                canvas.Alpha = 0.12f;

                canvas.FillCircle(x, y, radius);

                canvas.RestoreState();

                indicatorRect = new RectF(x - radius, y - radius, radius * 2, radius * 2);

                canvas.SaveState();

                var iconMarginX = 29;
                var iconMarginY = 23;

                var tX = dirtyRect.Width - iconMarginX;
                var tY = dirtyRect.Y + iconMarginY;

                if (entry.FlowDirection == FlowDirection.RightToLeft)
                {
                    iconMarginX = 19;
                    tX = iconMarginX;
                }

                canvas.Translate(tX, tY);

                var vBuilder = new PathBuilder();
                var path = vBuilder.BuildPath(MaterialEntryIndicatorIcon);

                if (entry.Background != null)
                {
                    var background = entry.Background;

                    if (background is SolidPaint solidPaint)
                    {
                        var color = solidPaint.Color.Lighter();
                        canvas.FillColor = color;
                    }
                    else
                        canvas.SetFillPaint(entry.Background, dirtyRect);
                }
                else
                    canvas.FillColor = Material.Color.Black.ToColor();

                canvas.FillPath(path);

                canvas.RestoreState();
            }
        }

        public void DrawPlaceholder(ICanvas canvas, RectF dirtyRect, IEntry entry)
        {
            canvas.SaveState();

            if (Application.Current?.RequestedTheme == AppTheme.Light)
                canvas.FontColor = Material.Color.DarkBackground.ToColor();
            else
                canvas.FontColor = Material.Color.LightBackground.ToColor();

            var materialPlaceholderFontSize = UnfocusedPlaceholderFontSize.Lerp(FocusedPlaceholderFontSize, AnimationPercent);

            canvas.FontSize = materialPlaceholderFontSize;

            float margin = 12f;

            var horizontalAlignment = HorizontalAlignment.Left;

            var x = dirtyRect.X + margin;

            var height = dirtyRect.Height;
            var width = dirtyRect.Width;

            var materialPlaceholderPosition = UnfocusedPlaceholderPosition.Lerp(FocusedPlaceholderPosition, AnimationPercent);

            canvas.DrawString(entry.Placeholder, x, materialPlaceholderPosition, width - margin, height, horizontalAlignment, VerticalAlignment.Top);

            canvas.RestoreState();
        }

        public override Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
            new Size(widthConstraint, 56f);
    }
}
