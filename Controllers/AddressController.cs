using Farmify_Api.Helpers;
using Farmify_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Farmify_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AddressController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("Add-Address")]
        public async Task<ActionResult<AddressResponse>> addAddress([FromBody] AddressRequest request)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
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
                var address = new Address
                {
                    Userid = user.Id,
                    HomeAddress = request.HomeAddress,
                    Street = request.Street,
                    Baranggay = request.Baranggay,
                    City = request.City,
                    Province = request.Province,
                    Zipcode = request.Zipcode
                };

                var response = new AddressResponse
                {
                    Id = address.Id,
                    Userid = address.Userid,
                    HomeAddress = address.HomeAddress,
                    Street = address.Street,
                    Baranggay = address.Baranggay,
                    City = address.City,
                    Province = address.Province,
                    Zipcode = address.Zipcode,
                    Primary = address.Primary
                };
                return response;
            }catch(Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
