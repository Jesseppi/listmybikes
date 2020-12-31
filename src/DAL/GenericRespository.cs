using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ListMyBikes.Data;
using Microsoft.EntityFrameworkCore;

namespace ListMyBikes.DAL
{
    public class GenericRespository<TEntity> where TEntity : class,IEntity
    {
        internal BikeContext context;
        internal DbSet<TEntity> dBSet;

        public GenericRespository(BikeContext context)
        {
            this.context = context;
            this.dBSet = context.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = ""
        ){
            IQueryable<TEntity> query = dBSet;

            if(filter != null){
                query = dBSet.Where(filter);
            }

            foreach(var includeProperty in includeProperties.Split(",",StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if(orderBy != null){
                return orderBy(query).ToList();
            }
            else {
                return await query.ToListAsync();
            }
        }

        public virtual async  Task<TEntity> GetByIdAsync(object id){
            return await dBSet.FindAsync(id);
        }

        public virtual async Task InsertAsync(TEntity entity){
            await dBSet.AddAsync(entity);
        }

        public virtual async Task DeleteAsync(object id){
            var entity = await dBSet.FindAsync(id);
            dBSet.Remove(entity);
        }

        public virtual bool Exists(long id) =>
            dBSet.Any<TEntity>(x => x.Id == id);

        public virtual bool Exists(TEntity entity) =>
            dBSet.Any<TEntity>(x => x.Id == entity.Id);

        public virtual void DeleteAsync(TEntity entity){
            if(context.Entry(entity).State == EntityState.Detached){
                dBSet.Attach(entity);
            }
            dBSet.Remove(entity);
        }

        public virtual void Update(TEntity entity){
            dBSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}