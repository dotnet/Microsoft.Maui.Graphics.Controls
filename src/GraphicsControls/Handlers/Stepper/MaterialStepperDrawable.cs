namespace Microsoft.Maui.Graphics.Controls
{
    public class MaterialStepperDrawable : ViewDrawable<IStepper>, IStepperDrawable
    {
        const string MaterialStepperMinusIcon = "M0.990234 1.96143H13.0098C13.5161 1.96143 13.9478 1.53809 13.9478 1.01514C13.9478 0.500488 13.5161 0.0688477 13.0098 0.0688477H0.990234C0.500488 0.0688477 0.0522461 0.500488 0.0522461 1.01514C0.0522461 1.53809 0.500488 1.96143 0.990234 1.96143Z";
        const string MaterialStepperPlusIcon = "M0.990234 7.95312H6.05371V13.0166C6.05371 13.5312 6.47705 13.9629 7 13.9629C7.52295 13.9629 7.94629 13.5312 7.94629 13.0166V7.95312H13.0098C13.5244 7.95312 13.9561 7.52979 13.9561 7.00684C13.9561 6.48389 13.5244 6.06055 13.0098 6.06055H7.94629V0.99707C7.94629 0.482422 7.52295 0.0507812 7 0.0507812C6.47705 0.0507812 6.05371 0.482422 6.05371 0.99707V6.06055H0.990234C0.475586 6.06055 0.0439453 6.48389 0.0439453 7.00684C0.0439453 7.52979 0.475586 7.95312 0.990234 7.95312Z";

        const float MaterialStepperHeight = 40.0f;
        const float MaterialStepperWidth = 110.0f;
        const float MaterialButtonMargin = 6.0f;
        const float MaterialButtonCornerRadius = 6.0f;

        public RectangleF MinusRectangle { get; set; }

        public RectangleF PlusRectangle { get; set; }

        public void DrawBackground(ICanvas canvas, RectangleF dirtyRect, IStepper stepper)
        {
            canvas.SaveState();

            canvas.StrokeSize = 1;

            if (stepper.Background != null)
                canvas.SetFillPaint(stepper.Background, dirtyRect);
            else
                canvas.FillColor = stepper.IsEnabled ? Material.Color.White.ToColor() : Material.Color.Gray6.ToColor();

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var height = MaterialStepperHeight;
            var width = MaterialStepperWidth / 2;

            canvas.FillRoundedRectangle(x, y, width, height, MaterialButtonCornerRadius);

            x = MaterialStepperWidth / 2 + MaterialButtonMargin;
            y = dirtyRect.Y;

            height = MaterialStepperHeight;
            width = MaterialStepperWidth / 2;

            canvas.FillRoundedRectangle(x, y, width, height, MaterialButtonCornerRadius);     

            canvas.RestoreState();
        }

        public void DrawMinus(ICanvas canvas, RectangleF dirtyRect, IStepper stepper)
        {
            canvas.SaveState();

            canvas.StrokeSize = 1;
            canvas.StrokeColor = Material.Color.Gray6.ToColor();

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var height = MaterialStepperHeight;
            var width = MaterialStepperWidth / 2;

            canvas.DrawRoundedRectangle(x, y, width, height, MaterialButtonCornerRadius);

            canvas.Translate(20, 20);

            var vBuilder = new PathBuilder();
            var path = vBuilder.BuildPath(MaterialStepperMinusIcon);

            if (VirtualView.IsEnabled)
                canvas.FillColor = Material.Color.Black.ToColor();
            else
                canvas.FillColor = Material.Color.Gray3.ToColor();

            canvas.FillPath(path);

            canvas.RestoreState();

            MinusRectangle = new RectangleF(x, y, width, height);
        }

        public void DrawPlus(ICanvas canvas, RectangleF dirtyRect, IStepper stepper)
        {
            canvas.SaveState();

            canvas.StrokeSize = 1;
            canvas.StrokeColor = Material.Color.Gray6.ToColor();

            var x = MaterialStepperWidth / 2 + MaterialButtonMargin;
            var y = dirtyRect.Y;

            var height = MaterialStepperHeight;
            var width = MaterialStepperWidth / 2;

            canvas.DrawRoundedRectangle(x, y, width, height, MaterialButtonCornerRadius);

            canvas.Translate(80, 14);

            var vBuilder = new PathBuilder();
            var path = vBuilder.BuildPath(MaterialStepperPlusIcon);

            if (stepper.IsEnabled)
                canvas.FillColor = Material.Color.Black.ToColor();
            else
                canvas.FillColor = Material.Color.Gray3.ToColor();

            canvas.FillPath(path);

            canvas.RestoreState();

            PlusRectangle = new RectangleF(x, y, width, height);
        }

        public void DrawSeparator(ICanvas canvas, RectangleF dirtyRect, IStepper stepper)
        {
        
        }

        public void DrawText(ICanvas canvas, RectangleF dirtyRect, IStepper stepper)
        {
        
        }
    }
}