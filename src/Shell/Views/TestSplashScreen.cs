using System.Threading;
using System.Threading.Tasks;
using Avalonia.Media;
using FluentAvalonia.UI.Windowing;

namespace Finanzmanager.Views;

public class TestSplashScreen : IApplicationSplashScreen
{
    public TestSplashScreen()
    {
        SplashScreenContent = "loading...";
    }
    
    public async Task RunTasks(CancellationToken cancellationToken)
    {
        await Task.Delay(3000);
    }

    public string AppName { get; }
    public IImage AppIcon { get; }
    public object SplashScreenContent { get; }
    public int MinimumShowTime { get; }
}
