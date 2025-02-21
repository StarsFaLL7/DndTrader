using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Interfaces;

namespace Domain.Entities;

public class Player : IHasId
{
    [Key]
    public Guid Id { get; set; }
    
    public required string Title { get; set; }

    public string Notes { get; set; } = "";
    
    public required int Balance { get; set; }
    
    public string? ImagePath { get; set; }
    
    public Guid? CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public Category? Category { get; set; }
}