using Xamarin.Forms;

namespace GraphicsControls
{
    public interface IBorder
    {
		Color BorderColor { get; }
		double BorderWidth { get; }
	}

	public static class BorderElement
	{
		public static readonly BindableProperty BorderColorProperty =
			BindableProperty.Create(nameof(IBorder.BorderColor), typeof(Color), typeof(IBorder), Color.Default);

		public static readonly BindableProperty BorderWidthProperty =
			BindableProperty.Create(nameof(IBorder.BorderWidth), typeof(double), typeof(IBorder), 1.0d);
	}
}