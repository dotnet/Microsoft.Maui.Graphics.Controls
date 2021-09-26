using Android.Text;
using System;

namespace Microsoft.Maui.Graphics.Controls
{
    public static class TextInputExtensions
    {
		public static void UpdateText(this ITextInput textInput, string? text)
		{
			// Even though <null> is technically different to "", it has no
			// functional difference to apps. Thus, hide it.
			var mauiText = textInput.Text ?? string.Empty;
			var nativeText = text ?? string.Empty;
			if (mauiText != nativeText)
				textInput.Text = nativeText;
		}

		public static void UpdateText(this ITextInput textInput, TextChangedEventArgs e)
		{
			if (e.BeforeCount == 0 && e.AfterCount == 0)
				return;

			if (e.Text is Java.Lang.ICharSequence cs)
				textInput.UpdateText(cs.ToString());
			else if (e.Text != null)
				textInput.UpdateText(String.Concat(e.Text));
			else
				textInput.UpdateText((string?)null);
		}
	}
}