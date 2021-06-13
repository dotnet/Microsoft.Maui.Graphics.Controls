#nullable enable
using System;
using System.Linq;
using Microsoft.Maui.Handlers;
#if __IOS__ || MACCATALYST
using NativeView = UIKit.UIView;
#elif MONOANDROID
using NativeView = Android.Views.View;
#elif WINDOWS
using NativeView = Microsoft.UI.Xaml.FrameworkElement;
#elif NETSTANDARD || (NET6_0 && !IOS && !ANDROID)
using NativeView = System.Object;
#endif

namespace Microsoft.Maui.Graphics.Controls
{
	public abstract partial class MixedGraphicsControlHandler<TViewDrawable, TVirtualView, TNativeView> : ViewHandler<TVirtualView, TNativeView>, IViewHandler, IMixedGraphicsHandler
		where TVirtualView : class, IView
		where TViewDrawable : class, IViewDrawable
#if !NETSTANDARD || IOS || ANDROID || WINDOWS
		where TNativeView : NativeView, IMixedNativeView
#else
		where TNativeView : class
#endif
	{
		TViewDrawable? _drawable;
		protected readonly DrawMapper _drawMapper;
		ControlState _currentState = ControlState.Default;

		protected MixedGraphicsControlHandler() : base(ViewHandler.Mapper)
		{
			_drawMapper = ViewHandler.DrawMapper;
		}

		protected MixedGraphicsControlHandler(DrawMapper? drawMapper, PropertyMapper mapper) : base(mapper ?? ViewHandler.Mapper)
		{
			_drawMapper = drawMapper ?? new DrawMapper<TViewDrawable, TVirtualView>(ViewHandler.DrawMapper);
		}

		DrawMapper IMixedGraphicsHandler.DrawMapper => _drawMapper;

		public RectangleF Bounds { get; private set; }

		public bool TouchEnabled { get; set; } = true;

		protected PointF CurrentTouchPoint { get; set; }

		public bool PointsContained(PointF[] points) => points.Any(p => Bounds.BoundsContains(p));

		public ControlState CurrentState
		{
			get => VirtualView!.IsEnabled ? _currentState : ControlState.Disabled;
			set
			{
				if (_currentState == value)
					return;

				_currentState = value;
				Drawable.CurrentState = value;

				ControlStateChanged();
				Invalidate();
			}
		}

		protected TViewDrawable Drawable
		{
			get => _drawable ??= CreateDrawable();
			set => _drawable = value;
		}

		protected abstract TViewDrawable CreateDrawable();

		public virtual void StartHoverInteraction(PointF[] points)
		{
			CurrentTouchPoint = points.FirstOrDefault();
			CurrentState = ControlState.Hovered;
		}

		public virtual void HoverInteraction(PointF[] points)
		{
		}

		public virtual void EndHoverInteraction()
		{
		}

		public virtual bool StartInteraction(PointF[] points)
		{
			CurrentTouchPoint = points.FirstOrDefault();
			CurrentState = ControlState.Pressed;

			return true;
		}

		public virtual void DragInteraction(PointF[] points)
		{
			CurrentTouchPoint = points.FirstOrDefault();
		}

		public virtual void EndInteraction(PointF[] points, bool inside)
		{
			CurrentState = ControlState.Default;
		}

		public virtual void CancelInteraction()
		{
			CurrentState = ControlState.Default;
		}

		protected virtual void ControlStateChanged()
		{
		}

		public void Invalidate()
		{
			if (NativeView is IInvalidatable invalidatableView)
				invalidatableView?.Invalidate();
		}

		public virtual void Resized(RectangleF bounds)
		{
			Bounds = bounds;
		}

		public override Size GetDesiredSize(double widthConstraint, double heightConstraint) =>
			Drawable.GetDesiredSize(VirtualView!, widthConstraint, heightConstraint);

		public override void SetVirtualView(IView view)
		{
			base.SetVirtualView(view);

			Drawable.View = VirtualView!;
			if (NativeView is IMixedNativeView mnv)
				mnv.Drawable = this;

			Invalidate();
		}

		public virtual void Draw(ICanvas canvas, RectangleF dirtyRect)
		{
			if (VirtualView == null || _drawMapper == null)
				return;

			canvas.SaveState();

			var layers = LayerDrawingOrder();
			var rect = dirtyRect;
			bool hasDrawnBase = false;
			var mixedNativeView = NativeView as IMixedNativeView;
			var nativeLayers = mixedNativeView?.NativeLayers;

			foreach (var layer in layers)
			{
				//This will allow the native layer to handle the layers it can,
				//i.e: For Entry, the Text layer and Caret is handled by the base drawing.
				if (nativeLayers.Contains(layer))
				{
					if (hasDrawnBase)
						continue;

					hasDrawnBase = true;
					mixedNativeView?.DrawBaseLayer(dirtyRect);
				}
				else
					_drawMapper?.DrawLayer(canvas, rect, Drawable, VirtualView, layer);
			}

			canvas.ResetState();
		}

		public abstract string[] LayerDrawingOrder();
	}
}