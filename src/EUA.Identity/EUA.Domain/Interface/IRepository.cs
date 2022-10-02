using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EUA.Domain.Interface
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        #region Async
        Task<bool> InsertAsync(TEntity model);
        Task<bool> InsertAsync(IEnumerable<TEntity> list);
        Task<bool> RemoveAsync(string uid);
        Task<bool> UpdateAsync(TEntity model);
        Task<bool> UpdateAsync(IEnumerable<TEntity> list);
        Task<bool> IsExistsAsync(object obj);
        Task<TEntity> GetAsync(string uid);
        #endregion


        #region Sync
        bool Insert(TEntity model);
        bool Insert(IEnumerable<TEntity> list);
        bool Remove(string uid);
        bool Update(TEntity model);
        bool Update(IEnumerable<TEntity> list);
        bool IsExists(object obj);
        TEntity Get(string uid);

        #endregion
    }
}
