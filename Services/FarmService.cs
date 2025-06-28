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
            var totalcount = await query.CountAsync();

            var farms = await PaginationHelper.paginateandproject<Farm, FarmResponse>(
                query, pageNumber, pageSize, _mapper);

            return PaginationHelper.paginatedresponse(farms, totalcount, pageNumber, pageSize);
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

            var savedfarm = await _query.getmethodfarmid(farm.Id);
            return _mapper.Map<FarmResponse>(savedfarm);
        }
        // [HttpPatch("farm/update/{id}")]
        public async Task<FarmResponse> updatefarm (FarmRequest request, int id)
        {
            var farm = await _query.patchmethodfarmid(id);

            _mapper.Map(request, farm);

            await _context.SaveChangesAsync();

            var updatedfarm = _query.getmethodfarmid(farm.Id);
            return _mapper.Map<FarmResponse>(updatedfarm);
        }
        // [HttpPatch("farm/hide/{id}")]
        public async Task<FarmResponse> hidefarm (int id)
        {
            var farm = await _query.patchmethodfarmid(id);

            farm.Removed = true;

            await _context.SaveChangesAsync();

            var updatedfarm = await _query.getmethodfarmid(farm.Id);
            return _mapper.Map<FarmResponse>(updatedfarm);
        }
        // [HttpDelete("farm/delete/{id}")]
        public async Task deletefarm(int id)
        {
            var farm = await _query.patchmethodfarmid(id);

            _context.Farms.Remove(farm);
            await _context.SaveChangesAsync();  
        }
    }
}
