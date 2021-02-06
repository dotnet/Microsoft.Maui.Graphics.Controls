using Xamarin.Forms;
using Xunit;

namespace GraphicsControls.Tests
{
    public class ProgressBarTests
	{
		public ProgressBarTests()
			=> Device.PlatformServices = new MockPlatformServices();

		[Fact]
		public void ConstructorTest()
		{
			ProgressBar progressBar = new ProgressBar();

			Assert.NotNull(progressBar);
		}

		[Fact]
		public void ProgressBarLayersTest()
		{
			var progressBar = new ProgressBar();
			int layersCount = progressBar.GraphicsLayers.Count;

			Assert.NotEqual(0, layersCount);
		}

		[Fact]
		public void ClampTest()
		{
			ProgressBar bar = new ProgressBar();

			bar.Progress = 2;
			Assert.Equal(1, bar.Progress);

			bar.Progress = -1;
			Assert.Equal(0, bar.Progress);
		}
	}
}