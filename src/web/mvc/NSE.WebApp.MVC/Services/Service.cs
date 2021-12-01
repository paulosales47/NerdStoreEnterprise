using NSE.WebApp.MVC.Extensions;

namespace NSE.WebApp.MVC.Services
{
    public abstract class Service
    {
        protected bool TratarErrosResponse(HttpResponseMessage response) 
        {
            switch ((int)response.StatusCode)
            {
                case StatusCodes.Status401Unauthorized:
                case StatusCodes.Status403Forbidden:
                case StatusCodes.Status404NotFound:
                case StatusCodes.Status500InternalServerError:
                    throw new CustomHttpRequestException(response.StatusCode);
                
                case StatusCodes.Status400BadRequest:
                    return false;

            }

            response.EnsureSuccessStatusCode();
            return true;
        }
    }
}
