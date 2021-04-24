using System.Collections.Generic;

namespace Microsoft.Maui.Graphics.Controls
{
    public static class ListExtensions
    {
		public static IList<T> InsertAfter<T>(this IList<T> list, T itemToAdd, T previousItem)
		{
			var index = list.IndexOf(previousItem);
			list.Insert(index + 1, itemToAdd);
			return list;
		}

		public static IList<T> InsertAfter<T>(this IList<T> list, IEnumerable<T> itemsToAdd, T previousItem)
		{
			var index = list.IndexOf(previousItem) + 1;
			foreach (var item in itemsToAdd)
				list.Insert(index++, item);
			return list;
		}
	}
}