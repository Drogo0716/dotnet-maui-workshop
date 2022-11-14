using MonkeyFinder.View;
using MonkeyFinder.Services;
using MonkeyFinder.ViewModel;

namespace MonkeyFinder;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		// Best to only loade these first 3 dependencies when demanded (Delay loading) 
		builder.Services.AddSingleton<IConnectivity>((c) => Connectivity.Current);
		builder.Services.AddSingleton<IGeolocation>((c) => Geolocation.Default);
		builder.Services.AddSingleton<IMap>((c) => Map.Default);

		builder.Services.AddSingleton<MonkeyService>();
        builder.Services.AddSingleton<MonkeysViewModel>();
        builder.Services.AddSingleton<MainPage>();
		
		builder.Services.AddTransient<MonkeyDetailsViewModel>();
		builder.Services.AddTransient<DetailsPage>();

		return builder.Build();
	}
}
