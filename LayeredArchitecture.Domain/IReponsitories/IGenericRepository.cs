using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using LayeredArchitecture.Domain.Models;
namespace LayeredArchitecture.Domain.IReponsitories
{
    public interface IGenericRepository<TEntity> where TEntity : BaseCommonModel
    {
        
        #region asynchronous
        Task<List<TEntity>> GetAllAsync(bool tracking = false);

        Task<List<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> expression, bool tracking = false);

        Task<bool> AnyByConditionAsync(Expression<Func<TEntity, bool>> expression);

        Task<TEntity?> GetByIdAsync(int id, bool tracking = false);

        Task<int> CreateAsync(TEntity entity);

        Task<IList<int>> CreateListAsync(IEnumerable<TEntity> entities);

        Task<bool> UpdateAsync(TEntity entity);

        Task<bool> UpdateListAsync(IEnumerable<TEntity> entities);

        Task<bool> SoftDeleteAsync(int id);

        Task<bool> SoftDeleteListAsync(IEnumerable<int> ids);

        Task<int> SaveChangesAsync();

        Task<IDbContextTransaction> BeginTransactionAsync();

        Task EndTransactionAsync();

        Task RollbackTransactionAsync();
        #endregion

        #region synchronous
        IQueryable<TEntity> GetAll(bool tracking = false);

        IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression, bool tracking = false);

        bool AnyByCondition(Expression<Func<TEntity, bool>> expression);

        TEntity? GetById(int id, bool tracking = false);

        int Create(TEntity entity);

        IList<int> CreateList(IEnumerable<TEntity> entities);

        bool UpdateEntity(TEntity entity);

        bool UpdateList(IEnumerable<TEntity> entities);

        bool SoftDelete(int id);

        bool SoftDeleteList(IEnumerable<int> ids);

        bool HardDelete(int id);

        bool HardDeleteList(IEnumerable<int> ids);

        int SaveChanges();

        IDbContextTransaction BeginTransaction();

        void EndTransaction();

        void RollbackTransaction();

        #endregion

        string GetLoginUserId();
    }
}
