using GraphicsControls.Sample.Controls;
using Microsoft.Maui.Graphics.Controls;
using Microsoft.Maui.Graphics.Controls.Hosting;

namespace GraphicsControls.Sample
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .ConfigureGraphicsControls(DrawableType.Material)
                .ConfigureMauiHandlers(handlers =>
                {
                    handlers.AddHandler(typeof(CustomSliderDrawable), typeof(CustomSliderDrawableHandler));
                    handlers.AddHandler(typeof(CustomSliderMapper), typeof(CustomSliderMapperHandler));
                    handlers.AddHandler(typeof(DrawCustomSlider), typeof(DrawCustomSliderHandler));
                });

            return builder.Build();
        }
    }
}