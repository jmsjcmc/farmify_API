using Farmify_Api.Models;

namespace Farmify_Api.Interfaces
{
    public interface IProductService
    {
        Task<Pagination<ProductResponse>> paginatedproducts(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null);
        Task<ProductResponse> getproduct(int id);
        Task<ProductResponse> createproduct(ProductRequest request);
        Task<ProductResponse> updateproduct(ProductRequest request, int id);
        Task<ProductResponse> hideproduct(int id);
        Task<ProductResponse> deleteproduct(int id);
    }
}
