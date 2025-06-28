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

            var savedCategory = await _query.getmethodcategoryid(category.Id);
            return _mapper.Map<CategoryResponse>(savedCategory);
        }
        // [HttpPatch("category/update/{id}")]
        public async Task<CategoryResponse> updatecategory(CategoryRequest request, int id)
        {
            var category = await getcategoryid(id);

            _mapper.Map(request, category);

            await _context.SaveChangesAsync();

            var updatedCategory = await _query.getmethodcategoryid(category.Id);
            return _mapper.Map<CategoryResponse>(updatedCategory);
        }
        // [HttpDelete("category/delete/{id}")]
        public async Task deletecategory(int id)
        {
            var category = await getcategoryid(id);

            _context.Categories.Remove(category);
        }
        // Helper
        private async Task<Category?> getcategoryid(int id)
        {
            return await _query.patchmethodcategoryid(id);
        }
    }
}
