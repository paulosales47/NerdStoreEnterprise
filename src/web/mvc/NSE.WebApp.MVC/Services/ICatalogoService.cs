using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services
{
    public interface ICatalogoService
    {
        Task<IEnumerable<ProdutoViewModel>> GetAllAsync();
        Task<ProdutoViewModel> GetByIdAsync(Guid id);
    }
}
