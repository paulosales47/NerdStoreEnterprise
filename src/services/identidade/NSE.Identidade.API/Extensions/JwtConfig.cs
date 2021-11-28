namespace NSE.Identidade.API.Extensions
{
    public class JwtConfig
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string key { get; set; }
        public int ExpirationTimeHours { get; set; }
    }
}
