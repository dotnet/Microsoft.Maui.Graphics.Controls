using System;
using Android.App;
using Android.Runtime;
using Microsoft.Maui;
using Microsoft.Maui.Graphics.Controls;

namespace GraphicsControls.Sample.Droid
{
	[Application]
	public class MainApplication : MauiApplication<Startup>
	{
		public MainApplication(IntPtr handle, JniHandleOwnership ownership) : base(handle, ownership)
		{
		}
	}
}