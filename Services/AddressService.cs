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
        // [HttpGet("islands")]
        public async Task<List<IslandResponse>> allislands()
        {
            var islands = await _query.islandlist();
            return _mapper.Map<List<IslandResponse>>(islands);
        }
        // [HttpGet("island/{id}")]
        public async Task<IslandResponse> getisland(int id)
        {
            var island = await _query.getmethodislandid(id);
            return _mapper.Map<IslandResponse>(island);
        }
        // [HttpPost("island")]
        public async Task<IslandResponse> createisland(IslandRequest request)
        {
            var island = _mapper.Map<Island>(request);

            _context.Islands.Add(island);
            await _context.SaveChangesAsync();

            var savedisland = _query.getmethodislandid(island.Id);
            return _mapper.Map<IslandResponse>(savedisland);
        }
        // [HttpPatch("island/update/{id}")]
        public async Task<IslandResponse> updateisland(IslandRequest request, int id)
        {
            var island = await _query.patchmethodislandid(id);

            _mapper.Map(request, island);
            await _context.SaveChangesAsync();

            var updatedisland = await _query.getmethodislandid(island.Id);
            return _mapper.Map<IslandResponse>(updatedisland);
        }
        // [HttpDelete("island/delete/{id}")]
        public async Task deleteisland (int id)
        {
            var island = await _query.patchmethodislandid(id);
            _context.Islands.Remove(island);
            await _context.SaveChangesAsync();
        }
        // [HttpGet("regions")]
        public async Task<List<RegionResponse>> allregions()
        {
            var regions = await _query.regionlist();
            return _mapper.Map<List<RegionResponse>>(regions);
        }
        // [HttpGet("region/{id}")]
        public async Task<RegionResponse> getregion(int id)
        {
            var region = await _query.getmethodregionid(id);
            return _mapper.Map<RegionResponse>(region);
        }
        // [HttpPost("region")]
        public async Task<RegionResponse> createregion(RegionRequest request)
        {
            var region = _mapper.Map<Region>(request);

            _context.Regions.Add(region);
            await _context.SaveChangesAsync();

            var savedregion = await _query.getmethodregionid(region.Id);
            return _mapper.Map<RegionResponse>(savedregion);
        }
        // [HttpPatch("region/update/{id}")]
        public async Task<RegionResponse> updateregion(RegionRequest request, int id)
        {
            var region = await _query.patchmethodislandid(id);
            _mapper.Map(request, region);

            await _context.SaveChangesAsync();

            var updatedregion = await _query.getmethodregionid(region.Id);
            return _mapper.Map<RegionResponse>(updatedregion);
        }
        // [HttpDelete("region/delete/{id}")]
        public async Task deleteregion(int id)
        {
            var region = await _query.patchmethodregionid(id);
            _context.Regions.Remove(region);
            await _context.SaveChangesAsync();
        }
        // [HttpGet("provinces")]
        public async Task<List<ProvinceResponse>> allprovinces()
        {
            var provinces = await _query.provincelist();
            return _mapper.Map<List<ProvinceResponse>>(provinces);
        }
        // [HttpGet("province/{id}")]
        public async Task<ProvinceResponse> getprovince(int id)
        {
            var province = await _query.getmethodprovinceid(id);
            return _mapper.Map<ProvinceResponse>(province);
        }
        // [HttpPost("province")]
        public async Task<ProvinceResponse> createprovince(ProvinceRequest request)
        {
            var province = _mapper.Map<Province>(request);

            _context.Provinces.Add(province);
            await _context.SaveChangesAsync();

            var savedprovince = await _query.getmethodprovinceid(province.Id);
            return _mapper.Map<ProvinceResponse>(savedprovince);
        }
        // [HttpPatch("province/update/{id}")]
        public async Task<ProvinceResponse> updateprovince(ProvinceRequest request, int id)
        {
            var province = await _query.patchmethodprovinceid(id);

            _mapper.Map(request, province);

            await _context.SaveChangesAsync();

            var updatedprovince = await _query.getmethodprovinceid(province.Id);
            return _mapper.Map<ProvinceResponse>(updatedprovince);
        }
        // [HttpDelete("province/delete/{id}")]
        public async Task deleteprovince(int id)
        {
            var province = await _query.patchmethodprovinceid(id);

            _context.Provinces.Remove(province);
            await _context.SaveChangesAsync();
        }
        // [HttpGet("city-municipalities")]
        public async Task<List<CityMunicipalityResponse>> allcitymunicipalities()
        {
            var city = await _query.citymunicipalitylist();
            return _mapper.Map<List<CityMunicipalityResponse>>(city);
        }
        // [HttpGet("city-municipality/{id}")]
        public async Task<CityMunicipalityResponse> getcitymunicipality(int id)
        {
            var city = await _query.getmethodcitymunicipalityid(id);
            return _mapper.Map<CityMunicipalityResponse>(city);
        }
        // [HttpPost("city-municipality")]
        public async Task<CityMunicipalityResponse> createcitymunicipality(CityMunicipalityRequest request)
        {
            var city = _mapper.Map<CityMunicipality>(request);

            _context.CityMunicipalities.Add(city);
            await _context.SaveChangesAsync();

            var savedcity = await _query.getmethodcitymunicipalityid(city.Id);
            return _mapper.Map<CityMunicipalityResponse>(savedcity);
        }
        // [HttpPatch("city-municipality/update/{id}")]
        public async Task<CityMunicipalityResponse> updatecitymunicipality(CityMunicipalityRequest request, int id)
        {
            var city = await _query.patchmethodcitymunicipalityid(id);
            _mapper.Map(request, city);

            await _context.SaveChangesAsync();

            var updatedcity = await _query.getmethodcitymunicipalityid(city.Id);
            return _mapper.Map<CityMunicipalityResponse>(updatedcity);
        }
        // [HttpDelete("city-municipality/delete/{id}")]
        public async Task deletecitymunicipality(int id)
        {
            var city = await _query.patchmethodcitymunicipalityid(id);

            _context.CityMunicipalities.Remove(city);
            await _context.SaveChangesAsync();
        }
        // [HttpGet("barangays")]
        public async Task<List<BarangayResponse>> allbarangays()
        {
            var barangays = await _query.barangaylist();
            return _mapper.Map<List<BarangayResponse>>(barangays);
        }
        // [HttpGet("barangay/{id}")]
        public async Task<BarangayResponse> getbarangay(int id)
        {
            var barangay = await _query.getmethodbarangayid(id);
            return _mapper.Map<BarangayResponse>(barangay);
        }
        // [HttpPost("barangay")]
        public async Task<BarangayResponse> createbarangay(BarangayRequest request)
        {
            var barangay = _mapper.Map<Barangay>(request);

            _context.Barangays.Add(barangay);
            await _context.SaveChangesAsync();

            var savedbarangay = await _query.getmethodbarangayid(barangay.Id);
            return _mapper.Map<BarangayResponse>(savedbarangay);
        }
        // [HttpPatch("barangay/update/{id}")]
        public async Task<BarangayResponse> updatebarangay(BarangayRequest request, int id)
        {
            var barangay = await _query.patchmethodbarangayid(id);

            _mapper.Map(request, barangay);

            await _context.SaveChangesAsync();

            var updatedbarangay = await _query.getmethodbarangayid(barangay.Id);
            return _mapper.Map<BarangayResponse>(updatedbarangay);
        }
        // [HttpDelete("barangay/delete/{id}")]
        public async Task deletebarangay (int id)
        {
            var barangay = await _query.patchmethodbarangayid(id);

            _context.Barangays.Remove(barangay);
            await _context.SaveChangesAsync();
        }
    }
}
