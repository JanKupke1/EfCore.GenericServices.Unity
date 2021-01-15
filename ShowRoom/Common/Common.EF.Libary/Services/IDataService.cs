using Common.EF.Library.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.EF.Library.Services
{
    public interface IDataService<T>
    {
        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NoDbConnectionException"></exception>
        Task<IEnumerable<T>> GetAll();
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NoDbConnectionException"></exception>
        Task<T> Get(uint id);


        /// <summary>
        /// Create
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="DbUpdateException"></exception>
        /// <exception cref="DbUpdateConcurrencyException"></exception>
        Task<T> Create(T entity);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="DbUpdateException"></exception>
        /// <exception cref="DbUpdateConcurrencyException"></exception>
        Task<T> Update(uint id, T entity);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="DbUpdateException"></exception>
        /// <exception cref="DbUpdateConcurrencyException"></exception>
        Task<bool> Delete(uint id);
    }
}
