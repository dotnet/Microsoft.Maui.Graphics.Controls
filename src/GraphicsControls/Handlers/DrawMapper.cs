using System;
using System.Collections.Generic;

namespace Microsoft.Maui.Graphics.Controls
{
    public class DrawMapper
	{
		internal Dictionary<string, Action<ICanvas, RectF, IViewDrawable, IView>> genericMap = new Dictionary<string, Action<ICanvas, RectF, IViewDrawable, IView>>();

		protected bool DrawLayer(string key, ICanvas canvas, RectF dirtyRect, IViewDrawable drawable, IView virtualView)
		{
			var action = Get(key);

			if (action == null)
				return false;

			action.Invoke(canvas, dirtyRect, drawable, virtualView);

			return true;
		}

		public bool DrawLayer(ICanvas canvas, RectF dirtyRect, IViewDrawable drawable, IView virtualView, string property)
		{
			if (virtualView == null)
				return false;

			return DrawLayer(property, canvas, dirtyRect, drawable, virtualView);
		}

		public virtual Action<ICanvas, RectF, IViewDrawable, IView> Get(string key)
		{
			genericMap.TryGetValue(key, out var action);
			return action;
		}
	}

	public class DrawMapper<TViewDrawable, TVirtualView> : DrawMapper
		where TVirtualView : IView
		where TViewDrawable : IViewDrawable
	{
		public DrawMapper()
		{

		}

		public DrawMapper(DrawMapper chained)
		{
			Chained = chained;
		}

		public DrawMapper? Chained { get; set; }

		public Action<ICanvas, RectF, TViewDrawable, TVirtualView> this[string key]
		{
			set => genericMap[key] = (canvas, rect, drawable, virtualView) =>
			value?.Invoke(canvas, rect, (TViewDrawable)drawable, (TVirtualView)virtualView);
		}

		public override Action<ICanvas, RectF, IViewDrawable, IView>? Get(string key)
		{
			if (genericMap.TryGetValue(key, out var action))
				return action;
			else
				return Chained?.Get(key) ?? null;
		}
	}
}