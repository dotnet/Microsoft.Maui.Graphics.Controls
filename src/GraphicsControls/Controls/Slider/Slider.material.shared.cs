using System.Graphics;
using Xamarin.Forms;
using GColor = System.Graphics.Color;

namespace GraphicsControls
{
    public partial class Slider
    {
        const float NormalMaterialThumbSize = 12f;
        const float SelectedMaterialThumbSize = 18f;

        float _materialThumbSize = NormalMaterialThumbSize;

        float MaterialFloatThumb
        {
            get { return _materialThumbSize; }
            set
            {
                _materialThumbSize = value;
                InvalidateDraw();
            }
        }

        void DrawMaterialSliderTrackBackground(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            canvas.FillColor = new GColor(Fluent.Color.Primary.ThemeLight);

            var x = dirtyRect.X;

            var width = dirtyRect.Width;
            var height = 2;

            var y = (float)((HeightRequest - height) / 2);

            canvas.FillRoundedRectangle(x, y, width, height, 0);

            canvas.RestoreState();

            _trackRect = new RectangleF(x, y, width, height);
        }

        protected virtual void DrawMaterialSliderTrackProgress(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            canvas.FillColor = new GColor(Material.Color.Blue);

            var x = dirtyRect.X;

            var width = (float)(dirtyRect.Width * Value);
            var height = 2;

            var y = (float)((HeightRequest - height) / 2);

            canvas.FillRoundedRectangle(x, y, width, height, 0);

            canvas.RestoreState();
        }

        protected virtual void DrawMaterialSliderThumb(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            var x = (float)((dirtyRect.Width * Value) - (MaterialFloatThumb / 2));

            if (x <= 0)
                x = 0;

            if (x >= dirtyRect.Width - MaterialFloatThumb)
                x = dirtyRect.Width - MaterialFloatThumb;

            var y = (float)((HeightRequest - MaterialFloatThumb) / 2);

            canvas.FillColor = new GColor(Material.Color.Blue);

            canvas.FillOval(x, y, MaterialFloatThumb, MaterialFloatThumb);

            canvas.RestoreState();

            _thumbRect = new RectangleF(x, y, MaterialFloatThumb, MaterialFloatThumb);
        }

        void AnimateMaterialThumbSize(bool increase)
        {
            float start = increase ? NormalMaterialThumbSize : SelectedMaterialThumbSize;
            float end = increase ? SelectedMaterialThumbSize : NormalMaterialThumbSize;

            var thumbSizeAnimation = new Animation(v => MaterialFloatThumb = (int)v, start, end, easing: Easing.SinInOut);
            thumbSizeAnimation.Commit(this, "ThumbSizeAnimation", length: 50, finished: (l, c) => thumbSizeAnimation = null);
        }
    }
}