using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NSE.Core.DomainObjects;
using NSE.Core.Mediator;
using NSE.Core.Messages;

namespace NSE.Clientes.Extensions
{
    public static class MediatorExtension
    {
        public static async Task PublicarEventos<T>(this IMediatorHandler mediator, T ctx) where T : DbContext
        {
            IEnumerable<EntityEntry<Entity>> domainEntities = ObterEntidadesContexto(ctx);

            List<Event> domainEvents = ObterEventosEntidades(domainEntities);

            domainEntities.ToList().ForEach(e => e.Entity.LimparEventos());

            var tasks = domainEvents.Select(async (domainEvent) =>
            {
                await mediator.PublicarEvento(domainEvent);
            });

            await Task.WhenAll(tasks);
        }

        private static List<Event> ObterEventosEntidades(IEnumerable<EntityEntry<Entity>> domainEntities)
        {
            return domainEntities
                .SelectMany(e => e.Entity.Notificacoes)
                .ToList();
        }

        private static IEnumerable<EntityEntry<Entity>> ObterEntidadesContexto<T>(T ctx) where T : DbContext
        {
            return ctx.ChangeTracker
                .Entries<Entity>()
                .Where(e => e.Entity.Notificacoes != null && e.Entity.Notificacoes.Any());
        }
    }
}
