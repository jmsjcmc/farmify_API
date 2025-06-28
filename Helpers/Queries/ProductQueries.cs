using Farmify_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Farmify_Api.Helpers.Queries
{
    public class ProductQueries
    {
        private readonly AppDbContext _context;
        public ProductQueries(AppDbContext context)
        {
            _context = context;
        }
        // Query for fetchiing all products with optional filter for product name
        public IQueryable<Product> paginatedproducts(string? searchTerm = null)
        {
            var query = _context.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .Where(p => !p.Removed)
                .OrderByDescending(d => d.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(p => p.Productname == searchTerm);
            }

            return query;
        }
        // Query for fetching specific product for GET method
        public async Task<Product?> getmethodproductid(int id)
        {
            return await _context.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        // Query for fetching specific product for PUT/PATCH/DELETE methods
        public async Task<Product?> patchmethodproductid(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
