using GBastos.MicroData.Domain.Models;
using NetDevPack.Data;

namespace GBastos.MicroData.Domain.Interfaces
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        Task<Pedido> GetById(Guid id);
        Task<Pedido> GetByEmail(string email);
        Task<IEnumerable<Pedido>> GetAll();

        void Add(Pedido customer);
        void Update(Pedido customer);
        void Remove(Pedido customer);
    }
}