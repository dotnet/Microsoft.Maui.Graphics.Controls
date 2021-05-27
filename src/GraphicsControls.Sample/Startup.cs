using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Hosting;

namespace Microsoft.Maui.Graphics.Controls
{
    public class Startup : IStartup
	{
		public void Configure(IAppHostBuilder appBuilder)
		{
	
			appBuilder
				.UseFormsCompatibility()
				.ConfigureMauiHandlers((_, handlersCollection) => handlersCollection.AddHandlers(new Dictionary<Type, Type>
				{
					{ typeof(IButton), typeof(ButtonHandler) },
					{ typeof(ICheckBox), typeof(CheckBoxHandler) },
					{ typeof(IDatePicker), typeof(DatePickerHandler) },
					{ typeof(IEditor), typeof(EditorHandler) },
					{ typeof(IEntry), typeof(EntryHandler) },
					{ typeof(IProgress), typeof(ProgressBarHandler) },
					{ typeof(ISlider), typeof(SliderHandler) },
					{ typeof(IStepper), typeof(StepperHandler) },
					{ typeof(ISwitch), typeof(SwitchHandler) },
					{ typeof(ITimePicker), typeof(TimePickerHandler) },
				}))
				.UseMauiApp<MyApp>();
		}
	}
}