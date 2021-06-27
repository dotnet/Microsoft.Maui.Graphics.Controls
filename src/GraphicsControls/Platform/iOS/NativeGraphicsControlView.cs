using System;
using System.Diagnostics;
using CoreGraphics;
using Foundation;
using Microsoft.Maui.Graphics.Native;
using UIKit;

namespace Microsoft.Maui.Graphics.Controls
{
	public class NativeGraphicsControlView : NativeGraphicsView
	{
        IGraphicsHandler? _graphicsControl;
		bool _pressedContained;

		public NativeGraphicsControlView()
		{
			BackgroundColor = UIColor.Clear;
		}

		public IGraphicsHandler? GraphicsControl
		{
			get => _graphicsControl;
			set => Drawable = _graphicsControl = value;
		}
			
		public override void TouchesBegan(NSSet touches, UIEvent? evt)
		{
			try
			{
                if (!IsFirstResponder)
					BecomeFirstResponder();

				var viewPoints = this.GetPointsInView(evt);
				GraphicsControl?.StartInteraction(viewPoints);
				_pressedContained = true;
			}
			catch (Exception exc)
			{
				Debug.WriteLine("An unexpected error occured handling a touch event within the control.", exc);
			}
		}

		public override bool PointInside(CGPoint point, UIEvent? uievent) =>
			(GraphicsControl?.TouchEnabled ?? false) && base.PointInside(point, uievent);

		public override void TouchesMoved(NSSet touches, UIEvent? evt)
		{
			try
			{
				var viewPoints = this.GetPointsInView(evt);
				_pressedContained = GraphicsControl?.PointsContained(viewPoints) ?? false;
				GraphicsControl?.DragInteraction(viewPoints);
			}
			catch (Exception exc)
			{
				Debug.WriteLine("An unexpected error occured handling a touch moved event within the control.", exc);
			}
		}

		public override void TouchesEnded(NSSet touches, UIEvent? evt)
		{
			try
			{
				var viewPoints = this.GetPointsInView(evt);
				GraphicsControl?.EndInteraction(viewPoints, _pressedContained);
			}
			catch (Exception exc)
			{
				Debug.WriteLine("An unexpected error occured handling a touch ended event within the control.", exc);
			}
		}

		public override void TouchesCancelled(NSSet touches, UIEvent? evt)
		{
			try
			{
				_pressedContained = false;
				GraphicsControl?.CancelInteraction();
			}
			catch (Exception exc)
			{
				Debug.WriteLine("An unexpected error occured cancelling the touches within the control.", exc);
			}
		}
	}
}