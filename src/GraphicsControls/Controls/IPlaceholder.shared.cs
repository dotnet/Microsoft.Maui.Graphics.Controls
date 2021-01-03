using Xamarin.Forms;

namespace GraphicsControls
{
	public interface IPlaceholder
	{
		string Placeholder { get; }
		Color PlaceholderColor { get; }
	}

	public static class PlaceholderElement
	{
		public static readonly BindableProperty PlaceholderProperty =
			BindableProperty.Create(nameof(IPlaceholder.Placeholder), typeof(string), typeof(IPlaceholder), string.Empty);

		public static readonly BindableProperty PlaceholderColorProperty =
			BindableProperty.Create(nameof(IPlaceholder.PlaceholderColor), typeof(Color), typeof(IPlaceholder), Color.Default);
	}
}