using AutoMapper;
using Farmify_Api.Helpers;
using Farmify_Api.Models;
using Farmify_Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Farmify_Api.Controllers
{
    public class CategoryController : BaseApiController
    {
        private readonly CategoryService _service;
        public CategoryController(AppDbContext context, IMapper mapper, CategoryService service) : base (context, mapper)
        {
            _service = service;
        }
        // Fetch all categories in list
        [HttpGet("categories")]
        public async Task<ActionResult<List<CategoryResponse>>> categorieslist(string? searchTerm = null)
        {
            try
            {
                var response = await _service.categorieslist(searchTerm);
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Fetch specific category
        [HttpGet("category/{id}")]
        public async Task<ActionResult<CategoryResponse>> getcategory(int id)
        {
            try
            {
                var response = await _service.getcategory(id);
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Create new category
        [HttpPost("category")]
        public async Task<ActionResult<CategoryResponse>> createcategory([FromBody] CategoryRequest request)
        {
            try
            {
                var response = await _service.createcategory(request);
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Update specific category
        [HttpPatch("category/update/{id}")]
        public async Task<ActionResult<CategoryResponse>> updatecategory([FromBody] CategoryRequest request, int id)
        {
            try
            {
                var response = await _service.updatecategory(request, id);
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Delete specific category in database
        [HttpDelete("category/delete/{id}")]
        public async Task<ActionResult> deletecategory(int id)
        {
            try
            {
                await _service.deletecategory(id);
                return Ok("Success.");
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
    }
}
