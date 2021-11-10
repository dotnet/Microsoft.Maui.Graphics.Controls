using Microsoft.Maui.Graphics;
using System;
using System.Diagnostics;

namespace GraphicsControls.Sample.Controls
{
    // This control is created for demonstration purposes.
    // It is not a production ready control. It would be necessary to implement the selected color as well as other options.
    public class ColorPicker : Microsoft.Maui.Graphics.Controls.GraphicsView
    {
        const float MinimumHeight = 60;

        Point _lastTouchPoint;

        public ColorPicker()
        {
            _lastTouchPoint = Point.Zero;
        }

        public override void Draw(ICanvas canvas, RectangleF dirtyRect)
        {
            float height = Math.Max(MinimumHeight, dirtyRect.Height);

            UpdateHeight(height);

            base.Draw(canvas, dirtyRect);

            // Draw gradient rainbow Color spectrum
            canvas.SaveState();

            LinearGradientPaint linearGradientPaint = new LinearGradientPaint
            {
                GradientStops = new GradientStop[]
                {
                    new GradientStop(0.0f, new Color(255, 0, 0)),
                    new GradientStop(0.2f, new Color(255, 255, 0)),
                    new GradientStop(0.4f, new Color(0, 255, 0)),
                    new GradientStop(0.6f, new Color(0, 255, 255)),
                    new GradientStop(0.8f, new Color(255, 0, 255)),
                    new GradientStop(1.0f, new Color(255, 0, 0))
                }
            };

            canvas.SetFillPaint(linearGradientPaint, dirtyRect);
                      
            canvas.FillRectangle(dirtyRect);
            
            canvas.RestoreState();

            // Painting the Touch point
            canvas.SaveState();

            float size = Math.Min(dirtyRect.Width, height);
            float pointerDiameter = (float)(size * 0.25f);

            canvas.StrokeColor = Colors.White;
            canvas.StrokeSize = 5.0f;

            // Set initial Touch point position
            if (_lastTouchPoint == Point.Zero)
                _lastTouchPoint = new Point(dirtyRect.Width / 2 - pointerDiameter / 2, height / 2 - pointerDiameter / 2);
            
            canvas.DrawEllipse((float)_lastTouchPoint.X, (float)_lastTouchPoint.Y, pointerDiameter, pointerDiameter);

            canvas.RestoreState();
        }

        public override void OnTouchDown(Point point)
        {
            Debug.WriteLine($"Touch Down {point}");

            UpdateTouchPoint(point);
        }

        public override void OnTouchMove(Point point)
        {
            Debug.WriteLine($"Touch Move {point}");

            UpdateTouchPoint(point);
        }

        void UpdateHeight(double height) => HeightRequest = height;

        void UpdateTouchPoint(Point point)
        {
            _lastTouchPoint = point;

            // Update the Canvas 
            Invalidate();
        }
    }
}