namespace NSE.Identidade.API.Models
{
    public class UsuarioRespostaLogin
    {
        public string AccessToken { get; set; }
        public double  ExpireIn { get; set; }
        public UsuarioToken UsuarioToken { get; set; }

    }
}
