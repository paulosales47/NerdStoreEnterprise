using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Extensions;
using System.Net.Http.Headers;

namespace NSE.WebApp.MVC.Services.Handlers
{
    public class HttpClientAuthorizationDelegatingHandler: DelegatingHandler
    {
        private IUser? _user;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpClientAuthorizationDelegatingHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _user = new AspNetUser(_httpContextAccessor);
            var authorizationHeader = _user.ObterHttpContext().Request.Headers["Authorization"];

            if (!string.IsNullOrEmpty(authorizationHeader)) 
            {
                request.Headers.Add("Authorization", new List<string> { authorizationHeader });
            }

            var token = _user.ObterUserToken();
            
            if (!string.IsNullOrEmpty(token)) 
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
