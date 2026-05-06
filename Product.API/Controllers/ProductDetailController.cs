using Microsoft.AspNetCore.Mvc;
using Product.ApplicationCore.Contracts.Services;
using Product.ApplicationCore.Entities;

namespace Product.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductDetailController : ControllerBase
{
    private readonly IProductDetailService _productDetailService;

    public ProductDetailController(IProductDetailService productDetailService)
    {
        _productDetailService = productDetailService;
    }

    // a. GET all ProductDetails
    [HttpGet]
    public async Task<IActionResult> GetAllProductDetails()
    {
        var details = await _productDetailService.GetAllProductDetailsAsync();
        return Ok(details);
    }

    // b. GET ProductDetail by Id
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProductDetailById(int id)
    {
        var detail = await _productDetailService.GetProductDetailByIdAsync(id);
        return detail is null ? NotFound() : Ok(detail);
    }

    // c. GET ProductDetails by Product Id
    [HttpGet("product/{productId:int}")]
    public async Task<IActionResult> GetDetailsByProductId(int productId)
    {
        var details = await _productDetailService.GetDetailsByProductIdAsync(productId);
        if (!details.Any())
            return NotFound($"No details found for product {productId}.");
        return Ok(details);
    }

    // d. POST - Create new ProductDetail
    [HttpPost]
    public async Task<IActionResult> CreateProductDetail([FromBody] ProductDetail productDetail)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _productDetailService.CreateProductDetailAsync(productDetail);
        return CreatedAtAction(nameof(GetProductDetailById), new { id = created.Id }, created);
    }

    // e. PUT - Update ProductDetail
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProductDetail(int id, [FromBody] ProductDetail productDetail)
    {
        if (id != productDetail.Id)
            return BadRequest("ID in URL does not match ID in body.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existing = await _productDetailService.GetProductDetailByIdAsync(id);
        if (existing is null)
            return NotFound();

        var updated = await _productDetailService.UpdateProductDetailAsync(productDetail);
        return Ok(updated);
    }

    // f. DELETE ProductDetail
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProductDetail(int id)
    {
        var deleted = await _productDetailService.DeleteProductDetailAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}