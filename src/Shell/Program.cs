using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Avalonia;
using Velopack;
using Velopack.Sources;
using Velopack.Windows;

namespace Finanzmanager;

public class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    [ExcludeFromCodeCoverage]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
    {
        VelopackApp
            .Build()
            #if WINDOWS
            .OnAfterInstallFastCallback(v => new Shortcuts().CreateShortcutForThisExe(ShortcutLocation.Desktop))
            #endif
            .Run();

        #if RELEASE
        CheckForUpdatesAsync();
        #endif

        var builder = AppBuilder
            .Configure<App>()
            .UsePlatformDetect()
            .With(new X11PlatformOptions { EnableMultiTouch = true, UseDBusMenu = true, })
            .WithInterFont()
            .UseSkia();


        return builder;
    }

    private static async void CheckForUpdatesAsync()
    {
        var mgr = new UpdateManager(new GithubSource("https://github.com/furesoft/Finanzmanager", null, false));

        var newVersion = await mgr.CheckForUpdatesAsync();
        if (newVersion == null)
            return;

        //ToDo: add ui for downloading updates
        await mgr.DownloadUpdatesAsync(newVersion);

        mgr.ApplyUpdatesAndRestart(newVersion);
    }
}
