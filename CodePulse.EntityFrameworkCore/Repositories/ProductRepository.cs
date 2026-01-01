using CodePulse.Domain.Entities;
using CodePulse.Domain.Repositories;
using CodePulse.EntityFrameworkCore.Data.PostGreSql;

namespace CodePulse.EntityFrameworkCore.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly PostgresDbContext _dbContext;

        public ProductRepository(PostgresDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }
    }
}
