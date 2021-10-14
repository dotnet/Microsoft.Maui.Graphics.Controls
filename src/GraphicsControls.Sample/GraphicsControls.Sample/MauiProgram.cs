using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Graphics.Controls;
using Microsoft.Maui.Graphics.Controls.Hosting;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Hosting;
using GraphicsControls.Sample.Controls;

[assembly: XamlCompilationAttribute(XamlCompilationOptions.Compile)]

namespace GraphicsControls.Sample
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var appBuilder = MauiApp.CreateBuilder();

            appBuilder
                .UseMauiApp<App>()
                .ConfigureGraphicsControls(DrawableType.Material)               
                .ConfigureMauiHandlers(handlers =>
                {
                    handlers.AddHandler(typeof(CustomSliderDrawable), typeof(CustomSliderDrawableHandler));
                    handlers.AddHandler(typeof(CustomSliderMapper), typeof(CustomSliderMapperHandler));
                    handlers.AddHandler(typeof(DrawCustomSlider), typeof(DrawCustomSliderHandler));
                });

            return appBuilder.Build();
        }
    }
}