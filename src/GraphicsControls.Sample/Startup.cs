using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;

namespace Microsoft.Maui.Graphics.Controls
{
    public class Startup : IStartup
	{
		public void Configure(IAppHostBuilder appBuilder)
		{
			appBuilder
				.UseFormsCompatibility()
				.ConfigureMauiHandlers(handlers =>
				{
					handlers.AddHandler(typeof(IButton), typeof(ButtonHandler));
					handlers.AddHandler(typeof(ICheckBox), typeof(CheckBoxHandler));
					handlers.AddHandler(typeof(IDatePicker), typeof(DatePickerHandler));
					handlers.AddHandler(typeof(IEditor), typeof(EditorHandler));
					handlers.AddHandler(typeof(IEntry), typeof(EntryHandler));
					handlers.AddHandler(typeof(IProgress), typeof(ProgressBarHandler));
					handlers.AddHandler(typeof(ISlider), typeof(SliderHandler));
					handlers.AddHandler(typeof(IStepper), typeof(StepperHandler));
					handlers.AddHandler(typeof(ISwitch), typeof(SwitchHandler));
					handlers.AddHandler(typeof(ITimePicker), typeof(TimePickerHandler));
				})
				.UseMauiApp<MyApp>();
		}
	}
}