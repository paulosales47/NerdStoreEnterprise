using System.Text.RegularExpressions;

namespace NSE.Core.DomainObjects
{
    public class Email
    {
        protected Email() { }

        public Email(string endereco)
        {
            if (!ValidateEmail(endereco))
                throw new DomainException("E-mail inválido");

            Endereco = endereco;
        }

        public string? Endereco { get; private set; }
        public const int EnderecoMaxLenght = 254;

        private static bool ValidateEmail(string email) 
        {
            var regexEmail = new Regex(@"^(?!\.)(""([^""\r\\]|\\[""\r\\]) * ""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$");
            return regexEmail.IsMatch(email);   
        }

    }
}
