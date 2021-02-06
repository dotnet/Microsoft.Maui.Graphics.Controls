using Xamarin.Forms;
using Xunit;

namespace GraphicsControls.Tests
{
    public class DatePickerTests
    {
		public DatePickerTests()
			 => Device.PlatformServices = new MockPlatformServices();

		[Fact]
		public void ConstructorTest()
		{
			var datePicker = new DatePicker();

			Assert.NotNull(datePicker);
		}

		[Fact]
		public void DatePickerLayersTest()
		{
			var datePicker = new DatePicker();
			int layersCount = datePicker.GraphicsLayers.Count;

			Assert.NotEqual(0, layersCount);
		}
	}
}