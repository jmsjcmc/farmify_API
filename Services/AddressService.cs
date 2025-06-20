using AutoMapper;
using Farmify_Api.Helpers.Queries;
using Farmify_Api.Interfaces;
using Farmify_Api.Models.Address;

namespace Farmify_Api.Services
{
    public class AddressService : IAddressService
    {
        private readonly AddressQueries _query;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public AddressService(AppDbContext context, IMapper mapper, AddressQueries query)
        {
            _context = context;
            _mapper = mapper;
            _query = query;
        }
        // 
        public async Task<List<IslandResponse>> allislands()
        {
            var islands = _query.islandlist();
            return _mapper.Map<List<IslandResponse>>(islands);
        }
        //
        public async Task<IslandResponse> getisland(int id)
        {
            var island = await _query.getmethodislandid(id);
            return _mapper.Map<IslandResponse>(island);
        }
        //
        public async Task<IslandResponse> createisland(IslandRequest request)
        {
            var island = _mapper.Map<Island>(request);
            _context.Islands.Add(island);
            await _context.SaveChangesAsync();

            var savedisland = _query.getmethodislandid(island.Id);
            return _mapper.Map<IslandResponse>(savedisland);
        }
        //
        public async Task<IslandResponse> updateisland(IslandRequest request, int id)
        {
            var island = await _query.patchmethodislandid(id);

            _mapper.Map(request, island);
            await _context.SaveChangesAsync();

            var updatedisland = await _query.getmethodislandid(island.Id);
            return _mapper.Map<IslandResponse>(updatedisland);
        }
        //
        public async Task deleteisland (int id)
        {
            var island = await _query.patchmethodislandid(id);
            _context.Islands.Remove(island);
            await _context.SaveChangesAsync();
        }
        //
        public async Task<List<RegionResponse>> allregions()
        {
            var regions = _query.regionlist();
            return _mapper.Map<List<RegionResponse>>(regions);
        }
        //
        public async Task<RegionResponse> getregion(int id)
        {
            var region = await _query.getmethodregionid(id);
            return _mapper.Map<RegionResponse>(region);
        }

    }
}
