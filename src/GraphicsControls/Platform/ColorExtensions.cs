﻿using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.ApplicationModel;

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

        public static Color WithDefault(this Color color, string defaultLightColor, string defaultDarkColor)
        {
            if (!color.IsDefault())
                return color;
            else
            {
                if (Application.Current?.RequestedTheme == AppTheme.Light)
                    return Color.FromArgb(defaultLightColor);
                else
                    return Color.FromArgb(defaultDarkColor);
            }
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

        public static Color ContrastColor(this Color color)
        {
            // Calculate the perceptive luminance (aka luma) - human eye favors green color 
            double luma = ((0.299 * color.Red) + (0.587 * color.Green) + (0.114 * color.Blue)) / 255;

            // Return black for bright colors, white for dark colors
            return luma > 0.5 ? Colors.Black : Colors.White;
        }

        /// <summary>
        /// Returns a color which is visible on top of the current color.
        /// If the current color is dark, returns white. If the current color is light, returns black.
        /// </summary>
        /// <param name="color">Color on which we want to base returned color.</param>
        /// <returns>Can return Colors.Black or Colors.White</returns>
        public static Color ComplementaryColor(this Color color)
        {
            if(color == null)
                return Colors.Black;

            color.ToHsl(out _, out _, out var lightness);
            return lightness > 0.4 ? Colors.Black : Colors.White;
        }
    }
}