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
					{ typeof(ICheckBox), typeof(CheckBoxHandler) },
					{ typeof(IProgress), typeof(ProgressBarHandler) },
					{ typeof(ISlider), typeof(SliderHandler) },
					{ typeof(IStepper), typeof(StepperHandler) },
					{ typeof(ISwitch), typeof(SwitchHandler) }
				}))
				.UseMauiApp<MyApp>();
		}
	}
}