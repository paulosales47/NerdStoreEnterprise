using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using System.Diagnostics;

namespace NSE.WebApp.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("erro/{statusCodeErro:length(3,3)}")]
        public IActionResult Error(int statusCodeErro)
        {
            var modelErro = new ErrorViewModel();

            if(statusCodeErro == StatusCodes.Status500InternalServerError) 
            {
                modelErro.Mensagem = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
                modelErro.Titulo = "Ocorreu um erro!";
                modelErro.ErroCode = StatusCodes.Status500InternalServerError;
            }
            else if (statusCodeErro == StatusCodes.Status404NotFound)
            {
                modelErro.Mensagem = "A página que você procura não existe! <br/> Em caso de dúvidas entre em contato com nosso suporte.";
                modelErro.Titulo = "Ops! Página não encontrada.";
                modelErro.ErroCode = StatusCodes.Status404NotFound;
            }
            else if (statusCodeErro == StatusCodes.Status403Forbidden)
            {
                modelErro.Mensagem = "Você não tem permissão para fazer isso.";
                modelErro.Titulo = "Acesso Negado.";
                modelErro.ErroCode = StatusCodes.Status403Forbidden;
            }
            else 
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return View("Error", modelErro);
        }

        [Route("sistema-indisponivel")]
        public IActionResult SistemaIndisponivel()
        {
            var modelErro = new ErrorViewModel();
            modelErro.Mensagem = "O sistema está temporariamente indisponível, tente novamente mais tarde";
            modelErro.Titulo = "Sistema Indisponível";
            modelErro.ErroCode = StatusCodes.Status503ServiceUnavailable;

            return View("Error", modelErro);
        }

    }
}