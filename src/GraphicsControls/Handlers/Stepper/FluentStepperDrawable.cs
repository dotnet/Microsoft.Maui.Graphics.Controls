namespace Microsoft.Maui.Graphics.Controls
{
    public class FluentStepperDrawable : ViewDrawable<IStepper>, IStepperDrawable
    {
        const string FluentStepperUpIcon = "M4 0.722656L7.97266 4.69531L7.02734 5.63672L4 2.60938L0.972656 5.63672L0.0273438 4.69531L4 0.722656Z";
        const string FluentStepperDownIcon = "M7.02734 0.363281L7.97266 1.30469L4 5.27734L0.0273438 1.30469L0.972656 0.363281L4 3.39062L7.02734 0.363281Z";

        const float FluentStepperHeight = 32.0f;
        const float FluentChevronSize = 14.0f;

        public RectangleF MinusRectangle { get; set; }

        public RectangleF PlusRectangle { get; set; }

        public void DrawBackground(ICanvas canvas, RectangleF dirtyRect, IStepper stepper)
        {
            canvas.SaveState();

            if (stepper.Background != null)
                canvas.SetFillPaint(stepper.Background, dirtyRect);
            else
                canvas.FillColor = stepper.IsEnabled ? Fluent.Color.Foreground.White.ToColor() : Fluent.Color.Background.NeutralLighter.ToColor();

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var height = FluentStepperHeight;
            var width = dirtyRect.Width;

            canvas.FillRoundedRectangle(x, y, width, height, 2);

            canvas.RestoreState();
        }

        public void DrawMinus(ICanvas canvas, RectangleF dirtyRect, IStepper stepper)
        {
            canvas.SaveState();

            var margin = 6;

            var tX = dirtyRect.Width - FluentChevronSize;

            canvas.Translate(tX, FluentStepperHeight - (margin * 2));

            var vBuilder = new PathBuilder();
            var path = vBuilder.BuildPath(FluentStepperDownIcon);

            if (stepper.IsEnabled)
                canvas.FillColor = Fluent.Color.Foreground.Black.ToColor();
            else
                canvas.FillColor = Fluent.Color.Foreground.NeutralTertiary.ToColor();

            canvas.FillPath(path);

            canvas.RestoreState();

            var touchMargin = 12;
            MinusRectangle = new RectangleF(tX - touchMargin, FluentStepperHeight / 2, FluentStepperHeight / 2 + touchMargin, FluentStepperHeight / 2);
        }

        public void DrawPlus(ICanvas canvas, RectangleF dirtyRect, IStepper stepper)
        {
            canvas.SaveState();

            var margin = 6;

            var tX = dirtyRect.Width - FluentChevronSize;

            canvas.Translate(tX, margin);

            var vBuilder = new PathBuilder();
            var path = vBuilder.BuildPath(FluentStepperUpIcon);

            if (stepper.IsEnabled)
                canvas.FillColor = Fluent.Color.Foreground.Black.ToColor();
            else
                canvas.FillColor = Fluent.Color.Foreground.NeutralTertiary.ToColor();

            canvas.FillPath(path);

            canvas.RestoreState();

            var touchMargin = 12;
            PlusRectangle = new RectangleF(tX - touchMargin, 0, FluentStepperHeight / 2 + touchMargin, FluentStepperHeight / 2);
        }

        public void DrawSeparator(ICanvas canvas, RectangleF dirtyRect, IStepper stepper)
        {
            if (stepper.IsEnabled)
            {
                canvas.SaveState();

                var strokeWidth = 1.0f;

                canvas.StrokeColor = Fluent.Color.Foreground.NeutralSecondary.ToColor();
                canvas.StrokeSize = strokeWidth;

                var x = dirtyRect.X;
                var y = dirtyRect.Y;

                var height = FluentStepperHeight;
                var width = dirtyRect.Width;

                canvas.DrawRoundedRectangle(x + strokeWidth / 2, y + strokeWidth / 2, width - strokeWidth, height - strokeWidth, 2);

                canvas.RestoreState();
            }
        }

        public void DrawText(ICanvas canvas, RectangleF dirtyRect, IStepper stepper)
        {
            canvas.SaveState();

            if (stepper.IsEnabled)
                canvas.FontColor = Fluent.Color.Foreground.Black.ToColor();
            else
                canvas.FontColor = Fluent.Color.Foreground.NeutralTertiary.ToColor();

            canvas.FontSize = 14f;

            float margin = 8f;

            var height = FluentStepperHeight;
            var width = dirtyRect.Width;

            canvas.DrawString(stepper.Value.ToString(), margin, 0, width - margin, height, HorizontalAlignment.Left, VerticalAlignment.Center);

            canvas.RestoreState();
        }
    }
}