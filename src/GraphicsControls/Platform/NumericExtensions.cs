using System;

namespace Microsoft.Maui.Graphics.Controls
{
    public static class NumericExtensions
    {
		public static double Clamp(this double self, double min, double max)
		{
			if (max < min)
			{
				return max;
			}
			else if (self < min)
			{
				return min;
			}
			else if (self > max)
			{
				return max;
			}

			return self;
		}

	}
}
