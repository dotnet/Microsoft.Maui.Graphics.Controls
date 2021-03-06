using Xamarin.Forms;

namespace GraphicsControls
{
	public interface IFont : IText
	{
		FontAttributes FontAttributes { get; }
		string FontFamily { get; }
		double FontSize { get; }
	}

	public static class FontElement
	{
		public static readonly BindableProperty FontAttributesProperty =
			BindableProperty.Create(nameof(IFont.FontAttributes), typeof(FontAttributes), typeof(IFont), FontAttributes.None);

		public static readonly BindableProperty FontFamilyProperty =
			BindableProperty.Create(nameof(IFont.FontFamily), typeof(string), typeof(IFont), string.Empty);

		public static readonly BindableProperty FontSizeProperty =
			BindableProperty.Create(nameof(IFont.FontSize), typeof(double), typeof(IFont),
				Device.GetNamedSize(NamedSize.Medium, typeof(Label)));
	}
}