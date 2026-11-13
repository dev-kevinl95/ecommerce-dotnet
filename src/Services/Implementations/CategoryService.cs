using Ecommerce.Data;
using Ecommerce.DTOs.Categories;
using Ecommerce.Models;
using Ecommerce.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly ApplicationDbContext _context;

    public CategoryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _context.Categories
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _context.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(category => category.Id == id);
    }

    public async Task<Category> CreateAsync(CreateCategoryDto dto)
    {
        var category = new Category
        {
            Name = dto.Name.Trim(),
            Description = dto.Description.Trim()
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return category;
    }

    public async Task<bool> UpdateAsync(int id, UpdateCategoryDto dto)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category is null)
        {
            return false;
        }

        category.Name = dto.Name.Trim();
        category.Description = dto.Description.Trim();

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category is null)
        {
            return false;
        }

        var hasProducts = await _context.Products
            .AnyAsync(product => product.CategoryId == id);

        if (hasProducts)
        {
            throw new InvalidOperationException(
                "No se puede eliminar una categoría que tiene productos asociados."
            );
        }

        _context.Categories.Remove(category);

        await _context.SaveChangesAsync();

        return true;
    }
}
