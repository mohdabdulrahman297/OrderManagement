using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Review.ApplicationCore.Contracts.Services;
using Review.ApplicationCore.Entities;

namespace Review.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public CustomerReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // a. GET all reviews — Admin only
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllReviews()
        {
            var reviews = await _reviewService.GetAllReviewsAsync();
            return Ok(reviews);
        }

        // b. GET approved reviews — Customer
        [HttpGet("approved")]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> GetApprovedReviews()
        {
            var reviews = await _reviewService.GetApprovedReviewsAsync();
            return Ok(reviews);
        }

        // c. GET reviews by ProductId — Customer
        [HttpGet("product/{productId:int}")]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> GetReviewsByProduct(int productId)
        {
            var reviews = await _reviewService.GetReviewsByProductIdAsync(productId);
            if (!reviews.Any()) return NotFound($"No reviews found for product ID {productId}.");
            return Ok(reviews);
        }

        // d. GET review by Id — Admin only
        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetReviewById(int id)
        {
            var review = await _reviewService.GetReviewByIdAsync(id);
            return review is null ? NotFound() : Ok(review);
        }

        // e. POST - Create review — Customer
        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CreateReview([FromBody] CustomerReview review)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _reviewService.CreateReviewAsync(review);
            return CreatedAtAction(nameof(GetReviewById), new { id = created.Id }, created);
        }

        // f. PATCH approve review — Admin only
        [HttpPatch("{id:int}/approve")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ApproveReview(int id)
        {
            try
            {
                var review = await _reviewService.ApproveReviewAsync(id);
                return Ok(review);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // g. PATCH reject review — Admin only
        [HttpPatch("{id:int}/reject")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RejectReview(int id)
        {
            try
            {
                var review = await _reviewService.RejectReviewAsync(id);
                return Ok(review);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // h. DELETE review — Admin only
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var deleted = await _reviewService.DeleteReviewAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}