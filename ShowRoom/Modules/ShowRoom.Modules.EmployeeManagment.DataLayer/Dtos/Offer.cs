using Common.EF.Library.Models;
using System.Collections.Generic;

namespace ShowRoom.Domain.Entities
{
    public class Offer : EFEntiyIdItem
    {
        public Offer()
        {
            Orders = new HashSet<Order>();
        }

        public string OfferNumber { get; set; }

        public Employee SalesManager { get; set; }
        public uint SalesManagerId { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}