﻿namespace Microsoft.Maui.Graphics.Controls
{
    public class ViewDrawable<TVirtualView> : ViewDrawable, IViewDrawable<TVirtualView>
		where TVirtualView : IView
	{
		public TVirtualView VirtualView { get => (TVirtualView)View; set => View = value; }
	}

	public class ViewDrawable : IViewDrawable
	{
		public IView View { get; set; }

		public ControlState CurrentState { get; set; }

		public virtual void DrawBackground(ICanvas canvas, RectF dirtyRect, IView view)
		{
			if (view.Background == null)
				return;

			canvas.SetFillPaint(view.Background, dirtyRect);

			canvas.DrawRectangle(dirtyRect);
		}

		public virtual void DrawClip(ICanvas canvas, RectF dirtyRect, IView view)
		{
		
		}

		public virtual void DrawText(ICanvas canvas, RectF dirtyRect, IText text)
		{
			if (text == null)
				return;

			canvas.FontColor = text.TextColor ?? Colors.Black;
			canvas.Font =  new Font(text.Font.Family,(int)text.Font.Weight);
			canvas.FontSize = (float)text.Font.Size;

			var horizontal =
				((text as ITextAlignment)?.HorizontalTextAlignment ?? TextAlignment.Center) switch
				{
					TextAlignment.Start => HorizontalAlignment.Left,
					TextAlignment.Center => HorizontalAlignment.Center,
					TextAlignment.End => HorizontalAlignment.Right,
					_ => HorizontalAlignment.Center,
				};

			canvas.DrawString((string)text.Text, dirtyRect, horizontalAlignment: horizontal, verticalAlignment: Microsoft.Maui.Graphics.VerticalAlignment.Center);

		}
		public virtual void DrawOverlay(ICanvas canvas, RectF dirtyRect, IView view)
		{

		}

		public virtual void DrawBorder(ICanvas canvas, RectF dirtyRect, IView view)
		{

		}

		public virtual Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
			new Size(100, 44);
	}
}