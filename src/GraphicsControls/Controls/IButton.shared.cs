using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace GraphicsControls
{
	public interface IButton
	{
		ICommand Command { get; }
		object CommandParameter { get; }
		bool IsPressed { get; }

		void PropagateUpClicked();
		void PropagateUpPressed();
		void PropagateUpReleased();
		void SetIsPressed(bool isPressed);
		void OnCommandCanExecuteChanged(object sender, EventArgs e);
		bool IsEnabledCore { set; }
	}

	public static class ButtonElement
	{
		public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(IButton.Command), typeof(ICommand), typeof(IButton), null, propertyChanging: OnCommandChanging, propertyChanged: OnCommandChanged);

		public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(IButton.CommandParameter), typeof(object), typeof(IButton), null,
			propertyChanged: (bindable, oldvalue, newvalue) => CommandCanExecuteChanged(bindable, EventArgs.Empty));

		static void OnCommandChanged(BindableObject bo, object o, object n)
		{
			IButton button = (IButton)bo;
			if (n is ICommand newCommand)
				newCommand.CanExecuteChanged += button.OnCommandCanExecuteChanged;

			CommandChanged(button);
		}

		static void OnCommandChanging(BindableObject bo, object o, object n)
		{
			IButton button = (IButton)bo;
			if (o != null)
			{
				(o as ICommand).CanExecuteChanged -= button.OnCommandCanExecuteChanged;
			}
		}

		public const string PressedVisualState = "Pressed";

		public static void CommandChanged(IButton sender)
		{
			if (sender.Command != null)
			{
				CommandCanExecuteChanged(sender, EventArgs.Empty);
			}
			else
			{
				sender.IsEnabledCore = true;
			}
		}

		public static void CommandCanExecuteChanged(object sender, EventArgs e)
		{
			IButton ButtonElementManager = (IButton)sender;
			ICommand cmd = ButtonElementManager.Command;
			if (cmd != null)
			{
				ButtonElementManager.IsEnabledCore = cmd.CanExecute(ButtonElementManager.CommandParameter);
			}
		}

		public static void ElementClicked(Xamarin.Forms.VisualElement visualElement, IButton ButtonElementManager)
		{
			if (visualElement.IsEnabled == true)
			{
				ButtonElementManager.Command?.Execute(ButtonElementManager.CommandParameter);
				ButtonElementManager.PropagateUpClicked();
			}
		}

		public static void ElementPressed(Xamarin.Forms.VisualElement visualElement, IButton ButtonElementManager)
		{
			if (visualElement.IsEnabled == true)
			{
				ButtonElementManager.SetIsPressed(true);
				ButtonElementManager.PropagateUpPressed();
			}
		}

		public static void ElementReleased(Xamarin.Forms.VisualElement visualElement, IButton ButtonElementManager)
		{
			if (visualElement.IsEnabled == true)
			{
				ButtonElementManager.SetIsPressed(false);
				ButtonElementManager.PropagateUpReleased();
			}
		}
	}
}