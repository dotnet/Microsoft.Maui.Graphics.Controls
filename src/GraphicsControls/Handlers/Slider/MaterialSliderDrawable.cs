namespace Microsoft.Maui.Graphics.Controls
{
	public class MaterialSliderDrawable : ViewDrawable<ISlider>, ISliderDrawable
	{
		RectangleF trackRect = new RectangleF();
		public RectangleF TrackRect => trackRect;

		RectangleF touchTargetRect = new RectangleF(0, 0, 44, 44);
		public RectangleF TouchTargetRect => touchTargetRect;

		public virtual void DrawBackground(ICanvas canvas, RectangleF dirtyRect, ISlider slider)
		{
			canvas.SaveState();

			canvas.FillColor = slider.MaximumTrackColor.WithDefault(VirtualView.IsEnabled ? Material.Color.LightBlue : Material.Color.Gray3);

			var x = dirtyRect.X;

			var width = dirtyRect.Width;
			var height = 2;

			trackRect.X = x;
			trackRect.Width = width;

			var y = (float)((dirtyRect.Height - height) / 2);

			canvas.FillRoundedRectangle(x, y, width, height, 0);

			canvas.RestoreState();
		}

		public virtual void DrawTrackProgress(ICanvas canvas, RectangleF dirtyRect, ISlider slider)
		{
			canvas.SaveState();

			canvas.FillColor = slider.MinimumTrackColor.WithDefault(slider.IsEnabled ? Material.Color.Blue : Material.Color.Gray1);

			var x = dirtyRect.X;

			var value = (slider.Value / slider.Maximum - slider.Minimum).Clamp(0, 1);
			var width = (float)(dirtyRect.Width * value);

			var height = 2;

			var y = (float)((dirtyRect.Height - height) / 2);

			canvas.FillRoundedRectangle(x, y, width, height, 0);

			canvas.RestoreState();
		}

		public virtual void DrawThumb(ICanvas canvas, RectangleF dirtyRect, ISlider slider)
		{
			var MaterialFloatThumb = 12f;

			canvas.SaveState();

			var value = (slider.Value / slider.Maximum - slider.Minimum).Clamp(0, 1);
			var x = (float)((dirtyRect.Width * value) - (MaterialFloatThumb / 2));

			if (x <= 0)
				x = 0;

			if (x >= dirtyRect.Width - MaterialFloatThumb)
				x = dirtyRect.Width - MaterialFloatThumb;

			var y = (float)((dirtyRect.Height - MaterialFloatThumb) / 2);

			canvas.FillColor = slider.ThumbColor.WithDefault(slider.IsEnabled ? Material.Color.Blue : Material.Color.Gray1);

			touchTargetRect.Center(new PointF(x, y));

			canvas.FillEllipse(x, y, MaterialFloatThumb, MaterialFloatThumb);

			canvas.RestoreState();
		}

		public virtual void DrawText(ICanvas canvas, RectangleF dirtyRect, ISlider slider)
		{

		}

		public override Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
			new Size(widthConstraint, 20f);
	}
}