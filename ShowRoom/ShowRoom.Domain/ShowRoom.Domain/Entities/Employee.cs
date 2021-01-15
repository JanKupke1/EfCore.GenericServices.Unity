using Common.EF.Library.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShowRoom.Domain.Entities
{

    public partial class Employee : EFEntiyIdItem
    {

        public Employee()
        {
            SalesManagerOffers = new HashSet<Offer>();
            ProjectManagerOrders = new HashSet<Order>();
            LabProjectManagerOrders = new HashSet<Order>();
        }


        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        public string LastName { get; set; }

        [Required]
        [StringLength(31)]
        public string UserName { get; set; }

        [Required]
        [StringLength(150)]
        public string Email { get; set; }

        public bool? IsActive { get; set; }


        public ICollection<Offer> SalesManagerOffers { get; set; }
        public ICollection<Order> ProjectManagerOrders { get; set; }
        public ICollection<Order> LabProjectManagerOrders { get; set; }
        
    }
}
