#nullable enable
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Animations;
using System.Linq;

namespace Microsoft.Maui.Graphics.Controls
{
    public class SwitchHandler : GraphicsControlHandler<ISwitchDrawable, ISwitch>
    {
        readonly DrawableType _drawableType;
        bool _hasSetState;
        IAnimationManager? _animationManager;

        public SwitchHandler() : base(DrawMapper, PropertyMapper)
        {

        }

        public SwitchHandler(DrawableType drawableType) : base(DrawMapper, PropertyMapper)
        {
            _drawableType = drawableType;
        }

        public static PropertyMapper<ISwitch, SwitchHandler> PropertyMapper = new PropertyMapper<ISwitch, SwitchHandler>(ViewHandler.Mapper)
        {
            [nameof(ISwitch.IsOn)] = MapIsOn,
            [nameof(ISwitch.TrackColor)] = ViewHandler.MapInvalidate,
            [nameof(ISwitch.ThumbColor)] = ViewHandler.MapInvalidate
        };

        public static DrawMapper<ISwitchDrawable, ISwitch> DrawMapper = new DrawMapper<ISwitchDrawable, ISwitch>(ViewHandler.DrawMapper)
        {
            ["Background"] = MapDrawBackground,
            ["Thumb"] = MapDrawThumb
        };

        public static string[] DefaultSwitchLayerDrawingOrder =
            ViewHandler.DefaultLayerDrawingOrder.ToList().InsertAfter(new string[]
            {
                "Background",
                "Thumb"
            }, "Text").ToArray();

        public override string[] LayerDrawingOrder() =>
            DefaultSwitchLayerDrawingOrder;

        protected override ISwitchDrawable CreateDrawable()
        {
            switch (_drawableType)
            {
                default:
                case DrawableType.Material:
                    return new MaterialSwitchDrawable();
                case DrawableType.Cupertino:
                    return new CupertinoSwitchDrawable();
                case DrawableType.Fluent:
                    return new FluentSwitchDrawable();
            }
        }

        public static void MapDrawBackground(ICanvas canvas, RectangleF dirtyRect, ISwitchDrawable drawable, ISwitch view)
            => drawable.DrawBackground(canvas, dirtyRect, view);

        public static void MapDrawThumb(ICanvas canvas, RectangleF dirtyRect, ISwitchDrawable drawable, ISwitch view)
            => drawable.DrawThumb(canvas, dirtyRect, view);

        public override bool StartInteraction(PointF[] points)
        {
            if (VirtualView != null)
            {
                VirtualView.IsOn = !VirtualView.IsOn;
                AnimateToggle();
            }

            return base.StartInteraction(points);
        }

        public static void MapIsOn(IElementHandler handler, ISwitch virtualView)
        {
            (handler as SwitchHandler)?.UpdateIsOn();
        }

        void UpdateIsOn()
        {
            if (!_hasSetState)
            {
                _hasSetState = true;
                Drawable.AnimationPercent = VirtualView.IsOn ? 1 : 0;
                Invalidate();
            }
            else
                AnimateToggle();           
        }

        internal void AnimateToggle()
        {
            if (_animationManager == null)
                _animationManager = MauiContext?.Services.GetRequiredService<IAnimationManager>();

            float start = VirtualView.IsOn ? 0 : 1;
            float end = VirtualView.IsOn ? 1 : 0;

            _animationManager?.Add(new Animation(callback: (progress) =>
            {
                Drawable.AnimationPercent = start.Lerp(end, progress);
                Invalidate();
            }, duration: 0.1, easing: Easing.Linear));
        }
    }
}