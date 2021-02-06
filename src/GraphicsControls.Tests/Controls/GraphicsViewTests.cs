using System.Linq;
using Xamarin.Forms;
using Xunit;

namespace GraphicsControls.Tests
{
    public class GraphicsViewTests
    {
        public GraphicsViewTests()
            => Device.PlatformServices = new MockPlatformServices();

        [Fact]
        public void LayersTest()
        {
            var view = new Slider();

            Assert.NotEmpty(view.GraphicsLayers);
        }

        [Fact]
        public void AddLayerTest()
        {
            var view = new Slider();

            var layersCount = view.GraphicsLayers.Count;

            Assert.Equal(4, layersCount);

            view.AddLayer("NewLayer");

            var newLayersCount = view.GraphicsLayers.Count;

            Assert.Equal(5, newLayersCount);
        }

        [Fact]
        public void RemoveLayerTest()
        {
            var view = new Slider();

            var layersCount = view.GraphicsLayers.Count;

            Assert.Equal(4, layersCount);

            view.RemoveLayer(Slider.Layers.TrackBackground);

            var newLayersCount = view.GraphicsLayers.Count;

            Assert.Equal(3, newLayersCount);
        }
    }
}
