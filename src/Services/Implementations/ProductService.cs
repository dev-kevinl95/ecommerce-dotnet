using Ecommerce.Data;
using Ecommerce.DTOs;
using Ecommerce.DTOs.Products;
using Ecommerce.Models;
using Ecommerce.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services.Implementations;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;

    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResponseDto<ProductResponseDto>> GetAllAsync(ProductQueryDto query)
    {

        var productsQuery = _context.Products
            .Include(product => product.Category)
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            var search = query.Search.Trim();

            productsQuery = productsQuery.Where(product =>
            EF.Functions.ILike(product.Name, $"%{search}%") ||
            EF.Functions.ILike(product.Description, $"%{search}%"));
        }

        if (query.CategoryId.HasValue)
        {
            productsQuery = productsQuery.Where(product => product.CategoryId == query.CategoryId.Value);
        }

        var totalItems = await productsQuery.CountAsync();

        var products = await productsQuery
        .OrderByDescending(product=> product.CreatedAt)
        .Skip((query.Page-1)* query.PageSize)
        .Take(query.PageSize)
        .ToListAsync();

        return new PagedResponseDto<ProductResponseDto>
        {
            Items=products.Select(ToResponseDto),
            Page=query.Page,
            PageSize=query.PageSize,
            TotalItems= totalItems,
            TotalPages=(int)Math.Ceiling(totalItems/(double)query.PageSize)
        };
    }

    public async Task<ProductResponseDto?> GetByIdAsync(int id)
    {
        var product = await _context.Products
            .Include(product => product.Category)
            .AsNoTracking()
            .FirstOrDefaultAsync(product => product.Id == id);

        return product is null ? null : ToResponseDto(product);
    }

    public async Task<ProductResponseDto> CreateAsync(CreateProductDto dto)
    {
        await EnsureCategoryExistsAsync(dto.CategoryId);

        var product = new Product
        {
            Name = dto.Name.Trim(),
            Description = dto.Description.Trim(),
            Price = dto.Price,
            Stock = dto.Stock,
            ImageUrl = dto.ImageUrl.Trim(),
            CategoryId = dto.CategoryId
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        var category = await _context.Categories.FindAsync(dto.CategoryId);

        product.Category = category;

        return ToResponseDto(product);
    }

    public async Task<bool> UpdateAsync(int id, UpdateProductDto dto)
    {
        var product = await _context.Products.FindAsync(id);

        if (product is null)
        {
            return false;
        }

        await EnsureCategoryExistsAsync(dto.CategoryId);

        product.Name = dto.Name.Trim();
        product.Description = dto.Description.Trim();
        product.Price = dto.Price;
        product.Stock = dto.Stock;
        product.ImageUrl = dto.ImageUrl.Trim();
        product.CategoryId = dto.CategoryId;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product is null)
        {
            return false;
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return true;
    }

    private async Task EnsureCategoryExistsAsync(int categoryId)
    {
        var categoryExists = await _context.Categories
            .AnyAsync(category => category.Id == categoryId);

        if (!categoryExists)
        {
            throw new InvalidOperationException(
                "La categoría seleccionada no existe."
            );
        }
    }

    private static ProductResponseDto ToResponseDto(Product product)
    {
        return new ProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            ImageUrl = product.ImageUrl,
            CategoryId = product.CategoryId,
            CategoryName = product.Category?.Name ?? string.Empty,
            CreatedAt = product.CreatedAt
        };
    }
}