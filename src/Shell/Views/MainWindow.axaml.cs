using System.Threading.Tasks;
using Avalonia.Controls;
using FluentAvalonia.UI.Windowing;
using StatusBar.Avalonia;

namespace Finanzmanager.Views;

public partial class MainWindow : AppWindow
{
    public StatusBarManager StatusBarManager { get; } = new();

    public MainWindow()
    {
        InitializeComponent();

        SplashScreen = new TestSplashScreen();
        TitleBar.ExtendsContentIntoTitleBar = true;

        StatusBarManager.BindContainer(_StatusBarContainer);

        var status = StatusBarManager.CreateStatusBarItem("status");
        status.Show();
        status.Text = "$(sync~spin) Syncing...";

        StatusBarManager.SetStatusBarMessage("$(sync~spin) Hello World from StatusBarManager", async () => await Task.Delay(30000));
    }
}
