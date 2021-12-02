using System.Net;

namespace NSE.WebApp.MVC.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext) 
        {
            try
            {
                await _next(httpContext);
            }
            catch (CustomHttpRequestException ex)
            {
                HandlerRequestExcpetionAsync(httpContext, ex);
            }
        }

        private static void HandlerRequestExcpetionAsync(HttpContext httpContext, CustomHttpRequestException httpRequestException) 
        { 
            if(httpRequestException.StatusCode == HttpStatusCode.Unauthorized) 
            {
                httpContext.Response.Redirect($"/login?ReturnUrl={httpContext.Request.Path}");
                return;
            }

            httpContext.Response.StatusCode = (int) httpRequestException.StatusCode;
        }
    }
}
