using NSE.Core.DomainObjects;

namespace NSE.Catalago.API.Models
{
    public class Produto: Entity, IAgregateRoot
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Imagem { get; set; }
        public int Quantidade { get; set; }
    }
}
