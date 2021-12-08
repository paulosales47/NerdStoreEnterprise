using Refit;
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
                HandlerRequestExcpetionAsync(httpContext, ex.StatusCode);
            }
            catch (ValidationApiException ex)
            {
                HandlerRequestExcpetionAsync(httpContext, ex.StatusCode);
            }
            catch (ApiException ex) 
            {
                HandlerRequestExcpetionAsync(httpContext, ex.StatusCode);
            }
        }

        private static void HandlerRequestExcpetionAsync(HttpContext httpContext, HttpStatusCode statusCode)
        { 
            if(statusCode == HttpStatusCode.Unauthorized) 
            {
                httpContext.Response.Redirect($"/login?ReturnUrl={httpContext.Request.Path}");
                return;
            }

            httpContext.Response.StatusCode = (int) statusCode;
        }
    }
}
