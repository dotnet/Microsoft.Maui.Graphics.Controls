using Xamarin.Forms;
using Xunit;

namespace GraphicsControls.Tests
{
    public class TimePickerTests
    {
		public TimePickerTests()
			   => Device.PlatformServices = new MockPlatformServices();

		[Fact]
		public void ConstructorTest()
		{
			var timePicker = new TimePicker();

			Assert.NotNull(timePicker);
		}

		[Fact]
		public void TimePickerLayersTest()
		{
			var timePicker = new TimePicker();
			int layersCount = timePicker.GraphicsLayers.Count;

			Assert.NotEqual(0, layersCount);
		}
	}
}