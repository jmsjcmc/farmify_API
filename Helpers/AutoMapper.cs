using AutoMapper;
using Farmify_Api.Models;

namespace Farmify_Api.Helpers
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {


            CreateMap<UserRequest, User>();
            CreateMap<User, UserResponse>();
            CreateMap<User, UserDetail>();


        }
    }
}
