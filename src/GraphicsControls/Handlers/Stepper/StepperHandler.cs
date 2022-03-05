using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Animations;
using System.Linq;

namespace Microsoft.Maui.Graphics.Controls
{
    public class StepperHandler : GraphicsControlHandler<IStepperDrawable, IStepper>
    {
		readonly DrawableType _drawableType;
		IAnimationManager? _animationManager;

		public StepperHandler() : base(DrawMapper, PropertyMapper)
		{

		}

		public StepperHandler(DrawableType drawableType) : base(DrawMapper, PropertyMapper)
		{
			_drawableType = drawableType;
		}

		public static PropertyMapper<IStepper> PropertyMapper = new PropertyMapper<IStepper>(ViewHandler.Mapper)
		{
			[nameof(IStepper.Minimum)] = ViewHandler.MapInvalidate,
			[nameof(IStepper.Maximum)] = ViewHandler.MapInvalidate,
			[nameof(IStepper.Interval)] = ViewHandler.MapInvalidate,
			[nameof(IStepper.Value)] = ViewHandler.MapInvalidate
		};

		public static DrawMapper<IStepperDrawable, IStepper> DrawMapper = new DrawMapper<IStepperDrawable, IStepper>(ViewHandler.DrawMapper)
		{
			["Background"] = MapDrawBackground,
			["Separator"] = MapDrawSeparator,
			["Minus"] = MapDrawMinus,
			["Plus"] = MapDrawPlus,
			["Text"] = MapDrawText
		};

		public static string[] DefaultStepperLayerDrawingOrder =
			ViewHandler.DefaultLayerDrawingOrder.ToList().InsertAfter(new string[]
			{
				"Background",
				"Separator",
				"Minus",
				"Plus",
				"Text",
			}, "Text").ToArray();

		public override string[] LayerDrawingOrder() =>
			DefaultStepperLayerDrawingOrder;

		protected override IStepperDrawable CreateDrawable()
		{
			switch (_drawableType)
			{
				default:
				case DrawableType.Material:
					return new MaterialStepperDrawable();
				case DrawableType.Cupertino:
					return new CupertinoStepperDrawable();
				case DrawableType.Fluent:
					return new FluentStepperDrawable();
			}
		}

		public static void MapDrawBackground(ICanvas canvas, RectF dirtyRect, IStepperDrawable drawable, IStepper view)
			=> drawable.DrawBackground(canvas, dirtyRect, view);

		public static void MapDrawSeparator(ICanvas canvas, RectF dirtyRect, IStepperDrawable drawable, IStepper view)
			=> drawable.DrawSeparator(canvas, dirtyRect, view);

		public static void MapDrawMinus(ICanvas canvas, RectF dirtyRect, IStepperDrawable drawable, IStepper view)
			=> drawable.DrawMinus(canvas, dirtyRect, view);

		public static void MapDrawPlus(ICanvas canvas, RectF dirtyRect, IStepperDrawable drawable, IStepper view)
			=> drawable.DrawPlus(canvas, dirtyRect, view);

		public static void MapDrawText(ICanvas canvas, RectF dirtyRect, IStepperDrawable drawable, IStepper view)
			=> drawable.DrawText(canvas, dirtyRect, view);

		public override bool StartInteraction(PointF[] points)
		{
			if (VirtualView == null || !VirtualView.IsEnabled)
				return false;

			var point = points[0];

			Drawable.TouchPoint = point;

			if (Drawable.MinusRectangle.Contains(point))
			{
				AnimateRippleEffect();
				VirtualView.Value -= VirtualView.Interval;
			}

			if (Drawable.PlusRectangle.Contains(point))
			{
				AnimateRippleEffect();
				VirtualView.Value += VirtualView.Interval;
			}

			return base.StartInteraction(points);
		}

		internal void AnimateRippleEffect()
		{
			if (!(Drawable is MaterialStepperDrawable))
				return;

			if (_animationManager == null)
				_animationManager = MauiContext?.Services.GetRequiredService<IAnimationManager>();

			float start = 0;
			float end = 1;

			_animationManager?.Add(new Animation(callback: (progress) =>
			{
				Drawable.AnimationPercent = start.Lerp(end, progress);
				Invalidate();
			}, duration: 0.3, easing: Easing.SinInOut,
			finished: () =>
			{
				Drawable.AnimationPercent = 0;
			}));
		}
	}
}