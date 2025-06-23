namespace Farmify_Api.Models
{
    public class UserRequest
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserResponse
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Boolean Removed { get; set; }
        public DateTime Datecreated { get; set; }
        public DateTime? Dateupdated { get; set; }
    }
}
