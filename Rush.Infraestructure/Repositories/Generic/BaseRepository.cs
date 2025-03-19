using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rush.Infraestructure.Common;

namespace Rush.Infraestructure.Repositories.Generic
{
    public class BaseRepository<T> where T : class
    {
        /// <summary>
        /// The context
        /// </summary>
        protected readonly ApplicationDbContext Context;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{T}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public BaseRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual async Task<Guid> InsertAsync(T entity)
        {
            var createdAtProperty = typeof(T).GetProperty("CreatedAt");
            if (createdAtProperty != null && createdAtProperty.PropertyType == typeof(DateTime))
            {
                createdAtProperty.SetValue(entity, DateTime.UtcNow);
            }

            Context.Set<T>().Add(entity);
            await Context.SaveChangesAsync();

            return (Guid)typeof(T).GetProperty("Id")?.GetValue(entity);
        }


        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual async Task<int> UpdateAsync(T entity)
        {
            Context.Set<T>().Update(entity);
            return await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Removes the asynchronous (soft delete).
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual async Task<int> RemoveAsync(T entity)
        {
            if (typeof(T).GetProperty("IsDeleted") != null)
            {
                typeof(T).GetProperty("IsDeleted")?.SetValue(entity, true);
                Context.Set<T>().Update(entity);
            }
            else
            {
                Context.Set<T>().Remove(entity);
            }
            return await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Removes the asynchronous by id (soft delete).
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual async Task<int> RemoveAsync(Guid id)
        {
            var entity = await Context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                if (typeof(T).GetProperty("IsDeleted") != null)
                {
                    typeof(T).GetProperty("IsDeleted")?.SetValue(entity, true);
                    Context.Set<T>().Update(entity);
                }
                else
                {
                    Context.Set<T>().Remove(entity);
                }
                return await Context.SaveChangesAsync();
            }
            return 0;
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public virtual async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = Context.Set<T>();

            var isDeletable = typeof(T).GetProperty("IsDeleted") != null;

            if (isDeletable)
            {
                query = query.Where(e => EF.Property<bool>(e, "IsDeleted") == false);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }


        /// <summary>
        /// Gets the single asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public virtual async Task<T?> GetSingleAsync(Expression<Func<T, bool>>? filter = null)
        {
            var query = Context.Set<T>().AsQueryable();

            if (typeof(T).GetProperty("IsDeleted") != null)
            {
                query = query.Where(e => EF.Property<bool>(e, "IsDeleted") == false);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }
        
        public virtual async Task<T?> GetSingleWithRelationsAsync(
            Expression<Func<T, bool>>? filter = null,
            params Expression<Func<T, object>>[] includes)
        {
            var query = Context.Set<T>().AsQueryable();

            if (includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            else
            {
                var navigations = Context.Model.FindEntityType(typeof(T))?
                    .GetNavigations()
                    .Select(n => n.Name)
                    .ToList();

                if (navigations != null)
                {
                    foreach (var navigation in navigations)
                    {
                        query = query.Include(navigation);
                    }
                }
            }
            
            if (typeof(T).GetProperty("IsDeleted") != null)
            {
                query = query.Where(e => EF.Property<bool>(e, "IsDeleted") == false);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            } ;

            return query.FirstOrDefault();
        }
    }
}
