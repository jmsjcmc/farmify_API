namespace Farmify_Api.Models
{
    public class CategoryRequest
    {
        public string Categoryname { get; set; }
    }
    public class CategoryResponse
    {
        public int Id { get; set; }
        public string Categoryname { get; set; }
    }
}
