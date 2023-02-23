using System.Security.Claims;

namespace eTickets.Extension
{
    public static class LoggingExtension
    {
        public static string GetUserName(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.Name);
        }
    }
}
