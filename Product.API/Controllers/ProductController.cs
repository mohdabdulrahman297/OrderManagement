using Microsoft.AspNetCore.Mvc;
using Product.ApplicationCore.Contracts.Services;
using ProductEntity = Product.ApplicationCore.Entities.Product;

namespace Product.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    // a. GET all Products
    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }

    // b. POST - Save new Product
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductEntity product)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _productService.CreateProductAsync(product);
        return CreatedAtAction(nameof(GetProductById), new { id = created.Id }, created);
    }

    // Helper for CreatedAtAction above
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        return product is null ? NotFound() : Ok(product);
    }

    // c. GET Products by Category
    [HttpGet("category/{category}")]
    public async Task<IActionResult> GetProductsByCategory(string category)
    {
        var products = await _productService.GetProductsByCategoryAsync(category);
        if (!products.Any())
            return NotFound($"No products found for category '{category}'.");
        return Ok(products);
    }

    // d. DELETE Product
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var deleted = await _productService.DeleteProductAsync(id);
        return deleted ? NoContent() : NotFound();
    }

    // e. PUT - Update Product
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductEntity product)
    {
        if (id != product.Id)
            return BadRequest("ID in URL does not match ID in body.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existing = await _productService.GetProductByIdAsync(id);
        if (existing is null)
            return NotFound();

        var updated = await _productService.UpdateProductAsync(product);
        return Ok(updated);
    }
}