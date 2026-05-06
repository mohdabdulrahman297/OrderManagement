using Product.ApplicationCore.Contracts.Repository;
using Product.ApplicationCore.Contracts.Services;
using Product.ApplicationCore.Entities;
using Product.Infrastructure.Repositories;

namespace Product.Infrastructure.Services;

public class ProductDetailService : IProductDetailService
{
    private readonly IProductDetailRepository _detailRepository;

    public ProductDetailService(IProductDetailRepository detailRepository)
    {
        _detailRepository = detailRepository;
    }

    public Task<IEnumerable<ProductDetail>> GetAllProductDetailsAsync()
        => _detailRepository.GetAllProductDetailsAsync();

    public Task<ProductDetail?> GetProductDetailByIdAsync(int id)
        => _detailRepository.GetProductDetailByIdAsync(id);

    public Task<IEnumerable<ProductDetail>> GetDetailsByProductIdAsync(int productId)
        => _detailRepository.GetDetailsByProductIdAsync(productId);

    public Task<ProductDetail> CreateProductDetailAsync(ProductDetail productDetail)
        => _detailRepository.AddProductDetailAsync(productDetail);

    public Task<ProductDetail> UpdateProductDetailAsync(ProductDetail productDetail)
        => _detailRepository.UpdateProductDetailAsync(productDetail);

    public Task<bool> DeleteProductDetailAsync(int id)
        => _detailRepository.DeleteProductDetailAsync(id);
}