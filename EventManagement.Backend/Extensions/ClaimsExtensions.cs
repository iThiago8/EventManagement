using System.Security.Claims;

namespace apis.Extensions
{
    public static class ClaimsExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
            return user.Claims.SingleOrDefault(u => u.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identiti/claims/givenname"))!.Value;
        }
    }
}
