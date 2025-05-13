using Microsoft.EntityFrameworkCore;
using PruebaSolvex.Domain.Repository;
using PruebaSolvex.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaSolvex.Persistence.Core
{
    public abstract class BaseRepository<Entity> : IBaseRepository<Entity> where Entity : class
    {

        private readonly DbsolvexContext dbsolvexContext;
        private readonly DbSet<Entity> dbset;

        public BaseRepository(DbsolvexContext dbtechtalesContext)
        {
            this.dbsolvexContext = dbtechtalesContext;
            this.dbset = this.dbsolvexContext.Set<Entity>();
        }

        public virtual async Task<List<Entity>> GetEntity()
        {
            return await dbset.ToListAsync();
        }

        public virtual async Task<Entity> GetById(int id)
        {
            return await this.dbset.FindAsync(id);
        }

        public virtual async Task Add(Entity entity)
        {
            await this.dbset.AddAsync(entity);
        }

        public virtual async Task Update(Entity entity)
        {
            this.dbset.Update(entity);
            await Task.CompletedTask;
        }

        public virtual async Task Remove(Entity entity)
        {
            this.dbset.Remove(entity);
            await Task.CompletedTask;
        }

        public virtual async Task SaveChanges()
        {
            await this.dbsolvexContext.SaveChangesAsync();
        }

    }
}
