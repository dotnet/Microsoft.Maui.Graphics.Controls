using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;
using Xamarin.Forms.Platform.GTK.Extensions;

namespace GraphicsControls.Sample.GTK
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Gtk.Application.Init();
            Forms.Init();
            
            var app = new App();
            var window = new FormsWindow();
            window.LoadApplication(app);
            window.SetApplicationTitle("GraphicsControls");
            //window.SetSize(800, 1000);
            window.Show();

            Gtk.Application.Run();

            GraphicsControls.GTK.GraphicsViewRenderer.Init();
        }
    }
}