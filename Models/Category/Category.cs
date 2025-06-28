namespace Farmify_Api.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Categoryname { get; set; }
        public ICollection<Product> Product { get; set; }
    }
}
