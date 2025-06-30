using AutoMapper;
using Farmify_Api.Helpers;
using Farmify_Api.Models;
using Farmify_Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Farmify_Api.Controllers
{
    public class FarmController : BaseApiController
    {
        private readonly FarmService _service;
        public FarmController(AppDbContext context, IMapper mapper, FarmService service) : base (context, mapper)
        {
            _service = service;
        }
        // Fetch all farms with optional filter for farm name
        [HttpGet("farms")]
        public async Task<ActionResult<Pagination<FarmResponse>>> paginatedfarms(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null)
        {
            try
            {
                var response = await _service.paginatedfarms(pageNumber, pageSize, searchTerm);
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Fetch specific farm
        [HttpGet("farm/{id}")]
        public async Task<ActionResult<FarmResponse>> getfarm(int id)
        {
            try
            {
                var response = await _service.getfarm(id);
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Create farm
        [HttpPost("farm")]
        public async Task<ActionResult<FarmResponse>> createfarm([FromBody] FarmRequest request)
        {
            try
            {
                var response = await _service.addfarm(request);
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Update specific farm
        [HttpPatch("farm/update/{id}")]
        public async Task<ActionResult<FarmResponse>> updatefarm([FromBody] FarmRequest request, int id)
        {
            try
            {
                var response = await _service.updatefarm(request, id);
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Remove specific farm without removing in database (soft delete)
        [HttpPatch("farm/hide/{id}")]
        public async Task<ActionResult<FarmResponse>> hidefarm(int id)
        {
            try
            {
                var response = await _service.hidefarm(id);
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Delete specific farm in database
        [HttpDelete("farm/delete/{id}")]
        public async Task<ActionResult<FarmResponse>> deletefarm(int id)
        {
            try
            {
                var response = await _service.deletefarm(id);
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
    }
}
