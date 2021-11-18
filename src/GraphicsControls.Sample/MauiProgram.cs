using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Graphics.Controls;
using Microsoft.Maui.Graphics.Controls.Hosting;
using Microsoft.Maui.Hosting;

namespace GraphicsControls.Sample
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureGraphicsControls(DrawableType.Material);

            return builder.Build();
        }
    }
}