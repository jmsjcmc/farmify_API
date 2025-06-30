using Farmify_Api.Models;

namespace Farmify_Api.Interfaces
{
    public interface IFarmService
    {
        Task<Pagination<FarmResponse>> paginatedfarms(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null);
        Task<FarmResponse> getfarm(int id);
        Task<FarmResponse> addfarm(FarmRequest request);
        Task<FarmResponse> updatefarm(FarmRequest request, int id);
        Task<FarmResponse> hidefarm(int id);
        Task<FarmResponse> deletefarm(int id);
    }
}
