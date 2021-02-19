using System;
using Xamarin.Forms;

namespace GraphicsControls
{
	public interface IVisualType
	{
		VisualType VisualType { get; }
	}

	public static class VisualTypeElement
	{
		public static readonly BindableProperty VisualTypeProperty =
			BindableProperty.Create(nameof(IVisualType.VisualType), typeof(VisualType), typeof(IVisualType), DefaultVisualType(),
				propertyChanged: OnVisualTypeChanged);

        static void OnVisualTypeChanged(BindableObject bindable, object oldValue, object newValue)
        {
			VisualTypeChanged?.Invoke(bindable, EventArgs.Empty);
		}

        internal static VisualType DefaultVisualType()
        {
			if (Device.RuntimePlatform == Device.Android)
				return VisualType.Material;

			if (Device.RuntimePlatform == Device.iOS)
				return VisualType.Cupertino;

			if (Device.RuntimePlatform == Device.UWP)
				return VisualType.Fluent;

			return VisualType.Material;
		}

		public static event EventHandler VisualTypeChanged;
	}
}