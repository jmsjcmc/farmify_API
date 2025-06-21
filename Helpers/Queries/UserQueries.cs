using Farmify_Api.Models.User;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Farmify_Api.Helpers.Queries
{
    public class UserQueries
    {
        private readonly AppDbContext _context;
        public UserQueries(AppDbContext context)
        {
            _context  = context;
        }
        // Query for fetching users (paginated)
        public async Task<IQueryable<User>> paginatedusers (string? searchTerm = null)
        {
            var query = _context.Users
                .AsNoTracking()
                .OrderByDescending(u => u.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(u => u.Username == searchTerm);
            }

            return query;
        }
        // Query for fetching specific user for GET method
        public async Task<User?> getmethoduserid(int id)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
        }
        // Query for fetching specific user for PUT/PATCH/DELETE methods
        public async Task<User?> patchmethoduserid(int id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
