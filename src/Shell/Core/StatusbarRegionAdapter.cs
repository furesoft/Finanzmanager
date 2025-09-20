using System;
using System.Collections.Specialized;
using Finanzmanager.Views;
using Prism.Ioc;
using Prism.Navigation.Regions;
using StatusBar.Avalonia;
using StatusBar.Avalonia.Controls;

namespace Finanzmanager.Core;

using Prism.Navigation.Regions;

public class StatusbarRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory, IContainerProvider containerProvider)
    : RegionAdapterBase<StatusBarContainer>(regionBehaviorFactory)
{
    protected override void Adapt(IRegion region, StatusBarContainer regionTarget)
    {
        var statusbarManager = containerProvider.Resolve<StatusBarManager>();

        region.Views.CollectionChanged += (s, e) =>
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (StatusBarItem element in e.NewItems!)
                {
                    var item = statusbarManager.CreateStatusBarItem(element.Id, element.Alignment, element.Priority);
                    item.Click = element.Click;
                    item.Text = element.Text;
                    item.Show();
                }
            }
        };
    }

    protected override IRegion CreateRegion()
    {
        return new AllActiveRegion();
    }
}
