using NetDevPack.Domain;

namespace GBastos.MicroData.Domain.Models
{
    public class Pedido : Entity, IAggregateRoot
    {
        public Pedido(Guid id, DateTime dataCad, string emailCli, List<Item> Itens)
        {
            Id = id;
            Data = dataCad;
            Email = emailCli;
        }

        private List<Item> _itens = new List<Item>();

        public Guid Id { get; set; }
        public DateTime Data { get; set; }
        public string Email { get; set; }
        public IReadOnlyList<Item> Itens => _itens;
        public decimal ValorTotal => _itens.Sum(i => i.Valor);

        public void AdicionarItem(Item item)
        {
            _itens.Add(item);
        }
    }
}