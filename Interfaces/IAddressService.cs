using Farmify_Api.Models.Address;
using Microsoft.AspNetCore.Mvc;

namespace Farmify_Api.Interfaces
{
    public interface IAddressService
    {
        // Island
        Task<List<IslandResponse>> allislands();
        Task<IslandResponse> getisland(int id);
        Task<IslandResponse> createisland(IslandRequest request);
        Task<IslandResponse> updateisland(IslandRequest request, int id);
        Task deleteisland(int id);
        // Region
        Task<List<RegionResponse>> allregions();
        Task<RegionResponse> getregion(int id);
        Task<RegionResponse> createregion(RegionRequest request);
        Task<RegionResponse> updateregion(RegionRequest request, int id);
        Task deleteregion(int id);
        // Province
        Task<List<ProvinceResponse>> allprovinces();
        Task<ProvinceResponse> getprovince(int id);
        Task<ProvinceResponse> createprovince(ProvinceRequest request);
        Task<ProvinceResponse> updateprovince(ProvinceRequest request, int id);
        Task deleteprovince (int id);
        // City / Municipality
        Task<List<CityMunicipalityResponse>> allcitymunicipalities();
        Task<CityMunicipalityResponse> getcitymunicipality(int id);
        Task<CityMunicipalityResponse> createcitymunicipality(CityMunicipalityRequest request);
        Task<CityMunicipalityResponse> updatecitymunicipality(CityMunicipalityRequest request, int id);
        Task deletecitymunicipality(int id);
        // Barangay
        Task<List<BarangayResponse>> allbarangays();
        Task<BarangayResponse> getbarangay(int id);
        Task<BarangayResponse> createbarangay(BarangayRequest request);
        Task<BarangayResponse> updatebarangay(BarangayRequest request, int id);
        Task deletebarangay (int id);
    }
}
