using Microsoft.AspNetCore.Components;

namespace DndTraderServer.Utils;

public static class WebUtils
{
    public static string GetFullImageUrl(NavigationManager navManager, string imagePath)
    {
        if (imagePath.Contains("http"))
        {
            return imagePath;
        }
        return $"{navManager.BaseUri}{imagePath}?cacheBuster={Guid.NewGuid()}";
    }
}