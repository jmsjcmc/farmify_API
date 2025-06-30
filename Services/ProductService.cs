using AutoMapper;
using Farmify_Api.Helpers;
using Farmify_Api.Helpers.Queries;
using Farmify_Api.Interfaces;
using Farmify_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Farmify_Api.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly ProductQueries _query;
        public ProductService(AppDbContext context, IMapper mapper, ProductQueries query) : base (context, mapper)
        {
            _query = query;
        }
        // [HttpGet("products")]
        public async Task<Pagination<ProductResponse>> paginatedproducts(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null)
        {
            var query = _query.paginatedproducts(searchTerm);
            return await PaginationHelper.paginateandmap<Product, ProductResponse>(query, pageNumber, pageSize, _mapper);
        }
        // [HttpGet("product/{id}")]
        public async Task<ProductResponse> getproduct(int id)
        {
            var product = await _query.getmethodproductid(id);
            return _mapper.Map<ProductResponse>(product);
        }
        // [HttpPost("product")]
        public async Task<ProductResponse> createproduct(ProductRequest request)
        {
            var product = _mapper.Map<Product>(request);

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return await productResponse(product.Id);
        }
        // [HttpPatch("product/update/{id}")]
        public async Task<ProductResponse> updateproduct(ProductRequest request, int id)
        {
            var product = await patchproductid(id);

            _mapper.Map(request, product);
            await _context.SaveChangesAsync();

            return await productResponse(product.Id);
        }
        // [HttpPatch("product/hide/{id}")]
        public async Task<ProductResponse> hideproduct(int id)
        {
            var product = await patchproductid(id);

            product.Removed = true;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return await productResponse(product.Id);
        }
        // [HttpDelete("product/delete/{id}")]
        public async Task<ProductResponse> deleteproduct(int id)
        {
            var product = await patchproductid(id);

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return await productResponse(product.Id);
        }
        // Helpers 
        private async Task<Product?> patchproductid(int id)
        {
            return await _query.patchmethodproductid(id);
        }
        private async Task<Product?> getproductid(int id)
        {
            return await _query.getmethodproductid(id);
        }
        private async Task<ProductResponse> productResponse(int id)
        {
            var response = await getproductid(id);
            return _mapper.Map<ProductResponse>(response);
        }
    }
}
