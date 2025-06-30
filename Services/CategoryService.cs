using AutoMapper;
using Farmify_Api.Helpers;
using Farmify_Api.Helpers.Queries;
using Farmify_Api.Interfaces;
using Farmify_Api.Models;

namespace Farmify_Api.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly CategoryQueries _query;
        public CategoryService(AppDbContext context, IMapper mapper, CategoryQueries query) : base (context, mapper)
        {
            _query = query;
        }
        // [HttpGet("categories")]
        public async Task<List<CategoryResponse>> categorieslist(string? searchTerm = null)
        {
            var category = await _query.categorieslist(searchTerm);
            return _mapper.Map<List<CategoryResponse>>(category);
        }
        // [HttpGet("category/{id}")]
        public async Task<CategoryResponse> getcategory(int id)
        {
            var category = await _query.getmethodcategoryid(id);
            return _mapper.Map<CategoryResponse>(category);
        }
        // [HttpPost("category")]
        public async Task<CategoryResponse> createcategory(CategoryRequest request)
        {
            var category = _mapper.Map<Category>(request);

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return await categoryResponse(category.Id);
        }
        // [HttpPatch("category/update/{id}")]
        public async Task<CategoryResponse> updatecategory(CategoryRequest request, int id)
        {
            var category = await patchcategoryid(id);

            _mapper.Map(request, category);

            await _context.SaveChangesAsync();

            return await categoryResponse(category.Id);
        }
        // [HttpDelete("category/delete/{id}")]
        public async Task<CategoryResponse> deletecategory(int id)
        {
            var category = await patchcategoryid(id);

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return await categoryResponse(category.Id);
        }
        // Helper
        private async Task<Category?> patchcategoryid(int id)
        {
            return await _query.patchmethodcategoryid(id);
        }
        private async Task<Category?> getcategoryid(int id)
        {
            return await _query.getmethodcategoryid(id);
        }
        private async Task<CategoryResponse> categoryResponse(int id)
        {
            var response = await getcategoryid(id);
            return _mapper.Map<CategoryResponse>(response);
        }
    }
}
