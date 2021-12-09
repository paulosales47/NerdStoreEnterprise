using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.Catalogo.API.Models;
using NSE.WebApi.Core.Identidade;

namespace NSE.Catalogo.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/catalogo")]
    public class CatalogoController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;

        public CatalogoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [AllowAnonymous]
        [HttpGet("produtos")]
        public async Task<IEnumerable<Produto>> Index() 
        {
            return await _produtoRepository.GetAllAsync();
        }

        [ClaimsAuthorize("Catalogo", "Ler")]
        [HttpGet("produtos/{id}")]
        public async Task<Produto> ProdutoDetalhe(Guid id) 
        {
            throw new Exception("Erro");
            return await _produtoRepository.GetByIdAsync(id);
        }
    }
}
