namespace Farmify_Api.Models.Address
{
    public class IslandRequest
    {
        public string Name { get; set; }
    }
    public class IslandResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
    public class RegionRequest
    {
        public string Name { get; set; }
        public string Regioncode { get; set; }
    }
    public class RegionResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Regioncode { get; set; }
    }
    public class ProvinceRequest
    {
        public string Name { get; set; }
    }
    public class ProvinceResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class CityMunicipalityRequest
    {
        public string Name { get; set; }
    }
    public class CityMunicipalityResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class BarangaRequest
    {
        public string Name { get; set; }
    }
    public class BarangayResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
