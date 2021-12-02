using Microsoft.Extensions.Options;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services
{
    public class AutenticacaoService : Service, IAutenticacaoService
    {
        private readonly HttpClient _httpClient;
        
        public AutenticacaoService(HttpClient httpClient, IOptions<EndPointSettings> options)
        {
            httpClient.BaseAddress = new Uri(options.Value.AutenticacaoUrl!);
            _httpClient = httpClient;
        }

        public async Task<UsuarioRespostaLogin> Login(UsuarioLogin usuarioLogin)
        {
            var loginContent = MontarConteudoRequisicao(usuarioLogin);
            var response = await _httpClient.PostAsync("/api/identidade/autenticar", loginContent);
            
            if (!TratarErrosResponse(response)) 
            {
                return new UsuarioRespostaLogin
                {
                    ResponseResult = await DesserializarObjetoResponse<ResponseResult>(response)
                };
            }

            return await DesserializarObjetoResponse<UsuarioRespostaLogin>(response);
        }

        public async Task<UsuarioRespostaLogin> Registro(UsuarioRegistro usuarioRegistro)
        {
            var registroContent = MontarConteudoRequisicao(usuarioRegistro);
            var response = await _httpClient.PostAsync("/api/identidade/nova-conta", registroContent);
            
            if (!TratarErrosResponse(response))
            {
                return new UsuarioRespostaLogin
                {
                    ResponseResult = await DesserializarObjetoResponse<ResponseResult>(response)
                };
            }

            return await DesserializarObjetoResponse<UsuarioRespostaLogin>(response);
        }
    }
}
