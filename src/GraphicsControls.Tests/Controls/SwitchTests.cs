using Xamarin.Forms;
using Xunit;

namespace GraphicsControls.Tests
{
    public class SwitchTests
	{
		public SwitchTests()
			=> Device.PlatformServices = new MockPlatformServices();

		[Fact]
		public void ConstructorTest()
		{
			Switch sw = new Switch();

			Assert.False(sw.IsToggled);
		}

		[Fact]
		public void SwitchLayersTest()
		{
			var sw = new Switch();
			int layersCount = sw.GraphicsLayers.Count;

			Assert.NotEqual(0, layersCount);
		}
	}
}