using Farmify_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Farmify_Api.Helpers.Queries
{
    public class FarmQueries
    {
        private readonly AppDbContext _context;
        public FarmQueries(AppDbContext context)
        {
            _context = context;
        }
        // Query for fetching all farms with optional filter for farm name
        public IQueryable<Farm> farmquery(string? searchTerm = null)
        {
            var query = _context.Farms
                .AsNoTracking()
                .Include(f => f.User)
                .OrderByDescending(f => f.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(f => f.Name == searchTerm);
            }

            return query;
        }
        // Query for fetching specific farm for GET method
        public async Task<Farm?> getmethodfarmid(int id)
        {
            return await _context.Farms
                .AsNoTracking()
                .Include(f => f.User)
                .FirstOrDefaultAsync(f => f.Id == id);
        } 
        // Query for fetching specific farm for PUT/PATCH/DELETE methods
        public async Task<Farm?> patchmethodfarmid(int id)
        {
            return await _context.Farms
                .Include(f => f.User)
                .FirstOrDefaultAsync(f => f.Id == id);
        }
    }
}
