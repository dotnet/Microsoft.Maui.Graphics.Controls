using System;
using Microsoft.Maui.Controls;

namespace Microsoft.Maui.Graphics.Controls
{
    public static class ColorExtensions
    {
        public static Color WithDefault(this Color color, string defaultColor)
        {
            if (color != null && !color.IsDefault())
                return color;
            else
                return Color.FromArgb(defaultColor);
        }

        public static Color ToColor(this string hex)
        {
            return Color.FromArgb(hex);
        }

		const float LighterFactor = 1.1f;
		const float DarkerFactor = 0.9f;

		public static Color Lighter(this Color color)
		{
			return new Color(
				color.Red * LighterFactor,
				color.Green * LighterFactor,
				color.Blue * LighterFactor,
				color.Alpha);
		}

		public static Color Darker(this Color color)
		{
			return new Color(
				color.Red * DarkerFactor,
				color.Green * DarkerFactor,
				color.Blue * DarkerFactor,
				color.Alpha);
		}
	}
}