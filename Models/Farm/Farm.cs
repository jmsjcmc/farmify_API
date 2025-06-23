namespace Farmify_Api.Models
{
    public class Farm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Farmaddress { get; set; }
        public string Phonenumber { get; set; }
        public string Email { get; set; }
        public Boolean Removed { get; set; }
        public int Userid { get; set; }
        public User User { get; set; }
    }
}
