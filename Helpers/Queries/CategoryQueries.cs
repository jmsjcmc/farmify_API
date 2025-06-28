using Farmify_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Farmify_Api.Helpers.Queries
{
    public class CategoryQueries
    {
        private readonly AppDbContext _context;
        public CategoryQueries(AppDbContext context)
        {
            _context = context;
        }
        // Query for fetching all categories (List)
        public async Task<List<Category>> categorieslist(string? searchTerm = null)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                return await _context.Categories
                    .AsNoTracking()
                    .Where(c => c.Categoryname == searchTerm)
                    .OrderByDescending(c => c.Id)
                    .ToListAsync();
            }
            else
            {
                return await _context.Categories
                    .AsNoTracking()
                    .OrderByDescending(c => c.Id)
                    .ToListAsync();
            }
        }
        // Query for fetching specific category for GET method
        public async Task<Category?> getmethodcategoryid(int id)
        {
            return await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        // Query for fetching specific category for PUT/PATCH/DELETE methods
        public async Task<Category?> patchmethodcategoryid(int id)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
