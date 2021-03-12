using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;

namespace GraphicsControls.Sample.GTK
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            GtkOpenGL.Init();
            Gtk.Application.Init();
            Forms.Init(); 
            GraphicsControls.GTK.GraphicsViewRenderer.Init();
            var app = new App();
            var window = new FormsWindow();
            window.LoadApplication(app);
            window.SetApplicationTitle("GraphicsControls");
            window.Show();
            Gtk.Application.Run();
        }
    }
}