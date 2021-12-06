using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace NSE.WebApi.Core.Identidade
{
    internal class RequisitoClaimFilter: IAuthorizationFilter
    {
        private readonly Claim _claim;

        public RequisitoClaimFilter(Claim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if(!context.HttpContext.User.Identity!.IsAuthenticated) 
            {
                context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
                return;
            }

           if(!CustomAuthorization.ValidarClaimUsuario(context.HttpContext, _claim.Type, _claim.Value)) 
            {
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
            }
                

        }
    }
}
