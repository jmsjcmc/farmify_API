namespace Farmify_Api.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Productname { get; set; }
        public Boolean Removed { get; set; }
        public int Categoryid { get; set; }
        public Category Category { get; set; }
    }
}
