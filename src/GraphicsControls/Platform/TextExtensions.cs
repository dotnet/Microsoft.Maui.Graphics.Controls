using System.Text;

namespace Microsoft.Maui.Graphics.Controls
{
    public static class TextExtensions
    {
        public static string WithCharacterSpacing(this string input, double characterSpacing)
        {
            int spaces = (int)characterSpacing;

            if (input.Length <= 1)
                return input;

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                sb.Append(input[i]);
                if (i != input.Length - 1)
                    sb.Append(' ', spaces);
            }

            return sb.ToString();
        }

        public static HorizontalAlignment ToHorizontalAlignment(this TextAlignment textAlignment)
        {
            switch (textAlignment)
            {
                case TextAlignment.Start:
                    return HorizontalAlignment.Left;
                case TextAlignment.Center:
                    return HorizontalAlignment.Center;
                case TextAlignment.End:
                    return HorizontalAlignment.Right;
                default:
                    return HorizontalAlignment.Left;
            }
        }

        public static VerticalAlignment ToVerticalAlignment(this TextAlignment textAlignment)
        {
            switch (textAlignment)
            {
                case TextAlignment.Start:
                    return VerticalAlignment.Top;
                case TextAlignment.Center:
                    return VerticalAlignment.Center;
                case TextAlignment.End:
                    return VerticalAlignment.Bottom;
                default:
                    return VerticalAlignment.Top;
            }
        }
    }
}