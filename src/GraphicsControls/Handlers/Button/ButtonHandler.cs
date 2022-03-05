using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Animations;
using System.Linq;

namespace Microsoft.Maui.Graphics.Controls
{
	public class ButtonHandler : GraphicsControlHandler<IButtonDrawable, IButton>
	{
        readonly DrawableType _drawableType;
		IAnimationManager? _animationManager;

		public ButtonHandler() : base(DrawMapper, PropertyMapper)
		{

		}

		public ButtonHandler(DrawableType drawableType) : base(DrawMapper, PropertyMapper)
		{
			_drawableType = drawableType;
		}

		public static PropertyMapper<IButton> PropertyMapper = new PropertyMapper<IButton>(ViewHandler.Mapper)
		{
			[nameof(IButton.Background)] = ViewHandler.MapInvalidate,
			[nameof(IText.Text)] = ViewHandler.MapInvalidate,
			[nameof(ITextStyle.TextColor)] = ViewHandler.MapInvalidate,
			[nameof(ITextStyle.Font)] = ViewHandler.MapInvalidate,
			[nameof(ITextStyle.CharacterSpacing)] = ViewHandler.MapInvalidate,
			[nameof(IButton.Padding)] = ViewHandler.MapInvalidate
		};

		public static DrawMapper<IButtonDrawable, IButton> DrawMapper = new DrawMapper<IButtonDrawable, IButton>(ViewHandler.DrawMapper)
		{
			["Background"] = MapDrawBackground,
			["Text"] = MapDrawText
		};

		public static string[] DefaultButtonLayerDrawingOrder =
			ViewHandler.DefaultLayerDrawingOrder.ToList().InsertAfter(new string[]
			{
				"Background",
				"Text",
			}, "Text").ToArray();

		public override string[] LayerDrawingOrder() =>
			DefaultButtonLayerDrawingOrder;

		protected override IButtonDrawable CreateDrawable()
		{
			switch (_drawableType)
			{
				default:
				case DrawableType.Material:
					return new MaterialButtonDrawable();
				case DrawableType.Cupertino:
					return new CupertinoButtonDrawable();
				case DrawableType.Fluent:
					return new FluentButtonDrawable();
			}
		}

		public static void MapDrawBackground(ICanvas canvas, RectF dirtyRect, IButtonDrawable drawable, IButton view)
			=> drawable.DrawBackground(canvas, dirtyRect, view);

		public static void MapDrawText(ICanvas canvas, RectF dirtyRect, IButtonDrawable drawable, IButton view)
			=> drawable.DrawText(canvas, dirtyRect, view);

        public override bool StartInteraction(PointF[] points)
        {
			Drawable.TouchPoint = points[0];

			if (VirtualView != null)
			{
				AnimateRippleEffect();

				VirtualView.Pressed();
				VirtualView.Clicked();
			}

			return base.StartInteraction(points);
        }

        public override void EndInteraction(PointF[] points, bool inside)
        {
			if(inside && VirtualView != null)
            {
				VirtualView.Released();
            }

            base.EndInteraction(points, inside);
        }

		internal void AnimateRippleEffect()
		{
			if (!(Drawable is MaterialButtonDrawable))
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