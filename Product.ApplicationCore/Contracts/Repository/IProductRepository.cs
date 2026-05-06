using Product.ApplicationCore.Entities;

namespace Product.ApplicationCore.Contracts.Repository;

public interface IProductRepository
{
    Task<IEnumerable<Entities.Product>> GetAllProductsAsync();
    Task<Entities.Product?> GetProductByIdAsync(int id);
    Task<IEnumerable<Entities.Product>> GetProductsByCategoryAsync(string category);
    Task<Entities.Product> AddProductAsync(Entities.Product product);
    Task<Entities.Product> UpdateProductAsync(Entities.Product product);
    Task<bool> DeleteProductAsync(int id);
}