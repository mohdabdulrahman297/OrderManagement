using Product.ApplicationCore.Entities;

namespace Product.ApplicationCore.Contracts.Services;

public interface IProductDetailService
{
    Task<IEnumerable<ProductDetail>> GetAllProductDetailsAsync();
    Task<ProductDetail?> GetProductDetailByIdAsync(int id);
    Task<IEnumerable<ProductDetail>> GetDetailsByProductIdAsync(int productId);
    Task<ProductDetail> CreateProductDetailAsync(ProductDetail productDetail);
    Task<ProductDetail> UpdateProductDetailAsync(ProductDetail productDetail);
    Task<bool> DeleteProductDetailAsync(int id);
}