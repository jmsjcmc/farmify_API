using AutoMapper;
using Farmify_Api.Models.Address;

namespace Farmify_Api
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            // Island Mapping
            CreateMap<IslandRequest, Island>();

            CreateMap<Island, IslandResponse>();
            // Region Mapping
            CreateMap<RegionRequest, Region>();

            CreateMap<Region, RegionResponse>();
            // Province Mapping
            CreateMap<ProvinceRequest, Province>();

            CreateMap<Province, ProvinceResponse>();
            // City Municipality Mapping
            CreateMap<CityMunicipalityRequest, CityMunicipality>();

            CreateMap<CityMunicipality, CityMunicipalityResponse>();
            // Barangay Mapping
            CreateMap<BarangaRequest, Barangay>();

            CreateMap<Barangay, BarangayResponse>();
        }
    }
}
