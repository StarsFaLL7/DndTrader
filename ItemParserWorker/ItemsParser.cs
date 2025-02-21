using Application.Services;
using Domain.Entities;
using Domain.Enums;
using ItemParserWorker.Entities;
using Newtonsoft.Json;

namespace ItemParserWorker;

public class ItemsParser
{
    public Action<Item>? OnItemAddedAction;
    
    public async Task LoadItemsFromDndSuAsync(BaseService<Item> itemsService, string pathToFile)
    {
        var fileContent = await File.ReadAllTextAsync(pathToFile);
        var info = JsonConvert.DeserializeObject<List<List<RegGroup>>>(fileContent)!;
        //var items = new List<Item>();
        var count = 1;
        foreach (var itemInfo in info)
        {
            var item = new DeserializedItemInfo
            {
                PagePath = itemInfo[0].Content,
                ItemType = itemInfo[1].Content,
                Title = itemInfo[2].Content,
                ItemRarity = itemInfo[3].Content
            };
            var fullInfoContent = await GetHtmlPageContentAsync("https://dnd.su" + item.PagePath);
            var fullInfo = await ParseInfoFromFullItemInfoPage(fullInfoContent);
            var itemResult = new Item
            {
                Id = Guid.NewGuid(),
                Title = item.Title,
                Description = fullInfo.Description,
                ImagePath = fullInfo.ImagesPaths.Count > 0 ? fullInfo.ImagesPaths[0] : "",
                DefaultPrice = (fullInfo.RecommendedPriceFrom + fullInfo.RecommendedPriceTo) / 2,
                Rarity = ParseRarity(item.ItemRarity),
                Type = ParseItemType(item.ItemType),
                HiddenDescription = ""
            };
            //items.Add(itemResult);
            await itemsService.SaveAsync(itemResult);
            //OnItemAddedAction?.Invoke(itemResult);
            Console.WriteLine($"{count}. Добавлен предмет \"{itemResult.Title}\" с редкость \"{item.ItemRarity}\" = \"{itemResult.Rarity.ToRus()}\" и типом " +
                              $"\"{item.ItemType}\" = \"{itemResult.Type.ToRus()}\". Цена {fullInfo.RecommendedPriceFrom}-{fullInfo.RecommendedPriceTo}");
            count++;
        }

        //return items.ToArray();
    }

    private ItemRarity ParseRarity(string rarity)
    {
        if (rarity.Contains("Очень редк", StringComparison.OrdinalIgnoreCase))
        {
            return ItemRarity.VeryRare;
        }
        if (rarity.Contains("варьируется", StringComparison.OrdinalIgnoreCase))
        {
            return ItemRarity.RarityVaries;
        }
        if (rarity.Contains("Редк", StringComparison.OrdinalIgnoreCase))
        {
            return ItemRarity.Rare;
        }
        if (rarity.Contains("Необычн", StringComparison.OrdinalIgnoreCase))
        {
            return ItemRarity.Uncommon;
        }
        if (rarity.Contains("Артефакт", StringComparison.OrdinalIgnoreCase))
        {
            return ItemRarity.Artefact;
        }
        if (rarity.Contains("Легендарн", StringComparison.OrdinalIgnoreCase))
        {
            return ItemRarity.Legendary;
        }
        if (rarity.Contains("Обычн", StringComparison.OrdinalIgnoreCase))
        {
            return ItemRarity.Common;
        }
        

        return ItemRarity.None;
    }
    
    private ItemType ParseItemType(string type)
    {
        return type switch
        {
            "Доспех" => ItemType.Armor,
            "Чудесный предмет" => ItemType.WonderfulItem,
            "Оружие" => ItemType.Weapon,
            "Волшебная палочка" => ItemType.MagicWand,
            "Зелье" => ItemType.Potion,
            "Жезл" or "Посох" => ItemType.Staff,
            "Кольцо" => ItemType.Ring,
            "Свиток" => ItemType.Scroll,
            _ => ItemType.Other
        };
    }
    
    private async Task<string> GetHtmlPageContentAsync(string pagePath)
    {
        var uri = new Uri(pagePath);
        using var http = new HttpClient();
        var response = await http.GetAsync(uri);
        var content = await response.Content.ReadAsStringAsync();
        return content;
    }

    private async Task<InfoFromFullPage> ParseInfoFromFullItemInfoPage(string htmlContent)
    {
        var startPhrase = "Рекомендованная стоимость:</strong>";
        var priceStartIndex = htmlContent.IndexOf(startPhrase, StringComparison.Ordinal) + startPhrase.Length;
        var endPhrase = "</li>";
        var priceEndIndex = htmlContent[priceStartIndex..].IndexOf(endPhrase, StringComparison.Ordinal) + priceStartIndex;
        var priceStrItems = htmlContent[priceStartIndex..priceEndIndex].Replace("&thinsp;", "").Split(" ");
        var priceMin = 0;
        var priceMax = 2000;
        if (priceStrItems.Length == 3)
        {
            var priceStr = priceStrItems[1];
            var priceParts = priceStr.Split('-');
            if (priceParts.Length == 2)
            {
                priceMin = int.Parse(priceParts[0]);
                priceMax = int.Parse(priceParts[1]);
            }
            else
            {
                priceMax = priceMin = int.Parse(priceParts[0]);
                
            }
            
        }
        
        var descStartPhrase = "<div itemprop=\"description\"><p>";
        var descStartIndex = htmlContent.IndexOf(descStartPhrase, StringComparison.OrdinalIgnoreCase) + descStartPhrase.Length;
        var descEndPhrase = "</div>";
        var descEndIndex = htmlContent[descStartIndex..]
            .IndexOf(descEndPhrase, StringComparison.OrdinalIgnoreCase) + descStartIndex;
        var desc = htmlContent[descStartIndex..descEndIndex].Replace("<p>", "").Replace("</p>", "\n").Trim();
        if (priceMin != 0)
        {
            desc = $"Рекомендованная стоимость: {priceMin} - {priceMax} зм\n" + desc;
        }
        else
        {
            desc = "Рекомендованная стоимость не указана.\n" + desc;
        }
        var imageStartIndex = htmlContent.IndexOf("img src='/gallery", StringComparison.OrdinalIgnoreCase) + "img src='".Length;
        var imageEndIndex = htmlContent[imageStartIndex..].IndexOf('\'') + imageStartIndex;
        var imagePath = "https://dnd.su" + htmlContent[imageStartIndex..imageEndIndex];
        
        return new InfoFromFullPage
        {
            Description = desc,
            ImagesPaths = [imagePath],
            RecommendedPriceFrom = priceMin,
            RecommendedPriceTo = priceMax
        };
    }
}