using Microsoft.Extensions.Options;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services
{
    public class CatalogoService : Service, ICatalogoService
    {
        private readonly HttpClient _httpClient;

        public CatalogoService(HttpClient httpClient, IOptions<EndPointSettings> options)
        {
            httpClient.BaseAddress = new Uri(options.Value.CatalagoUrl!);
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProdutoViewModel>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("/api/catalogo/produtos");
           
            TratarErrosResponse(response);

            return await DesserializarObjetoResponse<IEnumerable<ProdutoViewModel>>(response);
        }

        public async Task<ProdutoViewModel> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/catalogo/produtos/{id}");

            TratarErrosResponse(response);

            return await DesserializarObjetoResponse<ProdutoViewModel>(response);
        }
    }
}
