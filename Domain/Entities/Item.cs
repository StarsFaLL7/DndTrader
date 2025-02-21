using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;
using Domain.Interfaces;

namespace Domain.Entities;

public class Item : IHasId
{
    [Key]
    public required Guid Id { get; set; }
    
    public required string Title { get; set; }
    
    public required string Description { get; set; }
    
    public string? ImagePath { get; set; }
    
    public required int DefaultPrice { get; set; }
    
    public required ItemRarity Rarity { get; set; }
    
    public required ItemType Type { get; set; }
    
    public required string HiddenDescription { get; set; }
}