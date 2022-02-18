using Microsoft.Maui.Controls;
using Microsoft.Maui.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Maui.Graphics.Controls.Hosting
{
    public static class AppHostBuilderExtensions
    {
        public static MauiAppBuilder ConfigureGraphicsControls(this MauiAppBuilder builder, DrawableType drawableType = DrawableType.Material)
        {
            builder.ConfigureMauiHandlers(handlers =>
            {
                handlers.AddGraphicsControlsHandlers(drawableType);
            });

            return builder;
        }

        public static IMauiHandlersCollection AddGraphicsControlsHandlers(this IMauiHandlersCollection handlersCollection, DrawableType drawableType = DrawableType.Material)
        {
            handlersCollection.AddTransient(typeof(Button), h => new ButtonHandler(drawableType));
            handlersCollection.AddTransient(typeof(CheckBox), h => new CheckBoxHandler(drawableType));
            handlersCollection.AddTransient(typeof(DatePicker), h => new DatePickerHandler(drawableType));
            handlersCollection.AddTransient(typeof(Editor), h => new EditorHandler(drawableType));
            handlersCollection.AddTransient(typeof(Entry), h => new EntryHandler(drawableType));
            handlersCollection.AddTransient(typeof(GraphicsView), typeof(GraphicsViewHandler));
            handlersCollection.AddTransient(typeof(ProgressBar), h => new ProgressBarHandler(drawableType));
            handlersCollection.AddTransient(typeof(Slider), h => new SliderHandler(drawableType));
            handlersCollection.AddTransient(typeof(Stepper), h => new StepperHandler(drawableType));
            handlersCollection.AddTransient(typeof(Switch), h => new SwitchHandler(drawableType));
            handlersCollection.AddTransient(typeof(TimePicker), h => new TimePickerHandler(drawableType));

            return handlersCollection;
        }
    }
}