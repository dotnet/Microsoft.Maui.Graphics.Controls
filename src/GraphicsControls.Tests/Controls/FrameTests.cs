using Xamarin.Forms;
using Xunit;

namespace GraphicsControls.Tests
{
    public class FrameTests
	{
		public FrameTests()
			=> Device.PlatformServices = new MockPlatformServices();

		[Fact]
		public void ConstructorTest()
		{
			Frame frame = new Frame();

			Assert.Null(frame.Content);
		}

		[Fact]
		public void FrameLayersTest()
		{
			var frame = new Frame();
			int layersCount = frame.GraphicsLayers.Count;

			Assert.NotEqual(0, layersCount);
		}
	}
}