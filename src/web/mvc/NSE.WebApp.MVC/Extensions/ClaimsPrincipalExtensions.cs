using System.Security.Claims;

namespace NSE.WebApp.MVC.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal) 
        {
            return GetClaim(principal, "sub");
        }

        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            return GetClaim(principal, "email");
        }

        public static string GetUserToken(this ClaimsPrincipal principal)
        {
            return GetClaim(principal, "JWT");
        }

        private static string GetClaim(ClaimsPrincipal principal, string dado)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            var claim = principal.Claims.FirstOrDefault(c => c.Type.Equals(dado));
            return claim!.Value;
        }

    }
}
