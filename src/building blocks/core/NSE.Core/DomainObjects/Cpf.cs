using System.Text;

namespace NSE.Core.DomainObjects
{
    public class Cpf
    {
        public const int CpfMaxLenght = 11;
        public string? Numero { get; private set; }

        protected Cpf() { }

        public Cpf(string numero)
        {
            if (!ValidateCpf(numero))
                throw new DomainException("CPF inválido");

            Numero = numero;
        }

        public static bool ValidateCpf(string cpf)
        {
            bool valid = false;

            cpf = ClearAndCheck(cpf, ref valid);

            if (!valid)
                return false;

            int sum = 0;
            int remainder = 0;

            for (int i = 0; i < 9; i++)
                sum += (cpf[i] - 48) * ((8 - i) % 9 + 2);

            remainder = sum % 11;

            if (remainder < 2)
                remainder = 0;
            else
                remainder = 11 - remainder;

            if (cpf[9] != (char)(remainder + 48))
                return false;

            sum = remainder * 2;

            for (int i = 0; i < 9; i++)
                sum += (cpf[i] - 48) * ((9 - i) % 10 + 2);

            remainder = sum % 11;

            if (remainder < 2)
                remainder = 0;
            else
                remainder = 11 - remainder;

            if (cpf[10] != (char)(remainder + 48))
                return false;

            return true;
        }

        private static string ClearAndCheck(string value, ref bool valid)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;

            bool firstChar = true;
            char currentChar = char.MinValue;

            StringBuilder ret = new StringBuilder();

            foreach (var c in value)
            {
                if (c >= '0' && c <= '9')
                {
                    ret.Append(c);

                    if (firstChar)
                    {
                        firstChar = false;
                        currentChar = c;
                    }
                    else if (!valid && currentChar != c)
                        valid = true;
                }
            }

            if (ret.Length != 11)
                valid = false;

            return ret.ToString();
        }

    }
}
