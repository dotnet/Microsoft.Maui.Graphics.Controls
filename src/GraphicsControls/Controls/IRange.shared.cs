using Xamarin.Forms;

namespace GraphicsControls
{
	public interface IRange
	{
		double Minimum { get; }
		double Maximum { get; }
	}

	public static class RangeElement
	{
		public static readonly BindableProperty MinimumProperty =
			BindableProperty.Create(nameof(IRange.Minimum), typeof(double), typeof(IRange), 0.0d);

		public static readonly BindableProperty MaximumProperty =
			BindableProperty.Create(nameof(IRange.Maximum), typeof(double), typeof(IRange), 1.0d);
	}
}