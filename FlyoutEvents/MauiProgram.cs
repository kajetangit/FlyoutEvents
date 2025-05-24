using FlyoutEvents.Pages;
using FlyoutEvents.ViewModels;
using Microsoft.Extensions.Logging;

namespace FlyoutEvents;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UsePrism(
                new DryIocContainerExtension(), prism => prism.CreateWindow(service => service
                    .CreateBuilder()
                    .AddSegment("MainPage")
                    .AddTabbedSegment(s => s
                        .CreateTab(t => t.AddNavigationPage().AddSegment("FirstPage"))
                        .CreateTab(t => t.AddNavigationPage().AddSegment("SecondPage"))
                    )
                )
            )
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.RegisterForNavigation<MainPage, MainViewModel>();
        builder.Services.RegisterForNavigation<FirstPage, FirstViewModel>();
        builder.Services.RegisterForNavigation<SecondPage, SecondViewModel>();

        builder.Services.AddShinyMediator(x => { x.UseMaui(); });
        builder.Services.AddSingletonAsImplementedInterfaces<SecondViewModel>();
        

#if DEBUG
        builder.Logging.AddDebug();
#endif

        // Add global exception handling
        AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
        {
            var exception = e.ExceptionObject as Exception;
            // Log the exception
            Console.WriteLine($"Unhandled exception: {exception?.Message}");
            Console.WriteLine($"Stack trace: {exception?.StackTrace}");
        };

        return builder.Build();
    }
}