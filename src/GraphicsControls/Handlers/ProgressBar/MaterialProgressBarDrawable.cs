using Microsoft.Maui.Controls;
using Microsoft.Maui.ApplicationModel;

namespace Microsoft.Maui.Graphics.Controls
{
    public class MaterialProgressBarDrawable : ViewDrawable<IProgress>, IProgressBarDrawable
    {
        const float MaterialTrackHeight = 4.0f;

        public void DrawProgress(ICanvas canvas, RectF dirtyRect, IProgress progressBar)
        {
            canvas.SaveState();

            if (progressBar.IsEnabled)
                canvas.FillColor = progressBar.ProgressColor.WithDefault(Material.Color.Blue);
            else
            {
                if (Application.Current?.RequestedTheme == AppTheme.Light)
                    canvas.FillColor = Material.Color.Light.Gray3.ToColor();
                else
                    canvas.FillColor = Material.Color.Dark.Gray6.ToColor();
            }

            var x = dirtyRect.X;
            var y = (float)((dirtyRect.Height - MaterialTrackHeight) / 2);

            var width = dirtyRect.Width;

            canvas.FillRectangle(x, y, (float)(width * progressBar.Progress), MaterialTrackHeight);

            canvas.RestoreState();
        }

        public void DrawTrack(ICanvas canvas, RectF dirtyRect, IProgress progressBar)
        {
            canvas.SaveState();

            if (progressBar.Background != null)
                canvas.SetFillPaint(progressBar.Background, dirtyRect);
            else
            {
                if (Application.Current?.RequestedTheme == AppTheme.Light)
                    canvas.FillColor = Material.Color.Gray4.ToColor();
                else
                    canvas.FillColor = Material.Color.Gray2.ToColor();
            }

            var x = dirtyRect.X;
            var y = (float)((dirtyRect.Height - MaterialTrackHeight) / 2);

            var width = dirtyRect.Width;

            canvas.FillRectangle(x, y, width, MaterialTrackHeight);

            canvas.RestoreState();
        }

        public override Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
            new Size(widthConstraint, 12f);
    }
}