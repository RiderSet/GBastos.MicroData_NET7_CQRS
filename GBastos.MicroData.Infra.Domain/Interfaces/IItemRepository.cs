using GBastos.MicroData.Domain.Models;
using NetDevPack.Data;

namespace GBastos.MicroData.Domain.Interfaces
{
    public interface IItemRepository : IRepository<Item>
    {
        Task<Item> GetById(Guid id);
        Task<Item> GetByEmail(string email);
        Task<IEnumerable<Item>> GetAll();

        void Add(Item customer);
        void Update(Item customer);
        void Remove(Item customer);
    }
}