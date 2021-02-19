using Xamarin.Forms;
using Xunit;

namespace GraphicsControls.Tests
{
    public class EntryTests
	{
		public EntryTests()
			=> Device.PlatformServices = new MockPlatformServices();

		[Fact]
		public void ConstructorTest()
		{
			Entry entry = new Entry();

			Assert.NotNull(entry);
		}

		[Fact]
		public void EntryLayersTest()
		{
			var entry = new Entry();
			int layersCount = entry.GraphicsLayers.Count;

			Assert.NotEqual(0, layersCount);
		}
	}
}