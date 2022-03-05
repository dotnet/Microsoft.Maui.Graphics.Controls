﻿using System.Linq;

namespace Microsoft.Maui.Graphics.Controls
{
    public static class RectangleExtensions
	{
		public static PointF Center(this RectF rectangle) =>
			new PointF(rectangle.X + (rectangle.Width / 2), rectangle.Y + (rectangle.Height / 2));

		public static void Center(this ref RectF rectangle, PointF center)
		{
			var halfWidth = rectangle.Width / 2;
			var halfHeight = rectangle.Height / 2;
			rectangle.X = center.X - halfWidth;
			rectangle.Y = center.Y - halfHeight;
		}

		public static bool BoundsContains(this RectF rect, PointF point) =>
			point.X >= 0 && point.X <= rect.Width &&
			point.Y >= 0 && point.Y <= rect.Height;

        public static bool Contains(this RectF rect, PointF[] points)
			=> points.Any(x => rect.Contains(x));
	}
}
