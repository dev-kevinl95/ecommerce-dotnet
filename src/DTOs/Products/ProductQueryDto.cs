using System.ComponentModel.DataAnnotations;

namespace Ecommerce.DTOs.Products;

public class ProductQueryDto
{
    public string? Search { get; set; }

    [Range(1, int.MaxValue)]
    public int? CategoryId { get; set; }

    [Range(1, int.MaxValue)]
    public int Page { get; set; } = 1;

    [Range(1, 100)]
    public int PageSize { get; set; } = 10;
}