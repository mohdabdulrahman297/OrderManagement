using Review.ApplicationCore.Entities;

namespace Review.ApplicationCore.Contracts.Repository
{
    public interface IReviewRepository
    {
        Task<IEnumerable<CustomerReview>> GetAllReviewsAsync();
        Task<IEnumerable<CustomerReview>> GetApprovedReviewsAsync();
        Task<IEnumerable<CustomerReview>> GetReviewsByProductIdAsync(int productId);
        Task<CustomerReview?> GetReviewByIdAsync(int id);
        Task<CustomerReview> CreateReviewAsync(CustomerReview review);
        Task<CustomerReview> UpdateReviewAsync(CustomerReview review);
        Task<bool> DeleteReviewAsync(int id);
    }
}