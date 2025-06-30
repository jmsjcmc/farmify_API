using AutoMapper;
using Farmify_Api.Helpers;
using Farmify_Api.Helpers.Queries;
using Farmify_Api.Interfaces;
using Farmify_Api.Models;
using Farmify_Api.Validators;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Farmify_Api.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly UserQueries _query;
        private readonly AuthenticationHelper _authHelper;
        public UserService(AppDbContext context, IMapper mapper, UserQueries query, AuthenticationHelper authHelper) : base (context, mapper)
        {
            _query = query;
            _authHelper = authHelper;
        }
        // [HttpGet("users")]
        public async Task<Pagination<UserResponse>> paginatedusers(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null)
        {
            var query = await _query.paginatedusers(searchTerm);
            return await PaginationHelper.paginateandmap<User, UserResponse>(query, pageNumber, pageSize, _mapper);
        }
        // [HttpGet("user/{id}")]
        public async Task<UserResponse> getuser(int id)
        {
            var user = await _query.getmethoduserid(id);
            return _mapper.Map<UserResponse>(user);
        }
        // [HttpGet("user-detail")]
        public async Task<UserResponse> getuserdetail(ClaimsPrincipal detail)
        {
            int userId = UserValidator.ValidateUserClaim(detail);
            var user = await _query.getmethoduserid(userId);

            return _mapper.Map<UserResponse>(user);
        }
        // [HttpPost("login")]
        public async Task<object> login(string username, string password)
        {
            var user = await _context.Users
                .SingleOrDefaultAsync(u => u.Username == username);

            var accesstoken = _authHelper.generateaccesstoken(user);

            await _context.SaveChangesAsync();

            return new
            {
                AccessToken = accesstoken,
                User = new
                {
                    user.Id,
                    user.Username
                }
            };
        }
        // [HttpPost("user")]
        public async Task<UserResponse> createuser(UserRequest request)
        {
            var user = _mapper.Map<User>(request);
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.Datecreated = TimeHelper.getphilippinestandardtime();

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return await userResponse(user.Id);
        }
        // [HttpPatch("user/update/{id}")]
        public async Task<UserResponse> updateuser(UserRequest request, int id)
        {
            var user = await patchuserid(id);

            _mapper.Map(request, user);
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.Dateupdated = TimeHelper.getphilippinestandardtime();

            await _context.SaveChangesAsync();

            return await userResponse(user.Id);
        }
        // [HttpPatch("user/hide/{id}")]
        public async Task<UserResponse> hideuser(int id)
        {
            var user = await patchuserid(id);

            user.Removed = true;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return await userResponse(user.Id);
        }
        // [HttpDelete("user/delete/{id}")]
        public async Task<UserResponse> deleteuser (int id)
        {
            var user = await patchuserid(id);

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return await userResponse(user.Id);
        }
        // Helpers
        private async Task<User?> patchuserid(int id)
        {
            return await _query.patchmethoduserid(id);
        }
        private async Task<User?> getuserid(int id)
        {
            return await _query.getmethoduserid(id);
        }
        private async Task<UserResponse> userResponse(int id)
        {
            var response = await getuserid(id);
            return _mapper.Map<UserResponse>(response);
        }
    }
}
