using Microsoft.EntityFrameworkCore;

namespace Common.EF.Library.Contexts
{
    public interface IContextFactory
    {
        /// <summary>
        /// CreateDbContext
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exceptions.NoDbConnectionException"></exception>
        DbContext CreateDbContext();
    }
}