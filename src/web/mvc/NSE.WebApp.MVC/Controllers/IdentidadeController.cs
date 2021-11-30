using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Controllers
{
    public class IdentidadeController : Controller
    {
        [HttpGet]
        [Route("nova-conta")]
        public IActionResult Registro() { return View(); }

        [HttpPost]
        [Route("nova-conta")]
        public async Task<IActionResult> Registro(UsuarioRegistro usuarioRegistro) 
        {
            if(!ModelState.IsValid) return View(usuarioRegistro);

            //REALIZAR REGISTRO NA API DE IDENTIDADE
            
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login() { return View(); }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UsuarioLogin usuarioLogin) 
        {
            if (!ModelState.IsValid) return View(usuarioLogin);

            //REALIZAR LOGIN NA API DE IDENTIDADE

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("sair")]
        public async Task<IActionResult> Logout() 
        {
            return RedirectToAction("Index", "Home");
        }


    }
}
