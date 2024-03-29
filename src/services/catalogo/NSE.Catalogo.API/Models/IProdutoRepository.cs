﻿using NSE.Core.Data;

namespace NSE.Catalogo.API.Models
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<Produto> CreateAsync(Produto produto);
        Produto Update(Produto produto);
        Task<Produto> GetByIdAsync(Guid id);
        Task<IEnumerable<Produto>> GetAllAsync();
        
    }
}
