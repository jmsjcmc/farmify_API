using Farmify_Api.Models;

namespace Farmify_Api.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> categorieslist(string? searchTerm = null);
        Task<CategoryResponse> getcategory(int id);
        Task<CategoryResponse> createcategory(CategoryRequest request);
        Task<CategoryResponse> updatecategory(CategoryRequest request, int id);
        Task<CategoryResponse> deletecategory(int id);
    }
}
