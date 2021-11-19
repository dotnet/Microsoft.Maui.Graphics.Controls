using System;

namespace Microsoft.Maui.Graphics.Controls
{
    public static class ResourceDictionaryExtensions
	{
		public static void AddResources(string key, string uri)
		{
			var resources = UI.Xaml.Application.Current?.Resources;
		
			if (resources == null)
				return;

			var dictionaries = resources.MergedDictionaries;
			
			if (dictionaries == null)
				return;

			if (!resources.ContainsKey(key))
			{
				dictionaries.Add(new UI.Xaml.ResourceDictionary
				{
					Source = new Uri(uri)
				});
			}
		}

		public static void AddResources<T>()
			where T : UI.Xaml.ResourceDictionary, new()
		{
			var dictionaries = UI.Xaml.Application.Current?.Resources?.MergedDictionaries;
			
			if (dictionaries == null)
				return;

			var found = false;
			foreach (var dic in dictionaries)
			{
				if (dic is T)
				{
					found = true;
					break;
				}
			}

			if (!found)
			{
				var dic = new T();
				dictionaries.Add(dic);
			}
		}
	}
}
