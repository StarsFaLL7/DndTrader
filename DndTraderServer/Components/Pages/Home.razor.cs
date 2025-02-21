using Domain.DataQuery;
using Domain.Entities;
using Domain.Entities.Connections;

namespace DndTraderServer.Components.Pages;

public partial class Home
{
    private async Task LoadPlayersAsync()
    {
        var queryParams = new DataQueryParams<Player>
        {
            IncludeParams = new IncludeParams<Player>
            {
                IncludeProperties = [p => p.Category]
            }
        };
        var result = await PlayersService.GetAsync(queryParams);
        _players =  result.ToList();
    }

    private async Task LoadCategoriesAsync()
    {
        var res = await CategoryService.GetAsync(new DataQueryParams<Category>());
        _categories = res.ToList();
        _categoriesTitles = res.Select(c => c.Title).ToList();
    }

    private async Task LoadDiscounts()
    {
        var res = await DiscountService.GetAsync(new DataQueryParams<Discount>());
        _allDiscounts = res.ToList();
    }
    
    private async Task LoadTraders()
    {
        var dataQuery = new DataQueryParams<Trader>
        {
            Filters = [],
            IncludeParams = new IncludeParams<Trader>
            {
                IncludeProperties = [t => t.Category]
            }
        };
        
        var res = await TraderService.GetAsync(dataQuery);
        _traders = res.ToList();
        StateHasChanged();
    }

    private async Task LoadTraderItems()
    {
        if (CurrentState.DisplayedTrader == null)
        {
            _traderItems = [];
            return;
        }
        var resItems = await TraderItemService.GetAsync(new DataQueryParams<TraderItem>()
        {
            Expression = i => i.TraderId == CurrentState.DisplayedTrader.Id,
            IncludeParams = new IncludeParams<TraderItem>
            {
                IncludeProperties = [i => i.Item, i => i.AppliedDiscount]
            }
        });
        _traderItems = resItems.ToList();
    }
}