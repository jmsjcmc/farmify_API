using AutoMapper;
using Farmify_Api.Models.Address;
using Farmify_Api.Models.User;

namespace Farmify_Api
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            // User Mapping
            CreateMap<UserRequest, User>()
                .ForMember(d => d.Password, o => o.Ignore())
                .ForMember(d => d.Dateupdated, o => o.Ignore())
                .ForMember(d => d.Datecreated, o => o.Ignore());

            CreateMap<User, UserResponse>();
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
            CreateMap<BarangayRequest, Barangay>();

            CreateMap<Barangay, BarangayResponse>();
        }
    }
}
