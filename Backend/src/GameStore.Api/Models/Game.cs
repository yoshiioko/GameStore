using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Models;

public class Game
{
    public Guid Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public required string Name { get; set; }
    
    [Required]
    [StringLength(20)]
    public required string Genre { get; set; }
    
    [Range(1, 100)]
    public decimal Price { get; set; }
    public DateOnly ReleaseDate { get; set; }
}
