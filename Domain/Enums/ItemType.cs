namespace Domain.Enums;

public enum ItemType
{
    Other = 0,
    MagicWand = 1,
    Armor = 2,
    Staff = 3,
    Potion = 4,
    Ring = 5,
    Weapon = 6,
    Scroll = 7,
    WonderfulItem = 8
}

public static class ItemTypeExtensions
{
    public static string ToRus(this ItemType type)
    {
        return type switch
        {
            ItemType.Other => "Другое",
            ItemType.MagicWand => "Магическая палочка",
            ItemType.Armor => "Броня",
            ItemType.Staff => "Посох",
            ItemType.Potion => "Зелье",
            ItemType.Ring => "Кольцо",
            ItemType.Weapon => "Оружие",
            ItemType.Scroll => "Свиток",
            ItemType.WonderfulItem => "Чудесный предмет",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}