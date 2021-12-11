using FluentValidation;
using NSE.Core.DomainObjects;

namespace NSE.Clientes.API.Application.Commands
{
    public class RegistrarClienteValidation: AbstractValidator<RegistrarClienteCommand>
    {
        public RegistrarClienteValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente é inválido");

            RuleFor(c => c.Nome)
                .NotEmpty()
                .MinimumLength(3)
                .WithMessage("O nome do cliente não foi informado");

            RuleFor(c => c.Cpf)
                .Must(ValidateCpf)
                .WithMessage("O CPF informado não é válido");

            RuleFor(c => c.Email)
                .Must(ValidateEmail)
                .WithMessage("O e-mail informado não é válido");

        }

        protected static bool ValidateCpf(string cpf) 
        {
            return Cpf.ValidateCpf(cpf);
        }

        protected static bool ValidateEmail(string email)
        {
            return Email.ValidateEmail(email);
        }
    }
}
