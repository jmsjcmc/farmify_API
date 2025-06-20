using Farmify_Api.Models.Address;
using Microsoft.AspNetCore.Mvc;

namespace Farmify_Api.Interfaces
{
    public interface IAddressService
    {
        Task<List<IslandResponse>> allislands();
        Task<IslandResponse> getisland(int id);
        Task<IslandResponse> createisland(IslandRequest request);
        Task<IslandResponse> updateisland(IslandRequest request, int id);
        Task deleteisland(int id);
        Task<List<RegionResponse>> allregions();
        Task<RegionResponse> getregion(int id);
        Task<RegionResponse> createregion(RegionRequest request);
        Task<RegionResponse> updateregion(RegionRequest request, int id);
        Task deleteregion(int id);
        Task<List<ProvinceResponse>> allprovinces();
        Task<ProvinceResponse> getprovince(int id);
        Task<ProvinceResponse> createprovince(ProvinceRequest request);
        Task<ProvinceResponse> updateprovince(ProvinceRequest request, int id);
        Task deleteprovince (int id);
        Task<List<CityMunicipalityResponse>> allcitymunicipalities();
        Task<CityMunicipalityResponse> getcitymunicipality(int id);
        Task<CityMunicipalityResponse> createcitymunicipality(CityMunicipalityRequest request;
        Task<CityMunicipalityResponse> updatecitymunicipality(CityMunicipalityRequest request, int id);
        Task deletecitymunicipality(int id);
        Task<List<BarangayResponse>> allbarangays();
        Task<BarangayResponse> getbarangay(int id);
        Task<BarangayResponse> createbarangay(CityMunicipalityRequest request);
        Task<BarangayResponse> updatebarangay(CityMunicipalityRequest, int id);
        Task deletebarangay (int id);

    }
}
