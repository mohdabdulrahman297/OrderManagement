using Microsoft.EntityFrameworkCore;
using Product.ApplicationCore.Contracts.Repository;
using Product.ApplicationCore.Entities;
using Product.Infrastructure.Data;

namespace Product.Infrastructure.Repositories;

public class ProductDetailRepository : IProductDetailRepository
{
    private readonly ProductDbContext _context;

    public ProductDetailRepository(ProductDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductDetail>> GetAllProductDetailsAsync()
        => await _context.ProductDetails
            .AsNoTracking()
            .ToListAsync();

    public async Task<ProductDetail?> GetProductDetailByIdAsync(int id)
        => await _context.ProductDetails
            .AsNoTracking()
            .FirstOrDefaultAsync(pd => pd.Id == id);

    public async Task<IEnumerable<ProductDetail>> GetDetailsByProductIdAsync(int productId)
        => await _context.ProductDetails
            .Where(pd => pd.ProductId == productId)
            .AsNoTracking()
            .ToListAsync();

    public async Task<ProductDetail> AddProductDetailAsync(ProductDetail productDetail)
    {
        _context.ProductDetails.Add(productDetail);
        await _context.SaveChangesAsync();
        return productDetail;
    }

    public async Task<ProductDetail> UpdateProductDetailAsync(ProductDetail productDetail)
    {
        _context.ProductDetails.Update(productDetail);
        await _context.SaveChangesAsync();
        return productDetail;
    }

    public async Task<bool> DeleteProductDetailAsync(int id)
    {
        var detail = await _context.ProductDetails.FindAsync(id);
        if (detail is null) return false;

        _context.ProductDetails.Remove(detail);
        await _context.SaveChangesAsync();
        return true;
    }
}