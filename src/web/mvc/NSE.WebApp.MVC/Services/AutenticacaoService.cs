using NSE.WebApp.MVC.Models;
using System.Text;
using System.Text.Json;

namespace NSE.WebApp.MVC.Services
{
    public class AutenticacaoService : IAutenticacaoService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public AutenticacaoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonSerializerOptions =  new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }

        public async Task<UsuarioRespostaLogin> Login(UsuarioLogin usuarioLogin)
        {
            var loginContent = new StringContent(
                JsonSerializer.Serialize(usuarioLogin),
                Encoding.UTF8,
                mediaType: "application/json");

            var response = await _httpClient.PostAsync("https://localhost:7269/api/identidade/autenticar", loginContent);
            string jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<UsuarioRespostaLogin>(json: jsonResponse, _jsonSerializerOptions)!;
        }

        public async Task<UsuarioRespostaLogin> Registro(UsuarioRegistro usuarioRegistro)
        {
            var registroContent = new StringContent(
                JsonSerializer.Serialize(usuarioRegistro),
                Encoding.UTF8,
                mediaType: "application/json");

            var response = await _httpClient.PostAsync("https://localhost:7269/api/identidade/nova-conta", registroContent);
            string jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<UsuarioRespostaLogin>(json: jsonResponse, _jsonSerializerOptions)!;
        }
    }
}
