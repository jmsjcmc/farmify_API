namespace Farmify_Api.Models.Address
{
    public class Island
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Region> Region { get; set; }
    }
    public class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Regioncode { get; set; }
        public int Islandid { get; set; }
        public Island Island { get; set; }
        public ICollection<Province> Province { get; set; }
    }
    public class Province
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Regionid { get; set; }
        public Region Region { get; set; }
        public ICollection<CityMunicipality> CityMunicipality { get; set; }
    }
    public class CityMunicipality
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Provinceid { get; set; }
        public Province Province { get; set; }
        public ICollection<Barangay> Barangay { get; set; }
    }
    public class Barangay
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityMunicipalityid { get; set; }
        public CityMunicipality CityMunicipality { get; set; }
    }
}
