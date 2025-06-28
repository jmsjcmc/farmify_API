using AutoMapper;
using Farmify_Api.Models;

namespace Farmify_Api.Helpers
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            // User Mapping
            CreateMap<UserRequest, User>()
                .ForMember(d => d.Password, o => o.Ignore())
                .ForMember(d => d.Dateupdated, o => o.Ignore())
                .ForMember(d => d.Datecreated, o => o.Ignore());

            CreateMap<User, UserResponse>();
            // Role Mapping 
            CreateMap<RoleRequest, Role>();

            CreateMap<Role, RoleResponse>();
            // Farm Mapping
            CreateMap<FarmRequest, Farm>()
                .ForMember(d => d.Removed, o => o.Ignore());

            CreateMap<Farm, FarmResponse>()
                .ForMember(d => d.User, o => o.MapFrom(s => s.User));
            // Category Mapping
            CreateMap<CategoryRequest, Category>();

            CreateMap<Category, CategoryResponse>();
            // Product Mapping
            CreateMap<ProductRequest, Product>();

            CreateMap<Product, ProductResponse>()
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category));
        }
    }
}
