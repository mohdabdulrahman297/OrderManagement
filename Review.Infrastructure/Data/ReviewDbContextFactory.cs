using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Review.Infrastructure.Data
{
    public class ReviewDbContextFactory : IDesignTimeDbContextFactory<ReviewDbContext>
    {
        public ReviewDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ReviewDbContext>();
            optionsBuilder.UseSqlServer(
                "Server=.;Database=ReviewDb;Trusted_Connection=True;TrustServerCertificate=True");

            return new ReviewDbContext(optionsBuilder.Options);
        }
    }
}