using Microsoft.Maui.Controls;
using Microsoft.Maui.Hosting;

#if __ANDROID__ || __IOS__ || MACCATALYST
using Microsoft.Maui.Graphics.Native;
#elif WINDOWS
using Microsoft.Maui.Graphics.Win2D;
#endif

namespace Microsoft.Maui.Graphics.Controls.Hosting
{
    public static class AppHostBuilderExtensions
    {
        public static MauiAppBuilder ConfigureGraphicsControls(this MauiAppBuilder builder)
        {
#if __ANDROID__ || __IOS__ || __MACCATALYST__
            GraphicsPlatform.RegisterGlobalService(NativeGraphicsService.Instance);
#elif WINDOWS
            GraphicsPlatform.RegisterGlobalService(W2DGraphicsService.Instance);
#endif

            builder.ConfigureMauiHandlers(handlers =>    
            {
                handlers.AddHandler(typeof(Button), typeof(ButtonHandler));
                handlers.AddHandler(typeof(CheckBox), typeof(CheckBoxHandler));
                handlers.AddHandler(typeof(DatePicker), typeof(DatePickerHandler));
                handlers.AddHandler(typeof(Editor), typeof(EditorHandler));
                handlers.AddHandler(typeof(Entry), typeof(EntryHandler));
                handlers.AddHandler(typeof(GraphicsView), typeof(GraphicsViewHandler));
                handlers.AddHandler(typeof(ProgressBar), typeof(ProgressBarHandler));
                handlers.AddHandler(typeof(Slider), typeof(SliderHandler));
                handlers.AddHandler(typeof(Stepper), typeof(StepperHandler));
                handlers.AddHandler(typeof(Switch), typeof(SwitchHandler));
                handlers.AddHandler(typeof(TimePicker), typeof(TimePickerHandler));
            });

            return builder;
        }
    }
}