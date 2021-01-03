using Xamarin.Forms;

namespace GraphicsControls
{
    public interface ICornerRadius
	{
		double CornerRadius { get; }
	}

	public static class CornerRadiusElement
	{
		public static readonly BindableProperty CornerRadiusProperty =
			BindableProperty.Create(nameof(ICornerRadius.CornerRadius), typeof(double), typeof(ICornerRadius), 0.0d);
	}
}