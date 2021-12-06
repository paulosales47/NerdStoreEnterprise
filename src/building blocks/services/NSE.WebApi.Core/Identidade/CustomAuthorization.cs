using Microsoft.AspNetCore.Http;

namespace NSE.WebApi.Core.Identidade
{
    public class CustomAuthorization
    {
        public static bool ValidarClaimUsuario(HttpContext context, string claimName, string claimValue) 
        {
            return context.User.Identity!.IsAuthenticated &&
                   context.User.Claims.Any(c => c.Type.Equals(claimName) && c.Value.Equals(claimValue));
        }
    }
}
