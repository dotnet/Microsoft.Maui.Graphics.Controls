namespace Microsoft.Maui.Graphics.Controls
{
    public class GnomeStepperDrawable : ViewDrawable<IStepper>, IStepperDrawable
    {
        const string GnomeStepperMinusIcon = "M9.99997 0.967773H0.000366211V2.96796H9.99997V0.967773Z";
        const string GnomeStepperPlusIcon = "M4 0.967896V4.9679H0V6.9679H4V10.9679H6V6.9679H10V4.9679H6V0.967896H4Z";

        const float GnomeStepperHeight = 29.0f;
        const float GnomeStepperWidth = 58.98f;

        public RectangleF MinusRectangle { get; set; }

        public RectangleF PlusRectangle { get; set; }

        public void DrawBackground(ICanvas canvas, RectangleF dirtyRect, IStepper view)
        {
            canvas.SaveState();

            var strokeWidth = 1;
            canvas.StrokeSize = strokeWidth;
            canvas.StrokeColor = Color.FromHex("#919191");

            canvas.FillColor = VirtualView.BackgroundColor.WithDefault(VirtualView.IsEnabled ? "#EAEAEA" : "#F4F4F2");

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var height = GnomeStepperHeight;
            var width = GnomeStepperWidth;

            float margin = strokeWidth * 2;

            canvas.FillRoundedRectangle(x + strokeWidth, y + strokeWidth, width - margin, height - margin, 2);
            canvas.DrawRoundedRectangle(x + strokeWidth, y + strokeWidth, width - margin, height - margin, 2);

            canvas.RestoreState();
        }

        public void DrawMinus(ICanvas canvas, RectangleF dirtyRect, IStepper view)
        {
            canvas.SaveState();

            var tX = 38;
            var tY = 13;

            canvas.Translate(tX, tY);

            var vBuilder = new PathBuilder();
            var path = vBuilder.BuildPath(GnomeStepperMinusIcon);

            if (VirtualView.IsEnabled)
                canvas.FillColor = Color.FromHex("#2E3436");
            else
                canvas.FillColor = Color.FromHex("#909494");

            canvas.FillPath(path);

            canvas.RestoreState();

            MinusRectangle = new RectangleF(tX, tY, GnomeStepperHeight / 2, GnomeStepperHeight / 2);
        }

        public void DrawPlus(ICanvas canvas, RectangleF dirtyRect, IStepper view)
        {
            canvas.SaveState();

            var tX = 10;
            var tY = 9;

            canvas.Translate(tX, tY);

            var vBuilder = new PathBuilder();
            var path = vBuilder.BuildPath(GnomeStepperPlusIcon);

            if (VirtualView.IsEnabled)
                canvas.FillColor = Color.FromHex("#2E3436");
            else
                canvas.FillColor = Color.FromHex("#909494");

            canvas.FillPath(path);

            canvas.RestoreState();

            PlusRectangle = new RectangleF(tX, tY, GnomeStepperHeight / 2, GnomeStepperHeight / 2);
        }

        public void DrawSeparator(ICanvas canvas, RectangleF dirtyRect, IStepper view)
        {
            canvas.SaveState();

            var strokeWidth = 1;
            canvas.StrokeSize = strokeWidth;
            canvas.StrokeColor = Color.FromHex("#919191");

            var height = GnomeStepperHeight - (strokeWidth * 2);
            var width = 1;

            var x = (GnomeStepperWidth - width) / 2;
            var y = (GnomeStepperHeight - height) / 2;

            canvas.DrawLine(x, y, x, y + height);

            canvas.RestoreState();
        }

        public void DrawText(ICanvas canvas, RectangleF dirtyRect, IStepper view)
        {

        }
    }
}