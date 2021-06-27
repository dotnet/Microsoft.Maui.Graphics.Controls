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

		RectangleF trackRect = new RectangleF();
		public RectangleF TrackRect => trackRect;

		RectangleF touchTargetRect = new RectangleF(0, 0, 44, 44);
		public RectangleF TouchTargetRect => touchTargetRect;

		public virtual void DrawBackground(ICanvas canvas, RectangleF dirtyRect, ISlider slider) 
		{
			canvas.SaveState();

			canvas.FillColor = slider.MaximumTrackColor.WithDefault(Fluent.Color.Primary.ThemeLight);

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

			canvas.FillColor = slider.MinimumTrackColor.WithDefault(slider.IsEnabled ? Fluent.Color.Primary.ThemePrimary : Fluent.Color.Primary.ThemeSecondary);

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
			canvas.SaveState();

			var size = 16f;
			var strokeWidth = 2f;

			canvas.StrokeColor = slider.ThumbColor.WithDefault(Fluent.Color.Primary.ThemePrimary);

			canvas.StrokeSize = strokeWidth;

			var value = (slider.Value / slider.Maximum - slider.Minimum).Clamp(0, 1);

			stateDefaultValues.TryGetValue("TextSize", out var textSize);
			var x = (float)(((dirtyRect.Width - (float)textSize - TextMargin) * value) - (size / 2));

			if (x <= strokeWidth)
				x = strokeWidth / 2;

			if (x >= dirtyRect.Width - (size + strokeWidth))
				x = dirtyRect.Width - (size + strokeWidth);

			var y = (float)((dirtyRect.Height - size) / 2);

			touchTargetRect.Center(new PointF(x, y));

			canvas.FillColor = Colors.Black;

			canvas.FillEllipse(x, y, size, size);
			canvas.DrawEllipse(x, y, size, size);

			canvas.RestoreState();
		}

		public virtual void DrawText(ICanvas canvas, RectangleF dirtyRect, ISlider slider)
		{
			canvas.SaveState();

			canvas.FontColor = Fluent.Color.Foreground.Black.ToColor();
			canvas.FontSize = 14f;

			var height = dirtyRect.Height;
			var width = dirtyRect.Width;

			stateDefaultValues.TryGetValue("TextSize", out var textSize);

			var x = (float)(width - (float)textSize + TextMargin);
			var y = 2;

			canvas.SetToBoldSystemFont();

			string valueString = slider.Value.Clamp(slider.Minimum, slider.Maximum).ToString("####0.00");

			canvas.DrawString(valueString, x, y, (float)textSize, height, GHorizontalAlignment.Left, GVerticalAlignment.Center);

			canvas.RestoreState();
		}

		public override Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
			new Size(widthConstraint, 20f);
	}
}