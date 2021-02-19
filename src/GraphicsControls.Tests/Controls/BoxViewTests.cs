using Xamarin.Forms;
using Xunit;

namespace GraphicsControls.Tests
{
    public class BoxViewTests
	{
		public BoxViewTests()
			=> Device.PlatformServices = new MockPlatformServices();

		[Fact]
		public void ConstructorTest()
		{
			var box = new BoxView
			{
				CornerRadius = 12
			};

			Assert.Equal(12, box.CornerRadius);
		}
	}
}