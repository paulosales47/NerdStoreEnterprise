using Polly.CircuitBreaker;
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

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (CustomHttpRequestException ex)
            {
                HandlerRequestExcpetionAsync(context, ex.StatusCode);
            }
            catch (BrokenCircuitException)
            {
                HandleCircuitBrakerExeception(context);
            }
        }

        private static void HandlerRequestExcpetionAsync(HttpContext httpContext, HttpStatusCode statusCode)
        {
            if (statusCode == HttpStatusCode.Unauthorized)
            {
                httpContext.Response.Redirect($"/login?ReturnUrl={httpContext.Request.Path}");
                return;
            }

            httpContext.Response.StatusCode = (int)statusCode;
        }

        private static void HandleCircuitBrakerExeception(HttpContext context) 
        {
            context.Response.Redirect("/sistema-indisponivel");
        }
    }
}
