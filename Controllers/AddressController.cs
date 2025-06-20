using AutoMapper;
using Farmify_Api.Helpers;
using Farmify_Api.Models.Address;
using Farmify_Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Farmify_Api.Controllers
{
    public class AddressController : BaseApiController
    {
        private readonly AddressService _service;
        public AddressController(AppDbContext context, IMapper mapper, AddressService service) : base (context, mapper)
        {
            _service = service;
        }
        //
        [HttpGet("islands")]
        public async Task<ActionResult<List<IslandResponse>>> islandlist()
        {
            try
            {
                var response = await _service.allislands();
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        //
        [HttpGet("island/{id}")]
        public async Task<ActionResult<IslandResponse>> getisland(int id)
        {
            try
            {
                var response = await _service.getisland(id);
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        //
        [HttpPost("island")]
        public async Task<ActionResult<IslandResponse>> createisland([FromBody] IslandRequest request)
        {
            try
            {
                var response = await _service.createisland(request);
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        //
        [HttpPatch("island/update/{id}")]
        public async Task<ActionResult<IslandResponse>> updateisland([FromBody] IslandRequest request, int id)
        {
            try
            {
                var response = await _service.updateisland(request, id);
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        //
        [HttpDelete("island/delete/{id}")]
        public async Task<ActionResult> deleteisland(int id)
        {
            try
            {
                await _service.deleteisland(id);
                return Ok("Success.");
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        //
    }
}
