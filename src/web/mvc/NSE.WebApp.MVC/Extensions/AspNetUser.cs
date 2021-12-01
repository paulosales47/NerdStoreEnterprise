using System.Security.Claims;

namespace NSE.WebApp.MVC.Extensions
{
    public class AspNetUser: IUser
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ClaimsPrincipal? _user;

        public AspNetUser(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _user = _contextAccessor.HttpContext?.User;
        }

        public string Name => _user.Identity.Name;

        public bool EstaAutenticado()
        {
            return _user!.Identity!.IsAuthenticated;
        }

        public bool PossuiRole(string role)
        {
            return _user!.IsInRole(role);
        }

        public IEnumerable<Claim> ObterClaims()
        {
            return _user!.Claims;
        }

        public HttpContext ObterHttpContext()
        {
            return _contextAccessor.HttpContext!;
        }

        public string ObterUserEmail()
        {
            return EstaAutenticado() ? _user!.GetUserEmail() : String.Empty;
        }

        public Guid ObterUserId()
        {
            return EstaAutenticado() ? Guid.Parse(_user!.GetUserId()) : Guid.Empty;
        }

        public string ObterUserToken()
        {
            return EstaAutenticado() ? _user!.GetUserToken() : String.Empty;
        }
    }
}
