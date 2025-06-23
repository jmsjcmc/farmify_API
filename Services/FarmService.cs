using AutoMapper;
using Farmify_Api.Helpers;
using Farmify_Api.Helpers.Queries;
using Farmify_Api.Interfaces;
using Farmify_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Farmify_Api.Services
{
    public class FarmService : IFarmService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly FarmQueries _query;
        public FarmService(AppDbContext context,IMapper mapper, FarmQueries query)
        {
            _context = context;
            _mapper = mapper;
            _query = query;
        }
        // 
        public async Task<Pagination<FarmResponse>> paginatedfarms(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null)
        {
            var query = _query.farmquery(searchTerm);
            var totalcount = await query.CountAsync();

            var farms = await PaginationHelper.paginateandproject<Farm, FarmResponse>(
                query, pageNumber, pageSize, _mapper);

            return PaginationHelper.paginatedresponse(farms, totalcount, pageNumber, pageSize);
        }
        // 
        public async Task<FarmResponse> getfarm(int id)
        {
            var farm = _query.getmethodfarmid(id);
            return _mapper.Map<FarmResponse>(farm);
        }
        //
        public async Task<FarmResponse> addfarm(FarmRequest request)
        {
            var farm = _mapper.Map<Farm>(request);
            
            _context.Farms.Add(farm);
            await _context.SaveChangesAsync();

            var savedfarm = await _query.getmethodfarmid(farm.Id);
            return _mapper.Map<FarmResponse>(savedfarm);
        }
        // 
        public async Task<FarmResponse> updatefarm (FarmRequest request, int id)
        {
            var farm = _query.patchmethodfarmid(id);

            _mapper.Map(request, farm);

            await _context.SaveChangesAsync();

            var updatedfarm = _query.getmethodfarmid(farm.Id);
            return _mapper.Map<FarmResponse>(updatedfarm);
        }
    }
}
