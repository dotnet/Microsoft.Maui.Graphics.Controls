using System;
using UIKit;

namespace Microsoft.Maui.Graphics.Controls
{
    public partial class EditorHandler : MixedGraphicsControlHandler<IEditorDrawable, IEditor, GraphicsEditor>
	{
		protected override GraphicsEditor CreateNativeView()
		{
			UIEdgeInsets edgeInsets;

			if (Drawable is MaterialEditorDrawable)
				edgeInsets = new UIEdgeInsets(24, 8, 0, 8);
			else if (Drawable is FluentEditorDrawable)
				edgeInsets = new UIEdgeInsets(9, 6, 0, 6);
			else if (Drawable is CupertinoEditorDrawable)
				edgeInsets = new UIEdgeInsets(8, 4, 0, 4);
			else
				edgeInsets = new UIEdgeInsets();

			return new GraphicsEditor { GraphicsControl = this, EdgeInsets = edgeInsets };
		}

        protected override void ConnectHandler(GraphicsEditor nativeView)
        {
            base.ConnectHandler(nativeView);

            nativeView.Started += OnStarted;
			nativeView.Ended += OnEnded;
		}

		protected override void DisconnectHandler(GraphicsEditor nativeView)
        {
            base.DisconnectHandler(nativeView);

			nativeView.Started -= OnStarted;
			nativeView.Ended -= OnEnded;
		}

        public static void MapText(EditorHandler handler, IEditor editor)
		{
			handler.NativeView?.UpdateText(editor);
			(handler as IMixedGraphicsHandler)?.Invalidate();
		}

		public static void MapTextColor(EditorHandler handler, IEditor editor)
		{
			handler.NativeView?.UpdateTextColor(editor);
		}

		public static void MapCharacterSpacing(EditorHandler handler, IEditor editor)
		{
			handler.NativeView?.UpdateCharacterSpacing(editor);
		}

		[MissingMapper]
		public static void MapFont(EditorHandler handler, IEditor editor) { }

		[MissingMapper]
		public static void MapIsReadOnly(EditorHandler handler, IEditor editor) { }

		[MissingMapper]
		public static void MapIsTextPredictionEnabled(EditorHandler handler, IEditor editor) { }

		[MissingMapper]
		public static void MapMaxLength(EditorHandler handler, IEditor editor) { }

		[MissingMapper]
		public static void MapKeyboard(EditorHandler handler, IEditor editor) { }

		void OnStarted(object sender, EventArgs e)
		{
			Drawable.HasFocus = true;
			Invalidate();
		}

		void OnEnded(object? sender, EventArgs eventArgs)
		{
			Drawable.HasFocus = false;
			Invalidate();

			if (VirtualView == null || NativeView == null)
				return;

			if (NativeView.Text != VirtualView.Text)
				VirtualView.Text = NativeView.Text ?? string.Empty;

			// TODO: Update IsFocused property

			VirtualView.Completed();
		}
	}
}