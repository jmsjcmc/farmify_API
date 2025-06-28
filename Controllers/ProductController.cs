using AutoMapper;
using Farmify_Api.Helpers;
using Farmify_Api.Models;
using Farmify_Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Farmify_Api.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly ProductService _service;
        public ProductController(ProductService service, AppDbContext context, IMapper mapper) : base (context, mapper)
        {
            _service = service;
        }
        // Fetch all products with optional filter for product name (paginated)
        [HttpGet("products")]
        public async Task<ActionResult<Pagination<ProductResponse>>> paginatedproducts(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string searchTerm = null)
        {
            try
            {
                var response = await _service.paginatedproducts(pageNumber, pageSize, searchTerm);
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Fetch specific product
        [HttpGet("product/{id}")]
        public async Task<ActionResult<ProductResponse>> getproduct(int id)
        {
            try
            {
                var response = await _service.getproduct(id);
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Create new product
        [HttpPost("product")]
        public async Task<ActionResult<ProductResponse>> createproduct([FromBody] ProductRequest request)
        {
            try
            {
                var response = await _service.createproduct(request);
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Update specific product
        [HttpPatch("product/update/{id}")]
        public async Task<ActionResult<ProductResponse>> updateproduct([FromBody] ProductRequest request, int id)
        {
            try
            {
                var response = await _service.updateproduct(request, id);
                return response;
            } catch (Exception e)
            {
                return handleexception (e);
            }
        }
        // Remove specific product without deleting in database (soft delete)
        [HttpPatch("product/hide/{id}")]
        public async Task<ActionResult<ProductResponse>> hideproduct(int id)
        {
            try
            {
                var response = await _service.hideproduct(id);
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Delete specific product from database
        [HttpDelete("product/delete/{id}")]
        public async Task<ActionResult> deleteproduct(int id)
        {
            try
            {
                await _service.deleteproduct(id);
                return Ok("Success.");
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
    }
}
