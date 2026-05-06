using Product.ApplicationCore.Entities;

namespace Product.ApplicationCore.Contracts.Repository;

public interface IProductDetailRepository
{
    Task<IEnumerable<ProductDetail>> GetAllProductDetailsAsync();
    Task<ProductDetail?> GetProductDetailByIdAsync(int id);
    Task<IEnumerable<ProductDetail>> GetDetailsByProductIdAsync(int productId);
    Task<ProductDetail> AddProductDetailAsync(ProductDetail productDetail);
    Task<ProductDetail> UpdateProductDetailAsync(ProductDetail productDetail);
    Task<bool> DeleteProductDetailAsync(int id);
}