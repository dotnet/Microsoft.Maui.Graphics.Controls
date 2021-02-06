using System;
using Xamarin.Forms;

namespace GraphicsControls
{
    public class GraphicsVisualView : GraphicsView, IVisualType
    {
        public GraphicsVisualView()
        {
            VisualTypeElement.VisualTypeChanged += OnVisualTypeChanged;
        }

        public static readonly BindableProperty VisualTypeProperty = VisualTypeElement.VisualTypeProperty;

        public VisualType VisualType
        {
            get { return (VisualType)GetValue(VisualTypeElement.VisualTypeProperty); }
            set { SetValue(VisualTypeElement.VisualTypeProperty, value); }
        }

        public override void Unload()
        {
            base.Unload();

            VisualTypeElement.VisualTypeChanged -= OnVisualTypeChanged;
        }

        void OnVisualTypeChanged(object sender, EventArgs e)
        {
            InvalidateDraw();
        }
    }
}