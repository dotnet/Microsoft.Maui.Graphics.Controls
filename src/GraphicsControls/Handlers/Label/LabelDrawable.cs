namespace Microsoft.Maui.Graphics.Controls
{
    public class LabelDrawable : ViewDrawable<ILabel>, ILabelDrawable
    {
        public void DrawBackground(ICanvas canvas, RectF dirtyRect, ILabel label)
        {
            if (label.Background is null)
                return;

            canvas.SaveState();

            canvas.SetFillPaint(label.Background, dirtyRect);

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var height = dirtyRect.Height;
            var width = dirtyRect.Width;

            canvas.FillRectangle(x, y, width, height);

            canvas.RestoreState();
        }

        public void DrawText(ICanvas canvas, RectF dirtyRect, ILabel label)
        {
            canvas.SaveState();

            UpdateTextColor(canvas, dirtyRect, label);
            UpdateFont(canvas, dirtyRect, label);
            UpdateText(canvas, dirtyRect, label);

            canvas.RestoreState();
        }

        void UpdateTextColor(ICanvas canvas, RectF dirtyRect, ILabel label)
        {
            var textColor = label.TextColor;
            canvas.FontColor = textColor;
        }

        void UpdateFont(ICanvas canvas, RectF dirtyRect, ILabel label)
        {
            if (label.Font.Weight is FontWeight.Bold)
                canvas.Font = Font.DefaultBold;

            var fontSize = (float)label.Font.Size;
            canvas.FontSize = fontSize;
        }

        void UpdateText(ICanvas canvas, RectF dirtyRect, ILabel label)
        {
            var margin = label.Margin;
            var x = (float)(dirtyRect.X + margin.Left);
            var y = (float)(dirtyRect.Y + margin.Top);

            var height = dirtyRect.Height;
            var width = dirtyRect.Width;

            string text = label.Text;

            // Update CharacterSpacing
            double characterSpacing = label.CharacterSpacing;
            text = text.WithCharacterSpacing(characterSpacing);

            canvas.DrawString(
                text,
                x, y, width, height,
                label.HorizontalTextAlignment.ToHorizontalAlignment(),
                label.VerticalTextAlignment.ToVerticalAlignment(),
                TextFlow.ClipBounds);
        }
    }
}