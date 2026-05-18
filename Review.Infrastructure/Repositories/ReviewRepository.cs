using Microsoft.EntityFrameworkCore;
using Review.ApplicationCore.Contracts.Repository;
using Review.ApplicationCore.Entities;
using Review.Infrastructure.Data;

namespace Review.Infrastructure.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ReviewDbContext _context;

        public ReviewRepository(ReviewDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerReview>> GetAllReviewsAsync()
        {
            return await _context.Reviews
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<CustomerReview>> GetApprovedReviewsAsync()
        {
            return await _context.Reviews
                .AsNoTracking()
                .Where(r => r.IsApproved)
                .ToListAsync();
        }

        public async Task<IEnumerable<CustomerReview>> GetReviewsByProductIdAsync(int productId)
        {
            return await _context.Reviews
                .AsNoTracking()
                .Where(r => r.ProductId == productId && r.IsApproved)
                .ToListAsync();
        }

        public async Task<CustomerReview?> GetReviewByIdAsync(int id)
        {
            return await _context.Reviews
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<CustomerReview> CreateReviewAsync(CustomerReview review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<CustomerReview> UpdateReviewAsync(CustomerReview review)
        {
            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<bool> DeleteReviewAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review is null) return false;

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}