using System;
using Avalonia;
using Avalonia.ReactiveUI;
using Projektanker.Icons.Avalonia;
using Projektanker.Icons.Avalonia.FontAwesome;
using PurpleExplorer.Helpers;

namespace PurpleExplorer;

static class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    public static void Main(string[] args)
    {
        // Global exception handling
        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        // Log the exception
        BaseHelper.LogException(e.ExceptionObject as Exception);

        if (e.IsTerminating)
        {
            // Prevent the application from closing
            BaseHelper.ShowErrorMessage("An unexpected error occurred. The application will now close.");
        }
        else
        {
            // Show a message but don't close the app
            BaseHelper.ShowErrorMessage("An unexpected error occurred, but the application will attempt to continue running.");
        }
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace()
            .UseReactiveUI()
            .WithIcons(container => container
                .Register<FontAwesomeIconProvider>());
}
