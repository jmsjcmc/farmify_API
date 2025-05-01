namespace Farmify_Api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public Boolean Deleted { get; set; }
        public DateTime Createdon { get; set; }
        public DateTime Updatedon { get; set; }
        public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
    }

    public class UserDetail
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }

    public class UserResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }

    public class UserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<string> Role { get; set; }
    }

    public class Login
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class UserLoginResponse
    {
        public string accessToken { get; set; }
        public int Userid { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
    }

    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public Boolean Deleted { get; set; }
    }

    public class RoleRequest
    {
        public string RoleName { get; set; }
    }

    public class RoleResponse
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public Boolean Deleted { get; set; }
    }
}
