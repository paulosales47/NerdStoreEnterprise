using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace NSE.WebApi.Core.Identidade
{
    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claimName, string claimvalue) : base(typeof(RequisitoClaimFilter))
        {
            Arguments = new object[] { new Claim(claimName, claimvalue) };
        }
    }
}
