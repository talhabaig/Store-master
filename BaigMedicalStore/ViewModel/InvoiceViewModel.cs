using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaigMedicalStore.Models
{
    public class InvoiceModel
    {
        public string Name { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public int Discount { get; set; }
        public int? TotalStock { get; set; }
        public int? LimitNotification { get; set; } 
        public int? Pieceinpack { get; set; }
        public string Code { get; set; } 
        public decimal? SalePrice { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal? UnitPrice { get; set; }
    }

    public class GetData
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class InvoiceViewModel
    {
        public int InvoiceId { get; set; }
        public DateTime AddedOn { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal AmountRecieved { get; set; }
        public decimal Discount { get; set; }
        public int NoOfItems { get; set; }

        public string ViewInvoiceUrl { get; set; }
        public List<InvoiceDetailViewModel> InvDetList { get; set; }
        public int InvoiceNo { get; internal set; }

        public InvoiceViewModel()
        {
            InvDetList = new List<InvoiceDetailViewModel>();
        }
    }

    public class InvoiceDetailViewModel
    {
        public int InvoiceDetailId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public int Discount { get; set; }
        public string Code { get; set; }
        public int UnitPrice { get; set; }
        public int TotalPrice { get; set; }
    }
}