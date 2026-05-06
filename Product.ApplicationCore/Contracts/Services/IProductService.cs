using Product.ApplicationCore.Entities;

namespace Product.ApplicationCore.Contracts.Services;

public interface IProductService
{
    Task<IEnumerable<Entities.Product>> GetAllProductsAsync();
    Task<Entities.Product?> GetProductByIdAsync(int id);
    Task<IEnumerable<Entities.Product>> GetProductsByCategoryAsync(string category);
    Task<Entities.Product> CreateProductAsync(Entities.Product product);
    Task<Entities.Product> UpdateProductAsync(Entities.Product product);
    Task<bool> DeleteProductAsync(int id);
}