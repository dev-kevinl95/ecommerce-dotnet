using Ecommerce.DTOs;
using Ecommerce.DTOs.Products;

namespace Ecommerce.Services.Interfaces;

public interface IProductService
{
    Task<PagedResponseDto<ProductResponseDto>> GetAllAsync(
        ProductQueryDto query
    );

    Task<ProductResponseDto?> GetByIdAsync(int id);

    Task<ProductResponseDto> CreateAsync(CreateProductDto dto);

    Task<bool> UpdateAsync(int id, UpdateProductDto dto);

    Task<bool> DeleteAsync(int id);
}