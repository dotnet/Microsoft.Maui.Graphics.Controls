using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Graphics.Controls;
using Microsoft.Maui.Hosting;

[assembly: XamlCompilationAttribute(XamlCompilationOptions.Compile)]

namespace GraphicsControls.Sample
{
    public class Startup : IStartup
    {
        public void Configure(IAppHostBuilder appBuilder)
        {
            appBuilder
                .UseMauiApp<App>()
                .ConfigureMauiHandlers(handlers =>
                {
                    handlers.AddHandler(typeof(Button), typeof(ButtonHandler));
                    handlers.AddHandler(typeof(CheckBox), typeof(CheckBoxHandler));
                    handlers.AddHandler(typeof(DatePicker), typeof(DatePickerHandler));
                    handlers.AddHandler(typeof(Editor), typeof(EditorHandler));
                    handlers.AddHandler(typeof(Entry), typeof(EntryHandler));
                    handlers.AddHandler(typeof(ProgressBar), typeof(ProgressBarHandler));
                    handlers.AddHandler(typeof(Slider), typeof(SliderHandler));
                    handlers.AddHandler(typeof(Stepper), typeof(StepperHandler));
                    handlers.AddHandler(typeof(Switch), typeof(SwitchHandler));
                    handlers.AddHandler(typeof(TimePicker), typeof(TimePickerHandler));
                });
        }
    }
}