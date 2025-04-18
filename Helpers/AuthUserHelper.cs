using System.Security.Claims;

namespace Farmify_Api.Helpers
{
    public static class AuthUserHelper
    {
        public static string GetFullName(ClaimsPrincipal user)
        {
            var firstName = user.FindFirst("FirstName")?.Value ?? string.Empty;
            var lastName = user.FindFirst("LastName")?.Value ?? string.Empty;

            return $"{firstName} {lastName}".Trim();
        }

        public static string GetUserId(ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }

        public static string GetUsername(ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;
        }
    }
}
