using System.ComponentModel.DataAnnotations;

namespace SMartSHOP.Api.DTOs;

public class CreateProductDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [Range(0.01, 1000000)]
    public decimal Price { get; set; }

    [Required]
    public int CategoryId { get; set; }
}