using Common.EF.Library.Models;

namespace ShowRoom.Domain.Entities
{

    public class Order : EFEntiyIdItem
    {

        public Offer Offer { get; set; }
        public uint OfferId { get; set; }

        public string Comment { get; set; }
        public string OrderNumber { get; set; }

        public Employee ProjectManager { get; set; }
        public uint ProjectManagerId { get; set; }

        public virtual Employee LabProjectManager { get; set; }
        public uint? LabProjectManagerId { get; set; }


    }
}
