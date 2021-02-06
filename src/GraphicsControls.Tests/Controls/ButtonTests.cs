using Xamarin.Forms;
using Xunit;

namespace GraphicsControls.Tests
{
    public class ButtonTests
	{
		public ButtonTests()
			=> Device.PlatformServices = new MockPlatformServices();

		[Fact]
		public void ConstructorTest()
		{
			Button button = new Button();

			Assert.NotNull(button);
		}

		[Fact]
		public void ButtonLayersTest()
		{
			var button = new Button();
			int layersCount = button.GraphicsLayers.Count;

			Assert.NotEqual(0, layersCount);
		}

		[Fact]
		public void TextTest()
		{
			string text = "Button";

			Button button = new Button();

			button.Text = text;

			Assert.Equal(text, button.Text);
		}
	}
}