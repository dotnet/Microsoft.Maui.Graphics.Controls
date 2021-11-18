using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Controls;

namespace GraphicsControls.Sample.Controls
{
    public class CustomSliderMapper : Slider
	{

	}

	public class CustomSliderMapperHandler : SliderHandler
    {
		public CustomSliderMapperHandler()
		{
			DrawMapper[nameof(ISliderDrawable.DrawThumb)] = (canvas, rect, drawable, slider) =>
			{
				canvas.SaveState();

				float size = 28f;
				float strokeWidth = 0.5f;

				canvas.StrokeColor = slider.ThumbColor.WithDefault(Colors.White.ToArgbHex());
				canvas.StrokeSize = strokeWidth;

				var value = (slider.Value / slider.Maximum) - slider.Minimum;
				var x = (float)((rect.Width * value) - (size / 2));

				if (x <= strokeWidth)
					x = strokeWidth;

				if (x >= rect.Width - (size + strokeWidth))
					x = rect.Width - (size + strokeWidth);

				var y = (float)((rect.Height - size) / 2);

				canvas.FillColor = slider.ThumbColor.WithDefault(slider.IsEnabled ? Colors.White.ToArgbHex() : Colors.Gray.ToArgbHex());

				canvas.SetShadow(new SizeF(1, 1), 2, CanvasDefaults.DefaultShadowColor);
								
				canvas.FillEllipse(x, y, size, size);
				canvas.DrawEllipse(x, y, size, size);

				canvas.RestoreState();
			};
		}
	}
}