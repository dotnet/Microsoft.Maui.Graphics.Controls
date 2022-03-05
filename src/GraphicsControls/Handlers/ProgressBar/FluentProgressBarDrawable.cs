using Microsoft.Maui.Controls;

namespace Microsoft.Maui.Graphics.Controls
{
    public class FluentProgressBarDrawable : ViewDrawable<IProgress>, IProgressBarDrawable
    {
        const float FluentTrackProgressHeight = 3.0f;
        const float FluentTrackHeight = 1.0f;

        public void DrawProgress(ICanvas canvas, RectF dirtyRect, IProgress progressBar)
        {
            canvas.SaveState();

            canvas.FillColor = progressBar.ProgressColor.WithDefault(Fluent.Color.Light.Accent.Primary, Fluent.Color.Dark.Accent.Primary);

            var x = dirtyRect.X;
            var y = (float)((dirtyRect.Height - FluentTrackProgressHeight) / 2);

            var width = dirtyRect.Width;

            canvas.FillRoundedRectangle(x, y, (float)(width * progressBar.Progress), FluentTrackProgressHeight, 6);

            canvas.RestoreState();
        }

        public void DrawTrack(ICanvas canvas, RectF dirtyRect, IProgress progressBar)
        {
            canvas.SaveState();

            if (progressBar.Background != null)
                canvas.SetFillPaint(progressBar.Background, dirtyRect);
            else
            {
                if (progressBar.IsEnabled)
                    canvas.FillColor = (Application.Current?.RequestedTheme == AppTheme.Light) ? Fluent.Color.Light.Control.Border.Default.ToColor() : Fluent.Color.Dark.Control.Border.Default.ToColor();
                else
                    canvas.FillColor = (Application.Current?.RequestedTheme == AppTheme.Light) ? Fluent.Color.Light.Control.Border.Disabled.ToColor() : Fluent.Color.Dark.Control.Border.Disabled.ToColor();
            }

            var x = dirtyRect.X;
            var y = (float)((dirtyRect.Height - FluentTrackHeight) / 2);

            var width = dirtyRect.Width;

            canvas.FillRoundedRectangle(x, y, width, FluentTrackHeight, 6);

            canvas.RestoreState();
        }

        public override Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
            new Size(widthConstraint, 12f);
    }
}