using Common.Base;
using Common.EF.Library.Contexts;
using Common.EF.Library.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShowRoom.Domain.Contexts
{
    public class ShowRoomContextFactory : LocalizeBase, IContextFactory
    {
        private readonly string _ConnectionString;

        public ShowRoomContextFactory(string connectionString)
        {
            _ConnectionString = connectionString;
        }

        /// <summary>
        /// CreateDbContext
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NoDbConnectionException"></exception>
        public ShowRoomContext CreateDbContext()
        {
            DbContextOptionsBuilder<ShowRoomContext> options = new DbContextOptionsBuilder<ShowRoomContext>();
            options.UseSqlite(_ConnectionString);
            // options.UseMySql(_ConnectionString, x => x.ServerVersion(_ServerVersion));
            //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();


            var context = new ShowRoomContext(options.Options);

            //Ensure database is created
            context.Database.EnsureCreated();

            //context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            try
            {
                if (context.Database.CanConnect())
                {
                    return context;
                }
            }
            catch (System.Exception ex)
            {

                throw new NoDbConnectionException(Localize("Error_NoDbConnectionExeption"), ex);
            }

            throw new NoDbConnectionException(Localize("Error_NoDbConnectionExeption"));
        }

        /// <summary>
        /// CreateDbContext
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NoDbConnectionException"></exception>
        DbContext IContextFactory.CreateDbContext()
        {
            return CreateDbContext();
        }
    }
}
