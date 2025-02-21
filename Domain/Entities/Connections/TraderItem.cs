using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Interfaces;

namespace Domain.Entities.Connections;

public class TraderItem : IHasId
{
    [Key]
    public required Guid Id { get; set; }

    public required int Count { get; set; } = 1;
    
    public required Guid TraderId { get; set; }
    [ForeignKey("TraderId")]
    public Trader Trader { get; set; }
    
    public required Guid ItemId { get; set; }
    [ForeignKey("ItemId")]
    public Item Item { get; set; }
    
    public Guid? DiscountId { get; set; }
    [ForeignKey("DiscountId")]
    public Discount? AppliedDiscount { get; set; }
    
    public required bool UseDefaultPrice { get; set; }
    
    public int CustomPrice { get; set; }
}