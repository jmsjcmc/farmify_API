using AutoMapper;
using Farmify_Api.Helpers;
using Farmify_Api.Helpers.Queries;
using Farmify_Api.Interfaces;
using Farmify_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Farmify_Api.Services
{
    public class FarmService : BaseService, IFarmService
    {
        private readonly FarmQueries _query;
        public FarmService(AppDbContext context,IMapper mapper, FarmQueries query) : base (context, mapper)
        {
            _query = query;
        }
        // [HttpGet("farms")]
        public async Task<Pagination<FarmResponse>> paginatedfarms(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null)
        {
            var query =  _query.farmquery(searchTerm);
            return await PaginationHelper.paginateandmap<Farm, FarmResponse>(query, pageNumber, pageSize, _mapper);
        }
        // [HttpGet("farm/{id}")]
        public async Task<FarmResponse> getfarm(int id)
        {
            var farm = _query.getmethodfarmid(id);
            return _mapper.Map<FarmResponse>(farm);
        }
        // [HttpPost("farm")]
        public async Task<FarmResponse> addfarm(FarmRequest request)
        {
            var farm =  _mapper.Map<Farm>(request);
            
            _context.Farms.Add(farm);
            await _context.SaveChangesAsync();

            return await farmResponse(farm.Id);
        }
        // [HttpPatch("farm/update/{id}")]
        public async Task<FarmResponse> updatefarm (FarmRequest request, int id)
        {
            var farm = await patchfarmid(id);

            _mapper.Map(request, farm);

            await _context.SaveChangesAsync();

            return await farmResponse(farm.Id);
        }
        // [HttpPatch("farm/hide/{id}")]
        public async Task<FarmResponse> hidefarm (int id)
        {
            var farm = await patchfarmid(id);

            farm.Removed = true;

            await _context.SaveChangesAsync();

            return await farmResponse(farm.Id);
        }
        // [HttpDelete("farm/delete/{id}")]
        public async Task<FarmResponse> deletefarm(int id)
        {
            var farm = await patchfarmid(id);

            _context.Farms.Remove(farm);
            await _context.SaveChangesAsync();
            
            return await farmResponse(farm.Id);
        }
        // Helpers
        private async Task<Farm?> patchfarmid(int id)
        {
            return await _query.patchmethodfarmid(id);
        }
        private async Task<Farm?> getfarmid(int id)
        {
            return await _query.getmethodfarmid(id);
        }
        private async Task<FarmResponse> farmResponse(int id)
        {
            var response = await getfarmid(id);
            return _mapper.Map<FarmResponse>(response);
        }
    }
}
