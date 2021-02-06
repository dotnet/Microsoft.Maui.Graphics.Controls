using System;
using System.Runtime.InteropServices;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.Tizen;
using ElmSharp;
using System.Graphics.Skia.Views;
using GraphicsControls.Tizen;
using EPoint = ElmSharp.Point;
using GraphicsView = GraphicsControls.GraphicsView;
using Layout = Xamarin.Forms.Layout;
using Point = System.Graphics.Point;
using XForms = Xamarin.Forms.Forms;

[assembly: ExportRenderer(typeof(GraphicsView), typeof(GraphicsViewRenderer))]
namespace GraphicsControls.Tizen
{
    [Preserve(AllMembers = true)]
    public class GraphicsViewRenderer : LayoutRenderer
    {
        EvasObjectEvent<EvasMouseEventArgs> _mouseUp;
        EvasObjectEvent<EvasMouseEventArgs> _mouseDown;
        EvasObjectEvent<EvasMouseEventArgs> _mouseMove;
        SkiaGraphicsView _skiaGraphicsView;
        new GraphicsView Element => base.Element as GraphicsView;

        public GraphicsViewRenderer()
        {
            RegisterPropertyHandler(AutomationProperties.HelpTextProperty, UpdateAutomationHelpText);
            RegisterPropertyHandler(AutomationProperties.NameProperty, UpdateAutomationName);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Layout> e)
        {
            if (e.OldElement != null)
            {
                ((GraphicsView)e.OldElement).Invalidated -= OnDrawInvalidated;
            }

            if (e.NewElement != null)
            {
                ((GraphicsView)e.NewElement).Invalidated += OnDrawInvalidated;

                SetNativeControl(new Xamarin.Forms.Platform.Tizen.Native.Canvas(Forms.NativeParent));

                _skiaGraphicsView = new SkiaGraphicsView(XForms.NativeParent)
                {
                    Drawable = Element
                };

                _skiaGraphicsView.Show();
                _mouseDown = new EvasObjectEvent<EvasMouseEventArgs>(_skiaGraphicsView, EvasObjectCallbackType.MouseDown, EvasMouseEventArgs.Create);
                _mouseUp = new EvasObjectEvent<EvasMouseEventArgs>(_skiaGraphicsView, EvasObjectCallbackType.MouseUp, EvasMouseEventArgs.Create);
                _mouseMove = new EvasObjectEvent<EvasMouseEventArgs>(_skiaGraphicsView, EvasObjectCallbackType.MouseMove, EvasMouseEventArgs.Create);
                _mouseDown.On += OnMouseDown;
                _mouseUp.On += OnMouseUp;
                _mouseMove.On += OnMouseMove;
                Control.Children.Add(_skiaGraphicsView);
                Control.LayoutUpdated += OnLayoutUpdated;
            }
            base.OnElementChanged(e);
        }

        void OnLayoutUpdated(object sender, Xamarin.Forms.Platform.Tizen.Native.LayoutEventArgs e)
        {
            _skiaGraphicsView.Geometry = NativeView.Geometry;
            _skiaGraphicsView.Invalidate();
        }

        private void OnMouseDown(object sender, EvasMouseEventArgs e)
        {
            Element?.OnTouchDown(new Point(Forms.ConvertToScaledDP(e.Point.X), Forms.ConvertToScaledDP(e.Point.Y)));
        }

        private void OnMouseUp(object sender, EvasMouseEventArgs e)
        {
            Element?.OnTouchUp(new Point(Forms.ConvertToScaledDP(e.Point.X), Forms.ConvertToScaledDP(e.Point.Y)));
        }

        private void OnMouseMove(object sender, EvasMouseEventArgs e)
        {
            Element?.OnTouchMove(new Point(Forms.ConvertToScaledDP(e.Point.X), Forms.ConvertToScaledDP(e.Point.Y)));
        }

        protected override void OnElementReady()
        {
            base.OnElementReady();
            Element?.Load();
            Element?.AttachComponents();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Element?.Unload();
                Element?.DetachComponents();

                if (_mouseDown != null)
                {
                    _mouseDown.On -= OnMouseDown;
                    _mouseDown.Dispose();
                }

                if (_mouseUp != null)
                {
                    _mouseUp.On -= OnMouseUp;
                    _mouseUp.Dispose();
                }

                if (_mouseMove != null)
                {
                    _mouseMove.On -= OnMouseMove;
                    _mouseMove.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        void OnDrawInvalidated(object sender, EventArgs e)
        {
            _skiaGraphicsView?.Invalidate();
        }

        void UpdateAutomationHelpText()
        {
            _skiaGraphicsView?.SetAccessibilityDescription(Element);
        }

        void UpdateAutomationName()
        {
            _skiaGraphicsView?.SetAccessibilityName(Element);
        }
    }

    public class EvasMouseEventArgs : EventArgs
    {
        readonly IntPtr _nativeEventInfo;

        public EPoint Point { get; private set; }

        public EPoint CanvasPoint { get; private set; }

        public EvasEventFlag Flags
        {
            get
            {
                IntPtr offset = Marshal.OffsetOf<EvasEventMouseDown>("event_flags");
                return (EvasEventFlag)Marshal.ReadIntPtr(_nativeEventInfo, (int)offset);
            }
            set
            {
                IntPtr offset = Marshal.OffsetOf<EvasEventMouseDown>("event_flags");
                Marshal.WriteIntPtr(_nativeEventInfo, (int)offset, (IntPtr)value);
            }
        }

        EvasMouseEventArgs(IntPtr info)
        {
            _nativeEventInfo = info;
            var evt = Marshal.PtrToStructure<EvasEventMouseDown>(info);
            Point = evt.output;
            CanvasPoint = evt.canvas;
        }

        static public EvasMouseEventArgs Create(IntPtr data, IntPtr obj, IntPtr info)
        {
            return new EvasMouseEventArgs(info);
        }
    }

    [NativeStruct("Evas_Event_Mouse_Down", Include = "Elementary.h", PkgConfig = "elementary")]
    [StructLayout(LayoutKind.Sequential)]
    struct EvasEventMouseDown
    {
        public int button;
        public EPoint output;
        public EPoint canvas;
        public IntPtr data;
        public IntPtr modifiers;
        public IntPtr locks;
        public EvasButtonFlag flags;
        public uint timestamp;
        public EvasEventFlag event_flags;
        public IntPtr dev;
        public IntPtr event_source;
        double radius;
        double radius_x;
        double radius_y;
        double pressure;
        double angle;
    };

    public enum NativeStructArch
    {
        All = 0,
        Only32Bits = 1,
        Only64Bits = 2
    }

    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class, AllowMultiple = true)]
    public class NativeStructAttribute : Attribute
    {
        private readonly string _structName;

        public NativeStructAttribute(string structName)
        {
            _structName = structName;
        }

        public string StructName => _structName;

        public string PkgConfig { get; set; }

        public string Include { get; set; }

        public NativeStructArch Arch { get; set; }
    }

    [Flags]
    public enum EvasButtonFlag
    {
        DoubleClick = 0,
        TripleClick = 1,
    }
}