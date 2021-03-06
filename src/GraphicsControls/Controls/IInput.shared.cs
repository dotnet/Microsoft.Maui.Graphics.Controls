using Xamarin.Forms;

namespace GraphicsControls
{
	public interface IInput : IText, IPlaceholder, IFont
	{

	}

	public static class InputElement
    {
		public static readonly BindableProperty TextProperty =
			BindableProperty.Create(nameof(IInput.Text), typeof(string), typeof(IInput), string.Empty);

		public static readonly BindableProperty TextColorProperty =
			BindableProperty.Create(nameof(IInput.TextColor), typeof(Color), typeof(IInput), Color.Default);

		public static readonly BindableProperty CharacterSpacingProperty =
			BindableProperty.Create(nameof(IInput.CharacterSpacing), typeof(double), typeof(IInput), 0.0d);

		public static readonly BindableProperty PlaceholderProperty =
			BindableProperty.Create(nameof(IInput.Placeholder), typeof(string), typeof(IPlaceholder), string.Empty);

		public static readonly BindableProperty PlaceholderColorProperty =
			BindableProperty.Create(nameof(IInput.PlaceholderColor), typeof(Color), typeof(IPlaceholder), Color.Default);
	}
}