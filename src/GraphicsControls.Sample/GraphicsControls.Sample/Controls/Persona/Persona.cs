using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Controls;
using System.Diagnostics;

namespace GraphicsControls.Sample.Controls
{
    public class Persona : Microsoft.Maui.Graphics.Controls.GraphicsView
    {
        public Persona()
        {
            UpdateAvatarSize();
        }

        public static readonly BindableProperty NameProperty =
              BindableProperty.Create(nameof(Name), typeof(string), typeof(Persona), string.Empty);

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public static readonly BindableProperty AvatarSizeProperty =
            BindableProperty.Create(nameof(AvatarSize), typeof(AvatarSize), typeof(Persona), AvatarSize.Small,
                propertyChanged: OnAvatarSizeChanged);

        static void OnAvatarSizeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as Persona)?.UpdateAvatarSize();
        }

        public AvatarSize AvatarSize
        {
            get { return (AvatarSize)GetValue(AvatarSizeProperty); }
            set { SetValue(AvatarSizeProperty, value); }
        }

        public override void Draw(ICanvas canvas, RectangleF dirtyRect)
        {
            base.Draw(canvas, dirtyRect);

            DrawBackground(canvas, dirtyRect);
            DrawIndicator(canvas, dirtyRect);
            DrawInitials(canvas, dirtyRect);
        }

        public override void OnTouchDown(Point point)
        {
           Debug.WriteLine($"Touch Down {point}");
        }

        void DrawBackground(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            canvas.FillColor = Color.FromArgb("#4967F5");

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var height = AvatarSize.GetAvatarSize();
            var width = height;

            canvas.FillEllipse(x, y, width, height);

            canvas.RestoreState();
        }

        public virtual void DrawIndicator(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            canvas.FillColor = Color.FromArgb("#FFFFFF");

            var position = AvatarSize.GetAvatarSize() - AvatarSize.GetAvatarIndicatorSize();

            var x = position;
            var y = x;

            var avatarIndicatorBorderHeight = AvatarSize.GetAvatarIndicatorSize();
            var avatarIndicatorBorderWidth = avatarIndicatorBorderHeight;

            canvas.FillEllipse(x, y, avatarIndicatorBorderWidth, avatarIndicatorBorderHeight);

            canvas.RestoreState();

            canvas.SaveState();

            var borderWidth = 4;

            canvas.FillColor = Color.FromArgb("#6BB700");

            var avatarSize = AvatarSize.GetAvatarIndicatorSize() - borderWidth;
            var avatarIndicatorFillHeight = avatarSize;
            var avatarIndicatorFillWidth = avatarIndicatorFillHeight;

            canvas.FillEllipse(x + borderWidth / 2, y + borderWidth / 2, avatarIndicatorFillWidth, avatarIndicatorFillHeight);
            canvas.RestoreState();

            canvas.SaveState();

            var translateX = avatarSize / 4;
            var translateY = avatarSize / 3;

            canvas.Translate(x + translateX, y + translateY);

            var scale = AvatarSize.GetAvatarIndicatorIconScale();
            canvas.Scale(scale, scale);

            var vBuilder = new PathBuilder();

            var path =
                vBuilder.BuildPath(
                    "M6.13281 0.707031C6.24219 0.707031 6.34375 0.727865 6.4375 0.769531C6.53385 0.808594 6.61719 0.863281 6.6875 0.933594C6.75781 1.00391 6.8125 1.08724 6.85156 1.18359C6.89323 1.27734 6.91406 1.37891 6.91406 1.48828C6.91406 1.59245 6.89453 1.69271 6.85547 1.78906C6.81641 1.88542 6.76042 1.97005 6.6875 2.04297L3.58594 5.14844C3.51302 5.22135 3.42839 5.27865 3.33203 5.32031C3.23568 5.35938 3.13542 5.37891 3.03125 5.37891C2.92708 5.37891 2.82682 5.35938 2.73047 5.32031C2.63411 5.27865 2.54948 5.22135 2.47656 5.14844L1.0625 3.73438C0.989583 3.66146 0.932292 3.57682 0.890625 3.48047C0.851562 3.38411 0.832031 3.28385 0.832031 3.17969C0.832031 3.07031 0.852865 2.96875 0.894531 2.875C0.936198 2.77865 0.992188 2.69531 1.0625 2.625C1.13281 2.55469 1.21484 2.5 1.30859 2.46094C1.40495 2.41927 1.50781 2.39844 1.61719 2.39844C1.72135 2.39844 1.82161 2.41797 1.91797 2.45703C2.01432 2.49609 2.09896 2.55208 2.17188 2.625L3.03125 3.48438L5.57812 0.933594C5.65104 0.860677 5.73568 0.804688 5.83203 0.765625C5.92839 0.726562 6.02865 0.707031 6.13281 0.707031Z");

            canvas.FillColor = Colors.White;
            canvas.FillPath(path);

            canvas.RestoreState();
        }

        void DrawInitials(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            canvas.FontColor = Color.FromArgb(Fluent.Color.Dark.Foreground.Primary);

            canvas.FontSize = AvatarSize.GetInitialsFontSize();

            var height = AvatarSize.GetAvatarSize();
            var width = height;

            var initials = GetInitials(Name);
            canvas.DrawString(initials, 0, 0, width, height, HorizontalAlignment.Center, VerticalAlignment.Center);

            canvas.RestoreState();
        }

        void UpdateAvatarSize() => HeightRequest = WidthRequest = AvatarSize.GetAvatarSize();

        string GetInitials(string text)
        {
            string result = string.Empty;

            bool v = true;

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ' ')
                    v = true;
                else if (text[i] != ' ' && v)
                {
                    result += text[i];
                    v = false;
                }
            }

            return result;
        }
    }
}