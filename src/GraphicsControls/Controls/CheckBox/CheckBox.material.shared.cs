using System.Graphics;
using GraphicsControls.Extensions;

namespace GraphicsControls
{
    public partial class CheckBox
    {
        void DrawMaterialCheckBoxBackground(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            float size = 18f;

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            if (IsChecked)
            {
                canvas.FillColor = Color.ToGraphicsColor(Material.Color.Blue);
                canvas.FillRoundedRectangle(x, y, size, size, 2);
            }
            else
            {
                var strokeWidth = 2;

                canvas.StrokeSize = strokeWidth;
                canvas.StrokeColor = Color.ToGraphicsColor(Material.Color.Gray1);
                canvas.DrawRoundedRectangle(x + strokeWidth / 2, y + strokeWidth / 2, size - strokeWidth, size - strokeWidth, 2);
            }

            canvas.RestoreState();
        }

        void DrawMaterialCheckBoxMark(ICanvas canvas, RectangleF dirtyRect)
        {
            if (IsChecked)
            {
                canvas.SaveState();

                canvas.Translate(2, 4);

                var vBuilder = new PathBuilder();
                var path =
                    vBuilder.BuildPath(
                        "M13.3516 1.35156L5 9.71094L0.648438 5.35156L1.35156 4.64844L5 8.28906L12.6484 0.648438L13.3516 1.35156Z");

                canvas.StrokeColor = Colors.White;
                canvas.DrawPath(path);

                canvas.RestoreState();
            }
        }

        void DrawMaterialCheckBoxText(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            canvas.FontColor = TextColor.ToGraphicsColor(Cupertino.Color.Label.Light.Primary, Cupertino.Color.Label.Dark.Primary);
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