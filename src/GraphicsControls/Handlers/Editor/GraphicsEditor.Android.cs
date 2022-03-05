﻿using Android.Content;
using Android.Graphics;
using Android.Views;
using AndroidX.AppCompat.Widget;
using Microsoft.Maui.Graphics.Platform;

namespace Microsoft.Maui.Graphics.Controls
{
    public class GraphicsEditor : AppCompatEditText, IMixedPlatformView
    {
        readonly PlatformCanvas _canvas;
        readonly ScalingCanvas _scalingCanvas;
        readonly float _scale;

        bool _pressedContained;
        int _width, _height;
        Color? _backgroundColor;
        IMixedGraphicsHandler? _graphicsControl;
        IDrawable? _drawable;

        public GraphicsEditor(Context context) : base(context)
        {
            _scale = Resources?.DisplayMetrics?.Density ?? 1;
            _canvas = new PlatformCanvas(context);
            _scalingCanvas = new ScalingCanvas(_canvas);

            Background = null;
        }

        public Color? BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                Invalidate();
            }
        }

        public IMixedGraphicsHandler? GraphicsControl
        {
            get => _graphicsControl;
            set => Drawable = _graphicsControl = value;
        }

        public IDrawable? Drawable
        {
            get => _drawable;
            set
            {
                _drawable = value;
                Invalidate();
            }
        }

        static readonly string[] DefaultPlatformLayers = new[] { nameof(IEntry.Text) };

        public string[] PlatformLayers => DefaultPlatformLayers;

        public void DrawBaseLayer(RectF dirtyRect) { }

        public override void Draw(Canvas? androidCanvas)
        {
            if (_drawable == null)
                return;

            var dirtyRect = new RectF(0, 0, _width, _height);

            _canvas.Canvas = androidCanvas;

            if (_backgroundColor != null)
            {
                _canvas.FillColor = _backgroundColor;
                _canvas.FillRectangle(dirtyRect);
                _canvas.FillColor = Colors.White;
            }

            _scalingCanvas.ResetState();
            _scalingCanvas.Scale(_scale, _scale);

            dirtyRect.Height /= _scale;
            dirtyRect.Width /= _scale;
            _drawable.Draw(_scalingCanvas, dirtyRect);
            _canvas.Canvas = null;

            base.Draw(androidCanvas);
        }

        protected override void OnSizeChanged(int width, int height, int oldWidth, int oldHeight)
        {
            base.OnSizeChanged(width, height, oldWidth, oldHeight);
            _width = width;
            _height = height;
        }

        public override bool OnTouchEvent(MotionEvent? e)
        {
            if (e != null && Enabled)
            {
                var density = Resources?.DisplayMetrics?.Density ?? 1.0f;
                var interceptPoint = new Point(e.GetX() / density, e.GetY() / density);

                switch (e.Action)
                {
                    case MotionEventActions.Down:
                        PointF[] downPoints = new PointF[] { interceptPoint };
                        GraphicsControl?.StartInteraction(downPoints);
                        _pressedContained = true;
                        break;
                    case MotionEventActions.Move:
                        PointF[] movePoints = new PointF[] { interceptPoint };
                        _pressedContained = GraphicsControl?.PointsContained(movePoints) ?? false;
                        GraphicsControl?.DragInteraction(movePoints);
                        break;
                    case MotionEventActions.Up:
                        PointF[] upPoints = new PointF[] { interceptPoint };
                        GraphicsControl?.EndInteraction(upPoints, _pressedContained);
                        break;
                    case MotionEventActions.Cancel:
                        _pressedContained = false;
                        GraphicsControl?.CancelInteraction();
                        break;
                }
            }

            return base.OnTouchEvent(e);
        }
    }
}