using System;
using Android.Views;
using Microsoft.Maui.Handlers;

namespace Microsoft.Maui.Graphics.Controls
{
    public partial class EntryHandler : MixedGraphicsControlHandler<IEntry, IEntryDrawable, View>
	{
		protected override View CreateNativeView() => throw new NotImplementedException();

		public static void MapText(IViewHandler handler, IEntry entry) { }
	}
}