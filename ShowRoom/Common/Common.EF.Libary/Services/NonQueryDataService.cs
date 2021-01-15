using Common.EF.Library.Contexts;
using Common.EF.Library.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;

namespace Common.EF.Library.Services
{
    public class NonQueryDataService<T> where T : EFEntiyIdItem
    {
        private readonly IContextFactory _ContextFactory;

        public NonQueryDataService(IContextFactory contextFactory)
        {
            _ContextFactory = contextFactory;
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="DbUpdateException"></exception>
        /// <exception cref="DbUpdateConcurrencyException"></exception>
        /// <exception cref="Exceptions.NoDbConnectionException"></exception>
        public async Task<T> Create(T entity)
        {
            using (DbContext context = _ContextFactory.CreateDbContext())
            {
                EntityEntry<T> createdResult = await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();

                return createdResult.Entity;
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="DbUpdateException"></exception>
        /// <exception cref="DbUpdateConcurrencyException"></exception>
        /// <exception cref="Exceptions.NoDbConnectionException"></exception>
        public async Task<T> Update(uint id, T entity)
        {
            using (DbContext context = _ContextFactory.CreateDbContext())
            {
                entity.Id = id;

                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();

                return entity;
            }
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="DbUpdateException"></exception>
        /// <exception cref="DbUpdateConcurrencyException"></exception>
        /// <exception cref="Exceptions.NoDbConnectionException"></exception>
        public async Task<bool> Delete(uint id)
        {
            using (DbContext context = _ContextFactory.CreateDbContext())
            {
                T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();

                return true;
            }
        }
    }
}
