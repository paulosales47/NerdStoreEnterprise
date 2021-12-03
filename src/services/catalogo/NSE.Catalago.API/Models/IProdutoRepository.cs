using NSE.Core.Data;

namespace NSE.Catalago.API.Models
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<Produto> CreateAsync(Produto produto);
        Produto Update(Produto produto);
        Task<Produto> FindByIdAsync(Guid id);
        Task<IEnumerable<Produto>> GetAllAsync();
        
    }
}
