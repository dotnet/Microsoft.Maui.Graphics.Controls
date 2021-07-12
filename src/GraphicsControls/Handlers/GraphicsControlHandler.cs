using System.Linq;

namespace Microsoft.Maui.Graphics.Controls
{
	public abstract partial class GraphicsControlHandler<TViewDrawable, TVirtualView> : IGraphicsHandler
		where TVirtualView : class, IView
		where TViewDrawable : class, IViewDrawable
	{
		TViewDrawable? _drawable;
		protected readonly DrawMapper _drawMapper;
		ControlState _currentState = ControlState.Default;

		protected GraphicsControlHandler() : base(ViewHandler.Mapper)
		{
			_drawMapper = ViewHandler.DrawMapper;
		}

		protected GraphicsControlHandler(DrawMapper? drawMapper, PropertyMapper mapper) : base(mapper ?? ViewHandler.Mapper)
		{
			_drawMapper = drawMapper ?? new DrawMapper<TViewDrawable, TVirtualView>(ViewHandler.DrawMapper);
		}

		DrawMapper IGraphicsHandler.DrawMapper => _drawMapper;

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
			Invalidate();
		}

		public virtual void Draw(ICanvas canvas, RectangleF dirtyRect)
		{
			if (VirtualView == null || _drawMapper == null)
				return;

			canvas.SaveState();

			var layers = LayerDrawingOrder();
			var rect = dirtyRect;

			foreach (var layer in layers)
			{
				_drawMapper?.DrawLayer(canvas, rect, Drawable, VirtualView, layer);
			}

			canvas.ResetState();
		}

		public abstract string[] LayerDrawingOrder();
	}
}