﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NSE.Identidade.API.Extensions;
using NSE.Identidade.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NSE.Identidade.API.Controllers
{
    [ApiController]
    [Route("api/identidade")]
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly JwtConfig _jwtConfig;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IOptions<JwtConfig> jwtConfig)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtConfig = jwtConfig.Value;
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
                return Ok(await GerarJwt(usuarioRegistro.Email));
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

            if (result.Succeeded) 
                return Ok(await GerarJwt(usuarioLogin.Email));
            
            return BadRequest(result);
        }

        private async Task<UsuarioRespostaLogin> GerarJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);
            AdicionarClaims(user, claims, userRoles);

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.key);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _jwtConfig.Issuer,
                Audience = _jwtConfig.Audience,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_jwtConfig.ExpirationTimeHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            });
            var encodedToken = tokenHandler.WriteToken(token);
            var response = new UsuarioRespostaLogin
            {
                AccessToken = encodedToken,
                ExpireIn = TimeSpan.FromHours(_jwtConfig.ExpirationTimeHours).TotalSeconds,
                UsuarioToken = new UsuarioToken
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(claims => new UsuarioClaim(claims.Type,claims.Value))
                }
            };

            return response;
        }

        private void AdicionarClaims(IdentityUser user, IList<Claim> claims, IList<string> userRoles)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.Now).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.Now).ToString(), ClaimValueTypes.Integer64));
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }
        }

        private long ToUnixEpochDate(DateTime date)
        {
            return (long)Math.Round((date.ToUniversalTime() - 
                new DateTimeOffset(
                    year: 1970,
                    day: 1,
                    month: 1,
                    hour: 0,
                    minute: 0,
                    second: 0,
                    offset: TimeSpan.Zero)).TotalSeconds);
        }
    }
}
