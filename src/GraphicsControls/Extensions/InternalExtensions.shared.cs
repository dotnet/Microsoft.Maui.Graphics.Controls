using System.Reflection;
using Xamarin.Forms;

namespace GraphicsControls.Extensions
{
    public static class InternalExtensions
    {
        public static T GetInternalField<T>(this BindableObject element, string propertyKeyName) where T : class
        {
            var pi = element.GetType().GetField(propertyKeyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            var key = (T)pi?.GetValue(element);

            return key;
        }
    }
}