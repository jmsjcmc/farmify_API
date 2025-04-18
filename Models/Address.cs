namespace Farmify_Api.Models
{
    public class Address
    {
        public int Id { get; set; }
        public int Userid { get; set; }
        public virtual User User { get; set; }
        public string HomeAddress { get; set; }
        public string Street { get; set; }
        public string Baranggay { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public int Zipcode { get; set; }
        public Boolean Primary { get; set; }
    }

    public class AddressRequest
    {
        public string HomeAddress { get; set; }
        public string Street { get; set; }
        public string Baranggay { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public int Zipcode { get; set; }
        public Boolean Primary { get; set; }
    }

    public class AddressResponse
    {
        public int Id { get; set; }
        public int Userid { get; set; }
        public string HomeAddress { get; set; }
        public string Street { get; set; }
        public string Baranggay { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public int Zipcode { get; set; }
        public Boolean Primary { get; set; }
    }
}
