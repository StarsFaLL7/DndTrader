using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Interfaces;

namespace Domain.Entities;

public class Trader : IHasId
{
    [Key]
    public required Guid Id { get; set; }
    
    public required string Title { get; set; }
    
    public string? ImagePath { get; set; }
    
    public required string HiddenDescription { get; set; }
    
    public Guid? CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public Category? Category { get; set; }
}