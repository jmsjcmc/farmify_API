﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Farmify_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Farmify_Api.Helpers
{
    public static class PaginationHelper
    {
        public static async Task<List<TDestination>> paginateandproject<TSource, TDestination>(
            IQueryable<TSource> query,
            int pageNumber,
            int pageSize,
            IMapper mapper)
        {
            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<TDestination>(mapper.ConfigurationProvider)
                .ToListAsync();
        }
        public static Pagination<T> paginatedresponse<T>(
            List<T> items,
            int totalCount,
            int pageNumber, 
            int pageSize)
        {
            return new Pagination<T>
            {
                Items = items,
                Totalcount = totalCount,
                Pagenumber = pageNumber,
                Pagesize = pageSize
            };
        }
        public static async Task<Pagination<TDestination>> paginateandmap<TSource, TDestination>(
            IQueryable<TSource> query,
            int pageNumber, 
            int pageSize,
            IMapper mapper)
        {
            var totalCount = await query.CountAsync();
            var items = await paginateandproject<TSource, TDestination>(query, pageNumber, pageSize, mapper);

            return paginatedresponse(items, totalCount, pageNumber, pageSize);
        }
    }
}
