namespace Microsoft.Maui.Graphics.Controls
{
    public class FluentProgressBarDrawable : ViewDrawable<IProgress>, IProgressBarDrawable
    {
        const float FluentTrackHeight = 2.0f;

        public void DrawProgress(ICanvas canvas, RectangleF dirtyRect, IProgress view)
        {
            canvas.SaveState();

            canvas.FillColor = Fluent.Color.Primary.ThemePrimary.ToColor();

            var x = dirtyRect.X;
            var y = (float)((dirtyRect.Height - FluentTrackHeight) / 2);

            var width = dirtyRect.Width;

            canvas.FillRoundedRectangle(x, y, (float)(width * VirtualView.Progress), FluentTrackHeight, 0);

            canvas.RestoreState();
        }

        public void DrawTrack(ICanvas canvas, RectangleF dirtyRect, IProgress view)
        {
            canvas.SaveState();

            canvas.FillColor = VirtualView.BackgroundColor.WithDefault(VirtualView.IsEnabled ? Fluent.Color.Background.NeutralTertiaryAlt : Fluent.Color.Background.NeutralLight);

            var x = dirtyRect.X;
            var y = (float)((dirtyRect.Height - FluentTrackHeight) / 2);

            var width = dirtyRect.Width;

            canvas.FillRoundedRectangle(x, y, width, FluentTrackHeight, 0);

            canvas.RestoreState();
        }

        public override Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
            new Size(widthConstraint, 12f);
    }
}