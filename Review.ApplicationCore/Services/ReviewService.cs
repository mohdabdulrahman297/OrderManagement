using Review.ApplicationCore.Contracts.Repository;
using Review.ApplicationCore.Contracts.Services;
using Review.ApplicationCore.Entities;

namespace Review.ApplicationCore.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<IEnumerable<CustomerReview>> GetAllReviewsAsync()
            => await _reviewRepository.GetAllReviewsAsync();

        public async Task<IEnumerable<CustomerReview>> GetApprovedReviewsAsync()
            => await _reviewRepository.GetApprovedReviewsAsync();

        public async Task<IEnumerable<CustomerReview>> GetReviewsByProductIdAsync(int productId)
            => await _reviewRepository.GetReviewsByProductIdAsync(productId);

        public async Task<CustomerReview?> GetReviewByIdAsync(int id)
            => await _reviewRepository.GetReviewByIdAsync(id);

        public async Task<CustomerReview> CreateReviewAsync(CustomerReview review)
            => await _reviewRepository.CreateReviewAsync(review);

        public async Task<CustomerReview> ApproveReviewAsync(int id)
        {
            var review = await _reviewRepository.GetReviewByIdAsync(id)
                ?? throw new KeyNotFoundException($"Review with ID {id} not found.");

            review.IsApproved = true;
            return await _reviewRepository.UpdateReviewAsync(review);
        }

        public async Task<CustomerReview> RejectReviewAsync(int id)
        {
            var review = await _reviewRepository.GetReviewByIdAsync(id)
                ?? throw new KeyNotFoundException($"Review with ID {id} not found.");

            review.IsApproved = false;
            return await _reviewRepository.UpdateReviewAsync(review);
        }

        public async Task<bool> DeleteReviewAsync(int id)
            => await _reviewRepository.DeleteReviewAsync(id);
    }
}