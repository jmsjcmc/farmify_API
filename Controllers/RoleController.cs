using Farmify_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Farmify_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly AppDbContext _context;
        public RoleController(AppDbContext context)
        {
            _context = context;   
        }

        [HttpGet("All-Roles")]
        public async Task<ActionResult<Paginate<RoleResponse>>> getRoles([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? searchTerm = null)
        {
            try
            {
                var query = _context.Roles
                    .AsNoTracking()
                    .Where(r => r.Deleted == false)
                    .OrderBy(r => r.Id)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query = query.Where(r => r.RoleName == searchTerm);
                }

                var totalCount = await query.CountAsync();

                var roles = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var roleDetail = roles.Select(r => new RoleResponse
                {
                    Id = r.Id,
                    RoleName = r.RoleName,
                    Deleted = r.Deleted
                });

                var response = new Paginate<RoleResponse>
                {
                    Items = roleDetail,
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                return response;
            }catch(Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleResponse>> getRole(int id)
        {
            try
            {
                var role = await _context.Roles.FindAsync(id);

                if(role == null)
                {
                    return NotFound($"Role with id {id} not found!");
                }

                var response = new RoleResponse
                {
                    Id = role.Id,
                    RoleName = role.RoleName,
                    Deleted = role.Deleted
                };

                return response;
            }catch(Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpPost("Add-Role")]
        public async Task<ActionResult<RoleResponse>> createRole([FromBody] RoleRequest request)
        {
            try
            {
                if(await _context.Roles.AnyAsync(r => r.RoleName == request.RoleName))
                {
                    return BadRequest("Role already added!");
                }

                var role = new Role
                {
                    RoleName = request.RoleName,
                };

                _context.Roles.Add(role);
                await _context.SaveChangesAsync();

                var response = new RoleResponse
                {
                    Id = role.Id,
                    RoleName = role.RoleName,
                    Deleted = role.Deleted
                };

                return response;
            }catch(Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RoleResponse>> updateRole(int id, [FromBody] RoleRequest request)
        {
            try
            {
                var role = await _context.Roles.FindAsync(id);
                
                if (role == null)
                {
                    return NotFound($"Role with id {id} not found!");
                }

                role.RoleName = request.RoleName ?? role.RoleName;

                await _context.SaveChangesAsync();
                var response = new RoleResponse
                {
                    Id = role.Id,
                    RoleName = role.RoleName,
                    Deleted = role.Deleted
                };

                return response;
            }catch(Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<RoleResponse>> deleteRole(int id)
        {
            try
            {
                var role = await _context.Roles.FindAsync(id);

                if (role == null)
                {
                    return NotFound($"Role with id {id} not found!");
                }

                role.Deleted = true;

                await _context.SaveChangesAsync();

                var response = new RoleResponse
                {
                    Id = role.Id,
                    RoleName = role.RoleName,
                    Deleted = role.Deleted
                };
                return response;
            }catch(Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
