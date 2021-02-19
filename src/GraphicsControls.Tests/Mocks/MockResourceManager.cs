using System.Globalization;
using System.Resources;

namespace GraphicsControls.Tests
{
	class MockResourceManager : ResourceManager
	{
		public override string GetString(string name, CultureInfo culture) => culture.EnglishName;
	}
}