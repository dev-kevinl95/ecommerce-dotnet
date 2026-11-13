using Ecommerce.Models;
using Ecommerce.DTOs.Categories;

namespace Ecommerce.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetAllAsync();

    Task<Category?> GetByIdAsync(int id);

    Task<Category> CreateAsync(CreateCategoryDto dto);

    Task<bool> UpdateAsync(int id, UpdateCategoryDto dto);

    Task<bool> DeleteAsync(int id);
}
