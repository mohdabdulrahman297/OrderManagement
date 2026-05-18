using Review.ApplicationCore.Entities;

namespace Review.ApplicationCore.Contracts.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<CustomerReview>> GetAllReviewsAsync();
        Task<IEnumerable<CustomerReview>> GetApprovedReviewsAsync();
        Task<IEnumerable<CustomerReview>> GetReviewsByProductIdAsync(int productId);
        Task<CustomerReview?> GetReviewByIdAsync(int id);
        Task<CustomerReview> CreateReviewAsync(CustomerReview review);
        Task<CustomerReview> ApproveReviewAsync(int id);
        Task<CustomerReview> RejectReviewAsync(int id);
        Task<bool> DeleteReviewAsync(int id);
    }
}