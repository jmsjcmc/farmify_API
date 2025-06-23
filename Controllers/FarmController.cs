using AutoMapper;
using Farmify_Api.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Farmify_Api.Controllers
{
    public class FarmController : BaseApiController
    {
        public FarmController(AppDbContext context, IMapper mapper) : base (context, mapper)
        {
            
        }
    }
}
