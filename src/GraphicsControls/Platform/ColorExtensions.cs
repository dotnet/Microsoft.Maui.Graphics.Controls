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
                return Color.FromHex(defaultColor);
        }

        public static Color ToColor(this string hex)
        {
            return Color.FromHex(hex);
        }
    }
}