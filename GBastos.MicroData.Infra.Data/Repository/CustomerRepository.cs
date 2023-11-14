using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GBastos.MicroData.Domain.Interfaces;
using GBastos.MicroData.Domain.Models;
using GBastos.MicroData.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;

namespace GBastos.MicroData.Infra.Data.Repository
{
    public class CustomerRepository : IItemRepository
    {
        protected readonly CTX Db;
        protected readonly DbSet<Item> DbSet;

        public CustomerRepository(CTX context)
        {
            Db = context;
            DbSet = Db.Set<Item>();
        }

        public IUnitOfWork UnitOfWork => Db;

        public async Task<Item> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<IEnumerable<Item>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<Item> GetByEmail(string email)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Email == email);
        }

        public void Add(Item customer)
        {
           DbSet.Add(customer);
        }

        public void Update(Item customer)
        {
            DbSet.Update(customer);
        }

        public void Remove(Item customer)
        {
            DbSet.Remove(customer);
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
