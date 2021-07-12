namespace Microsoft.Maui.Graphics.Controls
{
    public class FluentDatePickerDrawable : ViewDrawable<IDatePicker>, IDatePickerDrawable
    {
        const string FluentDatePickerIcon = "M9.7793 0.75C9.97461 0.75 10.1602 0.791016 10.3359 0.873047C10.5156 0.951172 10.6719 1.05859 10.8047 1.19531C10.9414 1.32813 11.0488 1.48437 11.127 1.66406C11.209 1.83984 11.25 2.02539 11.25 2.2207V9.7793C11.25 9.97461 11.209 10.1621 11.127 10.3418C11.0488 10.5176 10.9414 10.6738 10.8047 10.8105C10.6719 10.9434 10.5156 11.0508 10.3359 11.1328C10.1602 11.2109 9.97461 11.25 9.7793 11.25H2.2207C2.02539 11.25 1.83789 11.2109 1.6582 11.1328C1.48242 11.0508 1.32617 10.9434 1.18945 10.8105C1.05664 10.6738 0.949219 10.5176 0.867188 10.3418C0.789062 10.1621 0.75 9.97461 0.75 9.7793V2.2207C0.75 2.02539 0.789062 1.83984 0.867188 1.66406C0.949219 1.48437 1.05664 1.32813 1.18945 1.19531C1.32617 1.05859 1.48242 0.951172 1.6582 0.873047C1.83789 0.791016 2.02539 0.75 2.2207 0.75H9.7793ZM2.25 1.5C2.14453 1.5 2.04688 1.51953 1.95703 1.55859C1.86719 1.59766 1.78711 1.65234 1.7168 1.72266C1.65039 1.78906 1.59766 1.86719 1.55859 1.95703C1.51953 2.04687 1.5 2.14453 1.5 2.25V3H10.5V2.25C10.5 2.14844 10.4805 2.05273 10.4414 1.96289C10.4023 1.86914 10.3477 1.78906 10.2773 1.72266C10.2109 1.65234 10.1309 1.59766 10.0371 1.55859C9.94727 1.51953 9.85156 1.5 9.75 1.5H2.25ZM9.75 10.5C9.85547 10.5 9.95312 10.4805 10.043 10.4414C10.1328 10.4023 10.2109 10.3496 10.2773 10.2832C10.3477 10.2129 10.4023 10.1328 10.4414 10.043C10.4805 9.95312 10.5 9.85547 10.5 9.75V3.75H1.5V9.75C1.5 9.85547 1.51953 9.95508 1.55859 10.0488C1.59766 10.1387 1.65039 10.2168 1.7168 10.2832C1.7832 10.3496 1.86133 10.4023 1.95117 10.4414C2.04492 10.4805 2.14453 10.5 2.25 10.5H9.75ZM3 6C3 5.89453 3.01953 5.79688 3.05859 5.70703C3.09766 5.61719 3.15039 5.53906 3.2168 5.47266C3.28711 5.40234 3.36719 5.34766 3.45703 5.30859C3.55078 5.26953 3.65039 5.25 3.75586 5.25C3.86133 5.25 3.95898 5.26953 4.04883 5.30859C4.13867 5.34766 4.2168 5.40039 4.2832 5.4668C4.34961 5.5332 4.40234 5.61133 4.44141 5.70117C4.48047 5.79102 4.5 5.88867 4.5 5.99414C4.5 6.09961 4.48047 6.19922 4.44141 6.29297C4.40234 6.38281 4.34766 6.46289 4.27734 6.5332C4.21094 6.59961 4.13281 6.65234 4.04297 6.69141C3.95312 6.73047 3.85547 6.75 3.75 6.75C3.64453 6.75 3.54492 6.73047 3.45117 6.69141C3.36133 6.65234 3.2832 6.59961 3.2168 6.5332C3.15039 6.4668 3.09766 6.38867 3.05859 6.29883C3.01953 6.20508 3 6.10547 3 6ZM5.25 6C5.25 5.89453 5.26953 5.79688 5.30859 5.70703C5.34766 5.61719 5.40039 5.53906 5.4668 5.47266C5.53711 5.40234 5.61719 5.34766 5.70703 5.30859C5.80078 5.26953 5.90039 5.25 6.00586 5.25C6.11133 5.25 6.20898 5.26953 6.29883 5.30859C6.38867 5.34766 6.4668 5.40039 6.5332 5.4668C6.59961 5.5332 6.65234 5.61133 6.69141 5.70117C6.73047 5.79102 6.75 5.88867 6.75 5.99414C6.75 6.09961 6.73047 6.19922 6.69141 6.29297C6.65234 6.38281 6.59766 6.46289 6.52734 6.5332C6.46094 6.59961 6.38281 6.65234 6.29297 6.69141C6.20312 6.73047 6.10547 6.75 6 6.75C5.89453 6.75 5.79492 6.73047 5.70117 6.69141C5.61133 6.65234 5.5332 6.59961 5.4668 6.5332C5.40039 6.4668 5.34766 6.38867 5.30859 6.29883C5.26953 6.20508 5.25 6.10547 5.25 6ZM9 5.99414C9 6.09961 8.98047 6.19922 8.94141 6.29297C8.90234 6.38281 8.84766 6.46289 8.77734 6.5332C8.71094 6.59961 8.63281 6.65234 8.54297 6.69141C8.45312 6.73047 8.35547 6.75 8.25 6.75C8.14453 6.75 8.04492 6.73047 7.95117 6.69141C7.86133 6.65234 7.7832 6.59961 7.7168 6.5332C7.65039 6.4668 7.59766 6.38867 7.55859 6.29883C7.51953 6.20508 7.5 6.10547 7.5 6C7.5 5.89453 7.51953 5.79688 7.55859 5.70703C7.59766 5.61719 7.65039 5.53906 7.7168 5.47266C7.78711 5.40234 7.86719 5.34766 7.95703 5.30859C8.05078 5.26953 8.15039 5.25 8.25586 5.25C8.36133 5.25 8.45898 5.26953 8.54883 5.30859C8.63867 5.34766 8.7168 5.40039 8.7832 5.4668C8.84961 5.5332 8.90234 5.61133 8.94141 5.70117C8.98047 5.79102 9 5.88867 9 5.99414ZM4.5 8.25C4.5 8.35547 4.48047 8.45312 4.44141 8.54297C4.40234 8.63281 4.34766 8.71289 4.27734 8.7832C4.21094 8.84961 4.13086 8.90234 4.03711 8.94141C3.94727 8.98047 3.84961 9 3.74414 9C3.63867 9 3.54102 8.98047 3.45117 8.94141C3.36133 8.90234 3.2832 8.84961 3.2168 8.7832C3.15039 8.7168 3.09766 8.63867 3.05859 8.54883C3.01953 8.45898 3 8.36133 3 8.25586C3 8.15039 3.01953 8.05273 3.05859 7.96289C3.09766 7.86914 3.15039 7.78906 3.2168 7.72266C3.28711 7.65234 3.36719 7.59766 3.45703 7.55859C3.54688 7.51953 3.64453 7.5 3.75 7.5C3.85547 7.5 3.95312 7.51953 4.04297 7.55859C4.13672 7.59766 4.2168 7.65039 4.2832 7.7168C4.34961 7.7832 4.40234 7.86328 4.44141 7.95703C4.48047 8.04688 4.5 8.14453 4.5 8.25ZM6.75 8.25C6.75 8.35547 6.73047 8.45312 6.69141 8.54297C6.65234 8.63281 6.59766 8.71289 6.52734 8.7832C6.46094 8.84961 6.38086 8.90234 6.28711 8.94141C6.19727 8.98047 6.09961 9 5.99414 9C5.88867 9 5.79102 8.98047 5.70117 8.94141C5.61133 8.90234 5.5332 8.84961 5.4668 8.7832C5.40039 8.7168 5.34766 8.63867 5.30859 8.54883C5.26953 8.45898 5.25 8.36133 5.25 8.25586C5.25 8.15039 5.26953 8.05273 5.30859 7.96289C5.34766 7.86914 5.40039 7.78906 5.4668 7.72266C5.53711 7.65234 5.61719 7.59766 5.70703 7.55859C5.79688 7.51953 5.89453 7.5 6 7.5C6.10547 7.5 6.20312 7.51953 6.29297 7.55859C6.38672 7.59766 6.4668 7.65039 6.5332 7.7168C6.59961 7.7832 6.65234 7.86328 6.69141 7.95703C6.73047 8.04688 6.75 8.14453 6.75 8.25Z";
        const float FluentDatePickerHeight = 32.0f;

        public void DrawBackground(ICanvas canvas, RectangleF dirtyRect, IDatePicker datePicker)
        {
            canvas.SaveState();

            if (datePicker.Background != null)
                canvas.SetFillPaint(datePicker.Background, dirtyRect);
            else
                canvas.FillColor = datePicker.IsEnabled ? Fluent.Color.Foreground.White.ToColor() : Fluent.Color.Background.NeutralLighter.ToColor();

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var width = dirtyRect.Width;
            var height = dirtyRect.Height;

            canvas.FillRoundedRectangle(x, y, width, height, 4);

            canvas.RestoreState();
        }

        public void DrawBorder(ICanvas canvas, RectangleF dirtyRect, IDatePicker datePicker)
        {
            if (datePicker.IsEnabled)
            {
                canvas.SaveState();

                var strokeWidth = 1.0f;
                      
                canvas.StrokeColor = Fluent.Color.Foreground.NeutralSecondary.ToColor();
                canvas.StrokeSize = strokeWidth;

                var x = dirtyRect.X;
                var y = dirtyRect.Y;

                var width = dirtyRect.Width;
                var height = dirtyRect.Height;

                canvas.DrawRoundedRectangle(x + strokeWidth / 2, y + strokeWidth / 2, width - strokeWidth, height - strokeWidth, 2);

                canvas.RestoreState();
            }
        }

        public void DrawDate(ICanvas canvas, RectangleF dirtyRect, IDatePicker datePicker)
        {
            canvas.SaveState();

            if (datePicker.IsEnabled)
                canvas.FontColor = datePicker.TextColor.WithDefault(Fluent.Color.Foreground.Black);
            else
                canvas.FontColor = datePicker.TextColor.WithDefault(Fluent.Color.Foreground.NeutralTertiary);

            canvas.FontSize = 14f;

            float margin = 8f;

            var x = dirtyRect.X + margin;

            var height = FluentDatePickerHeight;
            var width = dirtyRect.Width;

            var date = datePicker.Date;

            canvas.DrawString(date.ToShortDateString(), x, 0, width - margin, height, HorizontalAlignment.Left, VerticalAlignment.Center);

            canvas.RestoreState();
        }

        public void DrawIndicator(ICanvas canvas, RectangleF dirtyRect, IDatePicker datePicker)
        {
            canvas.SaveState();

            var margin = 10;
            canvas.Translate(dirtyRect.Width - (2 * margin), margin);

            var vBuilder = new PathBuilder();
            var path = vBuilder.BuildPath(FluentDatePickerIcon);

            canvas.FillColor = Fluent.Color.Foreground.NeutralSecondary.ToColor();
            canvas.FillPath(path);

            canvas.RestoreState();
        }

        public void DrawPlaceholder(ICanvas canvas, RectangleF dirtyRect, IDatePicker datePicker)
        {

        }

        public override Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
            new Size(widthConstraint, 32f);
    }
}
