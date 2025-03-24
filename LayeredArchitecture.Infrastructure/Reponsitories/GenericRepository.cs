﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using LayeredArchitecture.Domain.Models;
using LayeredArchitecture.Infrastructure.Utils;
using LayeredArchitecture.Domain.IReponsitories;
using Microsoft.AspNetCore.Http;

namespace caresystem_data_bussiness.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseCommonModel
    {
        internal LayeredArchitectureContext context;
        internal DbSet<TEntity> dbSet;
        private readonly IHttpContextAccessor _contextAccessor;

        public GenericRepository(LayeredArchitectureContext context, IHttpContextAccessor contextAccessor)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
            _contextAccessor = contextAccessor;
        }     

        #region asynchronous
        public async Task<List<TEntity>> GetAllAsync(bool tracking = false)
        {
            return tracking
                ? await context.Set<TEntity>().Where(e => e.delete_flg != true).ToListAsync()
                : await context.Set<TEntity>().Where(e => e.delete_flg != true).AsNoTracking().ToListAsync();
        }

        public async Task<List<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> expression, bool tracking = false)
        {
            return tracking
                ? await context.Set<TEntity>().Where(expression).ToListAsync()
                : await context.Set<TEntity>().AsNoTracking().Where(expression).ToListAsync();
        }

        public async Task<bool> AnyByConditionAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await context.Set<TEntity>().AnyAsync(expression);
        }

        public async Task<TEntity?> GetByIdAsync(int id, bool tracking = false)
        {
            return await FindByCondition(x => x.id.Equals(id) && x.delete_flg != true, tracking).FirstOrDefaultAsync();
        }

        public async Task<int> CreateAsync(TEntity entity)
        {
            var now = DateTime.Now;

            entity.created_at = now;
            entity.created_user ??= GetLoginUserId();
            entity.delete_flg = false;

            await context.Set<TEntity>().AddAsync(entity);
            await context.SaveChangesAsync();
            return entity.id;
        }

        public async Task<IList<int>> CreateListAsync(IEnumerable<TEntity> entities)
        {
            var now = DateTime.Now;

            foreach (var entity in entities)
            {
                entity.created_at = now;
                entity.created_user = GetLoginUserId();
                entity.updated_at = now;
                entity.updated_user = GetLoginUserId();
                entity.delete_flg = false;
            }
            await context.Set<TEntity>().AddRangeAsync(entities);
            await context.SaveChangesAsync();
            return entities.Select(e => e.id).ToList();
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            var now = DateTime.Now;
            if (context.Entry(entity).State == EntityState.Unchanged) return true;

            entity.updated_at = now;
            entity.updated_user = GetLoginUserId();

            TEntity exist = context.Set<TEntity>().Find(entity.id);
            context.Entry(exist).CurrentValues.SetValues(entity);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateListAsync(IEnumerable<TEntity> entities)
        {
            var now = DateTime.Now;
            foreach (var entity in entities)
            {
                entity.updated_at = now;
                entity.updated_user = GetLoginUserId();
            }
            context.Set<TEntity>().UpdateRange(entities);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var obj = await GetByIdAsync(id);

            if (obj != null)
            {
                obj.delete_flg = true;
                obj.updated_at = DateTime.Now;
                obj.updated_user = GetLoginUserId();

                context.Attach(obj);
                context.Entry(obj).Property(x => x.delete_flg).IsModified = true;
                return await context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> SoftDeleteListAsync(IEnumerable<int> ids)
        {
            var objs = await FindByConditionAsync(x => ids.Contains(x.id));
            var now = DateTime.Now;
            if (objs != null && objs.Count > 0)
            {
                foreach (var obj in objs)
                {
                    obj.delete_flg = true;
                    obj.updated_at = now;
                    obj.updated_user = GetLoginUserId();

                    context.Attach(obj);
                    context.Entry(obj).Property(x => x.delete_flg).IsModified = true;
                }
                return await context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await context.Database.BeginTransactionAsync();
        }

        public async Task EndTransactionAsync()
        {
            await context.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await context.Database.RollbackTransactionAsync();
        }
        #endregion

        #region synchronous
        public IQueryable<TEntity> GetAll(bool tracking = false)
        {
            return tracking
                ? context.Set<TEntity>().Where(e => e.delete_flg != true)
                : context.Set<TEntity>().Where(e => e.delete_flg != true).AsNoTracking();
        }

        private Expression<Func<TEntity, bool>> AddDeleteFlgCondition(Expression<Func<TEntity, bool>> expression)
        {
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var deleteFlgProperty = Expression.Property(parameter, "delete_flg");
            var condition = Expression.NotEqual(deleteFlgProperty, Expression.Constant(true, typeof(bool?)));

            var body = Expression.AndAlso(condition, Expression.Invoke(expression, parameter));
            return Expression.Lambda<Func<TEntity, bool>>(body, parameter);
        }

        public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression, bool tracking = false)
        {
            var combinedExpression = AddDeleteFlgCondition(expression);

            return tracking
                ? context.Set<TEntity>().Where(combinedExpression)
                : context.Set<TEntity>().AsNoTracking().Where(combinedExpression);
        }

        public bool AnyByCondition(Expression<Func<TEntity, bool>> expression)
        {
            return context.Set<TEntity>().Any(expression);
        }

        public TEntity? GetById(int id, bool tracking = false)
        {
            return FindByCondition(x => x.id.Equals(id), tracking).FirstOrDefault();
        }

        public int Create(TEntity entity)
        {
            var now = DateTime.Now;

            entity.created_at = now;
            entity.created_user = GetLoginUserId();
            entity.updated_at = null;
            entity.updated_user = null;
            entity.delete_flg = false;

            context.Set<TEntity>().Add(entity);
            context.SaveChanges();
            return entity.id;
        }

        public IList<int> CreateList(IEnumerable<TEntity> entities)
        {
            var now = DateTime.Now;

            foreach (var entity in entities)
            {
                entity.created_at = now;
                entity.created_user = GetLoginUserId();
                entity.updated_at = null;
                entity.updated_user = null;
                entity.delete_flg = false;
                context.Set<TEntity>().Add(entity);
            }
            context.SaveChanges();
            return entities.Select(e => e.id).ToList();
        }

        public bool UpdateEntity(TEntity entity)
        {
            var now = DateTime.Now;
            if (context.Entry(entity).State == EntityState.Unchanged) return true;

            entity.updated_at = now;
            entity.updated_user = GetLoginUserId();

            TEntity exist = context.Set<TEntity>().Find(entity.id);
            if (exist == null) return false;
            context.Entry(exist).CurrentValues.SetValues(entity);
            return context.SaveChanges() > 0;
        }

        public bool UpdateList(IEnumerable<TEntity> entities)
        {
            var now = DateTime.Now;
            foreach (var entity in entities)
            {
                entity.updated_at = now;
                entity.updated_user = GetLoginUserId();
            }
            context.Set<TEntity>().UpdateRange(entities);
            return context.SaveChanges() == entities.Count();
        }

        public bool SoftDelete(int id)
        {
            var obj = GetById(id);
            if (obj != null)
            {
                var now = DateTime.Now;
                obj.delete_flg = true;
                obj.updated_at = now;
                obj.updated_user = GetLoginUserId();

                context.Attach(obj);
                context.Entry(obj).Property(x => x.delete_flg).IsModified = true;
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool SoftDeleteList(IEnumerable<int> ids)
        {
            var now = DateTime.Now;
            var objs = FindByCondition(x => ids.Contains(x.id), true);
            if (objs != null && objs.Count() > 0)
            {
                foreach (var obj in objs)
                {
                    obj.delete_flg = true;
                    obj.delete_flg = true;
                    obj.updated_at = now;
                    obj.updated_user = GetLoginUserId();

                    context.Attach(obj);
                    context.Entry(obj).Property(x => x.delete_flg).IsModified = true;
                }
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool HardDelete(int id)
        {
            var obj = FindByCondition(x => x.id.Equals(id)).FirstOrDefault();
            if (obj != null)
            {
                context.Set<TEntity>().Remove(obj);
            }
            return context.SaveChanges() > 0;
        }

        public bool HardDeleteList(IEnumerable<int> ids)
        {
            var objs = FindByCondition(x => ids.Contains(x.id));
            if (objs != null && objs.Count() > 0)
            {
                context.Set<TEntity>().RemoveRange(objs);
            }
            return context.SaveChanges() == ids.Count();
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return context.Database.BeginTransaction();
        }

        public void EndTransaction()
        {
            context.Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            context.Database.RollbackTransaction();
        }
        #endregion

        public string GetLoginUserId()
        {
            var userId = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userid");
            if (userId == null) return 1.ToString();
            else
            {
                return userId.Value;
            }
        }

        
    }
}
