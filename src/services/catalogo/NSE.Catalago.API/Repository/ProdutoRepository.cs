using Microsoft.EntityFrameworkCore;
using NSE.Catalago.API.Data;
using NSE.Catalago.API.Models;
using NSE.Core.Data;

namespace NSE.Catalago.API.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly CatalagoContext _context;

        public IUnitOfWork UnitOfWork => _context;
        public ProdutoRepository(CatalagoContext context)
        {
            _context = context;
        }
        public async Task<Produto> CreateAsync(Produto produto)
        {
            var produtoCadastrado = await _context.AddAsync(produto);
            return produtoCadastrado.Entity;
        }
        public async Task<Produto> FindByIdAsync(Guid id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            
            if(produto is null)
                return new Produto();

            return produto;
        }
        public async Task<IEnumerable<Produto>> GetAllAsync()
        {
            return await _context.Produtos.AsNoTracking().ToListAsync();
        }
        public Produto Update(Produto produto)
        {
            var produtoAtualizado = _context.Produtos.Update(produto);
            return produtoAtualizado.Entity;
        }
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
