using System;
using Xamarin.Forms;
using Xunit;

namespace GraphicsControls.Tests
{
    public class StepperTests
	{
		public StepperTests()
			=> Device.PlatformServices = new MockPlatformServices();

		[Fact]
		public void ConstructorTest()
		{
			var stepper = new Stepper(120, 200, 150, 2);

			Assert.Equal(120, stepper.Minimum);
			Assert.Equal(200, stepper.Maximum);
			Assert.Equal(150, stepper.Value);
			Assert.Equal(2, stepper.Increment);
		}

		[Fact]
		public void StepperLayersTest()
		{
			var stepper = new Stepper();
			int layersCount = stepper.GraphicsLayers.Count;

			Assert.NotEqual(0, layersCount);
		}

		[Fact]
		public void InvalidConstructorTest()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => new Stepper(100, 0, 50, 1));
		}

		[Fact]
		public void InvalidMaxValueTest()
		{
			Stepper stepper = new Stepper();
			Assert.Throws<ArgumentException>(() => stepper.Maximum = stepper.Minimum - 1);
		}

		[Fact]
		public void InvalidMinValueTest()
		{
			Stepper stepper = new Stepper();
			Assert.Throws<ArgumentException>(() => stepper.Minimum = stepper.Maximum + 1);
		}

		[Fact]
		public void ValidMaxValueTest()
		{
			Stepper stepper = new Stepper();

			stepper.Maximum = 2000;

			Assert.Equal(2000, stepper.Maximum);
		}

		[Fact]
		public void ValidMinValueTest()
		{
			Stepper stepper = new Stepper();

			stepper.Maximum = 2000;
			stepper.Minimum = 200;

			Assert.Equal(200, stepper.Minimum);
		}

		[Fact]
		public void ConstructorClampValueTest()
		{
			Stepper stepper = new Stepper(0, 100, 2000, 1);

			Assert.Equal(100, stepper.Value);

			stepper = new Stepper(0, 100, -200, 1);

			Assert.Equal(0, stepper.Value);
		}

		[Fact]
		public void MinClampValueTest()
		{
			Stepper stepper = new Stepper();

			bool minThrown = false;
			bool valThrown = false;

			stepper.PropertyChanged += (sender, e) =>
			{
				switch (e.PropertyName)
				{
					case "Minimum":
						minThrown = true;
						break;
					case "Value":
						Assert.False(minThrown);
						valThrown = true;
						break;
				}
			};

			stepper.Minimum = 10;

			Assert.Equal(10, stepper.Minimum);
			Assert.Equal(10, stepper.Value);
			Assert.True(minThrown);
			Assert.True(valThrown);
		}

		[Fact]
		public void MaxClampValueTest()
		{
			Stepper stepper = new Stepper();

			stepper.Value = 50;

			bool maxThrown = false;
			bool valThrown = false;

			stepper.PropertyChanged += (sender, e) =>
			{
				switch (e.PropertyName)
				{
					case "Maximum":
						maxThrown = true;
						break;
					case "Value":
						Assert.False(maxThrown);
						valThrown = true;
						break;
				}
			};

			stepper.Maximum = 25;

			Assert.Equal(25, stepper.Maximum);
			Assert.Equal(25, stepper.Value);
			Assert.True(maxThrown);
			Assert.True(valThrown);
		}
	}
}
