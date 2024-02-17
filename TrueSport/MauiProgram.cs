using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace TrueSport;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .RegisterViewModels()
            .RegisterAppServices()
            .RegisterPages()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<DashboardViewModel>();
        mauiAppBuilder.Services.AddTransient<GameViewModel>();

        return mauiAppBuilder;
    }

    static MauiAppBuilder RegisterPages(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<AppShell>();
        mauiAppBuilder.Services.AddSingleton<DashboardPage>();
        mauiAppBuilder.Services.AddTransient<GamePage>();

        return mauiAppBuilder;
    }

    static MauiAppBuilder RegisterAppServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<IDashboardService, DashboardService>();
        mauiAppBuilder.Services.AddSingleton<IGameService, GameService>();
        mauiAppBuilder.Services.AddSingleton<IFriendService, FriendService>();

        return mauiAppBuilder;
    }
}