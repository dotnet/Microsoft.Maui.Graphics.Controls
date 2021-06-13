namespace Microsoft.Maui.Graphics.Controls
{
    public static class EntryExtensions
    {
        public static void UpdateText(this GraphicsEntry nativeView, IEntry entry)
        {
            nativeView.Text = entry.Text;
        }
    }
}