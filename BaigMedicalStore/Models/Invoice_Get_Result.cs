
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace BaigMedicalStore.Models
{

using System;
    
public partial class Invoice_Get_Result
{

    public Nullable<long> RowNumber { get; set; }

    public int InvoiceId { get; set; }

    public System.DateTime AddedOn { get; set; }

    public int CreatedBy { get; set; }

    public int NoOfItems { get; set; }

    public decimal TotalPrice { get; set; }

    public decimal AmountRecieved { get; set; }

    public decimal DiscountAmount { get; set; }

    public bool IsActive { get; set; }

}

}
