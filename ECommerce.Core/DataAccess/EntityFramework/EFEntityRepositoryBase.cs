﻿using ECommerce.Core.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ECommerce.Core.DataAccess.EntityFramework
{
    public class EFEntityRepositoryBase<TEntity, TContext>
    : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext
    {
        private readonly TContext context;
        public EFEntityRepositoryBase(TContext context)
        {
            this.context = context;
        }

        public async Task Add(TEntity entity)
        {

            var addedEntity = context.Entry(entity);
            addedEntity.State = EntityState.Added;
            await context.SaveChangesAsync();


        }

        public async Task Delete(TEntity entity)
        {


            var deletedEntity = context.Entry(entity);
            deletedEntity.State = EntityState.Deleted;
            await context.SaveChangesAsync();

        }

        public async Task DeleteList(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                var deletedEntity=context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;

            }
            await context.SaveChangesAsync();
        }
        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {


            return await context.Set<TEntity>().SingleOrDefaultAsync(filter);

        }

        public async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null ? await context.Set<TEntity>().ToListAsync() : await context.Set<TEntity>().Where(filter).ToListAsync();

        }

        public async Task Update(TEntity entity)
        {
            var updatedEntity = context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}