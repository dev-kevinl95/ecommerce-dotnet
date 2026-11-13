using System.ComponentModel.DataAnnotations;

namespace Ecommerce.DTOs.Products;

public class UpdateProductDto
{
    [Required]
    [StringLength(150, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(1000)]
    public string Description { get; set; } = string.Empty;

    [Range(0.01, 999999.99)]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue)]
    public int Stock { get; set; }

    [Url]
    [StringLength(500)]
    public string ImageUrl { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int CategoryId { get; set; }
}