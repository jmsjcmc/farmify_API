using Microsoft.EntityFrameworkCore;

namespace Farmify_Api.Validators
{
    public class UserValidator
    {
        private readonly AppDbContext _context;
        public UserValidator(AppDbContext context)
        {
            _context = context;
        }
        public async Task ValidateUserLoginRequest(string username, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);
            
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                throw new ArgumentException("Not authenticated.");
            }
        }
    }
}
