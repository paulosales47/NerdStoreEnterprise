using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using NSE.WebApp.MVC.Services;

namespace NSE.WebApp.MVC.Controllers
{
    public class CatalogoController : MainController
    {
        private readonly ICatalogoServiceRefit _catalogoService;

        public CatalogoController(ICatalogoServiceRefit catalogoService)
        {
            _catalogoService = catalogoService;
        }

        [HttpGet]
        [Route("")]
        [Route("vitrine")]
        public async Task<IActionResult> Index() 
        {
            IEnumerable<ProdutoViewModel> produtos = await _catalogoService.GetAllAsync();
            return View(produtos);  
        }

        [HttpGet]
        [Route("produto-detalhe/{id}")]
        public async Task<IActionResult> ProdutoDetalhe(Guid id) 
        {
            ProdutoViewModel produto = await _catalogoService.GetByIdAsync(id);
            return View(produto); 
        } 
    }
}
