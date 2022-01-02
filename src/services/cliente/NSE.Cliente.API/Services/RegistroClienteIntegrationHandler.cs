using FluentValidation.Results;
using NSE.Clientes.API.Application.Commands;
using NSE.Core.Mediator;
using NSE.Core.Messages.Integration;
using NSE.MessageBus;

namespace NSE.Clientes.API.Services
{
    public class RegistroClienteIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public RegistroClienteIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }

        private void SetResponder() 
        {
            _bus.RespondAsync<UsuarioRegistradoIntegrationEvent, ResponseMessage>(
                async request => await RegistrarCliente(request));

            _bus.AdvancedBus!.Connected += OnConnect!;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponder();

            return Task.CompletedTask;
        }

        private void OnConnect(Object s, EventArgs e) => SetResponder();

        private async Task<ResponseMessage> RegistrarCliente(UsuarioRegistradoIntegrationEvent registrarCliente) 
        {
            ValidationResult sucesso;
            var clienteCommand = new RegistrarClienteCommand(
                registrarCliente.Id,
                registrarCliente.Nome,
                registrarCliente.Email,
                registrarCliente.Cpf
                );
            
            using(var scope = _serviceProvider.CreateScope()) 
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                sucesso = await mediator.EnviarComando(clienteCommand);
            }

            return new ResponseMessage(sucesso);

        }
    }
}
