using NSE.WebApp.MVC.Extensions;
using System.Net.Http;
using System.Text;
using System.Text.Json;

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

        protected HttpContent MontarConteudoRequisicao(object dadoRequisicao) 
        {
            var conteudoRequisicao = new StringContent(
                JsonSerializer.Serialize(dadoRequisicao),
                Encoding.UTF8,
                mediaType: "application/json");

            return conteudoRequisicao;
        }

        protected async Task<T> DesserializarObjetoResponse<T>(HttpResponseMessage responseMessage) 
        {
            string jsonContent = await responseMessage.Content.ReadAsStringAsync();
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            
            if(jsonContent != null) 
            {
                return JsonSerializer.Deserialize<T>(jsonContent, jsonSerializerOptions)!;
            }
            
            throw new  ArgumentException(nameof(T));
        }
    }
}
