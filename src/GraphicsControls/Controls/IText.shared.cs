using Xamarin.Forms;

namespace GraphicsControls
{
	public interface IText
	{
		string Text { get; }
		Color TextColor { get; }
		double CharacterSpacing { get; }
	}

	public static class TextElement
    {
		public static readonly BindableProperty TextProperty =
			BindableProperty.Create(nameof(IText.Text), typeof(string), typeof(IText), string.Empty);

		public static readonly BindableProperty TextColorProperty =
			BindableProperty.Create(nameof(IText.TextColor), typeof(Color), typeof(IText), Color.Default);

		public static readonly BindableProperty CharacterSpacingProperty =
			BindableProperty.Create(nameof(IText.CharacterSpacing), typeof(double), typeof(IText), 0.0d);
	}
}