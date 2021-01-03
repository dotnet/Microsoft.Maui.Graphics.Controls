using Xamarin.Forms;
using GColor = System.Graphics.Color;
using XColor = Xamarin.Forms.Color;

namespace GraphicsControls.Helpers
{
    public static class ColorHelper
    {
        public static GColor GetGraphicsColor(string lightColor, string darkColor)
        {
            return new GColor(Application.Current?.RequestedTheme == OSAppTheme.Light ? lightColor : darkColor);
        }

        public static GColor GetGraphicsColor(XColor lightColor, XColor darkColor)
        {
            return new GColor(Application.Current?.RequestedTheme == OSAppTheme.Light ? lightColor.ToHex() : darkColor.ToHex());
        }

        public static XColor ChangeColorBrightness(XColor color, float correctionFactor)
        {
            float red = (float)color.R;
            float green = (float)color.G;
            float blue = (float)color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            return XColor.FromRgba((int)red, (int)green, (int)blue, color.A);
        }
    }
}
