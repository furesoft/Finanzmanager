using System.Threading.Tasks;
using Avalonia.Controls;
using StatusBar.Avalonia;

namespace Finanzmanager.Views;

/// <summary>Main window view.</summary>
public partial class MainWindow : Window
{
    public StatusBarManager StatusBarManager { get; } = new();

    public MainWindow()
    {
        InitializeComponent();

        StatusBarManager.BindContainer(_StatusBarContainer);

        var status = StatusBarManager.CreateStatusBarItem("status");
        status.Show();
        status.Text = "$(sync~spin) Syncing...";

        StatusBarManager.SetStatusBarMessage("$(sync~spin) Hello World from StatusBarManager", async () => await Task.Delay(30000));
    }
}
