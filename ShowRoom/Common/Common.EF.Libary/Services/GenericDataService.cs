using Common.EF.Library.Contexts;
using Common.EF.Library.Exceptions;
using Common.EF.Library.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.EF.Library.Services
{
    public class GenericDataService<T> : IDataService<T> where T : EFEntiyIdItem
    {
        private readonly IContextFactory _ContextFactory;
        private readonly NonQueryDataService<T> _NonQueryDataService;

        public GenericDataService(IContextFactory contextFactory)
        {
            _ContextFactory = contextFactory;
            _NonQueryDataService = new NonQueryDataService<T>(contextFactory);
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="DbUpdateException"></exception>
        /// <exception cref="DbUpdateConcurrencyException"></exception>
        public async Task<T> Create(T entity)
        {
            return await _NonQueryDataService.Create(entity);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="DbUpdateException"></exception>
        /// <exception cref="DbUpdateConcurrencyException"></exception>
        public async Task<bool> Delete(uint id)
        {
            return await _NonQueryDataService.Delete(id);
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NoDbConnectionException"></exception>
        public async Task<T> Get(uint id)
        {
            using (DbContext context = _ContextFactory.CreateDbContext())
            {
                T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
            }
        }

        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NoDbConnectionException"></exception>
        public async Task<IEnumerable<T>> GetAll()
        {
            using (DbContext context = _ContextFactory.CreateDbContext())
            {
                IEnumerable<T> entities = await context.Set<T>().ToListAsync();
                return entities;
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
        public async Task<T> Update(uint id, T entity)
        {
            return await _NonQueryDataService.Update(id, entity);
        }
    }
}
