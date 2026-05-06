using Microsoft.EntityFrameworkCore;
using Product.ApplicationCore.Contracts.Repository;
using Product.Infrastructure.Data;
using ProductEntity = Product.ApplicationCore.Entities.Product;

namespace Product.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ProductDbContext _context;

    public ProductRepository(ProductDbContext context)
    {
        _context = context;
    }

    // Get all products including their details
    public async Task<IEnumerable<ProductEntity>> GetAllProductsAsync()
        => await _context.Products
            .Include(p => p.ProductDetails)
            .AsNoTracking()
            .ToListAsync();

    // Get one product by its Id
    public async Task<ProductEntity?> GetProductByIdAsync(int id)
        => await _context.Products
            .Include(p => p.ProductDetails)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

    // Get all products by category
    public async Task<IEnumerable<ProductEntity>> GetProductsByCategoryAsync(string category)
        => await _context.Products
            .Include(p => p.ProductDetails)
            .Where(p => p.Category == category)
            .AsNoTracking()
            .ToListAsync();

    // Add a new product
    public async Task<ProductEntity> AddProductAsync(ProductEntity product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    // Update an existing product
    public async Task<ProductEntity> UpdateProductAsync(ProductEntity product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return product;
    }

    // Delete a product by Id
    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null) return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }
}