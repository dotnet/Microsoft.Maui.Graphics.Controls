using System;
using Xamarin.Forms;
using Xunit;

namespace GraphicsControls.Tests
{
    public class SliderTests
	{
		public SliderTests()
			=> Device.PlatformServices = new MockPlatformServices();

		[Fact]
		public void ConstructorTest()
		{
			var slider = new Slider(20, 200, 50);

			Assert.Equal(20, slider.Minimum);
			Assert.Equal(200, slider.Maximum);
			Assert.Equal(50, slider.Value);
		}

		[Fact]
		public void SliderLayersTest()
		{
			var slider = new Slider();
			int layersCount = slider.GraphicsLayers.Count;

			Assert.NotEqual(0, layersCount);
		}

		[Fact]
		public void InvalidConstructorTest()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => new Slider(10, 5, 10));
		}

		[Fact]
		public void ConstructorClampingTest()
		{
			Slider slider = new Slider(50, 100, 0);

			Assert.Equal(50, slider.Value);
		}

		[Fact]
		public void MinValueClampTest()
		{
			Slider slider = new Slider(0, 100, 0);

			slider.Minimum = 10;

			Assert.Equal(10, slider.Value);
			Assert.Equal(10, slider.Minimum);
		}

		[Fact]
		public void MaxValueClampTest()
		{
			Slider slider = new Slider(0, 100, 100);

			slider.Maximum = 10;

			Assert.Equal(10, slider.Value);
			Assert.Equal(10, slider.Maximum);
		}

		[Fact]
		public void TestInvalidMaxValue()
		{
			var slider = new Slider();
			Assert.Throws<ArgumentException>(() => slider.Maximum = slider.Minimum - 1);
		}

		[Fact]
		public void InvalidMinValueTest()
		{
			var slider = new Slider();
			Assert.Throws<ArgumentException>(() => slider.Minimum = slider.Maximum + 1);
		}

		[Fact]
		public void ValueChangedTest()
		{
			var slider = new Slider();
			var changed = false;

			slider.ValueChanged += (sender, arg) => changed = true;

			slider.Value += 1;

			Assert.True(changed);
		}
	}
}
