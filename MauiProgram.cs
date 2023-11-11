using Esri.ArcGISRuntime.Maui;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace CostaDeliveryApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
#if __ANDROID__
        Esri.ArcGISRuntime.UI.Controls.SceneView.MemoryLimit = 2 * 1073741824L; // 2Gb
#endif
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseSkiaSharp(true)
                .ConfigureFonts(fonts =>
                  {
                      fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                      fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                      fonts.AddFont("FontAwesomeSolid.otf", "AwesomeSolid");
                      fonts.AddFont("latoblack.TTF", "black");
                      fonts.AddFont("latobold.TTF", "bold");
                      fonts.AddFont("latoitalic.TTF", "italic");
                      fonts.AddFont("latoregular.TTF", "regular");
                  })
                .UseMauiMaps()
                .UseArcGISRuntime();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}