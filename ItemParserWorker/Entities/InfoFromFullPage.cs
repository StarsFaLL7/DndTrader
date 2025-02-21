namespace ItemParserWorker.Entities;

public class InfoFromFullPage
{
    public required string Description { get; set; }
    
    public required List<string> ImagesPaths { get; set; }
    
    public int RecommendedPriceFrom { get; set; }
    
    public int RecommendedPriceTo { get; set; }
}