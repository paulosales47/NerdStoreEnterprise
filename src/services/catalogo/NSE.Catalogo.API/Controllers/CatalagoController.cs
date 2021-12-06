using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.Catalogo.API.Models;
using NSE.WebApi.Core.Identidade;

namespace NSE.Catalogo.API.Controllers
{
    [Authorize]
    [ApiController]
    public class CatalagoController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;

        public CatalagoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [AllowAnonymous]
        [HttpGet("catalago/produtos")]
        public async Task<IEnumerable<Produto>> Index() 
        {
            return await _produtoRepository.GetAllAsync();
        }

        [ClaimsAuthorize("Catalogo", "Ler")]
        [HttpGet("catalago/produtos/{id}")]
        public async Task<Produto> ProdutoDetalhe(Guid id) 
        {
            return await _produtoRepository.FindByIdAsync(id);
        }
    }
}
