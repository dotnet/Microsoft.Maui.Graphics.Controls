using Microsoft.Maui.Controls;
using Microsoft.Maui.Hosting;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.LifecycleEvents;

#if __ANDROID__ || __IOS__ || MACCATALYST
using Microsoft.Maui.Graphics.Native;
#elif WINDOWS
using Microsoft.Maui.Graphics.Win2D;
#endif

namespace Microsoft.Maui.Graphics.Controls.Hosting
{
    public static class AppHostBuilderExtensions
    {
        public static MauiAppBuilder ConfigureGraphicsControls(this MauiAppBuilder builder, DrawableType drawableType = DrawableType.Material)
        {
#if __ANDROID__ || __IOS__ || __MACCATALYST__
            GraphicsPlatform.RegisterGlobalService(NativeGraphicsService.Instance);
#elif WINDOWS
            GraphicsPlatform.RegisterGlobalService(W2DGraphicsService.Instance);
#endif

            builder
                .ConfigureMauiHandlers(handlers =>
                {
                    handlers.AddGraphicsControlsHandlers(drawableType);
                })
                .ConfigureLifecycleEvents(lifecycle =>
                {
#if WINDOWS
					lifecycle
						.AddWindows(windows => windows
							.OnLaunched((app, e) =>
						{	
                            var dictionaries = UI.Xaml.Application.Current?.Resources?.MergedDictionaries;

				            if (dictionaries != null)
				            {
                                // TODO: Fix issue having Windows XAML files under the Windows Platform folder
                       	        //ResourceDictionaryExtensions.AddResources("GraphicsControlsResources", "ms-appx:///Microsoft.Maui.Graphics.Controls/Platform/Windows/Styles/Resources.xbf");
                            }
                        }));
#endif
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