using System.ComponentModel.DataAnnotations;
using Domain.Interfaces;

namespace Domain.Entities;

public class Category : IHasId
{
    [Key]
    public required Guid Id { get; set; }
    
    public required string Title { get; set; }
}