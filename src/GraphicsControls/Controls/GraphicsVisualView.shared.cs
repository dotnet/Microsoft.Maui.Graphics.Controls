using System;
using Xamarin.Forms;

namespace GraphicsControls
{
    public class GraphicsVisualView : GraphicsView, IVisual
    {
        public GraphicsVisualView()
        {
            VisualElement.VisualTypeChanged += OnVisualTypeChanged;
        }

        public static readonly BindableProperty VisualTypeProperty = VisualElement.VisualTypeProperty;

        public VisualType VisualType
        {
            get { return (VisualType)GetValue(VisualElement.VisualTypeProperty); }
            set { SetValue(VisualElement.VisualTypeProperty, value); }
        }

        public override void Unload()
        {
            base.Unload();

            VisualElement.VisualTypeChanged -= OnVisualTypeChanged;
        }

        void OnVisualTypeChanged(object sender, EventArgs e)
        {
            InvalidateDraw();
        }
    }
}