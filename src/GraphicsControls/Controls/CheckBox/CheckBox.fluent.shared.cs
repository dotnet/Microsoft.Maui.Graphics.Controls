using System.Graphics;
using GraphicsControls.Extensions;

namespace GraphicsControls
{
    public partial class CheckBox
    {
        void DrawFluentCheckBoxBackground(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            float size = 20f;

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            if (IsChecked)
            {
                canvas.FillColor = Color.ToGraphicsColor(Fluent.Color.Primary.ThemePrimary);
                canvas.FillRoundedRectangle(x, y, size, size, 2);
            }
            else
            {
                var strokeWidth = 2;

                canvas.StrokeSize = strokeWidth;
                canvas.StrokeColor = Color.ToGraphicsColor(Fluent.Color.Primary.ThemePrimary);
                canvas.DrawRoundedRectangle(x + strokeWidth / 2, y + strokeWidth / 2, size - strokeWidth, size - strokeWidth, 2);
            }

            canvas.RestoreState();
        }

        void DrawFluentCheckBoxMark(ICanvas canvas, RectangleF dirtyRect)
        {
            if (IsChecked)
            {
                canvas.SaveState();

                canvas.Translate(3, 5);

                var vBuilder = new PathBuilder();
                var path =
                    vBuilder.BuildPath(
                        "M13.3516 1.35156L5 9.71094L0.648438 5.35156L1.35156 4.64844L5 8.28906L12.6484 0.648438L13.3516 1.35156Z");

                canvas.StrokeColor = Colors.White;
                canvas.DrawPath(path);

                canvas.RestoreState();
            }
        }

        void DrawFluentCheckBoxText(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            canvas.FontColor = Colors.Black;
            canvas.FontSize = 14f;

            float size = 20f;
            float margin = 8f;

            var height = dirtyRect.Height;
            var width = dirtyRect.Width;

            canvas.DrawString(Text, size + margin, 0, width - (size + margin), height, HorizontalAlignment.Left, VerticalAlignment.Center);

            canvas.RestoreState();
        }
    }
}