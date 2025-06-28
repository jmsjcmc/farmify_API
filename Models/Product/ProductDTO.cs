namespace Farmify_Api.Models
{
    public class ProductRequest
    {
        public string Productname { get; set; }
        public int Categoryid { get; set; }
    }
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Productname { get; set; }
        public Boolean Removed { get; set; }
        public int Categoryid { get; set; }
        public CategoryResponse Category { get; set; }
    }
}
