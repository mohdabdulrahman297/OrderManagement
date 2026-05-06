using Product.ApplicationCore.Contracts.Repository;
using Product.ApplicationCore.Contracts.Services;
using Product.Infrastructure.Repositories;
using ProductEntity = Product.ApplicationCore.Entities.Product;

namespace Product.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public Task<IEnumerable<ProductEntity>> GetAllProductsAsync()
        => _productRepository.GetAllProductsAsync();

    public Task<ProductEntity?> GetProductByIdAsync(int id)
        => _productRepository.GetProductByIdAsync(id);

    public Task<IEnumerable<ProductEntity>> GetProductsByCategoryAsync(string category)
        => _productRepository.GetProductsByCategoryAsync(category);

    public Task<ProductEntity> CreateProductAsync(ProductEntity product)
        => _productRepository.AddProductAsync(product);

    public Task<ProductEntity> UpdateProductAsync(ProductEntity product)
        => _productRepository.UpdateProductAsync(product);

    public Task<bool> DeleteProductAsync(int id)
        => _productRepository.DeleteProductAsync(id);
}