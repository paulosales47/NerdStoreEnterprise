using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NSE.Catalogo.API.Models;
using NSE.Core.Data;
using NSE.Core.Messages;
using System.Linq;

namespace NSE.Catalogo.API.Data
{
    public class CatalagoContext: DbContext, IUnitOfWork
    {
        public CatalagoContext(DbContextOptions<CatalagoContext> options): base(options)
        {}

        public DbSet<Produto>? Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();

            ConfigurarCampoString(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalagoContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        private void ConfigurarCampoString(ModelBuilder modelBuilder) 
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(100)");
            }
        }

        public async Task<bool> CommitAsync()
        {
            return await base.SaveChangesAsync() > 0;
        }

    }
}
