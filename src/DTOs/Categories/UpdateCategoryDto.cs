using System.ComponentModel.DataAnnotations;

namespace Ecommerce.DTOs.Categories;

public class UpdateCategoryDto
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;
}