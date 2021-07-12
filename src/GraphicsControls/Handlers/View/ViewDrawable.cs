namespace Microsoft.Maui.Graphics.Controls
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

		public virtual void DrawBackground(ICanvas canvas, RectangleF dirtyRect, IView view)
		{
			if (view.Background == null)
				return;

			canvas.SetFillPaint(view.Background, dirtyRect);

			canvas.DrawRectangle(dirtyRect);
		}

		public virtual void DrawClip(ICanvas canvas, RectangleF dirtyRect, IView view)
		{
		
		}

		public virtual void DrawText(ICanvas canvas, RectangleF dirtyRect, IText text)
		{
			if (text == null)
				return;

			canvas.FontColor = text.TextColor ?? Colors.Black;
			canvas.FontName = text.Font.FontFamily;
			canvas.FontSize = (float)text.Font.FontSize;

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
		public virtual void DrawOverlay(ICanvas canvas, RectangleF dirtyRect, IView view)
		{

		}

		public virtual void DrawBorder(ICanvas canvas, RectangleF dirtyRect, IView view)
		{

		}

		public virtual Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
			new Size(100, 44);
	}
}