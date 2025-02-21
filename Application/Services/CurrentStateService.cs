using Domain.Entities;
using Domain.Entities.Connections;

namespace Application.Services;

public class CurrentStateService
{
    public event Func<Task>? PropertiesChangedCallback;
    public Trader? DisplayedTrader { get; set; }
    public HashSet<Guid> DisplayedPlayersIds { get; } = [];
    
    public bool HasTraderToDisplay => DisplayedTrader != null;

    public readonly List<TraderItem> ExpandedItems = [];

    private readonly HashSet<Guid> _hiddenItems = [];

    public void ToggleHideItem(TraderItem item)
    {
        if (_hiddenItems.Contains(item.Id))
        {
            _hiddenItems.Remove(item.Id);
        }
        else
        {
            _hiddenItems.Add(item.Id);
        }
    }

    public bool IsItemHidden(Guid id)
    {
        return _hiddenItems.Contains(id);
    }
    
    public bool ToggleItemExpand(TraderItem item)
    {
        var itemToRemove = ExpandedItems.FirstOrDefault(i => i.Id == item.Id);
        if (itemToRemove != null)
        {
            ExpandedItems.Remove(itemToRemove);
            return true;
        }
        ExpandedItems.Add(item);
        return true;
    }

    public bool IsItemExpanded(Guid id)
    {
        return ExpandedItems.Any(i => i.Id == id);
    }
    
    public bool TryAddPlayerToDisplay(Player player)
    {
        if (DisplayedPlayersIds.All(p => p != player.Id))
        {
            DisplayedPlayersIds.Add(player.Id);
            return true;
        }

        return false;
    }
    
    public void SetDisplayTrader(Trader trader)
    {
        DisplayedTrader = trader;
    }

    public void ClearDisplayTrader()
    {
        DisplayedTrader = null;
    }

    public Trader? GetDisplayTrader()
    {
        return DisplayedTrader;
    }

    public bool IsPlayerDisplaying(Player player)
    {
        return DisplayedPlayersIds.Any(p => p == player.Id);
    }
    
    public async Task StateChangedAsync()
    {
        if (PropertiesChangedCallback == null)
        {
            return;
        }
        var invocationList = PropertiesChangedCallback.GetInvocationList();
        var tasks = new List<Task>();
        foreach (var @delegate in invocationList)
        {
            var handler = (Func<Task>)@delegate;
            tasks.Add(handler());
        }
        
        await Task.WhenAll(tasks);
    }
}