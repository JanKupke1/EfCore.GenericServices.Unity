using Microsoft.EntityFrameworkCore;
using ShowRoom.Domain.Contexts.Configurations;
using ShowRoom.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ShowRoom.Domain.Contexts
{
    public class ShowRoomContext : DbContext
    {
        public ShowRoomContext([NotNull] DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Offer> Offers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeConfig());

            modelBuilder.ApplyConfiguration(new OfferConfig());
            modelBuilder.ApplyConfiguration(new OrderConfig());


            modelBuilder.Entity<Employee>().HasData(CreateEmployees());
            modelBuilder.Entity<Offer>().HasData(CreateOffers());

            modelBuilder.Entity<Order>().HasData(CreateOrders());

            base.OnModelCreating(modelBuilder);
        }



        private Employee[] CreateEmployees()
        {
            return new Employee[] {
              new Employee {Id=1, FirstName="Tom", LastName="Araya", Email="Tom.Araya@something.de", IsActive=true, UserName="Tom.Araya"} ,
                new Employee {Id=2, FirstName="Kerry", LastName="King", Email="Kerry.King@something.de", IsActive=true, UserName="Kerry.King"} ,
                new Employee {Id=3, FirstName="Jeff", LastName="Hanneman", Email="Jeff.Hanneman@something.de", IsActive=true, UserName="Jeff.Hanneman"} ,
                new Employee {Id=4, FirstName="Dave", LastName="Lombardo", Email="Dave.Lombardo@something.de", IsActive=true, UserName="Dave.Lombardo"} ,
            };
        }

        private Offer[] CreateOffers()
        {
            Offer[] offers = new Offer[] {
                   new Offer{Id=1, OfferNumber="OfferNumber1", SalesManagerId=1},
                   new Offer{Id=2, OfferNumber="OfferNumber2", SalesManagerId=2},
                   new Offer{Id=3, OfferNumber="OfferNumber3", SalesManagerId=1},
                   new Offer{Id=4, OfferNumber="OfferNumber4", SalesManagerId=1},
                   new Offer{Id=5, OfferNumber="OfferNumber5", SalesManagerId=2},
            };

            return offers;
        }

        private Order[] CreateOrders()
        {
            return new Order[] {
                 new Order{ Id=1, OrderNumber="OrderNumber_1", Comment="Comment1" , LabProjectManagerId=1, OfferId=1, ProjectManagerId=1},
                 new Order{ Id=2, OrderNumber="OrderNumber_2", Comment="Comment2" , LabProjectManagerId=2, OfferId=2, ProjectManagerId=3},
                 new Order{ Id=3, OrderNumber="OrderNumber_3", Comment="Comment3" , LabProjectManagerId=null, OfferId=3, ProjectManagerId=4},
            };
        }
    }
}
