//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Payment
    {
        public int PaymentId { get; set; }
        public int SupplierId { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string MemoNo { get; set; }
        public string PayBy { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Remarks { get; set; }
    
        public virtual Supplier Supplier { get; set; }
    }
}
