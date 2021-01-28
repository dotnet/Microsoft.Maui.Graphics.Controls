using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using System.Graphics.Skia.Views;

namespace GraphicsControls.Sample.Tizen
{
    class Program : FormsApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            var app = new App();
            app.UserAppTheme = OSAppTheme.Light;
            LoadApplication(app);
        }

        static void Main(string[] args)
        {
            var app = new Program();
            var option = new InitializationOptions(app)
            {
                DisplayResolutionUnit = DisplayResolutionUnit.DP(true),
                UseSkiaSharp = true
            };
            Forms.Init(option);
            app.Run(args);
        }
    }
}
