using Farmify_Api.Models;
using System.Security.Claims;

namespace Farmify_Api.Interfaces
{
    public interface IUserService
    {
        Task<Pagination<UserResponse>> paginatedusers(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null);
        Task<UserResponse> getuser(int id);
        Task<UserResponse> getuserdetail(ClaimsPrincipal detail);
        Task<object> login(string username, string password);
        Task<UserResponse> createuser(UserRequest request);
        Task<UserResponse> updateuser(UserRequest request, int id);
        Task<UserResponse> hideuser(int id);
        Task deleteuser(int id);
    }
}
