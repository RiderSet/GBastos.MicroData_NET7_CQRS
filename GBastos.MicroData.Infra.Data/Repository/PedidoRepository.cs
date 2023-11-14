using GBastos.MicroData.Domain.Interfaces;
using GBastos.MicroData.Domain.Models;
using GBastos.MicroData.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;

namespace GBastos.MicroData.Infra.Data.Repository
{
    public class PedidoRepository : IPedidoRepository
    {
        protected readonly CTX Db;
        protected readonly DbSet<Pedido> DbSet;

        public PedidoRepository(CTX context)
        {
            Db = context;
            DbSet = Db.Set<Pedido>();
        }

        public IUnitOfWork UnitOfWork => Db;

        public async Task<Pedido> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<IEnumerable<Pedido>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<Pedido> GetByEmail(string email)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Email == email);
        }

        public void Add(Pedido Pedido)
        {
           DbSet.Add(Pedido);
        }

        public void Update(Pedido Pedido)
        {
            DbSet.Update(Pedido);
        }

        public void Remove(Pedido Pedido)
        {
            DbSet.Remove(Pedido);
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
