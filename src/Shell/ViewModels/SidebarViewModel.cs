using Finanzmanager.Views;
using Prism.Commands;
using Prism.Navigation.Regions;

namespace Finanzmanager.ViewModels;

public class SidebarViewModel : ViewModelBase
{
    private readonly IRegionManager _regionManager;

    public SidebarViewModel(IRegionManager regionManager)
    {
        _regionManager = regionManager;
        Title = "Navigation";
    }

    public DelegateCommand CmdDashboard => new(() =>
    {
        _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(DashboardView));
    });

    public DelegateCommand CmdSettings => new(() => _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(SettingsView)));
}
