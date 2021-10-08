using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaigMedicalStore.ViewModel
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public int DistributorId { get; set; }

        public string DistributorName { get; set; }
        public bool IsDispatched { get; set; }
        public DateTime CreatedOn  { get; set; }
        public int CreatedBy { get; set; }

        public string EditItemUrl { get; internal set; }

        public string Distributor { get; set; }

    }
    public class OrderDetailViewModel
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public string ItemId { get; set; }
        public int Quantity { get; set; }
      
    }
}