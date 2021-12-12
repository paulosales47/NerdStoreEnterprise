using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NSE.Clientes.API.Models;
using NSE.Clientes.Extensions;
using NSE.Core.Data;
using NSE.Core.Mediator;
using NSE.Core.Messages;

namespace NSE.Clientes.API.Data
{
    public class ClienteContext: DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;
        public ClienteContext(DbContextOptions<ClienteContext> options, IMediatorHandler mediatorHandler): base(options)
        {
            _mediatorHandler = mediatorHandler;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Cliente>? Clientes { get; set; }
        public DbSet<Endereco>? Enderecos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(100)");
            }

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClienteContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> CommitAsync()
        {
            var sucesso = await base.SaveChangesAsync() > 0;

            if (sucesso)
                await _mediatorHandler.PublicarEventos(this);

            return sucesso;
        }
    }
}
