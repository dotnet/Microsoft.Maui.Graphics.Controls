using Microsoft.Maui.Controls;
using System.Collections.Generic;
using GHorizontalAlignment = Microsoft.Maui.Graphics.HorizontalAlignment;
using GVerticalAlignment = Microsoft.Maui.Graphics.VerticalAlignment;

namespace Microsoft.Maui.Graphics.Controls
{
    public class FluentSliderDrawable : ViewDrawable<ISlider>, ISliderDrawable
    {
        const double TextMargin = 6.0d;

        static readonly Dictionary<string, object> stateDefaultValues = new Dictionary<string, object>
        {
            ["TextSize"] = 36f
        };

        public bool IsDragging { get; set; }

        RectangleF trackRect = new RectangleF();
        public RectangleF TrackRect => trackRect;

        RectangleF touchTargetRect = new RectangleF(0, 0, 44, 44);
        public RectangleF TouchTargetRect => touchTargetRect;

        public virtual void DrawBackground(ICanvas canvas, RectangleF dirtyRect, ISlider slider)
        {
            canvas.SaveState();

            if (slider.IsEnabled)
                canvas.FillColor = slider.MaximumTrackColor.WithDefault(Fluent.Color.Light.Control.Border.Default, Fluent.Color.Dark.Control.Border.Default);
            else
                canvas.FillColor = slider.MaximumTrackColor.WithDefault(Fluent.Color.Light.Control.Border.Disabled, Fluent.Color.Light.Control.Border.Disabled);

            var x = dirtyRect.X;

            stateDefaultValues.TryGetValue("TextSize", out var textSize);
            var width = dirtyRect.Width - (float)textSize;
            var height = 4;

            var y = (float)((dirtyRect.Height - height) / 2);

            trackRect.X = x;
            trackRect.Width = width;

            canvas.FillRoundedRectangle(x, y, width, height, 4);

            canvas.RestoreState();
        }

        public virtual void DrawTrackProgress(ICanvas canvas, RectangleF dirtyRect, ISlider slider)
        {
            canvas.SaveState();

            if (slider.IsEnabled)
                canvas.FillColor = slider.MaximumTrackColor.WithDefault(Fluent.Color.Light.Accent.Primary, Fluent.Color.Dark.Accent.Primary);
            else
                canvas.FillColor = slider.MaximumTrackColor.WithDefault(Fluent.Color.Light.Accent.Disabled, Fluent.Color.Dark.Accent.Disabled);

            stateDefaultValues.TryGetValue("TextSize", out var textSize);

            var value = (slider.Value / slider.Maximum - slider.Minimum).Clamp(0, 1);

            var width = (float)((dirtyRect.Width - (float)textSize - TextMargin) * value);
            var height = 4;

            var x = dirtyRect.X;
            var y = (float)((dirtyRect.Height - height) / 2);

            canvas.FillRoundedRectangle(x, y, width, height, 4);

            canvas.RestoreState();
        }

        public virtual void DrawThumb(ICanvas canvas, RectangleF dirtyRect, ISlider slider)
        {
            // Thumb
            canvas.SaveState();

            var thumbSize = 20f;
            var strokeWidth = 1f;

            var value = (slider.Value / slider.Maximum - slider.Minimum).Clamp(0, 1);

            stateDefaultValues.TryGetValue("TextSize", out var textSize);
            var x = (float)(((dirtyRect.Width - (float)textSize - TextMargin) * value) - (thumbSize / 2));

            if (x <= strokeWidth)
                x = strokeWidth / 2;

            if (x >= dirtyRect.Width - (thumbSize + strokeWidth))
                x = dirtyRect.Width - (thumbSize + strokeWidth);

            var y = (float)((dirtyRect.Height - thumbSize) / 2);

            touchTargetRect.Center(new PointF(x, y));

                canvas.FillColor = slider.ThumbColor.WithDefault(Fluent.Color.Light.Control.Background.Default, Fluent.Color.Dark.Control.Background.Default);
        
            canvas.FillEllipse(x, y, thumbSize, thumbSize);

            canvas.RestoreState();

            // Inner Thumb
            if (!slider.IsEnabled)
            {
                canvas.SaveState();

                var innerThumbSize = 10f;

                canvas.FillColor = slider.ThumbColor.WithDefault(Fluent.Color.Light.Accent.Disabled, Fluent.Color.Dark.Accent.Disabled);

                var margin = (thumbSize - innerThumbSize) / 2;

                canvas.FillEllipse(x + margin, y + margin, innerThumbSize, innerThumbSize);

                canvas.RestoreState();
            }
               
            if (IsDragging)
            {
                canvas.SaveState();

                var innerThumbSize = 14f;

                canvas.FillColor = slider.ThumbColor.WithDefault(Fluent.Color.Light.Accent.Primary, Fluent.Color.Dark.Accent.Primary);

                var margin = (thumbSize - innerThumbSize) / 2;

                canvas.FillEllipse(x + margin, y + margin, innerThumbSize, innerThumbSize);

                canvas.RestoreState();
            }

            // Thumb Border
            if (slider.ThumbColor == null)
            {
                canvas.SaveState();

                canvas.StrokeSize = strokeWidth;

                canvas.StrokeColor = (Application.Current?.RequestedTheme == OSAppTheme.Light) ? Fluent.Color.Light.Control.Border.Default.ToColor() : Fluent.Color.Dark.Control.Border.Default.ToColor();

                canvas.DrawEllipse(x, y, thumbSize, thumbSize);

                canvas.RestoreState();
            }
        }

        public virtual void DrawText(ICanvas canvas, RectangleF dirtyRect, ISlider slider)
        {
            canvas.SaveState();

            canvas.FontColor = (Application.Current?.RequestedTheme == OSAppTheme.Light) ? Fluent.Color.Light.Foreground.Primary.ToColor() : Fluent.Color.Dark.Foreground.Primary.ToColor();
            canvas.FontSize = 14f;

            var height = dirtyRect.Height;
            var width = dirtyRect.Width;

            stateDefaultValues.TryGetValue("TextSize", out var textSize);

            var x = (float)(width - (float)textSize + TextMargin);
            var y = 2;

            string valueString = slider.Value.Clamp(slider.Minimum, slider.Maximum).ToString("####0.00");

            canvas.DrawString(valueString, x, y, (float)textSize, height, GHorizontalAlignment.Left, GVerticalAlignment.Center);

            canvas.RestoreState();
        }

        public override Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
            new Size(widthConstraint, 20f);
    }
}