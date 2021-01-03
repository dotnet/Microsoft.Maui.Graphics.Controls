using System.Graphics;
using GraphicsControls.Extensions;
using GraphicsControls.Helpers;
using Xamarin.Forms;
using GColor = System.Graphics.Color;

namespace GraphicsControls
{
    public partial class Stepper
    {
        const float FluentStepperHeight = 32.0f;
        const float FluentChevronSize = 14.0f;

        void DrawFluentStepperBackground(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            if (IsEnabled)
                canvas.FillColor = BackgroundColor.ToGraphicsColor(Fluent.Color.Foreground.White, Fluent.Color.Foreground.NeutralPrimaryAlt);
            else
                canvas.FillColor = ColorHelper.GetGraphicsColor(Fluent.Color.Background.NeutralLighter, Fluent.Color.Background.NeutralDark);

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var height = FluentStepperHeight;
            var width = dirtyRect.Width;

            canvas.FillRoundedRectangle(x, y, width, height, 2);

            canvas.RestoreState();
        }

        void DrawFluentStepperBorder(ICanvas canvas, RectangleF dirtyRect)
        {
            if (IsEnabled)
            {
                canvas.SaveState();

                var strokeWidth = 1.0f;

                canvas.StrokeColor = ColorHelper.GetGraphicsColor(Fluent.Color.Foreground.NeutralSecondary, Fluent.Color.Foreground.White);
                canvas.StrokeSize = strokeWidth;

                var x = dirtyRect.X;
                var y = dirtyRect.Y;

                var height = FluentStepperHeight;
                var width = dirtyRect.Width;

                canvas.DrawRoundedRectangle(x + strokeWidth / 2, y + strokeWidth / 2, width - strokeWidth, height - strokeWidth, 2);

                canvas.RestoreState();
            }
        }

        void DrawFluentStepperUp(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            var margin = 6;

            var tX = FlowDirection == FlowDirection.RightToLeft ? margin : dirtyRect.Width - FluentChevronSize;

            canvas.Translate(tX, margin);

            var vBuilder = new PathBuilder();
            var path =
                vBuilder.BuildPath(
                    "M4 0.722656L7.97266 4.69531L7.02734 5.63672L4 2.60938L0.972656 5.63672L0.0273438 4.69531L4 0.722656Z");

            if (IsEnabled)
                canvas.FillColor = ColorHelper.GetGraphicsColor(Fluent.Color.Foreground.Black, Fluent.Color.Foreground.White);
            else
                canvas.FillColor = new GColor(Fluent.Color.Foreground.NeutralTertiary);

            canvas.FillPath(path);

            canvas.RestoreState();

            var touchMargin = 12;
            _plusRect = new RectangleF(tX - touchMargin, 0, FluentStepperHeight / 2 + touchMargin, FluentStepperHeight / 2);
        }

        void DrawFluentStepperDown(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            var margin = 6;

            var tX = FlowDirection == FlowDirection.RightToLeft ? margin : dirtyRect.Width - FluentChevronSize;

            canvas.Translate(tX, FluentStepperHeight - (margin * 2));

            var vBuilder = new PathBuilder();
            var path =
                vBuilder.BuildPath(
                    "M7.02734 0.363281L7.97266 1.30469L4 5.27734L0.0273438 1.30469L0.972656 0.363281L4 3.39062L7.02734 0.363281Z");

            if (IsEnabled)
                canvas.FillColor = ColorHelper.GetGraphicsColor(Fluent.Color.Foreground.Black, Fluent.Color.Foreground.White);
            else
                canvas.FillColor = new GColor(Fluent.Color.Foreground.NeutralTertiary);

            canvas.FillPath(path);

            canvas.RestoreState();

            var touchMargin = 12;
            _minusRect = new RectangleF(tX - touchMargin, FluentStepperHeight / 2, FluentStepperHeight / 2 + touchMargin, FluentStepperHeight / 2);
        }

        void DrawFluentStepperText(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            if (IsEnabled)
                canvas.FontColor = ColorHelper.GetGraphicsColor(Fluent.Color.Foreground.Black, Fluent.Color.Foreground.White);
            else
                canvas.FontColor = new GColor(Fluent.Color.Foreground.NeutralTertiary);

            canvas.FontSize = 14f;

            float margin = 8f;

            var height = FluentStepperHeight;
            var width = dirtyRect.Width;

            var x = FlowDirection == FlowDirection.RightToLeft ? 0 : margin;
            HorizontalAlignment horizontalAlignment = FlowDirection == FlowDirection.RightToLeft ? HorizontalAlignment.Right : HorizontalAlignment.Left;

            canvas.DrawString(Value.ToString(), x, 0, width - margin, height, horizontalAlignment, VerticalAlignment.Center);

            canvas.RestoreState();
        }
    }
}