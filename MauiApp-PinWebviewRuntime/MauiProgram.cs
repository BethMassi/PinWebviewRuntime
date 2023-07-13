using Microsoft.Extensions.Logging;
using MauiApp_PinWebviewRuntime.Data;

namespace MauiApp_PinWebviewRuntime;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
#if WINDOWS
        string relativePath = @"..\Runtime";
        string basePath = AppContext.BaseDirectory;
        string wvrPath = Path.Combine(basePath, relativePath);        
        Environment.SetEnvironmentVariable("WEBVIEW2_BROWSER_EXECUTABLE_FOLDER", wvrPath);
#endif

        var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		builder.Services.AddSingleton<WeatherForecastService>();

		return builder.Build();
	}
}
