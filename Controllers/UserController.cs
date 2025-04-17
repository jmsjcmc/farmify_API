using Farmify_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Farmify_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("All-Users")]
        public async Task<ActionResult<Paginate<UserResponse>>> allUsers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? searchTerm = null)
        {
            try
            {
                var query = _context.Users
                    .AsNoTracking()
                    .OrderBy(d => d.Id)
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    query = query.Where(u => u.FirstName == searchTerm || u.LastName == searchTerm);
                }

                var totalCount = await query.CountAsync();

                var users = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                if (users == null)
                {
                    return NotFound("No users found!");
                }

                var userDetails = users.Select(u => new UserResponse
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Username = u.Username,
                    Password = u.Password,
                    Email = u.Email,
                    Role = u.Role
                });

                var response = new Paginate<UserResponse>
                {
                    Items = userDetails,
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                return response;
            }catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpPost("Add-User")]
        public async Task<ActionResult<UserResponse>> addUser([FromBody] UserRequest request)
        {
            try
            {
                if(await _context.Users.AnyAsync(u => u.Username == request.Username))
                {
                    return BadRequest("Username is already taken!");
                }

                var requestRole = request.Role
                    .Select
            }catch(Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
