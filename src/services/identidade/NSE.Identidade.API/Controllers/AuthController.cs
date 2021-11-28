using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NSE.Identidade.API.Models;

namespace NSE.Identidade.API.Controllers
{
    [ApiController]
    [Route("api/identidade")]
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("nova-conta")]
        public async Task<IActionResult> Registrar(UsuarioRegistro usuarioRegistro) 
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var user = new IdentityUser
            {
                UserName = usuarioRegistro.Email,
                Email = usuarioRegistro.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, usuarioRegistro.Senha);

            if (result.Succeeded) 
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok();
            }

            return BadRequest(result);
        }

        [HttpPost("autenticar")]
        public async Task<IActionResult> Login(UsuarioLogin usuarioLogin) 
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _signInManager.PasswordSignInAsync(
                usuarioLogin.Email,
                usuarioLogin.Senha,
                isPersistent: false, 
                lockoutOnFailure: true);

            if (result.Succeeded) return Ok();

            return BadRequest(result);
        }
    }
}
