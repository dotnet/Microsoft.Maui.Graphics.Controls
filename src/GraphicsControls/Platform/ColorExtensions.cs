using Microsoft.Maui.Controls;

namespace Microsoft.Maui.Graphics.Controls
{
    public static class ColorExtensions
    {
        public static Color WithDefault(this Color color, string defaultColor)
        {
            if (!color.IsDefault())
                return color;
            else
                return Color.FromArgb(defaultColor);
        }

        public static Color ToColor(this string hex)
        {
            return Color.FromArgb(hex);
        }

        public static Color Darker(this Color color, double factor = 0.5)
        {
            if (factor < 0 || factor > 1)
                return color;

            int r = (int)(factor * color.Red);
            int g = (int)(factor * color.Green);
            int b = (int)(factor * color.Blue);

            return Color.FromRgb(r, g, b);
        }

        public static Color Lighter(this Color color, double factor = 0.5)
        {
            if (factor < 0 || factor > 1)
                return color;

            int r = (int)(factor * color.Red + (1 - factor) * 255);
            int g = (int)(factor * color.Green + (1 - factor) * 255);
            int b = (int)(factor * color.Blue + (1 - factor) * 255);

            return Color.FromRgb(r, g, b);
        }
    }
}