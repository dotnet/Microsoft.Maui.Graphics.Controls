using System.Graphics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using XColor = Xamarin.Forms.Color;

namespace GraphicsControls
{
    public partial class ProgressBar : GraphicsVisualView
    {
        public static readonly BindableProperty ProgressProperty =
            BindableProperty.Create(nameof(Progress), typeof(double), typeof(ProgressBar), 0d,
                coerceValue: (bo, v) => ((double)v).Clamp(0, 1));

        public static readonly BindableProperty ProgressColorProperty =
            BindableProperty.Create(nameof(ProgressColor), typeof(XColor), typeof(ProgressBar), XColor.Default);

        public double Progress
        {
            get { return (double)GetValue(ProgressProperty); }
            set { SetValue(ProgressProperty, value); }
        }

        public XColor ProgressColor
        {
            get { return (XColor)GetValue(ProgressColorProperty); }
            set { SetValue(ProgressColorProperty, value); }
        }

        public override void Draw(ICanvas canvas, RectangleF dirtyRect)
        {
            base.Draw(canvas, dirtyRect);

            DrawProgressTrack(canvas, dirtyRect);
            DrawProgressBar(canvas, dirtyRect);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == ProgressProperty.PropertyName ||
                propertyName == BackgroundColorProperty.PropertyName ||
                propertyName == FlowDirectionProperty.PropertyName)
                InvalidateDraw();
        }

        public override void Load()
        {
            base.Load();

            HeightRequest = 12;
        }

        protected virtual void DrawProgressTrack(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    DrawMaterialProgressTrack(canvas, dirtyRect);
                    break;
                case VisualType.Cupertino:
                    DrawCupertinoProgressTrack(canvas, dirtyRect);
                    break;
                case VisualType.Fluent:
                    DrawFluentProgressTrack(canvas, dirtyRect);
                    break;
            }
        }

        protected virtual void DrawProgressBar(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    DrawMaterialProgressBar(canvas, dirtyRect);
                    break;
                case VisualType.Cupertino:
                    DrawCupertinoProgressBar(canvas, dirtyRect);
                    break;
                case VisualType.Fluent:
                    DrawFluentProgressBar(canvas, dirtyRect);
                    break;
            }
        }

        public Task<bool> ProgressTo(double value, uint length, Easing easing)
        {
            var tcs = new TaskCompletionSource<bool>();

            this.Animate("Progress", d => Progress = d, Progress, value, length: length, easing: easing, finished: (d, finished) => tcs.SetResult(finished));

            return tcs.Task;
        }
    }
}