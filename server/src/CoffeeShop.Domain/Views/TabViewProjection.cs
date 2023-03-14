using Baseline;
using CoffeeShop.Domain.Events;
using Marten.Events.Aggregation;
using Marten.Events.Projections;

namespace CoffeeShop.Domain.Views
{
    public class TabViewProjection : SingleStreamAggregation<TabView>
    {
        public TabViewProjection()
        {
            ProjectEvent<TabOpened>((view, @event) => view.Apply(@event));
            ProjectEvent<TabClosed>((view, @event) => view.Apply(@event));
            ProjectEvent<MenuItemsOrdered>((view, @event) => view.Apply(@event));
            ProjectEvent<MenuItemsServed>((view, @event) => view.Apply(@event));
            ProjectEvent<MenuItemsRejected>((view, @event) => view.Apply(@event));
        }
    }
}