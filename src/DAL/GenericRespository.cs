using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ListMyBikes.Data;
using Microsoft.EntityFrameworkCore;

namespace ListMyBikes.DAL
{
    public class GenericRespository<TEntity> where TEntity : class
    {
        internal BikeContext context;
        internal DbSet<TEntity> dBSet;

        public GenericRespository(BikeContext context)
        {
            this.context = context;
            this.dBSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
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
                return query.ToList();
            }
        }

        public virtual TEntity GetById(object id){
            return dBSet.Find(id);
        }

        public virtual void Insert(TEntity entity){
            dBSet.Add(entity);
        }

        public virtual void Delete(object id){
            var entity = dBSet.Find(id);
            dBSet.Remove(entity);
        }

        public virtual void Delete(TEntity entity){
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