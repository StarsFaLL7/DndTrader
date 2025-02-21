using System.ComponentModel.DataAnnotations;
using Domain.Interfaces;

namespace Domain.Entities;

public class Discount : IHasId
{
    [Key]
    public required Guid Id { get; set; }
    
    public required string Title { get; set; }
    
    public required bool IsPercent { get; set; }
    
    public int DiscountPercent { get; set; }
    
    public int DiscountFix { get; set; }
}