using NSE.WebApp.MVC.Models;
using Refit;

namespace NSE.WebApp.MVC.Services
{
    public interface ICatalogoServiceRefit
    {
        [Get("/api/catalogo/produtos")]
        Task<IEnumerable<ProdutoViewModel>> GetAllAsync();

        [Get("/api/catalogo/produtos/{id}")]
        Task<ProdutoViewModel> GetByIdAsync(Guid id);
    }
}
