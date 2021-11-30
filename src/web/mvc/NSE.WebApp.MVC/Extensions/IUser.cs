using System.Security.Claims;

namespace NSE.WebApp.MVC.Extensions
{
    public interface IUser
    {
        string Name { get; }
        Guid ObterUserId();
        string ObterUserEmail();
        string ObterUserToken();
        bool EstaAutenticado();
        bool EstaNaRole(string role);
        IEnumerable<Claim> ObterClaims();
        HttpContent ObterHttpContext();
    }
}
