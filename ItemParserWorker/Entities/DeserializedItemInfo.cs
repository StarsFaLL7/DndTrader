namespace ItemParserWorker.Entities;

public class DeserializedItemInfo
{
    public required string PagePath { get; set; }
    
    public required string ItemType { get; set; }
    
    public required string Title { get; set; }
    
    public required string ItemRarity { get; set; }
}