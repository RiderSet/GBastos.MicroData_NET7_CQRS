using NetDevPack.Domain;

namespace GBastos.MicroData.Domain.Models
{
    public class Item : Entity, IAggregateRoot
    {
        public Item(Guid id, string codProduto, string descricao, int quantidade, decimal itemValor)
        {
            if (id.ToString().Length < 1)
                throw new InvalidOperationException();
            if (string.IsNullOrEmpty(codProduto) || string.IsNullOrEmpty(descricao))
                throw new InvalidOperationException();
            if (quantidade < 0)
                throw new InvalidOperationException();

            Id = id;
            CodProduto = codProduto;
            Descricao = descricao;
            Qtd = quantidade;
            Valor = itemValor;
        }

        public Guid Id { get; private set; }
        public string CodProduto { get; private set; }
        public string Descricao { get; private set; }
        public int Qtd { get; set; }
        public decimal Valor { get; private set; }
    }
}