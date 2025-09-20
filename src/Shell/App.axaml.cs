using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Finanzmanager.Contracts;
using Finanzmanager.Core;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Navigation.Regions;
using Finanzmanager.Services;
using Finanzmanager.ViewModels;
using Finanzmanager.Views;
using StatusBar.Avalonia;

namespace Finanzmanager;

public partial class App : PrismApplication
{
    public IContainerRegistry ContainerRegistry;
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);

        // Required when overriding Initialize
        base.Initialize();
    }

    protected override AvaloniaObject CreateShell()
    {
        return Container.Resolve<MainWindow>();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        ContainerRegistry = containerRegistry;
        containerRegistry.RegisterSingleton<INotificationService, NotificationService>();

        // Views - Region Navigation
        containerRegistry.RegisterForNavigation<DashboardView, DashboardViewModel>();
        containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
        containerRegistry.RegisterForNavigation<SettingsSubView, SettingsSubViewModel>();

        // Dialogs, etc.
    }

    /// <summary>Register optional modules in the catalog.</summary>
    /// <param name="moduleCatalog">Module Catalog.</param>
    protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
    {
        base.ConfigureModuleCatalog(moduleCatalog);
    }

    protected override void OnInitialized()
    {
        // Register Views to the Region it will appear in. Don't register them in the ViewModel.
        var regionManager = Container.Resolve<IRegionManager>();

        regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(DashboardView));
        regionManager.RegisterViewWithRegion(RegionNames.SidebarRegion, typeof(SidebarView));
        regionManager.RegisterViewWithRegion(RegionNames.FooterRegion, typeof(Control));

        ////regionManager.RegisterViewWithRegion(RegionNames.DynamicSettingsListRegion, typeof(Setting1View));
        ////regionManager.RegisterViewWithRegion(RegionNames.DynamicSettingsListRegion, typeof(Setting2View));
    }

    /// <summary>Custom region adapter mappings.</summary>
    /// <param name="regionAdapterMappings">Region Adapters.</param>
    protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
    {
        regionAdapterMappings.RegisterMapping<ContentControl, ContentControlRegionAdapter>();
        regionAdapterMappings.RegisterMapping<StatusBarManager, StatusbarRegionAdapter>();
    }
}
