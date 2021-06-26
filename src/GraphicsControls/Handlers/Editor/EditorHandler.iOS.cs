using System;
using UIKit;

namespace Microsoft.Maui.Graphics.Controls
{
    public partial class EditorHandler : MixedGraphicsControlHandler<IEditorDrawable, IEditor, GraphicsEditor>
	{
		protected override GraphicsEditor CreateNativeView()
        {
            return new GraphicsEditor { EdgeInsets = new UIEdgeInsets(0, 12, 0, 12) };
		}

        protected override void ConnectHandler(GraphicsEditor nativeView)
        {
            base.ConnectHandler(nativeView);

			nativeView.EditingDidBegin += OnEditingDidBegin;
			nativeView.EditingDidEnd += OnEditingEnded;
			nativeView.Ended += OnEnded;
		}

        protected override void DisconnectHandler(GraphicsEditor nativeView)
        {
            base.DisconnectHandler(nativeView);

			nativeView.EditingDidBegin -= OnEditingDidBegin;
			nativeView.EditingDidEnd -= OnEditingEnded;
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

		void OnEditingDidBegin(object? sender, EventArgs e)
		{
			Drawable.HasFocus = true;
			Invalidate();
		}

		void OnEditingEnded(object? sender, EventArgs e)
		{
			Drawable.HasFocus = false;
			Invalidate();
		}

		void OnEnded(object? sender, EventArgs eventArgs)
		{
			if (VirtualView == null || NativeView == null)
				return;

			if (NativeView.Text != VirtualView.Text)
				VirtualView.Text = NativeView.Text ?? string.Empty;

			// TODO: Update IsFocused property

			VirtualView.Completed();
		}
	}
}