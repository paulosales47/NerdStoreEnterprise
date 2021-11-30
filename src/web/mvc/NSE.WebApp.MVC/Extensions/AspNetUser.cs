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
            throw new NotImplementedException();
        }

        public bool EstaNaRole(string role)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Claim> ObterClaims()
        {
            throw new NotImplementedException();
        }

        public HttpContent ObterHttpContext()
        {
            throw new NotImplementedException();
        }

        public string ObterUserEmail()
        {
            return EstaAutenticado() ? _user.FindFirstValue(ClaimTypes.Email) : String.Empty;
        }

        public Guid ObterUserId()
        {
            return EstaAutenticado() ? Guid.Parse(_user.FindFirstValue(ClaimTypes.NameIdentifier)) : Guid.Empty;
        }

        public string ObterUserToken()
        {
            return EstaAutenticado() ? _user.FindFirstValue(ClaimTypes.Sid) : String.Empty;
        }
    }
}
