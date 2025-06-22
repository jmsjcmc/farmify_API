﻿using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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

        public static int ValidateUserClaim(ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            
            if (userIdClaim == null)
            {
                throw new UnauthorizedAccessException("User ID claim not found.");
            }

            if (!int.TryParse(userIdClaim.Value, out var userId))
            {
                throw new UnauthorizedAccessException("Invalid user ID claim.");
            }

            return userId;
        }
    }
}
