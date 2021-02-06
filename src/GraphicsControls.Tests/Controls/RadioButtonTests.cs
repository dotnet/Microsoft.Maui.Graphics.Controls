using Xamarin.Forms;
using Xunit;

namespace GraphicsControls.Tests
{
    public class RadioButtonTests
    {
		public RadioButtonTests()
		   => Device.PlatformServices = new MockPlatformServices();

		[Fact]
		public void ConstructorTest()
		{
			var radioButton = new RadioButton();

			Assert.NotNull(radioButton);
		}

		[Fact]
		public void RadioButtonLayersTest()
		{
			var radioButton = new RadioButton();
			int layersCount = radioButton.GraphicsLayers.Count;

			Assert.NotEqual(0, layersCount);
		}

		[Fact]
		public void ThereCanBeOnlyOneTest()
		{
			var groupName = "foo";

			var radioButton1 = new RadioButton() { GroupName = groupName };
			var radioButton2 = new RadioButton() { GroupName = groupName };
			var radioButton3 = new RadioButton() { GroupName = groupName };
			var radioButton4 = new RadioButton() { GroupName = groupName };

			var layout = new Grid();

			layout.Children.Add(radioButton1);
			layout.Children.Add(radioButton2);
			layout.Children.Add(radioButton3);
			layout.Children.Add(radioButton4);

			radioButton1.IsChecked = true;

			Assert.True(radioButton1.IsChecked);
			Assert.False(radioButton2.IsChecked);
			Assert.False(radioButton3.IsChecked);
			Assert.False(radioButton4.IsChecked);

			radioButton3.IsChecked = true;

			Assert.False(radioButton1.IsChecked);
			Assert.False(radioButton2.IsChecked);
			Assert.True(radioButton3.IsChecked);
			Assert.False(radioButton4.IsChecked);
		}

		[Fact]
		public void ImpliedGroupTest()
		{
			var radioButton1 = new RadioButton();
			var radioButton2 = new RadioButton();
			var radioButton3 = new RadioButton();

			var layout = new Grid();

			layout.Children.Add(radioButton1);
			layout.Children.Add(radioButton2);
			layout.Children.Add(radioButton3);

			radioButton1.IsChecked = true;

			Assert.True(radioButton1.IsChecked);
			Assert.False(radioButton2.IsChecked);
			Assert.False(radioButton3.IsChecked);

			radioButton3.IsChecked = true;

			Assert.False(radioButton1.IsChecked);
			Assert.False(radioButton2.IsChecked);
			Assert.True(radioButton3.IsChecked);
		}

		[Fact]
		public void ImpliedGroupDoesNotIncludeExplicitGroupsTest()
		{
			var radioButton1 = new RadioButton();
			var radioButton2 = new RadioButton();
			var radioButton3 = new RadioButton() { GroupName = "foo" };

			var layout = new Grid();

			layout.Children.Add(radioButton1);
			layout.Children.Add(radioButton2);
			layout.Children.Add(radioButton3);

			radioButton1.IsChecked = true;
			radioButton3.IsChecked = true;

			Assert.True(radioButton1.IsChecked);
			Assert.False(radioButton2.IsChecked);
			Assert.True(radioButton3.IsChecked);
		}

		[Fact]
		public void RemovingSelectedButtonFromGroupClearsSelectionTest()
		{
			var radioButton1 = new RadioButton() { GroupName = "foo" };
			var radioButton2 = new RadioButton() { GroupName = "foo" };
			var radioButton3 = new RadioButton() { GroupName = "foo" };

			radioButton1.IsChecked = true;
			radioButton2.IsChecked = true;

			Assert.False(radioButton1.IsChecked);
			Assert.True(radioButton2.IsChecked);
			Assert.False(radioButton3.IsChecked);

			radioButton2.GroupName = "bar";

			Assert.False(radioButton1.IsChecked);
			Assert.True(radioButton2.IsChecked);
			Assert.False(radioButton3.IsChecked);
		}

		[Fact]
		public void GroupControllerSelectionIsNullWhenSelectedButtonRemovedTest()
		{
			var layout = new Grid();
			layout.SetValue(RadioButtonGroup.GroupNameProperty, "foo");
			var selected = layout.GetValue(RadioButtonGroup.SelectedValueProperty);

			Assert.Null(selected);

			var radioButton1 = new RadioButton() { Value = 1 };
			var radioButton2 = new RadioButton() { Value = 2 };
			var radioButton3 = new RadioButton() { Value = 3 };

			layout.Children.Add(radioButton1);
			layout.Children.Add(radioButton2);
			layout.Children.Add(radioButton3);

			Assert.Null(selected);

			radioButton1.IsChecked = true;

			selected = layout.GetValue(RadioButtonGroup.SelectedValueProperty);

			Assert.Equal(selected, 1);

			Assert.Equal(radioButton1.GroupName, "foo");
			radioButton1.GroupName = "bar";

			selected = layout.GetValue(RadioButtonGroup.SelectedValueProperty);
			Assert.Null(selected);
		}

		[Fact]
		public void GroupSelectedValueUpdatesWhenSelectedButtonValueUpdatesTest()
		{
			var layout = new Grid();
			layout.SetValue(RadioButtonGroup.GroupNameProperty, "foo");

			var radioButton1 = new RadioButton() { Value = 1, IsChecked = true };
			var radioButton2 = new RadioButton() { Value = 2 };
			var radioButton3 = new RadioButton() { Value = 3 };

			layout.Children.Add(radioButton1);
			layout.Children.Add(radioButton2);
			layout.Children.Add(radioButton3);

			Assert.Equal(1, layout.GetValue(RadioButtonGroup.SelectedValueProperty));

			radioButton1.Value = "updated";

			Assert.Equal("updated", layout.GetValue(RadioButtonGroup.SelectedValueProperty));
		}
	}
}