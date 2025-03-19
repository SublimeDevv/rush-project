﻿
using System.Linq.Expressions;


namespace Rush.Infraestructure.Repositories.Generic
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseRepository<T> where T : class
    {
        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<Guid> InsertAsync(T entity);
        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<int> UpdateAsync(T entity);
        /// <summary>
        /// Removes the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<int> RemoveAsync(T entity);
        /// <summary>
        /// Removes the asynchronous.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns></returns>
        Task<int> RemoveAsync(Guid Id);
        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
        /// <summary>
        /// Gets the single asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        Task<T?> GetSingleAsync(Expression<Func<T, bool>>? filter = null);
        
        Task<T?> GetSingleWithRelationsAsync(Expression<Func<T, bool>>? filter = null,
            params Expression<Func<T, object>>[] includes);

    }
}
