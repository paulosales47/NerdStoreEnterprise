using Microsoft.AspNetCore.Mvc;
using NSE.Catalogo.API.Models;

namespace NSE.Catalogo.API.Controllers
{
    [ApiController]
    public class CatalagoController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;

        public CatalagoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpGet("catalago/produtos")]
        public async Task<IEnumerable<Produto>> Index() 
        {
            return await _produtoRepository.GetAllAsync();
        }

        [HttpGet("catalago/produtos/{id}")]
        public async Task<Produto> ProdutoDetalhe(Guid id) 
        {
            return await _produtoRepository.FindByIdAsync(id);
        }
    }
}
