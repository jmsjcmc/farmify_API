using Farmify_Api.Models.User;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Farmify_Api.Helpers
{
    public class AuthenticationHelper
    {
        private readonly IConfiguration _config;
        public AuthenticationHelper(IConfiguration config)
        {
            _config = config;
        }
        public static int getuserid(ClaimsPrincipal user)
        {
            if (user == null)
                return 0;

            var value = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (int.TryParse(value, out int userid))
            {
                return userid;
            }
            return 0;
        }

        public string generateaccesstoken(User user)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var tokendescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(12),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenhandler.CreateToken(tokendescriptor);
            return tokenhandler.WriteToken(token);
        }
    }
}
