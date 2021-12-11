using FluentValidation.Results;
using MediatR;
using NSE.Clientes.API.Data.Repository;
using NSE.Clientes.API.Models;
using NSE.Core.Messages;

namespace NSE.Clientes.API.Application.Commands
{
    public class ClienteCommandHandler : CommandHandler, IRequestHandler<RegistrarClienteCommand, ValidationResult>
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ValidationResult> Handle(RegistrarClienteCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult!;

            var cliente = new Cliente(request.Id, request.Nome, request.Email, request.Cpf);
            Cliente clienteExistente = await _clienteRepository.GetByCpfAsync(cliente.Cpf!.Numero!);

            if (clienteExistente is not null) 
            {
                AdicionarErro("Este CPF já está em uso");
                return ValidationResult;
            }

            _clienteRepository.Add(cliente);

            return await PersistirDados(_clienteRepository.UnitOfWork);
            
        }
    }
}
