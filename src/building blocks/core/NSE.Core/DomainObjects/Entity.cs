﻿using NSE.Core.Messages;

namespace NSE.Core.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        private List<Event>? _notificacoes;
        public IReadOnlyCollection<Event> Notificacoes => _notificacoes?.AsReadOnly();

        public override bool Equals(object? obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(null, compareTo)) return false;  
            if (ReferenceEquals(this, compareTo)) return true;

            return Id.Equals(compareTo.Id);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }

        public static bool operator ==(Entity a, Entity b) 
        {
            if(ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
            if(ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }
        public void AdicionarEvento(Event evento) 
        {
            _notificacoes = _notificacoes ?? new List<Event>();
            _notificacoes.Add(evento);
        }

        public void RemoverEvento(Event evento) 
        {
            _notificacoes?.Remove(evento);
        }

        public void LimparEventos() 
        {
            _notificacoes?.Clear();
        }

        
    }
}
