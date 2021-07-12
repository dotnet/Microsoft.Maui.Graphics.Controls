namespace Microsoft.Maui.Graphics.Controls
{
    public static class EditorExtensions
    {
        public static void UpdateText(this GraphicsEditor nativeView, IEditor entry)
        {
            nativeView.Text = entry.Text;
        }
    }
}