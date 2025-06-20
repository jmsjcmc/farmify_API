using AutoMapper;
using Farmify_Api.Models.Address;
using Microsoft.EntityFrameworkCore;

namespace Farmify_Api.Helpers.Queries
{
    public class AddressQueries
    {
        private readonly AppDbContext _context;
        public AddressQueries(AppDbContext context)
        {
            _context = context;
        }
        // Query for fetching all island in list
        public async Task<List<Island>> islandlist()
        {
            return await _context.Islands
                .AsNoTracking()
                .OrderByDescending(i => i.Id)
                .ToListAsync();
        }
        // Query for fetching specific island for GET method
        public async Task<Island?> getmethodislandid(int id)
        {
            return await _context.Islands
                .AsNoTracking()
                .Include(i => i.Region)
                .FirstOrDefaultAsync(i => i.Id == id);
        }
        // Query for fetching specific island for PUT/PATCH/DELETE methods
        public async Task<Island?> patchmethodislandid(int id)
        {
            return await _context.Islands
                .Include(i => i.Region)
                .FirstOrDefaultAsync(i => i.Id == id);
        }
        // Query for fetching all region in list
        public async Task<List<Region>> regionlist()
        {
            return await _context.Regions
                .AsNoTracking()
                .OrderByDescending(r => r.Id)
                .ToListAsync();
        }
        // Query for fetching specific region for GET method
        public async Task<Region?> getmethodregionid(int id)
        {
            return await _context.Regions
                .AsNoTracking()
                .Include(r => r.Province)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
        // Query for fetching all province in list
        public async Task<List<Province>> provincelist()
        {
            return await _context.Provinces
                .AsNoTracking()
                .OrderByDescending(p => p.Id)
                .ToListAsync();
        }
        // Query for fetching all city municipality in list
        public async Task<List<CityMunicipality>> citymunicipalitylist()
        {
            return await _context.CityMunicipalities
                .AsNoTracking()
                .OrderByDescending(c => c.Id)
                .ToListAsync();
        }
        // Query for fetching all barangay in list
        public async Task<List<Barangay>> barangaylist()
        {
            return await _context.Barangays
                .AsNoTracking()
                .OrderByDescending(b => b.Id)
                .ToListAsync();
        }
        
    }
}
