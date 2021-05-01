using Android.App;
using Android.Content.PM;
using Microsoft.Maui;

namespace GraphicsControls.Sample.Droid
{
    [Activity(
        Label = "GraphicsControls.Sample",
        Icon = "@mipmap/icon",
        Theme = "@style/MainTheme",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : MauiAppCompatActivity
    {
    }
}