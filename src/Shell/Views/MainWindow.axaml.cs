using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using FluentAvalonia.UI.Windowing;
using Prism.Ioc;
using StatusBar.Avalonia;

namespace Finanzmanager.Views;

public partial class MainWindow : AppWindow
{
    public MainWindow()
    {
        InitializeComponent();

        SplashScreen = new MainAppSplashScreen(this);
        TitleBar.ExtendsContentIntoTitleBar = true;
        TitleBar.TitleBarHitTestType = TitleBarHitTestType.Complex;

        var statusBarManager = new StatusBarManager();
        statusBarManager.BindContainer(_StatusBarContainer);
        ((App)Application.Current!).ContainerRegistry.RegisterInstance(statusBarManager);

        var status = statusBarManager.CreateStatusBarItem("status");
        status.Show();
        status.Text = "$(sync~spin) Syncing...";

        statusBarManager.SetStatusBarMessage("$(sync~spin) Hello World from StatusBarManager", async () => await Task.Delay(30000));
    }
}
