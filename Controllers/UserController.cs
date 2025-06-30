using AutoMapper;
using Farmify_Api.Helpers;
using Farmify_Api.Models;
using Farmify_Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Farmify_Api.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly UserService _service;

        public UserController(AppDbContext context, IMapper mapper, UserService service) : base (context, mapper)
        {
            _service = service;
        }
        // Fetch all users (paginated)
        [HttpGet("users")]
        public async Task<ActionResult<Pagination<UserResponse>>> paginatedusers(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null)
        {
            try
            {
                var response = await _service.paginatedusers(pageNumber, pageSize, searchTerm);
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Fetch specific user
        [HttpGet("user/{id}")]
        public async Task<ActionResult<UserResponse>> getuser(int id)
        {
            try
            {
                var response = await _service.getuser(id);
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Fetch details for authenticated user
        [HttpGet("user-detail")]
        public async Task<ActionResult<UserResponse>> getuserdetail()
        {
            try
            {
                var response = await _service.getuserdetail(User);
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // User login
        [HttpPost("login")]
        public async Task<ActionResult> login(string username, string password)
        {
            try
            {
                var response = await _service.login(username, password);
                return Ok(response);
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Create user
        [HttpPost("user")]
        public async Task<ActionResult<UserResponse>> createuser([FromBody] UserRequest request)
        {
            try
            {
                var response = await _service.createuser(request);
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Update specific user
        [HttpPatch("user/update/{id}")]
        public async Task<ActionResult<UserResponse>> updateuser([FromBody] UserRequest request, int id)
        {
            try
            {
                var response = await _service.updateuser(request, id);
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Remove specific user (softdelete)
        [HttpPatch("user/hide/{id}")]
        public async Task<ActionResult<UserResponse>> hideuser(int id)
        {
            try
            {
                var response = await _service.hideuser(id);
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Delete specific user in database
        [HttpDelete("user/delete/{id}")]
        public async Task<ActionResult<UserResponse>> deleteuser(int id)
        {
            try
            {
                var  response = await _service.deleteuser(id);  
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
    }
}
