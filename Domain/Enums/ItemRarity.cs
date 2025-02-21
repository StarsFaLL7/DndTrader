using System.Drawing;

namespace Domain.Enums;

public enum ItemRarity
{
    None = 0,
    Common = 1,
    Uncommon = 2,
    Rare = 3,
    VeryRare = 4,
    Legendary = 5,
    Artefact = 6,
    RarityVaries = 7
}

public static class ItemRarityExtensions
{
    public static string ToRus(this ItemRarity rarity)
    {
        return rarity switch
        {
            ItemRarity.None => "Не имеет редкости",
            ItemRarity.Common => "Обычный",
            ItemRarity.Uncommon => "Необычный",
            ItemRarity.Rare => "Редкий",
            ItemRarity.VeryRare => "Очень редкий",
            ItemRarity.Legendary => "Легендарный",
            ItemRarity.Artefact => "Артефакт",
            ItemRarity.RarityVaries => "Редкость варьируется",
            _ => throw new ArgumentOutOfRangeException(nameof(rarity), rarity, null)
        };
    }

    public static string GetCssColor(this ItemRarity rarity)
    {
        return rarity switch
        {
            ItemRarity.None => "#ffffff",
            ItemRarity.Common => "#bebab4",
            ItemRarity.Uncommon => "#adb980",
            ItemRarity.Rare => "#82adc8",
            ItemRarity.VeryRare => "#d3a2de",
            ItemRarity.Legendary => "#f6ba95",
            ItemRarity.Artefact => "#e38983",
            ItemRarity.RarityVaries => "linear-gradient(135deg,#adb980 19%,#adb980 20%,#82adc8 39%,#82adc8 40%,#82adc8 41%,#d3a2de 59%,#d3a2de 60%,#d3a2de 61%,#f6ba95 79%,#f6ba95 80%)",
            _ => throw new ArgumentOutOfRangeException(nameof(rarity), rarity, null)
        };
    }
}