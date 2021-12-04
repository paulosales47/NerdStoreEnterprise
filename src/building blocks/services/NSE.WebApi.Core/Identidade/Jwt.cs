namespace NSE.WebApi.Core.Identidade
{
    public class Jwt
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string key { get; set; }
        public double ExpirationTimeHours { get; set; }
    }
}
