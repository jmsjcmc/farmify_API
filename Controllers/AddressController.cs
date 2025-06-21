using AutoMapper;
using Farmify_Api.Helpers;
using Farmify_Api.Models.Address;
using Farmify_Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Farmify_Api.Controllers
{
    public class AddressController : BaseApiController
    {
        private readonly AddressService _service;
        public AddressController(AppDbContext context, IMapper mapper, AddressService service) : base (context, mapper)
        {
            _service = service;
        }
        // Fetch all island in list
        [HttpGet("islands")]
        public async Task<ActionResult<List<IslandResponse>>> islandlist()
        {
            try
            {
                var response = await _service.allislands();
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Fetch specific island
        [HttpGet("island/{id}")]
        public async Task<ActionResult<IslandResponse>> getisland(int id)
        {
            try
            {
                var response = await _service.getisland(id);
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Create island
        [HttpPost("island")]
        public async Task<ActionResult<IslandResponse>> createisland([FromBody] IslandRequest request)
        {
            try
            {
                var response = await _service.createisland(request);
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Update specific island
        [HttpPatch("island/update/{id}")]
        public async Task<ActionResult<IslandResponse>> updateisland([FromBody] IslandRequest request, int id)
        {
            try
            {
                var response = await _service.updateisland(request, id);
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Delete island in database
        [HttpDelete("island/delete/{id}")]
        public async Task<ActionResult> deleteisland(int id)
        {
            try
            {
                await _service.deleteisland(id);
                return Ok("Success.");
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Fetch all region in list
        [HttpGet("regions")]
        public async Task<ActionResult<List<RegionResponse>>> regionlist()
        {
            try
            {
                var response = await _service.allregions();
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Fetch specific region 
        [HttpGet("region/{id}")]
        public async Task<ActionResult<RegionResponse>> getregion(int id)
        {
            try
            {
                var response = await _service.getregion(id);
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Create region
        [HttpPost("region")]
        public async Task<ActionResult<RegionResponse>> createregion([FromBody] RegionRequest request)
        {
            try
            {
                var response = await _service.createregion(request);
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Update specific region
        [HttpPatch("region/update/{id}")]
        public async Task<ActionResult<RegionResponse>> updateregion([FromBody] RegionRequest request, int id)
        {
            try
            {
                var response = await _service.updateregion(request, id);
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Delete specific region in database
        [HttpDelete("region/delete/{id}")]
        public async Task<ActionResult> deleteregion(int id)
        {
            try
            {
                await _service.deleteregion(id);
                return Ok("Success");
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Fetch all provinces in list
        [HttpGet("provinces")]
        public async Task<ActionResult<List<ProvinceResponse>>> provinceslist()
        {
            try
            {
                var response = await _service.allprovinces();
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Fetch specific province
        [HttpGet("province/{id}")]
        public async Task<ActionResult<ProvinceResponse>> getprovince(int id)
        {
            try
            {
                var response = await _service.getprovince(id);
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Create province
        [HttpPost("province")]
        public async Task<ActionResult<ProvinceResponse>> createprovince([FromBody] ProvinceRequest request)
        {
            try
            {
                var response = await _service.createprovince(request);
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Update specific province
        [HttpPatch("province/update/{id}")]
        public async Task<ActionResult<ProvinceResponse>> updateprovince([FromBody] ProvinceRequest request, int id)
        {
            try
            {
                var response = await _service.updateprovince(request, id);
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Delete specific province in database
        [HttpDelete("province/delete/{id}")]
        public async Task<ActionResult> deleteprovince(int id)
        {
            try
            {
                await _service.deleteprovince(id);
                return Ok("Success.");
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Fetch all city/municipalities in list
        [HttpGet("city-municipalities")]
        public async Task<ActionResult<List<CityMunicipalityResponse>>> citymunicipalitieslist()
        {
            try
            {
                var response = await _service.allcitymunicipalities();
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Fetch specific city/municipality
        [HttpGet("city-municipality/{id}")]
        public async Task<ActionResult<CityMunicipalityResponse>> getcitymunicipality(int id)
        {
            try
            {
                var response = await _service.getcitymunicipality(id);
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Create city/municipality
        [HttpPost("city-municipality")]
        public async Task<ActionResult<CityMunicipalityResponse>> createcitymunicipality([FromBody] CityMunicipalityRequest request)
        {
            try
            {
                var response = await _service.createcitymunicipality(request);
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Update specific city/municipality
        [HttpPatch("city-municipality/update/{id}")]
        public async Task<ActionResult<CityMunicipalityResponse>> updatecitymunicipality([FromBody] CityMunicipalityRequest request, int id)
        {
            try
            {
                var response = await _service.updatecitymunicipality(request, id);
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Delete specific city/municipalit in database
        [HttpDelete("city-municipality/delete/{id}")]
        public async Task<ActionResult> deletecitymunicipality(int id)
        {
            try
            {
                await _service.deletecitymunicipality(id);
                return Ok("Success.");
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Fetch all barangays in list
        [HttpGet("barangays")]
        public async Task<ActionResult<List<BarangayResponse>>> barangayslist()
        {
            try
            {
                var response = await _service.allbarangays();
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Fetch specific barangay
        [HttpGet("barangay/{id}")]
        public async Task<ActionResult<BarangayResponse>> getbarangay(int id)
        {
            try
            {
                var response = await _service.getbarangay(id);
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Create barangay
        [HttpPost("barangay")]
        public async Task<ActionResult<BarangayResponse>> createbarangay([FromBody] BarangayRequest request)
        {
            try
            {
                var response = await _service.createbarangay(request);
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Update specific barangay 
        [HttpPatch("barangay/update/{id}")]
        public async Task<ActionResult<BarangayResponse>> updatebarangay([FromBody] BarangayRequest request, int id)
        {
            try
            {
                var response = await _service.updatebarangay(request, id);
                return response;
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
        // Delete specific barangay in database
        [HttpDelete("barangay/delete/{id}")]
        public async Task<ActionResult> deletebarangay(int id)
        {
            try
            {
                await _service.deletebarangay(id);
                return Ok("Success.");
            } catch (Exception e)
            {
                return handleexception(e);
            }
        }
    }
}
