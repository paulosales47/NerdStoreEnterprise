namespace NSE.Identidade.API.Models
{
    public class UsuarioClaim
    {
        public UsuarioClaim(string type, string value)
        {
            Type = type;
            Value = value;
        }

        public string Value { get; set; }
        public string Type { get; set; }
    }
}
