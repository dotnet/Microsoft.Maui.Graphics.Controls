using System.Graphics;
using GraphicsControls.Extensions;
using Xamarin.Forms;
using GColor = System.Graphics.Color;

namespace GraphicsControls
{ 
    public partial class Button
    {
        const float MaterialBackgroundHeight = 36f;
        const float MaterialShadowOffset = 3f;

        void DrawMaterialButtonBackground(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            canvas.FillColor = BackgroundColor.ToGraphicsColor(Material.Color.Blue);

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var width = dirtyRect.Width - MaterialShadowOffset;
            canvas.SetShadow(new SizeF(0, 1), 3, new GColor(Application.Current?.RequestedTheme == OSAppTheme.Light ? Material.Color.Gray2 : Material.Color.Gray1));

            canvas.FillRoundedRectangle(x, y, width, MaterialBackgroundHeight, (float)CornerRadius);

            canvas.RestoreState();

            _backgroundRect = new RectangleF(x, y, width, MaterialBackgroundHeight);
        }

        void DrawMaterialButtonText(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            canvas.FontName = "Roboto";
            canvas.FontColor = TextColor.ToGraphicsColor(Material.Color.White);
            canvas.FontSize = Material.Font.Button;

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var width = dirtyRect.Width - MaterialShadowOffset;

            canvas.DrawString(Text.ToUpper(), x, y, width, MaterialBackgroundHeight, HorizontalAlignment.Center, VerticalAlignment.Center);

            canvas.RestoreState();
        }
    }
}