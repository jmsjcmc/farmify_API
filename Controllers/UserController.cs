using AutoMapper;
using Farmify_Api.Helpers;
using Farmify_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Farmify_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserController(AppDbContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpGet("All-Users")]
        public async Task<ActionResult<Paginate<UserResponse>>> allUsers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? searchTerm = null)
        {
            try
            {
                var query = _context.Users
                    .AsNoTracking()
                    .OrderByDescending(d => d.Id)
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

                var userDetails = _mapper.Map<List<UserResponse>>(users);

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

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse>> getUser(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);

                if(user == null)
                {
                    return NotFound($"User with id {id} not found!");
                }

                var response = _mapper.Map<UserResponse>(user);
                return response;
            }catch(Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpGet("User-Detail")]
        public async Task<ActionResult<UserDetail>> userDetail()
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if(userIdClaim == null)
                {
                    return Unauthorized("User unauthorized!");
                }

                int id = int.Parse(userIdClaim.Value);
                var user = await _context.Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
                {
                    return NotFound("User not found!");
                }

                var response = _mapper.Map<UserDetail>(user);

                return response;
            }catch(Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserLoginResponse>> login(Login request)
        {
            try
            {
                var user = await _context.Users
                    .SingleOrDefaultAsync(u => u.Username == request.Username);

                if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                {
                    return Unauthorized("User not authenticated!");
                }

                var accessToken = GenerateAccessToken(user);

                await _context.SaveChangesAsync();

                var response = new UserLoginResponse
                {
                    accessToken = accessToken,
                    Userid = user.Id,
                    Username = user.Username,
                    Role = user.Role
                };

                return response;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpPost("Add-User")]
        public async Task<ActionResult<UserResponse>> createUser([FromBody] UserRequest request)
        {
            try
            {
                if(await _context.Users.AnyAsync(u => u.Username == request.Username))
                {
                    return BadRequest("Username is already taken!");
                }

                var requestRole = request.Role
                    .Select(r => r.Trim())
                    .ToList();

                var existingRoles = await _context.Roles
                    .Where(r => requestRole.Contains(r.RoleName))
                    .ToListAsync();

                if(existingRoles.Count != requestRole.Count)
                {
                    return BadRequest("One or more selected roles don't exist!");
                }

                var roleName = string.Join(", ", existingRoles.Select(r => r.RoleName).OrderBy(r => r));

                var user = new User
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Username = request.Username,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    Email = request.Email,
                    Role = roleName,
                    Createdon = DateTimeHelper.GetPhilippineStandardTime()
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                var response = new UserResponse
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    Password = user.Password,
                    Email = user.Email,
                    Role = user.Role
                };
                return response;

            }catch(Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserResponse>> updateUser(int id, [FromBody] UserRequest request)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);

                if (user == null)
                {
                    return NotFound($"User with id {id} not found!");
                }

                if (await _context.Users.AnyAsync(u => u.Username == request.Username))
                {
                    return BadRequest("Username already used!");
                }

                var requestRoles = request.Role
                    .Select(r => r.Trim())
                    .ToList();

                var existingRoles = await _context.Roles
                    .Where(r => requestRoles.Contains(r.RoleName))
                    .ToListAsync();

                if (existingRoles.Count != requestRoles.Count)
                {
                    return BadRequest("One or more selected roles don't exist!");
                }

                var roleNames = string.Join(", ", existingRoles.Select(r => r.RoleName).OrderBy(r => r));

                user.FirstName = request.FirstName ?? user.FirstName;
                user.LastName = request.LastName ?? user.LastName;
                user.Username = request.Username ?? user.Username;
                user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password) ?? user.Password;
                user.Email = request.Email ?? user.Email;
                user.Role = roleNames ?? user.Role;

                var response = new UserResponse
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    Password = user.Password,
                    Email = user.Email,
                    Role = user.Role
                };

                return response;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        private string GenerateAccessToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(12),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
