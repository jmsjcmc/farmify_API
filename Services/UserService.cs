using AutoMapper;
using Farmify_Api.Helpers;
using Farmify_Api.Helpers.Queries;
using Farmify_Api.Interfaces;
using Farmify_Api.Models;
using Farmify_Api.Models.User;
using Microsoft.EntityFrameworkCore;

namespace Farmify_Api.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserQueries _query;
        public UserService(AppDbContext context, IMapper mapper, UserQueries query)
        {
            _context = context;
            _mapper = mapper;
            _query = query;
        }
        // [HttpGet("users")]
        public async Task<Pagination<UserResponse>> paginatedusers(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null)
        {
            var query = await _query.paginatedusers(searchTerm);
            var totalcount = await query.CountAsync();

            var users = await PaginationHelper.paginateandproject<User, UserResponse>(
                query, pageNumber, pageSize, _mapper);

            return PaginationHelper.paginatedresponse(users, totalcount, pageNumber, pageSize);
        }
        // [HttpGet("user/{id}")]
        public async Task<UserResponse> getuser(int id)
        {
            var user = await _query.getmethoduserid(id);
            return _mapper.Map<UserResponse>(user);
        }
        //
        public async Task<UserResponse> getuserdetail(int id)
        {

        }
        // [HttpPost("user")]
        public async Task<UserResponse> createuser(UserRequest request)
        {
            var user = _mapper.Map<User>(request);
            user.Datecreated = TimeHelper.getphilippinestandardtime();

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var saveduser = await _query.getmethoduserid(user.Id);
            return _mapper.Map<UserResponse>(saveduser);
        }
        // [HttpPatch("user/update/{id}")]
        public async Task<UserResponse> updateuser(UserRequest request, int id)
        {
            var user = await _query.patchmethoduserid(id);

            _mapper.Map(request, user);

            await _context.SaveChangesAsync();

            var updateduser = await _query.getmethoduserid(user.Id);
            return _mapper.Map<UserResponse>(updateduser);
        }
        // [HttpPatch("user/hide/{id}")]
        public async Task<UserResponse> hideuser(int id)
        {
            var user = await _query.patchmethoduserid(id);

            user.Removed = true;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            var updateduser = await _query.getmethoduserid(id);
            return _mapper.Map<UserResponse>(updateduser);
        }
        // [HttpDelete("user/delete/{id}")]
        public async Task deleteuser (int id)
        {
            var user = await _query.patchmethoduserid(id);

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
