using Xamarin.Forms;
using Xunit;

namespace GraphicsControls.Tests
{
    public class ActivityIndicatorTests
	{
		public ActivityIndicatorTests()
			=> Device.PlatformServices = new MockPlatformServices();

		[Fact]
		public void ActivityIndicatorTest()
		{
			var activityIndicator = new ActivityIndicator();

			Assert.NotNull(activityIndicator);
		}

		[Fact]
		public void ActivityIndicatorLayersTest()
		{
			var activityIndicator = new ActivityIndicator();
			int layersCount = activityIndicator.GraphicsLayers.Count;

			Assert.NotEqual(0, layersCount);
		}
	}
}