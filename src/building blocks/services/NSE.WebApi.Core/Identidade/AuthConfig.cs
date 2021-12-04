using Microsoft.AspNetCore.Builder;

namespace NSE.WebApi.Core.Identidade
{
    public static class AuthConfig
    {
        public static void UseAuthConfiguration(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
