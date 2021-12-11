using NSE.Clientes.API.Models;
using NSE.Core.Data;

namespace NSE.Clientes.API.Data.Repository
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<IEnumerable<Cliente>> GetAllAsync();
        Task<Cliente> GetByCpfAsync(string cpf);
        void Add(Cliente cliente);  
    }
}
