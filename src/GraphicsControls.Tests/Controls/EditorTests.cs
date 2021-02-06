using Xamarin.Forms;
using Xunit;

namespace GraphicsControls.Tests
{
    public class EditorTests
	{
		public EditorTests()
			=> Device.PlatformServices = new MockPlatformServices();

		[Fact]
		public void ConstructorTest()
		{
			Editor editor = new Editor();

			Assert.NotNull(editor);
		}

		[Fact]
		public void EditorLayersTest()
		{
			var editor = new Editor();
			int layersCount = editor.GraphicsLayers.Count;

			Assert.NotEqual(0, layersCount);
		}
	}
}